using CallCenterSimulation.Data;
using Microsoft.AspNetCore.Mvc;
using CallCenterSimulation.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
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

        public IActionResult Dashboard()
        {
            // Bekleyen müşteriler
            var musteriler = DataStore.MusteriKuyrugu.ElemanlariGetir();

            // Müşterilere sıra numarası ver (0'dan başlasın, sonra 1'e ekleyelim)
            var musterilerWithSira = musteriler.Select((musteri, index) => {
                musteri.SiraNumarasi = index + 1;  // 1'den başlatmak için 1 ekliyoruz
                return musteri;
            }).ToList();

            // Tamamlanan müşteriler
            var tamamlananMusteriler = DataStore.TemsilciLoglari
                .Select(log => new Customer
                {
                    Id = 0, // Opsiyonel Id için bir değer verilebilir.
                    Ad = log,
                    Talep = ""
                })
                .ToList();

            var viewModel = new DashboardViewModel
            {
                BekleyenMusteriler = musterilerWithSira, // Güncellenmiş listeyi kullanıyoruz
                TamamlananMusteriler = tamamlananMusteriler
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ServeCustomer()
        {
            if (DataStore.MusteriKuyrugu.KuyrukBos())
            {
                // Kuyruk boşsa işlem yapılmaz
                return RedirectToAction("Dashboard");
            }

            // Kuyruktan müşteri al
            var musteri = DataStore.MusteriKuyrugu.KuyrukSil();

            // Müşteriye sıra numarası ver
            musteri.SiraNumarasi = DataStore.MusteriKuyrugu.ElemanlariGetir().Count + 1;

            // Stack: geçmiş kaydını ekle
            DataStore.IslemGecmisi.Push($"{musteri.Ad} - {DateTime.Now}: Talep işlendi.");

            // LinkedList: temsilci logunu ekle
            DataStore.TemsilciLoglari.AddLast($"Temsilci, {musteri.Ad} adlı müşterinin talebini işledi. ({DateTime.Now})");

            // Dictionary: aktif müşteri kaldır
            if (DataStore.AktifMusteriler.ContainsKey(musteri.Id))
            {
                DataStore.AktifMusteriler.Remove(musteri.Id);
            }

            // 1️⃣ "Talep işleniyor" mesajı gönder
            await _hubContext.Clients.All.SendAsync("ReceiveResponse", musteri.Ad, "Talebiniz temsilci tarafından işleniyor...");

            // 2️⃣ 2 saniye sonra tamamlandı mesajı gönder
            await Task.Delay(2000);
            await _hubContext.Clients.All.SendAsync("ReceiveResponse", musteri.Ad, "Talebiniz başarıyla tamamlandı!");

            // 3️⃣ Kuyruk güncellemesi gönder
            await _hubContext.Clients.All.SendAsync("UpdateQueue");

            return RedirectToAction("Dashboard");
        }
    }
}
