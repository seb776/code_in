using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Base
{
    /// <summary>
    /// A Statement Node that has an input flow and an output flow
    /// </summary>
    public abstract class ADefaultStatementNode : AStatementNode
    {
        public FlowNodeAnchor FlowOutAnchor = null;

        public ADefaultStatementNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            FlowOutAnchor = this.CreateAndAddOutput<FlowNodeAnchor>();
            FlowOutAnchor.SetName("TMP: FlowOut");
        }
        public abstract override void InstantiateASTNode();
    }
}
