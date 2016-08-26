using System;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using code_in;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Code_in.VSCode_in
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(ConfigWindowPane), Style = VsDockStyle.MDI, Window = "3ae79031-e1bc-11d0-8f78-00a0c9110058", Transient = true)] // TODO Wrong GUID ?
    [ProvideToolWindow(typeof(NodalWindowPane), Style = VsDockStyle.MDI, Window = "3ae79031-e1bc-11d0-8f78-00a0c9110058", Transient = true)] // TODO Wrong GUID ?
    [Guid(GuidList.guidVSCode_inPkgString)]
    public sealed class VSCode_inPackage : Package, IEnvironmentWrapper
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public VSCode_inPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }



        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                CommandID openFileMenuCommandID = new CommandID(GuidList.guidVSCode_inCmdSet, (int)PkgCmdIDList.cmdidOpenFile);
                MenuCommand openFileMenuItem = new MenuCommand(OpenFileCallback, openFileMenuCommandID);
                mcs.AddCommand(openFileMenuItem);

                CommandID configMenuCommandID = new CommandID(GuidList.guidVSCode_inCmdSet, (int)PkgCmdIDList.cmdidConfigMenu);
                MenuCommand configMenuItem = new MenuCommand(ConfigCallback, configMenuCommandID);
                mcs.AddCommand(configMenuItem);

                CommandID newFileMenuCommandId = new CommandID(GuidList.guidVSCode_inCmdSet, (int)PkgCmdIDList.cmdidNewFile);
                MenuCommand newFileMenuItem = new MenuCommand(NewFileCallback, newFileMenuCommandId);
                mcs.AddCommand(newFileMenuItem);
            }
            Code_inApplication.StartApplication(this);
        }
        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>

        private void NewFileCallback(object sender, EventArgs e)
        {
            int i = 0;

            while (this.FindToolWindow(typeof(NodalWindowPane), i, false) != null)
                ++i;
            ToolWindowPane wp = this.CreateToolWindow(typeof(NodalWindowPane), i) as ToolWindowPane;
            if (wp != null)
            {
                IVsWindowFrame frame = wp.Frame as IVsWindowFrame;
                if (frame != null)
                {
                    frame.SetProperty((int)Microsoft.VisualStudio.Shell.Interop.__VSFPROPID.VSFPROPID_FrameMode, VSFRAMEMODE.VSFM_MdiChild);
                    frame.Show();
                    (wp as NodalWindowPane).PaneId = i;
                    (wp as NodalWindowPane).NewFile();
                }
            }
        }

        private void OpenFileCallback(object sender, EventArgs e)
        {
            int i = 0;

            while (this.FindToolWindow(typeof(NodalWindowPane), i, false) != null)
                ++i;
            
            ToolWindowPane wp = this.CreateToolWindow(typeof(NodalWindowPane), i) as ToolWindowPane;

            if (wp != null)
            {
                IVsWindowFrame frame = wp.Frame as IVsWindowFrame;
                if (frame != null)
                {
                    frame.SetProperty((int)Microsoft.VisualStudio.Shell.Interop.__VSFPROPID.VSFPROPID_FrameMode, VSFRAMEMODE.VSFM_MdiChild);
                    frame.Show();
                    (wp as NodalWindowPane).PaneId = i;
                    (wp as NodalWindowPane).OpenFile();
                }
            }
        }

        private void ConfigCallback(object sender, EventArgs e)
        {
            ToolWindowPane wp = this.FindToolWindow(typeof(ConfigWindowPane), 0, true);

            if (wp != null)
            {
                IVsWindowFrame frame = wp.Frame as IVsWindowFrame;
                if (frame != null)
                {
                    frame.SetProperty((int)Microsoft.VisualStudio.Shell.Interop.__VSFPROPID.VSFPROPID_FrameMode, VSFRAMEMODE.VSFM_MdiChild);
                    frame.Show();
                }
            }


        }

        public T CreateAndAddView<T>() where T : UserControl
        {
            Dictionary<String, Type> dict = new Dictionary<string, Type>()
            {
                {"MainView", typeof(NodalWindowPane)},
                {"ConfigView", typeof(ConfigWindowPane)}
            };
            int i = 0;

            while (this.FindToolWindow(typeof(NodalWindowPane), i, false) != null)
                ++i;

            ToolWindowPane wp = this.CreateToolWindow(dict[typeof(T).Name], i) as ToolWindowPane;

            if (wp != null)
            {
                IVsWindowFrame frame = wp.Frame as IVsWindowFrame;
                if (frame != null)
                {
                    frame.SetProperty((int)Microsoft.VisualStudio.Shell.Interop.__VSFPROPID.VSFPROPID_FrameMode, VSFRAMEMODE.VSFM_MdiChild);
                    frame.Show();
                }
                return wp.Content as T;
            }
            return null;
        }
        public void CloseView<T>(T view) where T : UserControl
        {
            
        }
    }

    public class ConfigWindowPane : ToolWindowPane // TODO move this elsewhere
    {
        private code_in.Views.ConfigView.ConfigView _configView = null;
        public ConfigWindowPane()
        {
            _configView = new code_in.Views.ConfigView.ConfigView();
            this.Content = _configView;
            this.Caption = "Configuration";
        }
    }

    public class NodalWindowPane : ToolWindowPane
    {
        private code_in.Views.MainView.MainView _mainView = null;
        private int _paneId = 0;
        static private List<OpenedFile> _fileList = new List<OpenedFile>();
        public int PaneId
        {
            get
            {
                return _paneId;
            }
            set
            {
                _paneId = value;
                this.Caption = "Blank" + _paneId;
            }
        }

        public NodalWindowPane()
        {
            _mainView = new code_in.Views.MainView.MainView();
            this.Content = _mainView;
            PaneId = 0;
            this._mainView.Unloaded += _mainView_Unloaded;
        }

        void _mainView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var _sender = sender as code_in.Views.MainView.MainView;
            foreach (var item in _fileList){
                if (item._filePath == _sender._filePath)
                    _fileList.Remove(item);
            }
        }

        public void OpenFile()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();

            bool? result = fileDialog.ShowDialog();

            if ((result == true) && (isFileAlreadyOpened(fileDialog.FileName) == false))
            {
                this.Caption = fileDialog.SafeFileName;
                this._mainView.OpenFile(fileDialog.FileName);
                _fileList.Add(new OpenedFile(PaneId, fileDialog.FileName));
            }
            //else //pas encore trouvé comment changer de focus
            //    foreach (var item in _fileList)
            //    {
            //        if (item._filePath == fileDialog.FileName)
            //            this.
            //    }
        }

        public void NewFile()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            
            fileDialog.Filter = "C# File (*.cs)|*.cs";

            bool? result = fileDialog.ShowDialog();

            if ((result ==  true) && (File.Exists(fileDialog.FileName) == false))
            {
                File.Create(fileDialog.FileName).Close();
                this.Caption = fileDialog.SafeFileName;
                this._mainView.OpenFile(fileDialog.FileName);
                _fileList.Add(new OpenedFile(PaneId, fileDialog.FileName));
            }
        }

        private bool isFileAlreadyOpened(string filePath){
            foreach (var file in _fileList){
                if (filePath == file._filePath)
                    return true;
            }
            return false;
        }


        public void ClosedFile() { }

        protected override void OnClose()
        {
            /* System.Windows.Forms.MessageBox.Show("Fermeture de fenetre");  TO PUSH WHEN IT WORKS
           
             foreach (var item in _fileList){
                 if (this._paneId == item._paneId)
                     _fileList.Remove(item);
             }
             base.OnClose();
             * 
                                                                                          /!\ TO PUSH WHEN IT WORKS /!\
             */
        }

    }

    /// <summary>
    /// Class used as in a list to prevent multiple instances of code_in tabs for one file
    /// </summary>
    public class OpenedFile{
        public int _paneId;
        public string _filePath;

        public OpenedFile(int paneId, string filePath){
            _paneId = paneId;
            _filePath = filePath;
        }
    }
}
