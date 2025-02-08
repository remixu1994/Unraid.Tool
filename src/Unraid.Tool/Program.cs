using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MudBlazor.Services;
using Unraid.Tool.Components;
using Unraid.Tool.QBittorrent;
using Unraid.Tool.QBittorrent.Endpoints;

var builder = WebApplication.CreateBuilder(args);
//add log
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole(); // 添加控制台日志
});

// add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Qbittorrent Manager API", Version = "v1" });
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//mud blazor
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient()
{
    BaseAddress = new Uri(sp.GetService<NavigationManager>()!.BaseUri)
});


builder.Services.AddScoped<IQbittorrentService, QbittorrentService>();
builder.Services.AddSingleton<QbittorrentClient>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Qbittorrent Manager API V1");
    c.RoutePrefix = "swagger";
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


app.MapQbittorrentEndpoints();

app.Run();