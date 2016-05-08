using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace code_in.Views.NodalView.NodesElem.Nodes.Base
{
    /// <summary>
    /// Defines an abstract visual node that has Input and OutputList
    /// </summary>
    public abstract class AIONode : BaseNode, IVisualNodeContainer
    {
        Grid _subGrid;
        protected StackPanel _inputs;
        protected StackPanel _outputs;
        public override void SetRootView(IVisualNodeContainerDragNDrop root)
        {
            base.SetRootView(root);
            foreach (var i in _inputs.Children)
            {
                AOItem it = i as AOItem;
                it.SetRootView(this.GetRootView());
            }
            foreach (var i in _outputs.Children)
            {
                AOItem it = i as AOItem;
                it.SetRootView(this.GetRootView());
            }
        }

        public void EvtRemoveNode(object sender, MouseButtonEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
            foreach (var i in this._inputs.Children)
            {
                AOItem it = i as AOItem;

                if (it.GetRootView().GetType() == typeof(NodalView))
                    (it.GetRootView() as NodalView).RemoveLink(it);
            }
            foreach (var i in this._outputs.Children)
            {
                AOItem it = i as AOItem;

                if (it.GetRootView().GetType() == typeof(NodalView))
                    (it.GetRootView() as NodalView).RemoveLink(it);
            }
            e.Handled = true;
        }
        public T CreateAndAddInput<T>() where T : AOItem
        {
            T item = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary());

            item.SetRootView(this.GetRootView());
            item.SetParentView(this);
            this.AddInput(item);            return item;
        }
        public T CreateAndAddOutput<T>() where T : AOItem
        {
            T item = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary());

            item.SetRootView(this.GetRootView());
            item.SetParentView(this);

            item.Orientation = AOItem.EOrientation.RIGHT;
            this.AddOutput(item);
            return item;
        }
        public void AddInput(AOItem item, int index = -1)
        {
            var old = item.Margin;
            var n = new Thickness(old.Left - 13, old.Top, 0, 0);
            item.Margin = n;
            if (index < 0)
                _inputs.Children.Add(item);
            else
                this._inputs.Children.Insert(index, item);
        }
        public void AddOutput(AOItem item, int index = -1)
        {
            var old = item.Margin;
            var n = new Thickness(0, old.Top, old.Right - 13, 0);
            item.Margin = n;
            if (index < 0)
                _outputs.Children.Add(item);
            else
                this._outputs.Children.Insert(index, item);
        }
        public void RemoveInput(AOItem item)
        {
            this._inputs.Children.Remove(item);
        }
        public void RemoveOutput(AOItem item)
        {
            this._outputs.Children.Remove(item);
        }
        public AIONode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            _subGrid = new Grid();
            _inputs = new StackPanel();
            _outputs = new StackPanel();
            var leftCol = new ColumnDefinition();
            var rightCol = new ColumnDefinition();

            leftCol.Width = new GridLength(1, GridUnitType.Star);
            rightCol.Width = new GridLength(1, GridUnitType.Star);

            _subGrid.ColumnDefinitions.Add(leftCol);
            _subGrid.ColumnDefinitions.Add(rightCol);

            _outputs.SetValue(StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Right);

            _inputs.SetValue(Grid.ColumnProperty, 0);
            _outputs.SetValue(Grid.ColumnProperty, 1);
            _subGrid.Children.Add(_inputs);
            _subGrid.Children.Add(_outputs);
            this.ContentLayout.Children.Add(_subGrid);

        }
        public override void MoveNodeSpecial()
        {
            Point nodeAnchorRelativeCoord;

            foreach (var i in _inputs.Children)
            {
                AOItem it = i as AOItem;
                UIElement parent = (this.GetParentView() != null ? this.GetParentView() as UIElement : this.GetRootView() as UIElement);
                nodeAnchorRelativeCoord = it._nodeAnchor.TransformToAncestor(parent).Transform(new Point(0, 0));

                for (int j = 0; j < it._nodeAnchor.IOLine.Count(); ++j)
                {
                     it._nodeAnchor.IOLine[j]._x2 = nodeAnchorRelativeCoord.X;
                    it._nodeAnchor.IOLine[j]._y2 = nodeAnchorRelativeCoord.Y + it._nodeAnchor.ActualHeight / 2;
                }
            }

            foreach (var i in _outputs.Children)
            {
                AOItem it = i as AOItem;
                UIElement parent = (this.GetParentView() != null ? this.GetParentView() as UIElement : this.GetRootView() as UIElement);
                nodeAnchorRelativeCoord = it._nodeAnchor.TransformToAncestor(parent).Transform(new Point(0, 0));

                for (int j = 0; j < it._nodeAnchor.IOLine.Count(); ++j)
                {
                    it._nodeAnchor.IOLine[j]._x1 = nodeAnchorRelativeCoord.X;
                    it._nodeAnchor.IOLine[j]._y1 = nodeAnchorRelativeCoord.Y + it._nodeAnchor.ActualHeight / 2;
                }
            }
        }

        public T CreateAndAddNode<T>(EIOOption ioOption = EIOOption.NONE) where T : UIElement, INodeElem
        {
            throw new NotImplementedException();
        }

        public void AddNode<T>(T noden, EIOOption ioOption = EIOOption.NONE, int idx = -1) where T : UIElement, INodeElem
        {
            throw new NotImplementedException();
        }

        public void RemoveNode(INodeElem node)
        {
            throw new NotImplementedException();
        }

        public int GetDropIndex(Point pos)
        {
            throw new NotImplementedException();
        }

        public void HighLightDropPlace(Point pos)
        {
            throw new NotImplementedException();
        }
    }
}
