using GymLifts.Services;

namespace GymLifts.ViewModel;

[QueryProperty("Exercise", "Exercise")]
public partial class ConfigureExerciseViewModel : BaseViewModel
{
    public ObservableCollection<Lift> Lifts { get; } = new();
    public ConfigureExerciseViewModel() { }

    [ObservableProperty]
    Exercise exercise;

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async Task GoBackButton()
    {
        await Shell.Current.GoToAsync("..", true);
    }

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

        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", "Error getting lifts.", "OK");
        }

        finally { IsBusy = false; }
    }

}
