using PrismMauiTesting.Abstractions;
using PrismMauiTesting.Services;

namespace PrismMauiTesting
{
    internal static class PrismStartup
	{
        public static void Configure(PrismAppBuilder builder)
        {
            builder.RegisterTypes(RegisterTypes)
                .AddGlobalNavigationObserver(context => context.Subscribe(x =>
                {
                    if (x.Type == NavigationRequestType.Navigate)
                        Console.WriteLine($"Navigation: {x.Uri}");
                    else
                        Console.WriteLine($"Navigation: {x.Type}");

                    var status = x.Cancelled ? "Cancelled" : x.Result.Success ? "Success" : "Failed";
                    Console.WriteLine($"Result: {status}");

                    if (status == "Failed" && !string.IsNullOrEmpty(x.Result?.Exception?.Message))
                        Console.Error.WriteLine(x.Result.Exception.Message);
                }))
                .OnInitialized(OnInitialized)
                .OnAppStart(OnAppStart);
        }

        private static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Prism
            containerRegistry.RegisterGlobalNavigationObserver();

            // Services
            containerRegistry.RegisterSingleton<ITeamService, LecTeamService>();

            // Pages
            containerRegistry.RegisterForNavigation<TeamsPage, TeamsViewModel>();
            containerRegistry.RegisterForNavigation<TeamPage, TeamViewModel>();
        }

        private static void OnInitialized(IContainerProvider containerProvider)
        {
        }

        private static async Task OnAppStart(INavigationService navigationService)
        {
            var result = await navigationService.CreateBuilder()
                .AddNavigationPage()
                .AddSegment<TeamsPage>()
                .NavigateAsync();

            if (!result.Success)
            {
                Debug.WriteLine(result.Exception);
                Debugger.Break();
            }
        }
    }
}

