namespace GymLifts.Services;

public class ExerciseService
{
    public ExerciseService() { }

    List<Exercise> exerciseList = new ();

    private string DirectoryPath => Path.Combine(FileSystem.AppDataDirectory, "SLLiftsTracker");
    private string FilePath => Path.Combine(DirectoryPath, "saved_exercises.json");

    public async Task<List<Exercise>> GetDefaultExercises()
    {
        List<Exercise> output = new List<Exercise> ();
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("exercises.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            output = JsonSerializer.Deserialize<List<Exercise>>(contents);
        }
        catch (Exception ex) { Debug.WriteLine(ex); }

        return output;
    }

    private async Task<List<Exercise>> GetSavedExercises()
    {
        List<Exercise> output = new List<Exercise>();

        try
        {
            using var stream = File.OpenRead(FilePath);
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            output = JsonSerializer.Deserialize<List<Exercise>>(contents);
        }
        catch (DirectoryNotFoundException) { Directory.CreateDirectory(DirectoryPath); }
        catch (FileNotFoundException) { }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex);
        }
        return output;
    }

    public async Task<List<Exercise>> GetExercises()
    {
        if (exerciseList?.Count > 0)
            return exerciseList;

        var defaultExercises = await GetDefaultExercises();
        var savedExercises = await GetSavedExercises();

        try 
        {
            exerciseList.Clear();
            exerciseList = defaultExercises.Concat(savedExercises).ToList(); 
        }
        catch (Exception ex) { Debug.WriteLine(ex); }

        return exerciseList;
    }

    public async Task SaveExerciseAsync(string exerciseName, string category, string muscleGroup)
    {
        var savedExercises = await GetSavedExercises();

        Exercise exercise = new Exercise() { Name = exerciseName, Category = category, MuscleGroup = muscleGroup };
        savedExercises.Add(exercise);

        var serializedExercises = JsonSerializer.Serialize(savedExercises);

        using FileStream outputStream = File.OpenWrite(FilePath);
        using StreamWriter writer = new StreamWriter(outputStream);
        await writer.WriteAsync(serializedExercises);
    }
}
