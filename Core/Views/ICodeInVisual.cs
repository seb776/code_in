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
        /// <summary>
        ///  Each visual element of the application can be modified through a unique resource dict
        /// </summary>
        /// <returns>The attached resource dictionary for theme management.</returns>
        ResourceDictionary GetThemeResourceDictionary();
        /// <summary>
        /// Assigns the visual elements the values of the theme dictionary.
        /// </summary>
        /// <param name="keyPrefix">The keyPrefix used to link with the resource dictionary.</param>
        void SetThemeResources(String keyPrefix);
    }
}
