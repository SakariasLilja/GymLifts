using System.Text.Json.Serialization;

namespace GymLifts.Model;

/// <summary>
/// <c>Exercise</c> record that contains its name, category, associated muscle group and image file
/// </summary>
public record Exercise
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string MuscleGroup { get; set; }
    public string ImageFile => Category.ToLower() + ".png";
}
