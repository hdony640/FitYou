namespace fityou.Contracts.Requests;

public class SessionExerciseRequest
{
    public int ExerciseId { get; set; }
    public int Reps { get; set; }
    public int Weight { get; set; }
}