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
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace code_in.Views.NodalView.NodesElems.Items
{
    public class ConstructorItem : ClassItem
    {
        public ConstructorDeclaration ConstructorNode = null;
        ParametersList _params;
        private Image _editButton;

        public ConstructorItem(ResourceDictionary themeResDict, INodalView nodalView, INodePresenter presenter) :
            base(themeResDict, nodalView, presenter)
        {
            { // TODO This is temporary
                _editButton = new Image();
                var imageSrc = new BitmapImage();
                _params = new ParametersList(themeResDict);
                this.AfterName.Children.Add(_params);
                imageSrc.BeginInit();
                imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/edit.png");
                imageSrc.EndInit();
                _editButton.Source = imageSrc;
                _editButton.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.Fant);
                _editButton.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                _editButton.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                _editButton.Width = 35;
                _editButton.Height = 35;
                _editButton.PreviewMouseDown += editButton_PreviewMouseDown;
                this.AfterName.Children.Add(_editButton);
                //_editButton.Visibility = System.Windows.Visibility.Visible;
                _editButton.SetValue(Image.OpacityProperty, 0.0);
                _editButton.IsEnabled = false;
            }
        }
        void editButton_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var view = Code_inApplication.EnvironmentWrapper.CreateAndAddView<NodalView>();
            view.EditConstructor(this);
        }
        public override void SetThemeResources(String keyPrefix) { }
        public override void OnMouseLeave()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1.0;
            da.To = 0.0;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.1));
            _editButton.BeginAnimation(Image.OpacityProperty, da);
            da.Completed += _animEditDisapearCompleted;
        }
        void _animEditDisapearCompleted(object sender, EventArgs e)
        {
            _editButton.IsEnabled = false;
        }
        public override void OnMouseEnter()
        {
            _editButton.BeginAnimation(Image.OpacityProperty, null);
            _editButton.SetValue(Image.OpacityProperty, 1.0);
            _editButton.IsEnabled = true;
        }
        public void AddParam(String type)
        {
            _params.AddParameter(type);
        }
        public ConstructorItem() :
            this(Code_inApplication.MainResourceDictionary, null, null)
        {
            throw new DefaultCtorVisualException();
        }
        #region IContainingModifiers
        public void setAccessModifiers(Modifiers modifiers)
        {
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Public) == ICSharpCode.NRefactory.CSharp.Modifiers.Public)
                Scope.Scope = ScopeItem.EScope.PUBLIC;
            else if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Private) != 0)
                Scope.Scope = ScopeItem.EScope.PRIVATE;
            else if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Protected) != 0)
                Scope.Scope = ScopeItem.EScope.PROTECTED;
            else if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Internal) != 0)
                Scope.Scope = ScopeItem.EScope.INTERNAL;

        }
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
