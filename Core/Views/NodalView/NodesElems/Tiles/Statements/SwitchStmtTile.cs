using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class SwitchStmtTile : BaseTile
    {
        public ExpressionItem Expression;
        public List<FlowTileItem> itemCases;
        public List<ExpressionItem> ExpressionCases;

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
    }
}
