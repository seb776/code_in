using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class UnaryExprNode : AExpressionNode
    {
        public DataFlowAnchor OperandA = null;
        public UnaryExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer)
            : base(themeResDict, nodalView, linkContainer)
        {
            OperandA = this.CreateAndAddInput<DataFlowAnchor>();
            OperandA.SetName("A");
            this.SetThemeResources("UnaryExprNode");
            //this.SetType("UnaryExpr");
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            Debug.Assert(Presenter.GetASTNode() != null);
            var astNode = Presenter.GetASTNode();
            if (astNode is ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression)
            {
                this.SetType((astNode as ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression).OperatorToken.ToString());
            }
        }
    }
}
