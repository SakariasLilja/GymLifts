using System.Text.Json.Serialization;

namespace GymLifts.Model;

public class Exercise
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string MuscleGroup { get; set; }
}
