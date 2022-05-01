namespace Games.Models;

public class Company : Generic
{
    public string Type { get; set; }
    public double? AverageRating { get; set; }
    public string Country { get; set; }
    public string? ParentCompanyId { get; set; }
    public List<string>? SubcompanyIds { get; set; }
}