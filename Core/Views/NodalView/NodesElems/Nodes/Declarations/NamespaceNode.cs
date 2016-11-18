﻿using code_in.Exceptions;
using code_in.Presenters.Nodal;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class NamespaceNode : AFlyingContentNode
    {
        public NamespaceNode(System.Windows.ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetType("namespace");
            this.SetName("TMP.DefaultNamespaceName");
            //this.SetThemeResources("NamespaceNode");
        }
        public NamespaceNode()
            : this(Code_inApplication.MainResourceDictionary, null)
        { throw new DefaultCtorVisualException(); }

        #region IVisualNodeContainer
        public override void HighLightDropPlace(System.Windows.Point pos)
        {
            throw new NotImplementedException();
        }
        
        public override int GetDropIndex(System.Windows.Point pos)
        { 
            return 0; 
        }

        public override void RemoveNode(INodeElem node)
        {
            this.ContentGridLayout.Children.Remove(node as UIElement);
        }
        #endregion IVisualNodeContainer
    } // Class
} // Namespace
