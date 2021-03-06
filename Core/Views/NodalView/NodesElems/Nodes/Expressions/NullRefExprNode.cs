﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class NullRefExprNode : AExpressionNode
    {
        public NullRefExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer) :
            base(themeResDict, nodalView, linkContainer)
        {
            this.SetType("Null");
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
        }

        public override void UpdateAnchorAttachAST()
        {
            // Not necessary we have no inputs
        }
    }
}
