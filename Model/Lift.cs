using System.Text.Json.Serialization;

namespace GymLifts.Model;

public class Lift
{
    public float Weight { get; set; }
    public int Reps { get; set; }
    public float RPE { get; set; }
    public string Time { get; set; }
}
