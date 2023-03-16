using Microsoft.Extensions.Logging;
using Prism;

namespace PrismMauiTesting;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UsePrism(prism => PrismStartup.Configure(prism))
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddLogging(logging =>
        {
            logging.AddDebug();
        });

        return builder.Build();
	}
}
