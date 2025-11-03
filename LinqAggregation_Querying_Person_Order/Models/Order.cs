using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LinqAggregation_Querying_Person_Order.Models
{
    internal class Order
    {
        public int OrderId { get; set; }
        public int PersonId { get; set; }
        public double Amount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
