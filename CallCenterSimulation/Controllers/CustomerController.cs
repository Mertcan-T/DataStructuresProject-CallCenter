using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using CallCenterSimulation.Models;
using CallCenterSimulation.Hubs;

namespace CallCenterSimulation.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHubContext<CallCenterHub> _hubContext;

        public CustomerController(IHubContext<CallCenterHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // Talep oluşturma formu
        public IActionResult Create()
        {
            return View();
        }

        // Talep gönderildiğinde çalışır
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Sıra numarasını belirle ve sıraya ekle
                customer.SiraNumarasi = DataStore.MusteriKuyrugu.ElemanSayisi() + 1;
                DataStore.MusteriKuyrugu.KuyrugaEkle(customer);

                // SignalR üzerinden temsilcilere bildir
                await _hubContext.Clients.All.SendAsync("ReceiveRequest", customer.Ad, customer.Talep);

                // Müşteri adını Success sayfasına TempData ile aktar
                TempData["Ad"] = customer.Ad;

                // Success sayfasına yönlendir
                return RedirectToAction("Success");
            }

            return View(customer);
        }

        // Talep başarılı sayfası
        public IActionResult Success()
        {
            return View();
        }
    }
}
