using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    public class SwitchStmtTile : BaseTile
    {
        public ExpressionItem Expression;
        public List<FlowTileItem> itemCases;
        public List<ExpressionItem> ExpressionCases;

        public override bool IsExpanded
        {
            get
            {
                return Expression.IsExpanded;
            }
            set
            {
                Expression.IsExpanded = value;
                if (itemCases != null)
                {
                    foreach (var a in itemCases)
                        a.IsExpanded = value;
                }
                if (ExpressionCases != null)
                {
                    foreach (var a in ExpressionCases)
                        a.IsExpanded = value;
                }
            }
        }

        public SwitchStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Switch");
            Expression = this.CreateAndAddItem<ExpressionItem>(true);
            itemCases = new List<FlowTileItem>();
            ExpressionCases = new List<ExpressionItem>();
        }
        public SwitchStmtTile() :
            base(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.SwitchStatement);
            Expression.SetName(stmt.Expression.ToString());
            int i = 0;

            foreach (var cases in stmt.SwitchSections)
            {
                foreach (var labels in cases.CaseLabels)
                {
                    ExpressionCases[i].SetName(labels.ToString());
                }
                ++i;
            }
        }

        public override void UpdateAnchorAttachAST()
        {
            var ifStmt = this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.SwitchStatement;
            this.Expression.ExprOut.SetASTNodeReference((e) => { ifStmt.Expression = e; });
            // TODO not complete
        }
    }
}
