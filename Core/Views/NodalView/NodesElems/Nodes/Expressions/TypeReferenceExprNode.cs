using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class TypeReferenceExprNode : AExpressionNode
    {
        public DataFlowAnchor Input = null;

        public TypeReferenceExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer) :
            base(themeResDict, nodalView, linkContainer)
        {
            this.SetType("TypeRef");
        }

        public override void UpdateAnchorAttachAST()
        {
            // Useless here
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
        }
    }
}
