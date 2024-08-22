namespace GymLifts.ViewModel;

[QueryProperty("Exercise", "Exercise")]
public partial class ConfigureExerciseViewModel : BaseViewModel
{
    public ConfigureExerciseViewModel() { }

    [ObservableProperty]
    Exercise exercise;

    [ObservableProperty]
    bool isRefreshing;

}
