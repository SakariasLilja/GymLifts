using GymLifts.Services;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace GymLifts
{
    /// <summary>
    /// The program associated to the app
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Creates a MAUI app with the needed associations
        /// </summary>
        /// <returns>The MAUI app with all functionalities</returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseSkiaSharp(true)
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Adding navigation settings

            builder.Services.AddSingleton<CategoryService>();
            builder.Services.AddSingleton<MuscleGroupService>();
            builder.Services.AddSingleton<ExerciseService>();
            builder.Services.AddTransient<LiftService>();

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<CreateExerciseViewModel>();
            builder.Services.AddSingleton<ManageLiftsViewModel>();
            builder.Services.AddSingleton<ManageExercisesViewModel>();
            builder.Services.AddTransient<ConfigureExerciseViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<CreateExercise>();
            builder.Services.AddSingleton<ManageLiftsPage>();
            builder.Services.AddSingleton<ManageExercisesPage>();
            builder.Services.AddTransient<ConfigureExercisePage>();

            return builder.Build();
        }
    }
}
