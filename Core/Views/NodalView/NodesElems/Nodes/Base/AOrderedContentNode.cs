using code_in.Presenters.Nodal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    public abstract class AOrderedContentNode : AContentNode
    {
        public System.Windows.Controls.StackPanel _orderedLayout;
        private StackPanel CurrentMovingNodes = null;
        private Point _lastPosition;

        public AOrderedContentNode(System.Windows.ResourceDictionary themeResDict, INodalView nodalView)
            : base(themeResDict, nodalView)
        {
            this.SetType("Namespace");
            this.SetName("System.Collections.Generic.TestDeLaMuerte");
            _orderedLayout = new System.Windows.Controls.StackPanel();
            _orderedLayout.SetValue(StackPanel.HeightProperty, double.NaN);
           // _orderedLayout.SetValue(StackPanel.fi)
            this.ContentLayout.Children.Add(_orderedLayout);

        }

        public AOrderedContentNode()
            : this(Code_inApplication.MainResourceDictionary, null)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }


        public override void Drop(IEnumerable<IDragNDropItem> items)
        {
            if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.MOVEOUT)
            {
                // TODO @Seb
            }
        }
        public override void Drag(EDragMode dragMode)
        {
            var selItems = Code_inApplication.RootDragNDrop.SelectedItems;

            if (CurrentMovingNodes == null)
            {
                CurrentMovingNodes = new StackPanel();
                this.ContentGridLayout.Children.Add(CurrentMovingNodes);
            }
            foreach (var item in selItems)
            {
                this._orderedLayout.Children.Remove(item as UIElement); // Temporary
                //item.RemoveFromContext(); // TODO @Seb
                CurrentMovingNodes.Children.Add(item as UIElement); // TODO @Seb Beuark
            }
        }
        public override void UpdateDragInfos(Point mousePosToMainGrid)
        {
            var relPos = (this.NodalView as NodalView).MainGrid.TranslatePoint(mousePosToMainGrid, this);
            this.CurrentMovingNodes.Margin = new Thickness(0.0, relPos.Y, 0.0, 0.0);
        }

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
            double offsetY = 0;
            int count = 0;
            foreach (var i in _orderedLayout.Children)
            {
                offsetY += ((FrameworkElement)i).ActualHeight;
                if (pos.Y < offsetY)
                    break;
                ++count;
            }
            return count;
        }

        public override void RemoveNode(INodeElem node)
        {
            _orderedLayout.Children.Remove(node as UIElement);
        }
        #endregion IVisualNodeContainer



    }
}
