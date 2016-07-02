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
        protected AFlyingContentNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            var resizeBtn = new Polygon();
            resizeBtn.Points = new System.Windows.Media.PointCollection();
            resizeBtn.Points.Add(new Point(15, 15));
            resizeBtn.Points.Add(new Point(0, 15));
            resizeBtn.Points.Add(new Point(15, 0));
            resizeBtn.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            resizeBtn.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            resizeBtn.Cursor = Cursors.SizeNWSE;
            resizeBtn.PreviewMouseLeftButtonDown += EvtDragResize;
            resizeBtn.Fill = new SolidColorBrush(Colors.GreenYellow);
            this.ContentLayout.Children.Add(resizeBtn);
        }

        private void EvtDragResize(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public override void AddNode<T>(T node, int index = -1)
        {
            this.ContentLayout.Children.Add(node as UIElement);
        }

        public override void RemoveNode(INodeElem node)
        {
            this.ContentLayout.Children.Remove(node as UIElement);
        }
    }
}
