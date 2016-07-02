﻿using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Block
{
    class ForeachStmtNode : ABlockStmtNodes
    {
        public DataFlowItem Condition = null;
        public AOItem inAnchor = null;
        public AOItem outAnchor = null;
        public AOItem trueAnchor = null;
        public ForeachStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
             this.SetName("ForEach");
             inAnchor = this.CreateAndAddInput<FlowNodeItem>();
             inAnchor.SetName("In");
             Condition = this.CreateAndAddInput<DataFlowItem>();
             Condition.SetName("Condition");
             outAnchor = this.CreateAndAddOutput<FlowNodeItem>();
             outAnchor.SetName("FlowNode");
             trueAnchor = this.CreateAndAddOutput<FlowNodeItem>();
             trueAnchor.SetName("True");
        }
    }
}
