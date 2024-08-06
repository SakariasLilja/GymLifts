using GymLifts.Services;

namespace GymLifts.ViewModel;

public partial class ManageLiftsViewModel : BaseViewModel
{
    public ObservableCollection<Exercise> Exercises { get; } = new ();

    ExerciseService exerciseService;
    public ManageLiftsViewModel(ExerciseService exerciseService) 
    {
        this.exerciseService = exerciseService;
        Title = "Manage Lifts and Exercises";
    }

    [RelayCommand]
    public async Task GetExercisesAsync()
    {
        if (IsBusy)
            return;
        
        try
        {
            if (Exercises?.Count > 0)
                return;

            IsBusy = true;
            var exercises = await exerciseService.GetExercises();

            foreach (var exercise in exercises)
                Exercises.Add(exercise);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        finally { IsBusy = false; }
    }
}
