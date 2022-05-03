namespace Games.Context;

public interface IMongoContext
{
    IMongoClient client { get; }
    IMongoDatabase Database { get; }
    IMongoCollection<Genre> GenresCollection { get; }
    IMongoCollection<GameMode> GameModesCollection { get; }
    IMongoCollection<PlayerPerspective> PlayerPerspectivesCollection { get; }
    IMongoCollection<Franchise> FranchisesCollection { get; }
    IMongoCollection<Theme> ThemesCollection { get; }
    IMongoCollection<Company> CompaniesCollection { get; }
    IMongoCollection<Platform> PlatformsCollection { get; }
    IMongoCollection<Game> GamesCollection { get; }
    IMongoCollection<Product> ProductsCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly DatabaseSettings _settings;

    public IMongoClient client
    {
        get
        {
            return _client;
        }
    }

    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Genre> GenresCollection
    {
        get
        {
            return _database.GetCollection<Genre>(_settings.GenresCollection);
        }
    }

    public IMongoCollection<GameMode> GameModesCollection
    {
        get
        {
            return _database.GetCollection<GameMode>(_settings.GameModesCollection);
        }
    }

    public IMongoCollection<PlayerPerspective> PlayerPerspectivesCollection
    {
        get
        {
            return _database.GetCollection<PlayerPerspective>(_settings.PlayerPerspectivesCollection);
        }
    }

    public IMongoCollection<Franchise> FranchisesCollection
    {
        get
        {
            return _database.GetCollection<Franchise>(_settings.FranchisesCollection);
        }
    }

    public IMongoCollection<Theme> ThemesCollection
    {
        get
        {
            return _database.GetCollection<Theme>(_settings.ThemesCollection);
        }
    }

    public IMongoCollection<Company> CompaniesCollection
    {
        get
        {
            return _database.GetCollection<Company>(_settings.CompaniesCollection);
        }
    }

    public IMongoCollection<Platform> PlatformsCollection
    {
        get
        {
            return _database.GetCollection<Platform>(_settings.PlatformsCollection);
        }
    }

    public IMongoCollection<Game> GamesCollection
    {
        get
        {
            return _database.GetCollection<Game>(_settings.GamesCollection);
        }
    }

    public IMongoCollection<Product> ProductsCollection
    {
        get
        {
            return _database.GetCollection<Product>(_settings.ProductsCollection);
        }
    }
}