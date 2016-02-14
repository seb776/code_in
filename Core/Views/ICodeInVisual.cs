using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views
{
    interface ICodeInVisual
    {
        ResourceDictionary GetThemeResourceDictionary(); // Each visual element of the application can be modified through a unique resource dict
        void SetDynamicResources(String keyPrefix);
    }
}
