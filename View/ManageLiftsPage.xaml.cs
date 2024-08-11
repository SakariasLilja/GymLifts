namespace GymLifts.View;

public partial class ManageLiftsPage : ContentPage
{
	ManageLiftsViewModel viewModel;
	public ManageLiftsPage(ManageLiftsViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		await viewModel.GetExercisesAsync();
		await viewModel.RetrieveLiftsAsync();
    }

	private async void RetrieveLifts(object sender, EventArgs e)
	{
		await viewModel.RetrieveLiftsAsync();
	}
}