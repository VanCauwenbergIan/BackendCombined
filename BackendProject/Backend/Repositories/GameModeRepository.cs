namespace Games.Repositories;

public interface IGameModeRepository
{
    Task<List<GameMode>> AddGameModes(List<GameMode> newGameModes);
    Task<List<GameMode>> GetAllGameModes();
    Task<GameMode> GetGameMode(string id);
    Task<GameMode> UpdateGameMode(string id, GameMode gameMode);
}

public class GameModeRepository : IGameModeRepository
{
    private readonly IMongoContext _context;

    public GameModeRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<GameMode>> AddGameModes(List<GameMode> newGameModes)
    {
        try
        {
            newGameModes.ForEach(gameMode => gameMode.CreatedOn = DateTime.Now);
            await _context.GameModesCollection.InsertManyAsync(newGameModes);
            return newGameModes;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<GameMode> UpdateGameMode(string id, GameMode gameMode)
    {
        try
        {
            await _context.GameModesCollection.ReplaceOneAsync(g => g.Id == id, gameMode);
            return gameMode;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<GameMode> GetGameMode(string id) => await _context.GameModesCollection.Find<GameMode>(g => g.Id == id).FirstOrDefaultAsync();

    public async Task<List<GameMode>> GetAllGameModes() => await _context.GameModesCollection.Find(_ => true).ToListAsync();
}