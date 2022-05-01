namespace Games.GraphQL;

public class Mutation
{
    #region GENERICS

    public async Task<AddGenericPayload> AddGenre([Service] IGameService gameService, AddGenericInput input)
    {
        var newGenre = new Genre()
        {
            Name = input.Name
        };
        var Created = await gameService.AddGenres(new List<Genre>() { newGenre });
        return new AddGenericPayload(Created.First());
    }

    public async Task<AddGenericsPayload> AddGenres([Service] IGameService gameService, AddGenericsInput input)
    {
        List<Genre> newGenres = new List<Genre>();
        List<Generic> returnValue = new List<Generic>();

        foreach (AddGenericInput i in input.generics)
        {
            var newGenre = new Genre()
            {
                Name = i.Name
            };
            newGenres.Add(newGenre);
        }

        var Created = await gameService.AddGenres(newGenres);

        foreach (Generic g in Created)
        {
            var newGeneric = new Generic()
            {
                Name = g.Name
            };
            returnValue.Add(newGeneric);
        }

        return new AddGenericsPayload(returnValue);
    }

    public async Task<UpdateGenericPayload> UpdateGenre([Service] IGameService gameService, UpdateGenericInput input)
    {

        var newGenre = new Genre()
        {
            Id = input.generic.Id,
            Name = input.generic.Name,
            CreatedOn = input.generic.CreatedOn
        };

        var result = await gameService.UpdateGenre(newGenre.Id, newGenre);
        return new UpdateGenericPayload(result);
    }

    public async Task<AddGenericPayload> AddGameMode([Service] IGameService gameService, AddGenericInput input)
    {
        var newGameMode = new GameMode()
        {
            Name = input.Name
        };
        var Created = await gameService.AddGameModes(new List<GameMode>() { newGameMode });
        return new AddGenericPayload(Created.First());
    }

    public async Task<AddGenericsPayload> AddGameModes([Service] IGameService gameService, AddGenericsInput input)
    {
        List<GameMode> newGameModes = new List<GameMode>();
        List<Generic> returnValue = new List<Generic>();

        foreach (AddGenericInput i in input.generics)
        {
            var newGameMode = new GameMode()
            {
                Name = i.Name
            };
            newGameModes.Add(newGameMode);
        }

        var Created = await gameService.AddGameModes(newGameModes);

        foreach (Generic g in Created)
        {
            var newGeneric = new Generic()
            {
                Name = g.Name
            };
            returnValue.Add(newGeneric);
        }

        return new AddGenericsPayload(returnValue);
    }

    public async Task<UpdateGenericPayload> UpdateGameMode([Service] IGameService gameService, UpdateGenericInput input)
    {

        var newGameMode = new GameMode()
        {
            Id = input.generic.Id,
            Name = input.generic.Name,
            CreatedOn = input.generic.CreatedOn
        };

        var result = await gameService.UpdateGameMode(newGameMode.Id, newGameMode);
        return new UpdateGenericPayload(result);
    }

    public async Task<AddGenericPayload> AddPlayerPerspective([Service] IGameService gameService, AddGenericInput input)
    {
        var newPlayerPerspective = new PlayerPerspective()
        {
            Name = input.Name
        };
        var Created = await gameService.AddPlayerPerspectives(new List<PlayerPerspective>() { newPlayerPerspective });
        return new AddGenericPayload(Created.First());
    }

    public async Task<AddGenericsPayload> AddPlayerPerspectives([Service] IGameService gameService, AddGenericsInput input)
    {
        List<PlayerPerspective> newPlayerPerspectives = new List<PlayerPerspective>();
        List<Generic> returnValue = new List<Generic>();

        foreach (AddGenericInput i in input.generics)
        {
            var newPlayerPerspective = new PlayerPerspective()
            {
                Name = i.Name
            };
            newPlayerPerspectives.Add(newPlayerPerspective);
        }

        var Created = await gameService.AddPlayerPerspectives(newPlayerPerspectives);

        foreach (Generic g in Created)
        {
            var newGeneric = new Generic()
            {
                Name = g.Name
            };
            returnValue.Add(newGeneric);
        }

        return new AddGenericsPayload(returnValue);
    }

    public async Task<UpdateGenericPayload> UpdatePlayerPerspective([Service] IGameService gameService, UpdateGenericInput input)
    {

        var newPlayerPerspective = new PlayerPerspective()
        {
            Id = input.generic.Id,
            Name = input.generic.Name,
            CreatedOn = input.generic.CreatedOn
        };

        var result = await gameService.UpdatePlayerPerspective(newPlayerPerspective.Id, newPlayerPerspective);
        return new UpdateGenericPayload(result);
    }

    public async Task<AddGenericPayload> AddFranchise([Service] IGameService gameService, AddGenericInput input)
    {
        var newFranchise = new Franchise()
        {
            Name = input.Name
        };
        var Created = await gameService.AddFranchises(new List<Franchise>() { newFranchise });
        return new AddGenericPayload(Created.First());
    }

    public async Task<AddGenericsPayload> AddFranchises([Service] IGameService gameService, AddGenericsInput input)
    {
        List<Franchise> newFranchises = new List<Franchise>();
        List<Generic> returnValue = new List<Generic>();

        foreach (AddGenericInput i in input.generics)
        {
            var newFranchise = new Franchise()
            {
                Name = i.Name
            };
            newFranchises.Add(newFranchise);
        }

        var Created = await gameService.AddFranchises(newFranchises);

        foreach (Generic g in Created)
        {
            var newGeneric = new Generic()
            {
                Name = g.Name
            };
            returnValue.Add(newGeneric);
        }

        return new AddGenericsPayload(returnValue);
    }

    public async Task<UpdateGenericPayload> UpdateFranchise([Service] IGameService gameService, UpdateGenericInput input)
    {

        var newFranchise = new Franchise()
        {
            Id = input.generic.Id,
            Name = input.generic.Name,
            CreatedOn = input.generic.CreatedOn
        };

        var result = await gameService.UpdateFranchise(newFranchise.Id, newFranchise);
        return new UpdateGenericPayload(result);
    }

    public async Task<AddGenericPayload> AddTheme([Service] IGameService gameService, AddGenericInput input)
    {
        var newTheme = new Theme()
        {
            Name = input.Name
        };
        var Created = await gameService.AddThemes(new List<Theme>() { newTheme });
        return new AddGenericPayload(Created.First());
    }

    public async Task<AddGenericsPayload> AddThemes([Service] IGameService gameService, AddGenericsInput input)
    {
        List<Theme> newThemes = new List<Theme>();
        List<Generic> returnValue = new List<Generic>();

        foreach (AddGenericInput i in input.generics)
        {
            var newTheme = new Theme()
            {
                Name = i.Name
            };
            newThemes.Add(newTheme);
        }

        var Created = await gameService.AddThemes(newThemes);

        foreach (Generic g in Created)
        {
            var newGeneric = new Generic()
            {
                Name = g.Name
            };
            returnValue.Add(newGeneric);
        }

        return new AddGenericsPayload(returnValue);
    }

    public async Task<UpdateGenericPayload> UpdateTheme([Service] IGameService gameService, UpdateGenericInput input)
    {

        var newTheme = new Theme()
        {
            Id = input.generic.Id,
            Name = input.generic.Name,
            CreatedOn = input.generic.CreatedOn
        };

        var result = await gameService.UpdateTheme(newTheme.Id, newTheme);
        return new UpdateGenericPayload(result);
    }

    #endregion

    #region PLATFORMS

    public async Task<AddPlatformPayload> AddPlatform([Service] IGameService gameService, AddPlatformInput input)
    {
        var newPlatform = new Platform()
        {
            ManufacturerId = input.ManufacturerId,
            Name = input.Name,
            ReleaseDate = input.ReleaseDate
        };

        var Created = await gameService.AddPlatforms(new List<Platform>() { newPlatform });
        return new AddPlatformPayload(Created.First());
    }

    public async Task<AddPlatformsPayload> AddPlatforms([Service] IGameService gameService, AddPlatformsInput input)
    {
        List<Platform> newPlatforms = new List<Platform>();

        foreach (AddPlatformInput i in input.platforms)
        {
            var newPlatform = new Platform()
            {
                Name = i.Name,
                ManufacturerId = i.ManufacturerId,
                ReleaseDate = i.ReleaseDate
            };
            newPlatforms.Add(newPlatform);
        }

        var Created = await gameService.AddPlatforms(newPlatforms);
        return new AddPlatformsPayload(Created);
    }

    public async Task<UpdatePlatformPayload> UpdatePlatform([Service] IGameService gameService, UpdatePlatformInput input)
    {

        var newPlatform = new Platform()
        {
            Id = input.platform.Id,
            Name = input.platform.Name,
            CreatedOn = input.platform.CreatedOn,
            ManufacturerId = input.platform.ManufacturerId,
            ReleaseDate = input.platform.ReleaseDate
        };

        var result = await gameService.UpdatePlatform(newPlatform.Id, newPlatform);
        return new UpdatePlatformPayload(result);
    }

    #endregion

    #region COMPANIES

    public async Task<AddCompanyPayload> AddCompany([Service] IGameService gameService, AddCompanyInput input)
    {
        var newCompany = new Company()
        {
            Name = input.Name,
            Type = input.Type,
            Country = input.Country,
            ParentCompanyId = input.ParentCompanyId,
            SubcompanyIds = input.subCompanyIds
        };

        var Created = await gameService.AddCompanies(new List<Company>() { newCompany });
        return new AddCompanyPayload(Created.First());
    }

    public async Task<AddCompaniesPayload> AddCompanies([Service] IGameService gameService, AddCompaniesInput input)
    {
        List<Company> newCompanies = new List<Company>();

        foreach (AddCompanyInput i in input.companies)
        {
            var newCompany = new Company()
            {
                Name = i.Name,
                Type = i.Type,
                Country = i.Country,
                ParentCompanyId = i.ParentCompanyId,
                SubcompanyIds = i.subCompanyIds
            };
            newCompanies.Add(newCompany);
        }

        var Created = await gameService.AddCompanies(newCompanies);
        return new AddCompaniesPayload(Created);
    }

    public async Task<UpdateCompanyPayload> UpdateCompany([Service] IGameService gameService, UpdateCompanyInput input)
    {

        var newCompany = new Company()
        {
            Id = input.company.Id,
            Name = input.company.Name,
            CreatedOn = input.company.CreatedOn,
            Type = input.company.Type,
            Country = input.company.Country,
            ParentCompanyId = input.company.ParentCompanyId,
            SubcompanyIds = input.company.SubcompanyIds
        };

        var result = await gameService.UpdateCompany(newCompany.Id, newCompany);
        return new UpdateCompanyPayload(result);
    }

    #endregion

    #region GAMES

    public async Task<AddGamePayload> AddGame([Service] IGameService gameService, AddGameInput input)
    {
        var newGame = new Game()
        {
            Name = input.Name,
            FranchiseId = input.FranchiseId,
            Ratings = input.Ratings,
            CoverUrl = input.CoverUrl,
            ReleaseDates = input.ReleaseDates,
            GameModeIds = input.GameModeIds,
            PlayerPerspectiveIds = input.PlayerPerspectiveIds,
            DeveloperId = input.DeveloperId,
            PublisherId = input.PublisherId,
            GenreIds = input.GenreIds,
            ThemeIds = input.ThemeIds,
            PlatformIds = input.PlatformIds
        };

        var Created = await gameService.AddGame(newGame);
        return new AddGamePayload(Created);
    }

    public async Task<UpdateGamePayload> UpdateGame([Service] IGameService gameService, UpdateGameInput input)
    {

        var newGame = new Game()
        {
            Id = input.game.Id,
            Name = input.game.Name,
            CreatedOn = input.game.CreatedOn,
            FranchiseId = input.game.FranchiseId,
            Ratings = input.game.Ratings,
            CoverUrl = input.game.CoverUrl,
            ReleaseDates = input.game.ReleaseDates,
            GameModeIds = input.game.GameModeIds,
            PlayerPerspectiveIds = input.game.PlayerPerspectiveIds,
            DeveloperId = input.game.DeveloperId,
            PublisherId = input.game.PublisherId,
            GenreIds = input.game.GenreIds,
            ThemeIds = input.game.ThemeIds,
            PlatformIds = input.game.PlatformIds
        };

        var result = await gameService.UpdateGame(newGame.Id, newGame);
        return new UpdateGamePayload(result);
    }

    #endregion
}