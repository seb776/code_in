using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Block
{
    public class SwitchStmtNode : ABlockStmtNodes
    {
        public SwitchStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetName("");
            this.SetNodeType("SwitchStmt");
            
        }
    }
}
