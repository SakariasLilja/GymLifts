namespace GymLifts.View;

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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await viewModel.GetMuscleGroupsAsync();
        await viewModel.GetCategoriesAsync();
    }
}