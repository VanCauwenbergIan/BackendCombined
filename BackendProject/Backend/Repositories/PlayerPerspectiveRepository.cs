namespace Games.Repositories;

public interface IPlayerPerspectiveRepository
{
    Task<List<PlayerPerspective>> AddPlayerPerspectives(List<PlayerPerspective> newPlayerPerspectives);
    Task<List<PlayerPerspective>> GetAllPlayerPerspectives();
    Task<PlayerPerspective> GetPlayerPerspective(string id);
    Task<PlayerPerspective> UpdatePlayerPerspective(string id, PlayerPerspective playerPerspective);
}

public class PlayerPerspectiveRepository : IPlayerPerspectiveRepository
{
    private readonly IMongoContext _context;

    public PlayerPerspectiveRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<PlayerPerspective>> AddPlayerPerspectives(List<PlayerPerspective> newPlayerPerspectives)
    {
        try
        {
            newPlayerPerspectives.ForEach(playerPerspective => playerPerspective.CreatedOn = DateTime.Now);
            await _context.PlayerPerspectivesCollection.InsertManyAsync(newPlayerPerspectives);
            return newPlayerPerspectives;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<PlayerPerspective> UpdatePlayerPerspective(string id, PlayerPerspective playerPerspective)
    {
        try
        {
            await _context.PlayerPerspectivesCollection.ReplaceOneAsync(p => p.Id == id, playerPerspective);
            return playerPerspective;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<PlayerPerspective> GetPlayerPerspective(string id) => await _context.PlayerPerspectivesCollection.Find<PlayerPerspective>(p => p.Id == id).FirstOrDefaultAsync();

    public async Task<List<PlayerPerspective>> GetAllPlayerPerspectives() => await _context.PlayerPerspectivesCollection.Find(_ => true).ToListAsync();
}