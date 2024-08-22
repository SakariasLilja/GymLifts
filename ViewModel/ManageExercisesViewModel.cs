using GymLifts.Services;

namespace GymLifts.ViewModel;

public partial class ManageExercisesViewModel : BaseViewModel
{
    public ObservableCollection<Exercise> Exercises { get; } = new ();
    
    private List<Exercise> defaultExercises = new List<Exercise> ();

    ExerciseService exerciseService;
    public ManageExercisesViewModel(ExerciseService exerciseService)
    {
        Title = "Manage Exercises";
        this.exerciseService = exerciseService;
    }

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    public async Task GetExercisesAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            IsRefreshing = true;

            defaultExercises = await exerciseService.GetDefaultExercises();

            var exercises = await exerciseService.GetExercises();

            Exercises.Clear();

            foreach (var exercise in exercises)
                Exercises.Add(exercise);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Warning", "Error loading exercises", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GoToAddExerciseAsync()
    {
        if (IsBusy)
            return;

        await Shell.Current.GoToAsync(nameof(CreateExercise), true);
    }

    [RelayCommand]
    async Task GoToConfigureExerciseAsync(Exercise exercise)
    {
        if (IsBusy || exercise is null)
            return;

        await Shell.Current.GoToAsync(nameof(ConfigureExercisePage), true, 
            new Dictionary<string, object>
            {
                {"Exercise", exercise}
            });
    }
}
