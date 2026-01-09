namespace Restaurant.Order.ExternalServices.Model.Preparation;

public class PreparationOrder
{

    public string? Id { get; set; }

    public DateTime? Data { get; set; }

    public int? Numero { get; set; }

    public string? Cliente { get; set; }

    public string? Status { get; set; }

    public PreparationOrderItem[] Items { get; set; } = [];

}
