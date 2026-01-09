namespace Restaurant.Order.Model;

public class InvalidOrderException : Exception
{

    public override string Message => "Invalid order";

}
