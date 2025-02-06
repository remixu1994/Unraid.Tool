using System.Text.Json.Serialization;

namespace Unraid.Tool.QBittorrent;

public class Torrent
{
      [JsonPropertyName("added_on")]
    public int AddedOn { get; set; }

    [JsonPropertyName("amount_left")]
    public decimal AmountLeft { get; set; }

    [JsonPropertyName("auto_tmm")]
    public bool AutoTmm { get; set; }

    [JsonPropertyName("availability")]
    public float Availability { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("comment")]
    public string Comment { get; set; }

    [JsonPropertyName("completed")]
    public long Completed { get; set; }

    [JsonPropertyName("completion_on")]
    public int CompletionOn { get; set; }

    [JsonPropertyName("content_path")]
    public string ContentPath { get; set; }

    [JsonPropertyName("dl_limit")]
    public int DlLimit { get; set; }

    [JsonPropertyName("dlspeed")]
    public int Dlspeed { get; set; }

    [JsonPropertyName("download_path")]
    public string DownloadPath { get; set; }

    [JsonPropertyName("downloaded")]
    public long Downloaded { get; set; }

    [JsonPropertyName("downloaded_session")]
    public long DownloadedSession { get; set; }

    [JsonPropertyName("eta")]
    public int Eta { get; set; }

    [JsonPropertyName("f_l_piece_prio")]
    public bool FLPiecePrio { get; set; }

    [JsonPropertyName("force_start")]
    public bool ForceStart { get; set; }

    [JsonPropertyName("has_metadata")]
    public bool HasMetadata { get; set; }

    [JsonPropertyName("hash")]
    public string Hash { get; set; }

    [JsonPropertyName("inactive_seeding_time_limit")]
    public int InactiveSeedingTimeLimit { get; set; }

    [JsonPropertyName("infohash_v1")]
    public string InfohashV1 { get; set; }

    [JsonPropertyName("infohash_v2")]
    public string InfohashV2 { get; set; }

    [JsonPropertyName("last_activity")]
    public int LastActivity { get; set; }

    [JsonPropertyName("magnet_uri")]
    public string MagnetUri { get; set; }

    [JsonPropertyName("max_inactive_seeding_time")]
    public int MaxInactiveSeedingTime { get; set; }

    [JsonPropertyName("max_ratio")]
    public float MaxRatio { get; set; }

    [JsonPropertyName("max_seeding_time")]
    public int MaxSeedingTime { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("num_complete")]
    public int NumComplete { get; set; }

    [JsonPropertyName("num_incomplete")]
    public int NumIncomplete { get; set; }

    [JsonPropertyName("num_leechs")]
    public int NumLeechs { get; set; }

    [JsonPropertyName("num_seeds")]
    public int NumSeeds { get; set; }

    [JsonPropertyName("popularity")]
    public float Popularity { get; set; }

    [JsonPropertyName("priority")]
    public int Priority { get; set; }

    [JsonPropertyName("private")]
    public bool Private { get; set; }

    [JsonPropertyName("progress")]
    public float Progress { get; set; }

    [JsonPropertyName("ratio")]
    public float Ratio { get; set; }

    [JsonPropertyName("ratio_limit")]
    public float RatioLimit { get; set; }

    [JsonPropertyName("reannounce")]
    public int Reannounce { get; set; }

    [JsonPropertyName("root_path")]
    public string RootPath { get; set; }

    [JsonPropertyName("save_path")]
    public string SavePath { get; set; }

    [JsonPropertyName("seeding_time")]
    public int SeedingTime { get; set; }

    [JsonPropertyName("seeding_time_limit")]
    public int SeedingTimeLimit { get; set; }

    [JsonPropertyName("seen_complete")]
    public int SeenComplete { get; set; }

    [JsonPropertyName("seq_dl")]
    public bool SeqDl { get; set; }

    [JsonPropertyName("size")]
    public long Size { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("super_seeding")]
    public bool SuperSeeding { get; set; }

    [JsonPropertyName("tags")]
    public string Tags { get; set; }

    [JsonPropertyName("time_active")]
    public int TimeActive { get; set; }

    [JsonPropertyName("total_size")]
    public long TotalSize { get; set; }

    [JsonPropertyName("tracker")]
    public string Tracker { get; set; }

    [JsonPropertyName("trackers_count")]
    public int TrackersCount { get; set; }

    [JsonPropertyName("up_limit")]
    public int UpLimit { get; set; }

    [JsonPropertyName("uploaded")]
    public long Uploaded { get; set; }

    [JsonPropertyName("uploaded_session")]
    public long UploadedSession { get; set; }

    [JsonPropertyName("upspeed")]
    public int Upspeed { get; set; }
}