namespace Games.Models;

public class ReleaseDate
{
    public string Id { get; set; }
    public int TimeStamp { get; set; }
    public List<string>? PlatformIds { get; set; }
    public DateTime CreatedOn { get; set; }
}