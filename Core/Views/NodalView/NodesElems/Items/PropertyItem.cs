using code_in.Exceptions;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElems.Items.Assets;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace code_in.Views.NodalView.NodesElems.Items
{
    public class PropertyItem : ClassItem
    {
        public PropertyDeclaration PropertyNode = null;
        public override void SetThemeResources(String keyPrefix) { }
        static Random r = new Random();
        private Button _getEditButton;
        private Button _setEditButton;

        public PropertyItem(ResourceDictionary themeResDict, INodalView nodalView, INodePresenter presenter) :
            base(themeResDict, nodalView, presenter)
        {
            _getEditButton = new Button();
            _setEditButton = new Button();
            _getEditButton.Content = "Get";
            _setEditButton.Content = "Set";
            _getEditButton.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            _setEditButton.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            _getEditButton.Width = 35;
            _getEditButton.Height = 25;
            _setEditButton.Width = 35;
            _setEditButton.Height = 25;
            this.AfterName.Children.Add(_getEditButton);
            this.AfterName.Children.Add(_setEditButton);
            _getEditButton.PreviewMouseDown += getEditButton_PreviewMouseDown;
            _setEditButton.PreviewMouseDown += setEditButton_PreviewMouseDown;
            //Scope.Scope = (ScopeItem.EScope)r.Next(0, 4); // TODO remove this, here only for demo purpose
        }
        void getEditButton_PreviewMouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var view = Code_inApplication.EnvironmentWrapper.CreateAndAddView<MainView.MainView>();
            view.EditProperty(this, true);
        }
        void setEditButton_PreviewMouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var view = Code_inApplication.EnvironmentWrapper.CreateAndAddView<MainView.MainView>();
            view.EditProperty(this, false);
        }
        public PropertyItem() :
            this(Code_inApplication.MainResourceDictionary, null, null)
        {
            throw new DefaultCtorVisualException();
        }
        #region IContainingModifiers
        //public void setAccessModifiers(Modifiers modifiers)
        //{
        //    if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Public) == ICSharpCode.NRefactory.CSharp.Modifiers.Public)
        //        Scope.Scope = ScopeItem.EScope.PUBLIC;
        //    else if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Private) != 0)
        //        Scope.Scope = ScopeItem.EScope.PRIVATE;
        //    else if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Protected) != 0)
        //        Scope.Scope = ScopeItem.EScope.PROTECTED;
        //    else if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Internal) != 0)
        //        Scope.Scope = ScopeItem.EScope.INTERNAL;

        //}
        #endregion
        #region IContainingAccessModifiers
        public void setModifiersList(Modifiers modifiers)
        {
            List<string> ModifiersList = new List<string>();

            ModifiersList.Clear();
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.New) == ICSharpCode.NRefactory.CSharp.Modifiers.New)
                ModifiersList.Add("new");
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Partial) == ICSharpCode.NRefactory.CSharp.Modifiers.Partial)
                ModifiersList.Add("partial");
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Static) == ICSharpCode.NRefactory.CSharp.Modifiers.Static)
                ModifiersList.Add("static");
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Abstract) == ICSharpCode.NRefactory.CSharp.Modifiers.Abstract)
                ModifiersList.Add("abstract");
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Const) == ICSharpCode.NRefactory.CSharp.Modifiers.Const)
                ModifiersList.Add("Const");
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Async) == ICSharpCode.NRefactory.CSharp.Modifiers.Async)
                ModifiersList.Add("async");
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Override) == ICSharpCode.NRefactory.CSharp.Modifiers.Override)
                ModifiersList.Add("override");
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Virtual) == ICSharpCode.NRefactory.CSharp.Modifiers.Virtual)
                ModifiersList.Add("virtual");
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Extern) == ICSharpCode.NRefactory.CSharp.Modifiers.Extern)
                ModifiersList.Add("extern");
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Readonly) == ICSharpCode.NRefactory.CSharp.Modifiers.Readonly)
                ModifiersList.Add("readonly");
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Sealed) == ICSharpCode.NRefactory.CSharp.Modifiers.Sealed)
                ModifiersList.Add("sealed");
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Unsafe) == ICSharpCode.NRefactory.CSharp.Modifiers.Unsafe)
                ModifiersList.Add("unsafe");
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Volatile) == ICSharpCode.NRefactory.CSharp.Modifiers.Volatile)
                ModifiersList.Add("volatile");
            ModifiersList.Distinct();
            Modifiers.SetModifiers(ModifiersList.ToArray());
        }
        #endregion
    }
}
