using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class ArrayCreateExprNode : AExpressionNode
    {
        public DataFlowAnchor ExprIn = null;
        public ArrayCreateExprNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetType("ArrayCreateExpr");
            ExprIn = this.CreateAndAddInput<DataFlowAnchor>();
        }

        public override void InstantiateASTNode()
        {
            throw new NotImplementedException();
        }
    }
}
