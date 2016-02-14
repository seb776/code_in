using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class FuncExitNode : IONode
    {
        public FuncExitNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.CreateAndAddInput<FlowNodeItem>();
        }
        public override void SetDynamicResources(String keyPrefix)
        { }
    }
}
