﻿using code_in.Models;
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

        private AIONode inputNode = null;
        private AIONode outputNode = null;
        private AIONode saveFirstNodeBlockStatement = null; // no idea for the name of the var for the moment (hamham)

        private AOItem outputFlownode = null;
        private AOItem inputFlownode = null;

        private enum EFlowMode
        {
            FLOWNODE = 1,
            IFTRUE = 2,
            IFFALSE = 3,
        }

        private EFlowMode flowMode;

        public NodalPresenterLocal(INodalView view)
        {
            this.flowMode = EFlowMode.FLOWNODE;
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
            UsingDeclNode usingNode;

            usingNode = this._view.CreateAndAddNode<UsingDeclNode>();
            this._generateVisualASTDeclarationRecur(model.AST, this._view, usingNode);
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
            view.setGenerics(typeDecl);
        }
        private void _generateVisualASTDeclarationRecur(AstNode node, IVisualNodeContainer parentContainer, IVisualNodeContainer UsingNode)
        {
            bool goDeeper = true;
            IVisualNodeContainer parentNode = null;
            if (node.Children == null)
                return;
            #region Namespace
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration))
            {
                NamespaceNode namespaceNode = parentContainer.CreateAndAddNode<NamespaceNode>();
                namespaceNode.SetNodePresenter(new NodePresenter(namespaceNode, this, node));

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
                    ClassDeclNode enumDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>();
                    enumDeclNode.SetClassType((code_in.Views.NodalView.NodesElems.Nodes.ClassDeclNode.EType)3);
                    enumDeclNode.SetName(tmpNode.Name);

                    foreach (var v in tmpNode.Members)
                    {
                        var tmp = v as EnumMemberDeclaration;
                        var item = enumDeclNode.CreateAndAddNode<DataFlowItem>();
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
                    ClassDeclNode classDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>();
                    classDeclNode.SetNodePresenter(new NodePresenter(classDeclNode, this, node));
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

                            var item = classDeclNode.CreateAndAddNode<ClassItem>();
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
                    ClassDeclNode interfaceDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>();
                    interfaceDeclNode.SetNodePresenter(new NodePresenter(interfaceDeclNode, this, node));
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

                            var item = interfaceDeclNode.CreateAndAddNode<ClassItem>();
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
                FuncDeclItem funcDecl = parentContainer.CreateAndAddNode<FuncDeclItem>();
                funcDecl.MethodNode = node as ICSharpCode.NRefactory.CSharp.MethodDeclaration;
                ICSharpCode.NRefactory.CSharp.MethodDeclaration method = node as ICSharpCode.NRefactory.CSharp.MethodDeclaration;
                funcDecl.SetNodePresenter(new NodePresenter(funcDecl, this, node));


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
                UsingDeclItem UsingItem = UsingNode.CreateAndAddNode<UsingDeclItem>();
                UsingDeclaration UsingAstNode = node as ICSharpCode.NRefactory.CSharp.UsingDeclaration;

                UsingItem.SetName(UsingAstNode.Namespace);
                goDeeper = false;
            }

            #endregion
            if (goDeeper)
                foreach (var n in node.Children) if (n.GetType() != typeof(ICSharpCode.NRefactory.CSharp.FieldDeclaration))
                        this._generateVisualASTDeclarationRecur(n, (parentNode != null ? parentNode : parentContainer), UsingNode);
        }

        private void _generateVisualASTFunctionBody(MethodDeclaration method)
        {
            var entry = this._view.CreateAndAddNode<FuncEntryNode>();
            var exit = this._view.CreateAndAddNode<ReturnStmtNode>();

            outputNode = entry;

            foreach (var i in method.Parameters)
            {
                var data = entry.CreateAndAddOutput<DataFlowItem>();
                data.SetName(i.Name);
                data.SetItemType(i.Type.ToString());
            }

            this._generateVisualASTStatements(method.Body, 100, 250);

            inputNode = exit;
            drawAutoLink();

            var returnType = exit.CreateAndAddInput<FlowNodeItem>();
            returnType.SetName(method.ReturnType.ToString());



            //exit.Margin = new Thickness(0, 150, 0, 0);
        }
        private void _generateVisualASTStatements(Statement stmtArg, int posX, int posY)
        {
            #region Block Statement
            if (stmtArg.GetType() == typeof(BlockStatement))
            {
              //  MessageBox.Show("block Statement");
                foreach (var stmt in (stmtArg as BlockStatement))
                {
                    posX += 250;
                    this._generateVisualASTStatements(stmt, posX, posY);
                }
            }
            if (stmtArg.GetType() == typeof(TryCatchStatement))
            {
                var tryCatch = this._view.CreateAndAddNode<UnSupStmtDeclNode>();
                var tryStmt = stmtArg as TryCatchStatement;
                tryCatch.NodeText.Text = tryStmt.ToString();
            }
            # region IfStmts
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.IfElseStatement))
            {
                MessageBox.Show("_generateVisualASTStatements if");
                var ifStmt = stmtArg as ICSharpCode.NRefactory.CSharp.IfElseStatement;
                var ifNode = this._view.CreateAndAddNode<IfStmtNode>();
                ifNode.Margin = new System.Windows.Thickness(posX, posY, 0, 0);
                inputNode = ifNode;
                ifNode.Condition.SetName(ifStmt.Condition.ToString());
                drawAutoLink();
                this._generateVisualASTExpressions(ifStmt.Condition, posX - 300, posY + 100);
                this.flowMode = EFlowMode.IFTRUE;
                this._generateVisualASTStatements(ifStmt.TrueStatement, posX, posY);
                outputNode = saveFirstNodeBlockStatement;
                this.flowMode = EFlowMode.IFFALSE;
                this._generateVisualASTStatements(ifStmt.FalseStatement, posX, posY);
                this.flowMode = EFlowMode.FLOWNODE;
                outputNode = saveFirstNodeBlockStatement;
            }


            # endregion IfStmts
            # region Loops
            bool isWhile = (isWhile = stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.WhileStatement)) ||
                stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.DoWhileStatement);
            if (isWhile)
            {
                dynamic whileStmt = stmtArg;
                WhileStmtNode nodeLoop;

                nodeLoop = this._view.CreateAndAddNode<WhileStmtNode>();
                nodeLoop.Margin = new System.Windows.Thickness(posX, posY, 0, 0);

                nodeLoop.SetName((stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.WhileStatement) ? "While" : "DoWhile")); // TODO Remove
                nodeLoop.Condition.SetName(whileStmt.Condition.ToString());

                this._generateVisualASTExpressions(whileStmt.Condition, posX - 300, posY + 100); // Expressions
                this._generateVisualASTStatements(whileStmt.EmbeddedStatement, posX, posY);
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForStatement))
            {
                var forStmt = stmtArg as ICSharpCode.NRefactory.CSharp.ForStatement;
                var nodeLoop = this._view.CreateAndAddNode<ForStmtNode>();
                nodeLoop.Margin = new System.Windows.Thickness(posX, posY, 0, 0);

                nodeLoop.SetName("For");

                foreach (var forStmts in forStmt.Initializers)
                    this._generateVisualASTStatements(forStmts, posX, posY);
                //nodeLoop.Condition.SetName(forStmt.Condition.ToString());

                foreach (var forStmts in forStmt.Iterators)
                    this._generateVisualASTStatements(forStmts, posX, posY);

                this._generateVisualASTExpressions(forStmt.Condition, posX - 300, posY + 100); // Expressions
                this._generateVisualASTStatements(forStmt.EmbeddedStatement, posX, posY);
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForeachStatement))
            {
                var forEachStmt = stmtArg as ICSharpCode.NRefactory.CSharp.ForeachStatement;
                var nodeLoop = this._view.CreateAndAddNode<ForeachStmtNode>();
                nodeLoop.Margin = new System.Windows.Thickness(posX, posY, 0, 0);

                nodeLoop.SetName("ForEach");
                //nodeLoop.Condition.SetName(forEachStmt.VariableType.ToString() + " " + forEachStmt.VariableName.ToString());
                this._generateVisualASTExpressions(forEachStmt.InExpression, posX - 300, posY + 100);
                this._generateVisualASTStatements(forEachStmt.EmbeddedStatement, posX, posY);

            }
            # endregion Loops
            #region Switch
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.SwitchStatement))
            {
                var switchStmtNode = this._view.CreateAndAddNode<SwitchStmtNode>();
                switchStmtNode.Margin = new System.Windows.Thickness(posX, posY, 0, 0);
                var switchStmt = (stmtArg as SwitchStatement);
                var exprInput = switchStmtNode.CreateAndAddInput<DataFlowItem>();
                exprInput.SetName(switchStmt.Expression.ToString());
                _generateVisualASTExpressions(switchStmt.Expression, posX - 300, posY + 100);
                foreach (var switchSection in switchStmt.SwitchSections)
                {
                    foreach (var caseLabel in switchSection.CaseLabels)
                    {
                        var caseInput = switchStmtNode.CreateAndAddInput<DataFlowItem>();
                        var caseOutput = switchStmtNode.CreateAndAddOutput<FlowNodeItem>();
                        caseInput.SetName(caseLabel.Expression.ToString());
                        _generateVisualASTExpressions(caseLabel.Expression, posX - 300, posY + 100);
                    }
                    foreach (var switchSectionStmt in switchSection.Statements)
                    {
                        posY += 150;
                        _generateVisualASTStatements(switchSectionStmt, posX, posY);
                    }
                }
            }
            #endregion Switch
            #endregion Block Statement
            #region Single Statement
            #region Variable Declaration
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.VariableDeclarationStatement))
            {
                MessageBox.Show("_generateVisualASTStatements variable");
                var varStmt = (ICSharpCode.NRefactory.CSharp.VariableDeclarationStatement)stmtArg;
                var variableNode = this._view.CreateAndAddNode<VarDeclStmtNode>();
                variableNode.Margin = new System.Windows.Thickness(posX, posY, 0, 0);
                variableNode.SetType("Variables");
                inputNode = variableNode;
                foreach (var v in varStmt.Variables)
                {
                    var item = variableNode.CreateAndAddOutput<DataFlowItem>();
                    item.SetName(v.Name);
                    item.SetItemType(varStmt.Type.ToString());
                }
            }
            #endregion Variable Declaration
            #region ExpressionStatement
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ExpressionStatement))
            {
                MessageBox.Show("_generateVisualASTStatements expression");
                var exprStmt = stmtArg as ExpressionStatement;

                var exprStmtNode = this._view.CreateAndAddNode<ExpressionStmtNode>();
                exprStmtNode.Margin = new System.Windows.Thickness(posX, posY, 0, 0);
                exprStmtNode.Expression.SetName(exprStmt.ToString());
                inputNode = exprStmtNode;
                this._generateVisualASTExpressions(exprStmt.Expression, posX - 300, posY + 100);
            }
            #endregion ExpressionStatement
            #region Return Statement
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ReturnStatement))
            {
                var returnStmt = stmtArg as ExpressionStatement;

                var returnStmtNode = this._view.CreateAndAddNode<ReturnStmtNode>();
                returnStmtNode.Margin = new System.Windows.Thickness(posX, posY, 0, 0);

                if (returnStmt != null)
                {
                    this._generateVisualASTExpressions(returnStmt.Expression, posX - 300, posY + 100);
                }
            }
            #endregion Return Statement
            #endregion Single Statement
            drawAutoLink();
        }
        private void _generateVisualASTExpressions(ICSharpCode.NRefactory.CSharp.Expression expr, int posX, int posY)
        {
            if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression))
            {
                var unaryExprOp = expr as ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression;
                var unaryExpr = this._view.CreateAndAddNode<UnaryExprNode>();
                unaryExpr.Margin = new System.Windows.Thickness(posX, posY, 0, 0);
                unaryExpr.SetName(unaryExprOp.OperatorToken.ToString());
            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression))
            {
                var binaryExpr = this._view.CreateAndAddNode<BinaryExprNode>();
                binaryExpr.Margin = new System.Windows.Thickness(posX, posY, 0, 0);


            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.InvocationExpression))
            {
                var invokExpr = this._view.CreateAndAddNode<FuncCallExprNode>();
                invokExpr.Margin = new System.Windows.Thickness(posX, posY, 0, 0);


            }
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

        private void drawAutoLink()
        {
            if (inputNode != null && outputNode != null)
            {
               // MessageBox.Show(flowMode.ToString());
                foreach (var i in outputNode._outputs.Children)
                {
                    AOItem it = i as AOItem;

                    if (this.flowMode == EFlowMode.IFTRUE && it.GetType() == typeof(FlowNodeItem) && it.GetName() == "True") // eww need to change about the condition getname I guess (hamham)
                    {
                        outputFlownode = it;
                        saveFirstNodeBlockStatement = outputNode;
                    }
                    else if (this.flowMode == EFlowMode.IFFALSE && it.GetType() == typeof(FlowNodeItem) && it.GetName() == "False") // eww need to change about the condition getname I guess (hamham)
                    {
                        outputFlownode = it;
                        saveFirstNodeBlockStatement = outputNode;
                    }
                    else if (it.GetType() == typeof(FlowNodeItem) && it.GetName() == "FlowNode") // eww need to change about the condition getname I guess (hamham)
                        outputFlownode = it;
                }

                foreach (var i in inputNode._inputs.Children)
                {
                    AOItem it = i as AOItem;
                    if (it.GetType() == typeof(FlowNodeItem))
                        inputFlownode = it;
                }

                (outputFlownode.GetRootView() as NodalView).drawLink(outputFlownode, inputFlownode);
                outputNode = inputNode;
                inputNode = null;
            }
        }

        Tuple<EContextMenuOptions, Action<object[]>>[] IContextMenu.GetMenuOptions()
        {
            return new Tuple<EContextMenuOptions, Action<object[]>>[] { 
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ADD, null), 
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ALIGN, null),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.COLLAPSEALL, null),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.SAVE, null),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.CLOSE, null),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.HELP, null)
            };
        }
    }
}