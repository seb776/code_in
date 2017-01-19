using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class BreakStmtTile : BaseTile
    {
        public override bool IsExpanded
        {
            get
            {
                return true;
            }
            set
            {
            }
        }
        public BreakStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("break");
        }
        public BreakStmtTile() :
            this(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            this.SetName("break");
        }
    }
}
