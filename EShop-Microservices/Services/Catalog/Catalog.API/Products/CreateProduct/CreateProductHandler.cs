﻿

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string>Category,string Description, string ImageFile,decimal Price)
            :ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public  async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {


            //create product entity from command objetc
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price


            };


            //save to database


            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            //return creatproductResult result
            
            return new CreateProductResult(product.Id);
        }
    }
}
