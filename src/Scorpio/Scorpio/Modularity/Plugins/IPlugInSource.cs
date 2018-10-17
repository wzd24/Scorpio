using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Modularity.Plugins
{
    public interface IPlugInSource
    {
        Type[] GetModules();

    }
}
