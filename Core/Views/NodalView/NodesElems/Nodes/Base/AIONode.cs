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
        public override void Remove()
        {
            throw new NotImplementedException();
        }
        private ILinkContainer _parentLinksContainer = null;
        public Grid _subGrid = null; // public tmp
        public StackPanel _inputs = null;  // public tmp (ham ham)
        public StackPanel _outputs = null; // public tmp (ham ham)

        public AIONode(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this._initLayout();

        }

        public void SetParentLinksContainer(ILinkContainer linksContainer)
        {
            System.Diagnostics.Debug.Assert(linksContainer != null);
            _parentLinksContainer = linksContainer;
            foreach (var i in _inputs.Children)
                (i as AIOAnchor).SetParentLinksContainer(_parentLinksContainer);
            foreach (var o in _outputs.Children)
                (o as AIOAnchor).SetParentLinksContainer(_parentLinksContainer);
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

        public override void SetPosition(int posX, int posY)
        {
            this.Margin = new Thickness(posX, posY, 0, 0);
            this.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            this.Arrange(new Rect(0, 0, this.DesiredSize.Width, this.DesiredSize.Height));

            foreach (var i in _inputs.Children)
            {
                AIOAnchor input = i as AIOAnchor;
                input.UpdateLinksPosition();
            }
            foreach (var o in _outputs.Children)
            {
                AIOAnchor output = o as AIOAnchor;
                output.UpdateLinksPosition();
            }
        }

        public T CreateAndAddInput<T>() where T : AIOAnchor
        {
            T inputAnchor = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary());
            this.AddInput(inputAnchor);
            inputAnchor.SetParentLinksContainer(_parentLinksContainer);
            inputAnchor.SetParentNode(this);
            inputAnchor.Orientation = AIOAnchor.EOrientation.LEFT;
            return inputAnchor;
        }

        public T CreateAndAddOutput<T>() where T : AIOAnchor
        {
            T outputAnchor = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary());
            this.AddOutput(outputAnchor);
            outputAnchor.SetParentLinksContainer(_parentLinksContainer);
            outputAnchor.SetParentNode(this);
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
