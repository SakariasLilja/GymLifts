using GymLifts.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace GymLifts.ViewModel;

public partial class ManageLiftsViewModel : BaseViewModel
{
    public ObservableCollection<Exercise> Exercises { get; } = new ();
    public Exercise SelectedExercise { get; set; }
    private bool ValidExercise => SelectedExercise != null;

    public string[] YAxis { get; } = { "Weight", "Reps", "RPE" };
    public string SelectedYAxis { get; set; }
    private bool ValidYAxis => SelectedYAxis != null;

    private bool ValidAttributes => ValidExercise && ValidYAxis;

    ExerciseService exerciseService;
    public ManageLiftsViewModel(ExerciseService exerciseService) 
    {
        this.exerciseService = exerciseService;
        Title = "Manage Lifts and Exercises";
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
            if (Exercises?.Count > 0)
                return;

            IsBusy = true;
            IsRefreshing = true;
            var exercises = await exerciseService.GetExercises();

            foreach (var exercise in exercises)
                Exercises.Add(exercise);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        finally { IsBusy = false; IsRefreshing = false; }
    }

    [RelayCommand]
    async Task SetGraph()
    {
        ISeries[] NewSeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = new double[] { 1, 2, 3, 4, 5 },
                Fill = null
            }
        };

        Series = NewSeries;
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
