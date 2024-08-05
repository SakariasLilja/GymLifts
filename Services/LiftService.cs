namespace GymLifts.Services;

public class LiftService
{
    private string exerciseName;
    List<Lift> lifts = new List<Lift>();
    private string FileName => exerciseName.Replace(' ', '_');
    private string FilePath => Path.Combine(FileSystem.AppDataDirectory, FileName);
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
        lifts = JsonSerializer.Deserialize<List<Lift>>(contents);
        return lifts;
    }
    
    public async Task SaveLiftAsync(Lift lift)
    {
        try { await GetLiftsAsync(); }
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
}
