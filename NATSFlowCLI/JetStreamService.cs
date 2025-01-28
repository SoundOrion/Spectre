using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Client.JetStream;
using NATS.Net;
using NATSFlowCLI.Models;

public class JetStreamService
{
    private readonly INatsClient _natsClient;
    private readonly INatsJSContext _jsContext;
    private readonly NatsSettings _settings;

    public JetStreamService(IOptions<NatsSettings> options)
    {
        _settings = options.Value;
        _natsClient = new NatsClient(_settings.Url);
        _jsContext = _natsClient.CreateJetStreamContext();

        // 接続を確立
        Task.Run(async () => await _natsClient.ConnectAsync()).Wait();
    }

    // ストリームをリストする
    public List<StreamConfigModel> ListStreams()
    {
        return _settings.Streams;
    }

    // ストリームを作成する
    public async void CreateStream(string name, List<string> subjects)
    {
        var config = new StreamConfig(name, subjects)
        {
            Retention = StreamConfigRetention.Limits
        };
        await _jsContext.CreateStreamAsync(config);
    }

    // メッセージをパブリッシュする
    public async void PublishMessage(string subject, string message)
    {
        await _jsContext.PublishAsync(subject, data: new { content = message });
    }
}
