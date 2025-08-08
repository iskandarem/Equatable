using System.Text.Json.Serialization;
using Equatable;

namespace UnitTests;

public class Town : BaseEquatable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime EstablishedDate { get; set; }
    public ICollection<User> Residents { get; set; } = new List<User>();

    public Town()
    {
        Name = string.Empty;
        EstablishedDate = DateTime.UtcNow;
    }

    public Town(string name) : this()
    {
        Name = name;
    }

    public Town(Town townToCopy)
    {
        Id = townToCopy.Id;
        Name = townToCopy.Name;
        Description = townToCopy.Description;
        EstablishedDate = townToCopy.EstablishedDate;
        Residents = new List<User>(townToCopy.Residents.Select(r => new User(r)));
    }

    [JsonIgnore]
    public override List<object?> Props => [
        Id,
        Name,
        Description,
        Residents,
    ];

    [JsonIgnore]
    public override bool? Stringify => true;
}