using BotLineApplication.Configuration;
using BotLineApplication.EventHandlers;
using BotLineApplication.Repositories;
using BotLineApplication.Services;
using Line;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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
              
                 var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");
                ConnectionConfiguration conn = new ConnectionConfiguration { Connection = connectionString };
            
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services
                  .AddSingleton<ILineConfiguration>(lineConfiguration)
                    .AddSingleton<ILineBot, LineBot>();
                builder.Services.AddSingleton<ConnectionConfiguration>(conn)
                    .AddSingleton<ILineDBRepository,LineDBRepository>();
                
                //builder.Services.AddTransient<ILineDBRepository, LineDBRepository>();

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