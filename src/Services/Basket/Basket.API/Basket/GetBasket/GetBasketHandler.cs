namespace Basket.API.Basket.GetBasket;

[PublicAPI]
public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

[PublicAPI]
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(query.UserName, cancellationToken);        
        
        return new GetBasketResult(basket);
    }
}