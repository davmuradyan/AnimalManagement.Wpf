namespace AnimalManagement.Wpf.Models.Requests;

public record GetAllAnimalsRequest
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int Count { get; init; }
    public bool IncludeClosed { get; init; }
    public int[] Colors { get; init; } = [];
    public int? MinAge { get; init; }
    public int? MaxAge { get; init; }
    public double? MinWeight { get; init; }
    public double? MaxWeight { get; init; }
    public int? Gender { get; init; }
}