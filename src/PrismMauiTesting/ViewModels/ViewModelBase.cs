namespace PrismMauiTesting.ViewModels;

public partial class ViewModelBase : BindableBase
{
    protected INavigationService navigationService { get; private set; }

    private string title;

    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public ViewModelBase(INavigationService navigationService)
    {
        this.navigationService = navigationService;
    }
}
