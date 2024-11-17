using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{
    internal class Rice : IFood
    {
        public override string type { get; set; } = "米";
    }
}
