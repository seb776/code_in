using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Items.Base
{
    class FlowNodeItem : IOItem
    {
        public override void SetDynamicResources(String keyPrefix)
        { }
        public FlowNodeItem() :
            base()
        {
            this.SetName("FlowNode");
        }
        public FlowNodeItem(ResourceDictionary resDict) :
            base(resDict)
        {

        }
        public FlowNodeItem(BaseNode parent) :
            this()
        {
            this._parentNode = parent;
        }
    }
}
