namespace GymLifts.Services;

/// <summary>
/// Service for CRUD operations on the exercises stored by the system
/// </summary>
public class ExerciseService
{
    public ExerciseService() { }

    List<Exercise> exerciseList = new ();

    private string DirectoryPath => Path.Combine(FileSystem.AppDataDirectory, "SLLiftsTracker");
    private string FilePath => Path.Combine(DirectoryPath, "saved_exercises.json");

    /// <summary>
    /// The application will always contain some basic exercises to be displayed, 
    /// which are read through this method.
    /// </summary>
    /// <returns>A list of default incluced exercises</returns>
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

    /// <summary>
    /// Reads the exercises saved by the user
    /// </summary>
    /// <returns>A list of user saved exercises</returns>
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

    /// <summary>
    /// Method that combines the <c>GetDefaultExercises</c> and <c>GetSavedExercises</c> methods
    /// </summary>
    /// <returns>A list of all saved exercises</returns>
    public async Task<List<Exercise>> GetExercises()
    {
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

    /// <summary>
    /// Saves a given exercise to the list of saved exercises
    /// </summary>
    /// <param name="exerciseName">The name of the exercise</param>
    /// <param name="category">The category of the exercise</param>
    /// <param name="muscleGroup">The primary targeted muscle group of the exercise</param>
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

    /// <summary>
    /// Deletes a given exercise from the user saved exercises
    /// </summary>
    /// <param name="exercise">The exercise to delete</param>
    public async Task DeleteExerciseAync(Exercise exercise)
    {
        var savedExercises = await GetSavedExercises();

        var i = savedExercises.FindIndex(e => e.Name == exercise.Name);

        if (i >= 0)
            savedExercises.RemoveAt(i);

        var serializedExercises = JsonSerializer.Serialize(savedExercises);

        //Deleting contents of file as overwriting with fewer charcters doesn't work
        await File.WriteAllTextAsync(FilePath, "");

        using var stream = File.OpenWrite(FilePath);
        using var writer = new StreamWriter(stream);
        await writer.WriteAsync(serializedExercises);
    }
}
