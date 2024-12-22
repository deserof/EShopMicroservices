using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById;

[PublicAPI]
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

[PublicAPI]
public record GetProductByIdResult(Product Product);

public class GetProductQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }
        
        return new GetProductByIdResult(product);
    }
}

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}