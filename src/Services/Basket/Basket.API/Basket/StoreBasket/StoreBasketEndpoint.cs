namespace Basket.API.Basket.StoreBasket;

[PublicAPI]
public record StoreBasketRequest(ShoppingCart ShoppingCart);

[PublicAPI]
public record StoreBasketResponse(string UserName);

[PublicAPI]
public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<StoreBasketCommand>();
                
                var result = await sender.Send(command, cancellationToken);

                var response = result.Adapt<StoreBasketResponse>();
            
                return Results.Created($"/basket/{response.UserName}", response);
            })
            .WithName("StoreBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store Basket")
            .WithTags("Store Basket");
    }
}