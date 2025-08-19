using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Animations;

namespace SpaERP.NativeApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		string serverUrl = "http://localhost:5000/";
		Console.WriteLine(AppContext.BaseDirectory);
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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
