using System.Reflection.Metadata.Ecma335;
// using System.Security;
using System.Security.Permissions;

namespace Services;

public class FileTrigger
{
    public string FilePath { get; set; }
    public FileTriggerActions triggerActions { get; set; }
    public FileTrigger(string filePath)
    {
        FilePath = filePath;
        triggerActions = new FileTriggerActions();
    }
    //making The Trigger Func
    public void Watcher()
    {
        using (FileSystemWatcher watcher = new FileSystemWatcher())
        {
            //set the folder Path
            watcher.Path = Path.GetDirectoryName(FilePath);
            watcher.NotifyFilter = NotifyFilters.LastWrite;

            //set the file Name
            watcher.Filter = Path.GetFileName(FilePath);

            //events
            watcher.Changed += triggerActions.OnChanged;
            // watcher.Created += triggerActions.OnChanged;
            // watcher.Deleted += triggerActions.OnChanged;
            // watcher.Renamed += triggerActions.Onrenamed;

            watcher.EnableRaisingEvents = true;

            Console.ReadKey();
        }
    }
}
