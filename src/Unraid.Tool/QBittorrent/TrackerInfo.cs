using System.Text.Json.Serialization;

namespace Unraid.Tool.QBittorrent;

public class TrackerInfo
{
    [JsonPropertyName("msg")]
    public string Msg { get; set; }

    [JsonPropertyName("num_downloaded")]
    public int NumDownloaded { get; set; }

    [JsonPropertyName("num_leeches")]
    public int NumLeeches { get; set; }

    [JsonPropertyName("num_peers")]
    public int NumPeers { get; set; }

    [JsonPropertyName("num_seeds")]
    public int NumSeeds { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("tier")]
    public int Tier { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}