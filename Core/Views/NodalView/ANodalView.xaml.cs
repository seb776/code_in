using code_in.Exceptions;
using code_in.Managers;
using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.MainView;
using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Tiles;
using code_in.Views.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace code_in.Views.NodalView
{
    /// <summary>
    /// The Nodal view is the layout that is able to display Nodes From the NodalPresenter;
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public abstract partial class ANodalView : UserControl, INodalView, stdole.IDispatch
    {
        static public INodeElem InstantiateVisualNode(NodePresenter.ECSharpNode nodeType, INodalView nodalView, ILinkContainer linkContainer)
        {
            Dictionary<NodePresenter.ECSharpNode, Type> types = new Dictionary<NodePresenter.ECSharpNode, Type>();
            // General scope
            //types.Add(NodePresenter.ECSharpNode.ATTRIBUTE, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // Irrelevant for now as the visual representation is different than the AST
            //types.Add(NodePresenter.ECSharpNode.ATTRIBUTE_SECTION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // Irrelevant for now as the visual representation is different than the AST
            //types.Add(NodePresenter.ECSharpNode.COMMENT, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // Irrelevant for now as the visual representation is different than the AST
            //types.Add(NodePresenter.ECSharpNode.CONSTRAINT, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // Irrelevant for now as the visual representation is different than the AST
            //types.Add(NodePresenter.ECSharpNode.DELEGATE_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // TODO
            //types.Add(NodePresenter.ECSharpNode.EXTERN_ALIAS_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // TODO
            types.Add(NodePresenter.ECSharpNode.NAMESPACE_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.PREPROCESSOR_DIRECTIVE, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // TODO
            //types.Add(NodePresenter.ECSharpNode.TEXT_NODE, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // Unknown / TODO
            types.Add(NodePresenter.ECSharpNode.TYPE_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // TODO need some work as it's used for class/struct/interface
            //types.Add(NodePresenter.ECSharpNode.TYPE_PARAMETER_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // ??? TODO
            //types.Add(NodePresenter.ECSharpNode.USING_ALIAS_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // TODO
            //types.Add(NodePresenter.ECSharpNode.USING_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // TODO Need some work as the representation is not clearly defined yet
            //types.Add(NodePresenter.ECSharpNode.UNSUPER_GENERAL_SCOPE, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // TODO Not sure if meaningful

            // TypeMembers
            //types.Add(NodePresenter.ECSharpNode.ACCESSOR, typeof(code_in.Views.NodalView.NodesElems.Items.PropertyItem)); // Irrelevant
            types.Add(NodePresenter.ECSharpNode.CTOR_DECL, typeof(code_in.Views.NodalView.NodesElems.Items.ConstructorItem));
            //types.Add(NodePresenter.ECSharpNode.DTOR_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode)); // TODO
            types.Add(NodePresenter.ECSharpNode.ENUM_MEMBER_DECL, typeof(code_in.Views.NodalView.NodesElems.Items.ClassItem));
            //types.Add(NodePresenter.ECSharpNode.EVENT_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.Ev)); // TODO
            types.Add(NodePresenter.ECSharpNode.FIELD_DECL, typeof(code_in.Views.NodalView.NodesElems.Items.ClassItem)); // TODO not sure
            //types.Add(NodePresenter.ECSharpNode.FIXED_FIELD_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.FIXED_VAR_INIT, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.INDEXER_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.METHOD_DECL, typeof(code_in.Views.NodalView.NodesElems.Items.FuncDeclItem));
            //types.Add(NodePresenter.ECSharpNode.OPERATOR_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.PARAMETER_DECL, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.PROPERTY_DECL, typeof(code_in.Views.NodalView.NodesElems.Items.PropertyItem));
            //types.Add(NodePresenter.ECSharpNode.VAR_INIT, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.UNSUP_TYPE_MEMBERS, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            
            // Statements
            types.Add(NodePresenter.ECSharpNode.BREAK_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.BreakStmtTile));
            //types.Add(NodePresenter.ECSharpNode.CHECKED_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.CONTINUE_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.DO_WHILE_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.DoWhileStmtTile));
            types.Add(NodePresenter.ECSharpNode.EXPRESSION_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.ExprStmtTile));
            //types.Add(NodePresenter.ECSharpNode.FIXED_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.FOR_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.ForStmtTile));
            types.Add(NodePresenter.ECSharpNode.FOREACH_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.ForEachStmtTile));
            //types.Add(NodePresenter.ECSharpNode.GOTO_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.IFELSE_STMT, typeof(code_in.Views.NodalView.NodesElems.Nodes.Statements.Block.IfStmtTile));
            //types.Add(NodePresenter.ECSharpNode.LABEL_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.LOCK_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.RETURN_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.ReturnStmtTile));
            types.Add(NodePresenter.ECSharpNode.SWITCH_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.SwitchStmtTile));
            types.Add(NodePresenter.ECSharpNode.THROW_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.ThrowStmtTile));
            types.Add(NodePresenter.ECSharpNode.TRY_CATCH_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.TryCatchStmtTile));
            //types.Add(NodePresenter.ECSharpNode.UNCHECKED_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.UNSAFE_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.USING_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.VAR_DECL_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.VarStmtTile));
            types.Add(NodePresenter.ECSharpNode.WHILE_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.WhileStmtTile));
            types.Add(NodePresenter.ECSharpNode.YIELD_BREAK_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.YieldBreakStmtTile));
            types.Add(NodePresenter.ECSharpNode.YIELD_RETURN_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.YieldReturnStmtTile));
            types.Add(NodePresenter.ECSharpNode.UNSUP_STMT, typeof(code_in.Views.NodalView.NodesElems.Tiles.Statements.UnSupStmtTile));
            
            // Expressions 
            //types.Add(NodePresenter.ECSharpNode.ANONYMOUS_METHOD_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.ANONYMOUS_TYPE_CREATE_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.ARRAY_CREATE_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.ArrayCreateExprNode));
            //types.Add(NodePresenter.ECSharpNode.ARRAY_INITIALIZER_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.AS_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.AsExprNode));
            types.Add(NodePresenter.ECSharpNode.ASSIGNMENT_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.IsExprNode));
            types.Add(NodePresenter.ECSharpNode.ASSIGNMENT_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.BaseReferenceExprNode));
            types.Add(NodePresenter.ECSharpNode.ASSIGNMENT_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.TypeReferenceExprNode));
            //types.Add(NodePresenter.ECSharpNode.BASE_REFERENCE_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.BINARY_OPERATOR_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.BinaryExprNode));
            //types.Add(NodePresenter.ECSharpNode.CAST_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.CHECKED_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.CONDITIONAL_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.DEFAULT_VALUE_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.Def)); // TODO
            //types.Add(NodePresenter.ECSharpNode.DIRECTION_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.ERROR_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.IDENTIFIER_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.IdentifierExprNode));
            types.Add(NodePresenter.ECSharpNode.INDEXER_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.IdentifierExprNode));
            types.Add(NodePresenter.ECSharpNode.INVOCATION_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.FuncCallExprNode));
            types.Add(NodePresenter.ECSharpNode.IS_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.IsExprNode));
            types.Add(NodePresenter.ECSharpNode.IS_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.BaseReferenceExprNode));
            types.Add(NodePresenter.ECSharpNode.IS_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.TypeReferenceExprNode));
            //types.Add(NodePresenter.ECSharpNode.LAMBDA_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.MEMBER_REFERENCE_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.NAMED_ARGUMENT_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.NAMED_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.NULL_REFERENCE_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.NullRefExprNode));
            //types.Add(NodePresenter.ECSharpNode.OBJECT_CREATE_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.Cre)); // TODO
            types.Add(NodePresenter.ECSharpNode.PARENTHESIZED_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.ParenthesizedExprNode));
            //types.Add(NodePresenter.ECSharpNode.POINTER_REFERENCE_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.PRIMITIVE_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.PrimaryExprNode));
            //types.Add(NodePresenter.ECSharpNode.QUERY_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.SIZEOF_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.SizeOfExprNode));
            //types.Add(NodePresenter.ECSharpNode.STACK_ALLOC_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.THIS_REFERENCE_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.TYPE_OF_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.TypeOfExprNode));
            //types.Add(NodePresenter.ECSharpNode.TYPE_REFERENCE_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.UNARY_OPERATOR_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.UnaryExprNode));
            //types.Add(NodePresenter.ECSharpNode.UNCHECKED_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            //types.Add(NodePresenter.ECSharpNode.UNDOCUMENTED_EXPRESSION, typeof(code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode));
            types.Add(NodePresenter.ECSharpNode.UNSUP_EXPR, typeof(code_in.Views.NodalView.NodesElems.Nodes.Expressions.UnSupExpNode));

            try
            {
                var typeTuple = types[nodeType];
                if (typeTuple != null)
                {
                    var newVisualNode = Activator.CreateInstance(typeTuple, (nodalView as ANodalView)._themeResourceDictionary/*TODO beaurk*/, nodalView, linkContainer) as INodeElem;
                    return newVisualNode;
                }
                throw new NotImplementedException("Cannot find the node \"" + nodeType.ToString() + "\" you are trying to instantiate.");
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured : " + e.Message);
            }
            return null;
        }
        // TODO @yaya
        Dictionary<string, List<INodeElem>> SearchMatchinNodes(string name, bool[]userOptions)
        {
            var results = new List<Tuple<string, List<INodeElem>>>();

        //    // 1 Get the nodalView
        //    // 2 parcours les noeuds en fonction si declarations ou execution
        //    //nodalView.RootTileContainer // For research in execution side (stmts and expr)
        //    //toto.MainGrid // Iterate over nodes (declaration)
        //    // 3 pour chaque noeud visuel tu compares recherche avec nom, type...
        //    // 4 if nameFound && iter.Match(userOptions)
        //    // 4.1 list.add();
        //    // return list;
            return results;
        }
        private List<INodeElem> _selectedNodes = null;


        private ResourceDictionary _themeResourceDictionary = null;
        private Point _lastPosition;
        public SearchBar SearchBar = null;

        public ANodalView(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
            _selectedNodes = new List<INodeElem>();
            _lastPosition = new Point();

            this.ZoomPanel.RenderTransform = new ScaleTransform();
            this.SearchBar = new SearchBar(this.GetThemeResourceDictionary());
            this.SearchBar.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            this.SearchBar.SetValue(WidthProperty, Double.NaN); // Width auto
            this.WinGrid.Children.Add(this.SearchBar);
        }


        public ANodalView() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new DefaultCtorVisualException(); }

        #region This

        public void AlignDeclarations()
        {
            if (true)
            {
                int offset_x = 50;
                int pos_x = 0;
                foreach (var nodeUi in this.MainGrid.Children)
                {
                    var nodeElem = (nodeUi as INodeElem);
                    if (nodeElem != null)
                    {
                        nodeElem.SetPosition(pos_x, 0);
                        int x_size;
                        int y_size;
                        nodeElem.GetSize(out x_size, out y_size);
                        pos_x += offset_x + x_size;
                    }
                }
            }
        }
        static public void CreateContextMenuFromOptions(Tuple<EContextMenuOptions, Action<object[]>>[] options, ResourceDictionary themeResDict, object presenter)
        {
            var im = new HexagonalMenu(themeResDict);

            foreach (var opt in options)
            {
                String buttonName = "Default";
                var imageSrc = new BitmapImage();
                imageSrc.BeginInit();
                switch (opt.Item1)
                {
                    case EContextMenuOptions.ADD:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/add.png");
                        buttonName = "Add";
                        break;
                    case EContextMenuOptions.ALIGN:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/align.png");
                        break;
                    case EContextMenuOptions.CLOSE:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/remove.png");
                        break;
                    case EContextMenuOptions.COLLAPSE:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/collapse.png");
                        break;
                    case EContextMenuOptions.COLLAPSEALL:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/collapse.png");
                        break;
                    case EContextMenuOptions.DUPLICATE:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/duplicate.png");
                        break;
                    case EContextMenuOptions.EDIT:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/edit.png");
                        break;
                    case EContextMenuOptions.EXPAND:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/expand.png");
                        break;
                    case EContextMenuOptions.EXPANDALL:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/expand.png");
                        break;
                    case EContextMenuOptions.GOINTO:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/go_into.png");
                        break;
                    case EContextMenuOptions.HELP:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/help.png");
                        break;
                    case EContextMenuOptions.REMOVE:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/remove.png");
                        buttonName = "Remove";
                        break;
                    case EContextMenuOptions.SAVE:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/save.png");
                        break;
                    case EContextMenuOptions.ADD_BREAKPOINT:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/breakpoint.png");
                        break;
                }
                imageSrc.EndInit();

                im.AddHexagonButtonCircle(buttonName, imageSrc, opt.Item2, presenter);
                //im.AddHexagonButtonCircle(buttonName, imageSrc, (parameters) =>
                //{
                //    var lineType = Code_inApplication.MainResourceDictionary["linkType"];
                //    if (lineType == null)
                //        Code_inApplication.MainResourceDictionary["linkType"] = 0;
                //    else
                //        Code_inApplication.MainResourceDictionary["linkType"] = ((int)(lineType) == 0 ? 1 : 0);
                //});
            }

            im.Placement = PlacementMode.Absolute;
            im.PlacementTarget = null;
            var tC = System.Windows.Forms.Control.MousePosition;
            im.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            im.HorizontalOffset = tC.X - (im.DesiredSize.Width / 2);
            im.VerticalOffset = tC.Y - (im.DesiredSize.Height / 2);
            im.IsOpen = true;
        }
        #region Events
        private void SliderZoom(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.ZoomPanel != null && this.offsetGrid != null)
            {
                // http://stackoverflow.com/questions/14729853/wpf-zooming-in-on-an-image-inside-a-scroll-viewer-and-having-the-scrollbars-a
                var middleOfScrollViewer = new Point(this.ScrollView.ActualWidth / 2.0f, this.ScrollView.ActualHeight / 2.0f);
                Point mouseAtImage = this.ScrollView.TranslatePoint(middleOfScrollViewer, this.offsetGrid); // ScrollViewer_CanvasMain.TranslatePoint(middleOfScrollViewer, Canvas_Main);
                Point mouseAtScrollViewer = new Point(this.ScrollView.ActualWidth / 2.0f, this.ScrollView.ActualHeight / 2.0f);// e.GetPosition(this.ScrollView);

                ScaleTransform st = this.ZoomPanel.LayoutTransform as ScaleTransform;
                if (st == null)
                {
                    st = new ScaleTransform();
                    ZoomPanel.LayoutTransform = st;
                }
                st.ScaleX = st.ScaleY = e.NewValue;
                #region [this step is critical for offset]
                ScrollView.ScrollToHorizontalOffset(0);
                ScrollView.ScrollToVerticalOffset(0);
                this.UpdateLayout();
                #endregion

                Vector offset = this.offsetGrid.TranslatePoint(mouseAtImage, ScrollView) - mouseAtScrollViewer; // (Vector)middleOfScrollViewer;
                ScrollView.ScrollToHorizontalOffset(offset.X);
                ScrollView.ScrollToVerticalOffset(offset.Y);
                this.UpdateLayout();
            }
        }
                private bool _movingView = false;
        private Point _lastMousePosFromWinGrid = new Point(0, 0);
        private void ZoomPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                _lastMousePosFromWinGrid = e.GetPosition(this.WinGrid);
                _movingView = true;
                e.Handled = true;
            }
        }

        private void ZoomPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _movingView = false;
        }
        private void ZoomPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            _movingView = false;
        }
        private void ZoomPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_movingView == true)
            {
                Point actualDiff = (Point)(_lastMousePosFromWinGrid - e.GetPosition(this.WinGrid));
                this.ScrollView.ScrollToHorizontalOffset(this.ScrollView.HorizontalOffset + actualDiff.X);
                this.ScrollView.ScrollToVerticalOffset(this.ScrollView.VerticalOffset + actualDiff.Y);
                _lastMousePosFromWinGrid = e.GetPosition(this.WinGrid);
            }
        }
        private void WinGrid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {

                e.Handled = true;
                if (this.ZoomPanel != null && this.offsetGrid != null)
                {
                    // http://stackoverflow.com/questions/14729853/wpf-zooming-in-on-an-image-inside-a-scroll-viewer-and-having-the-scrollbars-a
                    Point mouseAtImage = e.GetPosition(this.offsetGrid); // ScrollViewer_CanvasMain.TranslatePoint(middleOfScrollViewer, Canvas_Main);
                    Point mouseAtScrollViewer = e.GetPosition(this.ScrollView);

                    ScaleTransform st = this.ZoomPanel.LayoutTransform as ScaleTransform;
                    if (st == null)
                    {
                        st = new ScaleTransform();
                        ZoomPanel.LayoutTransform = st;
                    }

                    if (e.Delta > 0)
                    {
                        st.ScaleX = st.ScaleY = st.ScaleX * 1.25;
                        if (st.ScaleX > this.ZoomSlider.Maximum) st.ScaleX = st.ScaleY = this.ZoomSlider.Maximum;
                    }
                    else
                    {
                        st.ScaleX = st.ScaleY = st.ScaleX / 1.25;
                        if (st.ScaleX < this.ZoomSlider.Minimum) st.ScaleX = st.ScaleY = this.ZoomSlider.Minimum;
                    }
                    this.ZoomSlider.Value = st.ScaleX;
                    #region [this step is critical for offset]
                    ScrollView.ScrollToHorizontalOffset(0);
                    ScrollView.ScrollToVerticalOffset(0);
                    this.UpdateLayout();
                    #endregion

                    Vector offset = this.offsetGrid.TranslatePoint(mouseAtImage, ScrollView) - mouseAtScrollViewer; // (Vector)middleOfScrollViewer;
                    ScrollView.ScrollToHorizontalOffset(offset.X);
                    ScrollView.ScrollToVerticalOffset(offset.Y);
                    this.UpdateLayout();
                }

            }
        }

        #region Events.Keys
        void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            int step = 2;
            Rect tmp = (Rect)Code_inApplication.MainResourceDictionary["RectDims"];
            if (e.Key == Key.S && Keyboard.IsKeyDown(Key.LeftCtrl))
                this.Presenter.Save();
            if (e.Key == Key.Add)
            {
                tmp.Width += step;
                tmp.Height += step;
            }
            if (e.Key == Key.Subtract && tmp.Width > 15)
            {
                tmp.Width -= step;
                tmp.Height -= step;
            }

            if (e.Key == Key.L)
            {
                var lineType = Code_inApplication.MainResourceDictionary["linkType"];
                if (lineType == null)
                    Code_inApplication.MainResourceDictionary["linkType"] = 0;
                else
                    Code_inApplication.MainResourceDictionary["linkType"] = ((int)(lineType) == 0 ? 1 : 0);
            }
        }
        #endregion Events.Keys
        #region Events.Mouse
        private void MainGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Code_inApplication.RootDragNDrop.UnselectAllNodes();
        }
        void MainView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Code_inApplication.RootDragNDrop.DragMode != EDragMode.NONE)
            {
                Code_inApplication.RootDragNDrop.Drop(this);
                e.Handled = true;
            }
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            EDragMode dragMode = (Keyboard.IsKeyDown(Key.LeftCtrl) ? EDragMode.MOVEOUT : EDragMode.STAYINCONTEXT);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Code_inApplication.RootDragNDrop.UpdateDragInfos(dragMode, e.GetPosition(this.MainGrid));
                e.Handled = true;
            }
        }

        private void MainGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var options = Presenter.GetMenuOptions();
            CreateContextMenuFromOptions(options, this.GetThemeResourceDictionary(), this.Presenter);
            e.Handled = true;
        }

        private void MainGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            Code_inApplication.RootDragNDrop.Drop(null);
        }
        #endregion Events.Mouse
        #region Events.Others
        private void changeResourceLink(object sender, ResourcesEventArgs e)
        {
            //if (this.MainGrid != null)
            //    foreach (var t in this.MainGrid.Children)
            //    {
            //        if (t.GetType() == typeof(Code_inLink))
            //        {
            //            _link = (Code_inLink)t;
            //            _link.changeLineMode();
            //        }
            //    }
        }
        #endregion Events.Others
        #endregion Events
        #endregion This
        #region INodalView
        #endregion INodalView
        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public void SetThemeResources(String keyPrefix) { throw new NotImplementedException(); }
        #endregion ICodeInVisual
        #region IContainerDragNDrop
        public new void Drop(IEnumerable<IDragNDropItem> items)
        {
            this.UpdateDragInfos(_lastPosition);
        }

        public abstract bool IsDropValid(IEnumerable<IDragNDropItem> items);
        //{
        //    if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.STAYINCONTEXT)
        //        return true;

        //    return false; // Quick fix
        //    foreach (var i in items)
        //    {
        //        if (this.IsDeclarative)
        //            return ((i is code_in.Views.NodalView.NodesElems.Nodes.ClassDeclNode) || (i is code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode) || (i is code_in.Views.NodalView.NodesElems.Nodes.UsingDeclNode));
        //    }
        //    return false;

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePosition"> must be mouse position relative to NodalView.MainGrid</param>
        public void UpdateDragInfos(Point mousePosition)
        {
            var selectedNodes = Code_inApplication.RootDragNDrop.SelectedItems;
            if (selectedNodes.Count == 0)
                return;
            Vector diff;
            if ((_lastPosition.X + _lastPosition.Y) < 0.01)
                diff = new Vector(0, 0);
            else
                diff = _lastPosition - mousePosition;
            _lastPosition = mousePosition;

            foreach (var selNode in selectedNodes)
            {
                dynamic draggingNode = selNode;
                Thickness margin = (Thickness)draggingNode.GetType().GetProperty("Margin").GetValue(draggingNode);
                double marginLeft = margin.Left;
                double marginTop = margin.Top;
                Thickness newMargin = new Thickness();

                newMargin.Left = margin.Left;
                newMargin.Top = margin.Top;
                newMargin.Right = margin.Right;
                newMargin.Bottom = margin.Bottom;
                newMargin.Left -= diff.X;
                newMargin.Top -= diff.Y;
                newMargin.Left = Math.Max(newMargin.Left, 0);
                newMargin.Top = Math.Max(newMargin.Top, 0);
                draggingNode.SetPosition((int)newMargin.Left, (int)newMargin.Top);
            }
        }

        public void Drag(EDragMode dragMode)
        {
            _lastPosition = new Point(0.0, 0.0);
        }

        #endregion IContainerDragNDrop
        #region IVisualNodeContainer
        public T CreateAndAddNode<T>(INodePresenter nodePresenter) where T : UIElement, code_in.Views.NodalView.INode
        {
            System.Diagnostics.Debug.Assert(nodePresenter != null, "nodePresenter must be a non-null value");
            T node = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary, this, nodePresenter);

            node.SetParentView(this);
            node.SetNodePresenter(nodePresenter);
            nodePresenter.SetView(node);
            this.AddNode(node);
            return node;
        }
        public void AddNode<T>(T node, int index = -1) where T : UIElement, code_in.Views.NodalView.INode
        {
            this.MainGrid.Children.Add(node as UIElement);
        }

        public void RemoveNode(INodeElem node)
        {
            this.MainGrid.Children.Remove(node as UIElement);
        }

        public void RemoveTile(INodeElem tile)
        {
            this.MainGrid.Children.Remove(tile as UIElement);
        }

        #endregion IVisualNodeContainer


        public IEnvironmentWindowWrapper EnvironmentWindowWrapper
        {
            get;
            set;
        }


        public bool IsSaved
        {
            get { return Presenter.IsSaved; }
        }

        public void Save()
        {
            this.Presenter.Save();
        }

        public INodalPresenter Presenter
        {
            get;
            set;
        }
    } // Class
} // Namespace
