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

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    /// <summary>
    /// Interaction logic for BaseNode.xaml
    /// </summary>
    public abstract partial class BaseNode : UserControl, INodeElem, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        public BaseNode(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
        }
        public BaseNode() :
            this(Code_inApplication.MainResourceDictionary)
        {
        }

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }

        public abstract void SetThemeResources(string keyPrefix);
        #endregion ICodeInVisual
    }
}
