namespace AnonMessagesWeb;

public interface IAnonMessageService
{
    Task<string> SendMessage(string roomName, string password, string message);
}