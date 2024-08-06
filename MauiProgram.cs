using GymLifts.Services;
using Microsoft.Extensions.Logging;
using Microcharts.Maui;

namespace GymLifts
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<CategoryService>();
            builder.Services.AddSingleton<MuscleGroupService>();
            builder.Services.AddSingleton<ExerciseService>();
            builder.Services.AddTransient<LiftService>();

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<CreateExerciseViewModel>();
            builder.Services.AddSingleton<ManageLiftsViewModel>();
            builder.Services.AddSingleton<ProfileViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<CreateExercise>();
            builder.Services.AddSingleton<ManageLiftsPage>();
            builder.Services.AddSingleton<ProfilePage>();

            return builder.Build();
        }
    }
}
