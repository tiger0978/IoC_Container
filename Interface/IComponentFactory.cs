using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{
    public interface IComponentFactory
    {
        T Create<T>();
    }
}
