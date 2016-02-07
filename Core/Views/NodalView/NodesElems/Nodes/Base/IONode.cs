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
    public abstract class IONode : BaseNode
    {
        Grid _subGrid;
        StackPanel _inputs;
        StackPanel _outputs;
        public T CreateAndAddInput<T>() where T : IOItem
        {
            T item = (T)Activator.CreateInstance(typeof(T), code_in.Resources.SharedDictionaryManager.MainResourceDictionary);//(MainView == null ? code_in.Resources.SharedDictionaryManager.MainResourceDictionary : MainView.ResourceDict)) as T;

            _inputs.Children.Add(item);
            return item;
        }
        public T CreateAndAddOutput<T>() where T : IOItem
        {
            T item = (T)Activator.CreateInstance(typeof(T), code_in.Resources.SharedDictionaryManager.MainResourceDictionary);//(MainView == null ? code_in.Resources.SharedDictionaryManager.MainResourceDictionary : MainView.ResourceDict)) as T;
            item.Orientation = IOItem.EOrientation.RIGHT;
            _outputs.Children.Add(item);
            return item;
        }

        public IONode(ResourceDictionary resourceDict) :
            base(resourceDict)
        {
            _subGrid = new Grid();
            _inputs = new StackPanel();
            _outputs = new StackPanel();
            var leftCol = new ColumnDefinition();
            var rightCol = new ColumnDefinition();

            leftCol.Width = GridLength.Auto;
            rightCol.Width = GridLength.Auto;
            _subGrid.ColumnDefinitions.Add(leftCol);
            _subGrid.ColumnDefinitions.Add(rightCol);

            _inputs.SetValue(Grid.ColumnProperty, 0);
            _outputs.SetValue(Grid.ColumnProperty, 1);
            _subGrid.Children.Add(_inputs);
            _subGrid.Children.Add(_outputs);
            this.ContentGrid.Children.Add(_subGrid);

        }

    }
}
