﻿using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class NamespaceNode : AFlyingContentNode
    {
        public NamespaceNode()
            : this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }

        public NamespaceNode(System.Windows.ResourceDictionary themeResDict) : base(themeResDict)
        {
            this.SetNodeType("Namespace");
            this.SetName("System.Collections.Generic.TestDeLaMuerte");
            this.SetDynamicResources("NamespaceNode");
        }
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
            throw new NotImplementedException();
        }

        #endregion IVisualNodeContainer
    } // Class
} // Namespace