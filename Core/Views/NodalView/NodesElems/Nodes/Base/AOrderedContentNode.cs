﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    public abstract class AOrderedContentNode : AContentNode
    {
        public System.Windows.Controls.StackPanel _orderedLayout;

        public AOrderedContentNode(System.Windows.ResourceDictionary themeResDict)
            : base(themeResDict)
        {
            this.SetColorResource("NamespaceNodeColor");
            this.SetNodeType("Namespace");
            this.SetName("System.Collections.Generic.TestDeLaMuerte");
            _orderedLayout = new System.Windows.Controls.StackPanel();
            this.ContentGrid.Children.Add(_orderedLayout);
            this.MouseMove += EvtOrderedContentNode_MouseMove;
        }

        void EvtOrderedContentNode_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        public AOrderedContentNode()
            : this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        #region ICodeInVisual
        public abstract override void SetDynamicResources(String keyPrefix);
        #endregion ICodeInVisual
        #region IVisualNodeContainer
        public override void AddNode<T>(T node, int index = -1)
        {
            if (index < 0)
                this._orderedLayout.Children.Add(node as UIElement);
            else
                this._orderedLayout.Children.Insert(index, node as UIElement);
        }
        public override void HighLightDropPlace(Point pos)
        {
            throw new NotImplementedException();
        }
        public override int GetDropIndex(Point pos)
        {
            throw new NotImplementedException();
        }
        #endregion IVisualNodeContainer


    }
}