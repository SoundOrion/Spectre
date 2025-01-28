using Spectre.Console;

// ログを保存するリスト
var logMessages = new List<string>();

// メインメニュー
void MainMenu()
{
    while (true)
    {
        Console.Clear();

        // メインメニューを表示
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

        // 各メニューの説明文とアクションを対応付け
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

        //// ログを表示
        //UpdateLogs();
    }
}

// メニューの説明文を画面上部に一時的に表示
void ShowDescription(string description)
{
    AnsiConsole.Clear();
    AnsiConsole.Write(new Panel(description)
        .Border(BoxBorder.Rounded)
        .Header("[bold blue]Description[/]")
        .Padding(1, 1)
        .Expand());
    //AnsiConsole.MarkupLine("\nPress [green]Enter[/] to continue...");
    //Console.ReadLine();
}

// ログの更新
void UpdateLogs()
{
    var table = new Table()
        .Border(TableBorder.Rounded)
        .Title("[bold yellow]Logs[/]")
        .AddColumn("[bold blue]Log Messages[/]");

    // ログを最大10件に制限
    foreach (var log in logMessages.TakeLast(10))
    {
        table.AddRow(log);
    }

    // 画面の下部にログを表示
    AnsiConsole.Write(table);
}

//// ストリーム作成のサンプル
//void CreateStream()
//{
//    var streamName = AnsiConsole.Ask<string>("Enter [green]Stream Name[/]:");
//    var subjectName = AnsiConsole.Ask<string>("Enter [green]Subject Name[/]:");

//    // サンプルとして、ログを保存
//    var log = $"Stream '{streamName}' created with subject '{subjectName}' at {DateTime.Now}";
//    logMessages.Add(log);
//    AnsiConsole.MarkupLine($"[bold green]{log}[/]");
//}

void CreateStream()
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
    AddLog(log);
}

// メッセージのパブリッシュのサンプル
void PublishMessage()
{
    var streamName = AnsiConsole.Ask<string>("Enter [green]Stream Name[/]:");
    var message = AnsiConsole.Ask<string>("Enter [green]Message[/]:");

    // サンプルとして、ログを保存
    var log = $"Message published to stream '{streamName}': {message} at {DateTime.Now}";
    AddLog(log);
}

// メッセージの受信（簡易的なサンプル）
void ConsumeMessages()
{
    var streamName = AnsiConsole.Ask<string>("Enter [green]Stream Name[/]:");

    // サンプルとして、ログを保存
    var log = $"Started consuming messages from stream '{streamName}' at {DateTime.Now}";
    AddLog(log);
}

// ログを表示
void DisplayLogs()
{
    //AnsiConsole.Clear();
    var table = new Table()
        .Border(TableBorder.Rounded)
        .Title("[bold yellow]Log History[/]")
        .AddColumn("[bold blue]Log Messages[/]");

    // すべてのログを表示
    foreach (var log in logMessages)
    {
        table.AddRow(log);
    }

    AnsiConsole.Write(table);
    AnsiConsole.MarkupLine("\nPress [green]Enter[/] to return to the main menu...");
    Console.ReadLine();
}

// ログを追加するメソッド
void AddLog(string log)
{
    logMessages.Add(log);

    // 最新15件だけを保持
    if (logMessages.Count > 15)
    {
        logMessages.RemoveRange(0, logMessages.Count - 15); // 古いログを削除
    }
}

// アプリケーション開始
MainMenu();
