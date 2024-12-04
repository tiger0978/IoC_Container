using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{
    internal class Eagle : IBird
    {
        private IFood food {  get; set; }
        private ILogger<Eagle> logger { get; set; }
        public Eagle(IFood food, ILogger<Eagle> logger)
        {
            this.logger = logger;
            this.food = food;
        }

        void IBird.EAT()
        {
            Console.WriteLine($"老鷹吃{food.type}");
            logger.Log(LogLevel.Information, "老鷹吃米");
        }
        public string color { get; set; } = "Brown";
    }
}
