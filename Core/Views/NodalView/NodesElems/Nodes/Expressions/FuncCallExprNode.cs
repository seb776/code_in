using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class FuncCallExprNode : AExpressionNode
    {
        public DataFlowAnchor TargetIn = null; // The method name
        public FuncCallExprNode(ResourceDictionary themeResDict, INodalView nodalView)
            : base(themeResDict, nodalView)
        {
            this.SetType("FuncCallExpr");
            TargetIn = this.CreateAndAddInput<DataFlowAnchor>();
            TargetIn.SetName("MethodName");
        }
        public override void InstantiateASTNode()
        {
            this.GetNodePresenter().SetASTNode(new ICSharpCode.NRefactory.CSharp.InvocationExpression());
            throw new NotImplementedException();
        }
    }
}
