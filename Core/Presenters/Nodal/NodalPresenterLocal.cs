using code_in.Models;
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
    public abstract class ANodalPresenterLocal : INodalPresenter
    {
        public INodalView View
        {
            get;
            set;
        }
        public INodalModel Model;
        public abstract String DocumentName
        {
            get;
        }

        public ANodalPresenterLocal()
        {
        }

        #region INodalPresenter
        public abstract void User_AddNode_Callback(Type nodeTypeToAdd, Point mousePos);
        #endregion INodalPresenter

        #region this
        public void setOtherModifiers(IContainingModifiers view, Modifiers tmpModifiers)
        {
            view.setModifiersList(tmpModifiers);
        }
        public void setAccessModifiers(IContainingAccessModifiers view, Modifiers tmpModifiers)
        {
            view.setAccessModifiers(tmpModifiers);
        }
        public void InitInheritance(IContainingInheritance view, TypeDeclaration typeDecl)
        {
            List<string> InheritanceList = new List<string>();
            foreach (var inherit in typeDecl.BaseTypes)
                InheritanceList.Add(inherit.ToString());
            view.ManageInheritance(InheritanceList);
        }
        #endregion this


        public void SetAllGenerics(IContainingGenerics view, TypeDeclaration typeDecl)
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

        public void InitAttributes(IContainingAttribute view, TypeDeclaration typedecl)
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




        Tuple<EContextMenuOptions, Action<object[]>>[] IContextMenu.GetMenuOptions()
        {
            return new Tuple<EContextMenuOptions, Action<object[]>>[] { 
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ADD, AddNode), 
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ALIGN, _alignNodes), 
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.COLLAPSEALL, CollapseAllNode),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.EXPANDALL, ExpandAllNode),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.SAVE, Save),
                new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.HELP, HelpNode)
            };
        }



        static void _alignNodes(object[] objects)
        {
            ANodalPresenterLocal self = objects[0] as ANodalPresenterLocal;
            self.View.Align();
        }
        static void AddNode(object[] objects)
        {
            ANodalPresenterLocal self = objects[0] as ANodalPresenterLocal;

            ContextMenu cm = new ContextMenu();
            UIElement view = self.View as UIElement;
            cm.Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse;

            var listOfBs = self.GetAvailableNodes();

            foreach (var entry in listOfBs)
            {
                MenuItem mi = new MenuItem();
                mi.Header = entry.Name;
                mi.Click += AddNodeCallback_Click;
                mi.DataContext = entry;
                cm.Items.Add(mi);
            }
            cm.IsOpen = true;
            _viewStatic = (self.View) as ANodalView; // TODO
        }

        static void AddNodeCallback_Click(object sender, RoutedEventArgs e)
        {
            _viewStatic.Presenter.User_AddNode_Callback(((MenuItem)sender).DataContext as Type, Mouse.GetPosition(_viewStatic.MainGrid));
        }
        protected static ANodalView _viewStatic = null;

        private static Action EmptyDelegate = delegate() { };

        static void CloseNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void CollapseAllNode(object[] objects)
        {
            ANodalPresenterLocal self = objects[0] as ANodalPresenterLocal;

            foreach (var node in ((ANodalView)self.View)._registeredNodes)
                node.IsExpanded = false;
        }
        static void ExpandAllNode(object[] objects)
        {
            ANodalPresenterLocal self = objects[0] as ANodalPresenterLocal;

            foreach (var node in ((ANodalView)self.View)._registeredNodes)
                node.IsExpanded = true;
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
            ANodalPresenterLocal self = objects[0] as ANodalPresenterLocal;
            self.Save();
        }
        public abstract List<Type> GetAvailableNodes();




        public abstract void Save();

        public abstract bool IsSaved
        {
            get;
        }
    }
}