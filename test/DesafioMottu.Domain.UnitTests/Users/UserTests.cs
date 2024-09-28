using DesafioMottu.Domain.UnitTests.Infrastructure;
using DesafioMottu.Domain.Users;
using DesafioMottu.Domain.Users.Events;
using FluentAssertions;

namespace DesafioMottu.Domain.UnitTests.Users;

public class UserTests : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Act
        var user = User.Create(UserData.Name, UserData.Cnpj, UserData.BirthDate);

        // Assert
        user.Name.Should().Be(UserData.Name);
        user.Cnpj.Should().Be(UserData.Cnpj);
        user.BirthDate.Should().Be(UserData.BirthDate);
    }

    [Fact]
    public void Create_Should_RaiseUserCreatedDomainEvent()
    {
        // Act
        var user = User.Create(UserData.Name, UserData.Cnpj, UserData.BirthDate);

        // Assert
        UserCreatedDomainEvent userCreatedDomainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);

        userCreatedDomainEvent.UserId.Should().Be(user.Id);
    }
}
