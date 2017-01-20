using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class ParenthesizedExprNode : AExpressionNode
    {
        public DataFlowAnchor OperandA
        {
            get;
            private set;
        }

        public ParenthesizedExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer)
            : base(themeResDict, nodalView, linkContainer)
        {
            OperandA = this.CreateAndAddInput<DataFlowAnchor>();
            this.SetType("Parenthesis");
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
        }

        public override void UpdateAnchorAttachAST()
        {
            OperandA.SetASTNodeReference((e) => { (Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.ParenthesizedExpression).Expression = e; });
        }
    }
}
