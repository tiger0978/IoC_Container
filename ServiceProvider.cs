using Microsoft.Extensions.DependencyInjection;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{
    public class ServiceProvider : IServiceProvider
    {
        private ServiceCollection _services;
        private Dictionary<string, object> _tempInstances = new Dictionary<string, object>();
        public ServiceProvider(ServiceCollection serviceCollection) 
        {
            _services = serviceCollection;
        }
        public object GetService(Type serviceType)
        {
            Console.WriteLine(serviceType.FullName);

            var dict = _services.dict;
            //如果serviceType是可以直接從dict找到，代表是有註冊在容器中一般類型
            if(dict.TryGetValue(serviceType.FullName, out ServiceDescriptor descriptor))
            {
                return GetImplementationInstance(descriptor);
            }

            //如果他是IEnumerable<T>
            if (CheckIsIEnumerableType(serviceType))
            {
                var listInstance = GetGenericIListInstance(serviceType);
                return listInstance;
            }

            //如果他是IOption<T>
            if (serviceType.IsGenericType)
            {
                Type type = serviceType.GetGenericTypeDefinition();
                if (_services.dict.TryGetValue(type.FullName, out ServiceDescriptor genericServiceDescriptor))
                {
                    var serviceDescriptor = CreateGenericServiceDescriptor(serviceType, genericServiceDescriptor);
                    return GetImplementationInstance(serviceDescriptor);
                }
            }
            return null;
        }

        private bool CheckIsIEnumerableType(Type type)
        {
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return true;       
            }
            return false;
        }
        private IList GetGenericIListInstance(Type serviceType)
        {
            var genericArgument = serviceType.GetGenericArguments()[0];
            object genericInstance = GetService(genericArgument);
            if (genericInstance == null)
                return null;

            var genericList = CreateList(genericInstance.GetType());
            genericList.Add(genericInstance);
            return genericList;
        }

        private ServiceDescriptor CreateGenericServiceDescriptor(Type serviceType, ServiceDescriptor serviceDescriptor)
        {
            var result = new ServiceDescriptor(
                    serviceType,
                    serviceDescriptor.ImplementationType.MakeGenericType(serviceType.GetGenericArguments()),
                    serviceDescriptor.Lifetime);
            return result;
        }

        private IList CreateList(Type type)
        {
            Type listType = typeof(List<>);
            listType = listType.MakeGenericType(type); // List<LoggerFactory>
            return (IList)Activator.CreateInstance(listType);
        }


        private object GetImplementationInstance(ServiceDescriptor serviceDescriptor) 
        {
            if (serviceDescriptor == null)
                return null;
            if (serviceDescriptor.ImplementationInstance != null)
                return serviceDescriptor.ImplementationInstance;

            string keyName = serviceDescriptor.ServiceType.Name;
            object instance = default;
            if (serviceDescriptor.Lifetime == ServiceLifetime.Transient)
            {
                if (serviceDescriptor.ImplementationFactory != null)
                {
                    instance = serviceDescriptor.ImplementationFactory.Invoke(this);
                    return instance;
                }

                instance = CreateInstance(serviceDescriptor.ImplementationType);
            }
            else if (serviceDescriptor.Lifetime == ServiceLifetime.Singleton)
            {
                if (_tempInstances.TryGetValue(keyName, out instance))
                {
                    return instance;
                }
                if (serviceDescriptor.ImplementationFactory != null)
                {
                    instance = serviceDescriptor.ImplementationFactory.Invoke(this);
                    _tempInstances[keyName] = instance;
                    return instance;
                }

                instance = CreateInstance(serviceDescriptor.ImplementationType);
                _tempInstances[keyName] = instance;
            }
            return instance;
        }

        private object CreateInstance(Type implementationType)
        {
            // 優先使用參數最多的建構元
            var ctors = (implementationType).GetConstructors().OrderByDescending(x => x.GetParameters().Length);
            foreach (var ctor in ctors)
            {
                List<object> parameterList = new List<object>();
                var parms = ctor.GetParameters();
                if (parms.Length == 0)
                {
                    return Activator.CreateInstance(implementationType);
                }
                bool isResolve = true;
                foreach (var parm in parms)
                {
                    object parameter = GetService(parm.ParameterType);
                    
                    if(parameter==null)
                    {
                        isResolve = false;
                        break;
                    }
                    parameterList.Add(parameter);
                }
                if(isResolve)
                    return Activator.CreateInstance(implementationType, parameterList.ToArray());
            }
            return null;
        }
    }
}
