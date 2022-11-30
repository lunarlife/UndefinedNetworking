using System.Net;
using Newtonsoft.Json;
using Utils;

namespace UndefinedServer
{
    public sealed class ServerConfiguration : Configuration
    {
        [JsonIgnore] public override string FilePath => Paths.ServerConfigurationFile;

        [JsonProperty("token")] public string Token { get; init; }
        [JsonProperty("tick")] public int Tick { get; init; }
        [JsonProperty("server_port")] public int Port { get; init; }
        [JsonProperty("server_ip")] public string IP { get; init; }
        [JsonProperty("debug_enabled")] public bool IsDebugEnabled { get; init; }

        
        
        #region Ping
        [JsonProperty("ping_max_request_times")] public int MaxPingInvalidRequests { get; init; }
        [JsonProperty("ping_player_max_ms")] public int MaxPlayerPing { get; init; }
        [JsonProperty("ping_send_request_delay_ms")] public int PingCheckDelay { get; init; }
        #endregion
     

        [JsonIgnore] public IPAddress IPAddress => string.IsNullOrEmpty(IP) || IP == "127.0.0.1" ? IPAddress.Any : IPAddress.Parse(IP);
    }
}