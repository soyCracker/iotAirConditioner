using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace iotAC_PiIR
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            connet().Wait();
        }

        static async Task connet()
        {
            //與asp.net core signalr server連線
            //與asp.net signalr不同的地方在於：網址後面務必加上signalr server hub class name(小寫)
            HubConnection connection = await ConnectAsync("http://iotac-signalr-server20170916093639.azurewebsites.net/broadcastHub");
            //持續聆聽是否有新訊息
            while (true)
            {
                //若收到訊息就執行irSend(command)
                connection.On<string>("messageReceived", command=> 
                {
                    irSend(command);
                });               
            }
        }

        public static async Task<HubConnection> ConnectAsync(string baseUrl)
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

        //用shell command 呼叫 lirc 指令來發射紅外線訊號
        public static void irSend(string command)
        {
            Console.WriteLine(command);
            string cmd="";
            //若收到on指令，設定shell command為開機紅外線指令
            if (command == "on")
            {
                cmd = "irsend SEND_ONCE /home/pi/lircd.conf KEY_POWER";               
            }
            //若收到off指令，設定shell command為關機紅外線指令
            else if (command=="off")
            {
                //我把冷氣關機指令命名為KEY_POWER2
                cmd = "irsend SEND_ONCE /home/pi/lircd.conf KEY_POWER2";                
            }
            //若收到on或off，執行shell command來發射紅外線指令
            if(command=="on"||command=="off")
            {
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "bash",
                        Arguments = $"-c \"{cmd}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                    }
                };
                //執行指令
                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine(result);
            }
        }
    }
}
