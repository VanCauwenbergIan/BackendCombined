namespace Games.Configuration;

public class DatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? GenresCollection { get; set; }
    public string? GameModesCollection { get; set; }
    public string? PlayerPerspectivesCollection { get; set; }
    public string? FranchisesCollection { get; set; }
    public string? ThemesCollection { get; set; }
    public string? CompaniesCollection { get; set; }
    public string? PlatformsCollection { get; set; }
    public string? GamesCollection { get; set; }
}