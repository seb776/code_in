using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace code_in.Views.NodalView.NodesElem.Nodes.Base
{
    /// <summary>
    /// Defines an abstract visual node that has Input and OutputList
    /// </summary>
    public abstract class IONode : BaseNode
    {
        Grid _subGrid;
        StackPanel _inputs;
        StackPanel _outputs;
        public T CreateAndAddInput<T>() where T : IOItem
        {
            T item = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary());

            item.SetRootView(this.GetRootView());
            item.SetParentView(this);

            _inputs.Children.Add(item);
            return item;
        }
        public T CreateAndAddOutput<T>() where T : IOItem
        {
            T item = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary());

            item.SetRootView(this.GetRootView());
            item.SetParentView(this);

            item.Orientation = IOItem.EOrientation.RIGHT;
            _outputs.Children.Add(item);
            return item;
        }
        public void AddInput(IOItem item, int index = -1)
        {
            if (index < 0)
                _inputs.Children.Add(item);
            else
                this._inputs.Children.Insert(index, item);
        }
        public void AddOutput(IOItem item, int index = -1)
        {
            if (index < 0)
                _outputs.Children.Add(item);
            else
                this._outputs.Children.Insert(index, item);
        }
        public void RemoveInput(IOItem item)
        {
            this._inputs.Children.Remove(item);
        }
        public void RemoveOutput(IOItem item)
        {
            this._outputs.Children.Remove(item);
        }
        public IONode(ResourceDictionary themeResDict) :
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
            this.ContentGrid.Children.Add(_subGrid);

        }

    }
}
