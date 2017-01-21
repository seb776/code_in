using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class IdentifierExprNode : AExpressionNode
    {
        public IdentifierExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer) :
            base(themeResDict, nodalView, linkContainer)
        {
            this.SetType("IdentifierExpr");
        }
        public override void UpdateDisplayedInfosFromPresenter()
        {
            var nodePres = GetNodePresenter();
            var astNode = nodePres.GetASTNode() as ICSharpCode.NRefactory.CSharp.IdentifierExpression;
            this.SetName(astNode.Identifier);
        }

        public override void UpdateAnchorAttachAST()
        {
            // Not necessary we have no inputs
        }
    }
}
