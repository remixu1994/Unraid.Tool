using System.Net.Http.Headers;
using System.Text.Json;

namespace Unraid.Tool.QBittorrent;

public class QbittorrentClient
{
    public bool IsAuthenticated { get; set; } = false;
    private string _username;
    private string _password;
    private readonly HttpClient _httpClient;
    private string _baseUrl;

    public QbittorrentClient()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task AuthenticateAsync(string host, string username, string password)
    {
        _username = username;
        _password = password;
        _baseUrl = $"http://{host}/api/v2/";
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", _username),
            new KeyValuePair<string, string>("password", _password)
        });
        var response = await _httpClient.PostAsync(_baseUrl + "auth/login", content);
        if (!response.IsSuccessStatusCode)
        {
            IsAuthenticated = false;
            string errorInfo = await response.Content.ReadAsStringAsync();
            throw new Exception($"Authentication failed:{errorInfo}");
        }

        string resContent = await response.Content.ReadAsStringAsync();
        if (resContent != "Ok.")
        {
            IsAuthenticated = false;
            throw new Exception($"Authentication failed:{resContent}");
        }
        IsAuthenticated = true;
    }

    public async Task<Torrent[]?> GetTorrentsAsync()
    {
        var response = await _httpClient.GetAsync(_baseUrl + "torrents/info");
        response.EnsureSuccessStatusCode();
        var torrentsJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Torrent[]>(torrentsJson);
    }

    public async Task<TrackerInfo[]?> GetTrackersAsync(string torrentHash)
    {
        var response = await _httpClient.GetAsync(_baseUrl + $"torrents/trackers?hash={torrentHash}&m6uafhtd");
        response.EnsureSuccessStatusCode();
        var trackerJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TrackerInfo[]>(trackerJson);
    }

    public async Task EditTrackerAsync(string torrentHash, string origUrl, string newUrl)
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("hash", torrentHash),
            new KeyValuePair<string, string>("origUrl", origUrl),
            new KeyValuePair<string, string>("newUrl", newUrl)
        });
        var response = await _httpClient.PostAsync(_baseUrl + "torrents/editTracker", content);
        if (!response.IsSuccessStatusCode)
        {
            string errorInfo = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to edit tracker: {errorInfo}");
        }
    }

    public async Task AddTrackersAsync(string torrentHash, string trackerUrl)
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("hash", torrentHash),
            new KeyValuePair<string, string>("urls", trackerUrl)
        });
        var response = await _httpClient.PostAsync(_baseUrl + "torrents/addTrackers", content);
        if (!response.IsSuccessStatusCode)
        {
            string errorInfo = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to add trackers: {errorInfo}");
        }
    }

    public async Task RemoveTrackersAsync(string torrentHash, string trackerUrl)
    {
        var content = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("hash", torrentHash),
            new KeyValuePair<string, string>("urls", trackerUrl)
        ]);
        var response = await _httpClient.PostAsync(_baseUrl + "torrents/removeTrackers", content);
        if (!response.IsSuccessStatusCode)
        {
            string errorInfo = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to remove trackers: {errorInfo}");
        }
    }
}