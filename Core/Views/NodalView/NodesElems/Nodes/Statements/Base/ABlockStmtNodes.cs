using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Base
{
    public abstract class ABlockStmtNodes : AStatementNode
    {
        protected ABlockStmtNodes(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetThemeResources("BlockStmtNode");
        }
    }
}
