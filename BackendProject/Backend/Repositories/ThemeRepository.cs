namespace Games.Repositories;

public interface IThemeRepository
{
    Task<List<Theme>> AddThemes(List<Theme> newThemes);
    Task<List<Theme>> GetAllThemes();
    Task<Theme> GetTheme(string id);
    Task<Theme> UpdateTheme(string id, Theme theme);
}

public class ThemeRepository : IThemeRepository
{
    private readonly IMongoContext _context;

    public ThemeRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Theme>> AddThemes(List<Theme> newThemes)
    {
        try
        {
            newThemes.ForEach(theme => theme.CreatedOn = DateTime.Now);
            await _context.ThemesCollection.InsertManyAsync(newThemes);
            return newThemes;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Theme> UpdateTheme(string id, Theme theme)
    {
        try
        {
            await _context.ThemesCollection.ReplaceOneAsync(t => t.Id == id, theme);
            return theme;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Theme> GetTheme(string id) => await _context.ThemesCollection.Find<Theme>(t => t.Id == id).FirstOrDefaultAsync();

    public async Task<List<Theme>> GetAllThemes() => await _context.ThemesCollection.Find(_ => true).ToListAsync();
}