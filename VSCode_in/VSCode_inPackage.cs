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
using code_in.Views;
using code_in.Views.NodalView;
using code_in.Views.ConfigView;
using code_in.Presenters.Nodal;

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

        private Dictionary<string, NodalWindowPane> _openedFiles = new Dictionary<string, NodalWindowPane>();
        private HashSet<int> _takenIndexes = new HashSet<int>();

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

        NodalWindowPane _createNewEmptyNodalWindow()
        {
            int index = 0;
            if (_takenIndexes.Count != 0)
            {
                for (int i = 0; true; ++i)
                {
                    if (!_takenIndexes.Contains(i))
                    {
                        index = i;
                        break;
                    }
                }
            }
            _takenIndexes.Add(index);
            var nwp = this.CreateToolWindow(typeof(NodalWindowPane), index) as NodalWindowPane;
            nwp.PaneId = index;
            nwp.FocusCode_inWindow();
            return nwp;
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
                this._createOrFocusFileNodalWindowPane(filePath);
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
                NodalWindowPane matchingWindowsPane;
                _openedFiles.TryGetValue(filePath, out matchingWindowsPane);
                if (matchingWindowsPane != null)
                    matchingWindowsPane.FocusCode_inWindow();
                else
                {
                    var toolWindow = _createOrFocusFileNodalWindowPane(filePath);
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
            _createOrFocusConfigWindowPane();
        }

        private ConfigWindowPane _createOrFocusConfigWindowPane()
        {
            ConfigWindowPane cwp = this.FindToolWindow(typeof(ConfigWindowPane), 0, true) as ConfigWindowPane;

            cwp.FocusCode_inWindow();
            return cwp;
        }

        private NodalWindowPane _createOrFocusFileNodalWindowPane(String filePath)
        {
            NodalWindowPane wp = null;
            try
            {
                if (_openedFiles.TryGetValue(filePath, out wp))
                    wp.FocusCode_inWindow();
                else
                {
                    wp = _createNewEmptyNodalWindow();
                    var presenter = new DeclarationsNodalPresenterLocal();
                    var nodalView = new DeclarationsNodalView(Code_inApplication.MainResourceDictionary);
                    wp.Code_inView = nodalView;
                    code_in.Views.NodalView.NodalViewActions.AttachNodalViewAndPresenter(nodalView, presenter);
                    wp.OpenFile(filePath);
                    _openedFiles.Add(filePath, wp);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return wp;
        }

        #endregion Menu callbacks
        #region IEnvironmentWrapper
        public T CreateAndAddView<T>(params object[] args) where T : UserControl, code_in.Views.ICode_inWindow
        {
            ACode_inWindowPane wp = null;

            try
            {
                if (typeof(T) == typeof(ConfigView))
                    wp = _createOrFocusConfigWindowPane();
                else
                {
                    if (typeof(T) == typeof(DeclarationsNodalView))
                        wp = _createOrFocusFileNodalWindowPane(args[0] as String);
                    else if (typeof(T) == typeof(ExecutionNodalView))
                    {
                        // Do not check if it exist (the doublon avoid is done in "Code_in.Core"
                        wp = this._createNewEmptyNodalWindow();
                        wp.Code_inView = new ExecutionNodalView(Code_inApplication.MainResourceDictionary);
                    }
                }
                if (wp != null)
                    return wp.Code_inView as T;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return null;
        }
        public void CloseView<T>(T view) where T : UserControl, ICode_inWindow
        {
            view.EnvironmentWindowWrapper.CloseCode_inWindow();
        }
        #endregion IEnvironmentWrapper
    }

    public class ConfigWindowPane : ACode_inWindowPane
    {
        public ConfigWindowPane() :
            base()
        {
            try
            {
                this.Code_inView = new code_in.Views.ConfigView.ConfigView(Code_inApplication.MainResourceDictionary);
                this.SetTitle("Configuration");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public ConfigWindowPane(System.IServiceProvider serviceProvider) :
            base(serviceProvider)
        {
            this.Code_inView = new code_in.Views.ConfigView.ConfigView(Code_inApplication.MainResourceDictionary);
            this.SetTitle("Configuration"); // TODO langues
        }
    }

    public abstract class ACode_inWindowPane : ToolWindowPane, IEnvironmentWindowWrapper
    {
        public ACode_inWindowPane() :
            base()
        {
            _code_inView = null;
            _title = "";
        }
        public ACode_inWindowPane(System.IServiceProvider serviceProvider) :
            base(serviceProvider)
        {
            _code_inView = null;
            _title = "";
        }
        protected ICode_inWindow _code_inView;
        public ICode_inWindow Code_inView
        {
            get
            {
                return _code_inView;
            }
            set
            {
                if (_code_inView != null)
                    throw new Exception("Code_inView can only be set one time.");
                _code_inView = value;
                _code_inView.EnvironmentWindowWrapper = this;
                this.Content = _code_inView;
            }
        }
        public void FocusCode_inWindow()
        {
            IVsWindowFrame frame = this.Frame as IVsWindowFrame;
            if (frame != null)
            {
                frame.SetProperty((int)Microsoft.VisualStudio.Shell.Interop.__VSFPROPID.VSFPROPID_FrameMode, VSFRAMEMODE.VSFM_MdiChild);
                frame.Show();
            }
        }

        public void CloseCode_inWindow()
        {
            if (!this._code_inView.IsSaved)
            {
                var msgBoxRes = MessageBox.Show("The file you are closing is not saved. Do you want to save it ?", "File not saved", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (msgBoxRes == MessageBoxResult.Cancel)
                    return;
                if (msgBoxRes == MessageBoxResult.Yes)
                    this._code_inView.Save();
                IVsWindowFrame frame = this.Frame as IVsWindowFrame;
                if (frame != null)
                    frame.CloseFrame((uint)Microsoft.VisualStudio.Shell.Interop.__FRAMECLOSE.FRAMECLOSE_NoSave);
            }
        }

        string _title;
        // To update annotations (saved or other)
        public void UpdateTitleState()
        {
            this.Caption = _title + (!_code_inView.IsSaved ? "*" : "");
        }

        public void SetTitle(string title)
        {
            _title = title;
            UpdateTitleState();
        }
    }

    public class NodalWindowPane : ACode_inWindowPane
    {
        public int PaneId; // TODO init to -1
        public NodalWindowPane(System.IServiceProvider provider) :
            base(provider)
        {
            this.Content = new UserControl();
        }
        public NodalWindowPane() :
            base()
        {
            this.Content = new UserControl();

        }

        public void OpenFile(string filePath)
        {
            Debug.Assert((this._code_inView.GetType() == typeof(DeclarationsNodalView)));
            (this._code_inView as DeclarationsNodalView).OpenFile(filePath);
            this.SetTitle(Path.GetFileName(filePath));
        }
    }
}
