using System.Text.Json.Serialization;

namespace GymLifts.Model;

public class Lift
{
    public double Weight { get; set; }
    public int Reps { get; set; }
    public double RPE { get; set; }
    public string Time { get; set; }
}
