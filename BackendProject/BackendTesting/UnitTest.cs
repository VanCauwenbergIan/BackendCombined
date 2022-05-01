using Xunit;

namespace BackendTesting;

public class UnitTest
{
    // TODO: add averageRating to each game and change it each time a rating gets added
    [Fact]
    public async void Add_Rating()
    {
        var gameService = Helper.CreateGameService();

        List<Company> newCompanies = await gameService.AddCompanies(new List<Company>() { new Company() { Name = "Warhorse Studios", Type = "developer", Country = "Czechia" }, new Company() { Name = "Deep Silver", Type = "publisher", Country = "Germany" } });
        Assert.NotEmpty(newCompanies);
        Assert.True(newCompanies.Count == 2);

        Company dev = newCompanies.Where(c => c.Name == "Warhorse Studios").Single();
        Company pub = newCompanies.Where(c => c.Name == "Deep Silver").Single();
        Assert.Null(dev.AverageRating);
        Assert.Null(pub.AverageRating);

        Game newGame = new Game() { Name = "Kingdom Come: Deliverance" };
        newGame.DeveloperId = dev.Id;
        newGame.PublisherId = pub.Id;

        Game game = await gameService.AddGame(newGame);
        Assert.NotNull(game);

        Rating newRating = new Rating() { Author = "some_dude", Score = 8, Comment = "Still waiting on the sequel warhorse!" };

        List<Rating> ratings = await gameService.AddRating(game.Id, newRating);
        Assert.True(ratings.Count() == 1);

        List<Company> checkCompanies = await gameService.GetCompanies();
        Company checkDev = checkCompanies.Where(c => c.Name == "Warhorse Studios").Single();
        Company checkPub = checkCompanies.Where(c => c.Name == "Deep Silver").Single();
        Assert.True(checkDev.AverageRating == 80 && checkPub.AverageRating == 80);
    }

    
}