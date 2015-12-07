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
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.SharedDictionary["BaseNodeColor"] != null, "No BaseNodeColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.SharedDictionary["BaseNodeColorBack"] != null, "No BaseNodeColorBack in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.SharedDictionary["NamespaceNodeColor"] != null, "No NamespaceNodeColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.SharedDictionary["FuncDeclNodeColor"] != null, "No FuncDeclNodeColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.SharedDictionary["PreprocessColor"] != null, "No PreprocessColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.SharedDictionary["IntColor"] != null, "No IntColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.SharedDictionary["CharColor"] != null, "No CharColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.SharedDictionary["FloatingColor"] != null, "No FloatingColor in dictionnary");
        }


        public void setTheme(Models.Theme.IThemeData data) {

            checkResourceTheme();

            // Put random color to each resources of the dictionary
            // Have to take by the next colors in the given class code_inMgr

            Resources.SharedDictionaryManager.SharedDictionary["BaseNodeColor"] = data.getNodeForegroundColor();
            Resources.SharedDictionaryManager.SharedDictionary["BaseNodeColorBack"] = data.getNodeBackgroundColor();
            Resources.SharedDictionaryManager.SharedDictionary["NamespaceNodeColor"] = new SolidColorBrush(Colors.Indigo);
            Resources.SharedDictionaryManager.SharedDictionary["FuncDeclNodeColor"] = new SolidColorBrush(Colors.Yellow);
            Resources.SharedDictionaryManager.SharedDictionary["PreprocessColor"] = new SolidColorBrush(Colors.Tomato);
            Resources.SharedDictionaryManager.SharedDictionary["IntColor"] = new SolidColorBrush(Colors.OliveDrab);
            Resources.SharedDictionaryManager.SharedDictionary["CharColor"] = new SolidColorBrush(Colors.Bisque);
            Resources.SharedDictionaryManager.SharedDictionary["FloatingColor"] = new SolidColorBrush(Colors.HotPink);
        } 
    }
}
