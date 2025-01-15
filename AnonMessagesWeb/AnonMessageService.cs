using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;

namespace AnonMessagesWeb;

public class AnonMessageService : IAnonMessageService
{
    private readonly HttpClient _httpClient;
    private readonly string _telegramBotUrl;
    private const string HeaderName = "FromSite";
    
    public AnonMessageService(IHttpClientFactory httpClientFactory, IOptions<AnonMessagesConfiguration> options)
    {
        _httpClient = httpClientFactory.CreateClient("Telegram");
        _telegramBotUrl = options.Value.TelegramBotUrl;
    }
    
    public async Task<string> SendMessage(string roomName, string? password, string message)
    {
        var dataString = $"{{'roomName':'{roomName}', 'password': '{password}', 'message':'{message}'}}";
        var content = new StringContent(dataString.Replace('\'', '"'));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        content.Headers.Add(HeaderName, "yes");
        var botAnswer = await _httpClient.PostAsync(_telegramBotUrl, content
            );
        return await botAnswer.Content.ReadAsStringAsync();
    }
}