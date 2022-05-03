namespace Games.Repositories;

public interface IProductRepository
{
    Task<Product> AddProduct(Product newProduct);
    Task<List<Product>> GetAllProducts();
}

public class ProductRepository : IProductRepository
{
    private readonly IMongoContext _context;

    public ProductRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Product> AddProduct(Product newProduct)
    {
        try
        {
            if (newProduct.Stock < 50 && newProduct.Price > 100)
            {
                newProduct.Promo = true;
            }

            await _context.ProductsCollection.InsertOneAsync(newProduct);
            return newProduct;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<Product>> GetAllProducts() => await _context.ProductsCollection.Find(_ => true).ToListAsync();
}