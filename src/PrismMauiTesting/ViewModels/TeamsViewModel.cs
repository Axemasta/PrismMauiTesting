using System.Windows.Input;
using Microsoft.Extensions.Logging;
using PrismMauiTesting.Abstractions;
using PrismMauiTesting.Models;

namespace PrismMauiTesting.ViewModels;

public class TeamsViewModel : ViewModelBase
{
    public ObservableCollection<LecTeam> Teams { get; }

    public DelegateCommand<LecTeam> TeamSelectedCommand { get; }

    private readonly ILogger logger;

    public TeamsViewModel(
        ILogger<TeamsViewModel> logger,
        INavigationService navigationService,
        ITeamService teamService)
        : base(navigationService)
    {
        this.logger = logger;

        Title = "LEC Teams";

        var teams = teamService.GetLecTeams();

        Teams = new ObservableCollection<LecTeam>(teams);

        TeamSelectedCommand = new DelegateCommand<LecTeam>(OnTeamSelected);
    }

    private async void OnTeamSelected(LecTeam lecTeam)
    {
        if (lecTeam is null)
        {
            logger.LogDebug("Team was null");
            return;
        }

        logger.LogDebug("Team selected: {teamName}", lecTeam.Name);

        var parameters = new NavigationParameters()
        {
            { "SelectedTeam", lecTeam },
        };

        var result = await navigationService.CreateBuilder()
            .AddSegment<TeamViewModel>()
            .WithParameters(parameters)
            //.AddParameter("SelectedTeam", lecTeam)
            //.AddParameter(KnownNavigationParameters.Animated, true)
            .NavigateAsync();

        //var result = await navigationService.NavigateAsync("TeamPage", parameters);

        if (!result.Success)
        {
            logger.LogError(result.Exception, "Navigation to team page failed");
        }
    }
}

