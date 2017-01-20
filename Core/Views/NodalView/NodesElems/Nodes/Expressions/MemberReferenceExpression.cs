using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class MemberRefExprNode : AExpressionNode
    {
        public DataFlowAnchor Target = null;
        public MemberRefExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer)
            : base(themeResDict, nodalView, linkContainer)
        {
            Target = this.CreateAndAddInput<DataFlowAnchor>();
            Target.SetName("Target");
            this.SetType("MemberRef");
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            this.SetName((this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.MemberReferenceExpression).MemberName);
        }

        public override void UpdateAnchorAttachAST()
        {
            Target.SetASTNodeReference((e) => { (Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression).Left = e; });
        }
    }

}
