using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Dinner.ValueObjects;

public sealed class Location : ValueObject
{
    private Location(
        string name,
        string description,
        double latitude,
        double longitude)
    {
        Name = name;
        Description = description;
        Latitude = latitude;
        Longitude = longitude;
    }

    public string Name { get; }
    public string Description { get; }
    public double Latitude { get; }
    public double Longitude { get; }

    public static Location CreateNew(
        string name,
        string description,
        double latitude,
        double longitude)
    {
        return new Location(
            name,
            description,
            latitude,
            longitude);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Description;
        yield return Latitude;
        yield return Longitude;
    }
}