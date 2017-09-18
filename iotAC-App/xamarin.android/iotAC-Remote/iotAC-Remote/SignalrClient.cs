using System;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace iotAC_Remote
{
    class SignalrClient
    {
        public HubConnection connection;

        public async Task connet()
        {
            //與asp.net core signalr server連線
            //與asp.net signalr不同的地方在於：網址後面務必加上signalr server hub class name(小寫)
            connection = await ConnectAsync("http://iotac-signalr-server20170916093639.azurewebsites.net/broadcastHub");
        }

        public async Task<HubConnection> ConnectAsync(string baseUrl)
        {
            // Keep trying to until we can start
            // 不斷嘗試，直到連接成功
            while (true)
            {
                var connection = new HubConnectionBuilder()
                                .WithUrl(baseUrl)
                                .WithConsoleLogger(LogLevel.Trace)
                                .Build();
                try
                {
                    await connection.StartAsync();
                    return connection;
                }
                catch (Exception)
                {
                    await Task.Delay(1000);
                }
            }
        }

        public async Task send(string command)
        {
            //透過signalr server向其他client發送訊息
            await connection.InvokeAsync("broadcast", command);
        }
    }
}