using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data;

public static class Extensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var dbContext =scope.ServiceProvider.GetRequiredService<DiscountContext>();
        dbContext.Database.Migrate();
        
        return builder;
    }
}