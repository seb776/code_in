using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.MainView.Nodes.Items
{
    public class DataFlowItem : IOItem
    {
        public DataFlowItem() :
            base()
        {
            this.SetItemType("test");
        }
    }
}
