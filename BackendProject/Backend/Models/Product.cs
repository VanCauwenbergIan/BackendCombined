namespace Games.Models;

public class Product {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id {get; set;}
    public string Name {get; set;}
    public double Price {get; set;}
    public int Stock {get; set;}
    public bool? Promo {get; set;}
}