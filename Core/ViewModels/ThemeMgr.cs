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


        public void setTheme(System.Windows.ResourceDictionary resDict, Models.Theme.ThemeData data) {
            checkResourceTheme();

            // Put random color to each resources of the dictionary
            // Have to take by the next colors in the given class code_inMgr

            resDict["BaseNodeColor"] = new SolidColorBrush(setColorFromByte4(data.ForegroundColor));
            resDict["BaseNodeColorBack"] = new SolidColorBrush(setColorFromByte4(data.BackgroundColor));
            resDict["NamespaceNodeColor"] = new SolidColorBrush(Colors.Indigo);
            resDict["FuncDeclNodeColor"] = new SolidColorBrush(Colors.Yellow);
            resDict["PreprocessColor"] = new SolidColorBrush(Colors.Tomato);
            resDict["IntColor"] = new SolidColorBrush(Colors.OliveDrab);
            resDict["CharColor"] = new SolidColorBrush(Colors.Bisque);
            resDict["FloatingColor"] = new SolidColorBrush(Colors.HotPink);
        }

        public Color setColorFromByte4(Byte[] color)
        {
            Color res = new Color();

            System.Diagnostics.Debug.Assert(color.Count() > 3);
            res.A = color[0];
            res.R = color[1];
            res.G = color[2];
            res.B = color[3];
            return res;
        }

        public void setMainTheme(Models.Theme.ThemeData data)
        {
            setTheme(Resources.SharedDictionaryManager.MainResourceDictionary, data);
        }
        public void setPreviewTheme(Models.Theme.ThemeData data)
        {
            setTheme(Resources.SharedDictionaryManager.ThemePreviewResourceDictionary, data);
        }

    }
}
