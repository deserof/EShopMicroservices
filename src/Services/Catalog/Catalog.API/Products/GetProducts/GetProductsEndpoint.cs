namespace Catalog.API.Products.GetProducts;

[PublicAPI]
public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

[PublicAPI]
public record GetProductsResponse(IEnumerable<Product> Products);

[PublicAPI]
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = request.Adapt<GetProductsQuery>();
                
                var result = await sender.Send(query, cancellationToken);

                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithTags("Get Products");
        ;
    }
}