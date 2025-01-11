using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProducts
{

    public record GetProductQuery():IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler(IDocumentSession session,ILogger<GetProductsQueryHandler> logger) 
          : IQueryHandler<GetProductQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler handle called with {@Query}", query);
            var product = await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductsResult(product);
        }
    }
}
