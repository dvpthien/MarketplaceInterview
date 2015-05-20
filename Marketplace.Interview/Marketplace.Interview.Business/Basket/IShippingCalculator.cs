using System.Linq;
using Marketplace.Interview.Business.Shipping;

namespace Marketplace.Interview.Business.Basket
{
    public interface IShippingCalculator
    {
        decimal CalculateShipping(Basket basket);
    }

    public class ShippingCalculator : IShippingCalculator
    {

        public decimal CalculateShipping(Basket basket)
        {
            basket.Discount = 0;
            foreach (var lineItem in basket.LineItems)
            {
                lineItem.ShippingAmount = lineItem.Shipping.GetAmount(lineItem, basket);
                lineItem.ShippingDescription = lineItem.Shipping.GetDescription(lineItem, basket);
                if (basket.Discount == 0)
                {
                    basket.Discount = Operator.Apply(lineItem, "duplicate", basket.LineItems) ? 0.5m : 0;
                }
            }

            return basket.LineItems.Sum(li => li.ShippingAmount) - basket.Discount;
        }
    }
}