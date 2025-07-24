using Microsoft.Extensions.DependencyInjection;
using NLog.Targets;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{
    internal class PresenterFactory : IPresenterFactory
    {
        Dictionary<string, List<ServiceDescriptor>> dict;
        ServiceProvider provider;

        public PresenterFactory(IServiceProvider serviceProvider)
        {
            provider = (ServiceProvider)serviceProvider;
            dict = provider._services.dict;
        }


        public TPresenter CreatePresneter<TPresenter, TView>(TView view)
            where TPresenter : class
            where TView : class
        {
            var serviceDescriptors = dict[typeof(TPresenter).FullName];
            var presenterType = serviceDescriptors.LastOrDefault().ImplementationType;
            var constructor = presenterType.GetConstructors().OrderByDescending(x => x.GetParameters().Length).FirstOrDefault();
            var parameters = constructor.GetParameters();
            List<object> parameterList = new List<object>();
            bool isResolve = true;

            var parms = constructor.GetParameters();
            foreach (var parm in parms)
            {
                if (parm.ParameterType == typeof(TView))
                {
                    parameterList.Add(view);
                    continue;
                }
                object parameter = provider.GetService(parm.ParameterType);
                if (parameter == null)
                {
                    isResolve = false;
                    break;
                }
                parameterList.Add(parameter);
            }
            if (isResolve)
                return (TPresenter)Activator.CreateInstance(presenterType, parameterList.ToArray());
            return null;
        }

        public TPresenter CreatePresneter<TPresenter, TView>(TView view , Type presenterType)
            where TPresenter : class
            where TView : class
        {

            var serviceDescriptors = dict[typeof(TPresenter).FullName];
            var constructor = presenterType.GetConstructors().OrderByDescending(x => x.GetParameters().Length).FirstOrDefault();
            var parameters = constructor.GetParameters();
            List<object> parameterList = new List<object>();
            bool isResolve = true;

            var parms = constructor.GetParameters();
            foreach (var parm in parms)
            {
                if (parm.ParameterType == typeof(TView))
                {
                    parameterList.Add(view);
                    continue;
                }
                object parameter = provider.GetService(parm.ParameterType);
                if (parameter == null)
                {
                    isResolve = false;
                    break;
                }
                parameterList.Add(parameter);
            }
            if (isResolve)
                return (TPresenter)Activator.CreateInstance(presenterType, parameterList.ToArray());
            return null;
        }
    }
}
