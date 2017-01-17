using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class CheckedStmtTile : BaseTile
    {
        public FlowTileItem itemsChecked = null;

        public override bool IsExpanded
        {
            get
            {
                return itemsChecked.IsExpanded;
            }
            set
            {
                itemsChecked.IsExpanded = value;
            }
        }

        public CheckedStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Checked");
            itemsChecked = this.CreateAndAddItem<FlowTileItem>();
        }
        public CheckedStmtTile() :
            base(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            this.SetName("Checked");
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.CheckedStatement);
        }
    }
}
