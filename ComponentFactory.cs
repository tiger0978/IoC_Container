using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{
    public class ComponentFactory : IComponentFactory
    {
        private readonly IServiceProvider _sp;
        public ComponentFactory(IServiceProvider sp)
        {
            _sp = sp;
        }


        public T Create<T>()
        {
            T t = _sp.GetService<T>();
            return t;
        }

        public T Create<T>(Type type)
        {
            T t = _sp.GetServices<T>().FirstOrDefault(x => x.GetType() == type);

            return t;
        }
    }
}
