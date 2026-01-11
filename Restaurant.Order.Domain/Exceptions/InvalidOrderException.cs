namespace Restaurant.Order.Domain;

public class InvalidOrderException : Exception
{

    public override string Message => "Invalid order";

}
