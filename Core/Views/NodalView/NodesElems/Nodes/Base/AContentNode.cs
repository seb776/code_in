using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    public abstract class AContentNode : BaseNode, IVisualNodeContainer
    {
        protected AContentNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.MouseUp += EvtDropNode;
//            this.SetDynamicResources("AcontentNode");
        }

        void EvtDropNode(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.GetRootView().DropNodes(this);
            e.Handled = true;
        }
        #region IVisualNodeContainer
        /// <summary>
        /// Creates a INodeElem of type T and add it to this control by passing all required parameters (theme, language...)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T CreateAndAddNode<T>() where T : UIElement, INodeElem
        {
            T node = (T)Activator.CreateInstance(typeof(T), this.GetThemeResourceDictionary());
            node.SetParentView(this);
            node.SetRootView(this.GetRootView());
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
        public abstract void AddNode<T>(T node, int index = -1) where T : UIElement, INodeElem;
        public abstract override void RemoveNode(INodeElem node);

        public abstract int GetDropIndex(Point pos);
        public abstract void HighLightDropPlace(Point pos);

        public override void MoveNodeSpecial()
        {
        }

        #endregion IVisualNodeContainer
    }
}
