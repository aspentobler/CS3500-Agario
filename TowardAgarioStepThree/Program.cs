using Communications;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using AgarioModels;
using System.Text;
using TowardAgarioStepThree;
using System.Text.Json;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddFilter("Microsoft", LogLevel.Warning)
        .AddFilter("System", LogLevel.Warning)
        .AddFilter("Program", LogLevel.Debug)
        .AddConsole();
});
ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

Networking network = new Networking(logger, onConnect, onDisconnect, onMessage);

await network.ConnectAsync("localhost", 11000);
await network.HandleIncomingDataAsync(true);

Console.ReadLine();

void onConnect(Networking channel)
{

}

void onDisconnect(Networking channel)
{

}

void onMessage(Networking channel, string message)
{

    if (message.StartsWith(Protocols.CMD_Food))
    {
        string messageCopy = message;
        messageCopy = messageCopy.Remove(0, Protocols.CMD_Food.Length);
        List<Food> foodList = JsonSerializer.Deserialize<List<Food>>(messageCopy) ?? throw new Exception("bad json");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine(foodList[i]);
        }
    }
}