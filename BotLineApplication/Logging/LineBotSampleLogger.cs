using Line;
using System.Text;

namespace BotLineApplication
{
    public class LineBotSampleLogger : ILineBotLogger
    {
        public async Task LogApiCall(Uri uri, HttpContent httpContent)
        {
            var postedData = string.Empty;
            if (httpContent != null)
            {
                var bytes = await httpContent.ReadAsByteArrayAsync();
                postedData = $"PostedData: {Encoding.UTF8.GetString(bytes)}{Environment.NewLine}";
            }

            Console.WriteLine($"Request to: {uri}{Environment.NewLine}{postedData}");
        }

        public Task LogReceivedEvents(byte[] eventsData)
        {
            Console.WriteLine($"Events received: {Encoding.UTF8.GetString(eventsData)}");

            return Task.CompletedTask;
        }
    }
}
