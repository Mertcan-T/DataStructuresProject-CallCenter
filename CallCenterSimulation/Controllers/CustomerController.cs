using Microsoft.AspNetCore.Mvc;
using CallCenterSimulation.Models;
using CallCenterSimulation.Hubs;
using Microsoft.AspNetCore.SignalR;

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
            return View();
        }

        // Müşteri talep gönderme işlemi
        [HttpPost]
        public async Task<IActionResult> Create(string ad, string talep)
        {
            var musteri = new Customer
            {
                Ad = ad,
                Talep = talep
            };

            // Kuyruğa ekle
            DataStore.MusteriKuyrugu.KuyrugaEkle(musteri);

            // Aktif müşterilere ekle (Dictionary)
            DataStore.AktifMusteriler[ad.GetHashCode()] = musteri;

            // Kuyruk güncellendiğini tüm istemcilere bildir
            await _hubContext.Clients.All.SendAsync("UpdateQueue");

            // TempData ile ad bilgisini Success sayfasına taşı
            TempData["Ad"] = ad;

            return RedirectToAction("Success");
        }

        // Talep başarıyla alındı sayfası
        public IActionResult Success()
        {
            return View();
        }

        // Cevap alındığında temsilci cevabını göster
        [HttpPost]
        public IActionResult ReceiveCustomerAnswer(string ad, string cevap)
        {
            TempData["TemsilciCevabi"] = cevap;
            TempData["Ad"] = ad;
            return View("SuccessWithAnswer");
        }

        // Geri bildirim formu gösterimi
        [HttpGet]
        public IActionResult Feedback()
        {
            var ad = TempData["Ad"]?.ToString();
            var cevap = TempData["TemsilciCevabi"]?.ToString();
            TempData.Keep("Ad");
            TempData.Keep("TemsilciCevabi");

            ViewBag.Ad = ad;
            ViewBag.Cevap = cevap;

            return View();
        }

        // Geri bildirimi alma ve kaydetme
        [HttpPost]
        public IActionResult Feedback(string ad, int puan, string geriBildirim)
        {
            var feedback = new CustomerFeedback
            {
                Ad = ad,
                Puan = puan,
                GeriBildirim = geriBildirim
            };

            DataStore.GeriBildirimler.Push(feedback);

            // Modeli Thanks.cshtml sayfasına gönder
            return View("Thanks", feedback);
        }
    }
}
