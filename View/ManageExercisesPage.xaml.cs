namespace GymLifts.View;

public partial class ManageExercisesPage : ContentPage
{

	ManageExercisesViewModel viewModel;
	public ManageExercisesPage(ManageExercisesViewModel viewModel)
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