namespace Basket.API.Basket.DeleteBasket;

[PublicAPI]
public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

[PublicAPI]
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await basketRepository.DeleteBasket(command.UserName, cancellationToken);
        
        return new DeleteBasketResult(result);
    }
}

public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is required");
    }
}