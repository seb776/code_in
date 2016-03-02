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
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using code_in.Views.NodalView.NodesElems;

namespace code_in.Views.NodalView.NodesElems.Items.Base
{
    /// <summary>
    /// Interaction logic for ANodeItem.xaml
    /// </summary>
    public abstract partial class ANodeItem : UserControl, INodeElem, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        protected INodeElem _parentView = null;
        private IVisualNodeContainerDragNDrop _rootView = null;

        protected ANodeItem(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
        }
        protected ANodeItem() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        #region INodeElem
        public void SetParentView(INodeElem parent) { _parentView = parent; }
        public INodeElem GetParentView() { return _parentView; }
        public void SetRootView(IVisualNodeContainerDragNDrop root) { _rootView = root; }
        public IVisualNodeContainerDragNDrop GetRootView() { return _rootView; }
        public void RemoveNode(INodeElem node) { }
        public void SetName(String name)
        {
            this.ItemName.Text = name;
        }
        public String GetName()
        {
            return this.ItemName.Text;
        }
        #endregion INodeElem
        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public abstract void SetDynamicResources(String keyPrefix);

        #endregion ICodeInVisual
    } // Class
} // Namespace
