namespace Restaurant.Order.WebApi.Security;

public class Claims
{

    public class Order
    {
        public const string Create = "order:order:create";
        public const string AddItem = "order:order:add-item";
        public const string Confirm = "order:order:confirm";
        public const string Cancel = "order:order:cancel";
        public const string ConfirmPayment = "order:order:confirm-payment";
    }

    public class Product
    {
        public const string Get = "order:product:get";
        public const string GetByType = "order:product:get-by-type";
        public const string Create = "order:product:create";
        public const string Update = "order:product:update";
        public const string Delete = "order:product:delete";
    }

    public class ProductType
    {
        public const string GetList = "order:product:get-list";        
    }


    public static string[] All =>
    [
        Order.Create,
        Order.AddItem,
        Order.Confirm,
        Order.Cancel,
        Order.ConfirmPayment,
        Product.Get,
        Product.GetByType,
        Product.Create,
        Product.Update,
        Product.Delete,
        ProductType.GetList
    ];

}
