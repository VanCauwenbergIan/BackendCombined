namespace Games.Validators;

public abstract class GenericValidator<T> : AbstractValidator<T> where T : Generic
{
    public GenericValidator()
    {
        RuleFor(generic => generic.Name).NotNull().NotEmpty().WithMessage("Name must not be empty!");
        RuleFor(generic => generic.Name).MaximumLength(125).WithMessage("Name can not be larger than 125 characters.");
    }
}

public class GenreValidator : GenericValidator<Genre>
{
    private readonly IGameService _service;

    public GenreValidator(IGameService service)
    {
        _service = service;

        RuleFor(genre => genre.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!");
    }


    public GenreValidator(IGameService service, string oldName)
    {
        _service = service;

        RuleFor(genre => genre.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!").Unless(genre => genre.Name.ToLower() == oldName.ToLower());
    }

    public bool ValidateUniqueness(string name)
    {
        bool contains = false;

        List<Genre> result = _service.GetGenres().Result;

        if (result != null)
        {
            contains = result.Any(g => g.Name.ToLower() == name.ToLower());
        }

        return !contains;
    }
}

public class GameModeValidator : GenericValidator<GameMode>
{
    private readonly IGameService _service;

    public GameModeValidator(IGameService service)
    {
        _service = service;


        RuleFor(gameMode => gameMode.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!");
    }

    public GameModeValidator(IGameService service, string oldName)
    {
        _service = service;

        RuleFor(gameMode => gameMode.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!").Unless(genre => genre.Name.ToLower() == oldName.ToLower());
    }


    public bool ValidateUniqueness(string name)
    {
        bool contains = false;

        List<GameMode> result = _service.GetGameModes().Result;

        if (result != null)
        {
            contains = result.Any(g => g.Name.ToLower() == name.ToLower());
        }

        return !contains;
    }
}

public class PlayerPerspectiveValidator : GenericValidator<PlayerPerspective>
{
    private readonly IGameService _service;

    public PlayerPerspectiveValidator(IGameService service)
    {
        _service = service;


        RuleFor(playerPerspective => playerPerspective.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!");
    }

    public PlayerPerspectiveValidator(IGameService service, string oldName)
    {
        _service = service;

        RuleFor(playerPerspective => playerPerspective.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!").Unless(genre => genre.Name.ToLower() == oldName.ToLower());
    }


    public bool ValidateUniqueness(string name)
    {
        bool contains = false;

        List<PlayerPerspective> result = _service.GetPlayerPerspectives().Result;

        if (result != null)
        {
            contains = result.Any(g => g.Name.ToLower() == name.ToLower());
        }

        return !contains;
    }
}

public class FranchiseValidator : GenericValidator<Franchise>
{
    private readonly IGameService _service;

    public FranchiseValidator(IGameService service)
    {
        _service = service;

        RuleFor(franchise => franchise.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!");
    }

    public FranchiseValidator(IGameService service, string oldName)
    {
        _service = service;

        RuleFor(franchise => franchise.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!").Unless(genre => genre.Name.ToLower() == oldName.ToLower());
    }


    public bool ValidateUniqueness(string name)
    {
        bool contains = false;

        List<Franchise> result = _service.GetFranchises().Result;

        if (result != null)
        {
            contains = result.Any(g => g.Name.ToLower() == name.ToLower());
        }

        return !contains;
    }
}

public class ThemeValidator : GenericValidator<Theme>
{
    private readonly IGameService _service;

    public ThemeValidator(IGameService service)
    {
        _service = service;

        RuleFor(theme => theme.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!");
    }

    public ThemeValidator(IGameService service, string oldName)
    {
        _service = service;

        RuleFor(theme => theme.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!").Unless(genre => genre.Name.ToLower() == oldName.ToLower());
    }


    public bool ValidateUniqueness(string name)
    {
        bool contains = false;

        List<Theme> result = _service.GetThemes().Result;

        if (result != null)
        {
            contains = result.Any(g => g.Name.ToLower() == name.ToLower());
        }

        return !contains;
    }
}

