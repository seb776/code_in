using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;
using code_in.Presenters.Nodal;


namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class UsingDeclNode : AOrderedContentNode
    {

        public UsingDeclNode(System.Windows.ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetType("using");
            this.SetThemeResources("UsingDeclNode");
        }

        #region ICodeInVisual
        public override void SetThemeResources(string keyPrefix)
        { /*
            base.SetThemeResources(keyPrefix);
            if (_editIcon != null)
            {
                _editIcon.SetResourceReference(Line.StrokeProperty, keyPrefix + "SecondaryColor");
            } */
        }
        #endregion ICodeInVisual
        public override void RemoveNode(INodeElem node)
        {
            this.ContentLayout.Children.Remove(node as UIElement);
        }

    }
}
