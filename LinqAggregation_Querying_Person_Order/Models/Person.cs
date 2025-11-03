using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAggregation_Querying_Person_Order.Models
{
    internal class Person
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public string Email { get; set; }
    }
}
