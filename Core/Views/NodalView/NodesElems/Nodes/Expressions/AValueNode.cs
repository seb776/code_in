using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public abstract class AValueNode : AIONode
    {
        public DataFlowAnchor ExprOut = null;
        public AValueNode(ResourceDictionary themeResDict, INodalView nodalView)
            : base(themeResDict, nodalView)
        {
            ExprOut = this.CreateAndAddOutput<DataFlowAnchor>();
            ExprOut.SetName("expression");
        }
    }
}
