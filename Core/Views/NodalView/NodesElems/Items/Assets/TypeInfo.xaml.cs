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

namespace code_in.Views.NodalView.Nodes.Items.Assets
{
    /// <summary>
    /// Interaction logic for TypeInfo.xaml
    /// </summary>
    public partial class TypeInfo : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public void SetDynamicResources(String keyPrefix)
        {

        }
        public TypeInfo(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
        }
        public TypeInfo() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }
        public void SetTypeFromString(String type)
        {
            // Not implemented yet
        }
    }
}
