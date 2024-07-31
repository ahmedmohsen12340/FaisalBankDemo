namespace Services
{
    public class FileTriggerActions
    {
        public void OnChanged(object source, FileSystemEventArgs e)
        {
            FileTriggerLogging.WriteToTxtFile(e.Name + " " + e.ChangeType);
        }

        public void Onrenamed(object source, RenamedEventArgs e)
        {
            FileTriggerLogging.WriteToTxtFile(e.FullPath + " " + e.ChangeType);
        }
    }
}