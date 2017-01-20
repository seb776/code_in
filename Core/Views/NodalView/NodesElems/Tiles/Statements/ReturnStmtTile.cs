using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ReturnStmtTile : BaseTile
    {
        public ExpressionItem Expression = null;
        public override bool IsExpanded
        {
            get
            {
                return Expression.IsExpanded;
            }
            set
            {
                Expression.IsExpanded = value;
            }
        }
        public ReturnStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("return");
            Expression = this.CreateAndAddItem<ExpressionItem>(true);
        }
        public ReturnStmtTile() :
            base(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.ReturnStatement);
            this.SetName("return");
            Expression.SetName(stmt.Expression.ToString());
        }

        public override void UpdateAnchorAttachAST()
        {
            var ifStmt = this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.ReturnStatement;
            this.Expression.ExprOut.SetASTNodeReference((e) => { ifStmt.Expression = e; });
        }
    }
}
