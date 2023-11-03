using FluentAssertions;
using Pix.Microservices.Core.Security;
using Xunit;

namespace Pix.Microservices.UnitTests.Security;

public class PasswordHasherTests
{
    [Fact]
    public void HashPassword_ShouldReturnNonEmptyHash()
    {
        var hash = PasswordHasher.HashPassword("myPassword123");
        hash.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void VerifyPassword_WithCorrectPassword_ShouldReturnTrue()
    {
        var password = "myPassword123";
        var hash = PasswordHasher.HashPassword(password);
        PasswordHasher.VerifyPassword(password, hash).Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_WithWrongPassword_ShouldReturnFalse()
    {
        var hash = PasswordHasher.HashPassword("myPassword123");
        PasswordHasher.VerifyPassword("wrongPassword", hash).Should().BeFalse();
    }

    [Fact]
    public void HashPassword_ShouldGenerateUniqueSalts()
    {
        var hash1 = PasswordHasher.HashPassword("samePassword");
        var hash2 = PasswordHasher.HashPassword("samePassword");
        hash1.Should().NotBe(hash2);
    }

    [Fact]
    public void HashPassword_WithEmptyPassword_ShouldThrowArgumentException()
    {
        Action act = () => PasswordHasher.HashPassword("");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void VerifyPassword_WithEmptyPassword_ShouldThrowArgumentException()
    {
        var hash = PasswordHasher.HashPassword("validPassword");
        Action act = () => PasswordHasher.VerifyPassword("", hash);
        act.Should().Throw<ArgumentException>();
    }
}
