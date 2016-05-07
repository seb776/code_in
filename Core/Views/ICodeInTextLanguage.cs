using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views
{
    /// <summary>
    /// This interface must be implemented by each visual element of the program in order
    /// to be configurable via the language manager.
    /// </summary>
    interface ICodeInTextLanguage
    {
        ResourceDictionary GetLanguageResourceDictionary(); // Each visual element of the application uses a dictionary for the text content
        void SetLanguageResources(String keyPrefix); // Assigns the visual elements the values of the language dictionary
    }
}
