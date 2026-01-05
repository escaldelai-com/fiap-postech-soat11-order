namespace Restaurant.Order.Model;

public class Product
{

    public string Nome { get; private set; }

    public string Descricao { get; private set; }

    public string Tipo { get; private set; }

    public decimal Preco { get; private set; }


    public Product(string nome, string descricao, string tipo, decimal preco)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(nome)
            .IsNotNullOrWhiteSpace(descricao)
            .IsNotNullOrWhiteSpace(tipo)
            .GreaterThanZero(preco)
            .Validate();

        Nome = nome;
        Descricao = descricao;
        Tipo = tipo;
        Preco = preco;
    }

}
