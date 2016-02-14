using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Items
{
    class FlowNodeItem : IOItem
    {
         public FlowNodeItem(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetName("FlowNode");
        }
         public FlowNodeItem() :
             base()
         { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        #region ICodeInVisual
        public override void SetDynamicResources(String keyPrefix)
        { }
        #endregion ICodeInVisual
    }
}
