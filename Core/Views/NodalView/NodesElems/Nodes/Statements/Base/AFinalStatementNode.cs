using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Base
{
    /// <summary>
    /// A Statement that only has an input flow.
    /// </summary>
    public abstract class AFinalStatementNode : AStatementNode
    {
        public AFinalStatementNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
        }
        public abstract override void InstantiateASTNode();
    }
}
