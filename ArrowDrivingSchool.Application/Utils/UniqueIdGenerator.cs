namespace ArrowDrivingSchool.Application.Utils;

public static class UniqueIdGenerator
{
    private static readonly Random Random = new();

    public static string Generate(string prefix = "ADS")
    {
        return $"{prefix}-{Random.Next(1000, 9999)}";
    }
}