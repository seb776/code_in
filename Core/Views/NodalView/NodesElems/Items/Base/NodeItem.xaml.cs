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
    /// Interaction logic for NodeItem.xaml
    /// </summary>
    public abstract partial class NodeItem : UserControl, INode, ICodeInVisual
    {
        protected BaseNode _parentNode = null;
        private ResourceDictionary _resourceDictionary = null;
        private NodalView _nodalView = null;
        public ResourceDictionary GetResourceDictionary() { return _resourceDictionary; }
        public void SetParentNode(BaseNode parent) { _parentNode = parent; }
        public BaseNode GetParentNode() { return _parentNode; }
        public void SetNodalView(NodalView nv) { _nodalView = nv; }
        public NodalView GetNodalView() { return _nodalView; }
        public void SetName(String name) 
        {
            this.ItemName.Text = name;
        }
        public String GetName()
        {
            return this.ItemName.Text;
        }
        public abstract void SetDynamicResources(String keyPrefix);
        public BaseNode ParentNode
        {
            get
            {
                return _parentNode;
            }
            set
            {
                _parentNode = value;
            }
        }
        public NodeItem() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }

        protected NodeItem(ResourceDictionary resDict)
        {
            this._resourceDictionary = resDict;
            this.Resources.MergedDictionaries.Add(this._resourceDictionary);
            InitializeComponent();
            _parentNode = null;
        }
        public NodeItem(BaseNode parent) :
            this(parent.ResourceDict)
        {
            System.Diagnostics.Debug.Assert(parent != null, "The parentNode is null");
            _parentNode = parent;
        }
    }
}
