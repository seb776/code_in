using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Resources
{
    public static class SharedDictionaryManager
    {
        public static ResourceDictionary MainResourceDictionary
        {
            get
            {
                if (_mainResourceDictionary == null)
                {
                    System.Uri resourceLocater = new System.Uri("/TranslationTier;component/ResourcesDictionary.xaml",
                                        System.UriKind.Relative);
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
                    System.Uri resourceLocater = new System.Uri("/TranslationTier;component/ResourcesDictionary.xaml",
                                        System.UriKind.Relative);
                    _themePreviewResourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
                }
                return _themePreviewResourceDictionary;
            }
        }

        public static ResourceDictionary LanguageResourcesDictionary
        {
            get
            {
                if (_themePreviewResourceDictionary == null)
                {
                    System.Uri resourceLocater = new System.Uri("/TranslationTier;component/LanguageResourcesDictionary.xaml",
                                        System.UriKind.Relative);
                    _languageResourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
                }
                return _languageResourceDictionary;
            }
        }

        private static ResourceDictionary _themePreviewResourceDictionary = null;
        private static ResourceDictionary _mainResourceDictionary = null;
        private static ResourceDictionary _languageResourceDictionary = null;
    }
}
