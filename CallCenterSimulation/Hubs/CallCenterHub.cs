using Microsoft.AspNetCore.SignalR;

namespace CallCenterSimulation.Hubs
{
    public class CallCenterHub : Hub
    {
        // Müşteri talebi oluşturulduğunda tetiklenir
        public async Task SendCustomerRequest(string customerName, string request)
        {
            await Clients.All.SendAsync("ReceiveRequest", customerName, request);
        }

        // Temsilci müşteriyi çağırdığında kuyruk güncellenir
        public async Task NotifyQueueUpdated()
        {
            await Clients.All.SendAsync("UpdateQueue");
        }
    }
}
