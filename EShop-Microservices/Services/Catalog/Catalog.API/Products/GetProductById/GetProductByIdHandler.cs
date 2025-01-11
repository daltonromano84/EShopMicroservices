
namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductsByIdResult>;

    public record GetProductsByIdResult(Product Product);

    internal class GetProductsByIdQueryHandler(IDocumentSession session, ILogger<GetProductsByIdQueryHandler> logger)
          : IQueryHandler<GetProductByIdQuery, GetProductsByIdResult>
    {
        public async Task<GetProductsByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler handle called with {@Query}", query);
            var product = await session.LoadAsync<Product>(query.Id,cancellationToken);

            if (product == null)
            {

                throw new ProductNotFoundException();
            }

            return new GetProductsByIdResult(product);
        }
    }
}

