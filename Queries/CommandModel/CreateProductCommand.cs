namespace MicroservicesWithCQRSDesignPattern.Queries.CommandModel
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
