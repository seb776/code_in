using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class StatementNode : IONode
    {
        public StatementNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetNodeType("Statement");
            this.SetName("Default");
            this.CreateAndAddInput<FlowNodeItem>();
            this.CreateAndAddInput<DataFlowItem>();
            this.CreateAndAddOutput<FlowNodeItem>();
        }
    }
}
