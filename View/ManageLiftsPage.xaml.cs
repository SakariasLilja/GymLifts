namespace GymLifts.View;

/// <summary>
/// Content page for the manage lifts page
/// </summary>
public partial class ManageLiftsPage : ContentPage
{
	ManageLiftsViewModel viewModel;
	public ManageLiftsPage(ManageLiftsViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
	}

	/// <summary>
	/// Loads the exercises and lifts when page is navigated to
	/// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

		await viewModel.GetExercisesAsync();
		await viewModel.RetrieveLiftsAsync();
    }

	/// <summary>
	/// Method that reloads the lifts on the page when called
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private async void RetrieveLifts(object sender, EventArgs e)
	{
		await viewModel.RetrieveLiftsAsync();
	}
}