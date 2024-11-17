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

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public Microsoft.Extensions.DependencyInjection.ServiceDescriptor this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ServiceCollection AddTransient<T, T1>() 
        {
            ServiceDescriptor descriptor = new ServiceDescriptor(Lifetime.Transient, typeof(T1));
            dict.Add(typeof(T).Name, descriptor);
            return this;
        }

        public ServiceCollection AddTransient<T>(Func<ServiceProvider,object> func)
        {
            ServiceDescriptor descriptor = new ServiceDescriptor(Lifetime.Transient, typeof(T), func);
            dict.Add(typeof(T).Name, descriptor);
            return this;
        }

        public ServiceCollection AddSingleton<T, T1>()
        {
            ServiceDescriptor descriptor = new ServiceDescriptor(Lifetime.Singleton, typeof(T1));
            dict.Add(typeof(T).Name, descriptor);
            return this;
        }

        public ServiceCollection AddSingleton<T>(Func<ServiceProvider,object> func)
        {
            ServiceDescriptor descriptor = new ServiceDescriptor(Lifetime.Singleton, typeof(T), func);
            dict.Add(typeof(T).Name, descriptor);
            return this;
        }

        public ServiceProvider BuildServiceProvider()
        {
            return new ServiceProvider(this);
        }

        public int IndexOf(Microsoft.Extensions.DependencyInjection.ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Microsoft.Extensions.DependencyInjection.ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(Microsoft.Extensions.DependencyInjection.ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Microsoft.Extensions.DependencyInjection.ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Microsoft.Extensions.DependencyInjection.ServiceDescriptor[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Microsoft.Extensions.DependencyInjection.ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Microsoft.Extensions.DependencyInjection.ServiceDescriptor> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        
    }
}
