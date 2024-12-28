using Discount.GRPC.Data;
using Discount.GRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Services;

public class DiscountService(DiscountContext dbContext) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons
            .FirstOrDefaultAsync(x => EF.Functions.Like(x.ProductName, request.ProductName));
        
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));
        }

        var couponModel = coupon.Adapt<CouponModel>();
        
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        }
        
        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        var couponModel = coupon.Adapt<CouponModel>();
        
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.Id == request.Coupon.Id);
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));
        }
        
        coupon.Description = request.Coupon.Description;
        coupon.ProductName = request.Coupon.ProductName;
        coupon.Amount = request.Coupon.Amount;

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();
        
        var couponModel = coupon.Adapt<CouponModel>();
        
        return couponModel;
    }
    
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var count = await dbContext.Coupons
            .Where(x => x.ProductName == request.ProductName)
            .ExecuteDeleteAsync();

        if (count == 0)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
        }
        
        return new DeleteDiscountResponse { Success = true };
    }
}