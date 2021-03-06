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
    class UnSupStmtTile : BaseTile
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

        public UnSupStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Unsupported");
            Expression = this.CreateAndAddItem<ExpressionItem>(true);
        }
        public UnSupStmtTile() :
            base(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        #region INodeElem
        public override void UpdateDisplayedInfosFromPresenter()
        {
            Debug.Assert(Presenter != null);
            var unSup = Presenter.GetASTNode();
            Expression.SetName(unSup.ToString());
        }
        #endregion INodeElem

        public override void UpdateAnchorAttachAST()
        {
            // useless here ?
        }
    }
}
