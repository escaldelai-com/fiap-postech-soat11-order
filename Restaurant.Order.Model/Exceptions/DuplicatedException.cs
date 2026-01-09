namespace Restaurant.Order.Model.Exceptions;

public class DuplicatedException : Exception
{

    public override string Message => "Duplicated item.";

}
