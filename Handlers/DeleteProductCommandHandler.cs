using MicroservicesWithCQRSDesignPattern.Interfaces;
using MicroservicesWithCQRSDesignPattern.Models;
using MicroservicesWithCQRSDesignPattern.Queries.QueryModel;

namespace MicroservicesWithCQRSDesignPattern.Handlers
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IRepository<int, Product> _repository;
        public DeleteProductCommandHandler(IRepository<int, Product> repository)
        {
            _repository = repository;
        }
        public async Task Handle(DeleteProductCommand command)
        {
            var productToDelete = await _repository.GetByIdAsync(command.Id);
            if (productToDelete != null)
            {
                // Delete the product
                await _repository.DeleteAsync(productToDelete);
            }
            else
            {
                throw new Exception("Product not found"); // Handle product not found scenario
            }
        }
    }
}
