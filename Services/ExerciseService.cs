namespace GymLifts.Services;

public class ExerciseService
{
    public ExerciseService() { }

    List<Exercise> exerciseList = new ();

    public async Task<List<Exercise>> GetExercises()
    {
        if (exerciseList?.Count > 0)
            return exerciseList;

        using var stream = await FileSystem.OpenAppPackageFileAsync("exercises.json");

        using var reader = new StreamReader(stream);

        var contents = await reader.ReadToEndAsync();
        try
        {
            exerciseList = JsonSerializer.Deserialize<List<Exercise>>(contents);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Exercise exercise = new Exercise();
            exercise.Name = "Error loading exercises";
            exercise.MuscleGroup = "Null";
            exercise.Category = "Null";
            exerciseList.Add(exercise);
        }

        return exerciseList;
    }
}
