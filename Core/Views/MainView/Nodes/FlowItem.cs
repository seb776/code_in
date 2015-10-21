using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.MainView.Nodes
{
    public class FlowItem : IOItem
    {
        public FlowItem(BaseNode parent)
            : base(parent)
        {
            this.Anchor.AnchorBox.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Aqua);
            this.Label.Content = "FlowNode";
            this.Label.SetResourceReference(ForegroundProperty, parent.ColorResource); // TODO sample code for assigning dynamic resource dynamically
            //this.Label.Foreground = parent.get
            this.Anchor.AnchorBox.SetResourceReference(System.Windows.Shapes.Shape.FillProperty, parent.ColorResource);
        }
    }
}
