namespace Games.Services;

public interface IProductService
{
    Task<Product> AddProduct(Product product);
    Task<List<Product>> GetProducts();
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetProducts() => await _productRepository.GetAllProducts();

    public async Task<Product> AddProduct(Product product) => await _productRepository.AddProduct(product);
}