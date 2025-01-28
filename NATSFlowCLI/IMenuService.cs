using Spectre.Console;

public interface IMenuService
{
    Task ShowMainMenuAsync();
}

public class MenuService : IMenuService
{
    private readonly ILogService _logService;
    private readonly IStreamService _streamService;
    private readonly IMessageService _messageService;

    public MenuService(ILogService logService, IStreamService streamService, IMessageService messageService)
    {
        _logService = logService;
        _streamService = streamService;
        _messageService = messageService;
    }

    public async Task ShowMainMenuAsync()
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Panel("Welcome to [green]NATSFlowCLI[/]")
                    .Border(BoxBorder.Rounded)
                    .Padding(1, 1)
                    .Header("[bold blue]Main Menu[/]"));

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Choose an option[/]:")
                    .AddChoices("Create Stream", "Publish Message", "Consume Messages", "View Logs", "Exit"));

            switch (selection)
            {
                case "Create Stream":
                    await _streamService.CreateStreamAsync();
                    break;
                case "Publish Message":
                    await _messageService.PublishMessageAsync();
                    break;
                case "Consume Messages":
                    await _messageService.ConsumeMessagesAsync();
                    break;
                case "View Logs":
                    DisplayLogs();
                    break;
                case "Exit":
                    return;
            }
        }
    }

    private void DisplayLogs()
    {
        var logs = _logService.GetLogs();
        var table = new Table()
            .Border(TableBorder.Rounded)
            .Title("[bold yellow]Log History[/]")
            .AddColumn("[bold blue]Log Messages[/]");

        foreach (var log in logs)
        {
            table.AddRow(log);
        }

        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("\nPress [green]Enter[/] to return to the main menu...");
        Console.ReadLine();
    }
}
