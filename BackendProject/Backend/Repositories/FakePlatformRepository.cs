namespace Games.Repositories;

public class FakePlatformRepository : IPlatformRepository
{
    public static List<Platform> _platforms = new List<Platform>();
    public Task<List<Platform>> AddPlatforms(List<Platform> newPlatforms)
    {
        foreach (Platform p in newPlatforms)
        {
            p.Id = Guid.NewGuid().ToString();
            p.CreatedOn = DateTime.Now;
        }

        _platforms.AddRange(newPlatforms);
        return Task.FromResult(newPlatforms);
    }

    public Task<List<Platform>> GetAllPlatforms() => Task.FromResult(_platforms);

    public Task<Platform> GetPlatform(string id) => Task.FromResult(_platforms.Where(p => p.Id == id).Single());

    public Task<Platform> UpdatePlatform(string id, Platform platform)
    {
        try
        {
            int i = _platforms.FindIndex(p => p.Id == id);
            var item = _platforms[i];
            if (item != null)
            {
                item.Name = platform.Name;
                item.ReleaseDate = platform.ReleaseDate;
                item.ManufacturerId = platform.ManufacturerId;
            }
            return Task.FromResult(item);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}