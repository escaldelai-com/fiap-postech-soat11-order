using Restaurant.Order.Domain.Exceptions;

namespace Restaurant.Order.Domain;

public class OrderInfo
{

    public DateTime Data { get; private set; }

    public int Numero { get; private set; }

    public string Cliente { get; private set; }

    public string Status { get; private set; }

    public OrderItem[] Items { get; private set; }

    public OrderInfo(DateTime data, int numero, string cliente, string status)
    {
        Validator.Create()
            .IsInThePastOrPresent(data)
            .GreaterThanZero(numero)
            .IsNotNullOrWhiteSpace(cliente)
            .IsNotNullOrWhiteSpace(status)
            .Validate();

        Data = data;
        Numero = numero;
        Cliente = cliente;
        Status = status;
        Items = [];
    }



    public void AddItem(OrderItem item)
    {
        Validator.Create()
            .IsNotNull(item)
            .Validate();

        if (IsDuplicated(item))
            throw new DuplicatedException();

        Items = [.. Items, item];
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Elaboration)
            throw new OrderStatusException(Status, "confirm");

        if (Items.Length == 0)
            throw new InvalidOrderException();

        Status = OrderStatus.WaitingPayment;
    }

    public void ConfirmPay()
    {
        if (Status != OrderStatus.WaitingPayment)
            throw new OrderStatusException(Status, "confirm pay");

        if (Items.Length == 0)
            throw new InvalidOrderException();

        Status = OrderStatus.Paid;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Canceled)
            throw new OrderStatusException(Status, "cancel");

        Status = OrderStatus.Canceled;
    }



    private bool IsDuplicated(OrderItem item)
    {
        return Items
            .Any(i => i.Equals(item));
    }

}
