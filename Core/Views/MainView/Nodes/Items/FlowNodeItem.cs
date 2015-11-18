using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.MainView.Nodes.Items
{
    class FlowNodeItem : NodeItem
    {
        public FlowNodeItem() :
            base()
        {
            this.Container.Children.Remove(this.TypeField);
            this.ItemName.Text = "FlowNode";
        }
    }
}
