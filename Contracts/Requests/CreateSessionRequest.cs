namespace fityou.Contracts.Requests;

public class CreateSessionRequest
{
    public int UserId { get; set; }
    public DateTime Date { get; set; }
}