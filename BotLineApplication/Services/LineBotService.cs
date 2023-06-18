using BotLineApplication.EventHandlers;
using Line;

namespace BotLineApplication.Services
{
    public class LineBotService
    {
        private readonly ILineBot _lineBot;
        private readonly IServiceProvider _serviceProvider;

        public LineBotService(ILineBot lineBot, IServiceProvider serviceProvider)
        {
            _lineBot = lineBot;
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(HttpContext context)
        {
            if (context.Request.Method != HttpMethods.Post)
                return;

            var events = await _lineBot.GetEvents(context.Request);
            foreach (var evt in events)
            {
                foreach (var eventHandler in GetEventHandlers(evt.EventType))
                {
                    await eventHandler.Handle(_lineBot, evt);
                }
            }
        }

        private IEnumerable<ILineEventHandler> GetEventHandlers(LineEventType eventType)
        {
            return _serviceProvider.GetServices<ILineEventHandler>()
                .Where(_ => _.EventType == eventType);
        }
    }
}
