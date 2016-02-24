using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.MainView.Nodes.Items
{
    public class IOItem : NodeItem
    {
        NodeAnchor na;

        public IOItem() :
            base()
        {
            na = new NodeAnchor(this);

            na.Name = "Anchor"; // Not necessary but to be clean
            this.Container.RegisterName("Anchor", na); // To be able to find it through container.FindName(String)

            na.Width = 10;
            na.Height = 20;
            this.Container.Children.Insert(0, na);
        }

        public IOItem(BaseNode parent) :
            this()
        {
            this._parentNode = parent;
        }

        public void createLink(IOItem itemDest)
        {
            this._parentNode.CreateLink(na);            
            na.lineBegin.X = _parentNode.Margin.Left + _parentNode.ActualWidth;
            na.lineBegin.Y = _parentNode.Margin.Top + this.Margin.Top + _parentNode.NodeHeader.ActualHeight;
            na.IOLine.X1 = na.lineBegin.X;
            na.IOLine.Y1 = na.lineBegin.Y;
            na.IOLine.X2 = itemDest.Margin.Left + itemDest._parentNode.Margin.Left;
            na.IOLine.Y2 = itemDest.Margin.Top + itemDest._parentNode.Margin.Top;
            itemDest._parentNode.lineInput = this.na.IOLine;

            TransformingNode.Transformation = TransformingNode.TransformationMode.NONE;
        }
    }
}
