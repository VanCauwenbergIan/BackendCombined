namespace Games.Repositories;

public interface IPlatformRepository
{
    Task<List<Platform>> AddPlatforms(List<Platform> newPlatforms);
    Task<List<Platform>> GetAllPlatforms();
    Task<Platform> GetPlatform(string id);
    Task<Platform> UpdatePlatform(string id, Platform platform);
}

public class PlatformRepository : IPlatformRepository
{
    private readonly IMongoContext _context;

    public PlatformRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Platform>> AddPlatforms(List<Platform> newPlatforms)
    {
        try
        {
            newPlatforms.ForEach(platform => platform.CreatedOn = DateTime.Now);
            await _context.PlatformsCollection.InsertManyAsync(newPlatforms);
            return newPlatforms;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Platform> UpdatePlatform(string id, Platform platform)
    {
        try
        {
            await _context.PlatformsCollection.ReplaceOneAsync(p => p.Id == id, platform);
            return platform;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Platform> GetPlatform(string id) => await _context.PlatformsCollection.Find<Platform>(p => p.Id == id).FirstOrDefaultAsync();

    public async Task<List<Platform>> GetAllPlatforms() => await _context.PlatformsCollection.Find(_ => true).ToListAsync();
}