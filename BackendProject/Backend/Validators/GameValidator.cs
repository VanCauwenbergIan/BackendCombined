namespace Games.Validators;

public class GameValidator : AbstractValidator<Game>
{

    private readonly IGameService _service;

    public GameValidator(IGameService service)
    {
        _service = service;

        RuleFor(game => game.Name).NotNull().NotEmpty().WithMessage("Name must not be empty!");
        RuleFor(game => game.Name).MaximumLength(125).WithMessage("Name can not be larger than 125 characters.");

        RuleFor(game => game.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!");
        RuleFor(game => game.FranchiseId).Must(CheckExistenceFranchise).WithMessage("FranchiseId couldn't be found!").Unless(game => game.FranchiseId == null);
        RuleForEach(game => game.PlatformIds).Must(CheckExistencePlatform).WithMessage("One or more PlatformIds couldn't be found!").Unless(game => game.PlatformIds.IsNullOrEmpty());
        RuleFor(game => game.DeveloperId).Must(CheckExistenceCompany).WithMessage("DeveloperId couldn't be found!");
        RuleFor(game => game.PublisherId).Must(CheckExistenceCompany).WithMessage("PublisherId couldn't be found!");
        RuleForEach(game => game.GameModeIds).Must(CheckExistenceGameMode).WithMessage("One or more GameModeIds couldn't be found!").Unless(game => game.GameModeIds.IsNullOrEmpty());
        RuleForEach(game => game.PlayerPerspectiveIds).Must(CheckExistencePlayerPerspective).WithMessage("One or more PlayerPerspectiveIds couldn't be found!").Unless(game => game.PlayerPerspectiveIds.IsNullOrEmpty());
        RuleForEach(game => game.GenreIds).Must(CheckExistenceGenre).WithMessage("One or more GenreIds couldn't be found!").Unless(game => game.GenreIds.IsNullOrEmpty());
        RuleForEach(game => game.ThemeIds).Must(CheckExistenceTheme).WithMessage("One or more ThemeIds couldn't be found!").Unless(game => game.ThemeIds.IsNullOrEmpty());
    }

    public GameValidator(IGameService service, string oldName)
    {
        _service = service;

        RuleFor(game => game.Name).NotNull().NotEmpty().WithMessage("Name must not be empty!");
        RuleFor(game => game.Name).MaximumLength(125).WithMessage("Name can not be larger than 125 characters.");
        RuleFor(game => game.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!").Unless(company => company.Name.ToLower() == oldName.ToLower());
    }

    public bool ValidateUniqueness(string name)
    {
        bool contains = false;

        List<Game> result = _service.GetGames().Result;

        if (result != null)
        {
            contains = result.Any(g => g.Name.ToLower() == name.ToLower());
        }

        return !contains;
    }

    public bool CheckExistence(string id)
    {
        bool contains = false;

        List<Game> result = _service.GetGames().Result;

        if (result != null)
        {
            contains = result.Any(f => f.Id == id);
        }

        return contains;
    }

    public bool CheckExistenceFranchise(string id)
    {
        bool contains = false;

        List<Franchise> result = _service.GetFranchises().Result;

        if (result != null)
        {
            contains = result.Any(f => f.Id == id);
        }

        return contains;
    }

    public bool CheckExistencePlatform(string id)
    {
        bool contains = false;

        List<Platform> result = _service.GetPlatforms().Result;

        if (result != null)
        {
            contains = result.Any(p => p.Id == id);
        }

        return contains;
    }

    public bool CheckExistenceCompany(string id)
    {
        bool contains = false;

        List<Company> result = _service.GetCompanies().Result;

        if (result != null)
        {
            contains = result.Any(c => c.Id == id);
        }

        return contains;
    }

    public bool CheckExistenceGameMode(string id)
    {
        bool contains = false;

        List<GameMode> result = _service.GetGameModes().Result;

        if (result != null)
        {
            contains = result.Any(g => g.Id == id);
        }

        return contains;
    }

    public bool CheckExistencePlayerPerspective(string id)
    {
        bool contains = false;

        List<PlayerPerspective> result = _service.GetPlayerPerspectives().Result;

        if (result != null)
        {
            contains = result.Any(p => p.Id == id);
        }

        return contains;
    }


    public bool CheckExistenceGenre(string id)
    {
        bool contains = false;

        List<Genre> result = _service.GetGenres().Result;

        if (result != null)
        {
            contains = result.Any(g => g.Id == id);
        }

        return contains;
    }


    public bool CheckExistenceTheme(string id)
    {
        bool contains = false;

        List<Theme> result = _service.GetThemes().Result;

        if (result != null)
        {
            contains = result.Any(t => t.Id == id);
        }

        return contains;
    }
}

public class ReleaseDateValidator : AbstractValidator<ReleaseDate>
{
    private readonly IGameService _service;

    public ReleaseDateValidator(IGameService service)
    {
        _service = service;

        RuleFor(date => date.TimeStamp).NotNull().NotEmpty().WithMessage("Timestamp must not be empty!");

        RuleForEach(date => date.PlatformIds).Must(CheckExistence).Unless(date => date.PlatformIds.IsNullOrEmpty());
    }

    public bool CheckExistence(string id)
    {
        bool contains = false;

        List<Platform> result = _service.GetPlatforms().Result;

        if (result != null)
        {
            contains = result.Any(p => p.Id == id);
        }

        return contains;
    }
}

public class RatingValidator : AbstractValidator<Rating>
{
    public RatingValidator()
    {
        RuleFor(rating => rating.Author).NotNull().NotEmpty().WithMessage("Author must not be empty!");
        RuleFor(rating => rating.Author).MaximumLength(125).WithMessage("Author can not be larger than 125 characters.");
        RuleFor(rating => rating.Score).GreaterThanOrEqualTo(0).LessThanOrEqualTo(10).WithMessage("Score must be between 0 and 10.");
    }
}