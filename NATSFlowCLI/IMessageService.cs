using Spectre.Console;

public interface IMessageService
{
    Task PublishMessageAsync();
    Task ConsumeMessagesAsync();
}

public class MessageService : IMessageService
{
    private readonly ILogService _logService;

    public MessageService(ILogService logService)
    {
        _logService = logService;
    }

    public async Task PublishMessageAsync()
    {
        var streamName = AnsiConsole.Ask<string>("Enter [green]Stream Name[/]:");
        var message = AnsiConsole.Ask<string>("Enter [green]Message[/]:");

        // サンプルでログに記録
        var log = $"Message published to stream '{streamName}': {message} at {DateTime.Now}";
        _logService.AddLog(log);

        AnsiConsole.MarkupLine($"[bold green]{log}[/]");
        await Task.Delay(500); // Simulate async work
    }

    public async Task ConsumeMessagesAsync()
    {
        var streamName = AnsiConsole.Ask<string>("Enter [green]Stream Name[/]:");

        // サンプルでログに記録
        var log = $"Started consuming messages from stream '{streamName}' at {DateTime.Now}";
        _logService.AddLog(log);

        AnsiConsole.MarkupLine($"[bold yellow]{log}[/]");
        await Task.Delay(500); // Simulate async work
    }
}
