namespace Games.Models;

public class Generic
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedOn { get; set; }
}

public class Genre : Generic { }

public class GameMode : Generic { }

public class PlayerPerspective : Generic { }

public class Franchise : Generic { }

public class Theme : Generic { }
