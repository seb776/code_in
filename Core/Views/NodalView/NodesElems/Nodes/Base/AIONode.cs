using code_in.Exceptions;
using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public StackPanel _inputs = null;  // public tmp (ham ham)
        public StackPanel _outputs = null; // public tmp (ham ham)

        public AIONode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer) :
            base(themeResDict, nodalView)
        {
            ParentLinksContainer = linkContainer;
            this._initLayout();
        }
        public AIONode() :
            base(Code_inApplication.MainResourceDictionary, null)
        { throw new DefaultCtorVisualException(); }

        #region This
        /// <summary>
        /// Sets the visual layouts needed by this node;
        /// </summary>
        private void _initLayout()
        {
            var subGrid = new Grid();
            _inputs = new StackPanel();
            _outputs = new StackPanel();
            var leftCol = new ColumnDefinition();
            var rightCol = new ColumnDefinition();

            leftCol.Width = new GridLength(1, GridUnitType.Star);
            rightCol.Width = new GridLength(1, GridUnitType.Star);

            subGrid.ColumnDefinitions.Add(leftCol);
            subGrid.ColumnDefinitions.Add(rightCol);

            _outputs.SetValue(StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Right);

            _inputs.SetValue(Grid.ColumnProperty, 0);
            _outputs.SetValue(Grid.ColumnProperty, 1);
            subGrid.Children.Add(_inputs);
            subGrid.Children.Add(_outputs);
            this.ContentLayout.Children.Add(subGrid);
        }
        #endregion This
        #region INodeElem
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
        #endregion INodeElem
        #region IIOAnchorContainer
        public ILinkContainer ParentLinksContainer
        {
            get;
            set;
        }
        public T CreateAndAddInput<T>() where T : AIOAnchor
        {
            Debug.Assert(ParentLinksContainer != null);
            T inputAnchor = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary(), ParentLinksContainer);

            inputAnchor.Margin = new Thickness(-10, 0, 0, 0);
            this._inputs.Children.Add(inputAnchor);
            inputAnchor.SetParentNode(this);
            inputAnchor.Orientation = AIOAnchor.EOrientation.LEFT;
            return inputAnchor;
        }

        public T CreateAndAddOutput<T>() where T : AIOAnchor
        {
            Debug.Assert(ParentLinksContainer != null);
            T outputAnchor = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary(), ParentLinksContainer);

            outputAnchor.Margin = new Thickness(0, 0, -10, 0);
            this._outputs.Children.Add(outputAnchor);
            outputAnchor.SetParentNode(this);
            outputAnchor.Orientation = AIOAnchor.EOrientation.RIGHT;
            return outputAnchor;
        }
        #endregion IIOAnchorContainer

        /// <summary>
        /// This is to attach AST method Attach to input ancors
        /// </summary>
        public virtual void UpdateAnchorAttachAST()
        {
            throw new NotImplementedException(this.GetType().ToString());
        }
    }
}
