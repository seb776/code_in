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
using System.Windows;

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
                var classDeclNode = new code_in.Views.NodalView.NodesElems.Nodes.ClassDeclNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                classDeclNode.SetName("test");
                classDeclNode.GetName();
                classDeclNode.AddGeneric("test", EGenericVariance.IN);
                classDeclNode.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new System.Windows.ResourceDictionary());
                classDeclNode.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void FuncDeclNode()
        {
            try
            {
                var funcDeclNode = new code_in.Views.NodalView.NodesElems.Items.FuncDeclItem(code_in.Code_inApplication.MainResourceDictionary, null, null);
                funcDeclNode.InstantiateASTNode();
                funcDeclNode.SetName("test");
                funcDeclNode.GetName();
                funcDeclNode.AddGeneric("test", EGenericVariance.IN);
                funcDeclNode.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                funcDeclNode.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void NameSpaceNode()
        {
            try
            {
                var nameSpaceNode = new code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                nameSpaceNode.SetName("test");
                nameSpaceNode.GetName();
                nameSpaceNode.AddGeneric("test", EGenericVariance.IN);
                SearchBar sb = new SearchBar(new ResourceDictionary());
                nameSpaceNode.SetParentView(null);
                nameSpaceNode.GetParentView();
                nameSpaceNode.SetNodePresenter(null);
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
                var usingDeclNode = new code_in.Views.NodalView.NodesElems.Nodes.UsingDeclNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                usingDeclNode.SetName("test");
                usingDeclNode.GetName();
                usingDeclNode.AddGeneric("test", EGenericVariance.IN);
                usingDeclNode.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                usingDeclNode.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void ArrayCreateExprNode()
        {
            try
            {
                var arrayCreateExprNode = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.ArrayCreateExprNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                arrayCreateExprNode.SetName("test");
                arrayCreateExprNode.GetName();
                arrayCreateExprNode.AddGeneric("test", EGenericVariance.IN);
                arrayCreateExprNode.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                arrayCreateExprNode.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void ArrayInitExprNode()
        {
            try
            {
                var arrayInitExprNode = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.ArrayInitExprNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                arrayInitExprNode.SetName("test");
                arrayInitExprNode.GetName();
                arrayInitExprNode.AddGeneric("test", EGenericVariance.IN);
                arrayInitExprNode.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                arrayInitExprNode.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void BinaryExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.BinaryExprNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void FuncCallExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.FuncCallExprNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void IdentifierExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.IdentifierExprNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void ParenthesizedExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.ParenthesizedExprNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void PrimaryExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.PrimaryExprNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void TernaryExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.TernaryExprNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void UnaryExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.UnaryExprNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void UnSupExprNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Expressions.UnSupExpNode(code_in.Code_inApplication.MainResourceDictionary, null, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                //node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
                node.GetParentView();
                node.SetNodePresenter(null);
                //node.ShowEditMenu();
                //node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                //node.Remove();
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
                var node = new code_in.Views.NodalView.NodesElems.Nodes.Statements.Block.IfStmtTile(code_in.Code_inApplication.MainResourceDictionary, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void SwitchStmtNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Tiles.Statements.SwitchStmtTile(code_in.Code_inApplication.MainResourceDictionary, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                //node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
                node.GetParentView();
                node.SetNodePresenter(null);
                //node.ShowEditMenu();
                node.SetPosition(42, 42);
                node.GetPosition();
                int x;
                int y;
                node.GetSize(out x, out y);
                //node.Remove();
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
                var node = new code_in.Views.NodalView.NodesElems.Tiles.Statements.WhileStmtTile(code_in.Code_inApplication.MainResourceDictionary, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void BreakStmtNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Tiles.Statements.BreakStmtTile(code_in.Code_inApplication.MainResourceDictionary, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
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
            catch (Exception e)
            { }
        }

        [TestMethod()]
        public void ReturnStmtNode()
        {
            try
            {
                var node = new code_in.Views.NodalView.NodesElems.Tiles.Statements.ReturnStmtTile(code_in.Code_inApplication.MainResourceDictionary, null);
                node.SetName("test");
                node.GetName();
                node.AddGeneric("test", EGenericVariance.IN);
                //node.UpdateDisplayedInfosFromPresenter();
                SearchBar sb = new SearchBar(new ResourceDictionary());
                node.SetParentView(null);
                node.GetParentView();
                node.SetNodePresenter(null);
                //node.ShowEditMenu();
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
