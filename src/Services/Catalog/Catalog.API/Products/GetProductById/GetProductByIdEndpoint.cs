namespace Catalog.API.Products.GetProductById;

[PublicAPI]
public record GetProductByIdResponse(Product Product);

[PublicAPI]
public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product/{id}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id), cancellationToken);
                
                var response = result.Adapt<GetProductByIdResponse>();
                
                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithTags("Get Product By Id");
    }
}