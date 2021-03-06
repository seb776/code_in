﻿using code_in.Views.NodalView.NodesElems.Tiles;
using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Block
{
    public class IfStmtTile : BaseTile
    {
        public ExpressionItem Condition = null;
        public FlowTileItem ItemTrue = null;
        public FlowTileItem ItemFalse = null;
        public override bool IsExpanded
        {
            get
            {
                return Condition.IsExpanded;
            }
            set
            {
                Condition.IsExpanded = value;
                ItemTrue.IsExpanded = value;
                ItemFalse.IsExpanded = value;
            }
        }
        public IfStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("If");
            Condition = this.CreateAndAddItem<ExpressionItem>(true);
            ItemTrue = this.CreateAndAddItem<FlowTileItem>();
           // ItemTrue.SetName("true");
            ItemFalse = this.CreateAndAddItem<FlowTileItem>();
            ItemFalse.SetName("else");
            ItemFalse.SetThemeResources("false");
            this.BackGrid.Background = new SolidColorBrush(Color.FromArgb(51, 0x20, 0x77, 0xE3));
        }
        public IfStmtTile()  :
            this(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }
        public override void UpdateDisplayedInfosFromPresenter()
        {
            Debug.Assert(this.Presenter != null);
            var ifElse = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.IfElseStatement);
            Condition.SetName(ifElse.Condition.ToString());
            
            ItemFalse.SetName("else");
        }

        public override void UpdateAnchorAttachAST()
        {
            var ifStmt = this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.IfElseStatement;
            this.Condition.ExprOut.SetASTNodeReference((e) => { ifStmt.Condition = e; });
        }
    }
}
