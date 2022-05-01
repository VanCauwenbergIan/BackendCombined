namespace Games.Validators;

public class PlatformValidator : AbstractValidator<Platform>
{

    private readonly IGameService _service;

    public PlatformValidator(IGameService service)
    {
        _service = service;

        RuleFor(platform => platform.Name).NotNull().NotEmpty().WithMessage("Name must not be empty!");
        RuleFor(platform => platform.Name).MaximumLength(125).WithMessage("Name can not be larger than 125 characters.");

        RuleFor(platform => platform.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!");
        RuleFor(platform => platform.ManufacturerId).Must(CheckExistence).WithMessage("ManufacturerId couldn't be found!").Unless(platform => platform.ManufacturerId == null);
    }

    public PlatformValidator(IGameService service, string oldName)
    {
        _service = service;

        RuleFor(platform => platform.Name).NotNull().NotEmpty().WithMessage("Name must not be empty!");
        RuleFor(platform => platform.Name).MaximumLength(125).WithMessage("Name can not be larger than 125 characters.");

        RuleFor(platform => platform.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!").Unless(platform => platform.Name.ToLower() == oldName.ToLower());
        RuleFor(platform => platform.ManufacturerId).Must(CheckExistence).WithMessage("ManufacturerId couldn't be found!").Unless(platform => platform.ManufacturerId == null);
    }

    public bool ValidateUniqueness(string name)
    {
        bool contains = false;

        List<Platform> result = _service.GetPlatforms().Result;

        if (result != null)
        {
            contains = result.Any(p => p.Name.ToLower() == name.ToLower());
        }

        return !contains;
    }

    public bool CheckExistence(string id)
    {
        bool contains = false;

        List<Company> result = _service.GetCompanies().Result;

        if (result != null)
        {
            contains = result.Any(c => c.Id == id);
        }

        return contains;
    }
}