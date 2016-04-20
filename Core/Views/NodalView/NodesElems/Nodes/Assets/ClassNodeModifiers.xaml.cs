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
        public ClassNodeModifiers(ResourceDictionary themeResDict)
        {
            _themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(_themeResourceDictionary);
            InitializeComponent();
        }
        public ClassNodeModifiers() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public void SetDynamicResources(String keyPrefix)
        {

        }
        #endregion ICodeInVisual


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
