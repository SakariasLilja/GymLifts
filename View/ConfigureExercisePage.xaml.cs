namespace GymLifts.View;

/// <summary>
/// Content page for the configure exercise page
/// </summary>
public partial class ConfigureExercisePage : ContentPage
{
	ConfigureExerciseViewModel viewModel;
	public ConfigureExercisePage(ConfigureExerciseViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
	}

	/// <summary>
	/// Updates the visible lifts when appearing
	/// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await viewModel.GetLiftsAsync();
    }
}