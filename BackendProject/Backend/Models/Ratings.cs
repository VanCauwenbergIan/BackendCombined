namespace Games.Models;

public class Rating
{
    public string Id { get; set; }
    public string Author { get; set; }
    public int Score { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedOn { get; set; }
}