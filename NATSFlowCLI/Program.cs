using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using System.Text.Json.Serialization;

namespace NATSFlowCLI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var menuService = host.Services.GetRequiredService<IMenuService>();
        await menuService.ShowMainMenuAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<ILogService, LogService>();
                services.AddSingleton<IMenuService, MenuService>();
                services.AddSingleton<IStreamService, StreamService>();
                services.AddSingleton<IMessageService, MessageService>();
            });
}

//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Configuration;

//namespace NATSFlowCLI;

//public class Program
//{
//    public static async Task Main(string[] args)
//    {
//        var host = CreateHostBuilder(args).Build();

//        var menuService = host.Services.GetRequiredService<IMenuService>();
//        await menuService.ShowMainMenuAsync();
//    }

//    public static IHostBuilder CreateHostBuilder(string[] args) =>
//        Host.CreateDefaultBuilder(args)
//            .ConfigureAppConfiguration((hostingContext, config) =>
//            {
//                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
//            })
//            .ConfigureServices((hostContext, services) =>
//            {
//                // IConfiguration のバインド
//                var config = hostContext.Configuration;
//                services.Configure<NatsSettings>(config.GetSection("NATS"));

//                // サービス登録
//                services.AddSingleton<ILogService, LogService>();
//                services.AddSingleton<IMenuService, MenuService>();
//                services.AddSingleton<IStreamService, StreamService>();
//                services.AddSingleton<IMessageService, MessageService>();
//            });
//}
