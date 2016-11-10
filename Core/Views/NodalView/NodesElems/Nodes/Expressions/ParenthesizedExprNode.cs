using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    class ParenthesizedExprNode : AExpressionNode
    {
        public DataFlowAnchor OperandA = null;

        public ParenthesizedExprNode(ResourceDictionary themeResDict, INodalView nodalView)
            : base(themeResDict, nodalView)
        {
            OperandA = this.CreateAndAddInput<DataFlowAnchor>();
        }
        public override void InstantiateASTNode()
        {
            throw new NotImplementedException();
        }
    }
}
