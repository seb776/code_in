﻿using code_in.Views.NodalView.NodesElems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements
{
    public class ExpressionStmtNode : AStatementNode
    {
        public DataFlowItem Expression = null;
        public ExpressionStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.CreateAndAddInput<FlowNodeItem>();
            Expression = this.CreateAndAddInput<DataFlowItem>();
            Expression.SetName("Expression");
            this.CreateAndAddOutput<FlowNodeItem>();
            this.SetDynamicResources("ExpressionStmtNode");
        }
    }
}