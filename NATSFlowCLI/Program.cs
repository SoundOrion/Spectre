using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

// ログサービス
public class LogService
{
    private readonly List<string> _logMessages = new();

    public void AddLog(string log)
    {
        _logMessages.Add(log);

        // 最新15件だけを保持
        if (_logMessages.Count > 15)
        {
            _logMessages.RemoveRange(0, _logMessages.Count - 15);
        }
    }

    public IReadOnlyList<string> GetLogs() => _logMessages.AsReadOnly();
}

// アプリケーションメインクラス
public class NATSFlowCLI
{
    private readonly LogService _logService;

    public NATSFlowCLI(LogService logService)
    {
        _logService = logService;
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.Write(
                new Panel("Welcome to [green]NATSFlowCLI[/]")
                    .Border(BoxBorder.Rounded)
                    .Padding(1, 1)
                    .Header("[bold blue]Main Menu[/]"));

            // 選択メニュー
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Choose an option[/]:")
                    .AddChoices("Create Stream", "Publish Message", "Consume Messages", "View Logs", "Exit")
            );

            switch (selection)
            {
                case "Create Stream":
                    ShowDescription("Create a new JetStream. You'll be prompted for Stream Name and Subject.");
                    CreateStream();
                    break;
                case "Publish Message":
                    ShowDescription("Publish a new message to an existing stream.");
                    PublishMessage();
                    break;
                case "Consume Messages":
                    ShowDescription("Start consuming messages from a stream.");
                    ConsumeMessages();
                    break;
                case "View Logs":
                    ShowDescription("View the current logs.");
                    DisplayLogs();
                    break;
                case "Exit":
                    ShowDescription("Exiting the application. Goodbye!");
                    return;
            }
        }
    }

    private void ShowDescription(string description)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Panel(description)
            .Border(BoxBorder.Rounded)
            .Header("[bold blue]Description[/]")
            .Padding(1, 1)
            .Expand());
    }

    private void CreateStream()
    {
        // 動的に候補を生成（例: 既存ストリームのリスト）
        var streamCandidates = new List<string> { "DynamicStream1", "DynamicStream2", "DynamicStream3" };

        var streamName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select [green]stream name[/]:")
                .PageSize(10)
                .AddChoices(streamCandidates));

        var subjectName = AnsiConsole.Ask<string>("Enter [green]subject name[/]:");

        AnsiConsole.MarkupLine($"[bold green]Stream '{streamName}' created with subject '{subjectName}'![/]");

        // サンプルとして、ログを保存
        var log = $"Stream '{streamName}' created with subject '{subjectName}' at {DateTime.Now}";
        _logService.AddLog(log);
    }

    private void PublishMessage()
    {
        var streamName = AnsiConsole.Ask<string>("Enter [green]Stream Name[/]:");
        var message = AnsiConsole.Ask<string>("Enter [green]Message[/]:");

        var log = $"Message published to stream '{streamName}': {message} at {DateTime.Now}";
        _logService.AddLog(log);
    }

    private void ConsumeMessages()
    {
        var streamName = AnsiConsole.Ask<string>("Enter [green]Stream Name[/]:");

        var log = $"Started consuming messages from stream '{streamName}' at {DateTime.Now}";
        _logService.AddLog(log);
    }

    private void DisplayLogs()
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .Title("[bold yellow]Log History[/]")
            .AddColumn("[bold blue]Log Messages[/]");

        foreach (var log in _logService.GetLogs())
        {
            table.AddRow(log);
        }

        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("\nPress [green]Enter[/] to return to the main menu...");
        Console.ReadLine();
    }
}

// DIセットアップとエントリーポイント
class Program
{
    static void Main(string[] args)
    {
        // DIコンテナのセットアップ
        var serviceProvider = new ServiceCollection()
            .AddSingleton<LogService>()
            .AddTransient<NATSFlowCLI>()
            .BuildServiceProvider();

        // アプリケーションを実行
        var app = serviceProvider.GetRequiredService<NATSFlowCLI>();
        app.Run();
    }
}
