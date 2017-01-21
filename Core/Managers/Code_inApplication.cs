using code_in.Managers;
using code_in.Presenters;
using System;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace code_in
{
    // This stores the managers of code_in
    public static class Code_inApplication
    {
        private static readonly String _themeResourceDictionaryPath = "/code_inCore;component/Models/ThemeResourcesDictionary.xaml";
        private static readonly String _languageResourceDictionaryPath = "/code_inCore;component/Models/LanguageResourcesDictionary.xaml";
        private static IEnvironmentWrapper _environmentWrapper = null;
        private static RootDragNDropManager _dragNDropMgr = null;
        private static ThemePresenter _themePresenter = null;
        private static LanguagePresenter _languagePresenter = null;
        private static ResourceDictionary _languageResourceDictionary = null;
        private static ResourceDictionary _themePreviewResourceDictionary = null;
        private static ResourceDictionary _mainResourceDictionary = null;
        public static string keysave = "S";
        public static string keyclose = "A";

        public static void StartApplication(IEnvironmentWrapper wrapper)
        {
            System.Diagnostics.Debug.Assert(wrapper != null, "You must give a valid wrapper in order to start the application !");
            _environmentWrapper = wrapper;
             ResourceDictionary retResDict = null;
            try
            {
                var reader = new FileStream("../../../core/Models/FrenchResourcesDictionary.xaml", FileMode.Open, FileAccess.Read, FileShare.Read);
                retResDict = XamlReader.Load(reader) as ResourceDictionary;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            LanguagePresenter.ApplyLanguage(retResDict);
            string defaultThemePath = "../../../core/Models/DarkThemeResourcesDictionary.xaml";
            ResourceDictionary retResDictTheme = null;
            try
            {
                var reader = new FileStream(defaultThemePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                retResDictTheme = XamlReader.Load(reader) as ResourceDictionary;
            }
            catch (Exception except)
            {
                Console.Error.WriteLine(except.Message);
            }
            ThemePresenter.ApplyTheme(retResDictTheme);
        }

        #region Accessors
        public static RootDragNDropManager RootDragNDrop
        {
            get
            {
                if (_dragNDropMgr == null)
                    _dragNDropMgr = new RootDragNDropManager();
                return _dragNDropMgr;
            }
        }
        public static ThemePresenter ThemePresenter
        {
            get
            {
                if (_themePresenter == null)
                    _themePresenter = new ThemePresenter();
                return _themePresenter;
            }
        }

        public static LanguagePresenter LanguagePresenter
        {
            get
            {
                if (_languagePresenter == null)
                    _languagePresenter = new LanguagePresenter();
                return _languagePresenter;
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
