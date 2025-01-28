using Spectre.Console;

public interface IStreamService
{
    Task CreateStreamAsync();
}

public class StreamService : IStreamService
{
    private readonly ILogService _logService;

    public StreamService(ILogService logService)
    {
        _logService = logService;
    }

    public async Task CreateStreamAsync()
    {
        var streamName = AnsiConsole.Ask<string>("Enter [green]Stream Name[/]:");
        var subjectName = AnsiConsole.Ask<string>("Enter [green]Subject Name[/]:");

        // サンプルでログに記録
        var log = $"Stream '{streamName}' created with subject '{subjectName}' at {DateTime.Now}";
        _logService.AddLog(log);

        AnsiConsole.MarkupLine($"[bold green]{log}[/]");
        await Task.Delay(500); // Simulate async work
    }
}
