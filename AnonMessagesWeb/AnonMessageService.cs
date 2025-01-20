using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace AnonMessagesWeb;

public class AnonMessageService : IAnonMessageService
{
    private readonly HttpClient _httpClient;
    private readonly string _telegramBotUrl;
    private const string HeaderName = "Fromsite";
    
    public AnonMessageService(IHttpClientFactory httpClientFactory, IOptions<AnonMessagesConfiguration> options)
    {
        _httpClient = httpClientFactory.CreateClient("Telegram");
        _telegramBotUrl = options.Value.TelegramBotUrl;
    }
    
    public async Task<Tuple<HttpStatusCode, string>> SendMessage(string roomName, string? password, string message)
    {
        var dataString = JsonLite.EncodeDictionary(new Dictionary<string, string>()
        {
            { "roomName", roomName },
            { "password", password?? "null" },
            { "message", message
                .Replace("\r\n", " ")
                .Replace("\n", " ") }
        });
        var content = new StringContent(dataString);
        
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        content.Headers.Add(HeaderName, "yes");
        
        var botAnswer = await _httpClient.PostAsync(_telegramBotUrl, content);
        var answer = await botAnswer.Content.ReadAsStringAsync();
        var answerStatus = botAnswer.StatusCode;
        return new Tuple<HttpStatusCode, string>(answerStatus, answer);
    }
}