using GymLifts.Services;

namespace GymLifts.ViewModel;

public partial class MainPageViewModel : BaseViewModel
{
    ExerciseService exerciseService;

    public ObservableCollection<Exercise> Exercises { get; } = new ();
    public float[] RPEs { get; set; } = { 4, 4.5f, 5, 5.5f, 6, 6.5f, 7, 7.5f, 8, 8.5f, 9, 9.5f, 10};
    public string Reps { get; set; } = "";
    public string Weight { get; set; } = "";

    public MainPageViewModel(ExerciseService exerciseService)
    {
        Title = "Gym Lifts Tracker";
        this.exerciseService = exerciseService;
    }

    [RelayCommand]
    async Task GoToCreateExerciseAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(CreateExercise)}", true);
    }

    [RelayCommand]
    public async Task GetExercisesAsync()
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
