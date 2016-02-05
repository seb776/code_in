using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace code_in.Views.ConfigView
{
    /// <summary>
    /// Logique d'interaction pour ChangeTheme.xaml
    /// </summary>
    public partial class ChangeTheme : UserControl, ICodeInVisual
    {
        private ResourceDictionary _resourceDictionary = null;
        public ResourceDictionary GetResourceDictionary() { return _resourceDictionary; }
        public ChangeTheme(ResourceDictionary resDict)
        {
            this._resourceDictionary = resDict;
            this.Resources.MergedDictionaries.Add(this._resourceDictionary);
            InitializeComponent();
        }
        public ChangeTheme() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
            // Default constructor should not be called except for xaml tests purpose
        }
    }
}
