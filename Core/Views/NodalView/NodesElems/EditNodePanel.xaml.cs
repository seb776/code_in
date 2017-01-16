using code_in.Exceptions;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;
using code_in.Views.NodalView.NodesElems.Nodes.Expressions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        }

        #region Popup
        static Popup _parentPopup = null;

        //Placement
        public static readonly DependencyProperty PlacementProperty = Popup.PlacementProperty.AddOwner(typeof(EditNodePanel));
        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        //PlacementTarget
        public static readonly DependencyProperty PlacementTargetProperty = Popup.PlacementTargetProperty.AddOwner(typeof(EditNodePanel));
        public UIElement PlacementTarget
        {
            get { return (UIElement)GetValue(PlacementTargetProperty); }
            set { SetValue(PlacementTargetProperty, value); }
        }

        //PlacementRectangle
        public static readonly DependencyProperty PlacementRectangleProperty = Popup.PlacementRectangleProperty.AddOwner(typeof(EditNodePanel));
        public Rect PlacementRectangle
        {
            get { return (Rect)GetValue(PlacementRectangleProperty); }
            set { SetValue(PlacementRectangleProperty, value); }
        }

        //HorizontalOffset
        public static readonly DependencyProperty HorizontalOffsetProperty = Popup.HorizontalOffsetProperty.AddOwner(typeof(EditNodePanel));
        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        //VerticalOffset
        public static readonly DependencyProperty VerticalOffsetProperty = Popup.VerticalOffsetProperty.AddOwner(typeof(EditNodePanel));
        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty = Popup.IsOpenProperty.AddOwner(
                typeof(EditNodePanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnIsOpenChanged)));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EditNodePanel ctrl = (EditNodePanel)d;

            if ((bool)e.NewValue)
            {
                if (_parentPopup == null)
                {
                    _parentPopup = new Popup();
                    _parentPopup.AllowsTransparency = true;
                }
                Popup.CreateRootPopup(_parentPopup, ctrl);
            }
        }
        #endregion Popup

        public EditNodePanel() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new DefaultCtorVisualException(); }

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

        /// <summary>
        /// Called when editing a node, it sets the controls that allows specific node's modifications
        /// </summary>

        /* Settings of the fields */

        #region SetFieldsMethod
        public void SetFields(INodePresenter nodePresenter)
        {
            _nodePresenter = nodePresenter;
            var actions = nodePresenter.GetActions();
            int i = 0;

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
                _modifiersArea.IsEnabled = true;
                _accessModifiers.Visibility = System.Windows.Visibility.Visible;
                _modifiersArea.Visibility = System.Windows.Visibility.Visible;
                if (i != 0)
                {
                    FirstBorder.IsEnabled = true;
                    FirstBorder.Visibility = System.Windows.Visibility.Visible;
                    FirstBorder.BorderThickness = new Thickness(1);
                    Grid.SetColumn(FirstBorder, i); // i = 1
                    ++i;
                }
                Grid.SetColumn(_modifiersArea, i); // i = 2
                if (((actions & ENodeActions.MODIFIERS) == ENodeActions.MODIFIERS) == false)
                ++i;
            }

            if ((actions & ENodeActions.MODIFIERS) == ENodeActions.MODIFIERS)
            {
                _modifiersArea.Visibility = System.Windows.Visibility.Visible;
                _modifiersArea.IsEnabled = true;
                _modifiersList.IsEnabled = true;
                _modifiersList.Visibility = System.Windows.Visibility.Visible;
                Grid.SetColumn(_modifiersList, i); // i = 2
                ++i;
                UpdateModifiersListInEditMenu();
            }
            if ((actions & ENodeActions.GENERICS) == ENodeActions.GENERICS)
            {
                DeclGenericsField.IsEnabled = true;
                DeclGenericsField.Visibility = System.Windows.Visibility.Visible;
                if (i != 0)
                {
                    SecondBorder.IsEnabled = true;
                    SecondBorder.Visibility = System.Windows.Visibility.Visible;
                    SecondBorder.BorderThickness = new Thickness(1);
                    Grid.SetColumn(SecondBorder, i); // i = 3
                ++i;
                }
                Grid.SetColumn(DeclGenericsField, i); // i = 4
                ++i;
                UpdateGenericListInEditMenu();
            }
            if ((actions & ENodeActions.INHERITANCE) == ENodeActions.INHERITANCE)
            {
                InheritanceField.IsEnabled = true;
                InheritanceField.Visibility = System.Windows.Visibility.Visible;
                if (i != 0)
                {
                    ThirdBorder.IsEnabled = true;
                    ThirdBorder.Visibility = System.Windows.Visibility.Visible;
                    ThirdBorder.BorderThickness = new Thickness(1);
                    Grid.SetColumn(ThirdBorder, i); // i = 5
                    ++i;
                }
                Grid.SetColumn(InheritanceField, i); // i = 6
                ++i;
                UpdateInheritanceInEditMenu();
            }
            if ((actions & ENodeActions.COMMENT) == ENodeActions.COMMENT)
            {
                _commentArea.Visibility = System.Windows.Visibility.Visible;
                _commentArea.IsEnabled = true;
                if (i != 0)
                {
                    FourthBorder.Visibility = System.Windows.Visibility.Visible;
                    FourthBorder.IsEnabled = true;
                    FourthBorder.BorderThickness = new Thickness(1);
                    Grid.SetColumn(FourthBorder, i); // i = 7
                    ++i;
                }
                Grid.SetColumn(_commentArea, i); // i = 8
                ++i;
            }
            if ((actions & ENodeActions.ATTRIBUTE) == ENodeActions.ATTRIBUTE)
            {
                AttributesField.Visibility = System.Windows.Visibility.Visible;
                AttributesField.IsEnabled = true;
                if (i != 0)
                {
                    FifthBorder.Visibility = System.Windows.Visibility.Visible;
                    FifthBorder.IsEnabled = true;
                    FifthBorder.BorderThickness = new Thickness(1);
                    Grid.SetColumn(FifthBorder, i); // i = 9
                    ++i;
                }
                Grid.SetColumn(AttributesField, i); // i = 10
                ++i;
            }

            if ((actions & ENodeActions.EXEC_TYPE) == ENodeActions.EXEC_TYPE)
            {
                ExecTypeField.Visibility = System.Windows.Visibility.Visible;
                ExecTypeField.IsEnabled = true;
                if (i != 0)
                {
                    SixthBorder.Visibility = System.Windows.Visibility.Visible;
                    SixthBorder.IsEnabled = true;
                    SixthBorder.BorderThickness = new Thickness(1);
                    Grid.SetColumn(SixthBorder, i); // i = 11
                    ++i;
                }
                Grid.SetColumn(ExecTypeField, i); // i = 12
                ++i;
            }
            if ((actions & ENodeActions.EXEC_PARAMETERS) == ENodeActions.EXEC_PARAMETERS)
            {
                ExecParametersField.Visibility = System.Windows.Visibility.Visible;
                ExecParametersField.IsEnabled = true;
                if (i != 0)
                {
                    SeventhBorder.Visibility = System.Windows.Visibility.Visible;
                    SeventhBorder.IsEnabled = true;
                    SeventhBorder.BorderThickness = new Thickness(1);
                    Grid.SetColumn(SeventhBorder, i); // i = 13
                    ++i;
                }
                Grid.SetColumn(ExecParametersField, i); // i = 14
                ++i;
                UpdateExecParamsNbInMenu();
            }
            if ((actions & ENodeActions.EXEC_GENERICS) == ENodeActions.EXEC_GENERICS)
            {
                ExecGenericsField.Visibility = System.Windows.Visibility.Visible;
                ExecGenericsField.IsEnabled = true;
                if (i != 0)
                {
                    EighthBorder.Visibility = System.Windows.Visibility.Visible;
                    EighthBorder.IsEnabled = true;
                    EighthBorder.BorderThickness = new Thickness(1);
                    Grid.SetColumn(EighthBorder, i); // i = 15
                    ++i;
                }
                Grid.SetColumn(ExecGenericsField, i); // i = 16
                ++i;
                UpdateGenericListInEditMenu();
            }
            if ((actions & ENodeActions.TYPE) == ENodeActions.TYPE)
            {
                TypeField.Visibility = System.Windows.Visibility.Visible;
                TypeField.IsEnabled = true;
                if (i != 0)
                {
                    NinthBorder.Visibility = System.Windows.Visibility.Visible;
                    NinthBorder.IsEnabled = true;
                    NinthBorder.BorderThickness = new Thickness(1);
                    Grid.SetColumn(NinthBorder, i); // i = 17
                    ++i;
                }
                Grid.SetColumn(TypeField, i); // i = 18
                ++i;
                UpdateTypeInEditMenu();
            }
            if ((actions & ENodeActions.TEXT) == ENodeActions.TEXT)
            {
                TextField.Visibility = System.Windows.Visibility.Visible;
                TextField.IsEnabled = true;
                if (i != 0)
                {
                    TenthBorder.Visibility = System.Windows.Visibility.Visible;
                    TenthBorder.IsEnabled = true;
                    TenthBorder.BorderThickness = new Thickness(1);
                    Grid.SetColumn(TenthBorder, i); // i = 19
                    ++i;
                }
                Grid.SetColumn(TextField, i); // i = 20
            }
        }
        #endregion SetFieldsMethod

        /* Start of the menu definition and actions */

        #region NameManagement
        private void GeneralNameChanged(object sender, TextChangedEventArgs e)
        {
            if (_nodePresenter != null)
                _nodePresenter.SetName(NodeName.Text);
        }
        private void NodeName_GotFocus(object sender, RoutedEventArgs e)
        {
            NodeName.Text = "";
        }

        #endregion NameManagement

        #region TypeManagement
        private void UpdateTypeInEditMenu()
        {
            TypeName.Text = _nodePresenter.getType();
        }

        private void TypeName_KeyDown(object sender, KeyEventArgs e)
        {
            var tmp = sender as TextBox;

            tmp.Foreground = Brushes.Red;
            if (e.Key == Key.Enter)
            {
                _nodePresenter.UpdateType(tmp.Text);
                tmp.Foreground = Brushes.Black;
            }
        }

        #endregion TypeManagement

        #region ModifiersManagement
        private void UpdateModifiersListInEditMenu()
        {
            foreach (var tmp in _nodePresenter.getModifiersList())
            {
                if (tmp == "new")
                    NewBox.IsChecked = true;
                if (tmp == "partial")
                    PartialBox.IsChecked = true;
                if (tmp == "static")
                    StaticBox.IsChecked = true;
                if (tmp == "abstract")
                    AbstractBox.IsChecked = true;
                if (tmp == "const")
                    ConstBox.IsChecked = true;
                if (tmp == "async")
                    AsyncBox.IsChecked = true;
                if (tmp == "override")
                    OverrideBox.IsChecked = true;
                if (tmp == "virtual")
                    virtualBox.IsChecked = true;
                if (tmp == "extern")
                    ExternBox.IsChecked = true;
                if (tmp == "readonly")
                    ReadonlyBox.IsChecked = true;
                if (tmp == "sealed")
                    SealedBox.IsChecked = true;
                if (tmp == "unsafe")
                    UnsafeBox.IsChecked = true;
                if (tmp == "volatile")
                    VolatileBox.IsChecked = true;
            }
        }
        private void CheckedModifier(object sender, RoutedEventArgs e)
        {
            CheckBox tmp = sender as CheckBox;
            _nodePresenter.SetOtherModifiers(tmp.Content.ToString(), true);
        }
        private void _accessModifiersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem tmp = _accessModifiersList.SelectedItem as ComboBoxItem;
            _nodePresenter.SetAccesModifier(tmp.Content.ToString());
        }

        private void UncheckedModifier(object sender, RoutedEventArgs e)
        {
            CheckBox tmp = sender as CheckBox;
            _nodePresenter.SetOtherModifiers(tmp.Content.ToString(), false);
        }

        #endregion ModifiersManagement

        #region InheritanceManagement
        private void EventAddInheritance(object sender, RoutedEventArgs e)
        {
            StackPanel NewInheritance = new StackPanel();
            TextBox InheritanceName = new TextBox();
            Button deleteInheritance = new Button();

            NewInheritance.Orientation = Orientation.Horizontal;
            NewInheritance.Name = "Inheritance" + InheritancePanel.Children.Count.ToString();
            InheritanceName.Width = 100;
            InheritanceName.TextChanged += InheritanceNameTextChanged;
            InheritanceName.Text = "Type a name";
            InheritanceName.Foreground = Brushes.Gray;
            InheritanceName.GotFocus += InheritanceNameGotFocus;
            deleteInheritance.Click += DeleteInheritance;
            deleteInheritance.Margin = new Thickness(10, 0, 0, 0);
            deleteInheritance.Width = 20;
            deleteInheritance.Height = 20;
            deleteInheritance.Content = "X";
            NewInheritance.Children.Add(InheritanceName);
            NewInheritance.Children.Add(deleteInheritance);
            InheritancePanel.Children.Add(NewInheritance);
            _nodePresenter.AddInheritance(InheritanceName.Text);
        }

        private void InheritanceNameGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox NameBox = sender as TextBox;
            NameBox.Text = "";
        }

        public void AddInheritanceToEditMenu(string name)
        {
            StackPanel NewInheritance = new StackPanel();
            TextBox InheritanceName = new TextBox();
            Button deleteInheritance = new Button();

            NewInheritance.Orientation = Orientation.Horizontal;
            NewInheritance.Name = "Inheritance" + InheritancePanel.Children.Count.ToString();
            InheritanceName.Width = 100;
            InheritanceName.TextChanged += InheritanceNameTextChanged;
            InheritanceName.Text = name;
            deleteInheritance.Click += DeleteInheritance;
            deleteInheritance.Margin = new Thickness(10, 0, 0, 0);
            deleteInheritance.Width = 20;
            deleteInheritance.Height = 20;
            deleteInheritance.Content = "X";
            NewInheritance.Children.Add(InheritanceName);
            NewInheritance.Children.Add(deleteInheritance);
            InheritancePanel.Children.Add(NewInheritance);
        }

        public void UpdateInheritanceInEditMenu()
        {
            InheritancePanel.Children.Clear();
            if (_nodePresenter != null && _nodePresenter.getInheritanceList() != null)
            {
                List<string> List = _nodePresenter.getInheritanceList();
                foreach (string inherit in List)
                {
                    AddInheritanceToEditMenu(inherit);
                }
            }
        }

        private void InheritanceNameTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox NameBox = sender as TextBox;
            if (NameBox.Parent != null)
            {
                StackPanel Inheritance = NameBox.Parent as StackPanel;
                string stringIndex = Inheritance.Name.Substring(Inheritance.Name.Count() - 1, 1);
                int index = int.Parse(stringIndex, null);
                _nodePresenter.ChangeInheritanceName(index, NameBox.Text);
            }
        }

        private void DeleteInheritance(object sender, RoutedEventArgs e)
        {
            Button delButton = sender as Button;
            StackPanel parent = delButton.Parent as StackPanel;
            //            TextBox name = parent.Children[3] as TextBox;
            string stringIndex = parent.Name.Substring(parent.Name.Count() - 1, 1);

            int index = int.Parse(stringIndex, null);
            _nodePresenter.RemoveInheritance(index);
            UpdateInheritanceInEditMenu();
        }
        #endregion InheritanceManagement

        #region GenericManagement
        private void DeleteGeneric(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            StackPanel parent = deleteButton.Parent as StackPanel;
            string stringIndex = parent.Name.Substring(parent.Name.Count() - 1, 1);
            StackPanel parentOfParent = parent.Parent as StackPanel;

            int index = int.Parse(stringIndex, null);
            _nodePresenter.RemoveGeneric(index);
            parentOfParent.Children.Clear();
            UpdateGenericListInEditMenu();
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
            NewGeneric.Name = "Generic" + (DeclGenericsPanel.Children.Count);
            VarianceIn.Content = "In";
            VarianceIn.Foreground = Brushes.White;
            VarianceIn.Margin = new Thickness(0, 0, 10, 0);
            VarianceIn.Checked += VarianceInChecked;
            VarianceOut.Content = "Out";
            VarianceOut.Foreground = Brushes.White;
            VarianceOut.Margin = new Thickness(0, 0, 10, 0);
            VarianceOut.Checked += VarianceOutChecked;
            VarianceNothing.Content = "Nothing";
            VarianceNothing.Foreground = Brushes.White;
            VarianceNothing.Margin = new Thickness(0, 0, 10, 0);
            VarianceNothing.Checked += VarianceNothingChecked;
            GenericName.Width = 80;
            GenericName.TextChanged += GenericNameChanged;
            GenericName.Name = "TextBoxGenericName";
            GenericName.Text = "Type a name";
            GenericName.Foreground = Brushes.Gray;
            GenericName.GotFocus += GenericNameGotFocus;
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
            DeclGenericsPanel.Children.Add(NewGeneric);
        }

        private void GenericNameGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox GenericName = sender as TextBox;
            GenericName.Text = "";
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
            NewGeneric.Name = "Generic" + (DeclGenericsPanel.Children.Count);
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
            DeclGenericsPanel.Children.Add(NewGeneric);
        }
        public void UpdateGenericListInEditMenu()
        {
            if (_nodePresenter != null)
            {
                List<Tuple<string, EGenericVariance>> tmp = _nodePresenter.getGenericList();
                if (tmp != null)
                {
                    foreach (Tuple<string, EGenericVariance> generic in tmp)
                    {
                        AddExistingGenericsToEditMenu(generic.Item1, generic.Item2);
                    }
                }
            }
        }
        private void GenericNameChanged(object sender, TextChangedEventArgs e)
        {
            TextBox name = sender as TextBox;

            if (name.Parent != null)
            {
                StackPanel parent = name.Parent as StackPanel;
                string stringIndex = parent.Name.Substring(parent.Name.Count() - 1, 1);

                int index = int.Parse(stringIndex, null);
                _nodePresenter.ModifGenericName(name.Text, index);
            }
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

        #endregion GenereciManagement

        #region AttributesManagement
        private void AttributeName_KeyDown(object sender, KeyEventArgs e)
        {
            var tmp = sender as TextBox;
            tmp.Foreground = Brushes.Red;
            if (e.Key == Key.Enter)
            {
                tmp.Foreground = Brushes.Black;
                _nodePresenter.AddAttribute(tmp.Text);
                UpdateAttributeEditPanel();
            }
        }

        private void UpdateAttributeEditPanel()
        {
            AttributeStack.Children.Clear();
            foreach (KeyValuePair<string, string> attribute in _nodePresenter.getAttributeList())
            {
                StackPanel NewAttributeLine = new StackPanel();
                TextBox NewAttribute = new TextBox();
                Button deleteAttribute = new Button();

                NewAttributeLine.Orientation = Orientation.Horizontal;
                NewAttributeLine.Name = "Attribute" + AttributeStack.Children.Count.ToString();
                if (attribute.Key != null)
                    NewAttribute.Text = attribute.Key + " ";
                NewAttribute.Text = attribute.Key;
                NewAttribute.KeyDown += AttributeName_KeyDown;
                NewAttribute.Width = 100;
                deleteAttribute.Click += DeleteAttribute;
                deleteAttribute.Margin = new Thickness(10, 0, 0, 0);
                deleteAttribute.Width = 20;
                deleteAttribute.Height = 20;
                deleteAttribute.Content = "X";
                NewAttributeLine.Children.Add(NewAttribute);
                NewAttributeLine.Children.Add(deleteAttribute);
                AttributeStack.Children.Add(NewAttributeLine);
            }
        }

        private void AddAttributeButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel NewAttributeLine = new StackPanel();
            TextBox NewAttribute = new TextBox();
            Button deleteAttribute = new Button();

            NewAttributeLine.Orientation = Orientation.Horizontal;
            NewAttributeLine.Name = "Attribute" + AttributeStack.Children.Count.ToString();
            NewAttribute.KeyDown += AttributeName_KeyDown;
            NewAttribute.Foreground = Brushes.Red;
            NewAttribute.Width = 100;
            NewAttribute.Text = "Type a name";
            NewAttribute.Foreground = Brushes.Gray;
            NewAttribute.GotFocus += AttributeNameGotFocus;
            deleteAttribute.Click += DeleteAttribute;
            deleteAttribute.Margin = new Thickness(10, 0, 0, 0);
            deleteAttribute.Width = 20;
            deleteAttribute.Height = 20;
            deleteAttribute.Content = "X";
            NewAttributeLine.Children.Add(NewAttribute);
            NewAttributeLine.Children.Add(deleteAttribute);
            AttributeStack.Children.Add(NewAttributeLine);
        }

        private void AttributeNameGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox AttributeName = sender as TextBox;

            AttributeName.Text = "";
        }

        private void DeleteAttribute(object sender, RoutedEventArgs e)
        {
            Button delButton = sender as Button;
            StackPanel parent = delButton.Parent as StackPanel;
            //            TextBox name = parent.Children[3] as TextBox;
            string stringIndex = parent.Name.Substring(parent.Name.Count() - 1, 1);

            int index = int.Parse(stringIndex, null);
            _nodePresenter.RemoveAttribute(index);
            UpdateAttributeEditPanel();
        }

        #endregion AttributesManagement

        #region ExecParamsManagement
        public void UpdateExecParamsNbInMenu()
        {
            ExecParamsStack.Children.Clear();
            for (int i = 0; i != _nodePresenter.getExecParamsNb(); ++i)
            {
                StackPanel NewExecParamLine = new StackPanel();
                Label NewExecParam = new Label();
                Button deleteExecParam = new Button();

                NewExecParamLine.Orientation = Orientation.Horizontal;
                NewExecParamLine.Name = "ExecParam" + i;
                NewExecParam.Width = 100;
                NewExecParam.Background = Brushes.White;
                NewExecParam.Content = "ExecParam n°" + (i + 1);
                deleteExecParam.Click += DeleteExecParam;
                deleteExecParam.Margin = new Thickness(10, 0, 0, 0);
                deleteExecParam.Width = 20;
                deleteExecParam.Height = 20;
                deleteExecParam.Content = "X";
                NewExecParamLine.Children.Add(NewExecParam);
                NewExecParamLine.Children.Add(deleteExecParam);
                ExecParamsStack.Children.Add(NewExecParamLine);
            }
        }

        private void AddExecParamButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel NewExecParamLine = new StackPanel();
            Label NewExecParam = new Label();
            Button deleteExecParam = new Button();

            NewExecParamLine.Orientation = Orientation.Horizontal;
            NewExecParamLine.Name = "ExecParam" + ExecParamsStack.Children.Count.ToString();
            NewExecParam.Width = 100;
            NewExecParam.Background = Brushes.White;
            NewExecParam.Content = "ExecParam n°" + (ExecParamsStack.Children.Count + 1).ToString();
            deleteExecParam.Click += DeleteExecParam;
            deleteExecParam.Margin = new Thickness(10, 0, 0, 0);
            deleteExecParam.Width = 20;
            deleteExecParam.Height = 20;
            deleteExecParam.Content = "X";
            NewExecParamLine.Children.Add(NewExecParam);
            NewExecParamLine.Children.Add(deleteExecParam);
            ExecParamsStack.Children.Add(NewExecParamLine);
            _nodePresenter.AddExecParam();
        }

        private void DeleteExecParam(object sender, RoutedEventArgs e)
        {
            Button delButton = sender as Button;
            StackPanel parent = delButton.Parent as StackPanel;
            StackPanel parentofparent = parent.Parent as StackPanel;
            int index = parentofparent.Children.IndexOf(parent);
            _nodePresenter.RemoveExecParam(index);
            UpdateExecParamsNbInMenu();
        }

        #endregion ExecParamsManagement

        #region CommentsManagement
        private void CommentsAreaTextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void _CommentText_GotFocus(object sender, RoutedEventArgs e)
        {
            _CommentText.Text = "";
        }

        #endregion CommentsManagement

        /* End of the modification by cancelling the EditMenu */
        private void QuitEditMenu(object sender, RoutedEventArgs e)
        {
                ((dynamic)this).IsOpen = false;
        }

        /* ?? */
        private void TextForDefault_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        // event caught by the validate buton with the textBox
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(_nodePresenter.GetView() is UnSupExpNode)) // Fix Forum
                return;
            var parser = new ICSharpCode.NRefactory.CSharp.CSharpParser();
            ICSharpCode.NRefactory.CSharp.AstNode node = null;
            if (_nodePresenter.GetASTNode().GetType().IsSubclassOf(typeof(ICSharpCode.NRefactory.CSharp.Expression)))
                node = parser.ParseExpression(TextForDefault.Text) as ICSharpCode.NRefactory.CSharp.AstNode;
            else if (_nodePresenter.GetASTNode().GetType().IsSubclassOf(typeof(ICSharpCode.NRefactory.CSharp.Statement)))
            {
                var stmts = parser.ParseStatements(TextForDefault.Text);
                if (stmts.Count() != 1)
                {
                    MessageBox.Show("You must type only one root statement for unsupported statement.");
                }
                node = stmts.First();
            }
            else
                return;
            if (parser.Errors.Count() != 0)
            {
                String totalErr = "";
                foreach (var err in parser.Errors)
                    totalErr += err + "\n";
                MessageBox.Show(totalErr);
            }
            if (node.Parent != null)
                this._nodePresenter.GetASTNode().ReplaceWith(node);
            this._nodePresenter.SetASTNode(node);
            this._nodePresenter.GetView().UpdateDisplayedInfosFromPresenter(); // TODO @Seb
            // TODO update what is shown in the node
        }
    }
}
