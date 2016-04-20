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
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;

namespace code_in.Views.ConfigView.SubViews
{
    /// <summary>
    /// Logique d'interaction pour ShortcutsLayout.xaml
    /// </summary>
    public partial class ShortcutsLayout : UserControl, ICodeInVisual
    {
        public void SetDynamicResources(String keyPrefix)
        {

        }
        private ResourceDictionary _themeResourceDictionary;
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ShortcutsLayout(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
        }
        public ShortcutsLayout() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        private void Button_Confirm(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {

        }


        public ResourceDictionary GetLanguageResourceDictionary()
        {
            throw new NotImplementedException();
        }

        public void SetLanguageResources()
        {
            throw new NotImplementedException();
        }
    }
}
