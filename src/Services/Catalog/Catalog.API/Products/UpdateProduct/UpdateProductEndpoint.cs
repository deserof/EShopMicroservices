namespace Catalog.API.Products.UpdateProduct;

[PublicAPI]
public record UpdateProductRequest(string Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

[PublicAPI]
public record UpdateProductResponse(bool IsSuccess);

[PublicAPI]
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/product",
                async (UpdateProductRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = request.Adapt<UpdateProductCommand>();

                    var result = await sender.Send(command, cancellationToken);

                    var response = result.Adapt<UpdateProductResponse>();

                    return Results.Ok(response);
                }).WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithTags("Update Product");
    }
}