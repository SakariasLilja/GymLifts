namespace GymLifts.View;

/// <summary>
/// Content page for the manage exercises page
/// </summary>
public partial class ManageExercisesPage : ContentPage
{

	ManageExercisesViewModel viewModel;
	public ManageExercisesPage(ManageExercisesViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
	}

	/// <summary>
	/// Loads the exercises to the page when navigated to
	/// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

		await viewModel.GetExercisesAsync();
    }
}