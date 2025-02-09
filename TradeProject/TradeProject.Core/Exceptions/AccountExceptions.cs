namespace TradeProject.Core.Exceptions;
public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(Guid id)
        : base($"Account with ID {id} not found.")
    {

    }
}

