using System.Net;

namespace AnonMessagesWeb;

public interface IAnonMessageService
{
    Task<Tuple<HttpStatusCode, string>> SendMessage(string roomName, string? password, string message);
}