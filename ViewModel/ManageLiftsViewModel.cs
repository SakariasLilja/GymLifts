using GymLifts.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

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

    public ISeries[] Series { get; set; }
        = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
                Fill = null
            }
        };
}
