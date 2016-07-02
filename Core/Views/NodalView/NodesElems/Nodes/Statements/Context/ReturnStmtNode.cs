using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Context
{
    public class ReturnStmtNode : AContextStmtNode
    {
        public DataFlowItem ExprIn = null;
        public ReturnStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            ExprIn = this.CreateAndAddInput<DataFlowItem>();
            this.SetThemeResources("ReturnStmtNode");
            this.SetName("Return");
        }
    }
}
