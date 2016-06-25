using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements
{
    public class ExpressionStmtNode : AStatementNode
    {
        public DataFlowItem Expression = null;
        public AOItem inAnchor = null;
        public AOItem outAnchor = null;
        public ExpressionStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            inAnchor = this.CreateAndAddInput<FlowNodeItem>();
            Expression = this.CreateAndAddInput<DataFlowItem>();
            Expression.SetName("Expression");
            outAnchor = this.CreateAndAddOutput<FlowNodeItem>();
            this.SetThemeResources("ExprStmtNode");
        }
    }
}
