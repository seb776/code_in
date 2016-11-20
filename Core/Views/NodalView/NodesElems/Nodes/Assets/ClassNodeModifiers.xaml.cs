using code_in.Exceptions;
using code_in.Presenters.Nodal;
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

namespace code_in.Views.NodalView.NodesElems.Nodes.Assets
{
    /// <summary>
    /// Interaction logic for NodeModifiers.xaml
    /// </summary>
    public partial class ClassNodeModifiers : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        public ClassNodeModifiers(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(_themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(_languageResourceDictionary);
            InitializeComponent();
        }
        public ClassNodeModifiers() :
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
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual

        public void SetAccessModifiers(EAccessModifier mod)
        {
            switch (mod)
            {
                case EAccessModifier.PUBLIC:
                    this.AccessModifierBlock.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xA2, 0xFF));
                    this.AccessModifierSymbol.Content = "+";
                    break;
                case EAccessModifier.PRIVATE:
                    this.AccessModifierBlock.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x00, 0x23));
                    this.AccessModifierSymbol.Content = "-";
                    break;
                case EAccessModifier.PROTECTED:
                    this.AccessModifierBlock.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x51, 0x00));
                    this.AccessModifierSymbol.Content = "#";
                    break;
                case EAccessModifier.INTERNAL:
                    this.AccessModifierBlock.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xCF, 0x09));
                    this.AccessModifierSymbol.Content = "~";
                    break;
                case EAccessModifier.PROTECTED_INTERNAL:
                    this.AccessModifierBlock.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xBC, 0xCF, 0x00));
                    this.AccessModifierSymbol.Content = "#~";
                    break;
            }
        }

        public void SetModifiers(String[] modifiers)
        {
            this.ModifiersList.Children.Clear();
            foreach (var mod in modifiers)
            {
                Label lbl = new Label();
                lbl.Content = mod;
                lbl.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xA2, 0xFF));
                this.ModifiersList.Children.Add(lbl);
            }
        }

    }
}
