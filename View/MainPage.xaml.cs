namespace GymLifts.View;

public partial class MainPage : ContentPage
{
    MainPageViewModel viewModel;
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await viewModel.GetExercisesAsync();
    }
}
