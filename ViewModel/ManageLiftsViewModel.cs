using GymLifts.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;

namespace GymLifts.ViewModel;

public partial class ManageLiftsViewModel : BaseViewModel
{
    private static readonly SKColor lineColor = new(255, 222, 173);
    private static readonly SKColor fillColor = new(0, 0, 0);
    private static readonly SKColor fadeColor = new(76, 64, 51);

    private static readonly int strokeWidth = 5;

    private static readonly LiveChartsCore.SkiaSharpView.Painting.LinearGradientPaint gradient =
        new LiveChartsCore.SkiaSharpView.Painting.LinearGradientPaint(new[] { fadeColor, lineColor })
        { StrokeThickness = strokeWidth };

    public ObservableCollection<Exercise> Exercises { get; } = new ();
    public Exercise SelectedExercise { get; set; }
    private bool ValidExercise => SelectedExercise != null;

    private ObservableCollection<Lift> Lifts { get; set; } = new ();

    ExerciseService exerciseService;
    public ManageLiftsViewModel(ExerciseService exerciseService) 
    {
        this.exerciseService = exerciseService;
        Title = "Manage Lifts and Exercises";

        Series = new ObservableCollection<ISeries>
        {
            new LineSeries<Lift>
            {
                Values = Lifts,
                GeometrySize = 2 * strokeWidth,
                Stroke = gradient,
                GeometryStroke = gradient,
                Mapping = (lift, index) => new (index, lift.Weight),
                YToolTipLabelFormatter = p => $"{p.Model?.Time}\n{p.Model?.Weight} kg\n{p.Model?.Reps} reps\nRPE: {p.Model?.RPE}",
                Fill = null
            }
        };
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
                Lifts.Clear();

                foreach (var lift in lifts)
                    Lifts.Add(lift);
            }
            else { Lifts.Clear(); }
        }

        catch (Exception) { Lifts.Clear(); }

        finally { IsBusy = false; }
    }

    public ObservableCollection<ISeries> Series { get; set; }
}
