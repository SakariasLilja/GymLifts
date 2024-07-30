using GymLifts.View;

namespace GymLifts;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(CreateExercise), typeof(CreateExercise));
    }
}
