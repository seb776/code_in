using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.MainView.Nodes
{
    /// <summary>
    /// This class defines the anchor for the execution order (see EFeatures.IsFlowNode)
    /// </summary>
    /// 
    public class FlowNodeAnchor : NodeAnchor
    {
        public FlowNodeAnchor(BaseNode parent)
            : base(parent)
        {

        }
    }
}
