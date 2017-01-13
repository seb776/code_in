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

namespace code_in.Views.NodalView
{
    public class ExecutionNodalView : ANodalView
    {
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


    }
}
