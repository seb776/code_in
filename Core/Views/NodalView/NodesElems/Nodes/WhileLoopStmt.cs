using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class WhileLoopStmt : IONode
    {
        public WhileLoopStmt(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetNodeType("Statement");
            this.SetName("While");
            var item = this.CreateAndAddInput<FlowNodeItem>();
            item.SetName("In");
            var item2 = this.CreateAndAddInput<DataFlowItem>();
            item2.SetName("Condition");
            item = this.CreateAndAddOutput<FlowNodeItem>();
            item.SetName("True");
            item = this.CreateAndAddOutput<FlowNodeItem>();
            item.SetName("False");
        }
        public override void SetDynamicResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
    }
}
