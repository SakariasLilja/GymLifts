namespace GymLifts.Services;

public class MuscleGroupService
{
    public MuscleGroupService() { }

    string[] muscleGroups = { };

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
