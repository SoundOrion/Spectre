public interface ILogService
{
    void AddLog(string message);
    List<string> GetLogs(int maxCount = 15);
}

public class LogService : ILogService
{
    private readonly List<string> _logs = new();

    public void AddLog(string message)
    {
        _logs.Add(message);

        // 最新15件だけ保持
        if (_logs.Count > 15)
        {
            _logs.RemoveRange(0, _logs.Count - 15);
        }
    }

    public List<string> GetLogs(int maxCount = 15)
    {
        return _logs.TakeLast(maxCount).ToList();
    }
}
