using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Unraid.Tool.QBittorrent.Endpoints;

public static class QbittorrentEndpoints
{
    public static void MapQbittorrentEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGroup("/api/qbittorrent");

        routeBuilder.MapPost("Login", async ([FromBody] LoginRequest request, [FromServices]QbittorrentClient qbClient) =>
        {
            await qbClient.AuthenticateAsync(request.Host, request.UserName, request.Password);
            return Results.Ok("Login successfully.");
        });

        routeBuilder.MapGet("torrents", async ([FromServices]QbittorrentClient qbClient) =>
        {
            if (!qbClient.IsAuthenticated)
            {
                return Results.BadRequest("Please login first.");
            }

            var torrents = await qbClient.GetTorrentsAsync();
            return Results.Ok(torrents);
        }).WithName("GetAllTorrents");

        routeBuilder.MapPost("add-trackers/{torrentHash}", async (string torrentHash, [FromBody]string trackerUrl, [FromServices]QbittorrentClient qbClient) =>
        {
            if (!qbClient.IsAuthenticated)
            {
                return Results.BadRequest("Please login first.");
            }

            await qbClient.AddTrackersAsync(torrentHash, WebUtility.UrlDecode(trackerUrl));
            return Results.Ok("Trackers added successfully.");
        }).WithName("AddTrackers");

        routeBuilder.MapPost("edit-tracker/{torrentHash}",
            async ([FromRoute] string torrentHash, [FromBody] EditRequest request, [FromServices]QbittorrentClient qbClient) =>
            {
                if (!qbClient.IsAuthenticated)
                {
                    return Results.BadRequest("Please login first.");
                }

                await qbClient.EditTrackerAsync(torrentHash, request.OrigUrl, request.NewUrl);
                return Results.Ok("Tracker edited successfully.");
            }).WithName("EditTracker");
        
        
        routeBuilder.MapPost("replace-tracker",
                async ([FromBody] EditRequest request, IQbittorrentService service) =>
                {
                    bool success = await service.ReplaceTrackers(request.OrigUrl, request.NewUrl);
                    return Results.Ok("Tracker replace successfully.");
                })
            .WithName("ReplaceTracker")
            .WithDescription("Replace all original trackers with new Urls.");

        routeBuilder.MapDelete("delete-tracker/{torrentHash}",
            async ([FromRoute] string torrentHash, [FromQuery] string trackerUrl, [FromServices]QbittorrentClient qbClient) =>
            {
                if (!qbClient.IsAuthenticated)
                {
                    return Results.BadRequest("Please login first.");
                }

                await qbClient.RemoveTrackersAsync(torrentHash, WebUtility.UrlDecode(trackerUrl));
                return Results.Ok("Tracker delete successfully.");
            }).WithName("RemoveTrackersAsync");
    }
}
public record EditRequest(string OrigUrl, string NewUrl);

