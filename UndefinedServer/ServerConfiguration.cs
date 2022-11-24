using System.Net;
using Newtonsoft.Json;
using Utils;

namespace UndefinedServer
{
    internal class ServerConfiguration : Configuration
    {
        public override string FilePath => Paths.ServerConfigurationFile;

        [JsonProperty("token")] public string Token;
        [JsonProperty("tick")] public int Tick;
        [JsonProperty("server_port")] public int Port;
        [JsonProperty("server_ip")] public string IP;
        [JsonProperty("debug_enabled")] public bool IsDebugEnabled;

        [JsonIgnore] public IPAddress IPAddress => string.IsNullOrEmpty(IP) || IP == "127.0.0.1" ? IPAddress.Any : IPAddress.Parse(IP);
    }
}