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
        private AOItem outputFlownode = null;
        private AOItem inputFlownode = null;
        private AOItem nextOutputFlowNode = null;

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

            outputFlownode = entry.outAnchor;

            foreach (var i in method.Parameters)
            {
                var data = entry.CreateAndAddOutput<DataFlowItem>();
                data.SetName(i.Name);
                data.SetItemType(i.Type.ToString());
            }

            this._generateVisualASTStatements(method.Body);

            inputFlownode = exit.inAnchor;
            drawAutoLink();

            var returnType = exit.CreateAndAddInput<FlowNodeItem>();
            returnType.SetName(method.ReturnType.ToString());
        }
        private void _generateVisualASTStatements(Statement stmtArg)
        {
            #region Block Statement
            if (stmtArg.GetType() == typeof(BlockStatement))
            {
                foreach (var stmt in (stmtArg as BlockStatement))
                {
                    this._generateVisualASTStatements(stmt);
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
                var ifStmt = stmtArg as ICSharpCode.NRefactory.CSharp.IfElseStatement;
                var ifNode = this._view.CreateAndAddNode<IfStmtNode>();
                inputFlownode = ifNode.inAnchor;
                drawAutoLink();
                ifNode.Condition.SetName(ifStmt.Condition.ToString());
                this._generateVisualASTExpressions(ifStmt.Condition);
                outputFlownode = ifNode.trueAnchor;
                this._generateVisualASTStatements(ifStmt.TrueStatement);
                outputFlownode = ifNode.falseAnchor;
                this._generateVisualASTStatements(ifStmt.FalseStatement);
                outputFlownode = ifNode.outAnchor;
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
                inputFlownode = nodeLoop.inAnchor;
                drawAutoLink();
                nodeLoop.SetName((stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.WhileStatement) ? "While" : "DoWhile")); // TODO Remove
                nodeLoop.Condition.SetName(whileStmt.Condition.ToString());
                this._generateVisualASTExpressions(whileStmt.Condition); // Expressions
                outputFlownode = nodeLoop.trueAnchor;
                this._generateVisualASTStatements(whileStmt.EmbeddedStatement);
                outputFlownode = nodeLoop.outAnchor;
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForStatement))
            {
                var forStmt = stmtArg as ICSharpCode.NRefactory.CSharp.ForStatement;
                var nodeLoop = this._view.CreateAndAddNode<ForStmtNode>();
                inputFlownode = nodeLoop.inAnchor;
                drawAutoLink();
               foreach (var forStmts in forStmt.Initializers)
                    this._generateVisualASTStatements(forStmts);
                nodeLoop.Condition.SetName(forStmt.Condition.ToString());

                foreach (var forStmts in forStmt.Iterators)
                    this._generateVisualASTStatements(forStmts);

                this._generateVisualASTExpressions(forStmt.Condition); // Expressions
                outputFlownode = nodeLoop.trueAnchor;
                this._generateVisualASTStatements(forStmt.EmbeddedStatement);
                outputFlownode = nodeLoop.outAnchor;
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForeachStatement))
            {
                var forEachStmt = stmtArg as ICSharpCode.NRefactory.CSharp.ForeachStatement;
                var nodeLoop = this._view.CreateAndAddNode<ForeachStmtNode>();
                inputFlownode = nodeLoop.inAnchor;
                drawAutoLink();

                nodeLoop.Condition.SetName(forEachStmt.VariableType.ToString() + " " + forEachStmt.VariableName.ToString()); // is it the good condition? seems weird (hamham)
                this._generateVisualASTExpressions(forEachStmt.InExpression);
                outputFlownode = nodeLoop.trueAnchor;
                this._generateVisualASTStatements(forEachStmt.EmbeddedStatement);
                outputFlownode = nodeLoop.outAnchor;
            }
            # endregion Loops
            #region Switch
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.SwitchStatement))
            {
                var switchStmtNode = this._view.CreateAndAddNode<SwitchStmtNode>();
                var switchStmt = (stmtArg as SwitchStatement);
                var exprInput = switchStmtNode.CreateAndAddInput<DataFlowItem>();
                exprInput.SetName(switchStmt.Expression.ToString());
                inputFlownode = exprInput;
                drawAutoLink();
                _generateVisualASTExpressions(switchStmt.Expression);
                foreach (var switchSection in switchStmt.SwitchSections)
                {
                    foreach (var caseLabel in switchSection.CaseLabels)
                    {
                        var caseInput = switchStmtNode.CreateAndAddInput<DataFlowItem>();
                        var caseOutput = switchStmtNode.CreateAndAddOutput<FlowNodeItem>();
                        outputFlownode = caseOutput;
                        caseInput.SetName(caseLabel.Expression.ToString());
                        caseOutput.SetName("Case");
                        _generateVisualASTExpressions(caseLabel.Expression);
                    }
                    foreach (var switchSectionStmt in switchSection.Statements)
                        _generateVisualASTStatements(switchSectionStmt);
                }
                outputFlownode = switchStmtNode.outAnchor;
            }
            #endregion Switch
            #endregion Block Statement
            #region Single Statement
            #region Variable Declaration
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.VariableDeclarationStatement))
            {
                var varStmt = (ICSharpCode.NRefactory.CSharp.VariableDeclarationStatement)stmtArg;
                var variableNode = this._view.CreateAndAddNode<VarDeclStmtNode>();
                variableNode.SetName(varStmt.Type.ToString());
                inputFlownode = variableNode.inAnchor;
                nextOutputFlowNode = variableNode.outAnchor;
                foreach (var v in varStmt.Variables)
                {
                    var item = variableNode.CreateAndAddOutput<DataFlowItem>();
                    var inputValue = variableNode.CreateAndAddInput<DataFlowItem>(); // @Seb: The value assigned to the variable
                    item.SetName(v.Name);
                    inputValue.SetName("default()");
                    _generateVisualASTExpressions(v.Initializer);
                }
            }
            #endregion Variable Declaration
            #region ExpressionStatement
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ExpressionStatement))
            {
                var exprStmt = stmtArg as ExpressionStatement;

                var exprStmtNode = this._view.CreateAndAddNode<ExpressionStmtNode>();
                exprStmtNode.Expression.SetName(exprStmt.ToString());
                inputFlownode = exprStmtNode.inAnchor;
                nextOutputFlowNode = exprStmtNode.outAnchor;
                this._generateVisualASTExpressions(exprStmt.Expression);
            }
            #endregion ExpressionStatement
            #region Return Statement
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ReturnStatement))
            {
                var returnStmt = stmtArg as ExpressionStatement;

                var returnStmtNode = this._view.CreateAndAddNode<ReturnStmtNode>();

                if (returnStmt != null)
                    this._generateVisualASTExpressions(returnStmt.Expression);
            }
            #endregion Return Statement
            #endregion Single Statement
            drawAutoLink();
        }
        private void _generateVisualASTExpressions(ICSharpCode.NRefactory.CSharp.Expression expr)
        {
            if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression))
            {
                var unaryExprOp = expr as ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression;
                var unaryExpr = this._view.CreateAndAddNode<UnaryExprNode>();
                unaryExpr.SetName(unaryExprOp.OperatorToken.ToString());
                this._generateVisualASTExpressions(unaryExprOp.Expression);
            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ObjectCreateExpression))
            {
                var objCreateExpr = expr as ICSharpCode.NRefactory.CSharp.ObjectCreateExpression;
                var objCreateExprNode = this._view.CreateAndAddNode<FuncCallExprNode>(); // TODO Create a node for that
                objCreateExprNode.SetType("ObjCreateExpr");
                var newType = objCreateExprNode.CreateAndAddInput<DataFlowItem>(); // TODO text input
                newType.SetName(objCreateExpr.Type.ToString());
                int i = 0;
                foreach (var param in objCreateExpr.Arguments)
                {
                    var arg = objCreateExprNode.CreateAndAddInput<DataFlowItem>();
                    arg.SetName("param" + i);
                    i++;
                    _generateVisualASTExpressions(param);
                }

            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.IdentifierExpression))
            {
                var identExpr = expr as ICSharpCode.NRefactory.CSharp.IdentifierExpression;
                var identExprNode = this._view.CreateAndAddNode<FuncCallExprNode>(); // TODO Create a node for that
                identExprNode.SetType("IdentExpr");
                identExprNode.ExprOut.SetName(identExpr.Identifier);
            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.AssignmentExpression))
            {
                var assignExpr = expr as ICSharpCode.NRefactory.CSharp.AssignmentExpression;
                var assignExprNode = this._view.CreateAndAddNode<BinaryExprNode>();
                assignExprNode.SetType("AssignExpr");

                this._generateVisualASTExpressions(assignExpr.Left);
                this._generateVisualASTExpressions(assignExpr.Right);
            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression))
            {
                var binaryExpr = expr as ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression;
                var binaryExprNode = this._view.CreateAndAddNode<BinaryExprNode>();

                this._generateVisualASTExpressions(binaryExpr.Left);
                this._generateVisualASTExpressions(binaryExpr.Right);

            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MemberReferenceExpression))
            {
                var memberRefExpr = expr as ICSharpCode.NRefactory.CSharp.MemberReferenceExpression;
                var memberRefExprNode = this._view.CreateAndAddNode<FuncCallExprNode>(); // TODO Create a node for that
                var inputTarget = memberRefExprNode.CreateAndAddInput<DataFlowItem>();
                this._generateVisualASTExpressions(memberRefExpr.Target);
            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.PrimitiveExpression))
            {
                var primExpr = expr as ICSharpCode.NRefactory.CSharp.PrimitiveExpression;
                var primExprNode = this._view.CreateAndAddNode<FuncCallExprNode>(); // TODO Create a node for that
                primExprNode.SetType("PrimExpr");
                primExprNode.ExprOut.SetName(primExpr.LiteralValue);
            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.InvocationExpression))
            {
                var invokExpr = expr as ICSharpCode.NRefactory.CSharp.InvocationExpression;
                var invokExprNode = this._view.CreateAndAddNode<FuncCallExprNode>();
                var invokTargetNode = invokExprNode.CreateAndAddInput<DataFlowItem>();

                this._generateVisualASTExpressions(invokExpr.Target);
                // TODO @Seb @Mo display for generic parameters in FuncCallExprNode
                int i = 0;
                foreach (var param in invokExpr.Arguments)
                {
                    var paramMeth = invokExprNode.CreateAndAddInput<DataFlowItem>();
                    paramMeth.SetName("param" + i);
                    this._generateVisualASTExpressions(param);
                    i++;
                }
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
            if (inputFlownode != null && outputFlownode != null)
            {
                (outputFlownode.GetRootView() as NodalView).drawLink(outputFlownode, inputFlownode);
                outputFlownode = nextOutputFlowNode;
                nextOutputFlowNode = null;
                inputFlownode = null;
            }
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