using code_in.Presenters.Nodal;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using code_in.Tools;

namespace code_in.Views.NodalView
{
    public class ExecutionNodalView : ANodalView, ITileContainer
    {
        public class SearchOptions
        {
            private SearchOptions()
            {}
            public SearchOptions(bool caseSensitive)
            {
                CaseSensitive = caseSensitive;
            }
            public bool CaseSensitive;
        };
        public override Dictionary<string, List<INodeElem>> SearchMatchinNodes(string name, SearchOptions userOptions)
        {
            var localDict = new Dictionary<string, List<INodeElem>>();
            var declDict = this.ExecPresenter.ExecModel.AssociatedFile.Presenter.View.SearchMatchinNodes(name, userOptions);

            if (true) // option for decls
                localDict = declDict;

            foreach (var nodeView in _registeredNodes)
            {
                bool shouldAdd = false;
                string categoryString = "";
                var nodePresenter = nodeView.Presenter;
                var nodeAST = nodePresenter.GetASTNode();
                if (nodeView.GetName().Contains(name, userOptions.CaseSensitive) >= 0)//ANodalView.NameMatch(name, nodeView.GetName(), true))
                {
                    shouldAdd = true;
                    if (nodeAST is ICSharpCode.NRefactory.CSharp.InvocationExpression)
                        categoryString = "function";
                    else
                        categoryString = "others";

                }
                if (shouldAdd)
                {
                    if (!localDict.ContainsKey(categoryString))
                        localDict.Add(categoryString, null);
                    if (localDict[categoryString] == null)
                        localDict[categoryString] = new List<INodeElem>();
                    localDict[categoryString].Add(nodeView);
                }
            }

            return declDict;// localDict;
        }
        public ExecutionNodalPresenterLocal ExecPresenter
        {
            get
            {
                return Presenter as ExecutionNodalPresenterLocal;
            }
        }
        public ExecutionNodalView(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            RootTileContainer = new TileContainer(themeResDict, this) as ITileContainer;
            RootTileContainer.SetParentView(this);
            this.MainGrid.Children.Add(RootTileContainer as TileContainer);
        }
        public void EditFunction(FuncDeclItem node)
        {
            this.EnvironmentWindowWrapper.SetTitle(this.Presenter.DocumentName + ":" + node.GetName() + "()");
            this.ExecPresenter.EditFunction(node);
        }
        public override bool IsDropValid(IEnumerable<IDragNDropItem> items)
        {
            if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.STAYINCONTEXT)
                return true;
            return false;
        }
        public override void Align()
        {
            // TODO
            //foreach (var stmt in _registeredNodes)
            //{
            //    if (stmt is BaseTile)
            //        (stmt as BaseTile).Ali
            //}
        }
        public void EditProperty(PropertyItem node, bool isGetter)
        {
            if (isGetter)
            {
                this.EnvironmentWindowWrapper.SetTitle(this.Presenter.DocumentName + ":" + node.GetName() + "{get}");
                this.ExecPresenter.EditAccessor(node.PropertyNode.Getter);
            }
            else
            {
                this.EnvironmentWindowWrapper.SetTitle(this.Presenter.DocumentName + ":" + node.GetName() + "{set}");
                this.ExecPresenter.EditAccessor(node.PropertyNode.Setter);
            }
        }
        public void EditConstructor(ConstructorItem node)
        {
            this.EnvironmentWindowWrapper.SetTitle(this.Presenter.DocumentName + ":" + node.GetName() + "(ctor)");
            this.ExecPresenter.EditConstructor(node);
        }
        public void EditDestructor(DestructorItem node)
        {
            this.ExecPresenter.EditDestructor(node);
            this.EnvironmentWindowWrapper.SetTitle(this.Presenter.DocumentName + ":~" + node.GetName() + "()");
        }
        public ITileContainer RootTileContainer
        {
            get;
            set; // TODO From seb set it to private
        }



        public T CreateAndAddTile<T>(Presenters.Nodal.Nodes.INodePresenter presenter) where T : BaseTile
        {
            var visualNode = RootTileContainer.CreateAndAddTile<T>(presenter);
            this.AddTile<T>(visualNode);
            return visualNode;
        }

        public void AddTile<T>(T tile, int index = -1) where T : BaseTile
        {
            this.ExecPresenter.ExecModel.Root.AddChildWithExistingRole(tile.Presenter.GetASTNode());
            _registeredNodes.Add(tile);
        }

        public void RemoveTile(BaseTile tile)
        {
            RootTileContainer.RemoveTile(tile);
            _registeredNodes.Remove(tile);
        }

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

        public void UpdateDisplayedInfosFromPresenter()
        {
            throw new NotImplementedException();
        }

        public INodalView NodalView
        {
            get
            {
                return this;
            }
            set
            {
            }
        }

        public void SelectHighLight(bool highlighetd)
        {
            throw new NotImplementedException();
        }

        public void SetParentView(IContainerDragNDrop vc)
        {
            throw new NotImplementedException();
        }

        public IContainerDragNDrop GetParentView()
        {
            throw new NotImplementedException();
        }
    }
}
