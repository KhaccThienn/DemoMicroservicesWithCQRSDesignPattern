using MicroservicesWithCQRSDesignPattern.Interfaces;
using MicroservicesWithCQRSDesignPattern.Models;
using MicroservicesWithCQRSDesignPattern.Queries.QueryModel;

namespace MicroservicesWithCQRSDesignPattern.Handlers
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IRepository<int, Product> _repository;

        public UpdateProductCommandHandler(IRepository<int, Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProductCommand command)
        {
            var productToUpdate = await _repository.GetByIdAsync(command.Id);
            if (productToUpdate != null)
            {
                // Update the product properties
                productToUpdate.Name = command.Name;
                productToUpdate.Price = command.Price;

                // Save changes to the repository
                await _repository.UpdateAsync(productToUpdate);
            }
            else
            {
                throw new Exception("Product not found"); // Handle product not found scenario
            }
        }
    }
}
