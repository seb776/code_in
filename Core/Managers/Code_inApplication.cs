using code_in.Models.Theme;
using code_in.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in
{
    // This stores the managers of code_in
    public static class Code_inApplication
    {
        public static void StartApplication(IEnvironmentWrapper wrapper)
        {
            System.Diagnostics.Debug.Assert(wrapper != null, "You must give a valid wrapper in order to start the application !");
            _environmentWrapper = wrapper;
            // Code_inApplication.ThemeMgr.setMainTheme(new DefaultThemeData()); // TODO when theme management is complete and functional, load default theme
        }
        public static IEnvironmentWrapper _environmentWrapper = null;
        private static ThemeMgr _themeMgr = null;

        public static ThemeMgr ThemeMgr
        {
            get
            {
                if (_themeMgr == null)
                    _themeMgr = new ThemeMgr();
                return _themeMgr;
            }
        }
        public static IEnvironmentWrapper EnvironmentWrapper
        {
            get
            {
                return _environmentWrapper;
            }
        }
        public static ResourceDictionary LanguageResourcesDictionary
        {
            get
            {
                if (_languageResourceDictionary == null)
                {
                    System.Uri resourceLocater = new System.Uri("/Core;component/Models/LanguageResourcesDictionary.xaml",
                                        System.UriKind.Relative);
                    _languageResourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
                }
                return _languageResourceDictionary;
            }
        }
        private static ResourceDictionary _languageResourceDictionary = null;
    }
}
