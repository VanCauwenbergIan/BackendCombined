namespace Games.Repositories;

public interface ICompanyRepository
{
    Task<List<Company>> AddCompanies(List<Company> newCompanies);
    Task<List<Company>> GetAllCompanies();
    Task<Company> GetCompany(string id);
    Task<Company> UpdateCompany(string id, Company company);
    Task<Company> UpdateCompanyRating(string id, double newValue);
}

public class CompanyRepository : ICompanyRepository
{
    private readonly IMongoContext _context;

    public CompanyRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Company>> AddCompanies(List<Company> newCompanies)
    {
        try
        {
            newCompanies.ForEach(company => company.CreatedOn = DateTime.Now);
            await _context.CompaniesCollection.InsertManyAsync(newCompanies);
            return newCompanies;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Company> UpdateCompany(string id, Company company)
    {
        try
        {
            await _context.CompaniesCollection.ReplaceOneAsync(c => c.Id == id, company);
            return company;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Company> GetCompany(string id) => await _context.CompaniesCollection.Find<Company>(c => c.Id == id).FirstOrDefaultAsync();

    public async Task<List<Company>> GetAllCompanies() => await _context.CompaniesCollection.Find(_ => true).ToListAsync();

    public async Task<Company> UpdateCompanyRating(string id, double newValue)
    {
        Company company = await GetCompany(id);
        company.AverageRating = newValue;
        await UpdateCompany(company.Id, company);
        return company;
    }
}