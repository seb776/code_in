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
using System.Windows.Media;
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
        public Label AddInputLabel = null;
        public AIONode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer) :
            base(themeResDict, nodalView)
        {
            ParentLinksContainer = linkContainer;
            this._initLayout();
            var button = new Label();
            AddInputLabel = button;
            button.Content = "+";
            button.Foreground = new SolidColorBrush(Colors.GreenYellow);
            button.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x15, 0x15, 0x15));
            button.MouseLeftButtonDown += button_Addinput_Clicked;
            button.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            CustomButtonsLayout.Children.Add(button);
            CanAddInputs = false;
            this.SizeChanged += AIONode_SizeChanged;
        }

        void AIONode_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }
        bool _canAddInputs;
        public bool CanAddInputs
        {
            get
            {
                return _canAddInputs;
            }
            set
            {
                _canAddInputs = value;
                if (_canAddInputs)
                {
                    AddInputLabel.Visibility = System.Windows.Visibility.Visible;
                    AddInputLabel.IsEnabled = true;
                }
                else
                {
                    AddInputLabel.Visibility = System.Windows.Visibility.Collapsed;
                    AddInputLabel.IsEnabled = false;
                }
            }
        }
        public virtual void _addVariableParamInAST()
        {

        }
        void button_Addinput_Clicked(object sender, MouseButtonEventArgs e)
        {
            // We do not have other type of Anchor now.
            var newInput = this.CreateAndAddInput<DataFlowAnchor>();
            newInput.IsRemovable = true;
            this._addVariableParamInAST();
            this.UpdateAnchorAttachAST();
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
        public virtual void RemoveRuntimeParamFromAST(int index)
        {

        }
        public void RemoveIO(AIOAnchor anchor)
        {
            if (anchor._links.Count > 0)
                anchor.RemoveLink(anchor._links[0], false);
            this.RemoveRuntimeParamFromAST(this._inputs.Children.IndexOf(anchor));
            this._inputs.Children.Remove(anchor);
            this.UpdateAnchorAttachAST();
        }
        public abstract void UpdateAnchorAttachAST();
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
            inputAnchor.SetName("TMP: param" + (_inputs.Children.Count - 1).ToString());
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
    }
}
