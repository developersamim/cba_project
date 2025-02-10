using Moq;
using Shouldly;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Core.DTO;
using TradeProject.Core.Exceptions;
using TradeProject.Core.Features.Accounts.Commands.DeleteAccount;
using TradeProject.Domain;

namespace TradeProject.Core.Tests.Handlers;

public class DeleteAccountCommandHandlerTests
{
    private readonly Mock<IAccountRepository> _mockAccountRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly DeleteAccountCommandHandler _handler;

    public DeleteAccountCommandHandlerTests()
    {
        _mockAccountRepository = new Mock<IAccountRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handler = new DeleteAccountCommandHandler(_mockAccountRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task DeleteAccount_ShouldDeleteAccount()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var existingAccount = new AccountEntity
        {
            Id = accountId,
            FirstName = "john",
            LastName = "cena"
        };
        var command = new DeleteAccountCommand
        {
            Id = accountId
        };

        _mockAccountRepository
            .Setup(repo => repo.GetByIdAsync(accountId))
            .ReturnsAsync(existingAccount);
        _mockAccountRepository
            .Setup(repo => repo.DeleteAsync(It.IsAny<AccountEntity>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockAccountRepository
            .Verify(repo =>
                repo.DeleteAsync(It.Is<AccountEntity>(a =>
                    a.Id == accountId
                )), Times.Once);
    }

    [Fact]
    public async Task DeleteAccount_ShouldThrowException_WhenAccountNotFound()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var command = new DeleteAccountCommand
        {
            Id = accountId
        };

        _mockAccountRepository
            .Setup(repo => repo.GetByIdAsync(accountId))
            .ReturnsAsync((AccountEntity?)null);

        // Act
        Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await Should.ThrowAsync<AccountNotFoundException>(action);

        _mockAccountRepository
            .Verify(repo => repo.GetByIdAsync(accountId),
            Times.Once);
        _mockAccountRepository
            .Verify(repo => repo.DeleteAsync(It.IsAny<AccountEntity>()),
            Times.Never);
    }
}
