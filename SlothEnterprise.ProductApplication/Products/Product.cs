namespace SlothEnterprise.ProductApplication.Products
{
    public interface IProduct
    {
        int Id { get; }
    }

    public class Product : IProduct
    {
        public int Id { get; set; }
    }
}
