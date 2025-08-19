using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using SpaERP;
using System.Text.Json;

namespace SpaERP.NativeApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .AddHttpClient()
            .UseMauiApp<App>()
            .UseSkiaSharp()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder AddHttpClient(this MauiAppBuilder builder)
    {
        string serverUrl = "http://localhost:5000/";
        var configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(configPath))
        {
            configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.user.json");
        }

        if (File.Exists(configPath))
        {
            var json = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (config != null && config.ContainsKey("ServerUrl"))
                serverUrl = config["ServerUrl"];
        }

        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri(serverUrl) // Adjust the base address as needed
        });

        return builder;
    }

 }
