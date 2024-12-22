using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

[PublicAPI]
public record GetProductsQuery(int? PageNumber = 1,int? PageSize = 10) : IQuery<GetProductsResult>;

[PublicAPI]
public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);
        
        return new GetProductsResult(products);
    }
}
