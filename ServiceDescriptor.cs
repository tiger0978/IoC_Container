using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{
    public class ServiceDescriptor
    {
        public ServiceDescriptor(Lifetime lifetime, Type instance, Func<ServiceProvider, object> func = null) 
        {
            this.lifetime = lifetime;
            this.instance = instance;
            this.func = func;
        }

        public Lifetime lifetime {  get; set; }
        public Func<ServiceProvider, object> func { get; set; }
        public Type instance {  get; set; }

        public object tempInstance { get; set; }


    }
}
