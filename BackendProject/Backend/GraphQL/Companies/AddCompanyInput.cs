namespace Games.GraphQL;

public record AddCompanyInput(string Name, string Type, string Country, string? ParentCompanyId, List<string>? subCompanyIds);