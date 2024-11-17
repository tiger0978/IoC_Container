using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{
    public class ServiceProvider
    {
        private ServiceCollection _services;
        public ServiceProvider(ServiceCollection serviceCollection) 
        {
            _services = serviceCollection;
        }

        public T GetService<T>()
        {
            string keyName = typeof(T).Name;
            ServiceDescriptor serviceDescriptor = _services.dict[keyName];
            T instance = default;
            if(serviceDescriptor.lifetime == Lifetime.Transient)
            {
                if (serviceDescriptor.func != null)
                {
                    instance = (T)serviceDescriptor.func.Invoke(this);
                    return instance;
                }

                instance = (T)CreateInstance(serviceDescriptor.instance);
                //instance = (T)Activator.CreateInstance(serviceDescriptor.instance);
            }
            else if (serviceDescriptor.lifetime == Lifetime.Singleton)
            {
                if (serviceDescriptor.tempInstance != null)
                {
                    return (T)serviceDescriptor.tempInstance;
                }
                if (serviceDescriptor.func != null)
                {
                    instance = (T)serviceDescriptor.func.Invoke(this);
                    _services.dict[keyName].tempInstance = instance;
                    return instance;
                }
                instance = (T)CreateInstance(serviceDescriptor.instance);
                //instance = (T)Activator.CreateInstance(serviceDescriptor.instance);
                _services.dict[keyName].tempInstance = instance;
            }
            return instance;
        }

        private object CreateInstance(Type type)
        {
            // 優先使用參數最多的建構元
            var ctors = type.GetConstructors().OrderByDescending(x => x.GetParameters().Length);
            foreach (var ctor in ctors)
            {
                List<object> parameterList = new List<object>();
                var parms = ctor.GetParameters();
                if (parms.Length == 0)
                {
                    return Activator.CreateInstance(type);
                }
                foreach (var parm in parms)
                {
                    Type provider = typeof(ServiceProvider);
                    var method = provider.GetMethod("GetService");
                    var getService = method.MakeGenericMethod(parm.ParameterType);
                    object parameter = getService.Invoke(this, null);
                    parameterList.Add(parameter);
                }
                return Activator.CreateInstance(type, parameterList.ToArray());
            }
            return null;
        }
    }
}
