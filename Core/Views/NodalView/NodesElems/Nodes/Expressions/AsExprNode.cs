using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class AsExprNode : AExpressionNode
    {
        public DataFlowAnchor Input = null;
        public AsExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer) :
            base(themeResDict, nodalView, linkContainer)
        {
            this.SetType("As");
            Input = this.CreateAndAddInput<DataFlowAnchor>();
        }
        public override void UpdateAnchorAttachAST()
        {
            if (Presenter.GetASTNode() is ICSharpCode.NRefactory.CSharp.AsExpression)
            {
                var asExpr = Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.AsExpression;
                Input.SetASTNodeReference((e) => { asExpr.Expression = e; });
            }
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
        }
    }
}
