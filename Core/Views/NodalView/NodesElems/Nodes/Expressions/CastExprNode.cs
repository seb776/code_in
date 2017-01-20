using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class CastExprNode : AExpressionNode
    {
        public DataFlowAnchor Input = null;

        public CastExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer) :
            base(themeResDict, nodalView, linkContainer)
        {
            this.SetType("Is");
            Input = this.CreateAndAddInput<DataFlowAnchor>();
        }

        public override void UpdateAnchorAttachAST()
        {
            if (Presenter.GetASTNode() is ICSharpCode.NRefactory.CSharp.CastExpression)
            {
                var isExpr = Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.CastExpression;
                Input.SetASTNodeReference((e) => { isExpr.Expression = e; });
            }
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            throw new NotImplementedException();
        }
    }
}
