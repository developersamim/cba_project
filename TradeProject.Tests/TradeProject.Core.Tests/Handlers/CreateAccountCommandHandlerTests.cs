using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Core.Features.Accounts.Commands.CreateAccount;
using TradeProject.Domain;
using Xunit;

namespace TradeProject.Core.Tests.Handlers;

public class CreateAccountCommandHandlerTests
{
    private readonly Mock<IAccountRepository> _mockAccountRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateAccountCommandHandler _handler;

    public CreateAccountCommandHandlerTests()
    {
        _mockAccountRepository = new Mock<IAccountRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handler = new CreateAccountCommandHandler(_mockAccountRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task CreateAccount_ShouldCreateAccount()
    {
        // Arrange
        var command = new CreateAccountCommand
        {
            FirstName = "hrithik",
            LastName = "roshan"
        };


        _mockAccountRepository
            .Setup(repo => repo.AddAsync(It.IsAny<AccountEntity>()))
            .Returns(Task.CompletedTask);

        // Act
        var accountId = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockAccountRepository
            .Verify(repo => repo.AddAsync(It.Is<AccountEntity>(a =>
                a.FirstName == command.FirstName &&
                a.LastName == command.LastName)), Times.Once);
    }
}
