﻿using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
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
    public class WhileStmtNode : ABlockStmtNodes
    {
        public DataFlowAnchor Condition = null;
        public FlowNodeAnchor outAnchor = null;
        public FlowNodeAnchor trueAnchor = null;
        public WhileStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetName("While");
            Condition = this.CreateAndAddInput<DataFlowAnchor>();
            Condition.SetName("Condition");
            outAnchor = this.CreateAndAddOutput<FlowNodeAnchor>();
            outAnchor.SetName("FlowNode");
            trueAnchor = this.CreateAndAddOutput<FlowNodeAnchor>();
            trueAnchor.SetName("True");
            //this.SetDynamicResources("WhileStmtNode");

        }
    }
}
