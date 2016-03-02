﻿using code_in.Views.NodalView.NodesElems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements
{
    public class ReturnStmtNode : AStatementNode
    {
        public ReturnStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.CreateAndAddInput<FlowNodeItem>();
            this.SetDynamicResources("ReturnStmtNode");
            this.SetName("Return");
        }
    }
}