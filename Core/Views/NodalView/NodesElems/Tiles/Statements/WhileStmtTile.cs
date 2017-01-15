﻿using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class WhileStmtTile : BaseTile
    {
        public ExpressionItem Condition = null;
        public FlowTileItem trueItem = null;

        public WhileStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            Condition = this.CreateAndAddItem<ExpressionItem>(true);
            trueItem = this.CreateAndAddItem<FlowTileItem>();
            this.SetName("While");
            this.BackGrid.Background = new SolidColorBrush(Color.FromArgb(51, 0x20, 0x77, 0xE3));
        }
        public WhileStmtTile() :
            base(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.WhileStatement;
            this.SetName("While");
            Condition.SetName(stmt.Condition.ToString());
        }
    }
}
