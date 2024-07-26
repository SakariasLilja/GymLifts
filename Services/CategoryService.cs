namespace GymLifts.Services;

public class CategoryService
{
    public CategoryService() { }

    string[] categories = { };

    public async Task<string[]> GetCategories()
    {
        if (categories?.Length > 0)
            return categories;

        using var stream = await FileSystem.OpenAppPackageFileAsync("categories.csv");

        using var reader = new StreamReader(stream);

        var contents = await reader.ReadToEndAsync();

        categories = contents.Split(',');
        return categories;
    }
}
