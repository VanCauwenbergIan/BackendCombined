namespace Games.GraphQL;

public record AddGameInput(string Name, string? FranchiseId, List<Rating>? Ratings, string? CoverUrl, List<ReleaseDate>? ReleaseDates, List<string>? GameModeIds, List<string>? PlayerPerspectiveIds, string DeveloperId, string PublisherId, List<string>? GenreIds, List<string>? ThemeIds , List<string>? PlatformIds);