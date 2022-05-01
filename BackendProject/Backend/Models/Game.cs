namespace Games.Models;

public class Game : Generic
{
    public string? FranchiseId { get; set; }
    public List<Rating>? Ratings { get; set; }
    public string? CoverUrl { get; set; }
    public List<ReleaseDate>? ReleaseDates { get; set; }

    public List<string>? GameModeIds { get; set; }
    public List<string>? PlayerPerspectiveIds { get; set; }
    public string DeveloperId { get; set; }
    public string PublisherId { get; set; }
    public List<string>? GenreIds { get; set; }
    public List<string>? ThemeIds { get; set; }
    public List<string>? PlatformIds { get; set; }
}