using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public abstract class AValueNode : AIONode
    {
        public DataFlowAnchor ExprOut = null;
        public AValueNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer)
            : base(themeResDict, nodalView, linkContainer)
        {
            ExprOut = this.CreateAndAddOutput<DataFlowAnchor>();
            ExprOut.SetName("expression");
            SetThemeResources("AValueNode");
        }
        public override void SetThemeResources(string keyPrefix)
        {
            base.SetThemeResources(keyPrefix);
            this.HeaderLayout.SetResourceReference(Border.BackgroundProperty, keyPrefix + "MainColor");
            this.ContentBorder.SetResourceReference(Border.BorderBrushProperty, keyPrefix + "MainColor");
            this.ContentBorder.SetResourceReference(Border.BackgroundProperty, keyPrefix + "SecondaryColor");
        }
    }
}
