namespace NATSFlowCLI.Models;

public class NatsSettings
{
    public string Url { get; set; }
    public List<StreamConfigModel> Streams { get; set; }
}

public class StreamConfigModel
{
    public string Name { get; set; }
    public List<string> Subjects { get; set; }
    public string Retention { get; set; }
    public int MaxMsgs { get; set; }
    public int MaxBytes { get; set; }
    public int MaxAgeHours { get; set; }
}

