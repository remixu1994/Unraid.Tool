using System.Net.Http.Headers;
using System.Text.Json;

namespace Unraid.Tool.QBittorrent;

public class QbittorrentClient
{
    private readonly string _username;
    private readonly string _password;
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public QbittorrentClient(string host, string username, string password)
    {
        _username = username;
        _password = password;
        _baseUrl = $"http://{host}/api/v2/";
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task AuthenticateAsync()
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", _username),
            new KeyValuePair<string, string>("password", _password)
        });
        var response = await _httpClient.PostAsync(_baseUrl + "auth/login", content);
        if (!response.IsSuccessStatusCode)
        {
            string errorInfo = await response.Content.ReadAsStringAsync();
            throw new Exception($"Authentication failed:{errorInfo}");
        }
    }

    public async Task<Torrent[]> GetTorrentsAsync()
    {
        var response = await _httpClient.GetAsync(_baseUrl + "torrents/info");
        response.EnsureSuccessStatusCode();
        var torrentsJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Torrent[]>(torrentsJson);
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
    
    public async Task AddTrackersAsync(string torrentHash, string[] trackerUrls)
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("hash", torrentHash),
            new KeyValuePair<string, string>("urls", string.Join("\n", trackerUrls)) // 使用换行符分隔
        });
        var response = await _httpClient.PostAsync(_baseUrl + "torrents/addTrackers", content);
        if (!response.IsSuccessStatusCode)
        {
            string errorInfo = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to add trackers: {errorInfo}");
        }
    }
    
    public async Task RemoveTrackersAsync(string torrentHash, string[] trackerUrls)
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("hash", torrentHash),
            new KeyValuePair<string, string>("urls", string.Join("|", trackerUrls)) // 使用管道符分隔
        });
        var response = await _httpClient.PostAsync(_baseUrl + "torrents/removeTrackers", content);
        if (!response.IsSuccessStatusCode)
        {
            string errorInfo = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to remove trackers: {errorInfo}");
        }
    }
}