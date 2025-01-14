namespace GymLifts.View;

/// <summary>
/// Content page for the application's main page
/// </summary>
public partial class MainPage : ContentPage
{
    MainPageViewModel viewModel;
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    /// <summary>
    /// Loads the exercises when page is navigated to
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await viewModel.GetExercisesAsync();
    }
}
