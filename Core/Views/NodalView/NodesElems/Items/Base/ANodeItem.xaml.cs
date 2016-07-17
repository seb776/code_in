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

namespace code_in.Views.NodalView.NodesElems.Items.Base
{
    /// <summary>
    /// Interaction logic for ANodeItem.xaml
    /// </summary>
    public abstract partial class ANodeItem : UserControl, INodeElem, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        EditNodePanel EditMenu = null;
        protected IVisualNodeContainer _parentView = null;
        private IRootDragNDrop _rootView = null;
        protected INodePresenter _nodePresenter = null;

        protected ANodeItem(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(this._languageResourceDictionary);
            InitializeComponent();
            this.MouseEnter += Item_MouseEnter;
            this.MouseLeave += Item_MouseLeave;
        }
        protected ANodeItem() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        void Item_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Background = new SolidColorBrush(Colors.Transparent);
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
            NodalView.CreateContextMenuFromOptions(this._nodePresenter.GetMenuOptions(), this.GetThemeResourceDictionary(), this._nodePresenter);
        }
        public void ShowEditMenu()
        {
/*            this.EditMenuAndAttributesLayout.Children.Clear();
            EditMenu = new EditNodePanel(_themeResourceDictionary);
            EditMenu.SetFields(_nodePresenter);
            this.EditMenuAndAttributesLayout.Children.Add(EditMenu);*/
        }

        #region INodeElem
        public void SetParentView(IVisualNodeContainer parent) { _parentView = parent; }
        public IVisualNodeContainer GetParentView() { return _parentView; }
        public void SetRootView(IRootDragNDrop root) { _rootView = root; }
        public IRootDragNDrop GetRootView() { return _rootView; }
        public void SetName(String name)
        {
            this.ItemName.Content = name;
        }
        public String GetName()
        {
            return this.ItemName.Content as String;
        }
        public void SetNodePresenter(INodePresenter nodePresenter)
        {
            System.Diagnostics.Debug.Assert(nodePresenter != null);
            _nodePresenter = nodePresenter;
        }
        public void AddGeneric(string name, EGenericVariance variance)
        { }
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
    } // Class
} // Namespace
