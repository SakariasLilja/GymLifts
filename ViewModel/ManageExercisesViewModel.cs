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

    public async Task GetExercisesAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            defaultExercises = await exerciseService.GetDefaultExercises();

            var exercises = await exerciseService.GetExercises();

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
}
