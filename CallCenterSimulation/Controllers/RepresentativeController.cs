using Microsoft.AspNetCore.Mvc;
using CallCenterSimulation.Models;

namespace CallCenterSimulation.Controllers
{
    public class RepresentativeController : Controller
    {
        private const string KullaniciAdi = "temsilci";
        private const string Sifre = "1234";

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string kullaniciAdi, string sifre)
        {
            if (kullaniciAdi == KullaniciAdi && sifre == Sifre)
            {
                // Giriş başarılı
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Hata = "Kullanıcı adı veya şifre yanlış!";
                return View();
            }
        }

        public IActionResult Dashboard()
        {
            var musteriler = DataStore.MusteriKuyrugu.ElemanlariGetir();
            return View(musteriler);
        }

        [HttpPost]
        public IActionResult ServeCustomer()
        {
            if (!DataStore.MusteriKuyrugu.KuyrukBos())
            {
                DataStore.MusteriKuyrugu.KuyrukSil();
            }

            return RedirectToAction("Dashboard");
        }
    }
}
