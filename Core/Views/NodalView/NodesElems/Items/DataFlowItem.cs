using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Items
{
    public class DataFlowItem : Base.IOItem
    {
        public override void SetDynamicResources(String keyprefix)
        { }

        public DataFlowItem(ResourceDictionary themeResDict) :
            base(themeResDict)
        { }
    }
}
