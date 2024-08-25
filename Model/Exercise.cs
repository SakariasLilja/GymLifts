using System.Text.Json.Serialization;

namespace GymLifts.Model;

public record Exercise
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string MuscleGroup { get; set; }
    public string ImageFile => Category.ToLower() + ".png";
}
