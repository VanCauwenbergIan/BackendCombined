namespace Games.DTO;

public class ReleaseDateDTO
{
    public string Id { get; set; }
    public int TimeStamp { get; set; }
    public List<string>? PlatformNames { get; set; }
    public DateTime CreatedOn { get; set; }
}