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

    private List<Lift> Lifts { get; set; } = new List<Lift> ();

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

    public async Task RetrieveLiftsAsync()
    {
        if (IsBusy) 
            return;

        try
        {
            IsBusy = true;

            if (ValidExercise)
            {
                LiftService liftService = new LiftService(SelectedExercise.Name);
                var lifts = await liftService.GetLiftsAsync();
                Lifts = lifts;
            }
            else { Lifts.Clear(); }
        }

        catch (Exception) { Lifts.Clear(); }

        finally { IsBusy = false; }
    }

    public async Task SetGraph()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        ISeries[] NewSeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = Lifts.Select(l => l.Weight).ToArray(),
                Fill = null
            }
        };

        Series = NewSeries;

        IsBusy = false;
    }

    [ObservableProperty]
    public ISeries[] series = { };
}
