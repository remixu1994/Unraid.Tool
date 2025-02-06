using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Unraid.Tool.Components;
using Unraid.Tool.QBittorrent;

var builder = WebApplication.CreateBuilder(args);

// 添加 Swagger 生成器
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Qbittorrent Manager API", Version = "v1" });
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Qbittorrent Manager API V1");
    c.RoutePrefix = string.Empty; // 将 Swagger UI 设置为应用的根路径
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

QbittorrentClient qbClient = null;
app.MapPost("/qbittorrent/Login", async ([FromBody]LoginRequest request) =>
{
    qbClient = new QbittorrentClient(request.Host, request.UserName, request.Password);
    await qbClient.AuthenticateAsync();
    return Results.Ok("Login successfully.");
});

app.MapGet("/qbittorrent/torrents", async () =>
{
    if (qbClient == null)
    {
        return Results.BadRequest("Please login first.");
    }
    var torrents = await qbClient.GetTorrentsAsync();
    return Results.Ok(torrents);
}).WithName("GetAllTorrents");

app.MapPost("/qbittorrent/add-trackers", async (string torrentHash, string trackerUrls) =>
{
    if (qbClient == null)
    {
        return Results.BadRequest("Please login first.");
    }
    await qbClient.AddTrackersAsync(torrentHash, [trackerUrls]);
    return Results.Ok("Trackers added successfully.");
}).WithName("AddTrackers");;

app.Run();