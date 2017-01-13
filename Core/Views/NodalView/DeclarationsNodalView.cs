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
