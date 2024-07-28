using GymLifts.Services;

namespace GymLifts.ViewModel;

public partial class MainPageViewModel : BaseViewModel
{
    ExerciseService exerciseService;

    public ObservableCollection<Exercise> Exercises { get; } = new ();

    public MainPageViewModel(ExerciseService exerciseService)
    {
        Title = "Gym Lifts Tracker";
        this.exerciseService = exerciseService;
    }

    [RelayCommand]
    async Task GetExercisesAsync()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;
            var exercises = await exerciseService.GetExercises();

            if (exercises.Count != 0)
                Exercises.Clear();

            foreach (var exercise in exercises)
                Exercises.Add(exercise);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", "Error getting exercises", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
