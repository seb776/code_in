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
using System.Windows;

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

        List<OpenedFile> _fileList = new List<OpenedFile>();
        
        private bool _askForNewFile(ref string filePath, ref string fileName)
        {
            System.Windows.Forms.SaveFileDialog fileDialog = new System.Windows.Forms.SaveFileDialog();

            fileDialog.Filter = "C# File (*.cs)|*.cs";
            var res = fileDialog.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK || res == System.Windows.Forms.DialogResult.Yes)
            {
                filePath = fileDialog.FileName;
                fileName = Path.GetFileName(fileDialog.FileName);
                return true;
            }
            return false;
        }

        NodalWindowPane _createNodalWindow()
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
                }
            }
            return wp as NodalWindowPane;
        }


        private bool _askForOpenFile(ref string filePath, ref string fileName)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

            ofd.Filter = "C# File (*.cs)|*.cs";
            var res = ofd.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK && File.Exists(ofd.FileName))
            {
                filePath = ofd.FileName;
                fileName = Path.GetFileName(ofd.FileName);
                return true;
            }
            return false;
        }
        #region Menu callbacks
        private void NewFileCallback(object sender, EventArgs e)
        {
            string filePath = "";
            string fileName = "";
            if (_askForNewFile(ref filePath, ref fileName))
            {
                File.Create(filePath).Close();
                var toolWindow = this._createNodalWindow();
                toolWindow.Caption = fileName;
                toolWindow.OpenFile(filePath);
                _fileList.Add(new OpenedFile(filePath, toolWindow));
            }
            else
            {
                // TODO custom display
                MessageBox.Show("You must specify a valid file to create.");
            }


        }
        private void OpenFileCallback(object sender, EventArgs e)
        {
            string filePath = "";
            string fileName = "";
            if (_askForOpenFile(ref filePath, ref fileName))
            {
                var openedFile = _fileList.Find((of) => { return of._filePath == filePath; });
                if (openedFile != null) // If already opened we show the original
                    ((openedFile._windowPane.Frame) as IVsWindowFrame).Show();
                else
                {
                    var toolWindow = _createNodalWindow();
                    toolWindow.Caption = fileName;
                    toolWindow.OpenFile(filePath);
                    _fileList.Add(new OpenedFile(filePath, toolWindow));
                }
            }
            else
            {
                // TODO custom display
                MessageBox.Show("You must specify a valid file to open.");
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
        #endregion Menu callbacks
        #region IEnvironmentWrapper
        public T CreateAndAddView<T>() where T : UserControl
        {
            Dictionary<String, Type> dict = new Dictionary<string, Type>()
            {
                {"NodalView", typeof(NodalWindowPane)},
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
            //_fileList[0].
            // TODO @Seb
            //OpenedFile file = _fileList.Find((of) => { return of._filePath == ""; });
            //if (file != null)
            //    _fileList.Remove(file);
            //else
            //{
            //    // TODO Error message
            //}
        }
        #endregion IEnvironmentWrapper
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
        public code_in.Views.NodalView.NodalView _mainView = null;
        private int _paneId = 0;
        static private List<OpenedFile> _fileList = new List<OpenedFile>();
        public int PaneId
        {
            get {
                return _paneId;
            }
            set {
                _paneId = value;
                this.Caption = "Blank" + _paneId;
            }
        }

        public NodalWindowPane()
        {
            _mainView = new code_in.Views.NodalView.NodalView(Code_inApplication.MainResourceDictionary);
            this.Content = _mainView;
            PaneId = 0;
        }

        public void OpenFile(string filePath)
        {
            this._mainView.OpenFile(filePath);
        }
    }

    /// <summary>
    /// Class used as in a list to prevent multiple instances of code_in tabs for one file
    /// </summary>
    public class OpenedFile
    {
        public NodalWindowPane _windowPane;
        public string _filePath;

        public OpenedFile(string filePath, NodalWindowPane pane)
        {
            _windowPane = pane;
            _filePath = filePath;
        }
    }
}
