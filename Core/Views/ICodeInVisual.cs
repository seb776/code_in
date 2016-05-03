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
        ResourceDictionary GetLanguageResourceDictionary(); // Each visual element of the application uses a dictionary for the text content
        void SetThemeResources(String keyPrefix); // Assigns the visual elements the values of the theme dictionary
        void SetLanguageResources(String keyPrefix); // Assigns the visual elements the values of the language dictionary
    }
}
