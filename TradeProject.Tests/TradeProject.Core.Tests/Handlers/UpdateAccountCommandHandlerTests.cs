using Moq;
using Shouldly;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Core.Features.Accounts.Commands.UpdateAccount;
using TradeProject.Domain;

namespace TradeProject.Core.Tests.Handlers;

public class UpdateAccountCommandHandlerTests
{
    private readonly Mock<IAccountRepository> _mockAccountRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly UpdateAccountCommandHandler _handler;

    public UpdateAccountCommandHandlerTests()
    {
        _mockAccountRepository = new Mock<IAccountRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handler = new UpdateAccountCommandHandler(_mockAccountRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task UpdateAccount_ShouldUpdateFirstName()
    {
        // Arrange 
        var accountId = Guid.NewGuid();
        var existingAccount = new AccountEntity
        {
            Id = accountId,
            FirstName = "john",
            LastName = "cena"
        };
        var command = new UpdateAccountCommand
        {
            Id = accountId,
            FirstName = "batista"
        };

        _mockAccountRepository
            .Setup(repo => repo.GetByIdAsync(accountId))
            .ReturnsAsync(existingAccount);
        _mockAccountRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<AccountEntity>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockAccountRepository
            .Verify(repo =>
                repo.UpdateAsync(It.Is<AccountEntity>(a =>
                    a.Id == command.Id &&
                    a.FirstName == command.FirstName)), Times.Once);

        existingAccount.FirstName.ShouldBe(command.FirstName);

    }
}
