namespace Games.DTO;

public class GameDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? FranchiseName { get; set; }
    public List<Rating>? Ratings { get; set; }
    public string? CoverUrl { get; set; }
    public List<ReleaseDateDTO>? ReleaseDates { get; set; }

    public List<string>? GameModeNames { get; set; }
    public List<string>? PlayerPerspectiveNames { get; set; }
    public string DeveloperName { get; set; }
    public string PublisherName { get; set; }
    public List<string>? GenreNames { get; set; }
    public List<string>? ThemeNames { get; set; }
    public List<string>? PlatformNames { get; set; }
    public DateTime CreatedOn { get; set; }
}