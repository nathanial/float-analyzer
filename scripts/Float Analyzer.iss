#define BinPath "..\Float Analyzer\bin\Debug"
#define FullName "Float Analyzer"

[Setup]
AppName={#FullName}
AppVersion=3.1.0RC2
AppId=Seeker Simulator
DefaultDirName={pf}\DChem\{#FullName}
DefaultGroupName=DChem
CreateUninstallRegKey=false
UpdateUninstallLogAppName=false
UsePreviousAppDir=true
AppVerName={#FullName}
DirExistsWarning=no
OutputBaseFilename={#FullName}
PrivilegesRequired=admin

[Files]
Source: {#BinPath}\*.*; DestDir: {app}; Flags: ignoreversion recursesubdirs overwritereadonly

[Icons]
Name: {group}\{#FullName}; Filename: {app}\Float Analyzer.exe; WorkingDir: {userdesktop}
Name: {group}\{#FullName}; Filename: {app}\Float Analyzer.exe; WorkingDir: {userprograms}
