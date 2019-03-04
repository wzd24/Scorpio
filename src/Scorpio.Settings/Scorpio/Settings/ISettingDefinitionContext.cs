using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Settings
{
    public interface ISettingDefinitionContext
    {
        SettingDefinition GetOrNull(string name);

        void Add(params SettingDefinition[] definitions);
    }
}
