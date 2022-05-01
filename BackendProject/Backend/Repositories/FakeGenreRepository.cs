namespace Games.Repositories;

public class FakeGenreRepository : IGenreRepository
{
    public static List<Genre> _genres = new List<Genre>();
    public Task<List<Genre>> AddGenres(List<Genre> newGenres)
    {
        foreach (Genre g in newGenres)
        {
            g.Id = Guid.NewGuid().ToString();
            g.CreatedOn = DateTime.Now;
        }

        _genres.AddRange(newGenres);
        return Task.FromResult(newGenres);
    }

    public Task<List<Genre>> GetAllGenres() => Task.FromResult(_genres);

    public Task<Genre> GetGenre(string id) => Task.FromResult(_genres.Where(g => g.Id == id).Single());

    public Task<Genre> UpdateGenre(string id, Genre genre)
    {
        try
        {
            int i = _genres.FindIndex(g => g.Id == id);
            var item = _genres[i];
            if (item != null)
            {
                item.Name = genre.Name;
                _genres[i] = item;
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