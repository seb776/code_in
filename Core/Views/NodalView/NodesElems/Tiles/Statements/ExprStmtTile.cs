﻿using code_in.Views.NodalView.NodesElems.Tiles.Items;
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
            Debug.Assert(Presenter != null);
            var exprStmt = Presenter.GetASTNode();
           this.Expression.SetName(exprStmt.ToString().Remove(exprStmt.ToString().LastIndexOf(Environment.NewLine)));
           //this.Expression.SetName(exprStmt.ToString().Replace(System.Environment.NewLine, ""));
        }

        public override void UpdateAnchorAttachAST()
        {
            var ifStmt = this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.ExpressionStatement;
            this.Expression.ExprOut.SetASTNodeReference((e) => { ifStmt.Expression = e; });
        }
    }
}
