using SQLite;
using System.Text.Json.Serialization;

namespace GymLifts.Model;

/// <summary>
/// <c>Lift</c> record that stores information of the weight lifted, the 
/// number of repetitions (reps) performed, the rate of persieved exertion (RPE), 
/// and the date-time the lift was set
/// </summary>
[Table("lifts")]
public class Lift
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Column("exercise_name")]
    public string Name { get; set; }

    [Column("weight")]
    public double Weight { get; set; }

    [Column("reps")]
    public int Reps { get; set; }

    [Column("rpe")]
    public double RPE { get; set; }

    [Column("time")]
    public string Time { get; set; }
}
