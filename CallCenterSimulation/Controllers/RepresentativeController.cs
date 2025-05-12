using CallCenterSimulation.Data;
using Microsoft.AspNetCore.Mvc;
using CallCenterSimulation.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using CallCenterSimulation.Models;

namespace CallCenterSimulation.Controllers
{
    public class RepresentativeController : Controller
    {
        private readonly IHubContext<CallCenterHub> _hubContext;

        public RepresentativeController(IHubContext<CallCenterHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Dashboard()
        {
            var musteriler = DataStore.MusteriKuyrugu.ElemanlariGetir()
                .Select((m, i) =>
                {
                    m.SiraNumarasi = i + 1;
                    return m;
                }).ToList();

            var tamamlanan = DataStore.TemsilciLoglari
                .Select(log => new Customer { Ad = log, Talep = "" })
                .ToList();

            var vm = new DashboardViewModel
            {
                BekleyenMusteriler = musteriler,
                TamamlananMusteriler = tamamlanan
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> YanitGonder(int id, string yanitMesaji)
        {
            // Kuyruğun ilk elemanını al
            var musteri = DataStore.MusteriKuyrugu.ElemanlariGetir().FirstOrDefault(m => m.Id == id);

            // Müşteri var mı kontrol et
            if (musteri == null)
                return RedirectToAction("Dashboard");

            // Kuyruğun ilk elemanını kontrol et
            var ilkMusteri = DataStore.MusteriKuyrugu.ElemanlariGetir().FirstOrDefault();

            // Eğer gelen müşteri, kuyruğun ilk elemanı değilse, işlem yapılmasın
            if (musteri != ilkMusteri)
            {
                // Eğer ilk müşteri dışında bir müşteri işlem yapmaya çalışıyorsa, uyarı ver
                return RedirectToAction("Dashboard");
            }

            // Kuyruktan çıkar
            DataStore.MusteriKuyrugu.KuyrukSil(); // İlk müşteri kuyruğundan çıkarılır

            // Stack ve LinkedList güncellemeleri
            DataStore.IslemGecmisi.Push($"{musteri.Ad} - {DateTime.Now}: {yanitMesaji}");
            DataStore.TemsilciLoglari.AddLast($"{musteri.Ad}: {yanitMesaji} ({DateTime.Now})");

            // Müşteri aktif listeden çıkar
            if (DataStore.AktifMusteriler.ContainsKey(musteri.Id))
                DataStore.AktifMusteriler.Remove(musteri.Id);

            // SignalR ile müşteriye mesaj gönder
            await _hubContext.Clients.All.SendAsync("ReceiveResponse", musteri.Ad, yanitMesaji);
            await _hubContext.Clients.All.SendAsync("UpdateQueue");

            return RedirectToAction("Dashboard");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string kullaniciAdi, string sifre)
        {
            if (kullaniciAdi == "temsilci" && sifre == "1234")
                return RedirectToAction("Dashboard");

            ViewBag.Hata = "Kullanıcı adı veya şifre yanlış!";
            return View();
        }

        // GERİ BİLDİRİM LİSTESİ GÖRÜNTÜLEME
        public IActionResult GeriBildirimListesi()
        {
            // Stack'teki geri bildirimleri alıyoruz
            var geriBildirimlerStack = FeedbackStorage.LoadFeedbacks();

            // Stack'teki geri bildirimleri bir listeye dönüştürüyoruz
            if (geriBildirimlerStack == null)
            {
                geriBildirimlerStack = new Stack<CustomerFeedback>(); // Stack boşsa yeni bir Stack oluştur
            }

            var geriBildirimlerList = geriBildirimlerStack.ToList(); // Stack'teki elemanları listeye çeviriyoruz

            // Geri bildirimleri View'a gönderiyoruz
            return View(geriBildirimlerList); // Listeyi View'a gönderiyoruz
        }


    }
}
