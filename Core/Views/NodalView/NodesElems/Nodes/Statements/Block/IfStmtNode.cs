using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Block
{
    public class IfStmtNode : ABlockStmtNodes
    {
        public DataFlowItem Condition = null;
        public IfStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetName("If");
            var item = this.CreateAndAddInput<FlowNodeItem>();
            item.SetName("In");
            Condition = this.CreateAndAddInput<DataFlowItem>();
            Condition.SetName("Condition");
            item = this.CreateAndAddOutput<FlowNodeItem>();
            item.SetName("True");
            item = this.CreateAndAddOutput<FlowNodeItem>();
            item.SetName("False");
            //this.SetDynamicResources("IfStmtNode");
        }
    }
}
