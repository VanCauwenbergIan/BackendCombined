namespace Games.Services;

public interface IGameService
{
    Task<List<Company>> AddCompanies(List<Company> newCompanies);
    Task<List<Franchise>> AddFranchises(List<Franchise> newFranchises);
    Task<Game> AddGame(Game newGame);
    Task<List<GameMode>> AddGameModes(List<GameMode> newGameModes);
    Task<List<Game>> AddGames(List<Game> newGames);
    Task<List<Genre>> AddGenres(List<Genre> newGenres);
    Task<List<Platform>> AddPlatforms(List<Platform> newPlatforms);
    Task<List<PlayerPerspective>> AddPlayerPerspectives(List<PlayerPerspective> newPlayerPerspectives);
    Task<List<Rating>> AddRating(string id, Rating rating);
    Task<List<ReleaseDate>> AddReleaseDate(string id, ReleaseDate releaseDate);
    Task<List<Theme>> AddThemes(List<Theme> newThemes);
    Task<double> CalculateRating(string companyId);
    Task<List<Company>> GetCompanies();
    Task<Company> GetCompany(string id);
    Task<Franchise> GetFranchise(string id);
    Task<List<Franchise>> GetFranchises();
    Task<Game> GetGame(string id);
    Task<GameMode> GetGameMode(string id);
    Task<List<GameMode>> GetGameModes();
    Task<List<Game>> GetGames();
    Task<List<Game>> GetGamesByCompany(string companyId);
    Task<List<Game>> GetGamesByFranchise(string franchiseId);
    Task<List<Game>> GetGamesByGameMode(string gameModeId);
    Task<List<Game>> GetGamesByGenre(string genreid);
    Task<List<Game>> GetGamesByPlatform(string platformId);
    Task<List<Game>> GetGamesByPlayerPerspective(string playerPerspectiveId);
    Task<List<Game>> GetGamesByTheme(string themeId);
    Task<Genre> GetGenre(string id);
    Task<List<Genre>> GetGenres();
    Task<Platform> GetPlatform(string id);
    Task<List<Platform>> GetPlatforms();
    Task<PlayerPerspective> GetPlayerPerspective(string id);
    Task<List<PlayerPerspective>> GetPlayerPerspectives();
    Task<Theme> GetTheme(string id);
    Task<List<Theme>> GetThemes();
    Task<List<Rating>> RemoveRating(string gameId, string ratingId);
    Task<List<ReleaseDate>> RemoveReleaseDate(string gameId, string releaseDateId);
    Task SetUpData();
    Task<Company> UpdateCompany(string id, Company company);
    Task<Franchise> UpdateFranchise(string id, Franchise franchise);
    Task<Game> UpdateGame(string id, Game game);
    Task<GameMode> UpdateGameMode(string id, GameMode gameMode);
    Task<Genre> UpdateGenre(string id, Genre genre);
    Task<Platform> UpdatePlatform(string id, Platform platform);
    Task<PlayerPerspective> UpdatePlayerPerspective(string id, PlayerPerspective playerPerspective);
    Task<List<Rating>> UpdateRating(string gameId, string ratingId, Rating rating);
    Task<List<ReleaseDate>> UpdateReleaseDate(string gameId, string releaseDateId, ReleaseDate releaseDate);
    Task<Theme> UpdateTheme(string id, Theme theme);
}

public class GameService : IGameService
{
    private readonly IGenreRepository _genreRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IFranchiseRepository _franchiseRepository;
    private readonly IGameModeRepository _gameModeRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IPlatformRepository _platformRepository;
    private readonly IThemeRepository _themeRepository;
    private readonly IPlayerPerspectiveRepository _playerPerspectiveRepository;

    public GameService(IGenreRepository genreRepository, ICompanyRepository companyRepository, IFranchiseRepository franchiseRepository, IGameModeRepository gameModeRepository, IGameRepository gameRepository, IPlatformRepository platformRepository, IThemeRepository themeRepository, IPlayerPerspectiveRepository playerPerspectiveRepository)
    {
        _genreRepository = genreRepository;
        _companyRepository = companyRepository;
        _franchiseRepository = franchiseRepository;
        _gameModeRepository = gameModeRepository;
        _gameRepository = gameRepository;
        _platformRepository = platformRepository;
        _themeRepository = themeRepository;
        _playerPerspectiveRepository = playerPerspectiveRepository;
    }

    #region GET--ALL

    public async Task<List<Genre>> GetGenres() => await _genreRepository.GetAllGenres();

    public async Task<List<Company>> GetCompanies() => await _companyRepository.GetAllCompanies();

    public async Task<List<Franchise>> GetFranchises() => await _franchiseRepository.GetAllFranchises();

    public async Task<List<GameMode>> GetGameModes() => await _gameModeRepository.GetAllGameModes();

    public async Task<List<Game>> GetGames() => await _gameRepository.GetAllGames();

    public async Task<List<Platform>> GetPlatforms() => await _platformRepository.GetAllPlatforms();

    public async Task<List<Theme>> GetThemes() => await _themeRepository.GetAllThemes();

    public async Task<List<PlayerPerspective>> GetPlayerPerspectives() => await _playerPerspectiveRepository.GetAllPlayerPerspectives();

    #endregion

    #region GET--SINGLE

    public async Task<Genre> GetGenre(string id) => await _genreRepository.GetGenre(id);

    public async Task<Company> GetCompany(string id) => await _companyRepository.GetCompany(id);

    public async Task<Franchise> GetFranchise(string id) => await _franchiseRepository.GetFranchise(id);

    public async Task<GameMode> GetGameMode(string id) => await _gameModeRepository.GetGameMode(id);

    public async Task<Game> GetGame(string id) => await _gameRepository.GetGame(id);

    public async Task<Platform> GetPlatform(string id) => await _platformRepository.GetPlatform(id);

    public async Task<Theme> GetTheme(string id) => await _themeRepository.GetTheme(id);

    public async Task<PlayerPerspective> GetPlayerPerspective(string id) => await _playerPerspectiveRepository.GetPlayerPerspective(id);

    #endregion

    #region PUT

    public async Task<Game> UpdateGame(string id, Game game) => await _gameRepository.UpdateGame(id, game);

    public async Task<Genre> UpdateGenre(string id, Genre genre) => await _genreRepository.UpdateGenre(id, genre);

    public async Task<Company> UpdateCompany(string id, Company company) => await _companyRepository.UpdateCompany(id, company);

    public async Task<Franchise> UpdateFranchise(string id, Franchise franchise) => await _franchiseRepository.UpdateFranchise(id, franchise);

    public async Task<GameMode> UpdateGameMode(string id, GameMode gameMode) => await _gameModeRepository.UpdateGameMode(id, gameMode);

    public async Task<PlayerPerspective> UpdatePlayerPerspective(string id, PlayerPerspective playerPerspective) => await _playerPerspectiveRepository.UpdatePlayerPerspective(id, playerPerspective);

    public async Task<Theme> UpdateTheme(string id, Theme theme) => await _themeRepository.UpdateTheme(id, theme);

    public async Task<Platform> UpdatePlatform(string id, Platform platform) => await _platformRepository.UpdatePlatform(id, platform);

    public async Task<List<Rating>> UpdateRating(string gameId, string ratingId, Rating rating)
    {
        var r = await _gameRepository.UpdateRating(gameId, ratingId, rating);

        var game = await _gameRepository.GetGame(gameId);
        var dev = game.DeveloperId;
        var pub = game.PublisherId;

        await _companyRepository.UpdateCompanyRating(dev, await CalculateRating(dev));
        await _companyRepository.UpdateCompanyRating(pub, await CalculateRating(pub));
        return r;
    }

    public async Task<double> CalculateRating(string companyId)
    {
        List<Game> games = await _gameRepository.GetAllGames();
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

    public async Task<List<ReleaseDate>> UpdateReleaseDate(string gameId, string releaseDateId, ReleaseDate releaseDate) => await _gameRepository.UpdateReleaseDate(gameId, releaseDateId, releaseDate);

    #endregion

    #region POST

    public async Task<List<Genre>> AddGenres(List<Genre> newGenres) => await _genreRepository.AddGenres(newGenres);

    public async Task<List<Company>> AddCompanies(List<Company> newCompanies) => await _companyRepository.AddCompanies(newCompanies);

    public async Task<List<Franchise>> AddFranchises(List<Franchise> newFranchises) => await _franchiseRepository.AddFranchises(newFranchises);

    public async Task<List<GameMode>> AddGameModes(List<GameMode> newGameModes) => await _gameModeRepository.AddGameModes(newGameModes);

    public async Task<List<Game>> AddGames(List<Game> newGames) => await _gameRepository.AddGames(newGames);

    public async Task<List<Platform>> AddPlatforms(List<Platform> newPlatforms) => await _platformRepository.AddPlatforms(newPlatforms);

    public async Task<List<Theme>> AddThemes(List<Theme> newThemes) => await _themeRepository.AddThemes(newThemes);

    public async Task<List<PlayerPerspective>> AddPlayerPerspectives(List<PlayerPerspective> newPlayerPerspectives) => await _playerPerspectiveRepository.AddPlayerPerspectives(newPlayerPerspectives);

    public async Task<Game> AddGame(Game newGame) => await _gameRepository.AddGame(newGame);

    public async Task<List<Rating>> AddRating(string id, Rating rating)
    {
        var r = await _gameRepository.AddRating(id, rating);

        var game = await _gameRepository.GetGame(id);
        var dev = game.DeveloperId;
        var pub = game.PublisherId;

        await _companyRepository.UpdateCompanyRating(dev, await CalculateRating(dev));
        await _companyRepository.UpdateCompanyRating(pub, await CalculateRating(pub));

        return r;
    }

    public async Task<List<ReleaseDate>> AddReleaseDate(string id, ReleaseDate releaseDate) => await _gameRepository.AddReleaseDate(id, releaseDate);

    #endregion

    #region DELETE

    public async Task<List<Rating>> RemoveRating(string gameId, string ratingId)
    {

        var r = await _gameRepository.RemoveRating(gameId, ratingId);

        var game = await _gameRepository.GetGame(gameId);
        var dev = game.DeveloperId;
        var pub = game.PublisherId;

        await _companyRepository.UpdateCompanyRating(dev, await CalculateRating(dev));
        await _companyRepository.UpdateCompanyRating(pub, await CalculateRating(pub));

        return r;
    }

    public async Task<List<ReleaseDate>> RemoveReleaseDate(string gameId, string releaseDateId) => await _gameRepository.RemoveReleaseDate(gameId, releaseDateId);

    #endregion

    #region FILTERED-GET

    public async Task<List<Game>> GetGamesByCompany(string companyId)
    {
        Company company = await _companyRepository.GetCompany(companyId);

        List<string> toCheckForIds = new List<string>() { companyId };

        if (!company.SubcompanyIds.IsNullOrEmpty())
        {
            List<string> subCompanyIds = company.SubcompanyIds;

            while (!subCompanyIds.IsNullOrEmpty())
            {
                List<string> subCompanyIds2 = new List<string>();

                foreach (string id in subCompanyIds)
                {
                    Company c = await _companyRepository.GetCompany(id);
                    toCheckForIds.Add(c.Id);

                    if (!c.SubcompanyIds.IsNullOrEmpty())
                    {
                        subCompanyIds2.AddRange(c.SubcompanyIds);
                    }
                }

                subCompanyIds = subCompanyIds2;
            }
        }

        List<Game> games = await _gameRepository.GetAllGames();
        List<Game> results = games.Where(g => toCheckForIds.Contains(g.DeveloperId) || toCheckForIds.Contains(g.PublisherId)).ToList();

        return results;
    }

    public async Task<List<Game>> GetGamesByGenre(string genreid)
    {
        List<Game> games = await _gameRepository.GetAllGames();
        List<Game> result = new List<Game>();

        foreach (Game g in games)
        {
            if (!g.GenreIds.IsNullOrEmpty() && g.GenreIds.Contains(genreid))
                result.Add(g);
        }

        return result;
    }

    public async Task<List<Game>> GetGamesByFranchise(string franchiseId)
    {
        List<Game> games = await _gameRepository.GetAllGames();
        List<Game> result = new List<Game>();

        foreach (Game g in games)
        {
            if (g.FranchiseId != null && g.FranchiseId == franchiseId)
                result.Add(g);
        }

        return result;
    }

    public async Task<List<Game>> GetGamesByGameMode(string gameModeId)
    {
        List<Game> games = await _gameRepository.GetAllGames();
        List<Game> result = new List<Game>();

        foreach (Game g in games)
        {
            if (!g.GameModeIds.IsNullOrEmpty() && g.GameModeIds.Contains(gameModeId))
                result.Add(g);
        }

        return result;
    }

    public async Task<List<Game>> GetGamesByPlayerPerspective(string playerPerspectiveId)
    {
        List<Game> games = await _gameRepository.GetAllGames();
        List<Game> result = new List<Game>();

        foreach (Game g in games)
        {
            if (!g.PlayerPerspectiveIds.IsNullOrEmpty() && g.PlayerPerspectiveIds.Contains(playerPerspectiveId))
                result.Add(g);
        }

        return result;
    }

    public async Task<List<Game>> GetGamesByTheme(string themeId)
    {
        List<Game> games = await _gameRepository.GetAllGames();
        List<Game> result = new List<Game>();

        foreach (Game g in games)
        {
            if (!g.ThemeIds.IsNullOrEmpty() && g.ThemeIds.Contains(themeId))
                result.Add(g);
        }

        return result;
    }

    public async Task<List<Game>> GetGamesByPlatform(string platformId)
    {
        List<Game> games = await _gameRepository.GetAllGames();
        List<Game> result = new List<Game>();

        foreach (Game g in games)
        {
            if (!g.PlatformIds.IsNullOrEmpty() && g.PlatformIds.Contains(platformId))
                result.Add(g);
        }

        return result;
    }

    #endregion

    #region SETUP

    public async Task SetUpData()
    {
        try
        {
            // never use setupdata all at once, wrong Ids for games will cause errors with mapping! 
            if (!(await _genreRepository.GetAllGenres()).Any())
                await _genreRepository.AddGenres(new List<Genre>() { new Genre() { Name = "Fighting" }, { new Genre() { Name = "Shooter" } }, { new Genre() { Name = "Platform" } }, { new Genre() { Name = "Puzzle" } }, { new Genre() { Name = "Racing" } }, { new Genre() { Name = "Real Time Strategy (RTS)" } }, { new Genre() { Name = "Role-playing (RPG)" } }, { new Genre() { Name = "Music" } } ,
                { new Genre() { Name = "Simulator" } }, { new Genre() { Name = "Sport" } }, { new Genre() { Name = "Adventure" } }});

            if (!(await _companyRepository.GetAllCompanies()).Any())
                await _companyRepository.AddCompanies(new List<Company>() { new Company() { Name = "FromSoftware", Type = "developer" , Country = "Japan"}, new Company() { Name = "4A Games", Type = "developer" , Country = "Ukraine"}, new Company() { Name = "Rockstar Games", Type = "developer" , Country = "United States"}, new Company() { Name = "CD Projekt Red", Type = "developer" , Country = "Poland"}, new Company() { Name = "Ubisoft Montreal", Type = "developer" , Country = "Canada"}, new Company() { Name = "Bandai Namco Holdings", Type = "holding company", Country = "Japan"},
                new Company() { Name = "Bandai Namco Entertainment", Type = "publisher" , Country = "Japan"}, new Company() { Name = "Nordic Games Group", Type = "holding company" , Country = "Sweden"},new Company() { Name = "THQ Nordic", Type = "publisher" , Country = "Austria"}, new Company() { Name = "Koch Media", Type = "publisher" , Country = "Austria"} ,
                new Company() { Name = "Deep Silver", Type = "publisher" , Country = "Germany"},new Company() { Name = "Take-Two interactive", Type = "holding company", Country = "United States" }, new Company() { Name = "CD Projekt", Type = "publisher" , Country = "Poland"},new Company() { Name = "WB Games", Type = "publisher" , Country = "United States"}, new Company() { Name = "Ubisoft Entertainment", Type = "publisher" , Country = "France"}, new Company() { Name = "Microsoft", Type = "manufacturer" , Country = "United States"}, new Company() { Name = "Sony Computer Entertainment", Type = "manufacturer" , Country = "Japan"}, new Company() { Name = "Google", Type = "manufacturer" , Country = "United States"}, new Company() { Name = "Nintendo PTD", Type = "manufacturer" , Country = "Japan"} });

            if (!(await _franchiseRepository.GetAllFranchises()).Any())
                await _franchiseRepository.AddFranchises(new List<Franchise>() { new Franchise { Name = "Metro" }, new Franchise { Name = "Red Dead" }, new Franchise { Name = "Assassins's Creed" }, new Franchise { Name = "The Witcher" } });

            if (!(await _gameModeRepository.GetAllGameModes()).Any())
                await _gameModeRepository.AddGameModes(new List<GameMode>() { new GameMode { Name = "Single player" }, new GameMode { Name = "Multiplayer" }, new GameMode { Name = "Co-operative" }, new GameMode { Name = "Split screen" }, new GameMode { Name = "Massive Multiplayer Online (MMO)" }, new GameMode { Name = "Battle Royale" } });

            if (!(await _playerPerspectiveRepository.GetAllPlayerPerspectives()).Any())
                await _playerPerspectiveRepository.AddPlayerPerspectives(new List<PlayerPerspective>() { new PlayerPerspective { Name = "First person" }, new PlayerPerspective { Name = "Third person" }, new PlayerPerspective { Name = "Bird view / Isometric" }, new PlayerPerspective { Name = "Text" }, new PlayerPerspective { Name = "Side View" }, new PlayerPerspective { Name = "Virtual Reality" }, new PlayerPerspective { Name = "Auditory" } });

            if (!(await _themeRepository.GetAllThemes()).Any())
                await _themeRepository.AddThemes(new List<Theme>() { new Theme { Name = "Action" }, new Theme { Name = "Fantasy" }, new Theme { Name = "Mystery" }, new Theme { Name = "Open world" }, new Theme { Name = "Drama" }, new Theme { Name = "Horror" }, new Theme { Name = "Survival" }, new Theme { Name = "Science fiction" }, new Theme { Name = "Historical" }, new Theme { Name = "Stealth" } });

            if (!(await _platformRepository.GetAllPlatforms()).Any())
                await _platformRepository.AddPlatforms(new List<Platform>() { new Platform { Name = "Xbox One", ReleaseDate = 1385074800 },
                new Platform { Name = "Playstation 4",  ReleaseDate = 1384470000 }, new Platform { Name = "Playstation 5", ReleaseDate = 1605135600 }, new Platform { Name = "PC" }, new Platform { Name = "Xbox Series X | S", ReleaseDate = 1604962800 }, new Platform { Name = "Google Stadia",  ReleaseDate = 1574118000 },   new Platform { Name = "Nintendo Switch", ReleaseDate = 1488495600 } });

            if (!(await _gameRepository.GetAllGames()).Any())
                await _gameRepository.AddGames(new List<Game>() {
                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "some_dumbass420", Score = 10, Comment = "single handedly cured my entire family of cancer", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now},
                    new Rating {Author = "some_dumbass420", Score = 5, Comment = "nevermind, found a single bug",Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co4jni.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1645743600 , PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33",  "625f3b63dd256f5e40cabb34" , "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb36" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}}, GameModeIds = new List<string>() {"625f31236e495f62b556f1e2","625f31236e495f62b556f1e1","625f31236e495f62b556f1e0"} , PlayerPerspectiveIds = new List<string>() {"625f334581586edfafce4f9e" }, DeveloperId = "625f3a0a23345ba753604f90", PublisherId = "625f3a0a23345ba753604f96", GenreIds = new List<string>() {"625f35ca6d476218346a3dfc" }, ThemeIds = new List<string>() {"625f3a0a23345ba753604fa3","625f3a0a23345ba753604fa4","625f3a0a23345ba753604fa5","625f3a0a23345ba753604fa6"}, PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb32","625f3b63dd256f5e40cabb33","625f3b63dd256f5e40cabb34","625f3b63dd256f5e40cabb35","625f3b63dd256f5e40cabb36"}, Name = "Elden Ring" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "tennisbeforesunset", Score = 9, Comment = "Whether you have played the original or not, this is a game for anyone seeking atmosphere, story, and FPS gameplay blended into something amazing. Do not miss this experience.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1rd6.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1268694000 , PlatformIds = new List<string>() {"6264261c855881e755a5e273", "625f3b63dd256f5e40cabb35"}, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1409004000, PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1582758000, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb38" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1592863200, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb37" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0"} , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9d" }, DeveloperId = "625f3a0a23345ba753604f91", PublisherId = "625f3a0a23345ba753604f98", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "625f35ca6d476218346a3df7", "625f35ca6d476218346a3dfc" }, ThemeIds = new List<string>() {"625f3a0a23345ba753604fa3" , "625f3a0a23345ba753604fa8" , "625f3a0a23345ba753604faa" , "625f3a0a23345ba753604fa9", "625f3a0a23345ba753604fac", "62642a0fac1d976dca0231f8", "62642a0fac1d976dca0231f9"}, PlatformIds = new List<string>() {"6264261c855881e755a5e273", "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33",  "625f3b63dd256f5e40cabb38" , "625f3b63dd256f5e40cabb37" }, Name = "Metro 2033", FranchiseId = "625f2f636f10b8f02d5f97db" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "yogurt45", Score = 7, Comment = "Showing it's age now, but a really well made game with a brilliant premise and tight mechanics. It does almost all take place underground and drives home the apocolyptic theme, but don't let that put you off as its so well done.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "jokerSlonker", Score = 9, Comment = "This is incredible surprise. I expected nothing of the game, because I've never played the previous part and have never read any books from this setting, and I came out of it knowing that I now need to play the first part and probably read some books after.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "weliveinasociety", Score = 9, Comment = "This game is something that S.T.A.L.K.E.R. needed to be but failed at. Full of atmosphere, story-driven with impeccable gameplay. The setting of post-apocalyptic Russia is pitch-perfect, the surroundings are immersive, they show a grim picture of the world gone wrong.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co2h9d.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1368482400 , PlatformIds = new List<string>() {"6264261c855881e755a5e273" , "625f3b63dd256f5e40cabb35" , "62642d57ac1d976dca0231fa" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1409004000, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1582758000, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb38" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1592863200, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb37" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0"} , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9d" }, DeveloperId = "625f3a0a23345ba753604f91", PublisherId = "625f3a0a23345ba753604f9a", GenreIds = new List<string>() {"625f35ca6d476218346a3df7"}, ThemeIds = new List<string>() {"625f3a0a23345ba753604fa3" , "625f3a0a23345ba753604fa8" , "625f3a0a23345ba753604faa" , "625f3a0a23345ba753604fa9", "625f3a0a23345ba753604fac", "625f3a0a23345ba753604fab" }, PlatformIds = new List<string>() {"6264261c855881e755a5e273", "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33",  "625f3b63dd256f5e40cabb38" , "625f3b63dd256f5e40cabb37", "62642d57ac1d976dca0231fa" }, Name = "Metro: Last Light", FranchiseId = "625f2f636f10b8f02d5f97db" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "toastsandwich", Score = 9, Comment = "Play Metro 2033 and Last Light before you play this, the story, gameplay and graphics are all amazing. One of the best story based shooters next to the Bioshock series.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now},
                    new Rating {Author = "halo_guy", Score = 8, Comment = "There is no any apocalypse-survival-open world game like this and graphics are insane.",Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1iuj.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1550185200 , PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb37" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1626559200, PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb34" , "625f3b63dd256f5e40cabb36" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0"} , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9d" }, DeveloperId = "625f3a0a23345ba753604f91", PublisherId = "625f3a0a23345ba753604f9a", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "625f35ca6d476218346a3df7" }, ThemeIds = new List<string>() {"625f3a0a23345ba753604fa3" , "625f3a0a23345ba753604fa8" , "625f3a0a23345ba753604faa" , "625f3a0a23345ba753604fa9" }, PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb37" , "625f3b63dd256f5e40cabb34", "625f3b63dd256f5e40cabb36" }, Name = "Metro Exodus", FranchiseId = "625f2f636f10b8f02d5f97db" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "rice_men", Score = 5, Comment = "Read dead revolver can only be classed as average. The gameplay is boring and repetitive, and whilst it tries to do alot every time it tried something new it only annoyed me, hand to hand combat was poorly implemented and therefore should not be mandatory. Stealth is not a gameplay mechanics and therefore should not have a level built around it. The game let's you play as multiple characters once and then never again. The game seems to be ambitious but never meets it's ambitions. The final problems is difficulty. Difficulty rose and fell dramatically in the game which made the spikes a lot more frustrating after a set of brainless missions. On the positive side, atmosphere and dialogue are both great.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co2lcn.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1078268400 , PlatformIds = new List<string>() {"6264399b89678f4b841fc598" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1078354800, PlatformIds = new List<string>() { "626439ec89678f4b841fc599" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1318284000, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb33" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() { "625f31236e495f62b556f1e1", "625f31236e495f62b556f1e0" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e" }, DeveloperId = "6264387789678f4b841fc597", PublisherId = "625f3a0a23345ba753604f9b", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "625f35ca6d476218346a3df7" }, ThemeIds = new List<string>() {"625f3a0a23345ba753604fa3" , "625f3a0a23345ba753604fab" , "62643c6e89678f4b841fc59a" }, PlatformIds = new List<string>() {"6264399b89678f4b841fc598", "626439ec89678f4b841fc599", "625f3b63dd256f5e40cabb33" }, Name = "Red Dead Revolver", FranchiseId = "625f2f636f10b8f02d5f97dc" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "rice_men", Score = 6, Comment = "Although it can look pretty at times, and the sound, music and voice acting were good. The gameplay was boring. Both side quests and story beats felt like a chore with only a few being worth doing. Luckily the game picked up slightly by the end. I can't recommend the game unless you are really interested.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "grantorino", Score = 9, Comment = "I thought I bought the game, but in the end it turned out that I bought a ticket to the wild west.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co2lcv.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1268866800 , PlatformIds = new List<string>() {"62642d57ac1d976dca0231fa", "6264261c855881e755a5e273" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}}, GameModeIds = new List<string>() { "625f31236e495f62b556f1e1", "625f31236e495f62b556f1e0" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e" }, DeveloperId = "62643f5789678f4b841fc59b", PublisherId = "625f3a0a23345ba753604f92", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "625f35ca6d476218346a3df7" }, ThemeIds = new List<string>() {"625f3a0a23345ba753604fa3", "625f3a0a23345ba753604fa6", "62643c6e89678f4b841fc59a" }, PlatformIds = new List<string>() {"62642d57ac1d976dca0231fa", "6264261c855881e755a5e273" }, Name = "Red Dead Redemption", FranchiseId = "625f2f636f10b8f02d5f97dc" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "pianistxx20", Score = 9, Comment = "This is one of my favorite games of all time! I've spent hours playing this game ever since I've got it. It hooks you In the first time you play it. There is always something to do, whether you are playing story mode, or online. The only negative thing I have to say about the game is the fan base. There has been a good amount of times where I was playing online and I got killed for no reason. I guess some people think it's fun to mess with other players online, but I don't. I'm not saying this is the dev's fault at all, in fact, they tried to help with this issue. You can choose to spawn away from them once they kill you, problem fixed! This game has an amazing storyline, and visuals to accompany it, you should definitely give it a try!", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now},
                    new Rating {Author = "rice_men", Score = 8, Comment = "it´s a really vast game, top of the generation, great story, but the remains of old mission design make it feel a little weird next to all of this innovation",Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "arom", Score = 10, Comment = "The Greatest game I played ever, Great Story,Great gameplay, Everything about this game is Great!" ,Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now},
                    new Rating {Author = "abcow", Score = 7, Comment = "Too many stability issues at launch on PC, but it runs great now." ,Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1q1f.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1540504800 , PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1572908400, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1574118000, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb37" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e2", "625f31236e495f62b556f1e1", "625f31236e495f62b556f1e0" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9d", "625f334581586edfafce4f9e" }, DeveloperId = "625f3a0a23345ba753604f92", PublisherId = "625f3a0a23345ba753604f9b", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "625f35ca6d476218346a3df7", "625f35ca6d476218346a3dfc" }, ThemeIds = new List<string>() {"625f3a0a23345ba753604fa3" , "625f3a0a23345ba753604fa6" , "625f3a0a23345ba753604fa7" }, PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb37" }, Name = "Red Dead Redemption 2", FranchiseId = "625f2f636f10b8f02d5f97dc" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "dwarfsupremacy201", Score = 10, Comment = "For me, the whole game is great. Sapkowski has created a great world in books and the game series The Witcher is a great way to enter this world.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1xrx.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1193349600 , PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now} }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e", "625f334581586edfafce4f9f" }, DeveloperId = "625f3a0a23345ba753604f93", PublisherId = "62645e2e89678f4b841fc59c", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "625f35ca6d476218346a3dfc" }, ThemeIds = new List<string>() {"625f3a0a23345ba753604fa3", "625f3a0a23345ba753604fa4" }, PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb35"}, Name = "The Witcher", FranchiseId = "625f2f636f10b8f02d5f97de" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "dwarfsupremacy201", Score = 9, Comment = "For me, the whole game is great. Sapkowski has created a great world in books and the game series The Witcher is a great way to enter this world.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now},
                    new Rating {Author = "tomm1236", Score = 10, Comment = "Pretty good game to develop the lore of the witcher universe and the politics....a huge upgrade to the first game.",Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1wy4.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1305583200 , PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1334613600, PlatformIds = new List<string>() { "6264261c855881e755a5e273" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e" }, DeveloperId = "625f3a0a23345ba753604f93", PublisherId = "625f3a0a23345ba753604f93", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "625f35ca6d476218346a3dfc" }, ThemeIds = new List<string>() {"625f3a0a23345ba753604fa3", "625f3a0a23345ba753604fa4" }, PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb35", "6264261c855881e755a5e273" }, Name = "The Witcher 2: Assassins of Kings", FranchiseId = "625f2f636f10b8f02d5f97de" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "dwarfsupremacy201", Score = 10, Comment = "Absolutely great completion of the game series The Witcher. Together with the dlc, it opens up a huge world where almost everything goes, and you will meet many characters from the books.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now},
                    new Rating {Author = "rice_men", Score = 10, Comment = "it´s a really vast game, top of the generation, great story, but the remains of old mission design make it feel a little weird next to all of this innovation",Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "bac0n_", Score = 10, Comment = "If there is one game I would recommend, it would be this one. Since the beginning of the story in White Orchard to the lush fields of Toussaint found in the DLC, this game has changed me. I would highly recommend this game to those who enjoy playing a game for the story and taking the time to immerse yourself in this world created by CD Project Red." ,Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now},
                    new Rating {Author = "asparathefada", Score = 7, Comment = "I ended up uninstalling the game, it is so frustrating and doesn't hold the comparison against the Elder Scrolls franchise or the Assassin Creed one." ,Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1wyy.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1431986400 , PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1611788400, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb38" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e" }, DeveloperId = "625f3a0a23345ba753604f93", PublisherId = "625f3a0a23345ba753604f9d", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "625f35ca6d476218346a3dfc" }, ThemeIds = new List<string>() {"625f3a0a23345ba753604fa3", "625f3a0a23345ba753604fa4" , "625f3a0a23345ba753604fa6" }, PlatformIds = new List<string>() {"625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb38" }, Name = "The Witcher 3: Wild Hunt", FranchiseId = "625f2f636f10b8f02d5f97de" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "grapefruitghost", Score = 6, Comment = "Assassins creed can be defined by the word tedious, but there is still quite a bit to like in it's repetitive gameplay loop. Before each assassination you will find yourself doing same repetitive tasks, just to get a few lines of dialogue after an more interesting assassination mission. There are redeeming qualities though, setting is really well build for it's time, those few lines are usually interesting and bring the puzzle together bit by bit, ending is worth the struggle, it's one of those where journey is though to sit through, but you will be rewarded.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "spookyceres", Score = 8, Comment = "This game in short is a flawed, but excellent experience. While those may seem contradictory, let me break it down for you. First, it is difficult to get running properly on a modern gaming rig, simply because it was designed for technology that wasn't available almost 15 years ago. There are glitches, not many, and nothing 'game-breaking', but a few annoying ones. The game suffers greatly from monotonous plot progression elements, basically go to city, go to district, do some stuff, kill a guy, rinse and repeat many, many times. The collectables are not interesting and provide absolutely 0 benefit in doing them, and what's more, are very easy to lose place on (which is what I did). All that being said, the good in this game saves what could have been a mediocre game, into one that is memorable, definitely worth playing, and is the reason why a massively successful game series was launched from this title. First, the gameplay mechanics are incredibly detailed and well thought out. This is most evident for me in the combat system. I have played this game roughly 5 times beginning to end since the release, and I am STILL finding out different ways you can respond to enemies movements and to turn the battle in your favor. The combat in general is just very fluid and so much fun. I almost didn't mind the repetitive 'Save the Citizen' mini-games, because I got to have a cool sword fight. The plot, in my opinion, is one of the best present in any media franchise, movie, tv, game, or otherwise. It is very gripping, very unique, and very well told (though I wish there were subtitles). The graphics have also held up beautifully, especially the environmental ones. All in all, an amazing game if you are willing to work through some kinks.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "grapefruitghost", Score = 4, Comment = "I seriously don't understand how anyone can recommend this game! I understand that it was the first in a phenomenal series and was the first to introduce the assassin gameplay that we now know, love and cherish, but this game is just dreadful!", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1rrw.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1194908400 , PlatformIds = new List<string>() {"6264261c855881e755a5e273", "62642d57ac1d976dca0231fa" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1207605600, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e"}, DeveloperId = "625f3a0a23345ba753604f94", PublisherId = "625f3a0a23345ba753604f9e", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "625f35ca6d476218346a3df8" }, ThemeIds = new List<string>() {"625f35ca6d476218346a3e00", "625f3a0a23345ba753604fa4", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fa6", "62643c6e89678f4b841fc59a", "625f3a0a23345ba753604faa", "625f3a0a23345ba753604fac" }, PlatformIds = new List<string>() {"6264261c855881e755a5e273", "62642d57ac1d976dca0231fa", "625f3b63dd256f5e40cabb35" }, Name = "Assassin's Creed", FranchiseId = "625f2f636f10b8f02d5f97dd" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "wordsalad", Score = 10, Comment = "The best Assassin's Creed I played ever, Ezio, I am in a love with this game", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "spookyceres", Score = 9, Comment = "This game improves on literally every flaw of the original. Any complaints I had about the original (monotonous, collectible hunting had no real purpose, etc.) was remedied in the sequel. The plot is very engaging and well written and performed considering the age of this game. Speaking of which, this game's visuals have held up spectacularly considering it is 12 years old at the time of writing this review. The collectibles have value and either contribute to the plot, or provide benefit to you, as opposed to mindless flag-hunting. All in all, a must-play for anyone, and I mean anyone, because everyone should play this.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1rcf.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1258412400 , PlatformIds = new List<string>() {"6264261c855881e755a5e273", "62642d57ac1d976dca0231fa" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1268089200, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e"}, DeveloperId = "625f3a0a23345ba753604f94", PublisherId = "625f3a0a23345ba753604f9e", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "625f35ca6d476218346a3dfc" }, ThemeIds = new List<string>() {"625f35ca6d476218346a3e00", "625f3a0a23345ba753604fa4", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fa6", "62643c6e89678f4b841fc59a", "625f3a0a23345ba753604faa", "625f3a0a23345ba753604fac" }, PlatformIds = new List<string>() {"6264261c855881e755a5e273", "62642d57ac1d976dca0231fa", "625f3b63dd256f5e40cabb35" }, Name = "Assassin's Creed II", FranchiseId = "625f2f636f10b8f02d5f97dd" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "spookyceres", Score = 9, Comment = "This game takes an already near-perfect formula introduced in Assassin's Creed II, and adds onto it brilliantly. If the last entry introduced the idea of meaningful collectibles, this one takes that idea and doubles it. The story is amazing, the gameplay even better, you just really can't go wrong here.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1mxz.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1289862000 , PlatformIds = new List<string>() {"6264261c855881e755a5e273", "62642d57ac1d976dca0231fa" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1300316400, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0","625f31236e495f62b556f1e1" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e"}, DeveloperId = "625f3a0a23345ba753604f94", PublisherId = "625f3a0a23345ba753604f9e", GenreIds = new List<string>() {"625f35ca6d476218346a3e00"}, ThemeIds = new List<string>() {"625f35ca6d476218346a3e00", "625f3a0a23345ba753604fa4", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fa6", "62643c6e89678f4b841fc59a", "625f3a0a23345ba753604faa", "625f3a0a23345ba753604fac" }, PlatformIds = new List<string>() {"6264261c855881e755a5e273", "62642d57ac1d976dca0231fa", "625f3b63dd256f5e40cabb35" }, Name = "Assassin's Creed: Brotherhood", FranchiseId = "625f2f636f10b8f02d5f97dd" },

                new Game {Ratings = new List<Rating>() {
                    new Rating {Author = "spookyceres", Score = 8, Comment = "This game takes many steps forward, but also several steps back for the franchise. Most notably, in the sheer amount of bugs. In my playthrough there, were several missions I had to restart because a bug blocked my progression. The story and gameplay are top notch, and deserving of an exceptional but the amount of bugs really hurts the game.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now},                     new Rating {Author = "the_gladiator", Score = 8, Comment = "If you are already invested in the series then playing Revelations isn't even an option - it's mandatory. The story is just too important, and frankly, this is the best in the series when it comes to story and action sequences, and not even misplaced tower defense distraction and endless city domination missions can hold it back.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                }, CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1xih.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1321225200 , PlatformIds = new List<string>() { "62642d57ac1d976dca0231fa" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1321311600, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb35", "6264261c855881e755a5e273"}, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0","625f31236e495f62b556f1e1" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e"}, DeveloperId = "625f3a0a23345ba753604f94", PublisherId = "625f3a0a23345ba753604f9e", GenreIds = new List<string>() {"625f35ca6d476218346a3e00"}, ThemeIds = new List<string>() {"625f35ca6d476218346a3e00", "625f3a0a23345ba753604fa4", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fa6", "62643c6e89678f4b841fc59a", "625f3a0a23345ba753604faa", "625f3a0a23345ba753604fac" }, PlatformIds = new List<string>() {"6264261c855881e755a5e273", "62642d57ac1d976dca0231fa", "625f3b63dd256f5e40cabb35" }, Name = "Assassin's Creed: Revelations", FranchiseId = "625f2f636f10b8f02d5f97dd" },

                new Game {CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1xii.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1351551600 , PlatformIds = new List<string>() { "62642d57ac1d976dca0231fa","6264261c855881e755a5e273" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1353193200, PlatformIds = new List<string>() { "62646f01436844a63273940e" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1353366000, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb35"}, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0","625f31236e495f62b556f1e1" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e"}, DeveloperId = "625f3a0a23345ba753604f9e", PublisherId = "625f3a0a23345ba753604f9e", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "626470966f2f889748f19a48" }, ThemeIds = new List<string>() {"625f35ca6d476218346a3e00", "625f3a0a23345ba753604fa4", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fa6", "62643c6e89678f4b841fc59a", "625f3a0a23345ba753604faa", "625f3a0a23345ba753604fac" }, PlatformIds = new List<string>() {"6264261c855881e755a5e273", "62642d57ac1d976dca0231fa", "625f3b63dd256f5e40cabb35", "62646f01436844a63273940e" }, Name = "Assassin's Creed III", FranchiseId = "625f2f636f10b8f02d5f97dd" },

                new Game { Ratings = new List<Rating>() {
                    new Rating { Author = "spookyceres", Score = 7, Comment = "As a mobile game? Amazing. Outside of that? It loses a lot of it's luster. Things, especially the storytelling just do not carry over well to this game. Don't get me wrong, the game introduces some fantastic mechanics that I really hope will return at one point. The concept of different 'personas' having different abilities and even different missions is awesome. The Bayou area is super awesome and unique and I'd love to see it again. Really the main issues with this game are with the storytelling. The story is great, and the voice actors are good especially for a mobile game. The execution of the storytelling? Not so great. Things seem very sudden, or not explained well, and a lot of times you are wondering what the heck is going on. Some of this is remedied in the end, but some is made more confusing. The story also seems.... kind of pointless. Granted, this is a side-game. But I feel like something could have done to make it seem worth it, a better connection to the main arc or something to that effect. Overall, I would say a good game, but only for those that are already invested in the series.", Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }
                ,CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1xim.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1351551600 , PlatformIds = new List<string>() { "6264733e6f2f889748f19a49" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1389654000, PlatformIds = new List<string>() { "62642d57ac1d976dca0231fa" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1389740400, PlatformIds = new List<string>() { "6264261c855881e755a5e273", "625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0","625f31236e495f62b556f1e1" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e"}, DeveloperId = "626474f76f2f889748f19a4b", PublisherId = "625f3a0a23345ba753604f9e", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "626470966f2f889748f19a48", "6264747a6f2f889748f19a4a" }, ThemeIds = new List<string>() {"625f35ca6d476218346a3e00", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fa6", "625f3a0a23345ba753604fac" }, PlatformIds = new List<string>() {"625f2f636f10b8f02d5f97dd", "62642d57ac1d976dca0231fa", "6264261c855881e755a5e273", "625f3b63dd256f5e40cabb35" }, Name = "Assassin's Creed III: Liberation", FranchiseId = "625f2f636f10b8f02d5f97dd" },

                new Game { Ratings = new List<Rating>() {
                    new Rating { Author = "spicylime", Score = 6, Comment = "It's impossible for me to rate this game with more than 6 stars having so many bugs. No gamebreaker that I could experience, but all combats were bugged and quite uncomfortable. And playing an Assassin's Creed and not enjoying the battles and killings is really sad. The main story was ok, a bit shallow in the character development but quite decent. I really enjoyed the story outside the Animus, the info you get from hacking the computers was priceless to me!", Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }
                ,CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co2gjx.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1383001200 , PlatformIds = new List<string>() { "62646f01436844a63273940e", "62642d57ac1d976dca0231fa", "6264261c855881e755a5e273" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1384470000, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb33" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1384815600, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1385074800, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb32" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1631570400, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb37" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0","625f31236e495f62b556f1e1", "625f31236e495f62b556f1e2" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e"}, DeveloperId = "625f3a0a23345ba753604f94", PublisherId = "625f3a0a23345ba753604f9e", GenreIds = new List<string>() {"625f35ca6d476218346a3e00", "625f35ca6d476218346a3df8" }, ThemeIds = new List<string>() {"625f35ca6d476218346a3e00", "625f3a0a23345ba753604fa4", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fa6", "62643c6e89678f4b841fc59a", "625f3a0a23345ba753604faa", "625f3a0a23345ba753604fac" }, PlatformIds = new List<string>() {"62646f01436844a63273940e", "62642d57ac1d976dca0231fa", "6264261c855881e755a5e273", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb37" }, Name = "Assassin's Creed IV: Black Flag", FranchiseId = "625f2f636f10b8f02d5f97dd" },

                new Game { CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1xir.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate {TimeStamp = 1415660400 , PlatformIds = new List<string>() { "62642d57ac1d976dca0231fa", "6264261c855881e755a5e273" }, Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new ReleaseDate { TimeStamp = 1425855600, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }}, GameModeIds = new List<string>() {"625f31236e495f62b556f1e0","625f31236e495f62b556f1e1" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e"}, DeveloperId = "625f3a0a23345ba753604f9e", PublisherId = "625f3a0a23345ba753604f9e", GenreIds = new List<string>() {"625f35ca6d476218346a3e00"}, ThemeIds = new List<string>() {"625f35ca6d476218346a3e00", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fa6", "625f3a0a23345ba753604fac" }, PlatformIds = new List<string>() {"62642d57ac1d976dca0231fa", "6264261c855881e755a5e273", "625f3b63dd256f5e40cabb35" }, Name = "Assassin's Creed: Rogue", FranchiseId = "625f2f636f10b8f02d5f97dd" },

                new Game {Ratings = new List<Rating>() {
                    new Rating { Author = "spookyceres", Score = 6, Comment = "This game takes pretty much all of the previous Assassin's Creed gameplay, and turns it on it's head, for better, or for worse. Control schemes are wildly different than previous incarnations, multiplayer is now an essential part of the game if you want to progress fully, character customization. The list goes on and on. For all it does good, it does a lot wrong unfortunately. There were two specific things that I really was turned off by. The first, is that the last several entries in the series (III, Black Flag, Rogue), you had a very interesting environment to explore, and huge at that. This game was a huge regression in that to me. You basically have a city to run around and do things in again and that is it. It gets very boring quickly for me. The other thing is that the story just kind of.... sucked. Which is very unusual for a Creed game. Other than the beginning, there was little to no ties to the previous games, and no real progression of the main storyline in the series. Also the whole Animus TV stuff SUCKED, and it was weird and not done correctly. That's the real problem throughout the game, is it is not explained well enough, I felt lost in the story the entire game through. The DLC was a real treat, introduced a lot of awesome features we have never before seen in a Creed game, but even then, the story was just not implemented correctly. Okay, that's the bad. The good, is surprisingly enough, the multiplayer. I was very skeptical of this at first but the more I played it, the more I enjoyed it. Something about coordinating with team-members to plan an assassination just gives you a serious badass feeling. The combat is super fun and varied. The new items are great too. Basically, all of the gameplay elements are great. Everything else is just kind of meh. I would recommend this game for people who are fans of the series, but only hardcore fans.", Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new Rating { Author = "fargo", Score = 5, Comment = "It is very sad, this game could have been at least 90% with more developing time and care, and could have been among the best AC, but it will instead be remembered as one of the worst (if not the worst) by many people. It has a great setting, interesting and charismatic characters and the plot seems to be tapping something but they wrapped up, packed, and delivered before it got into something good. I am not going to discuss ALL the annoyances of the gameplay, I will just say that it was unbeliavably terrible.", Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } , new Rating { Author = "thegodfather", Score = 1, Comment = "The French revolution is one great theme. Yet the game failed to take it the correct way. The story has some awesome ideas, but it all seems unfinished, cut in the middle with some 'believable' ending. Same goes for the DLC Dead Kings. Side missions are very repetitive, which is for example compared to AC3 a deep jump down quality speaking. Graphics somewhere looks pretty amazing, but turn around and pixelated tree welcomes you. Back in the day, I was reading in the news how they did create an awesome algorithm for generating public crowds. And today it seems that they've spent most of the development time on it. Yet this functionality although looking fine is messing with CPU, generating so many bugs, and also slowing you down as a character forcing you to not run through streets and take other ways. So you'll evade crowd anyway. And... have I mentioned bugs? ACU is full of them even years after the last patch. Another punch in the face of players was having about 60-70% of inventory full of items that are 'achievable online only', although they've corrected it in the last patch. Still, it was there for months. I do not recommend this game. Rather check the story on youtube and go play Syndicate, as it has all the Unity features and more.", Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new Rating { Author = "davetherave", Score = 8, Comment = "after all the patches and the update the game got over the years, the game can be playable, and even fun. the city (Paris, actually Paris) is great, and it feels alive while you explore it, the mechanics are good but can be better, and the story is very good", Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, },
                CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1xiq.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate { TimeStamp = 1415660400, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1607986800, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb37" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() { "625f31236e495f62b556f1e0", "625f31236e495f62b556f1e1", "625f31236e495f62b556f1e2" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e" }, DeveloperId = "625f3a0a23345ba753604f9e", PublisherId = "625f3a0a23345ba753604f9e", GenreIds = new List<string>() { "625f35ca6d476218346a3e00" }, ThemeIds = new List<string>() { "625f35ca6d476218346a3e00", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fa6", "625f3a0a23345ba753604fac" }, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb37" }, Name = "Assassin's Creed: Unity", FranchiseId = "625f2f636f10b8f02d5f97dd" },

                new Game{Ratings = new List<Rating>() {
                    new Rating { Author = "ngc1569", Score = 8, Comment = "Played after Unity - one of the major setbacks for AC - and I really enjoyed it. The characters are fine, could be little better in their development but it is a good game to spend a good number of hours", Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new Rating { Author = "thehustler", Score = 8, Comment = "After so many AC that have god bad reviews (apart from 4) this was really a braeth of fresh air. I really like that you can play 2 characters and they are in itself different, even if you can play them the same way.", Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }},
                CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1xis.png", ReleaseDates = new List<ReleaseDate>() { new ReleaseDate { TimeStamp = 1445551200, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33"}, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1447887600, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1607986800, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb37" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } }, GameModeIds = new List<string>() { "625f31236e495f62b556f1e0" } , PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e" }, DeveloperId = "626485ad6f2f889748f19a4d", PublisherId = "625f3a0a23345ba753604f9e", GenreIds = new List<string>() { "625f35ca6d476218346a3e00" }, ThemeIds = new List<string>() { "625f35ca6d476218346a3e00", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fa6", "625f3a0a23345ba753604fac" }, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb37" }, Name = "Assassin's Creed: Syndicate", FranchiseId = "625f2f636f10b8f02d5f97dd" },

                new Game { Ratings = new List<Rating>() {
                    new Rating {Author = "alienbacon", Score = 10, Comment = "A masterpiece of a semi-reboot of the franchise, going from the more linear action adventure style to a more RPG oriented system, making it seem a lot bigger even if some aspects of the new genre might seem like step-backs at first, it was a really good call.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "justcookies", Score = 9, Comment = "When I started the game I wasn't impressed. They took away my AC franchise, I thought. And they did. They took the proven Assassin's Creed formula and refreshed the gameplay with new RPG elements keeping the story structure intact. According to the critics and fanbase, it was a successful rebirth of a brand that fell off. I fell in love with the AC franchise for elements that are not included in this new style anymore. I really, really like AC: Origins, but I haven't fallen in love with it. It is a great RPG action game, but it is not Assassin's Creed anymore. Whether that's good or bad is up to you.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "lastrada", Score = 10, Comment = "one of the best assassin's creed games, the graphics are wonderful, the gameplay is great and the story is very very good.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "davetherave", Score = 7, Comment = "I like the new mechanics systems a lot, it felt very much similar to a witcher game. That said, the open world was massive, but most of it is just empty desert full of the same few side events over and over. It felt like the story had recieved much less attention than previous in previous entries in the series. It felt really weak and the characters weren't interesting. My main issue was how it felt like the free running/climbing was sidelined in this game, its always been one of the reasons i liked the AC series but in this game you spend far more time wandering through deserts and swamps than climbing interesting scenery and architecture. Its still good though, just lacking a lot of the things I liked about assassins creed games previously.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                },
                    CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1rbe.png",
                    ReleaseDates = new List<ReleaseDate>() { new ReleaseDate { TimeStamp = 1508968800, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1509055200, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1607986800, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb37" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } },
                    GameModeIds = new List<string>() { "625f31236e495f62b556f1e0" },
                    PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e" },
                    DeveloperId = "625f3a0a23345ba753604f94",
                    PublisherId = "625f3a0a23345ba753604f9e",
                    GenreIds = new List<string>() { "625f35ca6d476218346a3e00", "625f35ca6d476218346a3dfc" },
                    ThemeIds = new List<string>() { "625f35ca6d476218346a3e00", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fa6", "625f3a0a23345ba753604fac" },
                    PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb37" },
                    Name = "Assassin's Creed: Origins",
                    FranchiseId = "625f2f636f10b8f02d5f97dd"
                },

                new Game { Ratings = new List<Rating>() {
                    new Rating {Author = "lastrada", Score = 10, Comment = "My first platinum trophy. Great, game, which further reinvents the AC franchise after its predecessor Origins started it all. Amazing story, graphics, gameplay, RPG elements. The only aspect lacking is the world: although massive, it is not as impressive as Origins' Egypt. If you like role-playing games, you will lose a hundred hours playing this game. And you won't regret it.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "mmmax", Score = 5, Comment = "Lowest common denominator, The first 15 hours of AC Odyssey are a peach; beautiful world, some interesting characters, combat feels decent and there's an interesting setup to the story. You look at the map and think -whoa- that's a massive world, can they make it interesting? No, they can't.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "itwild", Score = 10, Comment = "I've been playing this game since around midnight when it was officially released. Although I wanted to do a '24 hours in' review, I can't wait another two and a half hours to gush about this game!", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "cedarwood", Score = 9, Comment = "This game is on the same level as the Witcher 3, Red Dead II, any of the biggest and best open world engrossing action games. Assassin's Creed breaking its mold. Play this, play this, play this, if you like games that you can sink time into and be rewarded.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                },
                    CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co2nul.png",
                    ReleaseDates = new List<ReleaseDate>() { new ReleaseDate { TimeStamp = 1538690400, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1574118000, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb37" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1607986800, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb37" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now } },
                    GameModeIds = new List<string>() { "625f31236e495f62b556f1e0" },
                    PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e" },
                    DeveloperId = "626485ad6f2f889748f19a4d",
                    PublisherId = "625f3a0a23345ba753604f9e",
                    GenreIds = new List<string>() { "625f35ca6d476218346a3e00", "625f35ca6d476218346a3dfc" },
                    ThemeIds = new List<string>() { "625f35ca6d476218346a3e00", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fac" },
                    PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb37" },
                    Name = "Assassin's Creed: Odyssey",
                    FranchiseId = "625f2f636f10b8f02d5f97dd"
                },

                new Game { Ratings = new List<Rating>() {
                    new Rating {Author = "chocolatebar", Score = 8, Comment = "After Odyssey quite disappointing (especially storywise), but still enjoyable.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "dasboot", Score = 4, Comment = "Didn't even finish it. Wasn't fun to play compared to the other AC games.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "itwild", Score = 10, Comment = "I've been playing this game since around midnight when it was officially released. Although I wanted to do a '24 hours in' review, I can't wait another two and a half hours to gush about this game!", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}, new Rating {Author = "lastrada", Score = 8, Comment = "There is so much to say about the game, but I will try and say it in one paragraph. Not as good as Origins or Odyssey. Changes that have been made are for the worse. I couldn't care less about Vikings, England, and consequently, the story. There are many characters, almost all of them forgettable. Present day line interesting, although ultimately underwhelming. Collectibles are fun, territories are fun as well, but not different enough. And bugs, oh my god, the bugs. I would be ashamed to put out the game in the state it was when I started playing. After updates, it's okay, but not perfect still. But it's Assassin's Creed, my favorite franchise, I love it, and - give me more.", Id = Guid.NewGuid().ToString() , CreatedOn = DateTime.Now}
                },
                    CoverUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co2nul.png",
                    ReleaseDates = new List<ReleaseDate>() { new ReleaseDate { TimeStamp = 1604962800, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb36", "625f3b63dd256f5e40cabb37" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }, new ReleaseDate { TimeStamp = 1605135600, PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb34" }, Id = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now }},
                    GameModeIds = new List<string>() { "625f31236e495f62b556f1e0" },
                    PlayerPerspectiveIds = new List<string>() { "625f334581586edfafce4f9e" },
                    DeveloperId = "625f3a0a23345ba753604f94",
                    PublisherId = "625f3a0a23345ba753604f9e",
                    GenreIds = new List<string>() { "625f35ca6d476218346a3e00" },
                    ThemeIds = new List<string>() { "625f35ca6d476218346a3e00", "625f3a0a23345ba753604fab", "625f3a0a23345ba753604fa6" },
                    PlatformIds = new List<string>() { "625f3b63dd256f5e40cabb32", "625f3b63dd256f5e40cabb33", "625f3b63dd256f5e40cabb35", "625f3b63dd256f5e40cabb36", "625f3b63dd256f5e40cabb37", "625f3b63dd256f5e40cabb34" },
                    Name = "Assassin's Creed Valhalla",
                    FranchiseId = "625f2f636f10b8f02d5f97dd"
                },
        });
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    #endregion
}