using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace code_in.ViewModels
{
    interface IThemeData { }

    public class ThemeMgr
    {
        private code_inMgr _mainMgr;

        public ThemeMgr(code_inMgr mainMgr) {
            this._mainMgr = mainMgr;
        }

        private void checkResourceTheme()
        {
            System.Diagnostics.Debug.Assert(Views.SharedDictionaryManager.SharedDictionary["BaseNodeColor"] != null, "No BaseNodeColor in dictionnary");
            System.Diagnostics.Debug.Assert(Views.SharedDictionaryManager.SharedDictionary["BaseNodeColorBack"] != null, "No BaseNodeColorBack in dictionnary");
            System.Diagnostics.Debug.Assert(Views.SharedDictionaryManager.SharedDictionary["NamespaceNodeColor"] != null, "No NamespaceNodeColor in dictionnary");
            System.Diagnostics.Debug.Assert(Views.SharedDictionaryManager.SharedDictionary["FuncDeclNodeColor"] != null, "No FuncDeclNodeColor in dictionnary");
            System.Diagnostics.Debug.Assert(Views.SharedDictionaryManager.SharedDictionary["PreprocessColor"] != null, "No PreprocessColor in dictionnary");
            System.Diagnostics.Debug.Assert(Views.SharedDictionaryManager.SharedDictionary["IntColor"] != null, "No IntColor in dictionnary");
            System.Diagnostics.Debug.Assert(Views.SharedDictionaryManager.SharedDictionary["CharColor"] != null, "No CharColor in dictionnary");
            System.Diagnostics.Debug.Assert(Views.SharedDictionaryManager.SharedDictionary["FloatingColor"] != null, "No FloatingColor in dictionnary");
        }

        public void setTheme() {

            checkResourceTheme();

            // Put random color to each resources of the dictionary
            // Have to take by the next colors in the given class code_inMgr

            Views.SharedDictionaryManager.SharedDictionary["BaseNodeColor"] = new SolidColorBrush(Colors.Red);
            Views.SharedDictionaryManager.SharedDictionary["BaseNodeColorBack"] = new SolidColorBrush(Colors.Orange);
            Views.SharedDictionaryManager.SharedDictionary["NamespaceNodeColor"] = new SolidColorBrush(Colors.Indigo);
            Views.SharedDictionaryManager.SharedDictionary["FuncDeclNodeColor"] = new SolidColorBrush(Colors.Yellow);
            Views.SharedDictionaryManager.SharedDictionary["PreprocessColor"] = new SolidColorBrush(Colors.Tomato);
            Views.SharedDictionaryManager.SharedDictionary["IntColor"] = new SolidColorBrush(Colors.OliveDrab);
            Views.SharedDictionaryManager.SharedDictionary["CharColor"] = new SolidColorBrush(Colors.Bisque);
            Views.SharedDictionaryManager.SharedDictionary["FloatingColor"] = new SolidColorBrush(Colors.HotPink);
        } 
    }
}
