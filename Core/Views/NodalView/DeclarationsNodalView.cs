using code_in.Presenters.Nodal;
using code_in.Views.NodalView.NodesElems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Permissions;

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
            var listClass = new List<INodeElem>();
            var listMembers = new List<INodeElem>();
            var listEnums = new List<INodeElem>();
            var listNamespaces = new List<INodeElem>();
            foreach (var nodeView in _registeredNodes)
            {
                var nodePresenter = nodeView.Presenter;
                var nodeAST = nodePresenter.GetASTNode();

                if (ANodalView.NameMatch(name, nodeView.GetName(), true))
                {
                    if (nodeAST is ICSharpCode.NRefactory.CSharp.MethodDeclaration)
                    {
                        listFunc.Add(nodeView);
                    }
                    else if (nodeAST is ICSharpCode.NRefactory.CSharp.TypeDeclaration)
                    {
                        listClass.Add(nodeView);
                    }
                    else if (nodeAST is ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)
                    {
                        listClass.Add(nodeView);
                    }
                }
            }

            if (listFunc.Count != 0)
                results.Add("function", listFunc);
            if (listClass.Count != 0)
                results.Add("class", listClass);
            if (listMembers.Count != 0)
                results.Add("Members", listMembers);
            if (listEnums.Count != 0)
                results.Add("Enums", listEnums);
            if (listNamespaces.Count != 0)
                results.Add("Namespace", listNamespaces);
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
            FileSystemWatcher w = new FileSystemWatcher();

            if (this.NodalPresenterDecl._model != null)
            {
                var pathFile = this.NodalPresenterDecl._model.FilePath;

                w.Path = Path.GetDirectoryName(pathFile);
                // @ Zor (Hamham) : je t'ai mis juste le filtre quand la taille du fichier change (https://msdn.microsoft.com/fr-fr/library/system.io.notifyfilters(v=vs.110).aspx)
                /* petit truc tet un peu dérangeant, je ne sais pas si il y a un paramètre ou autre mais quand le fichier est déjà ouvert 
                 * avant que tu ne l'ouvres dans Code_in, si tu fais des changement, le callback ne sera pas appelé
                 * Tu dois obligatoirement avoir ouvert le fichier dans code_in puis l'ouvrir à l'extérieur et faire le changement pour que cela marche
                 */
                w.NotifyFilter = NotifyFilters.Size;
                w.Filter = Path.GetFileName(pathFile);
                w.Changed += OnChanged;

                w.EnableRaisingEvents = true;
            }

        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            MessageBox.Show("changed");
        }
    }
}
