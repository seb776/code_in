using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class YieldReturnStmtTile : BaseTile
    {
        public ExpressionItem Expression = null;

        public YieldReturnStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("YieldReturn;");
            Expression = this.CreateAndAddItem<ExpressionItem>(true);
        }
        public YieldReturnStmtTile() :
            base(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.YieldReturnStatement);
            this.SetName("YieldReturn");
            Expression.SetName(stmt.Expression.ToString());
        }
    }
}
