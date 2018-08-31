using System;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PizzaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebClient wc = new WebClient())
            {
                string pizzas = wc.DownloadString("http://files.olo.com/pizzas.json");

                JArray json = JArray.Parse(pizzas);
                
                var result = from j in json.SelectMany(n => n["toppings"]).Values<string>()
                             group j by j
                             into g
                             orderby g.Count() descending
                             select new { Toppings = g.Key, Count = g.Count()};
                int i = 0;

                foreach (var item in result)
                    if (i++ < 20)
                        Console.WriteLine("{0}. Toppings - {1}, rank = {2}", i, item.Toppings, item.Count);
                
            }
            Console.ReadLine();
        }
    }
}
