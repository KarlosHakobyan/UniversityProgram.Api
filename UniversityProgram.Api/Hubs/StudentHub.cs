using Microsoft.AspNetCore.SignalR;

namespace UniversityProgram.Api.Hubs
{
    public class StudentHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
            await Clients.All.SendAsync("OnUpdate", message);
            await Clients.All.SendAsync("UpdateMessage", message);
            await Clients.All.SendAsync("DeleteMessage", message);
        }
    }
}
