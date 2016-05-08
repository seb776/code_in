using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Items.Assets;
using System.Windows.Controls;

namespace code_in.Views.NodalView.NodesElems.Items.Base
{
    public abstract class AOItem : ANodeItem
    {
        public enum EOrientation
        {
            LEFT = 0,
            RIGHT = 1,
        }
        public Assets.NodeAnchor _nodeAnchor; // The anchor point of the item
        public AOItem IOAttached = null;  // The attached IOItem

        public AOItem(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            _nodeAnchor = new NodeAnchor(this.GetThemeResourceDictionary());

            _nodeAnchor.Name = "Anchor"; // Not necessary but to be clean
            //this.Container.RegisterName("Anchor", na); // To be able to find it through container.FindName(String)

            _nodeAnchor.Width = 10;
            _nodeAnchor.Height = 20;
            _nodeAnchor.setParentAnchor(this);
            this.BeforeName.Children.Add(_nodeAnchor);
        }
        public AOItem() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }
        public void RemoveLink()
        {
          /*  if (this.IOAttached != null)
            {
                IOItem bu = this.IOAttached; // to avoid recursion
                this.IOAttached = null;
                bu.RemoveLink();
            }
            if (this._nodeAnchor.IOLine != null)
                this._nodeAnchor.IOLine = null; */
            this._nodeAnchor.IOLine.Clear();
        }
        public void createLink()
        {
            this.GetRootView().DragNodes(TransformationMode.LINE, this, LineMode.SQUARE);

            //this._parentNode.CreateLink(_nodeAnchor);
            //_nodeAnchor.lineBegin.X = _parentNode.Margin.Left + _parentNode.ActualWidth;
            //_nodeAnchor.lineBegin.Y = _parentNode.Margin.Top + this.Margin.Top + _parentNode.NodeHeader.ActualHeight;
            //_nodeAnchor.IOLine.X1 = _nodeAnchor.lineBegin.X;
            //_nodeAnchor.IOLine.Y1 = _nodeAnchor.lineBegin.Y;
            //_nodeAnchor.IOLine.X2 = itemDest.Margin.Left + itemDest._parentNode.Margin.Left;
            //_nodeAnchor.IOLine.Y2 = itemDest.Margin.Top + itemDest._parentNode.Margin.Top;
            //itemDest._parentNode.lineInput = this._nodeAnchor.IOLine;

            //TransformingNode.Transformation = TransformingNode.TransformationMode.NONE;

        }

        public void dropLine()
        {
            this.GetRootView().DropNodes(this);
        }

        public void SetItemType(String type)
        {
            //if (this.Container.FindName("TypeField") == null)
            //{
            //    TypeInfo ti = new TypeInfo();

            //    ti.Name = "TypeField"; // Not necessary but to be clean ;)
            //    this.Container.RegisterName(ti.Name, ti); // To allow TypeField to be found through FindName
            //    this.Container.Children.Add(ti);
            //}
            //object typeField = this.FindName("TypeField");
            //if (typeField != null)
            //{
            //    var tF = typeField as TypeInfo;
            //    tF.Content = type;
            //}
        }


        private EOrientation _orientation;
        public EOrientation Orientation
        {
            get { return _orientation; }
            set
            {
                this.FlowDirection = (value == EOrientation.LEFT ?
                    System.Windows.FlowDirection.LeftToRight :
                    System.Windows.FlowDirection.RightToLeft);

                this.HorizontalAlignment = (value == EOrientation.LEFT ? HorizontalAlignment.Left : HorizontalAlignment.Right);

                //TypeInfo ti = (TypeInfo)this.Container.FindName("TypeField");
                //if (this.Container.FindName("TypeField") != null) // TypeField is not always part of an item
                //    ti.TypePanel.FlowDirection = System.Windows.FlowDirection.LeftToRight; // to avoid the type visual control to be reversed recursively

                _orientation = value;
            }
        }
    }
}
