using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlFu.Configuration.Internals;
using SqlFu.Providers;

namespace SqlFu.Builders
{
    public interface IQueryTemplate
    {
        string GetTemplate(ParametersManager paramManager);

    }
    public interface IQueryTemplate<T> : IQueryTemplate
    {
        
    }
}
