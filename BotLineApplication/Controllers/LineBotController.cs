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
        public string PostSendMessage(string id)
        {
            //  IMessage message = new Mess
            ISendMessage message = new TextMessage("Sample");
            _lineBot.Push(id, message);
            return "Ok";
        }
    }
}
