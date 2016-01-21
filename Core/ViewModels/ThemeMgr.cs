using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace code_in.ViewModels
{
    public class ThemeMgr
    {
        private code_inMgr _mainMgr;

        public ThemeMgr(code_inMgr mainMgr) {
            this._mainMgr = mainMgr;
        }

        private void checkResourceTheme()
        {
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["BaseNodeColor"] != null, "No BaseNodeColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["BaseNodeColorBack"] != null, "No BaseNodeColorBack in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["NamespaceNodeColor"] != null, "No NamespaceNodeColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["FuncDeclNodeColor"] != null, "No FuncDeclNodeColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["PreprocessColor"] != null, "No PreprocessColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["IntColor"] != null, "No IntColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["CharColor"] != null, "No CharColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["FloatingColor"] != null, "No FloatingColor in dictionnary");
        }


        public void setTheme(Models.Theme.ThemeData data) {
            checkResourceTheme();

            // Put random color to each resources of the dictionary
            // Have to take by the next colors in the given class code_inMgr

            Resources.SharedDictionaryManager.MainResourceDictionary["BaseNodeColor"] = new SolidColorBrush(Colors.GreenYellow);
            Resources.SharedDictionaryManager.MainResourceDictionary["BaseNodeColorBack"] = new SolidColorBrush(Colors.Gray);
            Resources.SharedDictionaryManager.MainResourceDictionary["NamespaceNodeColor"] = new SolidColorBrush(Colors.Indigo);
            Resources.SharedDictionaryManager.MainResourceDictionary["FuncDeclNodeColor"] = new SolidColorBrush(Colors.Yellow);
            Resources.SharedDictionaryManager.MainResourceDictionary["PreprocessColor"] = new SolidColorBrush(Colors.Tomato);
            Resources.SharedDictionaryManager.MainResourceDictionary["IntColor"] = new SolidColorBrush(Colors.OliveDrab);
            Resources.SharedDictionaryManager.MainResourceDictionary["CharColor"] = new SolidColorBrush(Colors.Bisque);
            Resources.SharedDictionaryManager.MainResourceDictionary["FloatingColor"] = new SolidColorBrush(Colors.HotPink);
        } 
    }
}
