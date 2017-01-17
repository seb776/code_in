using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class GotoStmtTile : BaseTile
    {
        public ExpressionItem label = null;

        public override bool IsExpanded
        {
            get
            {
                return label.IsExpanded;
            }
            set
            {
                label.IsExpanded = value;
            }
        }

        public GotoStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Goto");
            label = this.CreateAndAddItem<ExpressionItem>();
        }
        public GotoStmtTile() :
            base(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.GotoStatement);
            label.SetName(stmt.Label);
        }
    }
}