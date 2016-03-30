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

namespace code_in.Views.NodalView.NodesElems.Items
{
    public class FuncDeclItem : ATypedMemberItem
    {
        public MethodDeclaration MethodNode = null;
        StackPanel _params;
        public FuncDeclItem(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            _params = new StackPanel();
            this.AfterName.Margin = new Thickness(2, 4, 2, 4);
            this.AfterName.Background = new SolidColorBrush(Color.FromArgb(0x42, 0x25, 0x57, 0xC3));
            _params.Orientation = Orientation.Horizontal;
            var paramsOpen = new Label();
            paramsOpen.Content = "(";
            paramsOpen.Foreground = new SolidColorBrush(Colors.White);
            var paramsClose = new Label();
            paramsClose.Foreground = new SolidColorBrush(Colors.White);
            paramsClose.Content = ")";
            this.AfterName.Children.Add(paramsOpen);
            this.AfterName.Children.Add(_params);
            this.AfterName.Children.Add(paramsClose);
            { // TODO This is temporary
                var editButton = new Grid();
                editButton.Background = new SolidColorBrush(Colors.GreenYellow);
                editButton.Width = 25;
                editButton.Height = 25;
                editButton.PreviewMouseDown += editButton_PreviewMouseDown;
                this.AfterName.Children.Add(editButton);
            }
            this.MouseEnter += FuncDeclItem_MouseEnter;
            this.MouseLeave += FuncDeclItem_MouseLeave;
        }

        void editButton_PreviewMouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var view = Code_inApplication.EnvironmentWrapper.CreateAndAddView<MainView.MainView>();
            view.EditFunction(this);
        }

        void FuncDeclItem_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Background = new SolidColorBrush(Colors.Transparent);
        }

        void FuncDeclItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromArgb(0x21, 0xFF, 0xFF, 0xFF));
        }

        public void AddParam(String type)
        {
            var lbl = new Label();
            lbl.Content = type;
            lbl.Foreground = new SolidColorBrush(Color.FromRgb(0x1C, 0xC2, 0xEC));
            this._params.Children.Add(lbl);
        }

        public override void SetDynamicResources(String keyPrefix)
        {

        }
    }
}
