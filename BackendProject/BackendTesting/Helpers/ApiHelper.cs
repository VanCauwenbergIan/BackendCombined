public class Helper
{
    public static IGameService CreateGameService()
    {
        return CreateApi().Services.GetService<IGameService>();
    }

    public static WebApplicationFactory<Program> CreateApi()
    {
        var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IGenreRepository)));
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IPlatformRepository)));
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(ICompanyRepository)));
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IGameRepository)));
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IGameService)));

                var fakeGenreRepository = new ServiceDescriptor(typeof(IGenreRepository), typeof(FakeGenreRepository), ServiceLifetime.Transient);
                services.Add(fakeGenreRepository);

                var fakePlatformRepository = new ServiceDescriptor(typeof(IPlatformRepository), typeof(FakePlatformRepository), ServiceLifetime.Transient);
                services.Add(fakePlatformRepository);

                var fakeCompanyRepository = new ServiceDescriptor(typeof(ICompanyRepository), typeof(FakeCompanyRepository), ServiceLifetime.Transient);
                services.Add(fakeCompanyRepository);

                var fakeGameRepository = new ServiceDescriptor(typeof(IGameRepository), typeof(FakeGameReposiory), ServiceLifetime.Transient);
                services.Add(fakeGameRepository);

                var fakeGameService = new ServiceDescriptor(typeof(IGameService), typeof(FakeGameService), ServiceLifetime.Transient);
                services.Add(fakeGameService);
            });
        });


        return application;

    }
}