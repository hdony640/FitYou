namespace fityou.Contracts.Requests;

public class CreateSessionRequest
{
    public DateTime Date { get; set; }
    public List<SessionExerciseRequest> Exercices { get; set; } = new();

}