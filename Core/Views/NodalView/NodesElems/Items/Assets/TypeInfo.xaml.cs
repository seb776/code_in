using code_in.Exceptions;
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
    /// Interaction logic for TypeInfo.xaml
    /// </summary>
    public partial class TypeInfo : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;

        public TypeInfo(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(this._languageResourceDictionary);
            InitializeComponent();
        }
        public TypeInfo() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new DefaultCtorVisualException(); }

        public void SetTypeFromString(String type, params string[] subTypes)
        {
            if (subTypes != null && subTypes.Count() > 0)
            {
                this.SubTypesGroup.Visibility = System.Windows.Visibility.Visible;
                this.SubTypesGroup.IsEnabled = true;
                this.SubTypes.Children.Clear();
                foreach (var s in subTypes)
                {
                    Label lbl = new Label();
                    lbl.Content = s;
                    lbl.Foreground = new SolidColorBrush(Colors.White);
                    this.SubTypes.Children.Add(lbl);
                }
            }
            else
            {
                this.SubTypesGroup.Visibility = System.Windows.Visibility.Collapsed;
                this.SubTypesGroup.IsEnabled = false;
                this.SubTypes.Children.Clear();
            }
            this.TypeLabel.Content = type;
        }

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
    }
}
