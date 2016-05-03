using code_in.Views.NodalView.NodesElems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class UnaryExprNode : AExpressionNode
    {
        public DataFlowItem OperandA = null;
        public UnaryExprNode(ResourceDictionary themeResDict) : base(themeResDict)
        {
            OperandA = this.CreateAndAddInput<DataFlowItem>();
            OperandA.SetName("A");
            this.SetThemeResources("UnaryExprNode");
        }
    }
}
