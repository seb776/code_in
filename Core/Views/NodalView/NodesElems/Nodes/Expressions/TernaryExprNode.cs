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
    public class TernaryExprNode : AExpressionNode
    {
        public DataFlowAnchor OperandA = null;
        public DataFlowAnchor OperandB = null;
        public DataFlowAnchor OperandC = null;
        public TernaryExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer)
            : base(themeResDict, nodalView, linkContainer)
        {
            OperandA = this.CreateAndAddInput<DataFlowAnchor>();
            OperandB = this.CreateAndAddInput<DataFlowAnchor>();
            OperandC = this.CreateAndAddInput<DataFlowAnchor>();
            OperandA.SetName("A");
            OperandB.SetName("B");
            OperandC.SetName("C");
        }
    }
}
