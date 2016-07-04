using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace code_in.Views.NodalView
{
    /// <summary>
    /// Interaction logic for EditNodePanel.xaml
    /// </summary>
    public partial class EditNodePanel : UserControl, ICodeInVisual, ICodeInTextLanguage
    {
        INodePresenter _nodePresenter = null;
        public EditNodePanel(ResourceDictionary themeResDict)
        {
            this.Resources.MergedDictionaries.Add(themeResDict);
            InitializeComponent();
//            _nodePresenter = new NodePresenter(themeResDict);
            UpdateGenericListInEditMenu();
        }

        public EditNodePanel() :
            this(Code_inApplication.MainResourceDictionary)
        { /* Do not use this constructor except for tests */}

        /// <summary>
        /// Called when editing a node, it sets the controls that allows specific node's modifications
        /// </summary>
        public void SetFields(INodePresenter nodePresenter)
        {
            _nodePresenter = nodePresenter;
            var actions = nodePresenter.GetActions();
            bool modifiersArea = false;
            int i = 0; // i begin to 1 because grid.column"0" is the name as a default area

            if ((actions & ENodeActions.NAME) == ENodeActions.NAME) // TODO for optimisation, create borders and columns at runtime -> light optimisation
            {
                _mainArea.Visibility = System.Windows.Visibility.Visible;
                _mainArea.IsEnabled = true;
                Grid.SetColumn(_mainArea, i);
                ++i;
            }

            if ((actions & ENodeActions.ACCESS_MODIFIERS) == ENodeActions.ACCESS_MODIFIERS)
            {
                _accessModifiers.IsEnabled = true;
                _accessModifiers.Visibility = System.Windows.Visibility.Visible;
                modifiersArea = true;
                _modifiersArea.Visibility = System.Windows.Visibility.Visible;
                FirstBorder.IsEnabled = true;
                FirstBorder.Visibility = System.Windows.Visibility.Visible;
                Grid.SetColumn(FirstBorder, i); // i = 1
                ++i;
                Grid.SetColumn(_modifiersArea, i); // i = 2
            }
/*            else
            {
                _accessModifiers.IsEnabled = false;
                _accessModifiers.Visibility = System.Windows.Visibility.Hidden;
            }*/
            if ((actions & ENodeActions.MODIFIERS) == ENodeActions.MODIFIERS)
            {
                modifiersArea = true;
                _modifiersArea.Visibility = System.Windows.Visibility.Visible;
                _modifiersArea.IsEnabled = true;
                _modifiersList.IsEnabled = true;
                _modifiersList.Visibility = System.Windows.Visibility.Visible;
                Grid.SetColumn(_modifiersList, i); // i = 2
                ++i;
            }
            if ((actions & ENodeActions.GENERICS) == ENodeActions.GENERICS)
            {
                DeclGenericsField.IsEnabled = true;
                DeclGenericsField.Visibility = System.Windows.Visibility.Visible;
                SecondBorder.IsEnabled = true;
                SecondBorder.Visibility = System.Windows.Visibility.Visible;
                Grid.SetColumn(SecondBorder, i); // i = 3
                ++i;
                Grid.SetColumn(DeclGenericsField, i); // i = 4
                ++i;
            }
            if ((actions & ENodeActions.INHERITANCE) == ENodeActions.INHERITANCE)
            {
                InheritanceField.IsEnabled = true;
                InheritanceField.Visibility = System.Windows.Visibility.Visible;
                ThirdBorder.IsEnabled = true;
                ThirdBorder.Visibility = System.Windows.Visibility.Visible;
                Grid.SetColumn(ThirdBorder, i); // i = 5
                ++i;
                Grid.SetColumn(InheritanceField, i); // i = 6
                ++i;
            }
            if ((actions & ENodeActions.COMMENT) == ENodeActions.COMMENT)
            {
/*                _commentArea.Visibility = System.Windows.Visibility.Visible;
                _commentArea.IsEnabled = true;
                FourthBorder.Visibility = System.Windows.Visibility.Visible;
                FourthBorder.IsEnabled = true;
                Grid.SetColumn(FourthBorder, i); // i = 7
                ++i;
                Grid.SetColumn(_commentArea, i); // i = 8
                ++i;*/
            }
            if ((actions & ENodeActions.ATTRIBUTE) == ENodeActions.ATTRIBUTE)
            {
                AttributesField.Visibility = System.Windows.Visibility.Visible;
                AttributesField.IsEnabled = true;
                FifthBorder.Visibility = System.Windows.Visibility.Visible;
                FifthBorder.IsEnabled = true;
                Grid.SetColumn(FifthBorder, i); // i = 9
                ++i;
                Grid.SetColumn(AttributesField, i); // i = 10
                ++i;
            }
            this._modifiersArea.IsEnabled = modifiersArea;
            this._modifiersArea.Visibility = (modifiersArea ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden);

            if ((actions & ENodeActions.EXEC_TYPE) == ENodeActions.EXEC_TYPE)
            {
                ExecTypeField.Visibility = System.Windows.Visibility.Visible;
                ExecTypeField.IsEnabled = true;
                SixthBorder.Visibility = System.Windows.Visibility.Visible;
                SixthBorder.IsEnabled = true;
                Grid.SetColumn(SixthBorder, i); // i = 11
                ++i;
                Grid.SetColumn(ExecTypeField, i); // i = 12
                ++i;
            }
            if ((actions & ENodeActions.EXEC_PARAMETERS) == ENodeActions.EXEC_PARAMETERS)
            {
                ExecParametersField.Visibility = System.Windows.Visibility.Visible;
                ExecParametersField.IsEnabled = true;
                SeventhBorder.Visibility = System.Windows.Visibility.Visible;
                SeventhBorder.IsEnabled = true;
                Grid.SetColumn(SeventhBorder, i); // i = 13
                ++i;
                Grid.SetColumn(ExecParametersField, i); // i = 14
                ++i;
            }
            if ((actions & ENodeActions.EXEC_GENERICS) == ENodeActions.EXEC_GENERICS)
            {
                ExecGenericsField.Visibility = System.Windows.Visibility.Visible;
                ExecGenericsField.IsEnabled = true;
                EighthBorder.Visibility = System.Windows.Visibility.Visible;
                EighthBorder.IsEnabled = true;
                Grid.SetColumn(EighthBorder, i); // i = 15
                ++i;
                Grid.SetColumn(ExecGenericsField, i); // i = 16
                ++i;
            }
            if ((actions & ENodeActions.TYPE) == ENodeActions.TYPE)
            {
                TypeField.Visibility = System.Windows.Visibility.Visible;
                TypeField.IsEnabled = true;
                NinthBorder.Visibility = System.Windows.Visibility.Visible;
                NinthBorder.IsEnabled = true;
                Grid.SetColumn(NinthBorder, i); // i = 17
                ++i;
                Grid.SetColumn(TypeField, i); // i = 18
                ++i;
            }
            if ((actions & ENodeActions.TEXT) == ENodeActions.TEXT)
            {
                TextField.Visibility = System.Windows.Visibility.Visible;
                TextField.IsEnabled = true;
                TenthBorder.Visibility = System.Windows.Visibility.Visible;
                TenthBorder.IsEnabled = true;
                Grid.SetColumn(TenthBorder, i); // i = 19
                ++i;
                Grid.SetColumn(TextField, i); // i = 20
            }
        }

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary()
        {
            throw new NotImplementedException();
        }

        public void SetThemeResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual
        #region ICodeInTextLanguage
        public ResourceDictionary GetLanguageResourceDictionary()
        {
            throw new NotImplementedException();
        }

        public void SetLanguageResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInTextLanguage

        private void DeleteGeneric(object sender, RoutedEventArgs e)
        {

        }

        private void EventAddGenericIntoEditMenu(object sender, RoutedEventArgs e)
        {
            StackPanel NewGeneric = new StackPanel();
            RadioButton VarianceIn = new RadioButton();
            RadioButton VarianceOut = new RadioButton();
            RadioButton VarianceNothing = new RadioButton();
            TextBox GenericName = new TextBox();
            Button deleteGeneric = new Button();

            NewGeneric.Orientation = Orientation.Horizontal;
            NewGeneric.Margin = new Thickness(0, 10, 0, 0);
            NewGeneric.Name = "Generic" + (DeclGenericsField.Children.Count - 2);
            VarianceIn.Content = "In";
            VarianceIn.Margin = new Thickness(0, 0, 10, 0);
            VarianceIn.Checked += VarianceInChecked;
            VarianceOut.Content = "Out";
            VarianceOut.Margin = new Thickness(0, 0, 10, 0);
            VarianceOut.Checked += VarianceOutChecked;
            VarianceNothing.Content = "Nothing";
            VarianceNothing.Margin = new Thickness(0, 0, 10, 0);
            VarianceNothing.Checked += VarianceNothingChecked;
            GenericName.Width = 80;
            GenericName.TextChanged += GenericNameChanged;
            //GenericName.Text = "name";
            GenericName.Name = "TextBoxGenericName";
            deleteGeneric.Width = 20;
            deleteGeneric.Height = 20;
            deleteGeneric.Margin = new Thickness(10, 0, 0, 0);
            deleteGeneric.Content = "X";
            deleteGeneric.Click += DeleteGeneric;
            NewGeneric.Children.Add(VarianceIn);
            NewGeneric.Children.Add(VarianceOut);
            NewGeneric.Children.Add(VarianceNothing);
            NewGeneric.Children.Add(GenericName);
            NewGeneric.Children.Add(deleteGeneric);
            DeclGenericsField.Children.Insert(1, NewGeneric);
        }

        public void AddExistingGenericsToEditMenu(string name, EGenericVariance variance)
        {
            StackPanel NewGeneric = new StackPanel();
            RadioButton VarianceIn = new RadioButton();
            RadioButton VarianceOut = new RadioButton();
            RadioButton VarianceNothing = new RadioButton();
            TextBox GenericName = new TextBox();
            Button deleteGeneric = new Button();

            if (variance == EGenericVariance.IN)
                VarianceIn.IsChecked = true;
            if (variance == EGenericVariance.OUT)
                VarianceOut.IsChecked = true;
            if (variance == EGenericVariance.NOTHING)
                VarianceNothing.IsChecked = true;
            NewGeneric.Orientation = Orientation.Horizontal;
            NewGeneric.Margin = new Thickness(0, 10, 0, 0);
            NewGeneric.Name = "Generic" + (DeclGenericsField.Children.Count - 2);
            VarianceIn.Content = "In";
            VarianceIn.Margin = new Thickness(0, 0, 10, 0);
            VarianceIn.Checked += VarianceInChecked;
            VarianceOut.Content = "Out";
            VarianceOut.Margin = new Thickness(0, 0, 10, 0);
            VarianceOut.Checked += VarianceOutChecked;
            VarianceNothing.Content = "Nothing";
            VarianceNothing.Margin = new Thickness(0, 0, 10, 0);
            VarianceNothing.Checked += VarianceNothingChecked;
            GenericName.Width = 80;
            GenericName.TextChanged += GenericNameChanged;
            GenericName.Text = name;
            GenericName.Name = "TextBoxGenericName";
            deleteGeneric.Width = 20;
            deleteGeneric.Height = 20;
            deleteGeneric.Margin = new Thickness(10, 0, 0, 0);
            deleteGeneric.Content = "X";
            deleteGeneric.Click += DeleteGeneric;
            NewGeneric.Children.Add(VarianceIn);
            NewGeneric.Children.Add(VarianceOut);
            NewGeneric.Children.Add(VarianceNothing);
            NewGeneric.Children.Add(GenericName);
            NewGeneric.Children.Add(deleteGeneric);
            DeclGenericsField.Children.Insert(1, NewGeneric);
        }
        public void UpdateGenericListInEditMenu()
        {
            if (_nodePresenter != null)
            {
                List<Tuple<string, EGenericVariance>> tmp = _nodePresenter.getGenericList();
                foreach (Tuple<string, EGenericVariance> generic in tmp)
                {
                    AddExistingGenericsToEditMenu(generic.Item1, generic.Item2);
                }
            }
        }

        private void GenericNameChanged(object sender, TextChangedEventArgs e)
        {
            TextBox name = sender as TextBox;
            StackPanel parent = name.Parent as StackPanel;
            string stringIndex = parent.Name.Substring(parent.Name.Count() - 1, 1);

            int index = int.Parse(stringIndex, null);
            _nodePresenter.ModifGenericName(name.Text, index);
        }

        private void VarianceNothingChecked(object sender, RoutedEventArgs e)
        {
            RadioButton variance = sender as RadioButton;
            StackPanel parent = variance.Parent as StackPanel;
            TextBox name = parent.Children[3] as TextBox;
            string stringIndex = parent.Name.Substring(parent.Name.Count() - 1, 1);

            int index = int.Parse(stringIndex, null);
            if (_nodePresenter.ifGenericExist(name.Text))
                _nodePresenter.ModifGenericVariance(index, NodesElems.Nodes.Assets.EGenericVariance.NOTHING, name.Text);
            else
                _nodePresenter.AddGeneric(name.Text, NodesElems.Nodes.Assets.EGenericVariance.NOTHING);
        }

        private void VarianceOutChecked(object sender, RoutedEventArgs e)
        {
            RadioButton variance = sender as RadioButton;
            StackPanel parent = variance.Parent as StackPanel;
            TextBox name = parent.Children[3] as TextBox;
            string stringIndex = parent.Name.Substring(parent.Name.Count() - 1, 1);

            int index = int.Parse(stringIndex, null);
            if (_nodePresenter.ifGenericExist(name.Text))
                _nodePresenter.ModifGenericVariance(index, NodesElems.Nodes.Assets.EGenericVariance.OUT, name.Text);
            else
                _nodePresenter.AddGeneric(name.Text, NodesElems.Nodes.Assets.EGenericVariance.OUT);
        }

        private void VarianceInChecked(object sender, RoutedEventArgs e)
        {
            RadioButton variance = sender as RadioButton;
            StackPanel parent = variance.Parent as StackPanel;
            TextBox name = parent.Children[3] as TextBox;
            string stringIndex = parent.Name.Substring(parent.Name.Count() - 1, 1);

            int index = int.Parse(stringIndex, null);
            if (_nodePresenter.ifGenericExist(name.Text))
                _nodePresenter.ModifGenericVariance(index, NodesElems.Nodes.Assets.EGenericVariance.IN, name.Text);
            else
                _nodePresenter.AddGeneric(name.Text, NodesElems.Nodes.Assets.EGenericVariance.IN);
        }

        private void QuitEditMenu(object sender, RoutedEventArgs e)
        {
            ((StackPanel)this.Parent).Children.Clear();
        }


        private void VirtualChecked(object sender, RoutedEventArgs e)
        {
            CheckBox tmp = sender as CheckBox;
            _nodePresenter.SetOtherModifiers(tmp.Content.ToString(), true);
        }

        private void AbstractChecked(object sender, RoutedEventArgs e)
        {
            CheckBox tmp = sender as CheckBox;
            _nodePresenter.SetOtherModifiers(tmp.Content.ToString(), true);
        }

        private void OverrideChecked(object sender, RoutedEventArgs e)
        {
            CheckBox tmp = sender as CheckBox;
            _nodePresenter.SetOtherModifiers(tmp.Content.ToString(), true);
        }

        private void GeneralNameChanged(object sender, TextChangedEventArgs e)
        {
            if (_nodePresenter != null)
            _nodePresenter.SetName(NodeName.Text);
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void _accessModifiersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem tmp = _accessModifiersList.SelectedItem as ComboBoxItem;
            _nodePresenter.SetAccesModifier(tmp.Content.ToString());
        }

        private void VirtualUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox tmp = sender as CheckBox;
            _nodePresenter.SetOtherModifiers(tmp.Content.ToString(), false);

        }

        private void AbstractUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox tmp = sender as CheckBox;
            _nodePresenter.SetOtherModifiers(tmp.Content.ToString(), false);

        }

        private void OverrideUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox tmp = sender as CheckBox;
            _nodePresenter.SetOtherModifiers(tmp.Content.ToString(), false);

        }
    }
}
