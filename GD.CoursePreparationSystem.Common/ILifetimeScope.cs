using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.CoursePreparationSystem.Common
{
    public interface ILifetimeScope : IDisposable
    {
        TService Resolve<TService>();

        TService Resolve<TService>(string serviceName);

        TService Resolve<TService>(IEnumerable<Autofac.Core.Parameter> parameters);

        TService Resolve<TService>(params Autofac.Core.Parameter[] parameters);
    }
}
