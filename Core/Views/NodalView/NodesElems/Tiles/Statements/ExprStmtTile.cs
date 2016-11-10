using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ExprStmtTile : BaseTile
    {
        public ExpressionItem Expression;

        public ExprStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("");
            Expression = this.CreateAndAddItem<ExpressionItem>(true);
        }
        public ExprStmtTile() :
            this(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            Debug.Assert(this._presenter != null);
            var exprStmt = _presenter.GetASTNode();
           this.Expression.SetName(exprStmt.ToString().Remove(exprStmt.ToString().LastIndexOf(Environment.NewLine)));
           //this.Expression.SetName(exprStmt.ToString().Replace(System.Environment.NewLine, ""));
        }
    }
}
