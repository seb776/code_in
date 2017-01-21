using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class IsExprNode : AExpressionNode
    {
        public DataFlowAnchor Input = null;

        public IsExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer) :
            base(themeResDict, nodalView, linkContainer)
        {
            this.SetType("Is");
            Input = this.CreateAndAddInput<DataFlowAnchor>();
        }

        public override void UpdateAnchorAttachAST()
        {
            // useless here
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
        }
    }
}
