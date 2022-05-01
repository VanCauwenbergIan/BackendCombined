namespace Games.Repositories;

public class FakeCompanyRepository : ICompanyRepository
{
    public static List<Company> _companies = new List<Company>();
    public Task<List<Company>> AddCompanies(List<Company> newCompanies)
    {
        foreach (Company c in newCompanies)
        {
            c.Id = Guid.NewGuid().ToString();
            c.CreatedOn = DateTime.Now;
        }

        _companies.AddRange(newCompanies);
        return Task.FromResult(newCompanies);
    }

    public Task<List<Company>> GetAllCompanies() => Task.FromResult(_companies);

    public Task<Company> GetCompany(string id) => Task.FromResult(_companies.Where(c => c.Id == id).Single());

    public Task<Company> UpdateCompany(string id, Company company)
    {
        try
        {
            int i = _companies.FindIndex(c => c.Id == id);
            var item = _companies[i];
            if (item != null)
            {
                item.Name = company.Name;
                item.Type = company.Type;
                item.Country = company.Country;
                item.ParentCompanyId = company.ParentCompanyId;
                item.SubcompanyIds = company.SubcompanyIds;
                _companies[i] = item;
            }
            return Task.FromResult(item);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<Company> UpdateCompanyRating(string id, double newValue)
    {
        Company company = _companies.Where(c => c.Id == id).Single();
        company.AverageRating = newValue;
        UpdateCompany(company.Id, company);
        return Task.FromResult(company);
    }
}
