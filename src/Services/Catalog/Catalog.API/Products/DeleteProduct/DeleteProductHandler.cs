namespace Catalog.API.Products.DeleteProduct;

[PublicAPI]
public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

[PublicAPI]
public record DeleteProductResult(bool IsSuccess);
    
public class DeleteProductCommandHandler(IDocumentSession session)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        session.DeleteWhere<Product>(x => x.Id == command.Id);
        await session.SaveChangesAsync(cancellationToken);
        
        return new DeleteProductResult(true);
    }
}

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}