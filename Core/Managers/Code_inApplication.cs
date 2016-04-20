using code_in.Presenters;
using System;
using System.Windows;

namespace code_in
{
    // This stores the managers of code_in
    public static class Code_inApplication
    {
        private static readonly String _themeResourceDictionaryPath = "/code_inCore;component/Models/ThemeResourcesDictionary.xaml";
        private static readonly String _languageResourceDictionaryPath = "/code_inCore;component/Models/LanguageResourcesDictionary.xaml";
        private static IEnvironmentWrapper _environmentWrapper = null;
        private static ThemePresenter _themePresenter = null;
        private static ResourceDictionary _languageResourceDictionary = null;
        private static ResourceDictionary _themePreviewResourceDictionary = null;
        private static ResourceDictionary _mainResourceDictionary = null;
        public static void StartApplication(IEnvironmentWrapper wrapper)
        {
            System.Diagnostics.Debug.Assert(wrapper != null, "You must give a valid wrapper in order to start the application !");
            _environmentWrapper = wrapper;
            // Code_inApplication.ThemeMgr.setMainTheme(new DefaultThemeData()); // TODO when theme management is complete and functional, load default theme
        }

        #region Accessors
        public static ThemePresenter ThemePresenter
        {
            get
            {
                if (_themePresenter == null)
                    _themePresenter = new ThemePresenter();
                return _themePresenter;
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
                    System.Uri resourceLocater = new System.Uri(_languageResourceDictionaryPath, UriKind.Relative);
                    _languageResourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
                }
                return _languageResourceDictionary;
            }
        }
        public static ResourceDictionary MainResourceDictionary
        {
            get
            {
                if (_mainResourceDictionary == null)
                {
                    System.Uri resourceLocater = new System.Uri(_themeResourceDictionaryPath, System.UriKind.Relative);
                    _mainResourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
                }
                return _mainResourceDictionary;
            }
        }
        public static ResourceDictionary ThemePreviewResourceDictionary
        {
            get
            {
                if (_themePreviewResourceDictionary == null)
                {
                    System.Uri resourceLocater = new System.Uri(_themeResourceDictionaryPath, System.UriKind.Relative);
                    _themePreviewResourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
                }
                return _themePreviewResourceDictionary;
            }
        }
        #endregion Accessors
    }
}
