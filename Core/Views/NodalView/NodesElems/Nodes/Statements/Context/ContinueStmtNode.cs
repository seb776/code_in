using System;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Context
{
    class ContinueStmtNode : AContextStmtNode
    {
        public ContinueStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.CreateAndAddInput<FlowNodeItem>();
        }
    }
}
