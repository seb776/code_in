using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace code_in.Views.NodalView.NodesElems.Tiles.Items
{
    /// <summary>
    /// A TileItem to contain expressions (nodes).
    /// </summary>
    public partial class ExpressionItem : UserControl, ITileItem, IVisualNodeContainerDragNDrop, ICodeInVisual, ICodeInTextLanguage
    {
        private DataFlowAnchor _exprOut = null;
        private ResourceDictionary _themeResourceDictionary = null;
        public ExpressionItem(ResourceDictionary themeResourceDictionary)
        {
            Debug.Assert(themeResourceDictionary != null);
            _themeResourceDictionary = themeResourceDictionary;
            this.Resources.MergedDictionaries.Add(_themeResourceDictionary);
            InitializeComponent();
            _exprOut = new DataFlowAnchor(themeResourceDictionary);
            this.ExpressionsGrid.Children.Add(_exprOut);
        }
        public ExpressionItem() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        #region IVisualNodeContainerDragNDrop
        public bool IsDropNodeValid()
        {
            throw new NotImplementedException();
        }

        public int GetDropNodeIndex(Point pos)
        {
            throw new NotImplementedException();
        }

        public void HighLightDropNodePlace(Point pos)
        {
            throw new NotImplementedException();
        }

        public T CreateAndAddNode<T>(Presenters.Nodal.Nodes.INodePresenter nodePresenter) where T : UIElement, Presenters.Nodal.INodeElem
        {
            System.Diagnostics.Debug.Assert(nodePresenter != null, "nodePresenter must be a non-null value");
            T node = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary);

            node.SetParentView(null);
            //node.SetRootView(this); // TODO @Seb to see
            node.SetNodePresenter(nodePresenter);
            nodePresenter.SetView(node);
            if (typeof(AIONode).IsAssignableFrom(typeof(T)))
            {
                //(node as AIONode).SetParentLinksContainer(this);
            }
            //_visualNodes.Add(node); // TODO @Seb @Steph For automatic placement
            this.AddNode(node);
            return node;
        }

        public void AddNode<T>(T node, int idx = -1) where T : UIElement, Presenters.Nodal.INodeElem
        {
            this.ExpressionsGrid.Children.Add(node as UIElement);
        }

        public void RemoveNode(Presenters.Nodal.INodeElem node)
        {
            this.ExpressionsGrid.Children.Remove(node as UIElement);
        }
        #endregion IVisualNodeContainerDragNDrop

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary()
        {
            return _themeResourceDictionary;
        }

        public void SetThemeResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual

        #region ICodeInTextLanguage
        public ResourceDictionary GetLanguageResourceDictionary()
        {
            throw new NotImplementedException();
        }

        public void SetLanguageResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInTextLanguage
    }
}
