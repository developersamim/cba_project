using AutoMapper;
using Moq;
using Shouldly;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Core.DTO;
using TradeProject.Core.Features.Accounts.Queries;
using TradeProject.Core.Features.Accounts.Queries.GetAccounts;
using TradeProject.Domain;

namespace TradeProject.Core.Tests.Handlers;

public class GetAccountsQueryHandlerTests
{
    private readonly Mock<IAccountRepository> _mockAccountRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetAccountsQueryHandler _handler;

    public GetAccountsQueryHandlerTests()
    {
        _mockAccountRepository = new Mock<IAccountRepository>();
        _mockMapper = new Mock<IMapper>();

        _handler = new GetAccountsQueryHandler(_mockAccountRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAccounts_ShouldReturnAccounts()
    {
        // Arrange
        var query = new GetAccountsQuery();

        var accounts = new List<AccountEntity>
        {
            new AccountEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "john",
                LastName = "cena"
            },
            new AccountEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "randy",
                LastName = "orton"
            }
        };

        var accountsDto = new List<AccountDto>
        {
             new AccountDto
            {
                Id = accounts[0].Id,
                FirstName = accounts[0].FirstName,
                LastName = accounts[0].LastName
            },
            new AccountDto
            {
                Id = accounts[1].Id,
                FirstName = accounts[1].FirstName,
                LastName = accounts[1].LastName
            }
        };

        _mockAccountRepository
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(accounts);

        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<AccountDto>>(accounts)).Returns(accountsDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(accountsDto);
        result.Count().ShouldBe(2);

        _mockAccountRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }
}