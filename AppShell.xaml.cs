namespace GymLifts;

/// <summary>
/// The app shell.
/// Is responsible for navigation between pages and tabs
/// </summary>
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(CreateExercise), typeof(CreateExercise));
        Routing.RegisterRoute(nameof(ManageExercisesPage), typeof(ManageExercisesPage));
        Routing.RegisterRoute(nameof(ConfigureExercisePage), typeof(ConfigureExercisePage));
    }
}
