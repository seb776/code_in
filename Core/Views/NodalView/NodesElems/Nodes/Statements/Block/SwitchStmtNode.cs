﻿using code_in.Views.NodalView.NodesElems.Anchors;
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
    public class SwitchStmtNode : ABlockStmtNodes
    {
        public override void InstantiateASTNode()
        {
            this.GetNodePresenter().SetASTNode(new ICSharpCode.NRefactory.CSharp.ContinueStatement());
        }
        public SwitchStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetName("Switch");
            this.SetType("Switch");
        }
    }
}
