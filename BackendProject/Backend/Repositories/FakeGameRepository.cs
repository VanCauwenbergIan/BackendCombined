namespace Games.Repositories;

public class FakeGameReposiory : IGameRepository
{
    public static List<Game> _games = new List<Game>();
    public Task<Game> AddGame(Game newGame)
    {
        newGame.Id = Guid.NewGuid().ToString();
        newGame.CreatedOn = DateTime.Now;

        _games.Add(newGame);
        return Task.FromResult(newGame);
    }

    public Task<List<Game>> AddGames(List<Game> newGames)
    {
        foreach (Game g in newGames)
        {
            g.Id = Guid.NewGuid().ToString();
            g.CreatedOn = DateTime.Now;
        }

        _games.AddRange(newGames);
        return Task.FromResult(newGames);
    }

    public Task<List<Rating>> AddRating(string id, Rating newRating)
    {
        try
        {
            int i = _games.FindIndex(g => g.Id == id);
            Game game = _games[i];
            newRating.Id = Guid.NewGuid().ToString();
            if (game.Ratings != null)
            {
                game.Ratings.Add(newRating);
            }
            else
            {
                game.Ratings = new List<Rating>() { newRating };
            }
            _games[i] = game;

            return Task.FromResult(game.Ratings);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<List<ReleaseDate>> AddReleaseDate(string id, ReleaseDate newReleaseDate)
    {
        try
        {
            int i = _games.FindIndex(g => g.Id == id);
            Game game = _games[i];
            newReleaseDate.Id = Guid.NewGuid().ToString();
            if (game.ReleaseDates != null)
            {
                game.ReleaseDates.Add(newReleaseDate);
            }
            else
            {
                game.ReleaseDates = new List<ReleaseDate>() { newReleaseDate };
            }
            _games[i] = game;

            return Task.FromResult(game.ReleaseDates);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<List<Game>> GetAllGames() => Task.FromResult(_games);

    public Task<Game> GetGame(string id) => Task.FromResult(_games.Where(g => g.Id == id).Single());

    public Task<List<Rating>> RemoveRating(string gameId, string ratingId)
    {
        try
        {
            int i = _games.FindIndex(g => g.Id == gameId);
            Game game = _games[i];
            if (game.Ratings != null)
            {
                var rating = game.Ratings.SingleOrDefault<Rating>(r => r.Id == ratingId);
                if (rating != null)
                {
                    game.Ratings.Remove(rating);
                }
                _games[i] = game;
            }
            return Task.FromResult(game.Ratings);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<List<ReleaseDate>> RemoveReleaseDate(string gameId, string releaseDateId)
    {
        try
        {
            int i = _games.FindIndex(g => g.Id == gameId);
            Game game = _games[i];
            if (game.ReleaseDates != null)
            {
                var release = game.ReleaseDates.SingleOrDefault<ReleaseDate>(r => r.Id == releaseDateId);
                if (release != null)
                {
                    game.ReleaseDates.Remove(release);
                }
                _games[i] = game;
            }
            return Task.FromResult(game.ReleaseDates);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<Game> UpdateGame(string id, Game game)
    {
        try
        {
            int i = _games.FindIndex(g => g.Id == id);
            var item = _games[i];
            if (item != null)
            {
                item.Name = game.Name;
                item.DeveloperId = game.DeveloperId;
                item.PublisherId = game.PublisherId;
                item.CoverUrl = game.CoverUrl;
                item.FranchiseId = game.FranchiseId;
                item.GameModeIds = game.GameModeIds;
                item.GenreIds = game.GenreIds;
                item.PlatformIds = game.PlatformIds;
                item.PlayerPerspectiveIds = game.PlayerPerspectiveIds;
                item.Ratings = game.Ratings;
                item.ReleaseDates = game.ReleaseDates;
                item.ThemeIds = game.ThemeIds;
                _games[i] = item;
            }
            return Task.FromResult(item);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<List<Rating>> UpdateRating(string gameId, string ratingId, Rating rating)
    {
        try
        {
            int i = _games.FindIndex(g => g.Id == gameId);
            Game game = _games[i];
            if (game.Ratings != null)
            {
                var indexRating = game.Ratings.FindIndex(r => r.Id == ratingId);
                if (indexRating >= 0)
                {
                    game.Ratings[i] = rating;
                }

                _games[i] = game;
            }
            return Task.FromResult(game.Ratings);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<List<ReleaseDate>> UpdateReleaseDate(string gameId, string releaseDateId, ReleaseDate releaseDate)
    {
        try
        {
            int i = _games.FindIndex(g => g.Id == gameId);
            Game game = _games[i];
            if (game.ReleaseDates != null)
            {
                var indexRating = game.ReleaseDates.FindIndex(r => r.Id == releaseDateId);
                if (indexRating >= 0)
                {
                    game.ReleaseDates[i] = releaseDate;
                }

                _games[i] = game;
            }
            return Task.FromResult(game.ReleaseDates);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}