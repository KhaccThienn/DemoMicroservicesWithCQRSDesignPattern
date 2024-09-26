using MicroservicesWithCQRSDesignPattern.Interfaces;
using MicroservicesWithCQRSDesignPattern.Models;
using MicroservicesWithCQRSDesignPattern.Queries.QueryModel;

namespace MicroservicesWithCQRSDesignPattern.Handlers
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<GetAllProductsCommand>>
    {
        private readonly IRepository<int, Product> _productsRepository;

        public GetProductsQueryHandler(IRepository<int, Product> repository)
        {
            _productsRepository = repository;
        }

        public async Task<IEnumerable<GetAllProductsCommand>> Handle(GetProductsQuery query)
        {
            var products = await _productsRepository.GetAllAsync(); // Implement repository method
                                                                    // Map products to ProductViewModel
            return products.Select(p => new GetAllProductsCommand
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }); 
        }
    }
}
