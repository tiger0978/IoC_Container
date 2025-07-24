using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{
    public class Sparrow : IBird
    {
        public string color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string name { get; set; } = "sparrow";

        public void EAT()
        {
            throw new NotImplementedException();
        }
    }
}
