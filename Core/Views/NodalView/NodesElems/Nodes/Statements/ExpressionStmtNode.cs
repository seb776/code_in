using code_in.Views.NodalView.NodesElems.Anchors;
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
        public DataFlowAnchor Expression = null;
        public FlowNodeAnchor outAnchor = null;
        public ExpressionStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetType("ExpressionStmt");
            Expression = this.CreateAndAddInput<DataFlowAnchor>();
            Expression.SetName("Expression");
            outAnchor = this.CreateAndAddOutput<FlowNodeAnchor>();
            this.SetThemeResources("ExprStmtNode");
        }
    }
}
