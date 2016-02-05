using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    public abstract class OrderedContentNode : BaseNode
    {
        public override void SetDynamicResources(String keyPrefix)
        { }
        System.Windows.Controls.StackPanel _orderedLayout;
        public OrderedContentNode()
            : this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }

        public OrderedContentNode(System.Windows.ResourceDictionary resDict)
            : base(resDict)
        {
            //this.DisableFeatures(EFeatures.CONTAINSMODIFIERS, EFeatures.EXPENDABLES, EFeatures.ISFLOWNODE);
            this.SetColorResource("NamespaceNodeColor");
            this.SetNodeType("Namespace");
            this.SetName("System.Collections.Generic.TestDeLaMuerte");
            _orderedLayout = new System.Windows.Controls.StackPanel();
            this.ContentGrid.Children.Add(_orderedLayout);
        }
        public override void AddNode<T>(T node)
        {
            this._orderedLayout.Children.Add(node as UIElement);
        }
        
    }
}
