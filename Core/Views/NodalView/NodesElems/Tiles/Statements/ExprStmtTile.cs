using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ExprStmtTile : BaseTile
    {
        public ExpressionItem Expression;

        public ExprStmtTile(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetName("");
            Expression = this.CreateAndAddItem<ExpressionItem>(true);
        }
        public ExprStmtTile() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

    }
}
