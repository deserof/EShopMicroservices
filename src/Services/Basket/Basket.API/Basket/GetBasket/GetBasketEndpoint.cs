namespace Basket.API.Basket.GetBasket;

// public record GetBasketRequest(string UserName);

[PublicAPI]
public record GetBasketResponse(ShoppingCart Cart);

[PublicAPI]
public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName), cancellationToken);

            var response = result.Adapt<GetBasketResponse>();
            
            return Results.Ok(response);
        })
        .WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket")
        .WithTags("Get Basket");
    }
}