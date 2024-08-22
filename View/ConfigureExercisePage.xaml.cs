namespace GymLifts.View;

public partial class ConfigureExercisePage : ContentPage
{
	public ConfigureExercisePage(ConfigureExerciseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}