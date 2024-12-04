using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
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
            services.AddSingleton<IBird, Eagle>();
            services.AddTransient<IFood, Rice>();


            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory
                    .GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            services.AddLogging(LoggingBuilder => LoggingBuilder.AddNLog(config));

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
