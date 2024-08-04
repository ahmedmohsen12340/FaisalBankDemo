using Microsoft.AspNetCore.SignalR;
using SignalRProj.Hubs;

public class FileWatcherService
{
    private readonly string path = @"D:\FaisalBank\FaisalBankProject\SIgnalRDemo\Data\QueuingInfo.xml";
    private readonly IHubContext<TriggerHub> _hubContext;
    private readonly FileSystemWatcher _fileWatcher;
    private readonly Timer _debounceTimer;
    private string _lastChangePath;
    private readonly IConfiguration _config;

    public FileWatcherService(IHubContext<TriggerHub> hubContext, IConfiguration configuration)
    {
        _config = configuration;
        _hubContext = hubContext;

        _fileWatcher = new FileSystemWatcher()
        {
            Path = Path.GetDirectoryName(path),
            Filter = Path.GetFileName(path),
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName
        };

        _fileWatcher.Changed += OnChanged;
        _fileWatcher.Created += OnChanged;
        _fileWatcher.Deleted += OnChanged;
        _fileWatcher.Renamed += OnRenamed;

        _debounceTimer = new Timer(DebounceCallback, null, Timeout.Infinite, Timeout.Infinite);

        _fileWatcher.EnableRaisingEvents = true;
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        _lastChangePath = e.FullPath;
        _debounceTimer.Change(500, Timeout.Infinite); // Debounce for 500ms
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        _lastChangePath = e.FullPath;
        _debounceTimer.Change(500, Timeout.Infinite); // Debounce for 500ms
    }

    private async void DebounceCallback(object state)
    {
        await File.AppendAllTextAsync(@"D:\FaisalBank\FaisalBankProject\SIgnalRDemo\Data\Change.txt", Environment.NewLine + $"File changed: {_lastChangePath}");
        // await _hubContext.Clients.All.SendAsync("ReceiveMessage", $"File changed: {_lastChangePath}");
        await _hubContext.Clients.Client(_config["user1"]).SendAsync("ReceiveMessage", $"File changed: {_lastChangePath}");
    }
}
