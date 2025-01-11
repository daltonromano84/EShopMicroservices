namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category) : IQuery<GetProductsByCategoryResult>;

    public record GetProductsByCategoryResult(IEnumerable<Product> Products);

    internal class GetProductsByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductsByCategoryQueryHandler> logger)
          : IQueryHandler<GetProductByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler handle called with {@Query}", query);
            var products = await session.Query<Product>()
                         .Where(p => p.Category.Contains(query.category))
                         .ToListAsync(cancellationToken);
           

            return new GetProductsByCategoryResult(products);
        }
    }
}
