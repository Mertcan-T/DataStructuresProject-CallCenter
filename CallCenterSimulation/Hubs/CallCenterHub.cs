using Microsoft.AspNetCore.SignalR;

public class CallCenterHub : Hub
{
    // Müşteri talebi oluşturulduğunda çağrılacak metod
    public async Task SendCustomerRequest(string customerName, string request)
    {
        // Temsilciye talep bildirimi gönder
        await Clients.All.SendAsync("ReceiveRequest", customerName, request);
    }

    // Temsilci talebi işlediğinde, müşteriye yanıt gönderilecek
    public async Task SendResponseToCustomer(string customerName, string response)
    {
        await Clients.All.SendAsync("ReceiveResponse", customerName, response);
    }
}
