﻿using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class FuncEntryNode : IONode
    {
        public FuncEntryNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetNodeType("FuncEntry");
            this.SetName("Inputs");
            this.SetDynamicResources("FuncEntryNode");
            this.NodeHeader.Children.Remove(this.RmBtn);
            this.CreateAndAddOutput<FlowNodeItem>();
        }
    }
}
