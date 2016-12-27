using code_in.Models.NodalModel;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView;
using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Expressions;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Block;
using code_in.Views.NodalView.NodesElems.Tiles;
using code_in.Views.NodalView.NodesElems.Tiles.Items;
using code_in.Views.NodalView.NodesElems.Tiles.Statements;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
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
        static private NodalModel _parentModel = null;
        private CSharpParser _parser = null;

        public NodalPresenterLocal(INodalView view)
        {
            System.Diagnostics.Debug.Assert(view != null);
            _view = view;
            _parser = new CSharpParser();
        }

        public void OpenFile(String path)
        {
            _model = this.ParseFile(path);
            _parentModel = _model;
            this._generateVisualAST(_model);
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
        private void _generateVisualAST(NodalModel model)
        {
            this._generateVisualASTDeclaration(model.AST, this._view);
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

        private void InitAttributes(IContainingAttribute view, TypeDeclaration typedecl)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            foreach (ICSharpCode.NRefactory.CSharp.AttributeSection section in typedecl.Attributes)
            {
                int i = 0;
                while (i < section.Attributes.Count)
                {
                    KeyValuePair<string, string> newElem = new KeyValuePair<string, string>("", "");
                    ICSharpCode.NRefactory.CSharp.Attribute attr = section.Attributes.ElementAt(i);
                    if (attr.Type != null && attr.Arguments.Count > 0)
                        newElem = new KeyValuePair<string, string>(attr.Type.ToString(), attr.Arguments.ElementAt(0).ToString());
                    else if (attr.Type != null && attr.Arguments.Count == 0)
                        newElem = new KeyValuePair<string, string>(attr.Type.ToString(), "");
                    else if (attr.Type == null && attr.Arguments.Count > 0)
                        newElem = new KeyValuePair<string, string>("", attr.Arguments.ElementAt(0).ToString());
                    else
                        newElem = new KeyValuePair<string, string>("", attr.Arguments.ElementAt(0).ToString());
                    list.Add(newElem);
                    ++i;
                }
            }
            view.setExistingAttributes(list);
        }
        private void _generateVisualASTDeclarationRecur(AstNode node, IVisualNodeContainer parentContainer, ref int posX, ref int posY, UsingDeclNode usingDeclNode)
        {
            //foreach ()
        }
        private void _alignDeclarationsRecur()
        {

        }
        private void _generateVisualASTDeclaration(AstNode node, IVisualNodeContainer parentContainer)
        {
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.SyntaxTree))
            {
                var usingNodePresenter = new NodePresenter(this, null); // This node presenter does not reflect a node in the AST as our representation is different than the AST
                var usingNodeView = parentContainer.CreateAndAddNode<UsingDeclNode>(usingNodePresenter); // We have one using view for the global scope

                var syntaxTree = node as ICSharpCode.NRefactory.CSharp.SyntaxTree;
                foreach (var decl in syntaxTree.Members)
                    this._generateVisualASTDeclarationRecur(decl, parentContainer, usingNodeView);
                if (usingNodeView._orderedLayout.Children.Count == 0) // If there is no using at all we remove it
                    usingNodeView.Remove();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void _generateVisualASTDeclarationRecur(AstNode node, IVisualNodeContainer parentContainer, UsingDeclNode parentUsingDeclNode)
        {
            INodeElem visualNode = null;
            var nodePresenter = new NodePresenter(this, node);
            if (node.Children == null)
                return;

            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration))
            {
                NamespaceNode namespaceNode = parentContainer.CreateAndAddNode<NamespaceNode>(nodePresenter);
                var namespaceDecl = node as ICSharpCode.NRefactory.CSharp.NamespaceDeclaration;
                var usingNodePres = new NodePresenter(this, null);
                UsingDeclNode usingDeclView = namespaceNode.CreateAndAddNode<UsingDeclNode>(usingNodePres);

                visualNode = namespaceNode;
                namespaceNode.SetName(namespaceDecl.Name);
                foreach (var member in namespaceDecl.Members)
                    this._generateVisualASTDeclarationRecur(member, namespaceNode, usingDeclView);
                if (usingDeclView._orderedLayout.Children.Count == 0) // TODO do better than this
                    usingDeclView.Remove();
            }
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.UsingDeclaration))
            {
                UsingDeclItem usingItem = null;
                visualNode = usingItem = parentUsingDeclNode.CreateAndAddNode<UsingDeclItem>(nodePresenter);
                usingItem.SetName(nodePresenter.GetASTNode().ToString());
            }
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
                    //Attributes
                    InitAttributes(classDeclNode, tmpNode);
                    //Constraint
                    foreach (var constraint in tmpNode.Constraints)
                    {
                        classDeclNode.setConstraint(constraint.TypeParameter.ToString(), constraint.BaseTypes);
                    }
                    foreach (var member in tmpNode.Members)
                        _generateVisualASTDeclarationRecur(member, classDeclNode, null); // No using in typeDecl
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
                        _generateVisualASTDeclarationRecur(member, interfaceDeclNode, null); // No using in interface
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
                var item = parentContainer.CreateAndAddNode<PropertyItem>(nodePresenter);
                visualNode = item;
                item.PropertyNode = propertyDecl;
                item.SetName(propertyDecl.Name.ToString()); // TODO Complete
                item.setTypeFromString(propertyDecl.ReturnType.ToString());
                setAccessModifiers(item, propertyDecl.Modifiers);
            }
            #endregion Property (get, set)
            #region Constructor
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ConstructorDeclaration))
            {
                ConstructorItem constructorDecl = parentContainer.CreateAndAddNode<ConstructorItem>(nodePresenter);
                visualNode = constructorDecl;
                constructorDecl.ConstructorNode = node as ConstructorDeclaration;
                ConstructorDeclaration construct = node as ConstructorDeclaration;
                constructorDecl.SetName(construct.Name);
                setAccessModifiers(constructorDecl, construct.Modifiers);

                var parameters = construct.Parameters.ToList();
                for (int i = 0; i < parameters.Count; i++)
                {
                    constructorDecl.AddParam(parameters[i].Type.ToString());
                }
            }
            #endregion Constructor
            #region Destructor
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.DestructorDeclaration))
            {
                DestructorItem constructorDecl = parentContainer.CreateAndAddNode<DestructorItem>(nodePresenter);
                visualNode = constructorDecl;
                constructorDecl.DestructorNode = node as DestructorDeclaration;
                DestructorDeclaration construct = node as DestructorDeclaration;
                constructorDecl.SetName(construct.Name);
                setAccessModifiers(constructorDecl, construct.Modifiers);

            }
            #endregion Destructor
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
                funcDecl.UpdateDisplayedInfosFromPresenter();

                //foreach (var constraint in method.Constraints)
                //{
                //    funcDecl.setConstraint(constraint.TypeParameter.ToString(), constraint.BaseTypes);
                //}
            }
            #endregion Method
        }

        private void _generateVisualASTFunctionBody(MethodDeclaration method)
        {
            (this._view as NodalView).IsDeclarative = false;
            (this._view.RootTileContainer as UserControl).Margin = new Thickness(100, 100, 0, 0);
            this._generateVisualASTStatements(this._view.RootTileContainer, method.Body);
        }
        //TODO @YAYA
        private void _generateVisualASTConstructorBody(ConstructorDeclaration constructor)
        {
            (this._view as NodalView).IsDeclarative = false;
            (this._view.RootTileContainer as UserControl).Margin = new Thickness(100, 100, 0, 0);
            _generateVisualASTStatements(this._view.RootTileContainer, constructor.Body);
        }
        private void _generateVisualASTDestructorBody(DestructorDeclaration destructor)
        {
            (this._view as NodalView).IsDeclarative = false;
            (this._view.RootTileContainer as UserControl).Margin = new Thickness(100, 100, 0, 0);
            _generateVisualASTStatements(this._view.RootTileContainer, destructor.Body);
        }
        private void _generateVisualASTPropertyBody(Accessor access)
        {
            (this._view as NodalView).IsDeclarative = false;
            (this._view.RootTileContainer as UserControl).Margin = new Thickness(100, 100, 0, 0);
            _generateVisualASTStatements(this._view.RootTileContainer, access.Body);

        }

        /// <summary>
        /// This function displays the execution code from stmtArg to the NodalView attached.
        /// </summary>
        /// <param name="stmtArg"></param>
        private void _generateVisualASTStatements(ITileContainer tileContainer, Statement stmtArg)
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
                this._generateVisualASTExpressions(ifTile.Condition, ifStmt.Condition, ifTile.Condition.ExprOut, (e) => { ifStmt.Condition = e; });

                this._generateVisualASTStatements(ifTile.ItemTrue, ifStmt.TrueStatement);
                this._generateVisualASTStatements(ifTile.ItemFalse,  ifStmt.FalseStatement);
            }
            # endregion IfStmts
            # region tryCatchStmt
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.TryCatchStatement))
            {
                var tryStmt = stmtArg as TryCatchStatement;
                var tryTile = tileContainer.CreateAndAddTile<TryCatchStmtTile>(nodePresenter);

                this._generateVisualASTStatements(tryTile.ItemTry, tryStmt.TryBlock);
                foreach (var blockCatch in tryStmt.CatchClauses) {
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

                this._generateVisualASTExpressions(doWhileStmtTile.Condition, doWhileStmt.Condition, doWhileStmtTile.Condition.ExprOut, (e) => { doWhileStmt.Condition = e; });
                this._generateVisualASTStatements(doWhileStmtTile.trueItem, doWhileStmt.EmbeddedStatement);
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.WhileStatement))
            {
                var whileStmt = stmtArg as WhileStatement; // AST Node
                var whileStmtTile = tileContainer.CreateAndAddTile<WhileStmtTile>(nodePresenter); // Visual Node

                this._generateVisualASTExpressions(whileStmtTile.Condition, whileStmt.Condition, whileStmtTile.Condition.ExprOut, (e) => { whileStmt.Condition = e; });
                this._generateVisualASTStatements(whileStmtTile.trueItem, whileStmt.EmbeddedStatement);
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForStatement))
            {
                var forStmt = stmtArg as ForStatement; // AST Node
                var forStmtTile = tileContainer.CreateAndAddTile<ForStmtTile>(nodePresenter); // Visual Node

                this._generateVisualASTExpressions(forStmtTile.Condition, forStmt.Condition, forStmtTile.Condition.ExprOut,  (e) => { forStmt.Condition = e; });
                this._generateVisualASTStatements(forStmtTile.trueItem, forStmt.EmbeddedStatement);
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForeachStatement))
            {
                var forEachStmt = stmtArg as ForeachStatement;
                var forEachStmtTile = tileContainer.CreateAndAddTile<ForEachStmtTile>(nodePresenter);
                
                this._generateVisualASTExpressions(forEachStmtTile.Condition, forEachStmt.InExpression, forEachStmtTile.Condition.ExprOut, (e) => { forEachStmt.InExpression = e; });
                this._generateVisualASTStatements(forEachStmtTile.trueItem, forEachStmt.EmbeddedStatement);
            }
            # endregion Loops
            #region Switch
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.SwitchStatement))
            {
                var switchStmt = stmtArg as SwitchStatement;
                var switchStmtTile = tileContainer.CreateAndAddTile<SwitchStmtTile>(nodePresenter);
                this._generateVisualASTExpressions(switchStmtTile.Expression, switchStmt.Expression, switchStmtTile.Expression.ExprOut, (e) => {switchStmt.Expression = e;} );
                foreach (var caseBlock in switchStmt.SwitchSections)
                {
                    foreach (var caseLabel in caseBlock.CaseLabels) {
                        var itemExpr = switchStmtTile.CreateAndAddItem<ExpressionItem>();
                        switchStmtTile.ExpressionCases.Add(itemExpr);
                        this._generateVisualASTExpressions(itemExpr, caseLabel.Expression, itemExpr.ExprOut, (e) => { caseLabel.Expression = e; });
                    }
                    foreach (var caseBlockStmt in caseBlock.Statements) {
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
                    _generateVisualASTExpressions(exprItem, v.Initializer, exprItem.ExprOut, (e) => { v.Initializer = e; });
                }
                //varDeclStmtTile.UpdateDisplayedInfosFromPresenter();
            }
            #endregion Variable Declaration
            #region ExpressionStatement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ExpressionStatement))
            {
                var exprStmt = stmtArg as ExpressionStatement;
                var exprStmtTile = tileContainer.CreateAndAddTile<ExprStmtTile>(nodePresenter);
                //exprStmtTile.Expression.SetName(exprStmt.ToString().Replace(System.Environment.NewLine, "")); // TODO @Seb Make this be done automatically by creating CreateAnddAddTile (see presenters...)
                this._generateVisualASTExpressions(exprStmtTile.Expression, exprStmt.Expression, exprStmtTile.Expression.ExprOut, (e) => { exprStmt.Expression = e; });
                //exprStmtTile.UpdateDisplayedInfosFromPresenter();

            }
            #endregion ExpressionStatement
            #region Return Statement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ReturnStatement))
            {
                var returnStmt = stmtArg as ReturnStatement;
                var returnStmtTile = tileContainer.CreateAndAddTile<ReturnStmtTile>(nodePresenter);
                this._generateVisualASTExpressions(returnStmtTile.Expression, returnStmt.Expression, returnStmtTile.Expression.ExprOut, (e) => { returnStmt.Expression = e; });
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
                this._generateVisualASTExpressions(yieldReturnStmtTile.Expression, yieldReturnStmt.Expression, yieldReturnStmtTile.Expression.ExprOut, (e) => { yieldReturnStmt.Expression = e; });
            }
            #endregion YieldReturn Statement
            #region YieldBreak Statement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.YieldBreakStatement))
                tileContainer.CreateAndAddTile<YieldBreakStmtTile>(nodePresenter); // Visual Node
            #endregion YieldBreak Statement
            #region Continue Statement
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ContinueStatement)) {
                tileContainer.CreateAndAddTile<ContinueStmtTile>(nodePresenter);
            }
            #endregion ContinueStatement
            #region Throw
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ThrowStatement))
            {
                var throwStmt = stmtArg as ThrowStatement; // AST Node
                var throwStmtTile = tileContainer.CreateAndAddTile<ThrowStmtTile>(nodePresenter); // Visual Node
                this._generateVisualASTExpressions(throwStmtTile.Expression, throwStmt.Expression, throwStmtTile.Expression.ExprOut, (e) => { throwStmt.Expression = e; });
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
        private void _generateVisualASTExpressions(IVisualNodeContainer container, ICSharpCode.NRefactory.CSharp.Expression expr, DataFlowAnchor inAnchor, Action<ICSharpCode.NRefactory.CSharp.Expression> methodAttachIOToASTField)
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
                    this._generateVisualASTExpressions(container, elem, dataIn, (e) => { }); // TODO @z0rg callback to link expression
                    idx++;
                }
            }
            #region Indexer
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.IndexerExpression))
            {
                var invokExpr = expr as ICSharpCode.NRefactory.CSharp.IndexerExpression;
                var invokExprNode = container.CreateAndAddNode<IndexerExprNode>(nodePresenter);
                visualNode = invokExprNode;

                this._generateVisualASTExpressions(container, invokExpr.Target, invokExprNode.Target, (e) => { invokExpr.Target = e; });
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
                this._generateVisualASTExpressions(container, unaryExprOp.Expression, unaryExprNode.OperandA, (e) => { unaryExprOp.Expression = e; });
            }
            #endregion UnaryOperator
            #region Parenthesis Expr
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ParenthesizedExpression))
            {
                var parenthesizedExpr = expr as ICSharpCode.NRefactory.CSharp.ParenthesizedExpression;
                var parenthesizedExprNode = container.CreateAndAddNode<ParenthesizedExprNode>(nodePresenter);
                visualNode = parenthesizedExprNode;
                parenthesizedExprNode.SetName(parenthesizedExpr.ToString());
                this._generateVisualASTExpressions(container, parenthesizedExpr.Expression, parenthesizedExprNode.OperandA, (e) => { parenthesizedExpr.Expression = e; });           
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
                    _generateVisualASTExpressions(container, i, arrSize, (e) => { }); // TODO @z0rg callback
                }
                _generateVisualASTExpressions(container, arrCreateExpr.Initializer, arrCreateExprNode.ExprIn, (e) => { }); // TODO @z0rg callback
            }
            #endregion ArrayCreation
            #region ObjectCreation
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ObjectCreateExpression))
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
                    this._generateVisualASTExpressions(container, param, arg, (e) =>
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
                var identExprNode = container.CreateAndAddNode<IdentifierExprNode>(nodePresenter);
                visualNode = identExprNode;
                identExprNode.ExprOut.SetName(identExpr.Identifier);
            }
            #endregion Identifier
            #region Assignement
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.AssignmentExpression))
            {
                var assignExpr = expr as ICSharpCode.NRefactory.CSharp.AssignmentExpression;
                var assignExprNode = container.CreateAndAddNode<BinaryExprNode>(nodePresenter);
                visualNode = assignExprNode;
                assignExprNode.SetType("AssignExpr");

                this._generateVisualASTExpressions(container, assignExpr.Left, assignExprNode.OperandA, (e) => { assignExpr.Left = e; });
                this._generateVisualASTExpressions(container, assignExpr.Right, assignExprNode.OperandB, (e) => { assignExpr.Right = e; });
            }
            #endregion Assignement
            #region BinaryOperator
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression))
            {
                var binaryExpr = expr as ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression;
                var binaryExprNode = container.CreateAndAddNode<BinaryExprNode>(nodePresenter);
                visualNode = binaryExprNode;
                binaryExprNode.SetName(binaryExpr.Operator.ToString());

                this._generateVisualASTExpressions(container, binaryExpr.Left, binaryExprNode.OperandA, (e) => { binaryExpr.Left = e; });
                this._generateVisualASTExpressions(container, binaryExpr.Right, binaryExprNode.OperandB, (e) => { binaryExpr.Right = e; });

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
                var memberRefExprNode = container.CreateAndAddNode<FuncCallExprNode>(nodePresenter); // TODO Create a node for that
                visualNode = memberRefExprNode;
                var inputTarget = memberRefExprNode.CreateAndAddInput<DataFlowAnchor>();
                this._generateVisualASTExpressions(container, memberRefExpr.Target, null, null);
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
                var primExprNode = container.CreateAndAddNode<PrimaryExprNode>(nodePresenter); // TODO Create a node for that
                visualNode = primExprNode;
                primExprNode.ExprOut.SetName(primExpr.LiteralValue);
            }
            #endregion Primitive
            #region Invocative
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.InvocationExpression))
            {
                var invokExpr = expr as ICSharpCode.NRefactory.CSharp.InvocationExpression;
                var invokExprNode = container.CreateAndAddNode<FuncCallExprNode>(nodePresenter);
                visualNode = invokExprNode;

                this._generateVisualASTExpressions(container, invokExpr.Target, invokExprNode.TargetIn, (e) => { invokExpr.Target = e; });
                // TODO @Seb @Mo display for generic parameters in FuncCallExprNode
                int i = 0;
                foreach (var param in invokExpr.Arguments)
                {
                    var paramMeth = invokExprNode.CreateAndAddInput<DataFlowAnchor>();
                    paramMeth.SetName("param" + i);
                    this._generateVisualASTExpressions(container, param, paramMeth, (e) =>
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
              //  this._generateVisualASTExpressions(container, sizeofExpr, sizeofExprNode.Input, null);
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
            if (visualNode != null && inAnchor != null)
            {
                inAnchor.SetASTNodeReference(methodAttachIOToASTField);
                _createVisualLink(container as dynamic, inAnchor, visualNode.ExprOut); // TODO cast Beuark
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
                if (_model == null)
                    sw.Write(_parentModel.AST.ToString());
                else
                    sw.Write(_model.AST.ToString());
                sw.Close();
            }
        }

        private void _createVisualLink(ILinkContainer parent, AIOAnchor a, AIOAnchor b)
        {
            if (a == null || b == null)
                return;
            parent.DragLink(a, true);
            parent.DropLink(b, true);
        }

        Tuple<EContextMenuOptions, Action<object[]>>[] IContextMenu.GetMenuOptions()
        {
            return new Tuple<EContextMenuOptions, Action<object[]>>[] { 
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ADD, AddNode), 
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ALIGN, _alignNodes), 
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.COLLAPSEALL, CollapseAllNode),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.SAVE, Save),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.CLOSE, CloseNode),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.HELP, HelpNode)
            };
        }



        static void _alignNodes(object[] objects)
        {
            NodalPresenterLocal self = objects[0] as NodalPresenterLocal;
            self._view.AlignDeclarations();
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
                Dictionary<Type, code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode> types = new Dictionary<Type, code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode>();
                MethodInfo mi = _viewStatic.GetType().GetMethod("CreateAndAddNode");
                MethodInfo gmi = mi.MakeGenericMethod(((MenuItem)sender).DataContext as Type);

                types.Add(typeof(UsingDeclNode), code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode.USING_DECL); // TODO not sure
                types.Add(typeof(NamespaceNode), code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode.NAMESPACE_DECL);
                types.Add(typeof(ClassDeclNode), code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode.TYPE_DECL);
                var astNode = NodePresenter.InstantiateASTNode(types[((MenuItem)sender).DataContext as Type]);
                var nodePresenter = new NodePresenter(_viewStatic._nodalPresenter, astNode);
                var array = new object[1];
                array[0] = nodePresenter;
                BaseNode node = gmi.Invoke(_viewStatic, array) as BaseNode;
                var pos = Mouse.GetPosition(_viewStatic.MainGrid);
                node.SetPosition((int)pos.X, (int)pos.Y);
                if (_viewStatic.IsDeclarative)
                {
                    if (astNode != null)
                    {
                        var thisAst = (_viewStatic._nodalPresenter as NodalPresenterLocal)._model;
                        if (thisAst != null)
                            thisAst.AST.Members.Add(astNode);
                    }
                }
            }
            //_viewStatic = null;
        }
        static NodalView _viewStatic = null;

        private static Action EmptyDelegate = delegate() { };

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
        static void Save(object[] objects)
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