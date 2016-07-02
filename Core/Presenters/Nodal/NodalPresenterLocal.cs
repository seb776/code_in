using code_in.Models;
using code_in.Models.NodalModel;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView;
using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Assets;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;
using code_in.Views.NodalView.NodesElems.Nodes.Expressions;
using code_in.Views.NodalView.NodesElems.Nodes.Statements;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Block;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Context;
using code_in.Views.Utils;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Presenters.Nodal
{
    /// <summary>
    /// This NodalPresenter is used when you are wroking on a local file, without using network.
    /// </summary>
    public class NodalPresenterLocal : INodalPresenter
    {
        private INodalView _view = null; // TODO INodalView
        private NodalModel _model = null;
        private CSharpParser _parser = null;
        //private AOItem outputFlownode = null;
        //private AOItem inputFlownode = null;
        //private AOItem nextOutputFlowNode = null;

        public NodalPresenterLocal(INodalView view)
        {
            System.Diagnostics.Debug.Assert(view != null);
            _view = view;
            _parser = new CSharpParser();
        }

        public void OpenFile(String path)
        {
            _model = this.ParseFile(path);
            this._generateVisualASTDeclaration(_model);
        }
        public void EditFunction(FuncDeclItem node)
        {
            this._generateVisualASTFunctionBody(node.MethodNode);
        }

        private void _generateVisualASTDeclaration(NodalModel model)
        {
            //UsingDeclNode usingNode;
            //usingNode = this._view.CreateAndAddNode<UsingDeclNode>(); // TODO @Seb @Mo make it work again

            this._generateVisualASTDeclarationRecur(model.AST, this._view /*,usingNode*/);
        }

        private void setOtherModifiers(IContainingModifiers view, Modifiers tmpModifiers)
        {
            view.setModifiersList(tmpModifiers);
        }

        private void setAccessModifiers(IContainingAccessModifiers view, Modifiers tmpModifiers)
        {
            view.setAccessModifiers(tmpModifiers);
        }

        private void InitInheritance(IContainingInheritance view, TypeDeclaration typeDecl)
        {
            view.ManageInheritance(typeDecl);
        }

        private void SetAllGenerics(IContainingGenerics view, TypeDeclaration typeDecl)
        {
            Tuple<string, EGenericVariance> tuple;
            List<Tuple<string, EGenericVariance>> GenericList = new List<Tuple<string,EGenericVariance>>();

            foreach (var tmp in typeDecl.TypeParameters)
            {
                if (tmp.Variance == ICSharpCode.NRefactory.TypeSystem.VarianceModifier.Contravariant)
                    tuple = new Tuple<string, EGenericVariance>(tmp.Name.ToString(), EGenericVariance.IN);
                else if (tmp.Variance == ICSharpCode.NRefactory.TypeSystem.VarianceModifier.Covariant)
                    tuple = new Tuple<string, EGenericVariance>(tmp.Name.ToString(), EGenericVariance.OUT);
                else
                    tuple = new Tuple<string, EGenericVariance>(tmp.Name.ToString(), EGenericVariance.NOTHING);
                GenericList.Add(tuple);
            }
            view.setGenerics(GenericList);
        }
        private void _generateVisualASTDeclarationRecur(AstNode node, IVisualNodeContainer parentContainer)
        {
            var nodePresenter = new NodePresenter(this, node);
            bool goDeeper = true;
            IVisualNodeContainer parentNode = null;
            if (node.Children == null)
                return;
            #region Namespace
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration))
            {
                NamespaceNode namespaceNode = parentContainer.CreateAndAddNode<NamespaceNode>(nodePresenter);
                parentNode = namespaceNode;
                var tmpNode = (ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)node;
                namespaceNode.SetName(tmpNode.Name);
            }
            #endregion
            #region Classes (interface, struct, class, enum)
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.TypeDeclaration)) // Handles class, struct, enum (see further)
            {
                var tmpNode = (ICSharpCode.NRefactory.CSharp.TypeDeclaration)node;
                #region Enum
                if (tmpNode.ClassType == ICSharpCode.NRefactory.CSharp.ClassType.Enum)
                {
                    ClassDeclNode enumDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>(nodePresenter);
                    enumDeclNode.SetClassType((code_in.Views.NodalView.NodesElems.Nodes.ClassDeclNode.EType)3);
                    enumDeclNode.SetName(tmpNode.Name);

                    foreach (var v in tmpNode.Members)
                    {
                        var tmp = v as EnumMemberDeclaration;
                        var item = enumDeclNode.CreateAndAddNode<DataFlowItem>(nodePresenter);
                        if (tmp.Initializer.IsNull == false)
                            item.SetName(v.Name + " = " + tmp.Initializer.ToString());
                        else
                            item.SetName(v.Name);
                    }
                    setOtherModifiers(enumDeclNode, tmpNode.Modifiers);
                }
                #endregion Enum
                #region Class
                else if (tmpNode.ClassType == ICSharpCode.NRefactory.CSharp.ClassType.Class)
                {
                    ClassDeclNode classDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>(nodePresenter);
                    parentNode = classDeclNode;
                    classDeclNode.SetName(tmpNode.Name);
                    setAccessModifiers(classDeclNode, tmpNode.Modifiers);
                    setOtherModifiers(classDeclNode, tmpNode.Modifiers);

                    //inheritance
                    InitInheritance(classDeclNode, tmpNode);
                    //Generic
                    SetAllGenerics(classDeclNode, tmpNode);

                    //goDeeper = false;
                    foreach (var n in node.Children)
                    {
                        if (n.GetType() == typeof(ICSharpCode.NRefactory.CSharp.FieldDeclaration))
                        {
                            ICSharpCode.NRefactory.CSharp.FieldDeclaration field = n as ICSharpCode.NRefactory.CSharp.FieldDeclaration;

                            var item = classDeclNode.CreateAndAddNode<ClassItem>(nodePresenter);
                            item.SetName(field.Variables.FirstOrNullObject().Name);
                            //item.SetType(field.ReturnType.ToString());
                            setAccessModifiers(item, field.Modifiers); // here just call setAccessModifiers from the interface
                            setOtherModifiers(item, field.Modifiers);
                        }
                    }

                }
                #endregion Class
                #region Interface
                else if (tmpNode.ClassType == ICSharpCode.NRefactory.CSharp.ClassType.Interface)
                {
                    ClassDeclNode interfaceDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>(nodePresenter);
                    parentNode = interfaceDeclNode;
                    interfaceDeclNode.SetClassType((code_in.Views.NodalView.NodesElems.Nodes.ClassDeclNode.EType)2);
                    interfaceDeclNode.SetName(tmpNode.Name);
                    setAccessModifiers(interfaceDeclNode, tmpNode.Modifiers);

                    //inheritance
                    InitInheritance(interfaceDeclNode, tmpNode);
                    //Generic
                    SetAllGenerics(interfaceDeclNode, tmpNode);

                    //goDeeper = false;
                    foreach (var n in tmpNode.Members)
                    {
                        if (n.GetType() == typeof(ICSharpCode.NRefactory.CSharp.PropertyDeclaration))
                        {
                            ICSharpCode.NRefactory.CSharp.PropertyDeclaration properties = n as ICSharpCode.NRefactory.CSharp.PropertyDeclaration;

                            var item = interfaceDeclNode.CreateAndAddNode<ClassItem>(nodePresenter);
                            item.SetName(properties.Name);
                            
                        }
                    }
                }
                #endregion Interface
            }
            #endregion Classes (interface, struct, class, enum)
            #region Method
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MethodDeclaration))
            {
                FuncDeclItem funcDecl = parentContainer.CreateAndAddNode<FuncDeclItem>(nodePresenter);
                funcDecl.MethodNode = node as ICSharpCode.NRefactory.CSharp.MethodDeclaration;
                ICSharpCode.NRefactory.CSharp.MethodDeclaration method = node as ICSharpCode.NRefactory.CSharp.MethodDeclaration;


                var parameters = method.Parameters.ToList();
                for (int i = 0; i < parameters.Count; ++i)
                {
                    funcDecl.AddParam(parameters[i].Type.ToString());
                    //var item = funcDecl.CreateAndAddNode<ClassItem>(); // TODO ArgItem
                    //item.SetName(parameters[i].Name);
                    //item.SetItemType(parameters[i].Type.ToString());
                }
                funcDecl.SetName(method.Name);
                goDeeper = false;
                setOtherModifiers(funcDecl, method.Modifiers);
                setAccessModifiers(funcDecl, method.Modifiers);

            }
            #endregion
            #region Using
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.UsingDeclaration))
            {
                // TODO @Seb @Mo make it work and do better
                //UsingDeclItem UsingItem = UsingNode.CreateAndAddNode<UsingDeclItem>(nodePresenter);
                //UsingDeclaration UsingAstNode = node as ICSharpCode.NRefactory.CSharp.UsingDeclaration;

                //UsingItem.SetName(UsingAstNode.Namespace);
                //goDeeper = false;
            }

            #endregion
            if (goDeeper)
                foreach (var n in node.Children) if (n.GetType() != typeof(ICSharpCode.NRefactory.CSharp.FieldDeclaration))
                        this._generateVisualASTDeclarationRecur(n, (parentNode != null ? parentNode : parentContainer));
        }

        private void _generateVisualASTFunctionBody(MethodDeclaration method)
        {
            var nodePresenter = new NodePresenter(this, NodePresenter.EVirtualNodeType.FUNC_ENTRY);
            var entry = this._view.CreateAndAddNode<FuncEntryNode>(nodePresenter);

            foreach (var i in method.Parameters)
            {
                var data = entry.CreateAndAddOutput<DataFlowItem>();
                data.SetName(i.Name);
                data.SetItemType(i.Type.ToString());
            }
            this._generateVisualASTStatements(method.Body, entry.FlowOutAnchor);
        }

        /// <summary>
        /// This function displays the execution code from stmtArg to the NodalView attached.
        /// </summary>
        /// <param name="stmtArg"></param>
        private FlowNodeItem _generateVisualASTStatements(Statement stmtArg, FlowNodeItem lastOutput)
        {
            System.Diagnostics.Debug.Assert(lastOutput != null);
            AStatementNode visualNode = null;
            FlowNodeItem defaultFlowOut = null;
            var nodePresenter = new NodePresenter(this, stmtArg);
            #region Block Statement
            if (stmtArg.GetType() == typeof(BlockStatement))
            {
                FlowNodeItem defaultFlowOutTmp = lastOutput;
                foreach (var stmt in (stmtArg as BlockStatement))
                    defaultFlowOutTmp = this._generateVisualASTStatements(stmt, defaultFlowOutTmp);
            }
            # region IfStmts
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.IfElseStatement))
            {
                var ifStmt = stmtArg as ICSharpCode.NRefactory.CSharp.IfElseStatement;
                var ifNode = this._view.CreateAndAddNode<IfStmtNode>(nodePresenter);
                visualNode = ifNode;
                ifNode.Condition.SetName(ifStmt.Condition.ToString());
                this._generateVisualASTExpressions(ifStmt.Condition, ifNode.Condition);
                this._generateVisualASTStatements(ifStmt.TrueStatement, ifNode.trueAnchor);
                this._generateVisualASTStatements(ifStmt.FalseStatement, ifNode.falseAnchor);
                defaultFlowOut = ifNode.outAnchor;
            }
            # endregion IfStmts
            # region Loops
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.WhileStatement) ||
                stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.DoWhileStatement))
            {
                bool isWhile = stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.WhileStatement);
                dynamic whileStmt = stmtArg;
                WhileStmtNode nodeLoop;

                nodeLoop = this._view.CreateAndAddNode<WhileStmtNode>(nodePresenter);
                visualNode = nodeLoop;
                nodeLoop.SetName((isWhile ? "While" : "DoWhile")); // TODO Remove
                nodeLoop.Condition.SetName(whileStmt.Condition.ToString());
                this._generateVisualASTExpressions(whileStmt.Condition, nodeLoop.Condition); // Expressions
                this._generateVisualASTStatements(whileStmt.EmbeddedStatement, nodeLoop.trueAnchor);
                defaultFlowOut = nodeLoop.outAnchor;
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForStatement))
            {
                var forStmt = stmtArg as ICSharpCode.NRefactory.CSharp.ForStatement;
                var nodeLoop = this._view.CreateAndAddNode<ForStmtNode>(nodePresenter);
                visualNode = nodeLoop;

                //foreach (var forStmts in forStmt.Initializers) // TODO @Seb @Mo
                //    this._generateVisualASTStatements(forStmts);
                //nodeLoop.Condition.SetName(forStmt.Condition.ToString());

                //foreach (var forStmts in forStmt.Iterators)
                //    this._generateVisualASTStatements(forStmts);

                this._generateVisualASTExpressions(forStmt.Condition, nodeLoop.Condition); // Expressions
                this._generateVisualASTStatements(forStmt.EmbeddedStatement, nodeLoop.trueAnchor);
                defaultFlowOut = nodeLoop.outAnchor;
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForeachStatement))
            {
                var forEachStmt = stmtArg as ICSharpCode.NRefactory.CSharp.ForeachStatement;
                var nodeLoop = this._view.CreateAndAddNode<ForeachStmtNode>(nodePresenter);
                visualNode = nodeLoop;
                nodeLoop.Condition.SetName(forEachStmt.VariableType.ToString() + " " + forEachStmt.VariableName.ToString()); // is it the good condition? seems weird (hamham)
                this._generateVisualASTExpressions(forEachStmt.InExpression, nodeLoop.Condition);
                this._generateVisualASTStatements(forEachStmt.EmbeddedStatement, nodeLoop.trueAnchor);
                defaultFlowOut = nodeLoop.outAnchor;
            }
            # endregion Loops
            #region Switch
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.SwitchStatement))
            {
                var switchStmtNode = this._view.CreateAndAddNode<SwitchStmtNode>(nodePresenter);
                visualNode = switchStmtNode;
                var switchStmt = (stmtArg as SwitchStatement);
                var exprInput = switchStmtNode.CreateAndAddInput<DataFlowItem>();
                exprInput.SetName(switchStmt.Expression.ToString());
                _generateVisualASTExpressions(switchStmt.Expression, exprInput);
                foreach (var switchSection in switchStmt.SwitchSections)
                {
                    foreach (var caseLabel in switchSection.CaseLabels)
                    {
                        var caseInput = switchStmtNode.CreateAndAddInput<DataFlowItem>();
                        caseInput.SetName(caseLabel.Expression.ToString());
                        _generateVisualASTExpressions(caseLabel.Expression, caseInput);
                    }
                    foreach (var switchSectionStmt in switchSection.Statements) // TODO @Seb @Mo something is wrong here
                    {
                        var caseOutput = switchStmtNode.CreateAndAddOutput<FlowNodeItem>();
                        caseOutput.SetName("CaseOut");
                        _generateVisualASTStatements(switchSectionStmt, caseOutput);
                    }
                }
                defaultFlowOut = switchStmtNode.outAnchor;
            }
            #endregion Switch
            #endregion Block Statement
            #region Single Statement
            #region Variable Declaration
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.VariableDeclarationStatement))
            {
                var varStmt = (ICSharpCode.NRefactory.CSharp.VariableDeclarationStatement)stmtArg;
                var variableNode = this._view.CreateAndAddNode<VarDeclStmtNode>(nodePresenter);
                visualNode = variableNode;
                variableNode.SetName(varStmt.Type.ToString());
                defaultFlowOut = variableNode.outAnchor;
                foreach (var v in varStmt.Variables)
                {
                    var item = variableNode.CreateAndAddOutput<DataFlowItem>();
                    var inputValue = variableNode.CreateAndAddInput<DataFlowItem>(); // @Seb: The value assigned to the variable
                    item.SetName(v.Name);
                    inputValue.SetName("default()");
                    _generateVisualASTExpressions(v.Initializer, inputValue);
                }
            }
            #endregion Variable Declaration
            #region ExpressionStatement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ExpressionStatement))
            {
                var exprStmt = stmtArg as ExpressionStatement;

                var exprStmtNode = this._view.CreateAndAddNode<ExpressionStmtNode>(nodePresenter);
                visualNode = exprStmtNode;
                exprStmtNode.Expression.SetName(exprStmt.ToString());
                defaultFlowOut = exprStmtNode.outAnchor;
                this._generateVisualASTExpressions(exprStmt.Expression, exprStmtNode.Expression);
            }
            #endregion ExpressionStatement
            #region Return Statement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ReturnStatement))
            {
                var returnStmt = stmtArg as ExpressionStatement;
                var returnStmtNode = this._view.CreateAndAddNode<ReturnStmtNode>(nodePresenter);
                visualNode = returnStmtNode;

                if (returnStmt != null)
                    this._generateVisualASTExpressions(returnStmt.Expression, returnStmtNode.ExprIn);
            }
            #endregion Return Statement
            #endregion Single Statement
            else // Default Node
            {
                var unSupStmt = this._view.CreateAndAddNode<UnSupStmtDeclNode>(nodePresenter);
                visualNode = unSupStmt;
                unSupStmt.NodeText.Text = stmtArg.ToString();
                defaultFlowOut = unSupStmt.FlowOutAnchor;
            }
            //drawAutoLink(); // TODO draw method
            DrawFlowLinkNode(lastOutput, visualNode);
            return defaultFlowOut;
        }
        private void _generateVisualASTExpressions(ICSharpCode.NRefactory.CSharp.Expression expr, DataFlowItem outAnchor)
        {
            if (expr.IsNull)
                return;
            AValueNode visualNode = null;
            var nodePresenter = new NodePresenter(this, expr);
            if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression))
            {
                var unaryExprOp = expr as ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression;
                var unaryExprNode = this._view.CreateAndAddNode<UnaryExprNode>(nodePresenter);
                visualNode = unaryExprNode;
                unaryExprNode.SetName(unaryExprOp.OperatorToken.ToString());
                this._generateVisualASTExpressions(unaryExprOp.Expression, unaryExprNode.ExprOut);
            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ObjectCreateExpression))
            {
                var objCreateExpr = expr as ICSharpCode.NRefactory.CSharp.ObjectCreateExpression;
                var objCreateExprNode = this._view.CreateAndAddNode<FuncCallExprNode>(nodePresenter); // TODO Create a node for that
                visualNode = objCreateExprNode;
                objCreateExprNode.SetType("ObjCreateExpr");
                var newType = objCreateExprNode.CreateAndAddInput<DataFlowItem>(); // TODO @Seb text input ?
                newType.SetName(objCreateExpr.Type.ToString());
                int i = 0;
                foreach (var param in objCreateExpr.Arguments)
                {
                    var arg = objCreateExprNode.CreateAndAddInput<DataFlowItem>();
                    arg.SetName("param" + i);
                    i++;
                    this._generateVisualASTExpressions(param, arg);
                }

            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.IdentifierExpression))
            {
                var identExpr = expr as ICSharpCode.NRefactory.CSharp.IdentifierExpression;
                var identExprNode = this._view.CreateAndAddNode<FuncCallExprNode>(nodePresenter); // TODO Create a node for that
                visualNode = identExprNode;
                identExprNode.SetType("IdentExpr");
                identExprNode.ExprOut.SetName(identExpr.Identifier);
            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.AssignmentExpression))
            {
                var assignExpr = expr as ICSharpCode.NRefactory.CSharp.AssignmentExpression;
                var assignExprNode = this._view.CreateAndAddNode<BinaryExprNode>(nodePresenter);
                visualNode = assignExprNode;
                assignExprNode.SetType("AssignExpr");

                this._generateVisualASTExpressions(assignExpr.Left, assignExprNode.OperandA);
                this._generateVisualASTExpressions(assignExpr.Right, assignExprNode.OperandB);
            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression))
            {
                var binaryExpr = expr as ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression;
                var binaryExprNode = this._view.CreateAndAddNode<BinaryExprNode>(nodePresenter);
                visualNode = binaryExprNode;

                this._generateVisualASTExpressions(binaryExpr.Left, binaryExprNode.OperandA);
                this._generateVisualASTExpressions(binaryExpr.Right, binaryExprNode.OperandB);

            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MemberReferenceExpression))
            {
                var memberRefExpr = expr as ICSharpCode.NRefactory.CSharp.MemberReferenceExpression;
                var memberRefExprNode = this._view.CreateAndAddNode<FuncCallExprNode>(nodePresenter); // TODO Create a node for that
                visualNode = memberRefExprNode;
                var inputTarget = memberRefExprNode.CreateAndAddInput<DataFlowItem>();
                this._generateVisualASTExpressions(memberRefExpr.Target, memberRefExprNode.ExprOut);
            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.PrimitiveExpression))
            {
                var primExpr = expr as ICSharpCode.NRefactory.CSharp.PrimitiveExpression;
                var primExprNode = this._view.CreateAndAddNode<FuncCallExprNode>(nodePresenter); // TODO Create a node for that
                visualNode = primExprNode;
                primExprNode.SetType("PrimExpr");
                primExprNode.ExprOut.SetName(primExpr.LiteralValue);
            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.InvocationExpression))
            {
                var invokExpr = expr as ICSharpCode.NRefactory.CSharp.InvocationExpression;
                var invokExprNode = this._view.CreateAndAddNode<FuncCallExprNode>(nodePresenter);
                visualNode = invokExprNode;
                var invokTargetNode = invokExprNode.CreateAndAddInput<DataFlowItem>();

                this._generateVisualASTExpressions(invokExpr.Target, invokExprNode.ExprOut);
                // TODO @Seb @Mo display for generic parameters in FuncCallExprNode
                int i = 0;
                foreach (var param in invokExpr.Arguments)
                {
                    var paramMeth = invokExprNode.CreateAndAddInput<DataFlowItem>();
                    paramMeth.SetName("param" + i);
                    this._generateVisualASTExpressions(param, paramMeth);
                    i++;
                }
            }
            else
            {
                var defaultUnsupportedNode = this._view.CreateAndAddNode<UnSupExpNode>(nodePresenter);
                visualNode = defaultUnsupportedNode;
                defaultUnsupportedNode.NodeText.Text = expr.ToString();
            }
            DrawDataFlowLinkNode(outAnchor, visualNode);
        }

        private void DrawDataFlowLinkNode(DataFlowItem lastOutput, AValueNode valNode)
        {
            //System.Diagnostics.Debug.Assert(stmtNode != null);
            if (lastOutput != null && valNode != null)
                (lastOutput.GetRootView() as NodalView).drawLink(lastOutput, valNode.ExprOut);
        }

        public NodalModel ParseFile(String path)
        {
            StreamReader fileStream = new StreamReader(path);
            var ast = _parser.Parse(fileStream);
            NodalModel model = new NodalModel(ast);
            return model;
        }

        public void SaveFile(String filePath)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath);
            String code = "// Generated by Visual Studio's Code_in.";
            sw.WriteLine(code);
            //sw.Write(_codeData.AST.ToString());
        }

        //private void drawAutoLink()
        //{
        //    if (inputFlownode != null && outputFlownode != null)
        //    {
        //        (outputFlownode.GetRootView() as NodalView).drawLink(outputFlownode, inputFlownode);
        //        outputFlownode = nextOutputFlowNode;
        //        nextOutputFlowNode = null;
        //        inputFlownode = null;
        //    }
        //}

        private void DrawFlowLinkNode(FlowNodeItem lastOutput, AStatementNode stmtNode)
        {
            //System.Diagnostics.Debug.Assert(stmtNode != null);
            if (lastOutput != null && stmtNode != null)
                (lastOutput.GetRootView() as NodalView).drawLink(lastOutput, stmtNode.FlowInAnchor);
        }

        Tuple<EContextMenuOptions, Action<object[]>>[] IContextMenu.GetMenuOptions()
        {
            return new Tuple<EContextMenuOptions, Action<object[]>>[] { 
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ADD, AddNode), 
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ALIGN, AlignNode),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.COLLAPSEALL, CollapseAllNode),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.SAVE, SaveNode),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.CLOSE, CloseNode),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.HELP, HelpNode)
            };
        }
        static void AddNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void AlignNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void CloseNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void CollapseAllNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void HelpNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void SaveNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }

    }
}