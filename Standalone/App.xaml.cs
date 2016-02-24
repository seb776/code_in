using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace code_in
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IEnvironmentWrapper
    {
        MainWindow _window;

        public void App_Startup(object sender, StartupEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? result = fileDialog.ShowDialog();

            if (result == true)
            {
                Code_inApplication.StartApplication(this);
                _window = new MainWindow();
                var codeWin = new code_in.Views.MainView.MainView();
                TabItem nItem = new TabItem();
                nItem.Header = fileDialog.FileName;
                nItem.Content = codeWin;
                this._window.TabCtrl.Items.Add(nItem);
                //_window.MainGrid.Children.Add(codeWin);
                codeWin.OpenFile(fileDialog.FileName);
                _window.Show();
            }
        }

        public T CreateAndAddView<T>() where T : UserControl
        {
            T win = (T)Activator.CreateInstance(typeof(T), code_in.Resources.SharedDictionaryManager.MainResourceDictionary);

            TabItem nItem = new TabItem();
            nItem.Header = "NewTab";
            nItem.Content = win;
            this._window.TabCtrl.Items.Add(nItem);
            return win;
        }
        public void CloseView<T>(T view) where T : UserControl
        {

        }
    }
}
