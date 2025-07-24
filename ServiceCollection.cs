using IoC_Container.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{ 
    public class ServiceCollection : IServiceCollection
    {
        internal Dictionary<string, List<ServiceDescriptor>> dict = new Dictionary<string, List<ServiceDescriptor>>();

        public int Count => dict.Count;
        
        public ServiceCollection()
        {
            AddSingleton<IPresenterFactory, PresenterFactory>();
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public ServiceDescriptor this[int index]
        {
            get
            {
                return dict.Values.ElementAt(index).LastOrDefault();
            }
            set
            {
                //var key = dict.Keys.ElementAt(index);
                //dict[key] = value;
            }
        }
        public ServiceCollection AddTransient<T, T1>() 
        {
            ServiceDescriptor descriptor = new ServiceDescriptor(typeof(T), typeof(T1), ServiceLifetime.Transient);
            Add(descriptor);
            return this;
        }

        public ServiceCollection AddTransient<T>(Func<IServiceProvider,object> func)
        {
            ServiceDescriptor descriptor = new ServiceDescriptor(typeof(T), func, ServiceLifetime.Transient);
            Add(descriptor);
            return this;
        }

        public ServiceCollection AddSingleton<T, T1>()
        {

            ServiceDescriptor descriptor = new ServiceDescriptor(typeof(T), typeof(T1), ServiceLifetime.Singleton);
            Add(descriptor);
            return this;
        }

        public ServiceCollection AddSingleton<T>(Func<IServiceProvider,object> func)
        {
            ServiceDescriptor descriptor = new ServiceDescriptor(typeof(T), func, ServiceLifetime.Singleton);
            Add(descriptor);
            return this;
        }

        public ServiceCollection AutoInjectMVP(Assembly assemblies)
        {
            AutoInject(assemblies, "Presenter");
            AutoInject(assemblies, "Repository");
            return this;
        }

        public ServiceProvider BuildServiceProvider()
        {
            var provider = new ServiceProvider(this);
            ServiceDescriptor serviceDescriptor = new ServiceDescriptor(typeof(IServiceProvider),provider);  
            Add(serviceDescriptor);
            return provider;
        }

        public int IndexOf(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(ServiceDescriptor item)
        {
            if (dict.ContainsKey(item.ServiceType.FullName))
            {
                dict[item.ServiceType.FullName].Add(item);
                //dict.Add(item.ServiceType.FullName, item);
            }
            else
            {
                var list = new List<ServiceDescriptor>();
                list.Add(item);
                dict[item.ServiceType.FullName] = list;
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private void AutoInject(Assembly assemblies, string suffixName)
        {
            var types = assemblies.DefinedTypes.Where(x => x.Name.Contains(suffixName)).ToList();
            foreach (var type in types)
            {
                if (type.BaseType != null && type.BaseType.IsAbstract && type.BaseType.Name.Contains(suffixName))
                {
                    Add(new ServiceDescriptor(type.BaseType, type, ServiceLifetime.Singleton));
                }
                if (type.ImplementedInterfaces.Count() == 0)
                {
                    continue;
                }
                foreach (var implementedInterface in type.ImplementedInterfaces.Where(x => x.Name.Contains(suffixName)))
                {
                    Add(new ServiceDescriptor(implementedInterface, type, ServiceLifetime.Singleton));
                }
            }
            var injectedTypes = assemblies.DefinedTypes.Where(x => x.CustomAttributes.
                                                   Any(attr => attr.AttributeType.Name == typeof(SingletonAttribute).Name ||
                                                               attr.AttributeType.Name == typeof(TransientAttribute).Name)).ToList();
            foreach (var injectType in injectedTypes)
            {
                var temp = injectType.GetCustomAttribute<TransientAttribute>();
                ServiceDescriptor serviceDescriptor = temp == null ?
                                                new ServiceDescriptor(injectType.BaseType, injectType, ServiceLifetime.Singleton) :
                                                new ServiceDescriptor(injectType.BaseType, injectType, ServiceLifetime.Transient);
                Add(serviceDescriptor);
            }
        }
        
    }
}
