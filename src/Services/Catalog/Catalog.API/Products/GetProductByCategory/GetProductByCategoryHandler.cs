namespace Catalog.API.Products.GetProductByCategory;

[PublicAPI]
public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

[PublicAPI]
public record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetProductByCategoryQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(x => x.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);
        
        return new GetProductByCategoryResult(products);
    }
}

public class GetProductByCategoryQueryValidator : AbstractValidator<GetProductByCategoryQuery>
{
    public GetProductByCategoryQueryValidator()
    {
        RuleFor(x => x.Category).NotEmpty();
    }
}