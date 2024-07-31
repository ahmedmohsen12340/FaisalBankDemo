using SignalRProj.Hubs;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddSingleton<FileWatcherService>(); // Register the FileWatcherService


var app = builder.Build();

app.UseRouting();
app.MapHub<TriggerHub>("/TriggerHub");

// Start the FileWatcherService
var fileWatcherService = app.Services.GetRequiredService<FileWatcherService>();



app.Run();
