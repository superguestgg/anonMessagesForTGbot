using Microsoft.AspNetCore.Mvc;

namespace AnonMessagesWeb;

[ApiController]
[Route("api/[controller]")]
public class AnonMessageController : Controller
{
    private readonly IAnonMessageService _am;
    
    public AnonMessageController(IAnonMessageService am)
    {
        _am = am;
    }

    [HttpPost("sendMessage/{roomName}")]
    public Task<string> SendMessage([FromRoute] string roomName, [FromForm] string password, [FromForm] string message)
    {
        return _am.SendMessage(roomName, password, message);
    }
    
    [HttpPost("SendMessageForm")]
    public Task<string> SendMessageForm([FromForm] string roomName, [FromForm] string password, [FromForm] string message)
    {
        return _am.SendMessage(roomName, password, message);
    }
}