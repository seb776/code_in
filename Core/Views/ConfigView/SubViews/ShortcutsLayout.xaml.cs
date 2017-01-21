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
using code_in.Exceptions;

namespace code_in.Views.ConfigView.SubViews
{
    /// <summary>
    /// Logique d'interaction pour ShortcutsLayout.xaml
    /// </summary>
    public partial class ShortcutsLayout : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;

        public ShortcutsLayout(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(this._languageResourceDictionary);
            InitializeComponent();
            this.SetLanguageResources("ConfigShortcut");
        }
        public ShortcutsLayout() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new DefaultCtorVisualException(); }

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }

        public void SetThemeResources(String keyPrefix)
        {

        }
        public void SetLanguageResources(String keyPrefix)
        {
            SauvegardeField.SetResourceReference(Label.ContentProperty, "SauvegardeField");
            CloseField.SetResourceReference(Label.ContentProperty, "CloseField");
        }
        #endregion ICodeInVisual

        private void keySaveField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string key = keySaveField.Text.ToUpper();
            if (key.Length == 0 || key[0] < 'A' || key[0] > 'Z')
            {
                key = "S";
                keySaveField.Text = "S";
                MessageBox.Show("Not a letter, put S by default");
            }
            Code_inApplication.keysave = key;
        }

        private void keyCloseField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string key = keyCloseField.Text.ToUpper();
            if (key.Length == 0 || key[0] < 'A' || key[0] > 'Z')
            {
                key = "A";
                keyCloseField.Text = "A";
                MessageBox.Show("Not a letter, put A by default");
            }
            Code_inApplication.keyclose = key;
        }
    }
}
