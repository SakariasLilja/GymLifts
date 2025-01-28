using SQLite;

namespace GymLifts.Services;

/// <summary>
/// Service for modifying the lifts associated to an exercise
/// </summary>
public class LiftService
{
    private string exerciseName;

    public const string DatabaseFilename = "GymLiftsSQLite.db3";
    private readonly SQLiteAsyncConnection _connection;

    /// <summary>
    /// Opens a service to access the lifts associated to this exercise
    /// </summary>
    /// <param name="exerciseName">The exercise whose lifts to handle</param>
    public LiftService(string exerciseName) 
    { 
        this.exerciseName = exerciseName;
        _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename));
        _connection.CreateTableAsync<Lift>();
    }

    /// <summary>
    /// Reads the lifts assigned to this service
    /// </summary>
    /// <returns>A list of lifts</returns>
    public async Task<List<Lift>> GetLiftsAsync()
    {
        return await _connection.Table<Lift>().Where(l => l.Name == exerciseName).ToListAsync();
    }
    
    /// <summary>
    /// Saves a lift asynchronously
    /// </summary>
    /// <param name="lift">The lift to record</param>
    public async Task SaveLiftAsync(Lift lift)
    {
        lift.Name = exerciseName;
        await _connection.InsertAsync(lift);
    }

    /// <summary>
    /// Updates a lift asynchronously
    /// </summary>
    /// <param name="lift">The lift to update</param>
    public async Task UpdateLiftAsync(Lift lift)
    {
        lift.Name = exerciseName;
        await _connection.UpdateAsync(lift);
    }

    /// <summary>
    /// Deletes a specific lift asynchronously
    /// </summary>
    /// <param name="lift">The lift to remove</param>
    public async Task DeleteLiftAsync(Lift lift)
    {
        await _connection.DeleteAsync(lift);
    }

    /// <summary>
    /// Deletes all lifts associated to this <c>LiftService</c>
    /// </summary>
    public async Task DeleteLiftsAsync()
    {
        List<Lift> lifts = await GetLiftsAsync();
        foreach (Lift lift in lifts) { await _connection.DeleteAsync(lift); }
    }
}
