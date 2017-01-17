using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using code_in.Views.NodalView.NodesElems;
using code_in.Presenters.Nodal.Nodes;
using code_in.Presenters.Nodal;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;
using code_in.Exceptions;
using code_in.Views.Utils;

namespace code_in.Views.NodalView.NodesElems.Items.Base
{

    /// <summary>
    /// Interaction logic for ANodeItem.xaml
    /// </summary>
    public abstract partial class ANodeItem : UserControl, code_in.Views.NodalView.INode
    {
        public bool IsExpanded
        {
            get
            {
                return true;
            }
            set
            {
            }
        }
        public INodePresenter Presenter
        {
            get;
            set;
        }
        private IContainerDragNDrop _parentView = null;
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        EditNodePanel EditMenu = null;

        public void Remove()
        {
            (this._parentView as IVisualNodeContainer).RemoveNode(this);
        }
        public void InstantiateASTNode()
        {
        }


        protected ANodeItem(ResourceDictionary themeResDict, INodalView nodalView, INodePresenter presenter)
        {
            Presenter = presenter;
            this.NodalView = nodalView;
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(this._languageResourceDictionary);
            InitializeComponent();
            this.MouseEnter += Item_MouseEnter;
            this.MouseLeave += Item_MouseLeave;
        }
        protected ANodeItem() :
            this(Code_inApplication.MainResourceDictionary, null, null)
        { throw new DefaultCtorVisualException(); }

        void Item_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Background = new SolidColorBrush(Colors.Transparent);
            if (Code_inApplication.RootDragNDrop.IsSelectedItem(this))
                this.SelectHighLight(true);
            this.OnMouseLeave();
        }

        void Item_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromArgb(0x21, 0xFF, 0xFF, 0xFF));
            this.OnMouseEnter();
        }

        public virtual void OnMouseEnter()
        { }
        public virtual void OnMouseLeave()
        { }

        private void MainLayout_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            code_in.Views.NodalView.ANodalView.CreateContextMenuFromOptions(this.Presenter.GetMenuOptions(), this.GetThemeResourceDictionary(), this.Presenter);
        }

        public void ShowEditMenu()
        {
            EditMenu = new EditNodePanel(_themeResourceDictionary);
            EditMenu.SetFields(Presenter);
            EditMenu.IsOpen = true;
            EditMenu.PlacementTarget = FindVisualAncestor.FindParent<Grid>(this.NodalView as ANodalView);//this.EditItemPanelField;
            EditMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
            //EditMenu.VerticalOffset -= EditMenu.ActualHeight;
        }

        #region INodeElem
        public void SetParentView(IContainerDragNDrop parent) { _parentView = parent; }
        public IContainerDragNDrop GetParentView() { return _parentView; }
        public void SetName(String name)
        {
            this.ItemName.Content = name;
        }
        public String GetName()
        {
            return this.ItemName.Content as String;
        }
        public void SetNodePresenter(INodePresenter nodePresenter) // TODO To remove
        {
            System.Diagnostics.Debug.Assert(nodePresenter != null);
            Presenter = nodePresenter;
        }
        public void AddGeneric(string name, EGenericVariance variance)
        {
            GenericLabel.Content += variance.ToString().ToLower() + " " + name;
        }
        #endregion INodeElem
        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }

        public abstract void SetThemeResources(String keyPrefix);

        public void SetLanguageResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual

        
        public void SetPosition(int posX, int posY)
        {
            //throw new InvalidOperationException("As an item cannot be outside a node and cannot be flying, we cannot not set its position.");
        }


        public void GetSize(out int x, out int y)
        {
            x = 0;
            y = 0;
            //throw new NotImplementedException();
        }


        public void SetSelected(bool isSelected)
        {
            throw new NotImplementedException();
        }


        public Point GetPosition()
        {
            return new Point(this.Margin.Left, this.Margin.Top);
        }

        public void SelectHighLight(bool highlighetd)
        {
            if (highlighetd)
                this.Background = new SolidColorBrush(Color.FromArgb(0x42, 0xE2, 0x4E, 0x42));
            else
                this.Background = new SolidColorBrush(Color.FromArgb(0x0, 0,0,0));
        }


        public abstract void UpdateDisplayedInfosFromPresenter();

        public void MustBeRemovedFromContext()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromContext()
        {
            throw new NotImplementedException();
        }

        public INodalView NodalView
        {
            get;
            set;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Code_inApplication.RootDragNDrop.IsSelectedItem(this))
                Code_inApplication.RootDragNDrop.UnselectAllNodes();
            Code_inApplication.RootDragNDrop.AddSelectItem(this);

            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events

        }


        public void FocusToNode()
        {
            ((ANodalView)this.NodalView).FocusToNode(this);
        }
    } // Class
} // Namespace
