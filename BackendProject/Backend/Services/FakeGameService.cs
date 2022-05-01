namespace Games.Services;

public class FakeGameService : IGameService
{
    private readonly IGenreRepository _fakeGenreRepository;
    private readonly IGameRepository _fakeGameRepository;
    private readonly IPlatformRepository _fakePlatformRepository;
    private readonly ICompanyRepository _fakeCompanyRepository;

    public FakeGameService(IGenreRepository genreRepository, IGameRepository gameRepository, IPlatformRepository platformRepository, ICompanyRepository companyRepository)
    {
        _fakeGenreRepository = genreRepository;
        _fakeGameRepository = gameRepository;
        _fakePlatformRepository = platformRepository;
        _fakeCompanyRepository = companyRepository;
    }

    public async Task<List<Company>> AddCompanies(List<Company> newCompanies) => await _fakeCompanyRepository.AddCompanies(newCompanies);

    public Task<List<Franchise>> AddFranchises(List<Franchise> newFranchises)
    {
        throw new NotImplementedException();
    }

    public async Task<Game> AddGame(Game newGame) => await _fakeGameRepository.AddGame(newGame);

    public Task<List<GameMode>> AddGameModes(List<GameMode> newGameModes)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Game>> AddGames(List<Game> newGames) => await _fakeGameRepository.AddGames(newGames);

    public async Task<List<Genre>> AddGenres(List<Genre> newGenres) => await _fakeGenreRepository.AddGenres(newGenres);

    public async Task<List<Platform>> AddPlatforms(List<Platform> newPlatforms) => await _fakePlatformRepository.AddPlatforms(newPlatforms);

    public Task<List<PlayerPerspective>> AddPlayerPerspectives(List<PlayerPerspective> newPlayerPerspectives)
    {
        throw new NotImplementedException();
    }

    public Task<List<Theme>> AddThemes(List<Theme> newThemes)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Company>> GetCompanies() => await _fakeCompanyRepository.GetAllCompanies();

    public async Task<Company> GetCompany(string id) => await _fakeCompanyRepository.GetCompany(id);

    public Task<Franchise> GetFranchise(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Franchise>> GetFranchises()
    {
        throw new NotImplementedException();
    }

    public async Task<Game> GetGame(string id) => await _fakeGameRepository.GetGame(id);

    public Task<GameMode> GetGameMode(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<GameMode>> GetGameModes()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Game>> GetGames() => await _fakeGameRepository.GetAllGames();

    public async Task<Genre> GetGenre(string id) => await _fakeGenreRepository.GetGenre(id);

    public async Task<List<Genre>> GetGenres() => await _fakeGenreRepository.GetAllGenres();

    public async Task<Platform> GetPlatform(string id) => await _fakePlatformRepository.GetPlatform(id);

    public async Task<List<Platform>> GetPlatforms() => await _fakePlatformRepository.GetAllPlatforms();

    public Task<PlayerPerspective> GetPlayerPerspective(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PlayerPerspective>> GetPlayerPerspectives()
    {
        throw new NotImplementedException();
    }

    public Task<Theme> GetTheme(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Theme>> GetThemes()
    {
        throw new NotImplementedException();
    }

    public async Task<Company> UpdateCompany(string id, Company company) => await _fakeCompanyRepository.UpdateCompany(id, company);

    public Task<Franchise> UpdateFranchise(string id, Franchise franchise)
    {
        throw new NotImplementedException();
    }

    public async Task<Game> UpdateGame(string id, Game game) => await _fakeGameRepository.UpdateGame(id, game);

    public Task<GameMode> UpdateGameMode(string id, GameMode gameMode)
    {
        throw new NotImplementedException();
    }

    public Task<Genre> UpdateGenre(string id, Genre genre) => _fakeGenreRepository.UpdateGenre(id, genre);

    public async Task<Platform> UpdatePlatform(string id, Platform platform) => await _fakePlatformRepository.UpdatePlatform(id, platform);

    public Task<PlayerPerspective> UpdatePlayerPerspective(string id, PlayerPerspective playerPerspective)
    {
        throw new NotImplementedException();
    }

    public Task<Theme> UpdateTheme(string id, Theme theme)
    {
        throw new NotImplementedException();
    }

    public async Task SetUpData()
    {
        if (!(await _fakeGenreRepository.GetAllGenres()).Any())
            await _fakeGenreRepository.AddGenres(new List<Genre>() { new Genre() { Name = "Shooter" }, { new Genre() { Name = "Platform" } }, { new Genre() { Name = "Puzzle" } }, { new Genre() { Name = "Racing" } }, { new Genre() { Name = "Real Time Strategy (RTS)" } }, { new Genre() { Name = "Role-playing (RPG)" } }, { new Genre() { Name = "Music" } }, { new Genre() { Name = "Simulator" } }, { new Genre() { Name = "Sport" } }, { new Genre() { Name = "Adventure" } } });

        if (!(await _fakeCompanyRepository.GetAllCompanies()).Any())
            await _fakeCompanyRepository.AddCompanies(new List<Company>() { new Company() { Name = "FromSoftware", Type = "developer" , Country = "Japan"}, new Company() { Name = "4A Games", Type = "developer" , Country = "Ukraine"}, new Company() { Name = "Rockstar Games", Type = "developer" , Country = "United States"}, new Company() { Name = "CD Projekt Red", Type = "developer" , Country = "Poland"}, new Company() { Name = "Ubisoft Montreal", Type = "developer" , Country = "Canada"}, new Company() { Name = "Bandai Namco Holdings", Type = "holding company", Country = "Japan"},
            new Company() { Name = "Bandai Namco Entertainment", Type = "publisher" , Country = "Japan"}, new Company() { Name = "Nordic Games Group", Type = "holding company" , Country = "Sweden"},new Company() { Name = "THQ Nordic", Type = "publisher" , Country = "Austria"}, new Company() { Name = "Koch Media", Type = "publisher" , Country = "Austria"} ,
            new Company() { Name = "Deep Silver", Type = "publisher" , Country = "Germany"},new Company() { Name = "Take-Two interactive", Type = "holding company", Country = "United States" }, new Company() { Name = "CD Projekt", Type = "publisher" , Country = "Poland"},new Company() { Name = "WB Games", Type = "publisher" , Country = "United States"}, new Company() { Name = "Ubisoft Entertainment", Type = "publisher" , Country = "France"}, new Company() { Name = "Microsoft", Type = "manufacturer" , Country = "United States"}, new Company() { Name = "Sony Computer Entertainment", Type = "manufacturer" , Country = "Japan"}, new Company() { Name = "Google", Type = "manufacturer" , Country = "United States"}, new Company() { Name = "Nintendo PTD", Type = "manufacturer" , Country = "Japan"} });

        if (!(await _fakePlatformRepository.GetAllPlatforms()).Any())
            await _fakePlatformRepository.AddPlatforms(new List<Platform>() { new Platform { Name = "Xbox One", ReleaseDate = 1385074800 },
            new Platform { Name = "Playstation 4", ReleaseDate = 1384470000 }, new Platform { Name = "Playstation 5", ReleaseDate = 1605135600 }, new Platform { Name = "PC" }, new Platform { Name = "Xbox Series X | S", ReleaseDate = 1604962800 }, new Platform { Name = "Google Stadia", ReleaseDate = 1574118000 },   new Platform { Name = "Nintendo Switch", ReleaseDate = 1488495600 } });
    }

    public async Task<List<Rating>> AddRating(string id, Rating rating)
    {
        var r = await _fakeGameRepository.AddRating(id, rating);

        var game = await _fakeGameRepository.GetGame(id);
        var dev = game.DeveloperId;
        var pub = game.PublisherId;

        await _fakeCompanyRepository.UpdateCompanyRating(dev, await CalculateRating(dev));
        await _fakeCompanyRepository.UpdateCompanyRating(pub, await CalculateRating(pub));

        return r;
    }

    public async Task<List<ReleaseDate>> AddReleaseDate(string id, ReleaseDate releaseDate) => await _fakeGameRepository.AddReleaseDate(id, releaseDate);

    public async Task<double> CalculateRating(string companyId)
    {
        List<Game> games = await _fakeGameRepository.GetAllGames();
        int count = 0;
        double totalScore = 0;

        int subCount = 0;
        int subTotal = 0;

        foreach (Game g in games)
        {
            if ((g.DeveloperId == companyId || g.PublisherId == companyId) && g.Ratings != null && g.Ratings.Any())
            {
                foreach (Rating r in g.Ratings)
                {
                    subTotal += r.Score;
                    subCount++;
                }

                totalScore += subTotal / subCount;
                count++;
            }
        }

        return Math.Round((totalScore / count / 10) * 100, 2);
    }

    public async Task<List<Rating>> RemoveRating(string gameId, string ratingId)
    {
        var r = await _fakeGameRepository.RemoveRating(gameId, ratingId);

        var game = await _fakeGameRepository.GetGame(gameId);
        var dev = game.DeveloperId;
        var pub = game.PublisherId;

        await _fakeCompanyRepository.UpdateCompanyRating(dev, await CalculateRating(dev));
        await _fakeCompanyRepository.UpdateCompanyRating(pub, await CalculateRating(pub));

        return r;
    }

    public async Task<List<ReleaseDate>> RemoveReleaseDate(string gameId, string releaseDateId) => await _fakeGameRepository.RemoveReleaseDate(gameId, releaseDateId);

    public async Task<List<Rating>> UpdateRating(string gameId, string ratingId, Rating rating)
    {
        var r = await _fakeGameRepository.UpdateRating(gameId, ratingId, rating);

        var game = await _fakeGameRepository.GetGame(gameId);
        var dev = game.DeveloperId;
        var pub = game.PublisherId;

        await _fakeCompanyRepository.UpdateCompanyRating(dev, await CalculateRating(dev));
        await _fakeCompanyRepository.UpdateCompanyRating(pub, await CalculateRating(pub));
        return r;
    }

    public async Task<List<ReleaseDate>> UpdateReleaseDate(string gameId, string releaseDateId, ReleaseDate releaseDate) => await _fakeGameRepository.UpdateReleaseDate(gameId, releaseDateId, releaseDate);

    public Task<List<Game>> GetGamesByCompany(string companyName)
    {
        throw new NotImplementedException();
    }

    public Task<List<Game>> GetGamesByGenre(string genreid)
    {
        throw new NotImplementedException();
    }

    public Task<List<Game>> GetGamesByFranchise(string franchiseId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Game>> GetGamesByGameMode(string gameModeId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Game>> GetGamesByPlatform(string platformId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Game>> GetGamesByPlayerPerspective(string playerPerspectiveId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Game>> GetGamesByTheme(string themeId)
    {
        throw new NotImplementedException();
    }
}