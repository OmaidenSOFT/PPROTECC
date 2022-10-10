using Microsoft.AspNet.SignalR;

namespace WebApplication1.Controllers
{
    public class ChatHub : Hub
    {
        public void SendMessage(string userName, string message)
        {
            Clients.All.showMessage("Hi: " + userName);
        }
    }
}