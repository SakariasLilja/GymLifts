﻿using GymLifts.Services;

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
    private bool ValidReps(out int reps) => int.TryParse(Reps, out reps);

    public string Weight { get; set; } = "";
    private bool ValidWeight(out double weight) => double.TryParse(Weight, out weight);

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

    [RelayCommand]
    async Task RecordLiftAsync()
    {
        try
        {
            if (IsBusy)
                return;
            IsBusy = true;
            int reps;
            double weight;

            if (!ValidExercise)
                await Shell.Current.DisplayAlert("Warning", "Please select an exercise.", "OK");
            else if (!ValidRPE)
                await Shell.Current.DisplayAlert("Warning", "Please select a persieved RPE", "OK");
            else if (!ValidReps(out reps))
                await Shell.Current.DisplayAlert("Warning", "Please enter number of repetitions performed as a whole number.", "OK");
            else if (!ValidWeight(out weight))
                await Shell.Current.DisplayAlert("Warning", "Please enter the amount of weight lifted / your bodyweight.", "OK");
            else
            {
                var time = DateTime.Now.ToString();
                LiftService liftService = new LiftService(SelectedExercise.Name);
                Lift lift = new Lift() { Reps = reps, RPE = RPEs[RPEIndex], Time = time, Weight = weight };
                await liftService.SaveLiftAsync(lift);
                await Shell.Current.DisplayAlert("Success", $"Successfully recorded lift:\n{SelectedExercise.Name}\nWeight: {weight}, Reps: {reps}, RPE: {RPEs[RPEIndex]}", "OK");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex.Message}");
            await Shell.Current.DisplayAlert("Error", "Fatal error recording lift.", "OK");
        }
        finally { IsBusy = false; }
    }
}
