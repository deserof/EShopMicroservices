using Discount.GRPC;

namespace Basket.API.Basket.StoreBasket;

[PublicAPI]
public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

[PublicAPI]
public record StoreBasketResult(string UserName);

public class StoreBasketHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProto) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand commmand, CancellationToken cancellationToken)
    {
        var shoppingCart = await basketRepository.StoreBasket(commmand.ShoppingCart, cancellationToken);
        
        return new StoreBasketResult(shoppingCart.UserName);
    }
}

public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.ShoppingCart)
            .NotNull()
            .WithMessage("Basket couldn't be null");
        
        RuleFor(x => x.ShoppingCart.UserName)
            .NotEmpty()
            .WithMessage("UserName is required");
    }
}