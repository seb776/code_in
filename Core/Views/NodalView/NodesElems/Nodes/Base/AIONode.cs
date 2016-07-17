using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Anchors;
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
    public abstract class AIONode : BaseNode, IIOAnchorContainer
    {
        private Grid _subGrid = null;
        public StackPanel _inputs = null;  // public tmp (ham ham)
        public StackPanel _outputs = null; // public tmp (ham ham)

        public AIONode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this._initLayout();

        }
        /// <summary>
        /// Sets the visual layouts needed by this node;
        /// </summary>
        private void _initLayout()
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
        /// <summary>
        /// TODO rename UpdateLocalNodePosition
        /// This function is used to update the position of Code_inLinks when moving the node
        /// </summary>
        //public override void MoveNodeSpecial()
        //{
            // TODO @Seb 04/07/2016
            //Point nodeAnchorRelativeCoord;

            //foreach (var i in _inputs.Children)
            //{
            //    AOItem it = i as AOItem;
            //    UIElement parent = (this.GetParentView() != null ? this.GetParentView() as UIElement : this.GetRootView() as UIElement);
            //    nodeAnchorRelativeCoord = it._nodeAnchor.TransformToAncestor(parent).Transform(new Point(0, 0));

            //    for (int j = 0; j < it._nodeAnchor.IOLine.Count(); ++j)
            //    {
            //         it._nodeAnchor.IOLine[j]._x2 = nodeAnchorRelativeCoord.X;
            //        it._nodeAnchor.IOLine[j]._y2 = nodeAnchorRelativeCoord.Y + it._nodeAnchor.ActualHeight / 2;
            //    }
            //}

            //foreach (var i in _outputs.Children)
            //{
            //    AOItem it = i as AOItem;
            //    UIElement parent = (this.GetParentView() != null ? this.GetParentView() as UIElement : this.GetRootView() as UIElement);
            //    nodeAnchorRelativeCoord = it._nodeAnchor.TransformToAncestor(parent).Transform(new Point(0, 0));

            //    for (int j = 0; j < it._nodeAnchor.IOLine.Count(); ++j)
            //    {
            //        it._nodeAnchor.IOLine[j]._x1 = nodeAnchorRelativeCoord.X;
            //        it._nodeAnchor.IOLine[j]._y1 = nodeAnchorRelativeCoord.Y + it._nodeAnchor.ActualHeight / 2;
            //    }
            //}
        //}

        public T CreateAndAddInput<T>() where T : AIOAnchor
        {
            T inputAnchor = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary());
            this.AddInput(inputAnchor);
            inputAnchor.Orientation = AIOAnchor.EOrientation.LEFT;
            return inputAnchor;
        }

        public T CreateAndAddOutput<T>() where T : AIOAnchor
        {
            T outputAnchor = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary());
            this.AddOutput(outputAnchor);
            outputAnchor.Orientation = AIOAnchor.EOrientation.RIGHT;
            return outputAnchor;
        }

        public void AddInput(AIOAnchor input)
        {
            input.Margin = new Thickness(-10, 0, 0, 0);
            this._inputs.Children.Add(input);
        }

        public void AddOutput(AIOAnchor output)
        {
            output.Margin = new Thickness(0, 0, -10, 0);
            this._outputs.Children.Add(output);
        }
    }
}
