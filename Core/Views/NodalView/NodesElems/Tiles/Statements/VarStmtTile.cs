using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class VarStmtTile : BaseTile
    {
        public VarStmtTile(ResourceDictionary themeResDict) :
             base(themeResDict)
        {
            this.SetName("Variables");
        }
        public VarStmtTile() :
            base(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }
    }
}
