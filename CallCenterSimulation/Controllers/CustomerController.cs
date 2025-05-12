using Microsoft.AspNetCore.Mvc;
using CallCenterSimulation.Models;
using CallCenterSimulation.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenterSimulation.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHubContext<CallCenterHub> _hubContext;

        public CustomerController(IHubContext<CallCenterHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // Müşteri formu
        public IActionResult Index()
        {
            // Daha önce hata mesajı varsa göster
            if (TempData["Hata"] != null)
            {
                ViewBag.Hata = TempData["Hata"].ToString();
            }

            return View();
        }

        // Müşteri talep gönderme işlemi
        [HttpPost]
        public async Task<IActionResult> Create(string ad, string talep)
        {
            int hash = ad.GetHashCode();

            // Eğer müşteri daha önce temsilci tarafından işleme alındıysa ama aynı adla tekrar gelmek istiyorsa,
            // kuyrukta yoksa eski kaydı temizlemesine izin ver
            if (DataStore.AktifMusteriler.ContainsKey(hash) &&
                !DataStore.MusteriKuyrugu.ElemanlariGetir().Any(m => m.Ad == ad))
            {
                DataStore.AktifMusteriler.Remove(hash);
            }

            // Hâlâ aynı isimle kuyrukta olan bir müşteri varsa, uyarı ver
            if (DataStore.MusteriKuyrugu.ElemanlariGetir().Any(m => m.Ad == ad))
            {
                TempData["Hata"] = "Bu isimle zaten bir talep var. Lütfen farklı bir isim giriniz.";
                return RedirectToAction("Index");
            }

            var musteri = new Customer
            {
                Ad = ad,
                Talep = talep
            };

            // Kuyruğa ekle
            DataStore.MusteriKuyrugu.KuyrugaEkle(musteri);

            // Aktif müşterilere ekle (Dictionary)
            DataStore.AktifMusteriler[hash] = musteri;

            // SignalR ile kuyruk güncellemesini bildir
            await _hubContext.Clients.All.SendAsync("UpdateQueue");

            // Müşteri adını TempData ile Success ekranına taşı
            TempData["Ad"] = ad;

            return RedirectToAction("Success");
        }

        // Talep başarıyla alındı sayfası
        public IActionResult Success()
        {
            ViewBag.Ad = TempData["Ad"]?.ToString();
            return View();
        }

        // Temsilciden müşteri cevabını al
        [HttpPost]
        public IActionResult ReceiveCustomerAnswer(string ad, string cevap)
        {
            TempData["TemsilciCevabi"] = cevap;
            TempData["Ad"] = ad;
            return View("SuccessWithAnswer");
        }

        // Geri bildirim formunu göster
        [HttpGet]
        public IActionResult Feedback()
        {
            var ad = TempData["Ad"]?.ToString();
            var cevap = TempData["TemsilciCevabi"]?.ToString();
            TempData.Keep("Ad");
            TempData.Keep("TemsilciCevabi");

            // Get all feedbacks from the file
            var allFeedbacks = FeedbackStorage.LoadFeedbacks();

            // Pass the feedbacks to the view
            ViewBag.Ad = ad;
            ViewBag.Cevap = cevap;
            ViewBag.AllFeedbacks = allFeedbacks;

            return View();
        }


        // Geri bildirimi al ve sakla
        [HttpPost]
        public IActionResult Feedback(string ad, int puan, string geriBildirim)
        {
            var feedback = new CustomerFeedback
            {
                Ad = ad,
                Puan = puan,
                GeriBildirim = geriBildirim
            };

            // Save the feedback to the JSON file
            FeedbackStorage.SaveFeedback(feedback);

            // Optional: Store feedback into DataStore (stack) for temporary session usage
            DataStore.GeriBildirimler.Push(feedback);

            return View("Thanks", feedback);
        }





    }
}
