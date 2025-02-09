using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TradeProject.Core.Features.Accounts.Commands.CreateAccount;
public class CreateAccountCommand : IRequest<Guid>
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "FirstName must be between 2 and 100 characters long")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "LastName must be between 2 and 100 characters long")]
    public string LastName { get; set; }
}