using Microsoft.AspNetCore.Mvc;
using CallCenterSimulation.Models;

namespace CallCenterSimulation.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string ad, string talep)
        {
            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(talep))
            {
                ViewBag.Hata = "Lütfen tüm alanları doldurun.";
                return View();
            }

            Customer yeniMusteri = new Customer
            {
                Ad = ad,
                Talep = talep,
                SiraNumarasi = DataStore.MusteriKuyrugu.ElemanSayisi() + 1
            };

            DataStore.MusteriKuyrugu.KuyrugaEkle(yeniMusteri);

            ViewBag.Basarili = "Talebiniz alınmıştır! Sıra Numaranız: " + yeniMusteri.SiraNumarasi;
            return View();
        }
    }
}
