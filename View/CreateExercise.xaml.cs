namespace GymLifts.View;

/// <summary>
/// Content page for the create exercise page
/// </summary>
public partial class CreateExercise : ContentPage
{
	CreateExerciseViewModel viewModel;
	public CreateExercise(CreateExerciseViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    /// <summary>
    /// Updates the view to display the available muscle groups and exercise categories
    /// when navigated to
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await viewModel.GetMuscleGroupsAsync();
        await viewModel.GetCategoriesAsync();
    }
}