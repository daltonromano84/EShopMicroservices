
namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductsByIdResult>;

    public record GetProductsByIdResult(Product Product);

    internal class GetProductsByIdQueryHandler(IDocumentSession session)
          : IQueryHandler<GetProductByIdQuery, GetProductsByIdResult>
    {
        public async Task<GetProductsByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
          
            var product = await session.LoadAsync<Product>(query.Id,cancellationToken);

            if (product == null)
            {

                throw new ProductNotFoundException(query.Id);
            }

            return new GetProductsByIdResult(product);
        }
    }
}

