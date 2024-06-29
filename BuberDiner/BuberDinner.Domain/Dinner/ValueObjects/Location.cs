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

    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

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

    public Location()
    {
    }
}