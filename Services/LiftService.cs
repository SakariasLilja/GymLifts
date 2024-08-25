namespace GymLifts.Services;

public class LiftService
{
    private string exerciseName;
    List<Lift> lifts = new List<Lift>();
    private string FileName => exerciseName.Replace(' ', '_');
    private string DirectoryPath => Path.Combine(FileSystem.AppDataDirectory, "SLLiftsTracker");
    private string FilePath => Path.Combine(DirectoryPath, FileName);
    public LiftService(string exerciseName) 
    { 
        this.exerciseName = exerciseName; 
    }

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

    public async Task DeleteLiftsAsync()
    {
        Directory.CreateDirectory(DirectoryPath);
        await File.WriteAllTextAsync(FilePath, "");
    }
}
