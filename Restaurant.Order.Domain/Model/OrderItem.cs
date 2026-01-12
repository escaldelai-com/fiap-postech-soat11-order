namespace Restaurant.Order.Domain;

public class OrderItem : IComparable<OrderItem>, IEquatable<OrderItem>
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

        var result = string.Compare(Tipo, other.Tipo, StringComparison.OrdinalIgnoreCase);

        if (result != 0) return result;

        result = string.Compare(Nome, other.Nome, StringComparison.OrdinalIgnoreCase);

        return result;
    }

    public bool Equals(OrderItem? other)
    {
        return other != null &&
            Nome == other.Nome &&
            Tipo == other.Tipo &&
            Preco == other.Preco;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is OrderItem other && Equals(other));
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Nome, Tipo, Preco);
    }

    public static bool operator ==(OrderItem? left, OrderItem? right)
    {
        return ReferenceEquals(left, right) || (left is not null && left.Equals(right));
    }

    public static bool operator !=(OrderItem? left, OrderItem? right)
    {
        return !(left == right);
    }

    public static bool operator <(OrderItem? left, OrderItem? right)
    {
        return left == null || left.CompareTo(right) < 0;
    }

    public static bool operator <=(OrderItem? left, OrderItem? right)
    {
        return left == null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(OrderItem? left, OrderItem? right)
    {
        return left == null || left.CompareTo(right) > 0;
    }

    public static bool operator >=(OrderItem? left, OrderItem? right)
    {
        return left == null || left.CompareTo(right) >= 0;
    }

}
