namespace AnimalManagement.Wpf.Models.Requests;

public record GetAllAnimalsRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public int Count { get; init; } = 10;

    public bool IncludeClosed { get; init; }

    public string[] Colors { get; init; } = [];

    public int? MinAge { get; init; }
    public int? MaxAge { get; init; }

    public double? MinWeight { get; init; }
    public double? MaxWeight { get; init; }

    public string? Gender { get; init; }

    public int Language { get; init; }
}