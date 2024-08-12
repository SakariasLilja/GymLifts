using GymLifts.Services;

namespace GymLifts.ViewModel;

public partial class ManageExercisesViewModel : BaseViewModel
{
    ExerciseService exerciseService;
    public ManageExercisesViewModel(ExerciseService exerciseService)
    {
        Title = "Manage Exercises";
        this.exerciseService = exerciseService;
    }
}
