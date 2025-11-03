using LinqAggregation_Querying_Person_Order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAggregation_Querying_Person_Order
{
     class Program
    {
       static List<Person> people = new List<Person>
        {
            new Person { Id = 1, Name = "Alice", Age = 30, Email = "alice@example.com" },
            new Person { Id = 2, Name = "Bob", Age = 25, Email = "" },
            new Person { Id = 3, Name = "Charlie", Age = 35, Email = "charlie@example.com" },
            new Person { Id = 4, Name = "Diana", Age = 40, Email = "diana@example.com" }
        };


       static List<Order> orders = new List<Order>
        {
            new Order { OrderId = 1, PersonId = 1, Amount = 100, OrderDate = new DateTime(2025, 1, 15) },
            new Order { OrderId = 2, PersonId = 2, Amount = 50,  OrderDate = new DateTime(2025, 2, 20) },
            new Order { OrderId = 3, PersonId = 3, Amount = 75,  OrderDate = new DateTime(2025, 3, 5) },
            new Order { OrderId = 4, PersonId = 3, Amount = 120, OrderDate = new DateTime(2025, 3, 15) },
            new Order { OrderId = 5, PersonId = 4, Amount = 200, OrderDate = new DateTime(2025, 4, 10) }
        };
        public static void Main(string[] args)
        {
          

            while (true)
            {
                //Console.WriteLine("1: Display Total Orders and sum of Orders amount of each person");
                //Console.WriteLine("");
                Console.WriteLine("\n========= LINQ Operations Menu =========");
                Console.WriteLine("1: Aggregation Operations");
                Console.WriteLine("2: Element Operators");
                Console.WriteLine("3: Quantifier Operators");
                Console.WriteLine("4: Collection Conversion");
                Console.WriteLine("5: Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\n-- Aggregation Operations --");
                        Console.WriteLine("DisplayTotalOrdersAndSum");
                       
                        
                        DisplayTotalOrdersAndSum();
                        Console.WriteLine("--------------");
                        Console.WriteLine("CalculateAverageForOlderThan30");
                        CalculateAverageForOlderThan30();
                        Console.WriteLine("--------------");
                        Console.WriteLine("DisplayMinMaxAvgOrder");
                        DisplayMinMaxAvgOrder();
                        break;

                    case "2":
                        Console.WriteLine("\n-- Element Operators --");
                        Console.WriteLine(" FindOrderOnSpecificDate");
                        
                        FindOrderOnSpecificDate();
                        Console.WriteLine("--------------");
                        Console.WriteLine(" FindFirstOrderGreaterThan150");
                        FindFirstOrderGreaterThan150();
                        break;

                    case "3":
                        Console.WriteLine("\n-- Quantifier Operators --");
                        Console.WriteLine(" CheckAllPeopleHaveOrders");
                        
                        CheckAllPeopleHaveOrders();
                        Console.WriteLine("--------------");
                        Console.WriteLine("CheckAnyOrderGreaterThan250");
                        CheckAnyOrderGreaterThan250();
                        break;

                    case "4":
                        Console.WriteLine("\n-- Collection Conversion --");
                        Console.WriteLine("ConvertOrdersToDictionary");
                        
                        ConvertOrdersToDictionary();
                        Console.WriteLine("--------------");
                        Console.WriteLine(" DisplayNumberOfOrdersPerPerson");
                        DisplayNumberOfOrdersPerPerson();
                        break;

                   

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }
        // -------- Aggregation Operations --------
        static void DisplayTotalOrdersAndSum() {
            var result = people.GroupJoin(
            orders,
            p => p.Id,
            o => o.PersonId,
            (p, o) => new
            {
                PersonName = p.Name,
                OrderCount = o.Count(),
                TotalAmount = o.Sum(x => x.Amount)
            });

            Console.WriteLine("\nTotal Orders and Sum of Amount per Person:");
            foreach (var r in result)
            {
                Console.WriteLine($"{r.PersonName}: {r.OrderCount} orders, Total = {r.TotalAmount}");
            }

        }
        static void CalculateAverageForOlderThan30()
        {
            //var results = from p in people
            //              where p.Age > 30
            //              join o in orders on p.Id equals o.PersonId
            //              group o by p.Name into g
            //              select new
            //              {
            //                  Name = g.Key,
            //                  AverageAmount = g.Average(x => x.Amount)
            //              };

            //Console.WriteLine("\nAverage order amount for people older than 30:");
            //foreach (var item in results)
            //    Console.WriteLine($"{item.Name} => Average Order Amount: {item.AverageAmount:F2}");

            var avg = people
           .Where(p => p.Age > 30)
           .Join(orders, p => p.Id, o => o.PersonId, (p, o) => o.Amount)
           .Average();

            Console.WriteLine($"\nAverage Order Amount for People Older Than 30: {avg}");
        }
        static void DisplayMinMaxAvgOrder() {
            var res = people.GroupJoin(
                orders, p => p.Id,
                o => o.PersonId,
                (p, o) => new
                {
                    Name = p.Name,
                    MinOrder = o.Min(x => x.Amount),
                    MaxOrder = o.Max(x => x.Amount)
                });
            
                foreach(var po in res)
            {
                Console.WriteLine($"Person Id: {po.Name} Min Order is {po.MinOrder} and Max Order is {po.MaxOrder} ");
            }
            
        }

        // -------- Element Operators --------
        static void FindOrderOnSpecificDate() {
            var order = orders.Find(o => o.OrderDate == new DateTime(2025, 1, 15));
            Console.WriteLine("Order Id : "+order.OrderId+" Order Amount : "+order.Amount);
        }
        static void FindFirstOrderGreaterThan150() {
            var order = orders.FirstOrDefault(o => o.Amount > 150);
            Console.WriteLine("Order Id : " + order.OrderId + " Order Amount : " + order.Amount);

        }

        // -------- Quantifier Operators --------
        static void CheckAllPeopleHaveOrders() {
            var isPlacedOrder = people.GroupJoin(orders,
                p => p.Id,
                o => o.PersonId,
                (p, o) => new
                {
                    Ordered = o.Any()
                });

            if (isPlacedOrder != null)
            {
                Console.WriteLine("All the people is placed Atleast one order");
            }
            else
            {
                Console.WriteLine(" order is not placed by everyone");
            }

    
       }
        static void CheckAnyOrderGreaterThan250() {
            var orderGreaterThan250 = orders.Where(x => x.Amount > 250).ToList();
            if(orderGreaterThan250 != null)
            {

                Console.WriteLine("Orders greater than 250 are : " + orderGreaterThan250.Count);
                   
                
                
            }
        }

        // -------- Collection Conversion --------
        static void ConvertOrdersToDictionary() {
            var dict = people.GroupJoin(
                orders,
                p => p.Id,
                o => o.PersonId,
                (p, o) => new { p.Name, Orders = o.ToList() })
                .ToDictionary(x => x.Name, x => x.Orders);

            Console.WriteLine("\nOrders converted into Dictionary<PersonName, List<Order>>:");
            foreach (var kv in dict)
            {
                Console.WriteLine($"\n{kv.Key} has {kv.Value.Count} orders:");
                foreach (var order in kv.Value)
                    Console.WriteLine($"   OrderId: {order.OrderId}, Amount: {order.Amount}");
            }
        }
        static void DisplayNumberOfOrdersPerPerson() {
            var result = people.GroupJoin(
               orders,
               p => p.Id,
               o => o.PersonId,
               (p, o) => new { p.Name, OrderCount = o.Count() });

            Console.WriteLine("\nNumber of Orders per Person:");
            foreach (var r in result)
            {
                Console.WriteLine($"{r.Name}: {r.OrderCount}");
            }
        }
    }
}
