using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using ServerSignalR.Helpers;

namespace ServerSignalR.Hubs
{
    public class TriggerHub : Hub
    {
        private readonly ILogger<TriggerHub> _logger;

        public TriggerHub(ILogger<TriggerHub> logger)
        {
            _logger = logger;
        }
        public async Task Register(string macAddress)
        {
            // Load JSON from the file
            string json = await File.ReadAllTextAsync(@"Data\UserConnectionIds.json");
            JObject jsonObj = JObject.Parse(json);
            // Modify the configuration
            var connectionDetails = new { MacAddress = macAddress, ConnectionId = Context.ConnectionId };
            switch (macAddress)
            {
                case "a8ba6f638b73428b":
                    jsonObj["Counter 1"] = JObject.FromObject(connectionDetails);
                    break;
                    // case "1":
                    //     jsonObj["Counter 2"] = JObject.FromObject(connectionDetails);
                    //     break;
                    // case "2":
                    //     jsonObj["Counter 3"] = JObject.FromObject(connectionDetails);
                    //     break;
            }


            bool isSuccess = await FileHelper.WriteJsonToFileAsync(@"Data\UserConnectionIds.json", jsonObj);
            if (isSuccess)
            {
                _logger.LogInformation("JSON data written successfully.");
            }
            else
            {
                _logger.LogInformation("Failed to write JSON data after multiple attempts.");
            }
        }
        public void LogServerMessage(string logMessage)
        {
            _logger.LogInformation(logMessage);
        }
    }
}