// Models/DashboardViewModel.cs
namespace CallCenterSimulation.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Customer> BekleyenMusteriler { get; set; }
        public IEnumerable<Customer> TamamlananMusteriler { get; set; }
    }
}
