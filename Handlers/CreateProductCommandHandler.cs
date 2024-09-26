using MicroservicesWithCQRSDesignPattern.Interfaces;
using MicroservicesWithCQRSDesignPattern.Models;
using MicroservicesWithCQRSDesignPattern.Queries.CommandModel;

namespace MicroservicesWithCQRSDesignPattern.Handlers
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IRepository<int, Product> _repository;
        public CreateProductCommandHandler(IRepository<int, Product> repository)
        {
            _repository = repository;
        }
        public async Task Handle(CreateProductCommand command)
        {
            var product = new Product
            {
                Name = command.Name,
                Price = command.Price
            };

            await _repository.AddAsync(product);
            await _repository.SaveAsync();
        }
    }
}
