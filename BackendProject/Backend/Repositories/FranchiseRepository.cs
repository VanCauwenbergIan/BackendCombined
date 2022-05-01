namespace Games.Repositories;

public interface IFranchiseRepository
{
    Task<List<Franchise>> AddFranchises(List<Franchise> newFranchises);
    Task<List<Franchise>> GetAllFranchises();
    Task<Franchise> GetFranchise(string id);
    Task<Franchise> UpdateFranchise(string id, Franchise franchise);
}

public class FranchiseRepository : IFranchiseRepository
{
    private readonly IMongoContext _context;

    public FranchiseRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Franchise>> AddFranchises(List<Franchise> newFranchises)
    {
        try
        {
            newFranchises.ForEach(franchise => franchise.CreatedOn = DateTime.Now);
            await _context.FranchisesCollection.InsertManyAsync(newFranchises);
            return newFranchises;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Franchise> UpdateFranchise(string id, Franchise franchise)
    {
        try
        {
            await _context.FranchisesCollection.ReplaceOneAsync(f => f.Id == id, franchise);
            return franchise;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Franchise> GetFranchise(string id) => await _context.FranchisesCollection.Find<Franchise>(f => f.Id == id).FirstOrDefaultAsync();

    public async Task<List<Franchise>> GetAllFranchises() => await _context.FranchisesCollection.Find(_ => true).ToListAsync();
}