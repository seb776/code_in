using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Base
{
    /// <summary>
    /// Node for "continue" "break" "return" + "yield"
    /// </summary>
    public abstract class AContextStmtNode : AFinalStatementNode
    {
        protected AContextStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetThemeResources("ContextStmtNode");
        }
    }
}
