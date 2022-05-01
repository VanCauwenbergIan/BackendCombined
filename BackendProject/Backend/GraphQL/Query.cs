namespace Games.GraphQL;

public class Query
{
    public async Task<List<Genre>> GetGenres([Service] IGameService gameService) => await gameService.GetGenres();

    public async Task<List<GameMode>> GetGameModes([Service] IGameService gameService) => await gameService.GetGameModes();

    public async Task<List<PlayerPerspective>> GetPlayerPerspectives([Service] IGameService gameService) => await gameService.GetPlayerPerspectives();

    public async Task<List<Franchise>> GetFranchises([Service] IGameService gameService) => await gameService.GetFranchises();

    public async Task<List<Theme>> GetThemes([Service] IGameService gameService) => await gameService.GetThemes();

    public async Task<List<Company>> GetCompanies([Service] IGameService gameService) => await gameService.GetCompanies();

    public async Task<List<Platform>> GetPlatforms([Service] IGameService gameService) => await gameService.GetPlatforms();

    public async Task<List<Game>> GetGames([Service] IGameService gameService) => await gameService.GetGames();
}