namespace GymLifts.Services;

/// <summary>
/// Service for reading the exercise categories file and assembling the
/// contents in a readable format
/// </summary>
public class CategoryService
{
    public CategoryService() { }

    string[] categories = { };

    /// <summary>
    /// Reads the exercise categories file
    /// </summary>
    /// <returns>The contents of the exercise categories file as an array of strings</returns>
    public async Task<string[]> GetCategories()
    {
        if (categories?.Length > 0)
            return categories;

        using var stream = await FileSystem.OpenAppPackageFileAsync("exercisecategories.csv");

        using var reader = new StreamReader(stream);

        var contents = await reader.ReadToEndAsync();

        categories = contents.Split(',');
        return categories;
    }
}
