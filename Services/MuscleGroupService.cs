namespace GymLifts.Services;

/// <summary>
/// Service to read the available muscle groups
/// </summary>
public class MuscleGroupService
{
    public MuscleGroupService() { }

    string[] muscleGroups = { };

    /// <summary>
    /// Reads the file containing the list of muscle groups provided by the application
    /// </summary>
    /// <returns>An array of muscle groups, represented as strings</returns>
    public async Task<string[]> GetMuscleGroups()
    {
        if (muscleGroups?.Length > 0)
            return muscleGroups;

        using var stream = await FileSystem.OpenAppPackageFileAsync("musclegroups.csv");

        using var reader = new StreamReader(stream);

        var contents = await reader.ReadToEndAsync();

        muscleGroups = contents.Split(',');
        return muscleGroups;
    }
}
