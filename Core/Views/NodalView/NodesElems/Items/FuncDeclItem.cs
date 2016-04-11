using code_in.Views.NodalView.NodesElems.Items.Assets;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes;
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
    public class FuncDeclItem : ATypedMemberItem
    {
        public MethodDeclaration MethodNode = null; // TODO move to ANodePresenter
        ParametersList _params;
        private Image _editButton;
        public FuncDeclItem(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            _params = new ParametersList(themeResDict);
            //this.AfterName.Margin = new Thickness(2, 4, 2, 4);
            this.AfterName.Children.Add(_params);
            { // TODO This is temporary
                _editButton = new Image();
                var imageSrc = new BitmapImage();
                imageSrc.BeginInit();
                imageSrc.UriSource = new Uri("pack://application:,,,/TranslationTier;component/Resources/Graphics/edit.png");
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

        void editButton_PreviewMouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var view = Code_inApplication.EnvironmentWrapper.CreateAndAddView<MainView.MainView>();
            view.EditFunction(this);
        }

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

        public override void SetDynamicResources(String keyPrefix)
        {

        }
    }
}
