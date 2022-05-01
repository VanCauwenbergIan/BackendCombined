namespace Testing;

public class IntegrationTests
{
    // test generic GET
    [Fact]
    public async Task Should_Return_Genres()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        await client.GetAsync("/setup");

        var result = await client.GetAsync("/genres");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var genres = await result.Content.ReadFromJsonAsync<List<Genre>>();
        Assert.NotNull(genres);
        Assert.True(genres.Count > 0);
    }

    // test generic POST
    [Fact]
    public async Task Should_Return_GenresPost()
    {
        List<Genre> testGenres = new List<Genre>() { new Genre() { Name = "test1" }, new Genre() { Name = "test2" } };

        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var resultPost = await client.PostAsJsonAsync<List<Genre>>("/genres", testGenres);
        resultPost.StatusCode.Should().Be(HttpStatusCode.Created);
        var genres = await resultPost.Content.ReadFromJsonAsync<List<Genre>>();
        Assert.NotNull(resultPost.Headers.Location);
        Assert.True(genres.Count == 2);
    }

    // test validation name
    [Fact]
    public async Task Should_Return_BadRequestGenreNameEmpty()
    {
        // empty
        List<Genre> testGenres = new List<Genre>() { new Genre() { Name = "" }, new Genre() { Name = "test2" } };

        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync<List<Genre>>("/genres", testGenres);
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var message = await result.Content.ReadFromJsonAsync<List<Error>>();
        Assert.True(message[0].errors == "Name must not be empty!");

        // too long
        testGenres = new List<Genre>() { new Genre() { Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" } };

        result = await client.PostAsJsonAsync<List<Genre>>("/genres", testGenres);
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        message = await result.Content.ReadFromJsonAsync<List<Error>>();
        Assert.True(message[0].errors == "Name can not be larger than 125 characters.");
    }

    // test validation name
    [Fact]
    public async Task Should_Return_BadRequestGenreNameNotUnique()
    {
        // already added before
        List<Genre> testGenres1 = new List<Genre>() { new Genre() { Name = "test1" } };
        // here we use two genres with the same name, which should give an error
        List<Genre> testGenres2 = new List<Genre>() { new Genre() { Name = "test3" }, new Genre() { Name = "test3" } };

        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var result1 = await client.PostAsJsonAsync<List<Genre>>("/genres", testGenres1);
        result1.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var message1 = await result1.Content.ReadFromJsonAsync<List<Error>>();
        Assert.True(message1[0].errors == "Name must be unique!");

        var result2 = await client.PostAsJsonAsync<List<Genre>>("/genres", testGenres2);
        result2.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var message2 = await result2.Content.ReadFromJsonAsync<List<Error>>();
        Assert.True(message2[0].errors == "Name must be unique!");
    }

    [Fact]
    public async Task Should_Return_GenrePost()
    {
        List<Genre> testGenres = new List<Genre>() { new Genre() { Name = "test3" } };

        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Accept.Clear();
        var resultPost = await client.PostAsJsonAsync<List<Genre>>("/genres", testGenres);
        resultPost.StatusCode.Should().Be(HttpStatusCode.Created);
        var message = await resultPost.Content.ReadFromJsonAsync<List<Genre>>(); ;
        var genre = message[0];

        Assert.True(genre.Name == "test3");
        genre.Name = "new game";
        var resultPut = await client.PutAsJsonAsync<Genre>($"/genres/{genre.Id}", genre);
        resultPut.StatusCode.Should().Be(HttpStatusCode.OK);
        genre = await resultPut.Content.ReadFromJsonAsync<Genre>();
        Assert.NotNull(genre);
    }

    // same for other generics (gamemode, playerpespective, franchise, theme), so I won't test them all here

    // test platform single GET + GET all
    [Fact]
    public async Task Should_Return_Platforms()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        // GET multiple
        var result = await client.GetAsync("/platforms");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var platforms = await result.Content.ReadFromJsonAsync<List<Platform>>();
        Assert.NotNull(platforms);
        Assert.True(platforms.Count > 0);

        // GET single
        var platform = platforms[0];
        var resultSingle = await client.GetAsync($"/platforms/{platform.Id}");
        resultSingle.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // test platform POST
    [Fact]
    public async Task Should_Return_PlatformsPost()
    {
        List<Platform> testPlatforms = new List<Platform>() { new Platform() { Name = "nintendo wii u2" }, new Platform() { Name = "pc 2" } };

        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var resultPost = await client.PostAsJsonAsync<List<Platform>>("/platforms", testPlatforms);
        resultPost.StatusCode.Should().Be(HttpStatusCode.Created);
        var platforms = await resultPost.Content.ReadFromJsonAsync<List<Platform>>();
        Assert.NotNull(resultPost.Headers.Location);
        Assert.True(platforms.Count == 2);
    }

    // test platform validation ManufacturerId
    [Fact]
    public async Task Should_Return_BadRequestManufacturerId()
    {
        List<Platform> testPlatforms = new List<Platform>() { new Platform() { Name = "wii u2", ManufacturerId = "123456" }, new Platform() { Name = "pc 2" } };

        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync<List<Platform>>("/platforms", testPlatforms);
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var message = await result.Content.ReadFromJsonAsync<List<Error>>();
        Assert.True(message[0].errors == "ManufacturerId couldn't be found!");
    }

    // test company single GET + GET all
    [Fact]
    public async Task Should_Return_Companies()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        // GET multiple
        var result = await client.GetAsync("/companies");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var companies = await result.Content.ReadFromJsonAsync<List<Company>>();
        Assert.NotNull(companies);
        Assert.True(companies.Count > 0);

        // GET single
        var company = companies[0];
        var resultSingle = await client.GetAsync($"/companies/{company.Id}");
        resultSingle.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // test game POST + companies POST
    [Fact]
    public async Task Should_Return_GamesPost()
    {
        Game testGame = new Game() { Name = "Crusader Kings III" };

        List<Company> testCompanies = new List<Company>() { new Company() { Name = "Paradox Interactive", Type = "publisher", Country = "Sweden" }, new Company() { Name = "Paradox Development Studio", Type = "developer", Country = "Sweden" } };

        var application = Helper.CreateApi();
        var client = application.CreateClient();

        var resultCompanies = await client.PostAsJsonAsync<List<Company>>("/companies", testCompanies);
        var companies = await resultCompanies.Content.ReadFromJsonAsync<List<Company>>();
        Assert.True(companies.Count() > 0);

        var dev = companies[0];
        var pub = companies[1];

        testGame.PublisherId = dev.Id;
        testGame.DeveloperId = pub.Id;

        var resultPost = await client.PostAsJsonAsync<Game>("/games", testGame);
        resultPost.StatusCode.Should().Be(HttpStatusCode.Created);
        var game = await resultPost.Content.ReadFromJsonAsync<Game>();
        Assert.NotNull(resultPost.Headers.Location);
        Assert.NotNull(game);
    }

    // test game single GET + GET all
    [Fact]
    public async Task Should_Return_Games()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        // GET multiple
        var result = await client.GetAsync("/games");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var games = await result.Content.ReadFromJsonAsync<List<Game>>();
        Assert.NotNull(games);
        Assert.True(games.Count > 0);

        // GET single
        var game = games[0];
        var resultSingle = await client.GetAsync($"/games/{game.Id}");
        resultSingle.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}