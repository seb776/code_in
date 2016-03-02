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
        public DataFlowItem OperandA = null;
        public DataFlowItem OperandB = null;
        public DataFlowItem OperandC = null;
        public TernaryExprNode(ResourceDictionary themeResDict)
            : base(themeResDict)
        {
            OperandA = this.CreateAndAddInput<DataFlowItem>();
            OperandB = this.CreateAndAddInput<DataFlowItem>();
            OperandC = this.CreateAndAddInput<DataFlowItem>();
            OperandA.SetName("A");
            OperandB.SetName("B");
            OperandC.SetName("C");
        }
    }
}
