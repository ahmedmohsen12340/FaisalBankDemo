using Services;

FileTrigger myTrigger = new FileTrigger(@"D:\FaisalBankProject\C#Demo\Data\QueuingInfo.xml");
Console.WriteLine("Start Listening.....");
myTrigger.Watcher();
Console.WriteLine("End Process.....");
