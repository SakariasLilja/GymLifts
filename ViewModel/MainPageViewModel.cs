using GymLifts.Services;

namespace GymLifts.ViewModel;

public partial class MainPageViewModel : BaseViewModel
{
    ExerciseService exerciseService;

    public ObservableCollection<Exercise> Exercises { get; } = new ();
    public Exercise SelectedExercise { get; set; }
    private bool ValidExercise => SelectedExercise != null;

    public double[] RPEs { get; } = { 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10};
    public int RPEIndex { get; set; }
    private bool ValidRPE => RPEIndex >= 0;

    public string Reps { get; set; } = "";
    private int reps;
    private bool ValidReps => int.TryParse(Reps, out reps);

    public string Weight { get; set; } = "";
    private double weight;
    private bool ValidWeight => double.TryParse(Weight, out weight);

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
