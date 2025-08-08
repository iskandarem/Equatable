using Xunit;
using AutoFixture;
using System.Text.Json;

namespace UnitTests;

public class Main
{
    private readonly Fixture _fixture;
    public Main()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void SimpleDataTest()
    {
        var user = _fixture.Create<User>();
        var user2 = _fixture.Create<User>();
        Assert.NotEqual(user, user2);
        var user3 = new User(user); // Deep copy
        Assert.Equal(user, user3); // Should be equal because we copied the properties
        Assert.NotSame(user, user3); // Should not be the same reference
        Assert.Equal(user.GetHashCode(), user3.GetHashCode());
    }

    [Fact]
    public void EmptyUserTest()
    {
        var user = new User();
        var user2 = new User();
        Assert.Equal(user, user2); // Should be equal because both are empty
        Assert.NotNull(user);
        Assert.Equal(string.Empty, user.Username);
        Assert.Equal(DateTime.UtcNow.Date, user.RegistrationDate.Date); // Check only the date part
        Assert.Equal(0, user.Id);
        Assert.Equal(0, user.Rating);
    }

    [Fact]
    public void StringifyTest()
    {
        var user = _fixture.Create<User>();
        var userString = user.ToString();
        Assert.NotNull(userString);
        Assert.Contains(user.Username, userString);
        Assert.Contains(user.Email ?? string.Empty, userString);
        Assert.DoesNotContain(user.PasswordHash ?? string.Empty, userString); // Password should not be included
    }

    [Fact]
    public void GetHashCodeTest()
    {
        var user = _fixture.Create<User>();
        var user2 = new User(user);
        Assert.Equal(user.GetHashCode(), user2.GetHashCode());
        var user3 = _fixture.Create<User>();
        Assert.NotEqual(user.GetHashCode(), user3.GetHashCode());
    }

    [Fact]
    public void TownTest()
    {
        var town = _fixture.Create<Town>();
        var town2 = _fixture.Create<Town>();
        Assert.NotEqual(town, town2);
        var town3 = new Town(town); // Deep copy
        Assert.Equal(town, town3); // Should be equal because we copied the properties
        Assert.NotSame(town, town3); // Should not be the same reference
        // Assert.Equal(town.GetHashCode(), town3.GetHashCode());
        Assert.NotNull(town.Residents);
        Assert.NotEmpty(town.Residents);
        Assert.All(town.Residents, r => Assert.NotNull(r));
        Assert.All(town.Residents, r => Assert.NotEqual(0, r.Id));
        Assert.All(town.Residents, r => Assert.NotEqual(string.Empty, r.Username));
        Assert.All(town.Residents, r => Assert.NotEqual(DateTime.MinValue, r.RegistrationDate));
    }
    [Fact]
    public void EmptyTownTest()
    {
        var town = new Town();
        var town2 = new Town();
        System.Console.WriteLine($"Town: {JsonSerializer.Serialize(town)}, \nTown2: {JsonSerializer.Serialize(town2)}");
        Assert.Equal(town, town2); // Should be equal because both are empty
        Assert.NotNull(town);
        Assert.Equal(string.Empty, town.Name);
        Assert.Equal(DateTime.UtcNow.Date, town.EstablishedDate.Date); // Check only the date part
        Assert.Equal(0, town.Id);
        Assert.Empty(town.Residents);
        var town3 = new Town(town); // Deep copy
        var residents = _fixture.CreateMany<User>(5).ToList();
        town3.Residents = residents;
        Assert.NotEqual(town, town3); // Should not be equal because residents are different
        Assert.NotSame(town, town3); // Should not be the same reference
        Assert.NotEqual(town.GetHashCode(), town3.GetHashCode());
    }   

}
