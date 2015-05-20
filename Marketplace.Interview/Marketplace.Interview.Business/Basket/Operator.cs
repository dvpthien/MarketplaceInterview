using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Interview.Business.Shipping;

namespace Marketplace.Interview.Business.Basket
{
   
  public class Operator
   {
       private static Dictionary<string, Func<object, object, bool>> operators;
       static Operator()
       {
           operators = new Dictionary<string, Func<object, object, bool>>();
           operators["duplicate"] = (duplicate);
       }

       public static bool Apply(LineItem item, string op, object target)
       {
           return operators[op](item, target);
       }

       static bool duplicate(object o1, object o2)
       {
           var count = ((List<LineItem>)o2).Count(item => item.Shipping.GetType() == typeof(NewShipping) &&
                                                                  item.SupplierId == ((LineItem)o1).SupplierId &&
                                                                  item.DeliveryRegion == ((LineItem)o1).DeliveryRegion);
           return count>1;
       }
   }
}
