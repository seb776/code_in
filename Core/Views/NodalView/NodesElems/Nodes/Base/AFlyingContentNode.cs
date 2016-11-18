using code_in.Presenters.Nodal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    public abstract class AFlyingContentNode : AContentNode
    {
        private Point _lastPosition;
        protected AFlyingContentNode(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {

        }

        public override void Drop(IEnumerable<IDragNDropItem> items)
        {
            if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.MOVEOUT)
            {
                // TODO @Seb
            }
        }
        public override void Drag(EDragMode dragMode)
        {
            _lastPosition = new Point(0.0, 0.0);
        }
        public override void UpdateDragInfos(Point mousePosToMainGrid)
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

        public override void AddNode<T>(T node, int index = -1)
        {
            this.ContentGridLayout.Children.Add(node as UIElement);
        }

        public override void RemoveNode(INodeElem node)
        {
            this.ContentGridLayout.Children.Remove(node as UIElement);
        }
    }
}
