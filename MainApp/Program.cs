using System.Net;
using MainApp.Controller;
using MainApp.Interfaces;
using MainApp.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

namespace MainApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Listen(IPAddress.Any, 5174, lo =>
            {
                lo.Protocols = HttpProtocols.Http2;
            });
            options.Listen(IPAddress.Any, 5173, lo =>
            {
                lo.Protocols = HttpProtocols.Http1AndHttp2;
            });
        });
        builder.Services.AddControllers();
        builder.Services.AddGrpc();
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source=test.db"));
        builder.Services.AddTransient<IAgentService, AgentService>();
        builder.Services.AddTransient<IClientService, ClientService>();
        var app = builder.Build();
        app.MapGrpcService<AgentController>();
        app.MapControllers();

        app.Run();
    }
}