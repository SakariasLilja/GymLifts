using System.Text.Json.Serialization;

namespace GymLifts.Model;

/// <summary>
/// <c>Lift</c> record that stores information of the weight lifted, the 
/// number of repetitions (reps) performed, the rate of persieved exertion (RPE), 
/// and the date-time the lift was set
/// </summary>
public record Lift
{
    public double Weight { get; set; }
    public int Reps { get; set; }
    public double RPE { get; set; }
    public string Time { get; set; }
}
