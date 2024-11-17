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
        public Eagle(IFood food)
        {
            this.food = food;
        }
        void IBird.EAT()
        {
            Console.WriteLine($"老鷹吃{food.type}");
        }
        public string color { get; set; } = "Brown";
    }
}
