namespace fityou.Domains.Entities;

public class SessionExercise
{
    public int Id { get; set; }
    public int ExerciseId { get; set; }
    public int SessionId { get; set; }
    public int Reps { get; set; }
    public int Weight { get; set; }

    public Exercise Exercise { get; set; } = null!;
    public Session Session { get; set; } = null!;
}