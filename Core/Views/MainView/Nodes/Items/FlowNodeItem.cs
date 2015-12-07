using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.MainView.Nodes.Items
{
    class FlowNodeItem : IOItem
    {
        public FlowNodeItem() :
            base()
        {
            this.SetName("FlowNode");
        }

        public FlowNodeItem(BaseNode parent) :
            this()
        {
            this._parentNode = parent;
        }
    }
}
