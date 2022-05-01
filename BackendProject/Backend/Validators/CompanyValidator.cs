namespace Games.Validators;

public class CompanyValidator : AbstractValidator<Company>
{

    private readonly IGameService _service;

    public CompanyValidator(IGameService service)
    {
        _service = service;

        RuleFor(company => company.Name).NotNull().NotEmpty().WithMessage("Name must not be empty!");
        RuleFor(company => company.Name).MaximumLength(125).WithMessage("Name can not be larger than 125 characters.");
        RuleFor(company => company.Type).NotNull().NotEmpty().WithMessage("Type must not be empty!");
        RuleFor(company => company.Type).MaximumLength(125).WithMessage("Type can not be larger than 125 characters.");
        RuleFor(company => company.Country).NotNull().NotEmpty().WithMessage("Country must not be empty!");
        RuleFor(company => company.Country).MaximumLength(125).WithMessage("Country can not be larger than 125 characters.");

        RuleFor(company => company.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!");
        RuleFor(company => company.ParentCompanyId).Must(CheckExistence).WithMessage("ParentCompanyId couldn't be found!").Unless(company => company.ParentCompanyId == null);
        RuleForEach(company => company.SubcompanyIds).Must(CheckExistence).WithMessage("One or more SubCompanyIds couldn't be found!").Unless(company => company.SubcompanyIds.IsNullOrEmpty());
    }

    public CompanyValidator(IGameService service, string oldName)
    {
        _service = service;

        RuleFor(company => company.Name).NotNull().NotEmpty().WithMessage("Name must not be empty!");
        RuleFor(company => company.Name).MaximumLength(125).WithMessage("Name can not be larger than 125 characters.");
        RuleFor(company => company.Type).NotNull().NotEmpty().WithMessage("Type must not be empty!");
        RuleFor(company => company.Type).MaximumLength(125).WithMessage("Type can not be larger than 125 characters.");
        RuleFor(company => company.Country).NotNull().NotEmpty().WithMessage("Country must not be empty!");
        RuleFor(company => company.Country).MaximumLength(125).WithMessage("Country can not be larger than 125 characters.");

        RuleFor(company => company.Name).Must(ValidateUniqueness).WithMessage("Name must be unique!").Unless(company => company.Name.ToLower() == oldName.ToLower());
        RuleFor(company => company.ParentCompanyId).Must(CheckExistence).WithMessage("ParentCompanyId couldn't be found!").Unless(company => company.ParentCompanyId == null);
        RuleForEach(company => company.SubcompanyIds).Must(CheckExistence).WithMessage("One or more SubCompanyIds couldn't be found!").Unless(company => company.SubcompanyIds.IsNullOrEmpty());
    }

    public bool ValidateUniqueness(string name)
    {
        bool contains = false;

        List<Company> result = _service.GetCompanies().Result;

        if (result != null)
        {
            contains = result.Any(c => c.Name.ToLower() == name.ToLower());
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