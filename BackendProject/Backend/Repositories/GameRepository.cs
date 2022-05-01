namespace Games.Repositories;

public interface IGameRepository
{
    Task<Game> AddGame(Game newGame);
    Task<List<Game>> AddGames(List<Game> newGames);
    Task<List<Rating>> AddRating(string id, Rating newRating);
    Task<List<ReleaseDate>> AddReleaseDate(string id, ReleaseDate newReleaseDate);
    Task<List<Game>> GetAllGames();
    Task<Game> GetGame(string id);
    Task<List<Rating>> RemoveRating(string gameId, string ratingId);
    Task<List<ReleaseDate>> RemoveReleaseDate(string gameId, string releaseDateId);
    Task<Game> UpdateGame(string id, Game game);
    Task<List<Rating>> UpdateRating(string gameId, string ratingId, Rating rating);
    Task<List<ReleaseDate>> UpdateReleaseDate(string gameId, string releaseDateId, ReleaseDate releaseDate);
}

public class GameRepository : IGameRepository
{
    private readonly IMongoContext _context;

    public GameRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Game> AddGame(Game newGame)
    {
        try
        {
            newGame.CreatedOn = DateTime.Now;
            await _context.GamesCollection.InsertOneAsync(newGame);
            return newGame;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<Game>> AddGames(List<Game> newGames)
    {
        try
        {
            newGames.ForEach(game => game.CreatedOn = DateTime.Now);
            await _context.GamesCollection.InsertManyAsync(newGames);
            return newGames;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Game> UpdateGame(string id, Game game)
    {
        try
        {
            await _context.GamesCollection.ReplaceOneAsync(g => g.Id == id, game);
            return game;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Game> GetGame(string id) => await _context.GamesCollection.Find<Game>(g => g.Id == id).FirstOrDefaultAsync();

    public async Task<List<Game>> GetAllGames() => await _context.GamesCollection.Find(_ => true).ToListAsync();

    public async Task<List<Rating>> AddRating(string id, Rating newRating)
    {
        try
        {
            Game game = await GetGame(id);
            newRating.Id = Guid.NewGuid().ToString();
            if (game.Ratings != null)
            {
                game.Ratings.Add(newRating);
            }
            else
            {
                game.Ratings = new List<Rating>() { newRating };
            }

            await UpdateGame(id, game);
            return game.Ratings;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<Rating>> UpdateRating(string gameId, string ratingId, Rating rating)
    {
        try
        {
            Game game = await GetGame(gameId);
            if (game.Ratings != null)
            {
                var i = game.Ratings.FindIndex(r => r.Id == ratingId);
                if (i >= 0)
                {
                    game.Ratings[i] = rating;
                }
                await UpdateGame(gameId, game);
            }
            return game.Ratings;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<Rating>> RemoveRating(string gameId, string ratingId)
    {
        try
        {
            Game game = await GetGame(gameId);
            if (game.Ratings != null)
            {
                var rating = game.Ratings.SingleOrDefault<Rating>(r => r.Id == ratingId);
                if (rating != null)
                {
                    game.Ratings.Remove(rating);
                }
                await UpdateGame(gameId, game);
            }
            return game.Ratings;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<ReleaseDate>> AddReleaseDate(string id, ReleaseDate newReleaseDate)
    {
        try
        {
            Game game = await GetGame(id);
            newReleaseDate.Id = Guid.NewGuid().ToString();
            if (game.ReleaseDates != null)
            {
                game.ReleaseDates.Add(newReleaseDate);
            }
            else
            {
                game.ReleaseDates = new List<ReleaseDate>() { newReleaseDate };
            }
            await UpdateGame(id, game);
            return game.ReleaseDates;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<ReleaseDate>> UpdateReleaseDate(string gameId, string releaseDateId, ReleaseDate releaseDate)
    {
        try
        {
            Game game = await GetGame(gameId);
            if (game.ReleaseDates != null)
            {
                var i = game.ReleaseDates.FindIndex(r => r.Id == releaseDateId);
                if (i >= 0)
                {
                    game.ReleaseDates[i] = releaseDate;
                }

                await UpdateGame(gameId, game);
            }
            return game.ReleaseDates;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<ReleaseDate>> RemoveReleaseDate(string gameId, string releaseDateId)
    {
        try
        {
            Game game = await GetGame(gameId);
            if (game.ReleaseDates != null)
            {
                var release = game.ReleaseDates.SingleOrDefault<ReleaseDate>(r => r.Id == releaseDateId);
                if (release != null)
                {
                    game.ReleaseDates.Remove(release);
                }
                await UpdateGame(gameId, game);
            }
            return game.ReleaseDates;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}