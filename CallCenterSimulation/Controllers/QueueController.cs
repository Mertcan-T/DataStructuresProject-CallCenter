using Microsoft.AspNetCore.Mvc;
using CallCenterSimulation.Models;

namespace CallCenterSimulation.Controllers.Api
{
    [Route("api/queue")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        [HttpGet("position")]
        public IActionResult GetPosition(string name)
        {
            var kuyruk = DataStore.MusteriKuyrugu.ElemanlariGetir();
            for (int i = 0; i < kuyruk.Count; i++)
            {
                if (kuyruk[i].Ad.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return Ok((i + 1).ToString());
                }
            }
            return Ok("Sırada değilsiniz");
        }
    }
}
