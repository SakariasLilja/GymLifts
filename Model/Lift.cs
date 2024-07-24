using System.Text.Json.Serialization;

namespace GymLifts.Model;

public class Lift
{
    public string Name { get; set; }
    public float Weight { get; set; }
    public int Reps { get; set; }
    public float RPE { get; set; }
    public string Time { get; set; }
    public string Category { get; set; }
    public string MuscleGroup { get; set; }
}
