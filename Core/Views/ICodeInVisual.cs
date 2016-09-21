using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views
{
    /// <summary>
    /// This interface has to be implemented by each visual element of the interface for it to be
    /// configurable via the theme manager.
    /// </summary>
    public interface ICodeInVisual
    {
        ResourceDictionary GetThemeResourceDictionary(); // Each visual element of the application can be modified through a unique resource dict
        void SetThemeResources(String keyPrefix); // Assigns the visual elements the values of the theme dictionary
    }
}
