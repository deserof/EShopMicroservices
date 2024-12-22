namespace Catalog.API.Products.GetProductByCategory;

[PublicAPI]
public record GetProductByCategoryResponse(IEnumerable<Product> Products);

[PublicAPI]
public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/category/{category}",
                async (string category, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetProductByCategoryQuery(category), cancellationToken);

                    var response = result.Adapt<GetProductByCategoryResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Category")
            .WithTags("Get Product By Category");
    }
}