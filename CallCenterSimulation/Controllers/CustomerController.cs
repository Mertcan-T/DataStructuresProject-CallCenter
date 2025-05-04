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
            var musteri = new Customer { Ad = ad, Talep = talep };

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

        // Müşteri geri bildirim gönderir
        [HttpPost]
        public IActionResult GeriBildirim(string ad, int puan)
        {
            DataStore.TemsilciLoglari.AddLast($"{DateTime.Now}: {ad} adlı müşteri {puan} puan verdi.");
            TempData["GeriBildirildi"] = "Geri bildiriminiz için teşekkür ederiz!";
            return RedirectToAction("Index");
        }

        // Cevap alınması için SignalR dinleyici ekliyoruz
        [HttpPost]
        public IActionResult ReceiveCustomerAnswer(string ad, string cevap)
        {
            TempData["TemsilciCevabi"] = cevap;
            return RedirectToAction("Success");
        }
    }
}
