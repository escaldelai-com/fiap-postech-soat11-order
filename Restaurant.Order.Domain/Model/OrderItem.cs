namespace Restaurant.Order.Domain;

public class OrderItem : IComparable<OrderItem>
{

    public string Nome { get; private set; }

    public string Tipo { get; private set; }

    public decimal Preco { get; private set; }


    public OrderItem(string nome, string tipo, decimal preco)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(nome)
            .IsNotNullOrWhiteSpace(tipo)
            .GreaterThanZero(preco)
            .Validate();

        Nome = nome;
        Tipo = tipo;
        Preco = preco;
    }


    public int CompareTo(OrderItem? other)
    {
        if (other == null) return 1;

        var result = Tipo.CompareTo(other.Tipo);
        if (result != 0) return result;

        result = Nome.CompareTo(other.Nome);
        if (result != 0) return result;

        return result;
    }

}
