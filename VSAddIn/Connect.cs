using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;
using code_in.Views.MainView;
using System.Windows.Controls;

namespace code_in
{
	/// <summary>The object for implementing an Add-in.</summary>
	/// <seealso class='IDTExtensibility2' />
	public class Connect : IDTExtensibility2, IDTCommandTarget, IEnvironmentWrapper
	{
		/// <summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
		public Connect()
		{
		}

        public T CreateAndAddView<T>() where T : UserControl
        {
            object myUC = null;

            Windows2 vsWindows = _applicationObject.ItemOperations.DTE.Windows as Windows2;

            Window myWindow = vsWindows.CreateToolWindow2(_addInInstance,
                "bin/code_inCore.dll",//Assembly.GetExecutingAssembly().Location, // This path is used to get the dll where the Window is contained
                typeof(MainView).FullName,
                "NewTab",
                Guid.NewGuid().ToString(),
                ref myUC);
            myWindow.Visible = true;
            myWindow.IsFloating = false;
            myWindow.Linkable = false;
            return myUC as T;
        }
        public void CloseView<T>(T view) where T : UserControl
        {

        }
        void RenameView<T>(T view, String name) where T : UserControl
        {

        }


		/// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
		/// <param term='application'>Root object of the host application.</param>
		/// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
		/// <param term='addInInst'>Object representing this Add-in.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
            Code_inApplication.StartApplication(this);
			_applicationObject = (DTE2)application;
			_addInInstance = (AddIn)addInInst;
			if(connectMode == ext_ConnectMode.ext_cm_UISetup)
			{
				object []contextGUIDS = new object[] { };
				Commands2 commands = (Commands2)_applicationObject.Commands;
				string toolsMenuName = "Tools";

				//Place the command on the tools menu.
				//Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
				Microsoft.VisualStudio.CommandBars.CommandBar menuBarCommandBar = ((Microsoft.VisualStudio.CommandBars.CommandBars)_applicationObject.CommandBars)["MenuBar"];

				//Find the Tools command bar on the MenuBar command bar:
				CommandBarControl toolsControl = menuBarCommandBar.Controls[toolsMenuName];
				CommandBarPopup toolsPopup = (CommandBarPopup)toolsControl;

				//This try/catch block can be duplicated if you wish to add multiple commands to be handled by your Add-in,
				//  just make sure you also update the QueryStatus/Exec method to include the new command names.
				try
				{
					//Add a command to the Commands collection:
                    Command command = commands.AddNamedCommand2(_addInInstance, "code_in", "code_in", "Executes the command for code_in",
                        false, TranslationTier.Resources._1, ref contextGUIDS,
                        (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled,
                        (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);
                    Command menuConfig = commands.AddNamedCommand2(_addInInstance, "Parameters", "Parameters", "Parameters for code_in",
                        false, TranslationTier.Resources._1, ref contextGUIDS,
                        (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled,
                        (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);
                    CommandBar oBar = null;
                    for (int iloop = 1; iloop <= toolsPopup.CommandBar.Controls.Count; iloop++)
                    {
                        if (toolsPopup.CommandBar.Controls[iloop].Caption ==
                                               "Code_in")
                        {
                            CommandBarPopup op =
                                (CommandBarPopup)toolsPopup.CommandBar.Controls[iloop];
                            oBar = op.CommandBar;
                            break;
                        }
                    }
                    if (oBar == null)
                        oBar = (CommandBar)commands.AddCommandBar("Code_in", vsCommandBarType.vsCommandBarTypeMenu, toolsPopup.CommandBar, 1);

					//Add a control for the command to the tools menu:
                    if ((command != null) && (toolsPopup != null))
                    {
                        //command.AddControl(toolsPopup.CommandBar, 1);
                        command.AddControl(oBar, 1);
                    }
                    if (menuConfig != null)
                    {
                        menuConfig.AddControl(oBar, 2);
                    }
                }
				catch(System.ArgumentException e)
				{
                    //MessageBox.Show(e.ToString());
					//If we are here, then the exception is probably because a command with that name
					//  already exists. If so there is no need to recreate the command and we can 
                    //  safely ignore the exception.
				}
			}
		}

		/// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
		/// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
		}

		/// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />		
		public void OnAddInsUpdate(ref Array custom)
		{
		}

		/// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnStartupComplete(ref Array custom)
		{
		}

		/// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnBeginShutdown(ref Array custom)
		{
		}
		
		/// <summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
		/// <param term='commandName'>The name of the command to determine state for.</param>
		/// <param term='neededText'>Text that is needed for the command.</param>
		/// <param term='status'>The state of the command in the user interface.</param>
		/// <param term='commandText'>Text requested by the neededText parameter.</param>
		/// <seealso class='Exec' />
		public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
		{
			if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
			{
                if (commandName == "code_in.Connect.code_in" || commandName == "code_in.Connect.Parameters")
				{
					status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported|vsCommandStatus.vsCommandStatusEnabled;
					return;
				}
			}
		}

		/// <summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
		/// <param term='commandName'>The name of the command to execute.</param>
		/// <param term='executeOption'>Describes how the command should be run.</param>
		/// <param term='varIn'>Parameters passed from the caller to the command handler.</param>
		/// <param term='varOut'>Parameters passed from the command handler to the caller.</param>
		/// <param term='handled'>Informs the caller if the command was handled or not.</param>
		/// <seealso class='Exec' />
		public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
		{
			handled = false;
			if(executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
			{
                 
                if (commandName == "code_in.Connect.Parameters")
                {
                    object myUC = null;
                    Windows2 vsWindows = _applicationObject.ItemOperations.DTE.Windows as Windows2;

                    Window myWindow = vsWindows.CreateToolWindow2(_addInInstance,
                        "bin/code_inCore.dll",//Assembly.GetExecutingAssembly().Location, // This path is used to get the dll where the Window is contained
                        typeof(Views.ConfigView.ConfigView).FullName,
                        "Code_in - Parameters",
                        Guid.NewGuid().ToString(),
                        ref myUC);
                    myWindow.Visible = true;
                    myWindow.IsFloating = false;
                    myWindow.Linkable = false;
                    handled = true;

                    if (myUC == null)
                        throw new Exception("Cannot get a reference to the UI");
                }
				else if(commandName == "code_in.Connect.code_in")
				{
                    object myUC = null;
                    Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();

                    bool? result = fileDialog.ShowDialog();

                    if (result == true)
                    {
                        //_applicationObject.ItemOperations.NewFile();
                        Windows2 vsWindows = _applicationObject.ItemOperations.DTE.Windows as Windows2;

                        Window myWindow = vsWindows.CreateToolWindow2(_addInInstance,
                            "bin/code_inCore.dll",//Assembly.GetExecutingAssembly().Location, // This path is used to get the dll where the Window is contained
                            typeof(MainView).FullName,
                            fileDialog.SafeFileName,
                            Guid.NewGuid().ToString(),
                            ref myUC);
                        myWindow.Visible = true;
                        myWindow.IsFloating = false;
                        myWindow.Linkable = false;
                        handled = true;

                        if (myUC == null)
                            throw new Exception("Cannot get a reference to the UI");
                        ((code_in.Views.MainView.MainView)myUC).OpenFile(fileDialog.FileName); // To give the name of the file to the UserControl (Core)
                    }
					return;
				}
			}
		}
		private DTE2 _applicationObject;
		private AddIn _addInInstance;
	}
}