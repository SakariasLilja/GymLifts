using GymLifts.Services;

namespace GymLifts.ViewModel;

/// <summary>
/// View model for the configure exercise page
/// </summary>
[QueryProperty("Exercise", "Exercise")]
public partial class ConfigureExerciseViewModel : BaseViewModel
{
    public ObservableCollection<Lift> Lifts { get; } = new();
    public ConfigureExerciseViewModel() { }

    [ObservableProperty]
    Exercise exercise;

    [ObservableProperty]
    bool isRefreshing;

    /// <summary>
    /// Returns to the previous page from the navigation stack
    /// </summary>
    [RelayCommand]
    async Task GoBackButton()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    /// <summary>
    /// Gets and displays the lifts associated with this page asynchronously
    /// </summary>
    public async Task GetLiftsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            var liftService = new LiftService(this.Exercise.Name);

            var lifts = await liftService.GetLiftsAsync();

            Lifts.Clear();

            foreach (var lift in lifts)
                Lifts.Add(lift); 
        }
        catch (FileNotFoundException) { }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", "Error getting lifts.", "OK");
        }

        finally { IsBusy = false; }
    }

    /// <summary>
    /// Deletes a given lift asynchronously
    /// </summary>
    /// <param name="lift">The lift to delete</param>
    [RelayCommand]
    async Task DeleteLiftAsync(Lift lift)
    {
        if (IsBusy)
            return;

        string resultString = "Yes";
        var result = await Shell.Current.DisplayActionSheet("Delete this lift?", "Cancel", null, resultString);

        if (result != resultString)
            return;

        try
        {
            IsBusy = true;

            var liftService = new LiftService(this.Exercise.Name);
            await liftService.DeleteLiftAsync(lift);

            await Shell.Current.DisplayAlert("Success", "Successfully removed lift.", "OK");

            Lifts.Remove(lift);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Warning", "Error removing lift.", "OK");
        }
        finally { IsBusy = false; }
    }

}
