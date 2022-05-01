#region BUILDER

var builder = WebApplication.CreateBuilder(args);
var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);
var authenticationSettings = builder.Configuration.GetSection("AuthenticationSettings");
builder.Services.Configure<AuthenticationSettings>(authenticationSettings);
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IFranchiseRepository, FranchiseRepository>();
builder.Services.AddTransient<IGameModeRepository, GameModeRepository>();
builder.Services.AddTransient<IGameRepository, GameRepository>();
builder.Services.AddTransient<IPlatformRepository, PlatformRepository>();
builder.Services.AddTransient<IThemeRepository, ThemeRepository>();
builder.Services.AddTransient<IPlayerPerspectiveRepository, PlayerPerspectiveRepository>();
builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GenreValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GameModeValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PlayerPerspectiveValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<FranchiseValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ThemeValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CompanyValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GameValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PlatformValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RatingValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ReleaseDateValidator>());
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddMutationType<Mutation>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(options => { });

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AuthenticationSettings:issuer"],
        ValidAudience = builder.Configuration["AuthenticationSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationSettings:SecretForKey"]))
    };
});

var app = builder.Build();
app.MapGraphQL();
app.MapSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();

#endregion

// TODO : clean up + refactor program.cs/validators zodat program.cs niet vol staat met custom validation.

#region AUTHENTICATION

app.MapPost("/authentication", async (IAuthenticationService authenticationService, IOptions<AuthenticationSettings> authSettings, AuthenticationRequestBody authenticationRequestBody) =>
{
    var user = authenticationService.ValidateUser(authenticationRequestBody.username, authenticationRequestBody.password);


    if (user == null)
    {
        return Results.Unauthorized();
    }

    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(authSettings.Value.SecretForKey));

    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claimsForToken = new List<Claim>();

    claimsForToken.Add(new Claim("sub", "1"));
    claimsForToken.Add(new Claim("given_name", user.name));
    claimsForToken.Add(new Claim("city", user.city));

    var JwtSecurityToken = new JwtSecurityToken(
        authSettings.Value.Issuer,
        authSettings.Value.Audience,
        claimsForToken,
        DateTime.UtcNow,
        DateTime.UtcNow.AddHours(1),
        signingCredentials
    );

    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);

    return Results.Ok(tokenToReturn);
});

#endregion

#region GENERAL

app.MapGet("/", () => "The API is up and running!");

app.MapGet("/setup", async (IGameService gameService) =>
{
    await gameService.SetUpData();
    return "Data setup complete!";
});

#endregion

#region GENRES

app.MapGet("/genres", async (IGameService gameService) => Results.Ok(await gameService.GetGenres()));

app.MapGet("/genres/{id}/games", async (IGameService gameService, IMapper mapper, string id) => Results.Ok(mapper.Map<List<GameDTO>>(await gameService.GetGamesByGenre(id))));

app.MapPost("/genres", async (IGameService gameService, List<Genre> genres) =>
{

    foreach (Genre g in genres)
    {
        var validator = new GenreValidator(gameService);

        var validationResult = validator.Validate(g);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }
        if (genres.Where(i => i.Name == g.Name).Count() != 1)
        {
            return Results.BadRequest(new List<Games.Models.Error> { new Games.Models.Error() { errors = "Name must be unique!" } });
        }
    }

    return Results.Created("/genres", await gameService.AddGenres(genres));
});

app.MapPut("/genres/{id}", async (IGameService gameService, string id, Genre genre) =>
{
    var secValidator = new GameValidator(gameService);

    if (!secValidator.CheckExistenceGenre(id))
        return Results.NotFound();
    else
    {
        var result = await gameService.GetGenre(id);
        var validator = new GenreValidator(gameService, result.Name);
        var validationResult = validator.Validate(genre);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }

        return Results.Ok(await gameService.UpdateGenre(id, genre));
    }
});

#endregion

#region COMPANIES

app.MapGet("/companies", async (IGameService gameService) => Results.Ok(await gameService.GetCompanies()));

app.MapGet("/companies/{id}", async (IGameService gameService, string id) =>
{
    var validator = new GameValidator(gameService);

    if (!validator.CheckExistenceCompany(id))
    {
        return Results.NotFound();
    }

    return Results.Ok(await gameService.GetCompany(id));
});

app.MapGet("/companies/{id}/games", async (IGameService gameService, IMapper mapper, string id) => Results.Ok(mapper.Map<List<GameDTO>>(await gameService.GetGamesByCompany(id))));

app.MapPost("/companies", async (IGameService gameService, List<Company> companies) =>
{
    foreach (Company c in companies)
    {
        var validator = new CompanyValidator(gameService);

        var validationResult = validator.Validate(c);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }
        if (companies.Where(i => i.Name == c.Name).Count() != 1)
        {
            return Results.BadRequest(new List<Games.Models.Error> { new Games.Models.Error() { errors = "Name must be unique!" } });
        }
    }

    return Results.Created("/companies", await gameService.AddCompanies(companies));
});

app.MapPut("/companies/{id}", async (IGameService gameService, string id, Company company) =>
{
    var secValidator = new GameValidator(gameService);

    if (!secValidator.CheckExistenceCompany(id))
        return Results.NotFound();
    else
    {
        var result = await gameService.GetCompany(id);
        var validator = new CompanyValidator(gameService, result.Name);

        var validationResult = validator.Validate(company);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }

        return Results.Ok(await gameService.UpdateCompany(id, company));
    }
});

#endregion

#region FRANCHISES

app.MapGet("/franchises", async (IGameService gameService) => Results.Ok(await gameService.GetFranchises()));

app.MapGet("/franchises/{id}/games", async (IGameService gameService, IMapper mapper, string id) => Results.Ok(mapper.Map<List<GameDTO>>(await gameService.GetGamesByFranchise(id))));

app.MapPost("/franchises", async (IGameService gameService, List<Franchise> franchises) =>
{
    foreach (Franchise f in franchises)
    {
        var validator = new FranchiseValidator(gameService);

        var validationResult = validator.Validate(f);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }
        if (franchises.Where(i => i.Name == f.Name).Count() != 1)
        {
            return Results.BadRequest(new List<Games.Models.Error> { new Games.Models.Error() { errors = "Name must be unique!" } });
        }
    }

    return Results.Created("/franchises", await gameService.AddFranchises(franchises));
});

app.MapPut("/franchises/{id}", async (IGameService gameService, string id, Franchise franchise) =>
{
    var secValidator = new GameValidator(gameService);

    if (!secValidator.CheckExistenceFranchise(id))
        return Results.NotFound();
    else
    {
        var result = await gameService.GetFranchise(id);
        var validator = new FranchiseValidator(gameService, result.Name);

        var validationResult = validator.Validate(franchise);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }

        return Results.Ok(await gameService.UpdateFranchise(id, franchise));
    }
});

#endregion

#region GAMEMODES

app.MapGet("/gamemodes", async (IGameService gameService) => Results.Ok(await gameService.GetGameModes()));

app.MapGet("/gamemodes/{id}/games", async (IGameService gameService, IMapper mapper, string id) => Results.Ok(mapper.Map<List<GameDTO>>(await gameService.GetGamesByGameMode(id))));

app.MapPost("/gamemodes", async (IGameService gameService, List<GameMode> gameModes) =>
{
    foreach (GameMode g in gameModes)
    {
        var validator = new GameModeValidator(gameService);

        var validationResult = validator.Validate(g);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }
        if (gameModes.Where(i => i.Name == g.Name).Count() != 1)
        {
            return Results.BadRequest(new List<Games.Models.Error> { new Games.Models.Error() { errors = "Name must be unique!" } });
        }
    }

    return Results.Created("/gamemodes", await gameService.AddGameModes(gameModes));
});

app.MapPut("/gamemodes/{id}", async (IGameService gameService, string id, GameMode gameMode) =>
{
    var secValidator = new GameValidator(gameService);

    if (!secValidator.CheckExistenceGameMode(id))
        return Results.NotFound();
    else
    {
        var result = await gameService.GetGameMode(id);
        var validator = new GameModeValidator(gameService, result.Name);

        var validationResult = validator.Validate(gameMode);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }

        return Results.Ok(await gameService.UpdateGameMode(id, gameMode));
    }
});

#endregion

#region PLAYERPERSPECTIVES

app.MapGet("/playerperspectives", async (IGameService gameService) => Results.Ok(await gameService.GetPlayerPerspectives()));

app.MapGet("/playerperspectives/{id}/games", async (IGameService gameService, IMapper mapper, string id) => Results.Ok(mapper.Map<List<GameDTO>>(await gameService.GetGamesByPlayerPerspective(id))));

app.MapPost("/playerperspectives", async (IGameService gameService, List<PlayerPerspective> playerPerspectives) =>
{
    foreach (PlayerPerspective p in playerPerspectives)
    {
        var validator = new PlayerPerspectiveValidator(gameService);

        var validationResult = validator.Validate(p);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }
        if (playerPerspectives.Where(i => i.Name == p.Name).Count() != 1)
        {
            return Results.BadRequest(new List<Games.Models.Error> { new Games.Models.Error() { errors = "Name must be unique!" } });
        }
    }

    return Results.Created("/playerperspectives", await gameService.AddPlayerPerspectives(playerPerspectives));
});

app.MapPut("/playerperspectives/{id}", async (IGameService gameService, string id, PlayerPerspective playerPerspective) =>
{
    var secValidator = new GameValidator(gameService);

    if (!secValidator.CheckExistencePlayerPerspective(id))
        return Results.NotFound();
    else
    {
        var result = await gameService.GetPlayerPerspective(id);
        var validator = new PlayerPerspectiveValidator(gameService, result.Name);

        var validationResult = validator.Validate(playerPerspective);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }

        return Results.Ok(await gameService.UpdatePlayerPerspective(id, playerPerspective));
    }
});

#endregion

#region THEMES

app.MapGet("/themes", async (IGameService gameService) => Results.Ok(await gameService.GetThemes()));

app.MapGet("/themes/{id}/games", async (IGameService gameService, IMapper mapper, string id) => Results.Ok(mapper.Map<List<GameDTO>>(await gameService.GetGamesByTheme(id))));

app.MapPost("/themes", async (IGameService gameService, List<Theme> themes) =>
{
    foreach (Theme t in themes)
    {
        var validator = new ThemeValidator(gameService);

        var validationResult = validator.Validate(t);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }
        if (themes.Where(i => i.Name == t.Name).Count() != 1)
        {
            return Results.BadRequest(new List<Games.Models.Error> { new Games.Models.Error() { errors = "Name must be unique!" } });
        }
    }

    return Results.Created("/themes", await gameService.AddThemes(themes));
});

app.MapPut("/themes/{id}", async (IGameService gameService, string id, Theme theme) =>
{
    var secValidator = new GameValidator(gameService);

    if (!secValidator.CheckExistenceTheme(id))
        return Results.NotFound();
    else
    {
        var result = await gameService.GetTheme(id);

        var validator = new ThemeValidator(gameService, result.Name);

        var validationResult = validator.Validate(theme);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }

        return Results.Ok(await gameService.UpdateTheme(id, theme));
    }
});

#endregion

#region PLATFORMS

app.MapGet("/platforms", async (IGameService gameService) => Results.Ok(await gameService.GetPlatforms()));

app.MapGet("/platforms/{id}", async (IGameService gameService, string id) =>
{
    var validator = new GameValidator(gameService);

    if (!validator.CheckExistencePlatform(id))
    {
        return Results.NotFound();
    }

    return Results.Ok(await gameService.GetPlatform(id));
});

app.MapGet("/platforms/{id}/games", async (IGameService gameService, IMapper mapper, string id) => Results.Ok(mapper.Map<List<GameDTO>>(await gameService.GetGamesByPlatform(id))));

app.MapPost("/platforms", async (IGameService gameService, List<Platform> platforms) =>
{
    foreach (Platform p in platforms)
    {
        var validator = new PlatformValidator(gameService);

        var validationResult = validator.Validate(p);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }
        if (platforms.Where(i => i.Name == p.Name).Count() != 1)
        {
            return Results.BadRequest(new List<Games.Models.Error> { new Games.Models.Error() { errors = "Name must be unique!" } });
        }
    }

    return Results.Created("/platforms", await gameService.AddPlatforms(platforms));
});

app.MapPut("/platforms/{id}", async (IGameService gameService, string id, Platform platform) =>
{
    var secValidator = new GameValidator(gameService);

    if (!secValidator.CheckExistencePlatform(id))
        return Results.NotFound();
    else
    {
        var result = await gameService.GetPlatform(id);

        var validator = new PlatformValidator(gameService, result.Name);

        var validationResult = validator.Validate(platform);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }

        return Results.Ok(await gameService.UpdatePlatform(id, platform));
    }
});

#endregion

#region GAMES

app.MapGet("/games", async (IMapper mapper, IGameService gameService) => Results.Ok(mapper.Map<List<GameDTO>>(await gameService.GetGames())));

app.MapGet("/games/{id}", async (IMapper mapper, IGameService gameService, string id) =>
{
    var validator = new GameValidator(gameService);

    if (!validator.CheckExistence(id))
        return Results.NotFound();
    else
    {
        var result = await gameService.GetGame(id);
        return Results.Ok(mapper.Map<GameDTO>(result));
    }
});

app.MapPost("/games", async (IGameService gameService, Game game) =>
{

    var validator = new GameValidator(gameService);
    var secValidator = new RatingValidator();
    var terValidator = new ReleaseDateValidator(gameService);

    var validationResult = validator.Validate(game);

    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
    if (!game.ReleaseDates.IsNullOrEmpty())
    {
        foreach (ReleaseDate releaseDate in game.ReleaseDates)
        {
            var res = terValidator.Validate(releaseDate);

            if (!res.IsValid)
            {
                var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
                return Results.BadRequest(errors);
            }
        }
    }
    if (!game.Ratings.IsNullOrEmpty())
    {
        foreach (Rating rating in game.Ratings)
        {
            var res = secValidator.Validate(rating);

            if (!res.IsValid)
            {
                var errors = res.Errors.Select(x => new { errors = x.ErrorMessage });
                return Results.BadRequest(errors);
            }
        }
    }

    return Results.Created("/games/" + game.Id, await gameService.AddGame(game));
});

app.MapPut("/games/{id}", async (IGameService gameService, string id, Game game) =>
{
    var quaValidator = new GameValidator(gameService);

    if (!quaValidator.CheckExistence(id))
        return Results.NotFound();
    else
    {
        var result = await gameService.GetGame(id);
        var validator = new GameValidator(gameService, result.Name);
        var secValidator = new RatingValidator();
        var terValidator = new ReleaseDateValidator(gameService);

        var validationResult = validator.Validate(game);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }
        if (!game.Ratings.IsNullOrEmpty())
        {
            foreach (ReleaseDate releaseDate in game.ReleaseDates)
            {
                var res = terValidator.Validate(releaseDate);

                if (!res.IsValid)
                {
                    var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
                    return Results.BadRequest(errors);
                }
            }
        }
        if (!game.Ratings.IsNullOrEmpty())
        {
            foreach (Rating rating in game.Ratings)
            {
                var res = secValidator.Validate(rating);

                if (!res.IsValid)
                {
                    var errors = res.Errors.Select(x => new { errors = x.ErrorMessage });
                    return Results.BadRequest(errors);
                }
            }
        }

        return Results.Ok(await gameService.UpdateGame(id, game));
    }
});

#endregion

#region GAMES--RATINGS

app.MapPost("/games/{id}/ratings", [Authorize] async (IGameService gameService, string id, Rating rating) =>
{
    var secValidator = new GameValidator(gameService);

    if (!secValidator.CheckExistence(id))
    {
        return Results.NotFound();
    }
    else
    {
        var result = await gameService.GetGame(id);
        var validator = new RatingValidator();

        var validationResult = validator.Validate(rating);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }

        return Results.Created("/games/" + id, await gameService.AddRating(id, rating));
    }
});

app.MapPut("/games/{gameId}/ratings/{ratingId}", [Authorize] async (IGameService gameService, string gameId, string ratingId, Rating rating) =>
{
    var secValidator = new GameValidator(gameService);

    if (!secValidator.CheckExistence(gameId))
        return Results.NotFound();
    else
    {
        var result = await gameService.GetGame(gameId);
        var validator = new RatingValidator();

        var validationResult = validator.Validate(rating);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }

        return Results.Ok(await gameService.UpdateRating(gameId, ratingId, rating));
    }
});

app.MapDelete("/games/{gameId}/ratings/{ratingId}", [Authorize] async (IGameService gameService, string gameId, string ratingId) =>
{
    var validator = new GameValidator(gameService);

    if (!validator.CheckExistence(gameId))
    {
        return Results.NotFound();
    }
    else
    {
        var result = await gameService.GetGame(gameId);
        await gameService.RemoveRating(gameId, ratingId);
        return Results.NoContent();
    }
});

#endregion

#region GAMES--RELEASES

app.MapPost("/games/{id}/releasedates", async (IGameService gameService, string id, ReleaseDate releaseDate) =>
{
    var terValidator = new GameValidator(gameService);

    if (!terValidator.CheckExistence(id))
    {
        return Results.NotFound();
    }
    else
    {
        var result = await gameService.GetGame(id);
        var validator = new ReleaseDateValidator(gameService);
        var secValidator = new GameValidator(gameService);

        var validationResult = validator.Validate(releaseDate);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }

        return Results.Created("/games/" + id, await gameService.AddReleaseDate(id, releaseDate));
    }
});

app.MapPut("/games/{gameId}/releasedates/{releaseDateId}", async (IGameService gameService, string gameId, string releaseDateId, ReleaseDate releaseDate) =>
{
    var terValidator = new GameValidator(gameService);

    if (!terValidator.CheckExistence(gameId))
        return Results.NotFound();
    else
    {
        var result = await gameService.GetGame(gameId);
        var validator = new ReleaseDateValidator(gameService);
        var secValidator = new GameValidator(gameService);

        var validationResult = validator.Validate(releaseDate);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }

        return Results.Ok(await gameService.UpdateReleaseDate(gameId, releaseDateId, releaseDate));
    }
});

app.MapDelete("/games/{gameId}/releasedates/{releaseDateId}", async (IGameService gameService, string gameId, string releaseDateId) =>
{
    var validator = new GameValidator(gameService);

    if (!validator.CheckExistence(gameId))
    {
        return Results.NotFound();
    }
    else
    {
        var result = await gameService.GetGame(gameId);
        await gameService.RemoveReleaseDate(gameId, releaseDateId);
        return Results.NoContent();
    }
});

#endregion

// app.Run("http://0.0.0.0:3000");

app.Run();
public partial class Program { }
