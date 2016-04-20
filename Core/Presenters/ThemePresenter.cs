using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace code_in.Presenters
{
    public class ThemePresenter
    {
        ResourceDictionary _currentLoadedResDict = null;
        bool _savedResDict = false;
        public ThemePresenter()
        {
        }

        public void Save(String path)
        {
            if (_currentLoadedResDict != null)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(path);
                    XamlWriter.Save(_currentLoadedResDict, writer);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }
            else
            {
                throw new Exception("You can not save default theme");
            }
        }

        public void Load(String path)
        {
            try
            {
                var reader = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                ResourceDictionary retResDict = XamlReader.Load(reader) as ResourceDictionary;
                _currentLoadedResDict = retResDict;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        public void ApplyThemeToPreview(ResourceDictionary resDict)
        {
            foreach (var t in resDict.Keys)
            {
                Code_inApplication.ThemePreviewResourceDictionary[t as String] = resDict[t as String];
            }
        }


        /// <summary>
        /// Apply 'resDict' to the software.
        /// </summary>
        /// <param name="resDict">The theme to apply.</param>
        public void ApplyTheme(ResourceDictionary resDict)
        {
            foreach (var t in resDict.Keys)
            {
                Code_inApplication.MainResourceDictionary[t as String] = resDict[t as String];
            }
        }
    }
}
