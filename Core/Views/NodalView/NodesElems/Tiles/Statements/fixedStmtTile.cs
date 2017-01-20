using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class fixedStmtTile : BaseTile
    {
        public FlowTileItem itemsFixed = null;

        public override bool IsExpanded
        {
            get
            {
                return itemsFixed.IsExpanded;
            }
            set
            {
                itemsFixed.IsExpanded = value;
            }
        }

        public fixedStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Fixed");
            itemsFixed = this.CreateAndAddItem<FlowTileItem>();
        }
        public fixedStmtTile() :
            base(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.FixedStatement);
            //this.SetName("Fixed " + stmt.Type.ToString() + " " + stmt.Variables.ToString());
        }

        public override void UpdateAnchorAttachAST()
        {
            // useless here
        }
    }
}
