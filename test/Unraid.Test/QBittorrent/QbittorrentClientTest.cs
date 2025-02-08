using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Moq;
using Unraid.Tool.QBittorrent;

namespace Unraid.Test.QBittorrent;

[TestClass]
[TestSubject(typeof(QbittorrentClient))]
public class QbittorrentClientTest
{
    private QbittorrentClient _qbittorrentClient;

    public QbittorrentClientTest()
    {
        _qbittorrentClient = new QbittorrentClient();
        _qbittorrentClient.AuthenticateAsync("192.168.193.13:8082", "admin", "").Wait();
    }

    [TestMethod]
    public async Task Should_Get_All_Qbittorrents()
    {
        var torrents = await _qbittorrentClient.GetTorrentsAsync();
        IEnumerable<Torrent> enumerable = torrents.Where(x=>x.Hash == "0225896687fc2333b7d19f4cfed832cc16800d6b");
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
            await _qbittorrentClient.AddTrackersAsync(torrent.Hash, newTracker);
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
            await _qbittorrentClient.RemoveTrackersAsync(torrent.Hash, torrent.Tracker);
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

    [TestMethod]
    public async Task Should_Replace_Tracker()
    {
        var newTracker = "https://tracker.m-team.cc/announce?credential=c2lnbj0yNTFhYjcyZjNhMTYwNWYzNmJmZTc5MTIwODAzOTMyMSZ0PTE3Mzg5ODE0MjQmdGlkPTMxMDA3MiZ1aWQ9MzQ4Mzk5";
        var oldTracker = "https://tracker.m-team.cc/announce?credential=c2lnbj1kOTY4ZjM5ZTgwNTVjYjQ0NTIxZDZiZjczMTNhOThhYyZ0PTE3Mzg5NDMxNDMmdGlkPTkwMzY4NCZ1aWQ9MzQ4MzIz";
        await new QbittorrentService(_qbittorrentClient, Mock.Of<ILogger<IQbittorrentService>>())
            .ReplaceTrackers(oldTracker, newTracker);
    }
}