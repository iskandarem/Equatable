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

    [Fact]
    public void Equality_ShouldBeReflexive()
    {
        var user = _fixture.Create<User>();
        Assert.True(user.Equals(user));
    }

    [Fact]
    public void Equality_ShouldBeSymmetric()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);

        Assert.True(user1.Equals(user2));
        Assert.True(user2.Equals(user1));
    }

    [Fact]
    public void Equality_ShouldBeTransitive()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);
        var user3 = new User(user2);

        Assert.True(user1.Equals(user2));
        Assert.True(user2.Equals(user3));
        Assert.True(user1.Equals(user3));
    }

    [Fact]
    public void Equality_ShouldBeConsistent()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);

        for (int i = 0; i < 50; i++)
            Assert.True(user1.Equals(user2));
    }

    [Fact]
    public void Equality_WithNull_ShouldReturnFalse()
    {
        var user = _fixture.Create<User>();
        Assert.False(user.Equals(null));
    }

    [Fact]
    public void Equality_WithDifferentType_ShouldReturnFalse()
    {
        var user = _fixture.Create<User>();
        Assert.False(user.Equals("test"));
    }

    [Fact]
    public void OperatorEquality_ShouldWork()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);

        Assert.True(user1.Equals(user2));
        Assert.False(user1 == user2);
    }

    [Fact]
    public void OperatorEquality_BothNull_ShouldBeTrue()
    {
        User a = null;
        User b = null;

        Assert.True(a == b);
    }

    [Fact]
    public void OperatorEquality_OneNull_ShouldBeFalse()
    {
        var user = _fixture.Create<User>();
        User nullUser = null;

        Assert.False(user == nullUser);
        Assert.True(user != nullUser);
    }

    [Fact]
    public void HashCode_ShouldMatch_ForEqualObjects()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);

        Assert.Equal(user1.GetHashCode(), user2.GetHashCode());
    }

    [Fact]
    public void HashCode_ShouldDiffer_ForDifferentObjects()
    {
        var user1 = _fixture.Create<User>();
        var user2 = _fixture.Create<User>();

        Assert.NotEqual(user1.GetHashCode(), user2.GetHashCode());
    }

    [Fact]
    public void HashCode_ShouldBeConsistent()
    {
        var user = _fixture.Create<User>();

        var hash1 = user.GetHashCode();
        var hash2 = user.GetHashCode();

        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void Equality_ShouldFail_WhenIdDiffers()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);

        user2.Id++;

        Assert.NotEqual(user1, user2);
    }

    [Fact]
    public void Equality_ShouldFail_WhenUsernameDiffers()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);

        user2.Username += "_diff";

        Assert.NotEqual(user1, user2);
    }

    [Fact]
    public void Equality_ShouldFail_WhenEmailDiffers()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);

        user2.Email = "different@email.com";

        Assert.NotEqual(user1, user2);
    }

    [Fact]
    public void Equality_ShouldFail_WhenRatingDiffers()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);

        user2.Rating++;

        Assert.NotEqual(user1, user2);
    }

    [Fact]
    public void Equality_ShouldHandleNullProperties()
    {
        var user1 = _fixture.Create<User>();
        user1.Email = null;

        var user2 = new User(user1);

        Assert.Equal(user1, user2);
    }

    [Fact]
    public void ObjectEquals_ShouldWork()
    {
        object user1 = _fixture.Create<User>();
        object user2 = new User((User)user1);

        Assert.True(user1.Equals(user2));
    }

    [Fact]
    public void DeepCopy_ShouldNotBeSameReference()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);

        Assert.NotSame(user1, user2);
    }

    [Fact]
    public void DeepCopy_ShouldRemainEqual()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);

        Assert.Equal(user1, user2);
    }

    [Fact]
    public void ToString_ShouldContainExpectedValues()
    {
        var user = _fixture.Create<User>();
        var str = user.ToString();

        Assert.Contains(user.Username, str);
        Assert.Contains(user.Email ?? string.Empty, str);
    }

    [Fact]
    public void ToString_ShouldNotContainPassword()
    {
        var user = _fixture.Create<User>();
        var str = user.ToString();

        Assert.DoesNotContain(user.PasswordHash ?? string.Empty, str);
    }

    [Fact]
    public void ToString_ShouldBeStable()
    {
        var user = _fixture.Create<User>();

        var s1 = user.ToString();
        var s2 = user.ToString();

        Assert.Equal(s1, s2);
    }

    [Fact]
    public void Dictionary_ShouldRecognizeEqualKey()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);

        var dict = new Dictionary<User, string>();
        dict[user1] = "value";

        Assert.True(dict.ContainsKey(user2));
    }

    [Fact]
    public void HashSet_ShouldRecognizeEqualObject()
    {
        var user1 = _fixture.Create<User>();
        var user2 = new User(user1);

        var set = new HashSet<User>();
        set.Add(user1);

        Assert.Contains(user2, set);
    }

    [Fact]
    public void Serialization_ShouldPreserveEquality()
    {
        var user = _fixture.Create<User>();

        var json = JsonSerializer.Serialize(user);
        var deserialized = JsonSerializer.Deserialize<User>(json);

        Assert.Equal(user, deserialized);
    }

    [Fact]
    public void Town_DeepCopy_ShouldNotShareCollectionReference()
    {
        var town1 = _fixture.Create<Town>();
        var town2 = new Town(town1);

        Assert.NotSame(town1.Residents, town2.Residents);
    }

    [Fact]
    public void Town_DeepCopy_ShouldNotShareResidentInstances()
    {
        var town1 = _fixture.Create<Town>();
        var town2 = new Town(town1);

        for (int i = 0; i < town1.Residents.Count; i++)
            Assert.NotSame(town1.Residents.ElementAt(i), town2.Residents.ElementAt(i));
    }

    [Fact]
    public void Town_ShouldBeEqual_WhenResidentsEqual()
    {
        var town1 = _fixture.Create<Town>();
        var town2 = new Town(town1);

        Assert.Equal(town1, town2);
    }

    [Fact]
    public void Town_ShouldNotBeEqual_WhenResidentModified()
    {
        var town1 = _fixture.Create<Town>();
        var town2 = new Town(town1);

        town2.Residents.ElementAt(0).Username += "changed";

        Assert.NotEqual(town1, town2);
    }

    [Fact]
    public void Town_ShouldNotBeEqual_WhenResidentsAdded()
    {
        var town1 = _fixture.Create<Town>();
        var town2 = new Town(town1);

        town2.Residents.Add(_fixture.Create<User>());

        Assert.NotEqual(town1, town2);
    }

    [Fact]
    public void Town_ShouldNotBeEqual_WhenResidentsRemoved()
    {
        var town1 = _fixture.Create<Town>();
        var town2 = new Town(town1);

        town2.Residents.Remove(town2.Residents.First());

        Assert.NotEqual(town1, town2);
    }

    [Fact]
    public void Town_HashCode_ShouldMatch_WhenEqual()
    {
        var town1 = _fixture.Create<Town>();
        var town2 = new Town(town1);

        Assert.Equal(town1.GetHashCode(), town2.GetHashCode());
    }

    [Fact]
    public void MassiveEqualityStressTest()
    {
        var users = _fixture.CreateMany<User>(1000).ToList();

        foreach (var user in users)
        {
            var copy = new User(user);

            Assert.Equal(user, copy);
            Assert.Equal(user.GetHashCode(), copy.GetHashCode());
            Assert.NotSame(user, copy);
        }
    }

    [Fact]
    public void MassiveTownStressTest()
    {
        var towns = _fixture.CreateMany<Town>(200).ToList();

        foreach (var town in towns)
        {
            var copy = new Town(town);

            Assert.Equal(town, copy);
            Assert.NotSame(town, copy);
            Assert.NotSame(town.Residents, copy.Residents);
        }
    }

    [Fact]
    public void SelfReference_ShouldRemainEqual()
    {
        var user = _fixture.Create<User>();
        var copy = user;

        Assert.Equal(user, copy);
    }

    [Fact]
    public void MultipleHashCodeCalls_ShouldRemainStable()
    {
        var town = _fixture.Create<Town>();

        var hash = town.GetHashCode();

        for (int i = 0; i < 100; i++)
            Assert.Equal(hash, town.GetHashCode());
    }
  

}
