using System.ComponentModel.DataAnnotations;
using MediatR;
using TradeProject.Core.DTO;

namespace TradeProject.Core.Features.Accounts.Queries.SearchAccount;

public class SearchAccountQuery : IRequest<IEnumerable<AccountDto>>
{
    public Guid Id { get; set; }
    [StringLength(100, MinimumLength = 2, ErrorMessage = "FirstName must be between 2 and 100 characters long.")]
    public string? FirstName { get; set; }
    [StringLength(100, MinimumLength = 2, ErrorMessage = "LastName must be between 2 and 100 characters long.")]
    public string? LastName { get; set; }
}
