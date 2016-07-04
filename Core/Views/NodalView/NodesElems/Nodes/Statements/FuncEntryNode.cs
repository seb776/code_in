using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements
{
    /// <summary>
    /// This is a special node that has no equivalent in the AST. Its only goal is to provide
    /// a clear and simple entry point for function editing.
    /// </summary>
    public class FuncEntryNode : AStatementNode
    {
        public FlowNodeAnchor FlowOutAnchor = null;
        
        public FuncEntryNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetType("FuncEntry");
            this.SetName("Inputs");
            this.SetThemeResources("FuncEntryNode");
            FlowOutAnchor = this.CreateAndAddOutput<FlowNodeAnchor>();
        }
    }
}
