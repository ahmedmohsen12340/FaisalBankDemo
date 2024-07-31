namespace Services
{
    public static class FileTriggerLogging
    {
        public static void WriteToTxtFile(string logMessage)
        {
            File.AppendAllText(@"D:\FaisalBankProject\Change.txt", Environment.NewLine + logMessage);
        }
    }
}