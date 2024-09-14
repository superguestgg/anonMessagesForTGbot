using System.Net.Http.Headers;
using System.Text;

namespace AnonMessagesWeb;

public class AnonMessageService : IAnonMessageService
{
    private const string TelegramBotUrl = "https://functions.yandexcloud.net/d4e99li3ond8e6ug1bsu";
    
    public async Task<string> SendMessage(string roomName, string password, string message)
    {
        var h = new HttpClient();
        var st = $"{{'roomName':'{roomName}', 'password': '{password}', 'message':'{message}'}}";
        var c = new StringContent(st.Replace('\'', '"'));
        c.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        c.Headers.Add("Dick", "suck");
        var d = await h.PostAsync(TelegramBotUrl, c
            );
        return d.Content.ToString();
    }
}