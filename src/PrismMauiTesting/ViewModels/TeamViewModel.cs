namespace PrismMauiTesting.ViewModels;

public class TeamViewModel : ViewModelBase, IInitialize
{
    private LecTeam team;

    public LecTeam Team
    {
        get => team;
        set => SetProperty(ref team, value);
    }

    public TeamViewModel(INavigationService navigationService)
        : base(navigationService)
    {
    }

    public void Initialize(INavigationParameters parameters)
    {
        if (!parameters.TryGetValue("SelectedTeam", out LecTeam selectedTeam))
        {
            throw new ArgumentException("Team required");
        }

        Title = selectedTeam.Name;
        Team = selectedTeam;
    }
}

