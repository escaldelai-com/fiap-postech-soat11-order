using Restaurant.Order.Model.Exceptions;

namespace Restaurant.Order.Model;

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
        Items = Array.Empty<OrderItem>();
    }



    public void AddItem(OrderItem item)
    {
        Validator.Create()
            .IsNotNull(item)
            .Validate();

        if (IsDuplicated(item))
            throw new DuplicatedException();

        Items = Items
            .Concat([item])
            .ToArray();
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Elaboration)
            throw new OrderStatusException(Status, "confirm");

        if (!Items.Any())
            throw new InvalidOrderException();

        Status = OrderStatus.WaitingPayment;
    }

    public void ConfirmPay()
    {
        if (Status != OrderStatus.WaitingPayment)
            throw new OrderStatusException(Status, "confirm pay");

        if (!Items.Any())
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
