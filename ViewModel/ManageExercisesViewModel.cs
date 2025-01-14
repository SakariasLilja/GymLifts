using GymLifts.Services;

namespace GymLifts.ViewModel;

/// <summary>
/// View model for the manage exercises page
/// </summary>
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

    /// <summary>
    /// Reads and displays the exercises asynchronously
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    public async Task GetExercisesAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

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

    /// <summary>
    /// Navigates the user to the <c>CreateExercise</c> page
    /// </summary>
    [RelayCommand]
    async Task GoToAddExerciseAsync()
    {
        if (IsBusy)
            return;

        await Shell.Current.GoToAsync(nameof(CreateExercise), true);
    }

    /// <summary>
    /// Navigates the user to the <c>ConfigureExercisePage</c> page
    /// </summary>
    /// <param name="exercise">The <c>Exercise</c> association of the navigation</param>
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

    /// <summary>
    /// Deletes the given exercise from the application
    /// </summary>
    /// <param name="exercise">The <c>Exercise</c> to delete</param>
    [RelayCommand]
    async Task DeleteExerciseAsync(Exercise exercise)
    {
        if (IsBusy)
            return;

        string resultString = "Yes";
        var result = await Shell.Current.DisplayActionSheet("Delete this exercise?", "Cancel", null, resultString);

        if (result != resultString)
            return;

        try
        {
            IsBusy = true;

            var liftService = new LiftService(exercise.Name);
            await liftService.DeleteLiftsAsync();

            await Shell.Current.DisplayAlert("Success", "Successfully removed exercise and all its data.", "OK");

            if (!defaultExercises.Contains(exercise))
            {
                await exerciseService.DeleteExerciseAync(exercise);
                Exercises.Remove(exercise);
            }
                
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Warning", "Error deleting exercise files.", "OK");
        }
        finally { IsBusy = false; }
    }
}
