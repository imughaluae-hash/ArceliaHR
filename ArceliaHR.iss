[Setup]
; App Information
AppName=ArceliaHR
AppVersion=1.0.0
AppPublisher=ArceliaHR Inc.

DefaultDirName={autopf}\ArceliaHR
DefaultGroupName=ArceliaHR
OutputBaseFilename=ArceliaHR_Setup
Compression=lzma
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; All published files including exe, dlls, and dependencies
Source: "bin\Release\net8.0-windows\win-x64\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\ArceliaHR"; Filename: "{app}\ArceliaHR.exe"
Name: "{autodesktop}\ArceliaHR"; Filename: "{app}\ArceliaHR.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\ArceliaHR.exe"; Description: "{cm:LaunchProgram,ArceliaHR}"; Flags: nowait postinstall skipifsilent
