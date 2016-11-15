using code_in.Exceptions;
using code_in.Presenters.Nodal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        { throw new DefaultCtorVisualException(); }


        public override void Drop(IEnumerable<IDragNDropItem> items)
        {
            if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.MOVEOUT)
            {
                // TODO @Seb
            }
            else if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.STAYINCONTEXT)
            {
                List<UIElement> backUpItems = new List<UIElement>();
                foreach (var uiElem in this.CurrentMovingNodes.Children)
                {
                    backUpItems.Add(uiElem as UIElement);
                }
                CurrentMovingNodes.Children.Clear();
                foreach (var uiElem in backUpItems)
                {
                    this._orderedLayout.Children.Add(uiElem);
                }

                // TODO @Seb calculate drop index...
                //foreach (var uiElem in this._orderedLayout.Children)
                //{

                //}
            }
        }
        public override void Drag(EDragMode dragMode)
        {
            var selItems = Code_inApplication.RootDragNDrop.SelectedItems;
            _lastPosition = new Point(0.0, 0.0);
            if (dragMode == EDragMode.STAYINCONTEXT)
            {
                if (CurrentMovingNodes == null)
                {
                    CurrentMovingNodes = new StackPanel();
                    CurrentMovingNodes.Background = new SolidColorBrush(Color.FromArgb(0x42, 0, 0, 0));
                    CurrentMovingNodes.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    this.MoveNodeGrid.Children.Add(CurrentMovingNodes);
                }
                foreach (var item in selItems)
                {
                    this._orderedLayout.Children.Remove(item as UIElement); // Temporary
                    //item.RemoveFromContext(); // TODO @Seb
                    CurrentMovingNodes.Children.Add(item as UIElement); // TODO @Seb Beuark
                }
            }
            else
            {
                Mouse.OverrideCursor = Cursors.Hand;
            }
        }
        public override void UpdateDragInfos(Point mousePosToMainGrid)
        {
            if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.STAYINCONTEXT)
            {
                var relPos = (this.NodalView as NodalView).MainGrid.TranslatePoint(mousePosToMainGrid, this.ContentGridLayout);
                this.CurrentMovingNodes.Margin = new Thickness(0.0, Math.Max(relPos.Y, 0.0), 0.0, 0.0);
            }
            else
            {
                if (this.IsDropValid(Code_inApplication.RootDragNDrop.SelectedItems))
                    Mouse.OverrideCursor = Cursors.Hand;
                else
                    Mouse.OverrideCursor = Cursors.No;
            }
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
