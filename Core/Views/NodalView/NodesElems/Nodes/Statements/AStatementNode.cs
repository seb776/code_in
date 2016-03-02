using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements
{
    public abstract class AStatementNode : IONode
    {
        public AStatementNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetNodeType("Statement");
            this.LockName();
        }
    }
}
