namespace TradeProject.Core.Exceptions;

public class TradeTypeArgumentException : Exception
{
    public TradeTypeArgumentException(string type)
        : base($"Trade with Type {type} not found.")
    {

    }
}

public class TradeNotFoundException : Exception
{
    public TradeNotFoundException(Guid id)
        : base($"Trade with id {id} not found.")
    {

    }
}

public class TradeStatusArgumentException : Exception
{
    public TradeStatusArgumentException(string status)
        : base($"Trade with status {status} not found.")
    {

    }
}