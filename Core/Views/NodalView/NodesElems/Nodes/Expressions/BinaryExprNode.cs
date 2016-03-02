using code_in.Views.NodalView.NodesElems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class BinaryExprNode : AExpressionNode
    {
        public DataFlowItem OperandA = null;
        public DataFlowItem OperandB = null;
        public BinaryExprNode(ResourceDictionary themeResDict)
            : base(themeResDict)
        {
            OperandA = this.CreateAndAddInput<DataFlowItem>();
            OperandB = this.CreateAndAddInput<DataFlowItem>();
            OperandA.SetName("A");
            OperandB.SetName("B");
        }
    }
}
