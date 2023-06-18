using BotLineApplication.Configuration;
using BotLineApplication.EventHandlers;
using BotLineApplication.Services;
using Line;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using static System.Net.Mime.MediaTypeNames;

namespace BotLineApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {



                var builder = WebApplication.CreateBuilder(args);
                var _configuration = builder.Configuration;
  
                var lineConfiguration = builder.Configuration.Get<LineConfiguration>();
                var lineBotSampleConfiguration = builder.Configuration.Get<LineBotSampleConfiguration>();
                _configuration.Bind("LineConfiguration", lineConfiguration);
                _configuration.Bind("LineBotSampleConfiguration", lineBotSampleConfiguration);
                //_configuration.Bind("LineConfiguration", lineConfiguration);

                //_configuration.Bind("LineBotSampleConfiguration", lineBotSampleConfiguration);
                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services
                  .AddSingleton<ILineConfiguration>(lineConfiguration)
                    .AddSingleton<ILineBot, LineBot>();

                builder.Services
                  .AddSingleton<ILineBotLogger, LineBotSampleLogger>()
                    .AddSingleton<LineBotSampleConfiguration>(lineBotSampleConfiguration)
                    .AddSingleton<LineBotService>();

                builder.Services
                  .AddSingleton<ILineEventHandler, MessageEventHandler>()
                    .AddSingleton<ILineEventHandler, FollowEventHandler>();

                var app = builder.Build();

                var lineBotService = app.Services.GetRequiredService<LineBotService>();
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                    RequestPath = "/Resources"
                });
                app.Map("/bot", async (context) =>
                {
                    await lineBotService.Handle(context);
                });
                app.Run();
              
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}