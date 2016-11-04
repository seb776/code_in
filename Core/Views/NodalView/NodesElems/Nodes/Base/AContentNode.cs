using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    public abstract class AContentNode : BaseNode, IVisualNodeContainerDragNDrop
    {
        protected AContentNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
//            this.SetDynamicResources("AcontentNode");
        }

        #region IVisualNodeContainer
        /// <summary>
        /// Creates a INodeElem of type T and add it to this control by passing all required parameters (theme, language...)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T CreateAndAddNode<T>(INodePresenter nodePresenter) where T : UIElement, code_in.Views.NodalView.INode
        {
            T node = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary());
            node.SetParentView(this);
            node.SetNodePresenter(nodePresenter);
            nodePresenter.SetView(node);
            try
            {
                this.AddNode(node);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return default(T);
            }
            return node;
        }
        public abstract void AddNode<T>(T node, int index = -1) where T : UIElement, code_in.Views.NodalView.INode;
        public abstract void RemoveNode(INodeElem node);

        public abstract int GetDropIndex(Point pos);
        public abstract void HighLightDropPlace(Point pos);

        #endregion IVisualNodeContainer

        #region ICodeInVisual
        public override void SetThemeResources(string keyPrefix)
        {
        }
        #endregion ICodeInVisual

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

        public void AddSelectNode(IDragNDropItem item)
        {
            throw new NotImplementedException();
        }

        public void AddSelectNodes(List<IDragNDropItem> items)
        {
            throw new NotImplementedException();
        }

        public void Drag(EDragMode dragMode)
        {
            throw new NotImplementedException();
        }

        public void UpdateDragInfos()
        {
            throw new NotImplementedException();
        }

        public new void Drop(List<IDragNDropItem> items)
        {
            throw new NotImplementedException();
        }
    }
}
