using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElems.Items;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    public abstract class AContentNode : BaseNode, IContainerDragNDrop, IVisualNodeContainer
    {
        protected AContentNode(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
//            this.SetDynamicResources("AcontentNode");
            this.ContentLayout.MouseLeftButtonUp += ContentLayout_MouseLeftButtonUp;
            this.ContentLayout.MouseMove += ContentLayout_MouseMove;
        }
        void ContentLayout_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                EDragMode dragMode = (Keyboard.IsKeyDown(Key.LeftCtrl) ? EDragMode.MOVEOUT : EDragMode.STAYINCONTEXT);
                Code_inApplication.RootDragNDrop.UpdateDragInfos(dragMode, e.GetPosition((this.NodalView as NodalView.ANodalView).MainGrid));
                e.Handled = true;
            }
        }

        void ContentLayout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Code_inApplication.RootDragNDrop.DragMode != EDragMode.NONE)
                Code_inApplication.RootDragNDrop.Drop(this);
            e.Handled = true;
        }
        #region IVisualNodeContainer
        /// <summary>
        /// Creates a INodeElem of type T and add it to this control by passing all required parameters (theme, language...)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T CreateAndAddNode<T>(INodePresenter nodePresenter) where T : UIElement, code_in.Views.NodalView.INode
        {
            T node = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary(), this.NodalView, nodePresenter);
            node.SetParentView(this);
            node.NodalView = this.NodalView;
            nodePresenter.SetView(node);
            try
            {
                this.AddNode(node);
                if (typeof(T) == typeof(FuncDeclItem))
                {
                    (node as FuncDeclItem).MethodNode = nodePresenter.GetASTNode() as MethodDeclaration;
                }
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
        #region IContainerDragNDrop

        public void AddSelectNode(IDragNDropItem item)
        {
            Code_inApplication.RootDragNDrop.AddSelectItem(item);
        }

        public void AddSelectNodes(List<IDragNDropItem> items)
        {
            foreach (var i in items)
                this.AddSelectNode(i);
        }

        public abstract void Drag(EDragMode dragMode);

        public new void Drop(List<IDragNDropItem> items)
        {
            throw new NotImplementedException();
        }


        public void UnselectNode(IDragNDropItem item)
        {
            throw new NotImplementedException();
        }

        public void UnselectAllNodes()
        {
            Code_inApplication.RootDragNDrop.UnselectAllNodes();
        }
        #endregion IContainerDragNDrop

        public abstract void Drop(IEnumerable<IDragNDropItem> items);


        public bool IsDropValid(IEnumerable<IDragNDropItem> items)
        {
            if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.STAYINCONTEXT)
                return true;
            return false; // TODO @Seb
        }


        public abstract void UpdateDragInfos(Point mousePosToMainGrid);
    }
}
