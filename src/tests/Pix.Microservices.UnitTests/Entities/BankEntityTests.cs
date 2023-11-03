using FluentAssertions;
using Pix.Microservices.Domain.Entities;
using Xunit;

namespace Pix.Microservices.UnitTests.Entities;

public class BankEntityTests
{
    [Fact]
    public void Bank_WhenCreated_ShouldHaveCreatedAtSet()
    {
        var bank = new Bank(Guid.Empty, "Test Bank", 001, true);
        bank.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Bank_WhenCreated_ShouldHaveName()
    {
        var bank = new Bank(Guid.Empty, "Test Bank", 001, true);
        bank.Name.Should().Be("Test Bank");
    }

    [Fact]
    public void Bank_Activate_ShouldSetActiveTrue()
    {
        var bank = new Bank(Guid.Empty, "Test Bank", 001, false);
        bank.Activate();
        bank.Active.Should().BeTrue();
    }

    [Fact]
    public void Bank_Deactivate_ShouldSetActiveFalse()
    {
        var bank = new Bank(Guid.Empty, "Test Bank", 001, true);
        bank.Deactivate();
        bank.Active.Should().BeFalse();
    }

    [Fact]
    public void Bank_WhenCreated_ShouldHaveCorrectNumber()
    {
        var bank = new Bank(Guid.Empty, "Test Bank", 237, true);
        bank.Number.Should().Be(237);
    }
}
