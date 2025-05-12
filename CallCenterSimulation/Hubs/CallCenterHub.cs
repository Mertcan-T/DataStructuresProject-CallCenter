using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CallCenterSimulation.Hubs
{
    public class CallCenterHub : Hub
    {
        public async Task SendCustomerRequest(string ad, string talep, int siraNumarasi)
        {
            await Clients.All.SendAsync("ReceiveRequest", ad, talep, siraNumarasi);
        }

        public async Task NotifyQueueUpdated()
        {
            await Clients.All.SendAsync("UpdateQueue");
        }

        public async Task SendResponseToCustomer(string ad, string yanit)
        {
            await Clients.All.SendAsync("ReceiveResponse", ad, yanit);
        }
    }
}
