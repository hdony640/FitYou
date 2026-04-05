namespace fityou.Domains.Entities;

public class Session
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }

    public ICollection<SessionExercise> SessionExercises { get; set; } = new List<SessionExercise>();
}