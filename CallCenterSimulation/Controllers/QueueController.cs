using Microsoft.AspNetCore.Mvc;
using CallCenterSimulation.Models;

namespace CallCenterSimulation.Controllers.Api
{
    [Route("api/queue")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        // Sıra bilgisi döndüren API
        [HttpGet("position")]
        public IActionResult GetPosition(string name)
        {
            var kuyruk = DataStore.MusteriKuyrugu.ElemanlariGetir();

            // Müşterinin sırasını bulmak için kuyrukta döngü yapıyoruz
            for (int i = 0; i < kuyruk.Count; i++)
            {
                if (kuyruk[i].Ad.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    // Sıra numarasını döndürüyoruz (1'den başlıyor)
                    return Ok((i + 1).ToString());
                }
            }

            // Eğer müşteri kuyruğa bulunamadıysa, uygun bir mesaj dönüyoruz
            return Ok("Talep bulunamadı.");
        }
    }
}
