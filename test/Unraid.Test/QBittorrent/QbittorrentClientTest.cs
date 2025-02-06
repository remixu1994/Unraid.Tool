using JetBrains.Annotations;
using Unraid.Tool.QBittorrent;

namespace Unraid.Test.QBittorrent;

[TestClass]
[TestSubject(typeof(QbittorrentClient))]
public class QbittorrentClientTest
{
    private QbittorrentClient _qbittorrentClient;

    public QbittorrentClientTest()
    {
        _qbittorrentClient = new QbittorrentClient("192.168.1.13:8080", "", "");
    }
    
    [TestMethod]
    public async Task Test_Add_Tracker()
    {
        var newTracker = "https://tracker.carpt.net/announce.php?passkey=2c41be0128b3674c04bad0ecae56ca85";
        var oldTracker = "https://tracker.carpt.net/announce.php?passkey=fede2a966617e011feefbada42119afa";
        var torrents = await _qbittorrentClient.GetTorrentsAsync();
        List<Torrent> oldTrackers = torrents.Where(x => x.Tracker == oldTracker).ToList();

        foreach (var torrent in oldTrackers)
        {
            await _qbittorrentClient.AddTrackersAsync(torrent.Hash, [newTracker]);
        }
    }

    [TestMethod]
    public async Task Should_Remove_Old_Tracker()
    {
        var oldTracker = "https://tracker.carpt.net/announce.php?passkey=fede2a966617e011feefbada42119afa";
        var torrents = await _qbittorrentClient.GetTorrentsAsync();
        List<Torrent> oldTrackers = torrents.Where(x => x.Tracker == oldTracker).ToList();
        foreach (var torrent in oldTrackers)
        {
            await _qbittorrentClient.RemoveTrackersAsync(torrent.Hash, [torrent.Tracker]);
        }
    }

    [TestMethod]
    public async Task Should_Edit()
    {
        var newTracker = "https://tracker.carpt.net/announce.php?passkey=2c41be0128b3674c04bad0ecae56ca85";
        var oldTracker = "https://tracker.carpt.net/announce.php?passkey=fede2a966617e011feefbada42119afa";
        var torrents = await _qbittorrentClient.GetTorrentsAsync();
        List<Torrent> oldTrackers = torrents.Where(x => x.Tracker == oldTracker).ToList();

        foreach (var torrent in oldTrackers)
        {
            await _qbittorrentClient.EditTrackerAsync(torrent.Hash, oldTracker, newTracker);
        }
    }
}