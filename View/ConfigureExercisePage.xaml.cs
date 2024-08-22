namespace GymLifts.View;

public partial class ConfigureExercisePage : ContentPage
{
	ConfigureExerciseViewModel viewModel;
	public ConfigureExercisePage(ConfigureExerciseViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await viewModel.GetLiftsAsync();
    }
}