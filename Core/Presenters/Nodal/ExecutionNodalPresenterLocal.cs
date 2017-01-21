using code_in.Managers;
using code_in.Models;
using code_in.Models.NodalModel;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views;
using code_in.Views.NodalView;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Nodes.Expressions;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Block;
using code_in.Views.NodalView.NodesElems.Tiles;
using code_in.Views.NodalView.NodesElems.Tiles.Items;
using code_in.Views.NodalView.NodesElems.Tiles.Statements;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace code_in.Presenters.Nodal
{
    public class ExecutionNodalPresenterLocal : ANodalPresenterLocal
    {
        public ExecutionNodalModel ExecModel
        {
            get
            {
                return Model as ExecutionNodalModel;
            }
        }

        public ExecutionNodalView ExecNodalView
        {
            get
            {
                return View as ExecutionNodalView;
            }
        }
        public void changeResourceLink(object sender, ResourcesEventArgs e)
        {
            //(this.View as ExecutionNodalView).RootTileContainer.
            foreach (var node in Tools.Tools.FindVisualChildren<Code_inLink>((this.View as ExecutionNodalView).RootTileContainer as TileContainer))
            {
                node.changeLineMode();
            }
            //if (this.MainGrid != null)
            //    foreach (var t in this.MainGrid.Children)
            //    {
            //        if (t.GetType() == typeof(Code_inLink))
            //        {
            //            _link = (Code_inLink)t;
            //            _link.changeLineMode();
            //        }
            //    }
        }
        #region _generateFunctions
        /// <summary>
        /// This function displays the execution code from stmtArg to the NodalView attached.
        /// </summary>
        /// <param name="stmtArg"></param>
        protected void _generateVisualASTStatements(ITileContainer tileContainer, Statement stmtArg)
        {
            var nodePresenter = new NodePresenter(this, stmtArg);
            #region Block Statements
            if (stmtArg.GetType() == typeof(BlockStatement))
            {
                foreach (var stmt in (stmtArg as BlockStatement))
                    this._generateVisualASTStatements(tileContainer, stmt);
            }
            # region IfStmts
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.IfElseStatement))
            {
                var ifStmt = stmtArg as IfElseStatement; // AST node
                var ifTile = tileContainer.CreateAndAddTile<IfStmtTile>(nodePresenter); // Visual Node

                //ifTile.TileType.setName(ifStmt.Condition.ToString());
                ifTile.Condition.ExprOut.SetASTNodeReference((e) => { ifStmt.Condition = e; });
                this._generateVisualASTExpressions(ifTile.Condition, ifStmt.Condition, null, ifTile.Condition.ExprOut, (e) => { ifStmt.Condition = e; });

                this._generateVisualASTStatements(ifTile.ItemTrue, ifStmt.TrueStatement);
                this._generateVisualASTStatements(ifTile.ItemFalse, ifStmt.FalseStatement);
            }
            # endregion IfStmts
            # region tryCatchStmt
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.TryCatchStatement))
            {
                var tryStmt = stmtArg as TryCatchStatement;
                var tryTile = tileContainer.CreateAndAddTile<TryCatchStmtTile>(nodePresenter);

                this._generateVisualASTStatements(tryTile.ItemTry, tryStmt.TryBlock);
                foreach (var blockCatch in tryStmt.CatchClauses)
                {
                    var item = tryTile.CreateAndAddItem<FlowTileItem>();
                    tryTile.ItemsCatch.Add(item);
                    item.SetName("catch " + blockCatch.VariableName);
                    this._generateVisualASTStatements(item, blockCatch.Body);
                }
                if (tryStmt.FinallyBlock.ToString() != "") // TODO: eww need to find a way to detect if there's a finally block or not in the code
                {
                    var item = tryTile.CreateAndAddItem<FlowTileItem>();
                    item.SetName("finally");
                    this._generateVisualASTStatements(item, tryStmt.FinallyBlock);
                }
            }
            # endregion tryCatchStmt
            # region Loops
            else if (stmtArg.GetType() == typeof(DoWhileStatement))
            {
                var doWhileStmt = stmtArg as DoWhileStatement; // AST Node
                var doWhileStmtTile = tileContainer.CreateAndAddTile<DoWhileStmtTile>(nodePresenter); // Visual Node

                doWhileStmtTile.Condition.ExprOut.SetASTNodeReference((e) => { doWhileStmt.Condition = e; });
                this._generateVisualASTExpressions(doWhileStmtTile.Condition, doWhileStmt.Condition, null, doWhileStmtTile.Condition.ExprOut, (e) => { doWhileStmt.Condition = e; });
                this._generateVisualASTStatements(doWhileStmtTile.trueItem, doWhileStmt.EmbeddedStatement);
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.WhileStatement))
            {
                var whileStmt = stmtArg as WhileStatement; // AST Node
                var whileStmtTile = tileContainer.CreateAndAddTile<WhileStmtTile>(nodePresenter); // Visual Node

                whileStmtTile.Condition.ExprOut.SetASTNodeReference((e) => { whileStmt.Condition = e; });
                this._generateVisualASTExpressions(whileStmtTile.Condition, whileStmt.Condition, null, whileStmtTile.Condition.ExprOut, (e) => { whileStmt.Condition = e; });
                this._generateVisualASTStatements(whileStmtTile.trueItem, whileStmt.EmbeddedStatement);
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForStatement))
            {
                var forStmt = stmtArg as ForStatement; // AST Node
                var forStmtTile = tileContainer.CreateAndAddTile<ForStmtTile>(nodePresenter); // Visual Node

                forStmtTile.Condition.ExprOut.SetASTNodeReference((e) => { forStmt.Condition = e; });
                this._generateVisualASTExpressions(forStmtTile.Condition, forStmt.Condition, null, forStmtTile.Condition.ExprOut, (e) => { forStmt.Condition = e; });
                this._generateVisualASTStatements(forStmtTile.trueItem, forStmt.EmbeddedStatement);
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForeachStatement))
            {
                var forEachStmt = stmtArg as ForeachStatement;
                var forEachStmtTile = tileContainer.CreateAndAddTile<ForEachStmtTile>(nodePresenter);

                forEachStmtTile.Condition.ExprOut.SetASTNodeReference((e) => { forEachStmt.InExpression = e; });
                this._generateVisualASTExpressions(forEachStmtTile.Condition, forEachStmt.InExpression, null, forEachStmtTile.Condition.ExprOut, (e) => { forEachStmt.InExpression = e; });
                this._generateVisualASTStatements(forEachStmtTile.trueItem, forEachStmt.EmbeddedStatement);
            }
            # endregion Loops
            #region Switch
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.SwitchStatement))
            {
                var switchStmt = stmtArg as SwitchStatement;
                var switchStmtTile = tileContainer.CreateAndAddTile<SwitchStmtTile>(nodePresenter);
                switchStmtTile.Expression.ExprOut.SetASTNodeReference((e) => { switchStmt.Expression = e; });
                this._generateVisualASTExpressions(switchStmtTile.Expression, switchStmt.Expression, null, switchStmtTile.Expression.ExprOut, (e) => { switchStmt.Expression = e; });
                foreach (var caseBlock in switchStmt.SwitchSections)
                {
                    foreach (var caseLabel in caseBlock.CaseLabels)
                    {
                        var itemExpr = switchStmtTile.CreateAndAddItem<ExpressionItem>();
                        switchStmtTile.ExpressionCases.Add(itemExpr);
                        itemExpr.ExprOut.SetASTNodeReference((e) => { caseLabel.ReplaceWith(e); });
                        this._generateVisualASTExpressions(itemExpr, caseLabel.Expression, null, itemExpr.ExprOut, (e) => { caseLabel.Expression = e; });
                    }
                    foreach (var caseBlockStmt in caseBlock.Statements)
                    {
                        var itemCase = switchStmtTile.CreateAndAddItem<FlowTileItem>();
                        switchStmtTile.itemCases.Add(itemCase);
                        this._generateVisualASTStatements(itemCase, caseBlockStmt);
                    }
                }
            }
            #endregion Switch
            #endregion Block Statements
            #region Single Statement
            #region Variable Declaration
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.VariableDeclarationStatement))
            {
                var varDeclStmt = stmtArg as VariableDeclarationStatement;
                var varDeclStmtTile = tileContainer.CreateAndAddTile<VarStmtTile>(nodePresenter);
                bool first = true;
                foreach (var v in varDeclStmt.Variables)
                {
                    var exprItem = varDeclStmtTile.CreateAndAddItem<ExpressionItem>(first);
                    first = false;
                    exprItem.SetName(v.ToString());
                    exprItem.ExprOut.SetASTNodeReference((e) => { v.ReplaceWith(e); });
                    _generateVisualASTExpressions(exprItem, v.Initializer, null, exprItem.ExprOut, (e) => { v.Initializer = e; });
                }
            }
            #endregion Variable Declaration
            #region ExpressionStatement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ExpressionStatement))
            {
                var exprStmt = stmtArg as ExpressionStatement;
                var exprStmtTile = tileContainer.CreateAndAddTile<ExprStmtTile>(nodePresenter);
                //exprStmtTile.Expression.SetName(exprStmt.ToString().Replace(System.Environment.NewLine, "")); // TODO @Seb Make this be done automatically by creating CreateAnddAddTile (see presenters...)
                exprStmtTile.Expression.ExprOut.SetASTNodeReference((e) => { exprStmt.Expression = e; });
                this._generateVisualASTExpressions(exprStmtTile.Expression, exprStmt.Expression, null, exprStmtTile.Expression.ExprOut, (e) => { exprStmt.Expression = e; });
            }
            #endregion ExpressionStatement
            #region Return Statement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ReturnStatement))
            {
                var returnStmt = stmtArg as ReturnStatement;
                var returnStmtTile = tileContainer.CreateAndAddTile<ReturnStmtTile>(nodePresenter);
                returnStmtTile.Expression.ExprOut.SetASTNodeReference((e) => { returnStmt.Expression = e; });
                this._generateVisualASTExpressions(returnStmtTile.Expression, returnStmt.Expression, null, returnStmtTile.Expression.ExprOut, (e) => { returnStmt.Expression = e; });
            }
            #endregion Return Statement
            #region Break Statement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.BreakStatement))
            {
                var breakStmt = stmtArg as BreakStatement; // ASTNode
                var breakStmtTile = tileContainer.CreateAndAddTile<BreakStmtTile>(nodePresenter); // Visual Node
            }
            #endregion Break Statement
            #region Label Statement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.LabelStatement))
            {
                var labelStmt = stmtArg as LabelStatement; // ASTNode
                var labelStmtTile = tileContainer.CreateAndAddTile<LabelStmtTile>(nodePresenter); // Visual Node
                //this._generateVisualASTExpressions(labelStmtTile.Expression, labelStmt.Expression, labelStmtTile.Expression.ExprOut, (e) => { labelStmt.Expression = e; });
            }
            #endregion Label Statement
            #region YieldReturn Statement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.YieldReturnStatement))
            {
                var yieldReturnStmt = stmtArg as YieldReturnStatement; // AST Node
                var yieldReturnStmtTile = tileContainer.CreateAndAddTile<YieldReturnStmtTile>(nodePresenter); // Visual Node
                yieldReturnStmtTile.Expression.ExprOut.SetASTNodeReference((e) => { yieldReturnStmt.Expression = e; });
                this._generateVisualASTExpressions(yieldReturnStmtTile.Expression, yieldReturnStmt.Expression, null, yieldReturnStmtTile.Expression.ExprOut, (e) => { yieldReturnStmt.Expression = e; });
            }
            #endregion YieldReturn Statement
            #region YieldBreak Statement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.YieldBreakStatement))
                tileContainer.CreateAndAddTile<YieldBreakStmtTile>(nodePresenter); // Visual Node
            #endregion YieldBreak Statement
            #region Continue Statement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ContinueStatement))
            {
                tileContainer.CreateAndAddTile<ContinueStmtTile>(nodePresenter);
            }
            #endregion ContinueStatement
            #region Throw
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ThrowStatement))
            {
                var throwStmt = stmtArg as ThrowStatement; // AST Node
                var throwStmtTile = tileContainer.CreateAndAddTile<ThrowStmtTile>(nodePresenter); // Visual Node
                throwStmtTile.Expression.ExprOut.SetASTNodeReference((e) => { throwStmt.Expression = e; });
                this._generateVisualASTExpressions(throwStmtTile.Expression, throwStmt.Expression, null, throwStmtTile.Expression.ExprOut, (e) => { throwStmt.Expression = e; });
            }
            #endregion Throw
            #region checked
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.CheckedStatement))
            {
                var checkedStmt = stmtArg as CheckedStatement;
                var checkedStmtTile = tileContainer.CreateAndAddTile<CheckedStmtTile>(nodePresenter);
                this._generateVisualASTStatements(checkedStmtTile.itemsChecked, checkedStmt.Body);
            }
            #endregion checked
            #region unchecked
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.UncheckedStatement))
            {
                var uncheckedStmt = stmtArg as UncheckedStatement;
                var uncheckedStmtTile = tileContainer.CreateAndAddTile<UncheckedStmtTile>(nodePresenter);
                this._generateVisualASTStatements(uncheckedStmtTile.itemsUnchecked, uncheckedStmt.Body);
            }
            #endregion unchecked
            #region fixed
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.FixedStatement))
            {
                var fixedStmt = stmtArg as FixedStatement;
                var fixedStmtTile = tileContainer.CreateAndAddTile<fixedStmtTile>(nodePresenter);

                this._generateVisualASTStatements(fixedStmtTile.itemsFixed, fixedStmt.EmbeddedStatement);
            }
            #endregion fixed
            #region Goto
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.GotoStatement))
            {
                var gotoStmt = stmtArg as GotoStatement; // AST Node
                var gotoStmtTile = tileContainer.CreateAndAddTile<GotoStmtTile>(nodePresenter); // Visual Node
                //      this._generateVisualASTExpressions(gotoStmtTile.label, gotoStmt.Label, gotoStmtTile.label.ExprOut, (e) => { gotoStmt.Label = e; });
            }
            #endregion Throw
            #endregion Single Statement
            else // Default Node
                tileContainer.CreateAndAddTile<UnSupStmtTile>(nodePresenter);
            tileContainer.UpdateDisplayedInfosFromPresenter();
        }
        protected void _generateVisualASTExpressions(IVisualNodeContainer container, ICSharpCode.NRefactory.CSharp.Expression expr, AValueNode previousNode, DataFlowAnchor inAnchor, Action<ICSharpCode.NRefactory.CSharp.Expression> methodAttachIOToASTField)
        {
            if (expr.IsNull)
                return;
            AValueNode visualNode = null;
            var nodePresenter = new NodePresenter(this, expr);
            if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ArrayInitializerExpression))
            {
                var arrayInitExpr = expr as ArrayInitializerExpression;
                var arrayInitNode = container.CreateAndAddNode<ArrayInitExprNode>(nodePresenter);
                visualNode = arrayInitNode;
                int idx = 0;
                foreach (var elem in arrayInitExpr.Elements)
                {
                    var dataIn = arrayInitNode.CreateAndAddInput<DataFlowAnchor>();
                    dataIn.IsRemovable = true;
                    this._generateVisualASTExpressions(container, elem, visualNode, dataIn, (e) => { }); // TODO @z0rg callback to link expression
                    idx++;
                }
            }
            #region Indexer
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.IndexerExpression))
            {
                var invokExpr = expr as ICSharpCode.NRefactory.CSharp.IndexerExpression;
                var invokExprNode = container.CreateAndAddNode<IndexerExprNode>(nodePresenter);
                visualNode = invokExprNode;

                this._generateVisualASTExpressions(container, invokExpr.Target, visualNode, invokExprNode.Target, (e) => { invokExpr.Target = e; });
                // TODO @Seb @Mo display for generic parameters in FuncCallExprNode
                int i = 0;
                foreach (var param in invokExpr.Arguments)
                {
                    var paramMeth = invokExprNode.CreateAndAddInput<DataFlowAnchor>();
                    //paramMeth.SetName("param" + i);
                    //this._generateVisualASTExpressions(container, param, paramMeth, (e) =>
                    //{
                    //    if (e == param)
                    //        return;
                    //    if (e != null)
                    //        invokExpr.Arguments.InsertAfter(param, e);
                    //    invokExpr.Arguments.Remove(param);
                    //});
                    //i++;
                }
            }
            #endregion Indexer
            #region UnaryOperator
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression))
            {
                var unaryExprOp = expr as ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression;
                var unaryExprNode = container.CreateAndAddNode<UnaryExprNode>(nodePresenter);
                visualNode = unaryExprNode;
                unaryExprNode.SetName(unaryExprOp.OperatorToken.ToString());
                this._generateVisualASTExpressions(container, unaryExprOp.Expression, visualNode, unaryExprNode.OperandA, (e) => { unaryExprOp.Expression = e; });
            }
            #endregion UnaryOperator
            #region Parenthesis Expr
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ParenthesizedExpression))
            {
                var parenthesizedExpr = expr as ICSharpCode.NRefactory.CSharp.ParenthesizedExpression;
                var parenthesizedExprNode = container.CreateAndAddNode<ParenthesizedExprNode>(nodePresenter);
                visualNode = parenthesizedExprNode;
                parenthesizedExprNode.SetName(parenthesizedExpr.ToString());
                this._generateVisualASTExpressions(container, parenthesizedExpr.Expression, visualNode, parenthesizedExprNode.OperandA, (e) => { parenthesizedExpr.Expression = e; });
            }
            #endregion Parenthesis Expr
            #region ArrayCreation
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ArrayCreateExpression))
            {
                var arrCreateExpr = expr as ICSharpCode.NRefactory.CSharp.ArrayCreateExpression;
                var arrCreateExprNode = container.CreateAndAddNode<ArrayCreateExprNode>(nodePresenter);
                visualNode = arrCreateExprNode;
                foreach (var i in arrCreateExpr.Arguments)
                {
                    var arrSize = arrCreateExprNode.CreateAndAddInput<DataFlowAnchor>();
                    arrSize.SetName(i.ToString());
                    _generateVisualASTExpressions(container, i, visualNode, arrSize, (e) => { }); // TODO @z0rg callback
                }
                _generateVisualASTExpressions(container, arrCreateExpr.Initializer, visualNode, arrCreateExprNode.ExprIn, (e) => { }); // TODO @z0rg callback
            }
            #endregion ArrayCreation
            #region ObjectCreation
            else if (false)//expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ObjectCreateExpression))
            {
                if (false)
                {
                    var objCreateExpr = expr as ICSharpCode.NRefactory.CSharp.ObjectCreateExpression;
                    var objCreateExprNode = container.CreateAndAddNode<FuncCallExprNode>(nodePresenter); // TODO Create a node for that
                    visualNode = objCreateExprNode;
                    objCreateExprNode.SetType("ObjCreateExpr");
                    var newType = objCreateExprNode.CreateAndAddInput<DataFlowAnchor>(); // TODO @Seb text input ?
                    newType.SetName(objCreateExpr.Type.ToString());
                    int i = 0;
                    foreach (var param in objCreateExpr.Arguments)
                    {
                        var arg = objCreateExprNode.CreateAndAddInput<DataFlowAnchor>();
                        arg.SetName("param" + i);
                        i++;
                        this._generateVisualASTExpressions(container, param, visualNode, arg, (e) =>
                        {
                            if (e == param)
                                return;
                            if (e != null)
                                objCreateExpr.Arguments.InsertAfter(param, e);
                            objCreateExpr.Arguments.Remove(param);
                        });
                    }
                }
            }
            #endregion ObjectCreation
            #region Identifier
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.IdentifierExpression))
            {
                var identExpr = expr as ICSharpCode.NRefactory.CSharp.IdentifierExpression;
                var identExprNode = container.CreateAndAddNode<IdentifierExprNode>(nodePresenter);
                visualNode = identExprNode;
                identExprNode.ExprOut.SetName(identExpr.Identifier);
            }
            #endregion Identifier
            #region Assignement
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.AssignmentExpression))
            {
                var assignExpr = expr as ICSharpCode.NRefactory.CSharp.AssignmentExpression;
                var assignExprNode = container.CreateAndAddNode<AssignExprNode>(nodePresenter);
                visualNode = assignExprNode;

                this._generateVisualASTExpressions(container, assignExpr.Left, visualNode, assignExprNode.OperandA, (e) => { assignExpr.Left = e; });
                this._generateVisualASTExpressions(container, assignExpr.Right, visualNode, assignExprNode.OperandB, (e) => { assignExpr.Right = e; });
            }
            #endregion Assignement
            #region BinaryOperator
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression))
            {
                var binaryExpr = expr as ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression;
                var binaryExprNode = container.CreateAndAddNode<BinaryExprNode>(nodePresenter);
                visualNode = binaryExprNode;
                binaryExprNode.SetName(binaryExpr.Operator.ToString());

                this._generateVisualASTExpressions(container, binaryExpr.Left, visualNode, binaryExprNode.OperandA, (e) => { binaryExpr.Left = e; });
                this._generateVisualASTExpressions(container, binaryExpr.Right, visualNode, binaryExprNode.OperandB, (e) => { binaryExpr.Right = e; });

            }
            #endregion BinaryOperator
            #region AsExpression
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.AsExpression))
            {
                var asExpr = expr as ICSharpCode.NRefactory.CSharp.AsExpression;
                var asExprNode = container.CreateAndAddNode<AsExprNode>(nodePresenter);
                visualNode = asExprNode;
            }
            #endregion AsExpression
            #region IsExpression
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.IsExpression))
            {
                var isExpr = expr as ICSharpCode.NRefactory.CSharp.IsExpression;
                var isExprNode = container.CreateAndAddNode<IsExprNode>(nodePresenter);
                visualNode = isExprNode;
            }
            #endregion IsExpression
            #region BaseReferenceExpression
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.BaseReferenceExpression))
            {
                var isExpr = expr as ICSharpCode.NRefactory.CSharp.BaseReferenceExpression;
                var isExprNode = container.CreateAndAddNode<BaseReferenceExprNode>(nodePresenter);
                visualNode = isExprNode;
            }
            #endregion BaseReferenceExpression
            #region TypeReferenceExpression
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.TypeReferenceExpression))
            {
                var isExpr = expr as ICSharpCode.NRefactory.CSharp.TypeReferenceExpression;
                var isExprNode = container.CreateAndAddNode<TypeReferenceExprNode>(nodePresenter);
                visualNode = isExprNode;
            }
            #endregion TypeReferenceExpression
            #region NullExpression
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NullReferenceExpression))
            {
                var nullExpr = expr as ICSharpCode.NRefactory.CSharp.NullReferenceExpression;
                var nullExprNode = container.CreateAndAddNode<NullRefExprNode>(nodePresenter);
                visualNode = nullExprNode;
            }
            #endregion NullExpression
            #region MemberReference
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MemberReferenceExpression))
            {
                var memberRefExpr = expr as ICSharpCode.NRefactory.CSharp.MemberReferenceExpression;
                var memberRefExprNode = container.CreateAndAddNode<MemberRefExprNode>(nodePresenter); // TODO Create a node for that
                visualNode = memberRefExprNode;
                var inputTarget = memberRefExprNode.CreateAndAddInput<DataFlowAnchor>();
                this._generateVisualASTExpressions(container, memberRefExpr.Target, visualNode, null, null);
            }
            #endregion MemberReference
            #region Primitive




            // Nodal View
            //nodePresenter = nodalPresenter.InstantiateNodePresenter(ECSharpNode.PrimaryExprNode)
            //container.CreateAndAddNode<ECSharpNode.PrimaryExprNode>(nodePresenter);
            // Presenter
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.PrimitiveExpression))
            {
                var primExpr = expr as ICSharpCode.NRefactory.CSharp.PrimitiveExpression;
                var primExprNode = container.CreateAndAddNode<PrimaryExprNode>(nodePresenter);
                visualNode = primExprNode;
            }
            #endregion Primitive
            #region Invocative
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.InvocationExpression))
            {
                var invokExpr = expr as ICSharpCode.NRefactory.CSharp.InvocationExpression;
                var invokExprNode = container.CreateAndAddNode<FuncCallExprNode>(nodePresenter);
                visualNode = invokExprNode;

                this._generateVisualASTExpressions(container, invokExpr.Target, visualNode, invokExprNode.TargetIn, (e) => { invokExpr.Target = e; });
                // TODO @Seb @Mo display for generic parameters in FuncCallExprNode
                int i = 0;
                foreach (var param in invokExpr.Arguments)
                {
                    var paramMeth = invokExprNode.CreateAndAddInput<DataFlowAnchor>();
                    paramMeth.IsRemovable = true;
                    this._generateVisualASTExpressions(container, param, visualNode, paramMeth, (e) =>
                    {
                        if (e == param)
                            return;
                        if (e != null)
                            invokExpr.Arguments.InsertAfter(param, e);
                        invokExpr.Arguments.Remove(param);
                    });
                    i++;
                }
            }
            #endregion Invocative
            #region SizeOf
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.SizeOfExpression))
            {
                var sizeofExpr = expr as ICSharpCode.NRefactory.CSharp.SizeOfExpression;
                var sizeofExprNode = container.CreateAndAddNode<SizeOfExprNode>(nodePresenter);
                visualNode = sizeofExprNode;
                //this._generateVisualASTExpressions(container, sizeofExpr, sizeofExprNode.Input, (e) => { sizeofExpr.Type = e; });
            }
            #endregion Sizeof
            #region TypeOf
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.TypeOfExpression))
            {
                var typeofExpr = expr as ICSharpCode.NRefactory.CSharp.TypeOfExpression;
                var typeofExprNode = container.CreateAndAddNode<TypeOfExprNode>(nodePresenter);
                visualNode = typeofExprNode;
                //this._generateVisualASTExpressions(container, typeofExpr, typeofExprNode.Input, ((e) => {typeofExpr.Type}));
            }
            #endregion TypeOf
            else
            {
                var defaultUnsupportedNode = container.CreateAndAddNode<UnSupExpNode>(nodePresenter);
                visualNode = defaultUnsupportedNode;
                defaultUnsupportedNode.NodeText.Text = expr.ToString();
            }
            if (visualNode != null)
                visualNode.UpdateDisplayedInfosFromPresenter();
            if (visualNode != null && inAnchor != null)
            {
                if (previousNode != null)
                    previousNode.UpdateAnchorAttachAST();
                //inAnchor.SetASTNodeReference(methodAttachIOToASTField);

                _createVisualLink(container as dynamic, inAnchor, visualNode.ExprOut); // TODO cast Beuark
            }
        }
        protected void _generateVisualASTFunctionBody(ICSharpCode.NRefactory.CSharp.MethodDeclaration method)
        {
            (this.ExecNodalView.RootTileContainer as System.Windows.Controls.UserControl).Margin = new System.Windows.Thickness(100, 100, 0, 0);
            this._generateVisualASTStatements(this.ExecNodalView.RootTileContainer, method.Body);
        }

        protected void _generateVisualASTConstructorBody(ICSharpCode.NRefactory.CSharp.ConstructorDeclaration constructor)
        {
            (this.ExecNodalView.RootTileContainer as System.Windows.Controls.UserControl).Margin = new System.Windows.Thickness(100, 100, 0, 0);
            _generateVisualASTStatements(this.ExecNodalView.RootTileContainer, constructor.Body);
        }
        private void _generateVisualASTDestructorBody(ICSharpCode.NRefactory.CSharp.DestructorDeclaration destructor)
        {
            (this.ExecNodalView.RootTileContainer as System.Windows.Controls.UserControl).Margin = new System.Windows.Thickness(100, 100, 0, 0);
            _generateVisualASTStatements(this.ExecNodalView.RootTileContainer, destructor.Body);
        }
        protected void _generateVisualASTPropertyBody(ICSharpCode.NRefactory.CSharp.Accessor access)
        {
            (this.ExecNodalView.RootTileContainer as System.Windows.Controls.UserControl).Margin = new System.Windows.Thickness(100, 100, 0, 0);
            _generateVisualASTStatements(this.ExecNodalView.RootTileContainer, access.Body);

        }
        #endregion _generateFunctions


        private void _createVisualLink(ILinkContainer parent, AIOAnchor a, AIOAnchor b)
        {
            if (a == null || b == null)
                return;
            parent.DragLink(a, true);
            parent.DropLink(b, true);
        }

        #region this
        public ExecutionNodalPresenterLocal(DeclarationsNodalPresenterLocal assocFile, INodePresenter nodePresenter) :
            base()
        {
            Model = new ExecutionNodalModel(assocFile.DeclModel, nodePresenter.GetASTNode());
            ExecModel.Presenter = this;
        }

        public void EditFunction(FuncDeclItem node)
        {
            this._generateVisualASTFunctionBody(node.MethodNode);
        }
        public void EditAccessor(Accessor node)
        {
            _generateVisualASTPropertyBody(node);
        }
        public void EditConstructor(ConstructorItem node)
        {
            this._generateVisualASTConstructorBody(node.ConstructorNode);
        }
        public void EditDestructor(DestructorItem node)
        {
            this._generateVisualASTDestructorBody(node.DestructorNode);
        }


        #endregion this
        #region IContextMenu
        public override List<Type> GetAvailableNodes()
        {
            List<Type> availableNodes = new List<Type>();

            availableNodes = Tools.Tools.GetAllSubclassesOf(typeof(BaseTile));
            return availableNodes;
        }
        #endregion IContextMenu
        #region INodalPresenter
        public override void User_AddNode_Callback(Type nodeTypeToAdd, Point mousePos)
        {
            if (nodeTypeToAdd != null)
            {
                Dictionary<Type, code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode> types = new Dictionary<Type, code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode>();
                MethodInfo mi = _viewStatic.GetType().GetMethod("CreateAndAddTile");
                MethodInfo gmi = mi.MakeGenericMethod(nodeTypeToAdd);

                types.Add(typeof(BreakStmtTile), code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode.BREAK_STMT);
                types.Add(typeof(ExprStmtTile), code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode.EXPRESSION_STMT);
                types.Add(typeof(YieldBreakStatement), code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode.YIELD_BREAK_STMT);
                types.Add(typeof(YieldReturnStatement), code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode.YIELD_RETURN_STMT);
                types.Add(typeof(ReturnStatement), code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode.RETURN_STMT);

                code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode resultType = NodePresenter.ECSharpNode.TEXT_NODE;
                if (types.ContainsKey(nodeTypeToAdd))
                    resultType = types[nodeTypeToAdd];
                var astNode = NodePresenter.InstantiateASTNode(resultType);
                var nodePresenter = new NodePresenter(_viewStatic.Presenter, astNode);
                var array = new object[1];
                array[0] = nodePresenter;
                BaseTile node = gmi.Invoke(_viewStatic, array) as BaseTile;
                if (astNode == null)
                    throw new NotImplementedException(nodeTypeToAdd.ToString());

                if (ExecModel.Root is MethodDeclaration)
                {
                    astNode.Remove();
                    (ExecModel.Root as MethodDeclaration).Body.Add(astNode as Statement);
                }
                node.UpdateAnchorAttachAST();
                //_viewStatic = null;
            }
        }
        public override bool IsSaved
        {
            get { return ExecModel.IsSaved; }
        }
        public override void Save()
        {
            this.ExecModel.Save();
        }
        public override String DocumentName
        {
            get
            {
                return ""; // TODO not sure if still useful
            }
        }
        #endregion INodalPresenter
    }
}
