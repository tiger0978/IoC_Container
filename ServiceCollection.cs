using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{ 
    public class ServiceCollection : IServiceCollection
    {
        public Dictionary<string, ServiceDescriptor> dict = new Dictionary<string, ServiceDescriptor>();

        public int Count => dict.Count;
        

        public bool IsReadOnly => throw new NotImplementedException();

        public ServiceDescriptor this[int index]
        {
            get
            {
                return dict.Values.ElementAt(index);
            }
            set
            {
                var key = dict.Keys.ElementAt(index);
                dict[key] = value;
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

        public ServiceProvider BuildServiceProvider()
        {
            return new ServiceProvider(this);
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
            if (!dict.ContainsKey(item.ServiceType.FullName))
            {
                dict.Add(item.ServiceType.FullName, item);
            }
            else
            {
                dict[item.ServiceType.FullName] = item;
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
        
    }
}
