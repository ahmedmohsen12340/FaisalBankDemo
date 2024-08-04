using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;

namespace SignalRProj.Hubs
{
    public class TriggerHub : Hub
    {
        public string user1ConnectionId,user2ConnectionId;
        public void Register(string userName)
        {
            switch (userName)
            {
                case "user1":
                user1ConnectionId = Context.ConnectionId;
                break;
                case "user2":
                user2ConnectionId = Context.ConnectionId;
                break;

            }
                    // Load the customsettings.json configuration file
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("UserConnectionIds.json", optional: false, reloadOnChange: true)
            .Build();

        // Load JSON from the file
        string json = File.ReadAllText("UserConnectionIds.json");
        JObject jsonObj = JObject.Parse(json);

        // Modify the configuration
        jsonObj["user1"] = user1ConnectionId;
        jsonObj["user2"] = user2ConnectionId;

        // Write back to the file
        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText("UserConnectionIds.json", output);
        }
    }
}