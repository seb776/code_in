using code_in.Models;
using code_in.Models.NodalModel;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView;
using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Assets;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Expressions;
using code_in.Views.NodalView.NodesElems.Nodes.Statements;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Block;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Context;
using code_in.Views.Utils;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace code_in.Presenters.Nodal
{
    /// <summary>
    /// This NodalPresenter is used when you are wroking on a local file, without using network.
    /// </summary>
    public class NodalPresenterLocal : INodalPresenter
    {
        public INodalView _view = null;
        private NodalModel _model = null;
        private CSharpParser _parser = null;
        const int nodeHorizontalOffset = 50; // Used to set the offset of nodes display from the left-top corner of the screen
        const int nodeVerticalOffset = 50; // Used to set the offset of nodes display from the left-top corner of the screen

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
            if(node.MethodNode is ICSharpCode.NRefactory.CSharp.ConstructorDeclaration)
            {
                this._generateVisualASTConstructorBody(node.MethodNode);
            }
            else
            {
                this._generateVisualASTFunctionBody(node.MethodNode);
            }
        }

        private void _generateVisualASTDeclaration(NodalModel model)
        {
            int accHorizontal = 0;
            int accVertical = 0;
            this._generateVisualASTDeclarationRecur(model.AST, this._view, ref accHorizontal, ref accVertical);
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
            List<string> InheritanceList = new List<string>();
            foreach (var inherit in typeDecl.BaseTypes)
                InheritanceList.Add(inherit.ToString());
            view.ManageInheritance(InheritanceList);
        }

        private void SetAllGenerics(IContainingGenerics view, TypeDeclaration typeDecl)
        {
            Tuple<string, EGenericVariance> tuple;
            List<Tuple<string, EGenericVariance>> GenericList = new List<Tuple<string, EGenericVariance>>();

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
        private void _generateVisualASTDeclarationRecur(AstNode node, IVisualNodeContainer parentContainer, ref int posX, ref int posY)
        {
            int currentPosX = posX;
            int currentPosY = posY;
            int horizontalStep = 20;
            int verticalStep = 20;
            INodeElem visualNode = null;
            var nodePresenter = new NodePresenter(this, node);
            if (node.Children == null)
                return;
            #region Global scope
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.SyntaxTree))
            {
                int localPosX = nodeHorizontalOffset, localPosY = nodeVerticalOffset;
                var syntaxTree = node as ICSharpCode.NRefactory.CSharp.SyntaxTree;
                foreach (var decl in syntaxTree.Members)
                {
                    this._generateVisualASTDeclarationRecur(decl, parentContainer, ref localPosX, ref localPosY);
                }
            }
            #endregion Global scope
            #region Namespace
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration))
            {
                int localPosX = nodeHorizontalOffset, localPosY = nodeVerticalOffset;
                NamespaceNode namespaceNode = parentContainer.CreateAndAddNode<NamespaceNode>(nodePresenter);
                visualNode = namespaceNode;
                var namespaceDecl = node as ICSharpCode.NRefactory.CSharp.NamespaceDeclaration;
                namespaceNode.SetName(namespaceDecl.Name);
                foreach (var member in namespaceDecl.Members)
                    this._generateVisualASTDeclarationRecur(member, namespaceNode, ref localPosX, ref localPosY);
            }
            #endregion
            #region Classes (interface, struct, class, enum)
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.TypeDeclaration)) // Handles class, struct, enum (see further)
            {
                var tmpNode = (ICSharpCode.NRefactory.CSharp.TypeDeclaration)node;
                #region Enum
                if (tmpNode.ClassType == ICSharpCode.NRefactory.CSharp.ClassType.Enum)
                {
                    ClassDeclNode enumDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>(nodePresenter);
                    visualNode = enumDeclNode;
                    enumDeclNode.SetClassType(code_in.Views.NodalView.NodesElems.Nodes.ClassDeclNode.EType.ENUM);
                    enumDeclNode.SetName(tmpNode.Name);

                    foreach (var v in tmpNode.Members)
                    {
                        var tmp = v as EnumMemberDeclaration;
                        var item = enumDeclNode.CreateAndAddNode<ClassItem>(nodePresenter);
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
                    visualNode = classDeclNode;
                    classDeclNode.SetName(tmpNode.Name);
                    setAccessModifiers(classDeclNode, tmpNode.Modifiers);
                    setOtherModifiers(classDeclNode, tmpNode.Modifiers);

                    //inheritance
                    InitInheritance(classDeclNode, tmpNode);
                    //Generic
                    SetAllGenerics(classDeclNode, tmpNode);

                    foreach (var member in tmpNode.Members)
                        _generateVisualASTDeclarationRecur(member, classDeclNode, ref posX, ref posY);
                }
                #endregion Class
                #region Interface
                else if (tmpNode.ClassType == ICSharpCode.NRefactory.CSharp.ClassType.Interface)
                {
                    ClassDeclNode interfaceDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>(nodePresenter);
                    visualNode = interfaceDeclNode;
                    interfaceDeclNode.SetClassType(code_in.Views.NodalView.NodesElems.Nodes.ClassDeclNode.EType.INTERFACE);
                    interfaceDeclNode.SetName(tmpNode.Name);
                    setAccessModifiers(interfaceDeclNode, tmpNode.Modifiers);

                    //inheritance
                    InitInheritance(interfaceDeclNode, tmpNode);
                    //Generic
                    SetAllGenerics(interfaceDeclNode, tmpNode);

                    foreach (var member in tmpNode.Members)
                        _generateVisualASTDeclarationRecur(member, interfaceDeclNode, ref posX, ref posY);
                }
                #endregion Interface
            }
            #endregion Classes (interface, struct, class, enum)
            #region Field
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.FieldDeclaration))
            {
                var fieldDecl = node as ICSharpCode.NRefactory.CSharp.FieldDeclaration;
                var item = parentContainer.CreateAndAddNode<ClassItem>(nodePresenter);
                visualNode = item;
                item.setTypeFromString(fieldDecl.ReturnType.ToString()); //Type setter for variable base -> TypeInfo.xaml.cs
                string varName = null;
                foreach (var variable in fieldDecl.Variables)
                {
                    if (variable != fieldDecl.Variables.Last<VariableInitializer>())
                        varName += variable.Name + ", ";
                    else
                        varName += variable.Name;
                }
                item.SetName(varName);
                setAccessModifiers(item, fieldDecl.Modifiers); // here just call setAccessModifiers from the interface
                setOtherModifiers(item, fieldDecl.Modifiers);
            }
            #endregion Field
            #region Property (get, set)
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.PropertyDeclaration))
            {
                var propertyDecl = node as ICSharpCode.NRefactory.CSharp.PropertyDeclaration;
                var item = parentContainer.CreateAndAddNode<ClassItem>(nodePresenter);
                visualNode = item;
                item.SetName(propertyDecl.Name.ToString()); // TODO Complete
                item.setTypeFromString(propertyDecl.ReturnType.ToString());
                setAccessModifiers(item, propertyDecl.Modifiers);
            }
            #endregion Property (get, set)
            #region Constructor
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ConstructorDeclaration))
            {
                FuncDeclItem constructorDecl = parentContainer.CreateAndAddNode<FuncDeclItem>(nodePresenter);
                visualNode = constructorDecl;
                constructorDecl.MethodNode = node as MethodDeclaration;
                ICSharpCode.NRefactory.CSharp.ConstructorDeclaration construct = node as ICSharpCode.NRefactory.CSharp.ConstructorDeclaration;
                var parameters = construct.Parameters.ToList();
                for(int i = 0; i < parameters.Count; i++)
                {
                    constructorDecl.AddParam(parameters[i].Type.ToString());
                    constructorDecl.SetName(construct.Name);
                    setAccessModifiers(constructorDecl, construct.Modifiers);
                }
            }
            #endregion Constructor
            #region Method
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MethodDeclaration))
            {
                FuncDeclItem funcDecl = parentContainer.CreateAndAddNode<FuncDeclItem>(nodePresenter);
                visualNode = funcDecl;
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
                funcDecl.setTypeFromString(method.ReturnType.ToString());
                setOtherModifiers(funcDecl, method.Modifiers);
                setAccessModifiers(funcDecl, method.Modifiers);
            }
            #endregion Method
            #region Using
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.UsingDeclaration))
            {
                var usingDecl = node as ICSharpCode.NRefactory.CSharp.UsingDeclaration;
                var usingDeclNode = parentContainer.CreateAndAddNode<UsingDeclNode>(nodePresenter);
                visualNode = usingDeclNode;
                UsingDeclItem UsingItem = usingDeclNode.CreateAndAddNode<UsingDeclItem>(nodePresenter);
                UsingItem.SetName(usingDecl.Namespace);
            }
            #endregion Using

            if (visualNode != null)
            {
                visualNode.SetPosition(currentPosX, currentPosY);
                int nodeWidth = 0, nodeHeight = 0;
                visualNode.GetSize(out nodeWidth, out nodeHeight);
                //posX += nodeWidth + horizontalStep;
                posY += nodeHeight + verticalStep;
            }
        }

        private void _generateVisualASTFunctionBody(MethodDeclaration method)
        {
            (this._view as NodalView).IsDeclarative = false;
            var nodePresenter = new NodePresenter(this, NodePresenter.EVirtualNodeType.FUNC_ENTRY);
            var entry = this._view.CreateAndAddNode<FuncEntryNode>(nodePresenter);

            foreach (var i in method.Parameters)
            {
                var data = entry.CreateAndAddOutput<DataFlowAnchor>();
                data.SetName(i.Name);
                //data.SetItemType(i.Type.ToString());
            }
            this._generateVisualASTStatements(method.Body, entry.FlowOutAnchor, null, null);
        }

        private void _generateVisualASTConstructorBody(MethodDeclaration constructor)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function displays the execution code from stmtArg to the NodalView attached.
        /// </summary>
        /// <param name="stmtArg"></param>
        private FlowNodeAnchor _generateVisualASTStatements(Statement stmtArg, FlowNodeAnchor lastOutput, Func<Statement, Statement> MethodAttachSTMT, Action MethodDetachStmt)
        {
            System.Diagnostics.Debug.Assert(lastOutput != null);
            AStatementNode visualNode = null;
            FlowNodeAnchor defaultFlowOut = null;
            var nodePresenter = new NodePresenter(this, stmtArg);
            #region Block Statement
            if (stmtArg.GetType() == typeof(BlockStatement))
            {
                FlowNodeAnchor defaultFlowOutTmp = lastOutput;
                foreach (var stmt in (stmtArg as BlockStatement))
                {
                    Func<Statement, Statement> specificMethodAttachSTMT = MethodAttachSTMT;
                    //if (true)//MethodAttachSTMT == null)
                    {
                        //defaultFlowOutTmp.DebugDisplay();
                        specificMethodAttachSTMT = (s) =>
                        {
                            //return null;
                            //defaultFlowOutTmp.DebugDisplay();
                            //MessageBox.Show("Attach " + defaultFlowOutTmp.ParentNode.GetNodePresenter().GetASTNode().ToString() + "\n" +
                            //    ((defaultFlowOutTmp.ParentNode as AStatementNode).FlowInAnchor._links[0].Output as FlowNodeAnchor).ParentNode.GetNodePresenter().GetASTNode().ToString()
                            //    );
                            Statement stmtToSpreadUp = null;
                            var curStmtNode = (defaultFlowOutTmp.ParentNode as AStatementNode);
                            var curNodeDefaultFlowIn = curStmtNode.FlowInAnchor;
                            if (curNodeDefaultFlowIn._links.Count != 0)
                            {
                                var curASTStmt = curStmtNode.GetNodePresenter().GetASTNode() as Statement;
                                bool enter = false;
                                (defaultFlowOutTmp.ParentNode as AStatementNode).SetSelected(true);
                                if (!(s is BlockStatement))
                                {
                                    BlockStatement blockStmt = new BlockStatement();
                                    blockStmt.Statements.Add(s);
                                    stmtToSpreadUp = blockStmt;
                                    enter = true;
                                }
                                else if (curASTStmt != (stmtArg as BlockStatement).First())
                                {
                                    BlockStatement blockStmt = s as BlockStatement;
                                    curASTStmt.Remove();
                                    blockStmt.Statements.InsertAfter(null, curASTStmt);
                                    stmtToSpreadUp = blockStmt;
                                    enter = true;
                                }
                                if (enter)
                                {
                                    //MessageBox.Show("Prev " + ((curNodeDefaultFlowIn._links[0].Output as FlowNodeAnchor).ParentNode as AStatementNode).GetNodePresenter().GetASTNode().ToString());
                                    (curNodeDefaultFlowIn._links[0].Output as FlowNodeAnchor).MethodAttachASTStmt(stmtToSpreadUp); // Go back to recreate the blockStatement
                                }
                            }
                            //(stmtArg as BlockStatement).Statements.InsertAfter(null, s);
                            return null;
                        };
                    }
                    defaultFlowOutTmp = this._generateVisualASTStatements(stmt, defaultFlowOutTmp, specificMethodAttachSTMT, () => { 
                        (stmtArg as BlockStatement).Statements.Remove(stmt);
                    });
                    if (stmt == (stmtArg as BlockStatement).Last() && defaultFlowOutTmp != null)
                        defaultFlowOutTmp.MethodAttachASTStmt = specificMethodAttachSTMT;
                }
            }
            # region IfStmts
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.IfElseStatement))
            {
                var ifStmt = stmtArg as ICSharpCode.NRefactory.CSharp.IfElseStatement;
                var ifNode = this._view.CreateAndAddNode<IfStmtNode>(nodePresenter);
                visualNode = ifNode;
                ifNode.Condition.SetName(ifStmt.Condition.ToString());
                this._generateVisualASTExpressions(ifStmt.Condition, ifNode.Condition, (e) => { ifStmt.Condition = e; });
                this._generateVisualASTStatements(ifStmt.TrueStatement, ifNode.trueAnchor, (s) => { var old = ifStmt.TrueStatement; ifStmt.TrueStatement = s; return old; }, () => { ifStmt.TrueStatement = null; });
                int curNodeWidth = 0, curNodeHeight = 0;
                ifNode.GetSize(out curNodeWidth, out curNodeHeight);
                this._generateVisualASTStatements(ifStmt.FalseStatement, ifNode.falseAnchor, (s) => { var old = ifStmt.FalseStatement; ifStmt.FalseStatement = s; return old; }, () => { ifStmt.FalseStatement = null; });
                defaultFlowOut = ifNode.FlowOutAnchor;
            }
            # endregion IfStmts
            # region Loops
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.DoWhileStatement))
            {
                bool isWhile = stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.WhileStatement);
                ICSharpCode.NRefactory.CSharp.DoWhileStatement whileStmt = stmtArg as ICSharpCode.NRefactory.CSharp.DoWhileStatement;
                WhileStmtNode nodeLoop;

                nodeLoop = this._view.CreateAndAddNode<WhileStmtNode>(nodePresenter);
                visualNode = nodeLoop;
                nodeLoop.SetName((isWhile ? "While" : "DoWhile")); // TODO Remove
                nodeLoop.Condition.SetName(whileStmt.Condition.ToString());
                this._generateVisualASTExpressions(whileStmt.Condition, nodeLoop.Condition, (e) => { whileStmt.Condition = e; }); // Expressions
                this._generateVisualASTStatements(whileStmt.EmbeddedStatement, nodeLoop.trueAnchor, (s) => { var old = whileStmt.EmbeddedStatement; whileStmt.EmbeddedStatement = s; return old; }, () => { whileStmt.EmbeddedStatement = null; });
                defaultFlowOut = nodeLoop.FlowOutAnchor;
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.WhileStatement))
            {
                bool isWhile = stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.WhileStatement);
                ICSharpCode.NRefactory.CSharp.WhileStatement whileStmt = stmtArg as ICSharpCode.NRefactory.CSharp.WhileStatement;
                WhileStmtNode nodeLoop;

                nodeLoop = this._view.CreateAndAddNode<WhileStmtNode>(nodePresenter);
                visualNode = nodeLoop;
                nodeLoop.SetName((isWhile ? "While" : "DoWhile")); // TODO Remove
                nodeLoop.Condition.SetName(whileStmt.Condition.ToString());
                this._generateVisualASTExpressions(whileStmt.Condition, nodeLoop.Condition, (e) => { whileStmt.Condition = e; }); // Expressions
                this._generateVisualASTStatements(whileStmt.EmbeddedStatement, nodeLoop.trueAnchor, (s) => { var old = whileStmt.EmbeddedStatement; whileStmt.EmbeddedStatement = s; return old; }, () => { whileStmt.EmbeddedStatement = null; });
                defaultFlowOut = nodeLoop.FlowOutAnchor;
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

                this._generateVisualASTExpressions(forStmt.Condition, nodeLoop.Condition, (e) => { forStmt.Condition = e; }); // Expressions
                this._generateVisualASTStatements(forStmt.EmbeddedStatement, nodeLoop.trueAnchor, (s) => { var old = forStmt.EmbeddedStatement; forStmt.EmbeddedStatement = s; return old; }, () => { forStmt.EmbeddedStatement = null; });
                defaultFlowOut = nodeLoop.FlowOutAnchor;
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForeachStatement))
            {
                var forEachStmt = stmtArg as ICSharpCode.NRefactory.CSharp.ForeachStatement;
                var nodeLoop = this._view.CreateAndAddNode<ForeachStmtNode>(nodePresenter);
                visualNode = nodeLoop;
                nodeLoop.Condition.SetName(forEachStmt.VariableType.ToString() + " " + forEachStmt.VariableName.ToString()); // is it the good condition? seems weird (hamham)
                this._generateVisualASTExpressions(forEachStmt.InExpression, nodeLoop.Condition, (e) => { forEachStmt.InExpression = e; });
                this._generateVisualASTStatements(forEachStmt.EmbeddedStatement, nodeLoop.trueAnchor, (s) => { var old = forEachStmt.EmbeddedStatement; forEachStmt.EmbeddedStatement = s; return old; }, () => { forEachStmt.EmbeddedStatement = null; });
                defaultFlowOut = nodeLoop.FlowOutAnchor;
            }
            # endregion Loops
            #region Switch
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.SwitchStatement))
            {
                var switchStmtNode = this._view.CreateAndAddNode<SwitchStmtNode>(nodePresenter);
                visualNode = switchStmtNode;
                var switchStmt = (stmtArg as SwitchStatement);
                var exprInput = switchStmtNode.CreateAndAddInput<DataFlowAnchor>();
                exprInput.SetName(switchStmt.Expression.ToString());
                _generateVisualASTExpressions(switchStmt.Expression, exprInput, (e) => { switchStmt.Expression = e; });
                foreach (var switchSection in switchStmt.SwitchSections)
                {
                    foreach (var caseLabel in switchSection.CaseLabels)
                    {
                        var caseInput = switchStmtNode.CreateAndAddInput<DataFlowAnchor>();
                        caseInput.SetName(caseLabel.Expression.ToString());
                        _generateVisualASTExpressions(caseLabel.Expression, caseInput, (e) => { caseLabel.Expression = e; });
                    }
                    foreach (var switchSectionStmt in switchSection.Statements) // TODO @Seb @Mo something is wrong here
                    {
                        var caseOutput = switchStmtNode.CreateAndAddOutput<FlowNodeAnchor>();
                        caseOutput.SetName("CaseOut");
                        _generateVisualASTStatements(switchSectionStmt, caseOutput, null, null); // TODO @z0rg
                    }
                }
                defaultFlowOut = switchStmtNode.FlowOutAnchor;
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
                defaultFlowOut = variableNode.FlowOutAnchor;
                foreach (var v in varStmt.Variables)
                {
                    var item = variableNode.CreateAndAddOutput<DataFlowAnchor>();
                    var inputValue = variableNode.CreateAndAddInput<DataFlowAnchor>(); // @Seb: The value assigned to the variable
                    item.SetName(v.Name);
                    inputValue.SetName(v.Name);
                    _generateVisualASTExpressions(v.Initializer, inputValue, (e) => { v.Initializer = e; });
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
                defaultFlowOut = exprStmtNode.FlowOutAnchor;
                this._generateVisualASTExpressions(exprStmt.Expression, exprStmtNode.Expression, (e) => { exprStmt.Expression = e; });
            }
            #endregion ExpressionStatement
            #region Return Statement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ReturnStatement))
            {
                var returnStmt = stmtArg as ExpressionStatement;
                var returnStmtNode = this._view.CreateAndAddNode<ReturnStmtNode>(nodePresenter);
                visualNode = returnStmtNode;

                if (returnStmt != null)
                    this._generateVisualASTExpressions(returnStmt.Expression, returnStmtNode.ExprIn, (e) => { returnStmt.Expression = e; });
            }
            #endregion Return Statement
            #region Break Statement
            else if(stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.BreakStatement))
            {
                var breakStmt = stmtArg as BreakStatement;
                var breakStmtNode = this._view.CreateAndAddNode<BreakStmtNode>(nodePresenter);
                visualNode = breakStmtNode;
            }
            #endregion Break Statement
            #region Continue Statement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ContinueStatement))
            {
                var continueStmt = stmtArg as ContinueStatement;
                var continueStmtNode = this._view.CreateAndAddNode<ContinueStmtNode>(nodePresenter);
                visualNode = continueStmtNode;
            }
            #endregion ContinueStatement
            #endregion Single Statement
            else // Default Node
            {
                var unSupStmt = this._view.CreateAndAddNode<UnSupStmtDeclNode>(nodePresenter);
                visualNode = unSupStmt;
                unSupStmt.NodeText.Text = stmtArg.ToString();
                defaultFlowOut = unSupStmt.FlowOutAnchor;
            }

            if (visualNode != null)
                _createVisualLink(lastOutput, visualNode.FlowInAnchor);
            lastOutput.MethodAttachASTStmt = MethodAttachSTMT;
            lastOutput.MethodDetachASTStmt = MethodDetachStmt;
            return defaultFlowOut;
        }
        private void _generateVisualASTExpressions(ICSharpCode.NRefactory.CSharp.Expression expr, DataFlowAnchor inAnchor, Action<ICSharpCode.NRefactory.CSharp.Expression> methodAttachIOToASTField)
        {
            if (expr.IsNull)
                return;
            AValueNode visualNode = null;
            var nodePresenter = new NodePresenter(this, expr);
            if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ArrayInitializerExpression))
            {
                var arrayInitExpr = expr as ArrayInitializerExpression;
                var arrayInitNode = this._view.CreateAndAddNode<ArrayInitExprNode>(nodePresenter);
                visualNode = arrayInitNode;
                int idx = 0;
                foreach (var elem in arrayInitExpr.Elements)
                {
                    var dataIn = arrayInitNode.CreateAndAddInput<DataFlowAnchor>();
                    this._generateVisualASTExpressions(elem, dataIn, (e) => {  }); // TODO @z0rg callback to link expression
                    idx++;
                }
            }
            #region UnaryOperator
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression))
            {
                var unaryExprOp = expr as ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression;
                var unaryExprNode = this._view.CreateAndAddNode<UnaryExprNode>(nodePresenter);
                visualNode = unaryExprNode;
                unaryExprNode.SetName(unaryExprOp.OperatorToken.ToString());
                this._generateVisualASTExpressions(unaryExprOp.Expression, unaryExprNode.OperandA, (e) => { unaryExprOp.Expression = e; });
            }
            #endregion UnaryOperator
            #region Parenthesis Expr
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ParenthesizedExpression))
            {
                var parenthesizedExpr = expr as ICSharpCode.NRefactory.CSharp.ParenthesizedExpression;
                var parenthesizedExprNode = this._view.CreateAndAddNode<ParenthesizedExprNode>(nodePresenter);
                visualNode = parenthesizedExprNode;
                parenthesizedExprNode.SetName(parenthesizedExpr.ToString());
                this._generateVisualASTExpressions(parenthesizedExpr.Expression, parenthesizedExprNode.OperandA, (e) => { parenthesizedExpr.Expression = e; });
                                
            }
            #endregion Parenthesis Expr
            #region ArrayCreation
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ArrayCreateExpression))
            {
                var arrCreateExpr = expr as ICSharpCode.NRefactory.CSharp.ArrayCreateExpression;
                var arrCreateExprNode = this._view.CreateAndAddNode<ArrayCreateExprNode>(nodePresenter);
                visualNode = arrCreateExprNode;
                foreach (var i in arrCreateExpr.Arguments)
                {
                    var arrSize = arrCreateExprNode.CreateAndAddInput<DataFlowAnchor>();
                    arrSize.SetName(i.ToString());
                    _generateVisualASTExpressions(i, arrSize, (e) => { }); // TODO @z0rg callback
                }
                _generateVisualASTExpressions(arrCreateExpr.Initializer, arrCreateExprNode.ExprIn, (e) => { }); // TODO @z0rg callback
            }
            #endregion ArrayCreation
            #region ObjectCreation
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ObjectCreateExpression))
            {
                var objCreateExpr = expr as ICSharpCode.NRefactory.CSharp.ObjectCreateExpression;
                var objCreateExprNode = this._view.CreateAndAddNode<FuncCallExprNode>(nodePresenter); // TODO Create a node for that
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
                    this._generateVisualASTExpressions(param, arg, (e) =>
                    {
                        if (e == param)
                            return;
                        if (e != null)
                            objCreateExpr.Arguments.InsertAfter(param, e);
                        objCreateExpr.Arguments.Remove(param);
                    });
                }

            }
            #endregion ObjectCreation
            #region Identifier
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.IdentifierExpression))
            {
                var identExpr = expr as ICSharpCode.NRefactory.CSharp.IdentifierExpression;
                var identExprNode = this._view.CreateAndAddNode<IdentifierExprNode>(nodePresenter);
                visualNode = identExprNode;
                identExprNode.ExprOut.SetName(identExpr.Identifier);
            }
            #endregion Identifier
            #region Assignement
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.AssignmentExpression))
            {
                var assignExpr = expr as ICSharpCode.NRefactory.CSharp.AssignmentExpression;
                var assignExprNode = this._view.CreateAndAddNode<BinaryExprNode>(nodePresenter);
                visualNode = assignExprNode;
                assignExprNode.SetType("AssignExpr");

                this._generateVisualASTExpressions(assignExpr.Left, assignExprNode.OperandA, (e) => { assignExpr.Left = e; });
                this._generateVisualASTExpressions(assignExpr.Right, assignExprNode.OperandB, (e) => { assignExpr.Right = e; });
            }
            #endregion Assignement
            #region binaryOperator
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression))
            {
                var binaryExpr = expr as ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression;
                var binaryExprNode = this._view.CreateAndAddNode<BinaryExprNode>(nodePresenter);
                visualNode = binaryExprNode;
                binaryExprNode.SetName(binaryExpr.Operator.ToString());

                this._generateVisualASTExpressions(binaryExpr.Left, binaryExprNode.OperandA, (e) => { binaryExpr.Left = e; });
                this._generateVisualASTExpressions(binaryExpr.Right, binaryExprNode.OperandB, (e) => { binaryExpr.Right = e; });

            }
            #endregion binaryOperator
            #region MemberReference
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MemberReferenceExpression))
            {
                var memberRefExpr = expr as ICSharpCode.NRefactory.CSharp.MemberReferenceExpression;
                var memberRefExprNode = this._view.CreateAndAddNode<FuncCallExprNode>(nodePresenter); // TODO Create a node for that
                visualNode = memberRefExprNode;
                var inputTarget = memberRefExprNode.CreateAndAddInput<DataFlowAnchor>();
                this._generateVisualASTExpressions(memberRefExpr.Target, null, null);
            }
            #endregion MemberReference
            #region Primitive
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.PrimitiveExpression))
            {
                var primExpr = expr as ICSharpCode.NRefactory.CSharp.PrimitiveExpression;
                var primExprNode = this._view.CreateAndAddNode<PrimaryExprNode>(nodePresenter); // TODO Create a node for that
                visualNode = primExprNode;
                primExprNode.ExprOut.SetName(primExpr.LiteralValue);
            }
            #endregion Primitive
            #region Invocative
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.InvocationExpression))
            {
                var invokExpr = expr as ICSharpCode.NRefactory.CSharp.InvocationExpression;
                var invokExprNode = this._view.CreateAndAddNode<FuncCallExprNode>(nodePresenter);
                visualNode = invokExprNode;

                this._generateVisualASTExpressions(invokExpr.Target, invokExprNode.TargetIn, (e) => { invokExpr.Target = e; });
                // TODO @Seb @Mo display for generic parameters in FuncCallExprNode
                int i = 0;
                foreach (var param in invokExpr.Arguments)
                {
                    var paramMeth = invokExprNode.CreateAndAddInput<DataFlowAnchor>();
                    paramMeth.SetName("param" + i);
                    this._generateVisualASTExpressions(param, paramMeth, (e) =>
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
            else
            {
                var defaultUnsupportedNode = this._view.CreateAndAddNode<UnSupExpNode>(nodePresenter);
                visualNode = defaultUnsupportedNode;
                defaultUnsupportedNode.NodeText.Text = expr.ToString();
            }
            if (visualNode != null && inAnchor != null)
            {
                inAnchor.SetASTNodeReference(methodAttachIOToASTField);
                _createVisualLink(inAnchor, visualNode.ExprOut);
            }
        }

        public NodalModel ParseFile(String path)
        {
            StreamReader fileStream = new StreamReader(path);
            var ast = _parser.Parse(fileStream);
            NodalModel model = new NodalModel(ast);
            return model;
        }

        public void SaveFile(String dirPath)
        {
            string filePath = dirPath + "\\TestExportFile.cs";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath))
            {
                String code = "// Generated by Visual Studio's Code_in.";
                sw.AutoFlush = true;
                sw.WriteLine(code);
                sw.Write(_model.AST.ToString());
                sw.Close();
            }
        }

        private void _createVisualLink(AIOAnchor a, AIOAnchor b)
        {
            if (a == null || b == null)
                return;
            _view.DragLink(a, true);
            _view.DropLink(b, true);
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
            ContextMenu cm = new ContextMenu();
            UIElement view = (objects[0] as NodalPresenterLocal)._view as UIElement;
            cm.Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse;

            //var listOfBs = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
            //                from assemblyType in domainAssembly.GetTypes()
            //                where typeof(BaseNode).IsAssignableFrom(assemblyType)
            //                select assemblyType).ToArray();

            var listOfBs = (objects[0] as NodalPresenterLocal).GetAvailableNodes();

            foreach (var entry in listOfBs)
            {
                MenuItem mi = new MenuItem();
                mi.Header = entry.Name;
                mi.Click += mi_Click;
                mi.DataContext = entry;
                cm.Items.Add(mi);
            }
            cm.IsOpen = true;
            _viewStatic = ((objects[0] as NodalPresenterLocal)._view) as NodalView;
        }

        static void mi_Click(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).DataContext != null)
            {
//                (((MenuItem)sender).DataContext as NodalPresenterLocal)._view.CreateAndAddNode<_nodeCreationType>();
                MethodInfo mi = _viewStatic.GetType().GetMethod("CreateAndAddNode");
                MethodInfo gmi = mi.MakeGenericMethod(((MenuItem)sender).DataContext as Type);
                var nodePresenter = new NodePresenter(_viewStatic._nodalPresenter, null);
                var array = new object[1];
                array[0] = nodePresenter;
                BaseNode node = gmi.Invoke(_viewStatic, array) as BaseNode;
                try
                {
                    node.InstantiateASTNode();
                }
                catch (Exception fail)
                {
                    MessageBox.Show("You will not be able to modify this node's content with the edit menu.");
                }
                var pos = Mouse.GetPosition(_viewStatic.MainGrid);
                node.SetPosition((int)pos.X, (int)pos.Y);
                if (_viewStatic.IsDeclarative)
                {
                    var astNode = node.GetNodePresenter().GetASTNode();
                    if (astNode != null)
                    {
                        var thisAst = (_viewStatic._nodalPresenter as NodalPresenterLocal)._model;
                        thisAst.AST.Members.Add(astNode);                            
                    }
                }
            }
            //_viewStatic = null;
        }
        static NodalView _viewStatic = null;

        private static Action EmptyDelegate = delegate() { };
        static void AlignNode(object[] objects)
        {
            for (int i = 0; i < 100; ++i)
            {
                Thread.Sleep(10);
                (objects[0] as NodalPresenterLocal)._view.AlignNodes(0.1);
                ((UIElement)(objects[0] as NodalPresenterLocal)._view).Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
            }
            //MessageBox.Show(objects[0].GetType().ToString());
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
            MessageBox.Show("Saving file to => " + Environment.CurrentDirectory);
            System.Diagnostics.Debug.Assert(objects != null);
            System.Diagnostics.Debug.Assert(objects[0] != null);
            NodalPresenterLocal self = objects[0] as NodalPresenterLocal;
            self.SaveFile(Environment.CurrentDirectory);
            //            MessageBox.Show(Environment.CurrentDirectory);
        }



        public List<Type> GetAvailableNodes()
        {
            List<Type> tmp = new List<Type>();

            tmp.Add(typeof(ClassDeclNode));
            tmp.Add(typeof(NamespaceNode));
            tmp.Add(typeof(UsingDeclNode));
            if (false)
            {
                //TODO zorg
            }
            return (tmp);
        }
    }
}