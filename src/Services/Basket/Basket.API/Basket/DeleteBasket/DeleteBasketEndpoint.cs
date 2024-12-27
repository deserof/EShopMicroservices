namespace Basket.API.Basket.DeleteBasket;

// public record DeleteBasketRequest(string UserName);

[PublicAPI]
public record DeleteBasketResponse(bool IsSuccess);

[PublicAPI]
public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(userName), cancellationToken);

                var response = result.Adapt<DeleteBasketResponse>();
            
                return Results.Ok(response);
            })
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Basket")
            .WithTags("Delete Basket");
    }
}