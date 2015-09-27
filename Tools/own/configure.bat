:: Author Sebastien Maire
:: Contribs

@echo off

set pathDevEnv=
:: Used to store devenv.exe path
set pathDevEnvFilePath=pathDevEnv.cfg
:: Used to store .AddIn file to configure VS AddIn
set pathFileVsAddin=pathVSAddin.cfg

set workingDir=..\..\VSAddIn
set configFile=..\..\VSAddIn\VSAddIn.csproj.user
set CURRENTDIR=%cd%

:setPath
if exist %pathDevEnvFilePath% (
echo Found %pathDevEnvFilePath% that stores devenv.exe path
) else (
echo configure.bat did not found %pathDevEnvFilePath% and cannot configure.
echo Please give the path to the folder that contains devenv.exe  generally "X:/.../Microsoft Visual Studio xx.0/Common7/IDE/"
folderBrowser.exe > %pathDevEnvFilePath%
)

set /p pathDevEnv=< %pathDevEnvFilePath%

if not exist "%pathDevEnv%\devenv.exe" (
echo  "%pathDevEnv%\devenv.exe"
echo Wrong path, you must give a valid path to devenv.exe
del %pathDevEnvFilePath%
goto :setPath
)

:setAddinPath
if exist %pathFileVsAddin% (
echo Found %pathFileVsAddin% that stores addin loader file.
) else (
echo Please give the path to Visual Studio Addins generally X:/...My Documents/Visual Studio 20XX/Addins/
folderBrowser.exe > %pathFileVsAddin%
)

set /p pathVSAddin=< %pathFileVsAddin%

echo ^<^?xml version="1.0" encoding="UTF-16" standalone="no"^?^> > "%pathVsAddin%/code_inTMP.AddIn"
echo ^<Extensibility xmlns="http://schemas.microsoft.com/AutomationExtensibility"^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 ^<HostApplication^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 	^<Name^>Microsoft Visual Studio^</Name^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 	^<Version^>12.0^</Version^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 ^</HostApplication^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 ^<Addin^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 	^<FriendlyName^>code_in - No Name provided.^</FriendlyName^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 	^<Description^>code_in - No Description provided.^</Description^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 	^<Assembly^>%CURRENTDIR%\%workingDir%\bin\Debug\VScode_in.dll^</Assembly^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 	^<FullClassName^>code_in.Connect^</FullClassName^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 	^<LoadBehavior^>0^</LoadBehavior^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 	^<CommandPreload^>1^</CommandPreload^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 	^<CommandLineSafe^>0^</CommandLineSafe^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo	 ^</Addin^> >> "%pathVsAddin%/code_inTMP.AddIn"
echo ^</Extensibility^> >> "%pathVsAddin%/code_inTMP.AddIn"

cd ..\ext
iconv.exe -t UCS-2LE "%pathVsAddin%\code_inTMP.AddIn" > "%pathVsAddin%\code_in.AddIn"
del "%pathVsAddin%\code_inTMP.AddIn"
cd ..\own

echo ^<^?xml version="1.0" encoding="utf-8"?^> > %configFile%
echo ^<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003"^> >> %configFile%
echo   ^<PropertyGroup Condition="'$(Configuration)|$(Platform)' == 'Debug|AnyCPU'"^> >> %configFile%
echo     ^<StartAction^>Program^</StartAction^> >> %configFile%
echo     ^<StartProgram^>%pathDevEnv%\devenv.exe^</StartProgram^> >> %configFile%
echo     ^<StartArguments^>/resetaddin code_in.Connect^</StartArguments^> >> %configFile%
echo     ^<StartWorkingDirectory^>%CURRENTDIR%\%workingDir%^</StartWorkingDirectory^> >> %configFile%
echo   ^</PropertyGroup^> >> %configFile%
echo ^</Project^> >> %configFile%

echo Configuring done

pause