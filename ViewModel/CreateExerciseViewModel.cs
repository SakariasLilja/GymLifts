﻿using GymLifts.Services;

namespace GymLifts.ViewModel;

public partial class CreateExerciseViewModel : BaseViewModel
{
    CategoryService categoryService;
    MuscleGroupService muscleGroupService;

    public ObservableCollection<string> Categories { get; } = new();
    public ObservableCollection<string> MuscleGroups { get; } = new();

    public CreateExerciseViewModel(CategoryService categoryService, MuscleGroupService muscleGroupService)
    {
        Title = "Register New Exercise";
        this.categoryService = categoryService;
        this.muscleGroupService = muscleGroupService;
    }

    [RelayCommand]
    async Task GetCategoriesAsync()
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

    [RelayCommand]
    async Task GetMuscleGroupsAsync()
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
