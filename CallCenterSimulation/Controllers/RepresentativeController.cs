using Microsoft.AspNetCore.Mvc;
using CallCenterSimulation.Models;
using CallCenterSimulation.Hubs;
using Microsoft.AspNetCore.SignalR;

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
            var musteriler = DataStore.MusteriKuyrugu.ElemanlariGetir();
            return View(musteriler);
        }

        [HttpPost]
        public async Task<IActionResult> ServeCustomer()
        {
            if (!DataStore.MusteriKuyrugu.KuyrukBos())
            {
                var silinen = DataStore.MusteriKuyrugu.KuyrukSil();

                // 1️⃣ Çağırılan müşteriye özel mesaj gönder
                await _hubContext.Clients.All.SendAsync("ReceiveResponse", silinen.Ad, "Talebiniz temsilci tarafından işleniyor.");

                // 2️⃣ Tüm istemcilere sıra güncelleme bildirimi
                await _hubContext.Clients.All.SendAsync("UpdateQueue");
            }

            return RedirectToAction("Dashboard");
        }
    }
}
