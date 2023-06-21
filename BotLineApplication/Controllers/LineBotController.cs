using BotLineApplication.Models;
using Line;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BotLineApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        private readonly ILineBot _lineBot;
        public LineBotController(ILineBot lineBot)
        {
            _lineBot = lineBot;
        }
        [HttpGet(Name = "GetLineBot")]
        public string Get()
        {
            return "Ok";
        }
        [Route("/SendMessage")]
        [HttpPost]
        public string PostSendMessage(SendLine send)
        {
            if (send == null) return "No Message object";
            if ( string.IsNullOrEmpty(send.msg)) return "No Message";
            if (string.IsNullOrEmpty(send.user)) return "No User Id";
            ISendMessage message = new TextMessage(send.user);
            _lineBot.Push(send.user, message);
            return "Ok";
        }
    }
}
