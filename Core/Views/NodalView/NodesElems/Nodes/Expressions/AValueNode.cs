﻿using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public abstract class AValueNode : AIONode
    {
        public AValueNode(ResourceDictionary themeResDict) : base(themeResDict)
        {
            var expr = this.CreateAndAddOutput<DataFlowItem>();
            expr.SetName("expression");
        }
    }
}
