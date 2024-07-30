using GymLifts.Services;
using Microsoft.Extensions.Logging;

namespace GymLifts
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
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
            builder.Services.AddSingleton<ExerciseService>(); //Might need to be transient due to updates to file

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<CreateExerciseViewModel>();
            builder.Services.AddSingleton<ProfileViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<CreateExercise>();

            return builder.Build();
        }
    }
}
