namespace Bijington.Cupcakes.Settings.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(ISettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
        InitializeComponent();
    }
    
    private readonly ISettingsRepository _settingsRepository;

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        CurrencyCell.Text = _settingsRepository.Currency;
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        
        _settingsRepository.Currency = CurrencyCell.Text;
    }
}