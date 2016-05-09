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

namespace code_in.Views.NodalView.NodesElems.Items.Assets
{
    /// <summary>
    /// Interaction logic for ItemModifiers.xaml
    /// </summary>
    public partial class ItemModifiers : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        public ItemModifiers(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(_themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(_languageResourceDictionary);
            InitializeComponent();
        }
        public ItemModifiers() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }

        #region This
        public void SetModifiers(String[] modifiers)
        {
            this._modifiersList.Children.Clear();
            if (modifiers.Count() != 0)
            {
                this._modifiersBorder.IsEnabled = false;
                this._modifiersBorder.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                this._modifiersBorder.IsEnabled = true;
                this._modifiersBorder.Visibility = System.Windows.Visibility.Visible;
            }
            foreach (var mod in modifiers)
            {
                Label lbl = new Label();
                //lbl.SetResourceReference(Label.ForegroundProperty, "whatever");
                lbl.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xA2, 0xFF)); // TODO replace this by the line above for the value to be based on theme
                this._modifiersList.Children.Add(lbl);
            }
        }
        #endregion This

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }

        public void SetThemeResources(String keyPrefix)
        {

        }
        #endregion ICodeInVisual
    }
}
