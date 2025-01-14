namespace GymLifts.Services;

/// <summary>
/// Service for modifying the lifts associated to an exercise
/// </summary>
public class LiftService
{
    private string exerciseName;
    List<Lift> lifts = new List<Lift>();
    private string FileName => exerciseName.Replace(' ', '_');
    private string DirectoryPath => Path.Combine(FileSystem.AppDataDirectory, "SLLiftsTracker");
    private string FilePath => Path.Combine(DirectoryPath, FileName);

    /// <summary>
    /// Opens a service to access the lifts associated to this exercise
    /// </summary>
    /// <param name="exerciseName">The exercise whose lifts to handle</param>
    public LiftService(string exerciseName) 
    { 
        this.exerciseName = exerciseName; 
    }

    /// <summary>
    /// Reads the lifts assigned to this service
    /// </summary>
    /// <returns>A list of lifts</returns>
    public async Task<List<Lift>> GetLiftsAsync()
    {
        if (lifts?.Count != 0)
            return lifts;

        using var stream = File.OpenRead(FilePath);
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();

        //Stops error from occurring when JSON file is empty
        if (contents != "")
            lifts = JsonSerializer.Deserialize<List<Lift>>(contents);

        return lifts;
    }
    
    /// <summary>
    /// Saves a lift asynchronously
    /// </summary>
    /// <param name="lift">The lift to save to the list of lifts</param>
    public async Task SaveLiftAsync(Lift lift)
    {
        try { await GetLiftsAsync(); }
        catch (DirectoryNotFoundException) { Directory.CreateDirectory(DirectoryPath); }
        catch (FileNotFoundException) { }
        finally
        {
            lifts.Add(lift);
            var jsonString = JsonSerializer.Serialize(lifts);

            using var stream = File.OpenWrite(FilePath);
            using var writer = new StreamWriter(stream);
            await writer.WriteAsync(jsonString);
        }
    }

    /// <summary>
    /// Deletes a specific lift asynchronously
    /// </summary>
    /// <param name="lift">The lift to remove</param>
    public async Task DeleteLiftAsync(Lift lift)
    {
        try { await GetLiftsAsync(); }
        catch (DirectoryNotFoundException) { Directory.CreateDirectory(DirectoryPath); }
        catch (FileNotFoundException) { }
        finally
        {
            lifts?.Remove(lift);
            var jsonString = JsonSerializer.Serialize(lifts);

            //Clears out all previous lifts correctly, as overwriting with fewer characters doesn't work
            await DeleteLiftsAsync();

            using var stream = File.OpenWrite(FilePath);
            using var writer = new StreamWriter(stream);
            await writer.WriteAsync(jsonString);
        }
    }

    /// <summary>
    /// Deletes all lifts associated to this <c>LiftService</c>
    /// </summary>
    public async Task DeleteLiftsAsync()
    {
        Directory.CreateDirectory(DirectoryPath);
        await File.WriteAllTextAsync(FilePath, "");
    }
}
