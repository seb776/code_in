using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class VarStmtTile : BaseTile
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
        public VarStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Variables");
        }
        public VarStmtTile() :
            base(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            Debug.Assert(Presenter != null);
            var varDecl = Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.VariableDeclarationStatement;
            this.SetName(varDecl.Type.ToString());
        }

        public override void UpdateAnchorAttachAST()
        {
            // useless here
        }
    }
}
