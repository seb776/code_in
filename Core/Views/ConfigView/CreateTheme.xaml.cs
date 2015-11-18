using System;
using System.Collections.Generic;
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

namespace code_in.Views.ConfigView
{
    /// <summary>
    /// Logique d'interaction pour CreateTheme.xaml
    /// </summary>
    public partial class CreateTheme : UserControl
    {
        private List<string> themeList = new List<string>();
        private string name = "";
        public CreateTheme()
        {
            int i = 0;
            InitializeComponent();
            themeList.Add("DarkTheme");
            themeList.Add("LightTheme");
            while (i != themeList.Count)
            {
                BoxTheme.Items.Add(themeList[i]);
                ++i;
            }
            ThemeName.AddHandler(TextBox.TextInputEvent,
                   new TextCompositionEventHandler(ThemeName_TextInput_1),
                   true);
        }

        private void DeleteTheme(object sender, RoutedEventArgs e)
        {
            if (BoxTheme.SelectedItem.ToString() != null)
            {
                themeList.Remove(BoxTheme.SelectedItem.ToString());
                BoxTheme.Items.Remove(BoxTheme.SelectedItem);
            }
        }

        private void ThemeName_TextInput_1(object sender, TextCompositionEventArgs e)
        {
            ThemeName.KeyUp += new KeyEventHandler(ThemeName_KeyDown);

            if (e.Text != "\n")
            {
                name += e.Text;
            }
        }

        private void ThemeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PreviewName.Text = "Aperçu de " + name;
            }
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
