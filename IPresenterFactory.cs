using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{
    public interface IPresenterFactory
    {
        TPresenter CreatePresneter<TPresenter, TView>(TView view)
            where TPresenter : class
            where TView : class;
        TPresenter CreatePresneter<TPresenter, TView>(TView view, Type presenterType)
            where TPresenter : class
            where TView : class;
    }
}
