using code_in.Presenters.Nodal;
using code_in.Views.NodalView.NodesElems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView
{
    public static class NodalViewActions
    {
        public static void AttachNodalViewAndPresenter(INodalView view, INodalPresenter presenter)
        {
            view.Presenter = presenter;
            presenter.View = view;
        }
    }

    public class DeclarationsNodalView : ANodalView
    {
        public override Dictionary<string, List<INodeElem>> SearchMatchinNodes(string name, bool[] userOptions)
        {
            var results = new Dictionary<string, List<INodeElem>>();

            var listFunc = new List<INodeElem>();
            foreach (var c in this.MainGrid.Children)
            {
                if (c is INodeElem)
                    listFunc.Add(c as INodeElem);
            }
            
            results.Add("function", listFunc);
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
        public override bool IsDropValid(IEnumerable<IDragNDropItem> items)
        {
            if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.STAYINCONTEXT)
                return true;

            return false; // Quick fix
            foreach (var i in items)
            {
                return ((i is code_in.Views.NodalView.NodesElems.Nodes.ClassDeclNode) || (i is code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode) || (i is code_in.Views.NodalView.NodesElems.Nodes.UsingDeclNode));
            }
            return false;
        }
        public DeclarationsNodalPresenterLocal NodalPresenterDecl
        {
            get
            {
                return Presenter as DeclarationsNodalPresenterLocal;
            }
            private set
            {
                Debug.Assert(value.GetType() == typeof(DeclarationsNodalPresenterLocal));
                Presenter = value;
            }
        }

        public DeclarationsNodalView(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
        }

        public void OpenFile(String path)
        {
            // TODO Show Animation loadingFile
            this.NodalPresenterDecl.OpenFile(path);
            AlignDeclarations();
        }
    }
}
