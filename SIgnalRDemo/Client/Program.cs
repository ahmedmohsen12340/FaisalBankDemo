using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5257/TriggerHub")
                .Build();

            connection.On<string>("ReceiveMessage", (message) =>
            {
                Console.WriteLine($"Message from server: {message}");
            });

            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connected to the server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to the server: {ex.Message}");
                return;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            await connection.StopAsync();
        }
    }
}
