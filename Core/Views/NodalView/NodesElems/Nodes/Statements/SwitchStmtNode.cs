using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements
{
    public class SwitchStmtNode : AStatementNode
    {
        public SwitchStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetName("");
            this.SetNodeType("SwitchStmt");
            
        }
    }
}
