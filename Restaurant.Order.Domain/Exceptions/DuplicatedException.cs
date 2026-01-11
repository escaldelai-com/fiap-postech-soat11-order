namespace Restaurant.Order.Domain.Exceptions;

public class DuplicatedException : Exception
{

    public override string Message => "Duplicated item.";

}
