using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace iotAC_SignalR_Server
{
    //SignalR Hub
    public class BroadcastHub:Hub
    {
        public Task broadcast(string command)
        {
            return Clients.All.InvokeAsync("messageReceived", command);
        }
    }
}
