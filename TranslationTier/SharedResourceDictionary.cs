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
        public static ResourceDictionary SharedDictionary
        {
            get
            {
                if (_sharedDictionary == null)
                {
                    System.Uri resourceLocater = new System.Uri("/TranslationTier;component/ResourcesDictionary.xaml",
                                        System.UriKind.Relative);
                    _sharedDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
                }

                return _sharedDictionary;
            }
        }

        private static ResourceDictionary _sharedDictionary;
    }
}
