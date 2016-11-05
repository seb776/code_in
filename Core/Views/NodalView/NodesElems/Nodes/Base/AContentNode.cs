using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    public abstract class AContentNode : BaseNode, IContainerDragNDrop, IVisualNodeContainer
    {
        private Point _lastPosition;
        protected AContentNode(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
//            this.SetDynamicResources("AcontentNode");
            this.ContentLayout.MouseLeftButtonUp += ContentLayout_MouseLeftButtonUp;
            this.ContentLayout.MouseMove += ContentLayout_MouseMove;
        }
        void ContentLayout_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                EDragMode dragMode = (Keyboard.IsKeyDown(Key.LeftCtrl) ? EDragMode.MOVEOUT : EDragMode.STAYINCONTEXT);
                Code_inApplication.RootDragNDrop.UpdateDragInfos(dragMode, e.GetPosition((this.NodalView as NodalView).MainGrid));
            }
            e.Handled = true;
        }

        void ContentLayout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Code_inApplication.RootDragNDrop.DragMode != EDragMode.NONE)
                Code_inApplication.RootDragNDrop.Drop(this);
            e.Handled = true;
        }
        #region IVisualNodeContainer
        /// <summary>
        /// Creates a INodeElem of type T and add it to this control by passing all required parameters (theme, language...)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T CreateAndAddNode<T>(INodePresenter nodePresenter) where T : UIElement, code_in.Views.NodalView.INode
        {
            T node = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary(), this.NodalView);
            node.SetParentView(this);
            node.SetNodePresenter(nodePresenter);
            node.NodalView = this.NodalView;
            nodePresenter.SetView(node);
            try
            {
                this.AddNode(node);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return default(T);
            }
            return node;
        }
        public abstract void AddNode<T>(T node, int index = -1) where T : UIElement, code_in.Views.NodalView.INode;
        public abstract void RemoveNode(INodeElem node);

        public abstract int GetDropIndex(Point pos);
        public abstract void HighLightDropPlace(Point pos);

        #endregion IVisualNodeContainer

        #region ICodeInVisual
        public override void SetThemeResources(string keyPrefix)
        {
        }
        #endregion ICodeInVisual
        #region IContainerDragNDrop

        public void AddSelectNode(IDragNDropItem item)
        {
            Code_inApplication.RootDragNDrop.AddSelectItem(item);
        }

        public void AddSelectNodes(List<IDragNDropItem> items)
        {
            foreach (var i in items)
                this.AddSelectNode(i);
        }

        public void Drag(EDragMode dragMode)
        {
            _lastPosition = new Point(0.0, 0.0);
        }

        public new void Drop(List<IDragNDropItem> items)
        {
            throw new NotImplementedException();
        }


        public void UnselectNode(IDragNDropItem item)
        {
            throw new NotImplementedException();
        }

        public void UnselectAllNodes()
        {
            Code_inApplication.RootDragNDrop.UnselectAllNodes();
        }
        #endregion IContainerDragNDrop

        public abstract void Drop(IEnumerable<IDragNDropItem> items);


        public bool IsDropValid(IEnumerable<IDragNDropItem> items)
        {
            if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.STAYINCONTEXT)
                return true;
            return false; // TODO @Seb
        }


        public void UpdateDragInfos(Point mousePosToMainGrid)
        {
            var selectedNodes = Code_inApplication.RootDragNDrop.SelectedItems;
            if (selectedNodes.Count == 0)
                return;
            Vector diff;
            if ((_lastPosition.X + _lastPosition.Y) < 0.01)
                diff = new Vector(0, 0);
            else
                diff = _lastPosition - mousePosToMainGrid;
            _lastPosition = mousePosToMainGrid;

            //MessageBox.Show(_selectedNodes.GroupBy(n => n).Any(c => c.Count() > 1).ToString()); // Checks for doublons
            foreach (var selNode in selectedNodes)
            {
                dynamic draggingNode = selNode;
                Thickness margin = (Thickness)draggingNode.GetType().GetProperty("Margin").GetValue(draggingNode);
                double marginLeft = margin.Left;
                double marginTop = margin.Top;
                Thickness newMargin = new Thickness();

                newMargin.Left = margin.Left;
                newMargin.Top = margin.Top;
                newMargin.Right = margin.Right;
                newMargin.Bottom = margin.Bottom;
                newMargin.Left -= diff.X;
                newMargin.Top -= diff.Y;
                newMargin.Left = Math.Max(newMargin.Left, 0);
                newMargin.Top = Math.Max(newMargin.Top, 0);
                draggingNode.SetPosition((int)newMargin.Left, (int)newMargin.Top);
            }
        }
    }
}
