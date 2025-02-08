using System.Text;
using Unraid.Tool.QBittorrent.Endpoints;

namespace Unraid.Tool.QBittorrent;

public class QbittorrentService(QbittorrentClient qbClient, ILogger<IQbittorrentService> logger) : IQbittorrentService
{
    public async Task<bool> ReplaceTrackers(string origUrl, string newUrl)
    {
        var log = new StringBuilder();
        Torrent[]? torrents = await qbClient.GetTorrentsAsync();
        if (torrents != null && torrents.Any())
        {
            var replaceTorrents = torrents.Where(x => x.Tracker == origUrl || x.TrackersCount > 1).ToList();
            foreach (var torrent in replaceTorrents)
            {
                try
                {
                    if (torrent.TrackersCount > 1)
                    {
                        TrackerInfo[]? trackersAsync = await qbClient.GetTrackersAsync(torrent.Hash);
                        if (trackersAsync== null)
                        {
                            continue;
                        }
                        if (trackersAsync.Any(x => x.Url == newUrl))
                        {
                            logger.LogInformation("{Name}-{hash} already has {newUrl} tracker. Skip.", torrent.Name, torrent.Hash, newUrl);
                            continue;
                        }
                        if (trackersAsync.Any(x=>x.Url != origUrl))
                        {
                            continue;
                        }
                    }
                    await qbClient.EditTrackerAsync(torrent.Hash, origUrl, newUrl);
                }
                catch (Exception e)
                {
                    logger.LogError(e, "{Name}-{hash} already has {newUrl} tracker. Skip.", torrent.Name, torrent.Hash, newUrl);
                }
            }
        }

        return true;
    }
}

public interface IQbittorrentService
{
    Task<bool> ReplaceTrackers(string origUrl, string newUrl);
}

