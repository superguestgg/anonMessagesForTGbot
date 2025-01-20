using System.Net;
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
    public async Task<string> SendMessage([FromRoute] string roomName, [FromForm] string? password, [FromForm] string message)
    {
        return (await _am.SendMessage(roomName, password, message)).Item2;
    }
    
    [HttpPost("SendMessageForm")]
    public async Task<ActionResult<string>> SendMessageForm([FromForm] string roomName, [FromForm] string? password, [FromForm] string message)
    {
        var answer = await _am.SendMessage(roomName, password, message);
        if (answer.Item1 == HttpStatusCode.OK)
            return new ActionResult<string>(answer.Item2);
        return StatusCode((int)answer.Item1
            , answer.Item2);
    }
}