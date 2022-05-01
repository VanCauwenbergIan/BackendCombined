namespace Games.Repositories;

public interface IGenreRepository
{
    Task<List<Genre>> AddGenres(List<Genre> newGenres);
    Task<List<Genre>> GetAllGenres();
    Task<Genre> GetGenre(string id);
    Task<Genre> UpdateGenre(string id, Genre genre);
}

public class GenreRepository : IGenreRepository
{
    private readonly IMongoContext _context;

    public GenreRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Genre>> AddGenres(List<Genre> newGenres)
    {
        try
        {
            newGenres.ForEach(genre => genre.CreatedOn = DateTime.Now);
            await _context.GenresCollection.InsertManyAsync(newGenres);
            return newGenres;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Genre> UpdateGenre(string id, Genre genre)
    {
        try
        {
            await _context.GenresCollection.ReplaceOneAsync(g => g.Id == id, genre);
            return genre;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Genre> GetGenre(string id) => await _context.GenresCollection.Find<Genre>(g => g.Id == id).FirstOrDefaultAsync();

    public async Task<List<Genre>> GetAllGenres() => await _context.GenresCollection.Find(_ => true).ToListAsync();
}