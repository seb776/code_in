using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class FuncExitNode : IONode
    {
        public FuncExitNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetNodeType("FuncExit");
            this.SetName("Outputs");
            this.SetDynamicResources("FuncExitNode");
            this.NodeHeader.Children.Remove(this.RmBtn);
            this.CreateAndAddInput<FlowNodeItem>();
        }
    }
}
