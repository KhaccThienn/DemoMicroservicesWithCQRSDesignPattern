namespace MicroservicesWithCQRSDesignPattern.Queries.QueryModel
{
    public class GetAllProductsCommand
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
    }
}
