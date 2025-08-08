using Equatable;
using System.Text.Json.Serialization;


public class User : BaseEquatable
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? AvatarPath { get; set; }
    [JsonIgnore]
    public int Rating { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int StatusId { get; set; }
   


    public User()
    {
        Username = string.Empty;
        RegistrationDate = DateTime.UtcNow;
    }
    public User(string username): this()
    {
        Username = username;
    }
    /// <summary>
    /// Deep-copy of class user. Implements value-copying so that copied class and class created from that copied class have different references.
    /// </summary>
    /// <param name="userToCopy">Class to copy.</param>
    public User(User userToCopy)
    {
        Id = userToCopy.Id;
        Username = userToCopy.Username;
        Email = userToCopy.Email;
        PasswordHash = userToCopy.PasswordHash;
        AvatarPath = userToCopy.AvatarPath;
        Rating = userToCopy.Rating;
        RegistrationDate = userToCopy.RegistrationDate;
        StatusId = userToCopy.StatusId;
    }
    
    [JsonIgnore]
    public override List<object?> Props => [
        Id,
        Email,
        Username,
        Rating,
        StatusId,
    ];
    [JsonIgnore]
    public override bool? Stringify => true;
}
