using code_in.Presenters.Nodal.Nodes;
using code_in.Views.MainView;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSCode_in_UnitTests.Core.UI
{
    /// <summary>
    /// TODO @Steph, check if the functions 
    /// </summary>
    [TestClass()]
    public class INodeElemImplem
    {
        [TestMethod()]
        public void classDeclNode()
        {
            //new MainView()
            try
            {
                var classDeclNode = new code_in.Views.NodalView.NodesElems.Nodes.ClassDeclNode(code_in.Code_inApplication.MainResourceDictionary);
                classDeclNode.InstantiateASTNode();
                classDeclNode.SetName("test");
                classDeclNode.GetName();
                classDeclNode.AddGeneric("test", EGenericVariance.IN);
                classDeclNode.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                classDeclNode.SetParentView(sb);
                classDeclNode.GetParentView();
                classDeclNode.SetNodePresenter(null);
                classDeclNode.ShowEditMenu();
                classDeclNode.SetPosition(42, 42);
                classDeclNode.GetPosition();
                int x;
                int y;
                classDeclNode.GetSize(out x, out y);
                classDeclNode.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void FuncDeclNode()
        {
            try
            {
                var funcDeclNode = new code_in.Views.NodalView.NodesElems.Nodes.FuncDeclNode(code_in.Code_inApplication.MainResourceDictionary);
                funcDeclNode.InstantiateASTNode();
                funcDeclNode.SetName("test");
                funcDeclNode.GetName();
                funcDeclNode.AddGeneric("test", EGenericVariance.IN);
                funcDeclNode.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                funcDeclNode.SetParentView(sb);
                funcDeclNode.GetParentView();
                funcDeclNode.SetNodePresenter(null);
                funcDeclNode.ShowEditMenu();
                funcDeclNode.SetPosition(42, 42);
                funcDeclNode.GetPosition();
                int x;
                int y;
                funcDeclNode.GetSize(out x, out y);
                funcDeclNode.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void NameSpaceNode()
        {
            try
            {
                var nameSpaceNode = new code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode(code_in.Code_inApplication.MainResourceDictionary);
                nameSpaceNode.InstantiateASTNode();
                nameSpaceNode.SetName("test");
                nameSpaceNode.GetName();
                nameSpaceNode.AddGeneric("test", EGenericVariance.IN);
                nameSpaceNode.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                nameSpaceNode.SetParentView(sb);
                nameSpaceNode.GetParentView();
                nameSpaceNode.SetNodePresenter(null);
                nameSpaceNode.ShowEditMenu();
                nameSpaceNode.SetPosition(42, 42);
                nameSpaceNode.GetPosition();
                int x;
                int y;
                nameSpaceNode.GetSize(out x, out y);
                nameSpaceNode.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void UsingDeclNode()
        {
            try
            {
                var usingDeclNode = new code_in.Views.NodalView.NodesElems.Nodes.UsingDeclNode(code_in.Code_inApplication.MainResourceDictionary);
                usingDeclNode.InstantiateASTNode();
                usingDeclNode.SetName("test");
                usingDeclNode.GetName();
                usingDeclNode.AddGeneric("test", EGenericVariance.IN);
                usingDeclNode.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                usingDeclNode.SetParentView(sb);
                usingDeclNode.GetParentView();
                usingDeclNode.SetNodePresenter(null);
                usingDeclNode.ShowEditMenu();
                usingDeclNode.SetPosition(42, 42);
                usingDeclNode.GetPosition();
                int x;
                int y;
                usingDeclNode.GetSize(out x, out y);
                usingDeclNode.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void ArrayCreateExprNode()
        {
            try
            {
                var arrayCreateExprNode = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.ArrayCreateExprNode(code_in.Code_inApplication.MainResourceDictionary);
                arrayCreateExprNode.InstantiateASTNode();
                arrayCreateExprNode.SetName("test");
                arrayCreateExprNode.GetName();
                arrayCreateExprNode.AddGeneric("test", EGenericVariance.IN);
                arrayCreateExprNode.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                arrayCreateExprNode.SetParentView(sb);
                arrayCreateExprNode.GetParentView();
                arrayCreateExprNode.SetNodePresenter(null);
                arrayCreateExprNode.ShowEditMenu();
                arrayCreateExprNode.SetPosition(42, 42);
                arrayCreateExprNode.GetPosition();
                int x;
                int y;
                arrayCreateExprNode.GetSize(out x, out y);
                arrayCreateExprNode.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void ArrayInitExprNode()
        {
            try
            {
                var arrayInitExprNode = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.ArrayInitExprNode(code_in.Code_inApplication.MainResourceDictionary);
                arrayInitExprNode.InstantiateASTNode();
                arrayInitExprNode.SetName("test");
                arrayInitExprNode.GetName();
                arrayInitExprNode.AddGeneric("test", EGenericVariance.IN);
                arrayInitExprNode.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                arrayInitExprNode.SetParentView(sb);
                arrayInitExprNode.GetParentView();
                arrayInitExprNode.SetNodePresenter(null);
                arrayInitExprNode.ShowEditMenu();
                arrayInitExprNode.SetPosition(42, 42);
                arrayInitExprNode.GetPosition();
                int x;
                int y;
                arrayInitExprNode.GetSize(out x, out y);
                arrayInitExprNode.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void BinaryExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.BinaryExprNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void FuncCallExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.FuncCallExprNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void IdentifierExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.IdentifierExprNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void ParenthesizedExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.ParenthesizedExprNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void PrimaryExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.PrimaryExprNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void TernaryExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.TernaryExprNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void UnaryExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.UnaryExprNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void UnSupExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.UnSupExpNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void IfStmtNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Statements.Block.IfStmtNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void SwitchStmtNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Statements.Block.SwitchStmtNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void WhileStmtNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Statements.Block.WhileStmtNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void BreakStmtNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Statements.Context.BreakStmtNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }

        [TestMethod()]
        public void ReturnStmtNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Statements.Context.ReturnStmtNode(code_in.Code_inApplication.MainResourceDictionary);
                node.InstantiateASTNode();
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                IVisualNodeContainerDragNDrop sb = new SearchBar();
                node.SetParentView(sb);
                node.GetParentView();
                node.SetNodePresenter(null);
                node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                node.Remove();
            }
            catch (NotImplementedException e)
            {
                throw new Exception();
            }
        }
    }
}
