﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Base
{
    public abstract class AContextStmtNode : AStatementNode
    {
        protected AContextStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetThemeResources("ContextStmtNode");
        }
    }
}
