using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ProgramPartListWeb.Hubs
{
    [HubName("stockHub")]
    public class StockHub : Hub
    {
        public void SendStockAlert(string partName, int quantity)
        {
            Clients.All.receiveStockAlert(partName, quantity);
        }
    }
}