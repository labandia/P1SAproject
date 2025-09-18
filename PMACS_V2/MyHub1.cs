using Microsoft.AspNet.SignalR;

namespace PMACS_V2
{
    public class MyHub1 : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}