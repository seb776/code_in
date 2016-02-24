using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code_in.Views.MainView.Nodes;
using ICSharpCode.NRefactory.TypeSystem;

namespace code_in.ViewModels
{
    public class CodeMgr
    {
        private code_inMgr _code_inMgr;
        private Models.CodeData _codeData;

        public CodeMgr(code_inMgr ciMgr)
        {
            _codeData = new Models.CodeData();
            _code_inMgr = ciMgr;
        }
        private ICSharpCode.NRefactory.CSharp.SyntaxTree _parseFile(String filePath)
        {
            System.IO.StreamReader fileStream = new System.IO.StreamReader(filePath);
            return _codeData.Parser.Parse(fileStream);
        }

        private void _generateVisualAST(System.Windows.Controls.Grid mainGrid)
        {
            _generateVisualASTRecur(_codeData.AST, this._code_inMgr._mainView);
        }
        int offsetX = 0;
        int offsetY = 0;

        // (z0rg)TODO: Big refacto
        // - Create Node through mainview
        // - Improve code interface of nodes management
        // - Make this function more generic
        //      - avoid setting the name of each node depending of it's type, almost all nodes have a name
        //      - improve interface to make the parent totally transparent
        //      - ...
        // - Improve design to make the node alignement after the parsing
        void _generateVisualASTRecur(ICSharpCode.NRefactory.CSharp.AstNode node, Views.MainView.IVisualNodeContainer parentContainer)
        {
            //bool goDeeper = true;
            Views.MainView.Nodes.BaseNode visualNode = null;
            if (node.Children == null)
                return;
            #region Namespace
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration))
            {
                Views.MainView.Nodes.NamespaceNode namespaceNode = parentContainer.AddNode<Views.MainView.Nodes.NamespaceNode>();
                visualNode = namespaceNode;

                //namespaceNode.SetSize(400, 250);

                var tmpNode = (ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)node;
                namespaceNode.SetNodeName(tmpNode.Name);
            }
            #endregion
            #region Variable Declaration
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.VariableDeclarationStatement))
            {
                Views.MainView.Nodes.FuncDeclNode variableNode = parentContainer.AddNode<Views.MainView.Nodes.FuncDeclNode>();
                variableNode.SetNodeName("VarDecl");
                var tmpNode = (ICSharpCode.NRefactory.CSharp.VariableDeclarationStatement)node;
                foreach (var v in tmpNode.Variables)
                {
                    var item = new Views.MainView.Nodes.Items.DataFlowItem();
                    item.SetName(v.Name);
                    item.SetItemType(tmpNode.Type.ToString());
                    variableNode.AddInput(item);
                }
            }
            #endregion
            #region Type Declaration
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.TypeDeclaration)) // Handles class, struct, enum (see further)
            {
                var tmpNode = (ICSharpCode.NRefactory.CSharp.TypeDeclaration)node;
                #region Class
                if (tmpNode.ClassType == ICSharpCode.NRefactory.CSharp.ClassType.Class)
                {
                    Views.MainView.Nodes.ClassDeclNode classDeclNode = parentContainer.AddNode<Views.MainView.Nodes.ClassDeclNode>();
                    visualNode = classDeclNode;
                    classDeclNode.SetSize(400, 250);



                    classDeclNode.SetNodeName(tmpNode.Name);
                    // TODO protected internal
                    switch (tmpNode.Modifiers.ToString()) // Puts the right scope
                    {
                        case "Public":
                            classDeclNode.NodeScope.Scope = Views.MainView.Nodes.Items.ScopeItem.EScope.PUBLIC;
                            break;
                        case "Private":
                            classDeclNode.NodeScope.Scope = Views.MainView.Nodes.Items.ScopeItem.EScope.PRIVATE;
                            break;
                        case "Protected":
                            classDeclNode.NodeScope.Scope = Views.MainView.Nodes.Items.ScopeItem.EScope.PROTECTED;
                            break;
                        default:
                            break;
                    }
                    //goDeeper = false;
                    foreach (var n in node.Children)
                    {
                        if (n.GetType() == typeof(ICSharpCode.NRefactory.CSharp.FieldDeclaration))
                        {
                            ICSharpCode.NRefactory.CSharp.FieldDeclaration field = n as ICSharpCode.NRefactory.CSharp.FieldDeclaration;


                            var item = new code_in.Views.MainView.Nodes.Items.ClassItem(classDeclNode);
                            classDeclNode.AddInput(item);
                            item.SetName(field.Variables.FirstOrNullObject().Name);
                            item.SetItemType(field.ReturnType.ToString());
                            switch (field.Modifiers.ToString()) // Puts the right scope
                            {
                                case "Public":
                                    item.ItemScope.Scope = Views.MainView.Nodes.Items.ScopeItem.EScope.PUBLIC;
                                    break;
                                case "Private":
                                    item.ItemScope.Scope = Views.MainView.Nodes.Items.ScopeItem.EScope.PRIVATE;
                                    break;
                                case "Protected":
                                    item.ItemScope.Scope = Views.MainView.Nodes.Items.ScopeItem.EScope.PROTECTED;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                #endregion Class
                #region Enum
                if (tmpNode.ClassType == ICSharpCode.NRefactory.CSharp.ClassType.Enum)
                {
                    Views.MainView.Nodes.ClassDeclNode enumDeclNode = parentContainer.AddNode<Views.MainView.Nodes.ClassDeclNode>();
                    visualNode = enumDeclNode;
                    enumDeclNode.SetSize(400, 250);
                    enumDeclNode.SetNodeName("EnumDecl " + tmpNode.Name);

                    foreach (var v in tmpNode.Members)
                    {
                        var item = new Views.MainView.Nodes.Items.DataFlowItem();
                        item.SetName(v.Name);
                        enumDeclNode.AddInput(item);
                    }
                }
                #endregion
            }
            #endregion
            #region Method
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MethodDeclaration))
            {
                FuncDeclNode funcDecl = parentContainer.AddNode<FuncDeclNode>();
                visualNode = funcDecl;
                ICSharpCode.NRefactory.CSharp.MethodDeclaration method = node as ICSharpCode.NRefactory.CSharp.MethodDeclaration;

                var parameters = method.Parameters.ToList();
                for (int i = 0; i < parameters.Count; ++i)
                {
                    var item = new code_in.Views.MainView.Nodes.Items.DataFlowItem(funcDecl);
                    item.SetName(parameters[i].Name);
                    item.SetItemType(parameters[i].Type.ToString());
                    funcDecl.AddInput(item);
                }
                funcDecl.SetNodeName(method.Name);
            }
            #endregion
            #region Attribute
            #endregion
            //if (visualNode != null)
            //    parentContainer.AddNode(visualNode);
            //if (goDeeper)
            foreach (var n in node.Children) if (n.GetType() != typeof(ICSharpCode.NRefactory.CSharp.FieldDeclaration))
                    _generateVisualASTRecur(n, (visualNode != null ? visualNode : parentContainer));
        }

        public void LoadFile(String filePath, System.Windows.Controls.Grid mainGrid)
        {
            _codeData.AST = _parseFile(filePath);
            _generateVisualAST(mainGrid);
            // Do node placement:
            // - auto
            // - from file
        }

        public void SaveFile(String filePath)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath);
            String code = "// Generated by Visual Studio's Code_in.";
            sw.WriteLine(code);
            sw.Write(_codeData.AST.ToString());
        }
    }
}
