﻿using GymLifts.Services;

namespace GymLifts.ViewModel;

/// <summary>
/// View model for the create exercise page
/// </summary>
public partial class CreateExerciseViewModel : BaseViewModel
{
    CategoryService categoryService;
    MuscleGroupService muscleGroupService;
    ExerciseService exerciseService;

    public ObservableCollection<string> Categories { get; } = new();
    public ObservableCollection<string> MuscleGroups { get; } = new();

    public string ExerciseName { get; set; } = "";
    private bool IsNamed => ExerciseName != null && ExerciseName != "";
    public string SelectedCategory { get; set; }
    private bool HasCategory => SelectedCategory != null && SelectedCategory != "";
    public string SelectedMuscleGroup { get; set; }
    private bool HasMuscleGroup => SelectedMuscleGroup != null && SelectedMuscleGroup != "";

    /// <summary>
    /// No two exercises may be the same, this method checks all the saved exercises for duplicate names
    /// </summary>
    /// <param name="exerciseName">The name to check</param>
    /// <returns>If a given name is a duplicate</returns>
    private async Task<bool> IsDuplicate(string exerciseName)
    {
        var exerciseList = await exerciseService.GetExercises();
        return exerciseList.Select(e => e.Name).Contains(exerciseName);
    }

    /// <summary>
    /// Creates a view model for the create exercise page
    /// </summary>
    /// <param name="categoryService">Associated <c>CategoryService</c></param>
    /// <param name="muscleGroupService">Associated <c>MuscleGroupService</c></param>
    /// <param name="exerciseService">Associated <c>ExerciseService</c></param>
    public CreateExerciseViewModel(CategoryService categoryService, MuscleGroupService muscleGroupService, ExerciseService exerciseService)
    {
        Title = "Register New Exercise";
        this.categoryService = categoryService;
        this.muscleGroupService = muscleGroupService;
        this.exerciseService = exerciseService;
    }

    /// <summary>
    /// Tries to save the exercise with data from the page to the list of saved exercises.
    /// Upon success/failure/exception, a popup will be prompted to the user
    /// </summary>
    [RelayCommand]
    async Task RegisterExerciseAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            if (!IsNamed) { await Shell.Current.DisplayAlert("Warning", "Please name the exercise.", "OK"); }
            else if (await IsDuplicate(ExerciseName)) { await Shell.Current.DisplayAlert("Warning", "This exercise is already registered.", "OK"); }
            else if (!HasCategory) { await Shell.Current.DisplayAlert("Warning", "Please select an exercise category.", "OK"); }
            else if (!HasMuscleGroup) { await Shell.Current.DisplayAlert("Warning", "Please select the primary targeted muscle group.", "OK"); }
            else 
            { 
                await exerciseService.SaveExerciseAsync(ExerciseName, SelectedCategory, SelectedMuscleGroup);
                IsBusy = false;
                await Shell.Current.DisplayAlert("Success!", $"Successfully registered exercise:\n{ExerciseName}", "OK");
                await Shell.Current.GoToAsync("..", true);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Error registering new exercise: {ex}", "OK");
        }
        finally { IsBusy = false; }
    }

    /// <summary>
    /// Reads and displays the exercise categories to the page asynchronously
    /// </summary>
    [RelayCommand]
    public async Task GetCategoriesAsync()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            var categories = await categoryService.GetCategories();

            if (categories.Length != 0)
                Categories.Clear();
            foreach (var category in categories)
                Categories.Add(category);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", "Error showing exercise categories", "OK");

        }
        finally
        {
            IsBusy = false;
        }
    }

    /// <summary>
    /// Reads and displays the muscle groups to the page asynchronously
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    public async Task GetMuscleGroupsAsync()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            var muscleGroups = await muscleGroupService.GetMuscleGroups();

            if (muscleGroups.Length != 0)
                MuscleGroups.Clear();
            foreach (var muscleGroup in muscleGroups)
                MuscleGroups.Add(muscleGroup);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", "Error showing muscle groups", "OK");

        }
        finally
        {
            IsBusy = false;
        }
    }
}
