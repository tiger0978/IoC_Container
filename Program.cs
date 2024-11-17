using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddTransient<IBird, Eagle>();
            services.AddTransient<IFood, Rice>();
            var provides = services.BuildServiceProvider();
            var bird = provides.GetService<IBird>();
            bird.EAT();
            bird.color = "White";



            //services.AddTransient<生肖, 生肖>();
            //var provides = services.BuildServiceProvider();
            //var animals = provides.GetService<生肖>();
            //animals.PrintAnimal();




            //Console.WriteLine(bird2.color);
            Console.ReadKey();





        }
    }
}
