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
    /// Interaction logic for ParametersList.xaml
    /// </summary>
    public partial class ParametersList : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        public ParametersList(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
        }
        public ParametersList() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        public void AddParameter(String type)
        {
            var lbl = new Label();
            lbl.Content = type;
            lbl.Foreground = new SolidColorBrush(Color.FromRgb(0x1C, 0xC2, 0xEC));
            this.ParamsList.Children.Add(lbl);
        }

        public void AddParameter(int index, String type)
        {
            var lbl = new Label();
            lbl.Content = type;
            lbl.Foreground = new SolidColorBrush(Color.FromRgb(0x1C, 0xC2, 0xEC));
            this.ParamsList.Children.Insert(index, lbl);
        }

        public void RemoveParameter(int index)
        {
            this.ParamsList.Children.RemoveAt(index);
        }

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public void SetDynamicResources(String keyPrefix) { }
        #endregion ICodeInVisual
    }
}
