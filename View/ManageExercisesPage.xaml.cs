namespace GymLifts.View;

public partial class ManageExercisesPage : ContentPage
{
	public ManageExercisesPage(ManageExercisesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}