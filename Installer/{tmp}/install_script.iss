

#define Version "1.3.4"
#define AppName "File2Startup"
#define ExeName "File2Startup"
#define StartMenuGroup "• ElektroStudios"

#define AuthorWebsite "https://codecanyon.net/user/elektrostudios/portfolio"

[Languages]
Name: en; MessagesFile: compiler:Default.isl
Name: es; MessagesFile: compiler:Languages\Spanish.isl
Name: pt; MessagesFile: compiler:Languages\Portuguese.isl
Name: bra; MessagesFile: compiler:Languages\BrazilianPortuguese.isl
Name: rus; MessagesFile: compiler:Languages\Russian.isl
Name: jap; MessagesFile: compiler:Languages\Japanese.isl
Name: ita; MessagesFile: compiler:Languages\Italian.isl
Name: fre; MessagesFile: compiler:Languages\French.isl
Name: pl; MessagesFile: compiler:Languages\Polish.isl
Name: ukr; MessagesFile: compiler:Languages\Ukrainian.isl
Name: dt; MessagesFile: compiler:Languages\Dutch.isl
Name: ger; MessagesFile: compiler:Languages\German.isl

[LangOptions]
DialogFontName=Segoe UI
DialogFontSize=10
WelcomeFontName=Verdana
WelcomeFontSize=16
TitleFontName=Arial
TitleFontSize=29
CopyrightFontName=Arial
CopyrightFontSize=10

[Setup]
AppName={#AppName}
AppID={#AppName}
AppVerName={#AppName} {#Version}
SetupMutex={#AppName} {#Version}
AppVersion={#Version}
VersionInfoVersion={#Version}
AppCopyright=© ElektroStudios 2024
VersionInfoCopyright=© ElektroStudios 2024
AppPublisher=ElektroStudios
DefaultDirName={autopf}\ElektroStudios\{#AppName}
DefaultGroupName=ElektroStudios Installers\{#StartMenuGroup}
UninstallDisplayIcon={app}\{#ExeName}.exe
OutputBaseFilename={#AppName} v{#Version} AnyCPU Installer
Compression=zip
InternalCompressLevel=normal
SolidCompression=false
AlwaysShowComponentsList=False
DisableWelcomePage=False
UserInfoPage=False
DisableDirPage=False
AllowRootDirectory=False
AllowNetworkDrive=False
AllowUNCPath=False
AppendDefaultDirName=True
AppendDefaultGroupName=False
UsePreviousAppDir=False
UsePreviousGroup=False
WizardImageStretch=True
TerminalServicesAware=True
MinVersion=0,6.1sp1
Password=
DisableProgramGroupPage=false
AllowNoIcons=True
DirExistsWarning=True
DisableReadyPage=False
AlwaysShowDirOnReadyPage=True
AlwaysShowGroupOnReadyPage=True
WizardStyle=Classic
SetupLogging=true
UninstallLogMode=New
ASLRCompatible=True
DEPCompatible=True
LZMAUseSeparateProcess=True
MissingMessagesWarning=True
MissingRunOnceIdsWarning=True
NotRecognizedMessagesWarning=True
UsedUserAreasWarning=True
CompressionThreads=Auto
CloseApplications=True
CloseApplicationsFilter=*.*
DefaultDialogFontName=Tahoma
DisableStartupPrompt=True
FlatComponentsList=False
LanguageDetectionMethod=UILanguage
PrivilegesRequired=PowerUser
RestartIfNeededByRun=True
ShowLanguageDialog=Yes
ShowTasksTreeLines=True
SetupIconFile=Icon.ico
WizardImageFile=embedded\WizardImage.bmp
WizardSmallImageFile=embedded\WizardSmallImage.bmp
InfoBeforeFile=embedded\InfoBefore.rtf
Uninstallable=True
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64

[Files]
; Temp files
Source: {tmp}\*; DestDir: {tmp}; Flags: recursesubdirs createallsubdirs ignoreversion deleteafterinstall

; Program
Source: {app}\*; DestDir: {app}; Flags: recursesubdirs createallsubdirs ignoreversion

[Registry]
; ContextMenu Integration
Root: HKCR; SubKey: SystemFileAssociations\.exe\Shell\{#AppName}; ValueType: string; ValueName: MUIVerb; ValueData: {#AppName}; Flags: uninsdeletekey; Tasks: exe
Root: HKCR; SubKey: SystemFileAssociations\.exe\Shell\{#AppName}; ValueType: string; ValueName: SubCommands; ValueData: "{#ExeName}.Run;{#ExeName}.OpenFile"; Flags: uninsdeletekey; Tasks: exe
Root: HKCR; SubKey: SystemFileAssociations\.exe\Shell\{#AppName}; ValueType: string; ValueName: Icon; ValueData: {app}\{#ExeName}.exe; Flags: uninsdeletekey; Tasks: exe
Root: HKCR; SubKey: SystemFileAssociations\.exe\Shell\{#AppName}; ValueType: string; ValueName: Position; ValueData: bottom; Flags: uninsdeletekey; Tasks: exe

; Shell Commands
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.Run; ValueType: string; ValueName: ; ValueData: Run {#AppName}; Flags: uninsdeletekey; Tasks: exe
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.Run; ValueType: string; ValueName: icon; ValueData: {app}\{#ExeName}.Run.ico; Flags: uninsdeletekey; Tasks: exe
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.Run\command; ValueType: string; ValueName: ; ValueData: """{app}\{#ExeName}.exe"""; Flags: uninsdeletekey; Tasks: exe

Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.OpenFile; ValueType: string; ValueName: ; ValueData: Open file in {#AppName}; Flags: uninsdeletekey; Tasks: exe
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.OpenFile; ValueType: string; ValueName: icon; ValueData: {app}\{#ExeName}.OpenFile.ico; Flags: uninsdeletekey; Tasks: exe
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.OpenFile\command; ValueType: string; ValueName: ; ValueData: """{app}\{#ExeName}.exe"" ""%1"""; Flags: uninsdeletekey; Tasks: exe

[Tasks]
Name: exe; Description: Executable files (.exe); GroupDescription: ContextMenu Integration:

[UninstallDelete]
Name: {localappdata}\ElektroStudios\{#ExeName}.exe*; Type: filesandordirs
Name: {localappdata}\ElektroStudios; Type: dirifempty

[Run]
Filename: {app}\{#ExeName}.exe; Description: {cm:LaunchProgram,{#AppName}}; Flags: postinstall unchecked skipifsilent nowait

[Icons]
Name: {autodesktop}\{#AppName}; Filename: {app}\{#ExeName}.exe; WorkingDir: {app}; IconFilename: {app}\{#ExeName}.exe; Check: IsCreateDesktopShortcutChecked
Name: {group}\{#AppName}; Filename: {app}\{#ExeName}.exe; WorkingDir: {app}; IconFilename: {app}\{#ExeName}.exe

[Code]

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Variables ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

Var
  // Custom Pages
  CustomSelectDirPage: TWizardPage;

  // Custom Controls
  DesktopShortcutCheckBox: TNewCheckBox;

  // Custom Vars
  UninstallSuccess : Boolean; // Determines whether the uninstallation succeeded.

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Utility Functions ----------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// - - - - - - - - - - - - - - - - - - - - //
// Determines whether a directory is empty //
// - - - - - - - - - - - - - - - - - - - - //
function IsDirEmpty(dirName: String): Boolean;
var
  FindRec: TFindRec;
  FileCount: Integer;
begin
  Result := Not DirExists(dirName);
  if FindFirst(dirName+'\*', FindRec) then begin
    try
      repeat
        if (FindRec.Name <> '.') and (FindRec.Name <> '..') then begin
          FileCount := 1;
          break;
        end;
      until not FindNext(FindRec);
    except
      ShowExceptionMessage();
    finally
      FindClose(FindRec);
      if FileCount = 0 then Result := True;
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Determine whether the source directory path is a root directory (eg. 'C:\') //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
Function IsRootDir(Path: String): Boolean;
var
  PathLength: LongInt;
begin
  StringChangeEx(Path, ':', '', True);
  Path := RemoveBackslash(Path);
  PathLength := Length(Path);
  Result := (PathLength <= 1)
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - //
// Converts a Yes/No or True/False string to Boolean //
// - - - - - - - - - - - - - - - - - - - - - - - - - //
function YesNoOrTrueFalseToBool(Value: String): Boolean;
begin
  if (LowerCase(Value) = 'true') or (LowerCase(Value) = 'yes') or (LowerCase(Value) = 'auto') then begin
    Result := True;

  end else if (LowerCase(Value) = 'false') or (LowerCase(Value) = 'no') then begin
    Result := False;

  end else begin
    RaiseException('Invalid string format for parameter Value ("' + Value + '") in "YesNoOrTrueFalseToBool" function.');
  end;
end;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Custom Event-Handlers ------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Occurs when the 'AuthorWebsiteLabel' and 'AuthorWebsiteBitmap' controls are clicked //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure AuthorWebsiteControlClick(Sender: TObject);
var
  ErrorCode: Integer;
begin
  // This will open the specified url in the default web-browser.
  // https://stackoverflow.com/a/38934870/1248295
  ShellExec('open', '{#AuthorWebsite}', '', '', SW_SHOWNORMAL, ewNoWait, ErrorCode);
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -- //
// Occurs when the 'Next' button in the 'CustomSelectDirPage' page is pressed  //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function CustomSelectDirPageNextButtonClick(Sender: TWizardPage): Boolean;
begin
  Result := True;

  // Ignore validations if installing in a temporary directory or in Windows directory.
  if (Pos('{tmp}', LowerCase(WizardDirValue)) <> 0) or (Pos('.tmp', LowerCase(WizardDirValue)) <> 0) then Exit;
  if (Pos('{win}', LowerCase(WizardDirValue)) <> 0) or (Pos(LowerCase(ExpandConstant('{win}')), LowerCase(WizardDirValue)) <> 0) then Exit;

  // Prevents the user from installing the program in a root directory if 'AllowRootDirectory' directive is set to 'False'.
  If not YesNoOrTrueFalseToBool('{#SetupSetting("AllowRootDirectory")}') and IsRootDir(WizardDirValue) then begin
    MsgBox('Setup does not allow {#AppName} to be installed on a root directory ("' + WizardDirValue + '").', mbCriticalError, MB_OK);
    Result := False;
    Exit;
  end;

  // Optionally, forces the user to install the program on a empty directory.
  // Result := IsDirEmpty(ExpandConstant(WizardDirValue));
  // If not Result then begin
  //   MsgBox('{#AppName} can only be installed to an empty directory.', mbCriticalError, MB_OK);
  //   Exit;
  // end;

  // Prevents the user from installing the program in a empty directory if 'DirExistsWarning' directive is set to 'True'.
  if (YesNoOrTrueFalseToBool('{#SetupSetting("DirExistsWarning")}')) and (DirExists(WizardDirValue))  then begin
    if MsgBox(FmtMessage(SetupMessage(msgDirExists), [WizardDirValue]), mbConfirmation, MB_YESNO) = IDNO then begin
      Result := false;
    end;
  end;

end;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Custom UI Methods ----------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// - - - - - - - - - - - - - - - - - - - - - - //
// Creates the author website related controls //
// - - - - - - - - - - - - - - - - - - - - - - //
procedure CreateAuthorControls(AuthorWebsiteUrl: String);
var
  InstallerAuthorLabel: TNewStaticText;
  AuthorWebsiteLabel  : TNewStaticText;
  AuthorWebsiteBitmap : TBitmapImage;
  UseDarkTheme        : Boolean;

begin
  UseDarkTheme := False;

  // Set AuthorWebsiteBitmap control properties...
  AuthorWebsiteBitmap          := TBitmapImage.Create(WizardForm);
  AuthorWebsiteBitmap.Parent   := WizardForm.WelcomePage;
  AuthorWebsiteBitmap.AutoSize := True;
  AuthorWebsiteBitmap.Left     := (WizardForm.WizardBitmapImage.Left + WizardForm.WizardBitmapImage.Width) + ScaleX(10);
  AuthorWebsiteBitmap.Top      := (WizardForm.WelcomeLabel2.Top + WizardForm.WelcomeLabel2.Height) - (AuthorWebsiteBitmap.Height div 2) - ScaleX(16);
  AuthorWebsiteBitmap.Cursor   := crHand
  AuthorWebsiteBitmap.OnClick  := @AuthorWebsiteControlClick;
  AuthorWebsiteBitmap.Anchors  := [akLeft, akBottom];
  AuthorWebsiteBitmap.Visible  := (AuthorWebsiteUrl <> '');

  if FileExists(ExpandConstant('{tmp}\carbon.vsf')) then begin
      ExtractTemporaryFiles('{tmp}\WizardBitmaps\AuthorWebsiteCarbon.bmp');
      AuthorWebsiteBitmap.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\AuthorWebsiteCarbon.bmp'));
  end else begin
    if UseDarkTheme then begin
      ExtractTemporaryFiles('{tmp}\WizardBitmaps\AuthorWebsiteCarbon.bmp');
      AuthorWebsiteBitmap.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\AuthorWebsiteCarbon.bmp'));
    end else begin
      ExtractTemporaryFiles('{tmp}\WizardBitmaps\AuthorWebsiteWhite.bmp');
      AuthorWebsiteBitmap.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\AuthorWebsiteWhite.bmp'));
    end;
  end;

  // Resize WelcomeLabel2 height to be able see AuthorWebsiteBitmap control.
  WizardForm.WelcomeLabel2.Height := WizardForm.WelcomeLabel2.Height - (AuthorWebsiteBitmap.Height + ScaleY(8));
  // This will not work...
  // AuthorWebsiteBitmap.BringToFront();

  // Set AuthorWebsiteLabel control properties...
  AuthorWebsiteLabel         := TNewStaticText.Create(WizardForm);
  AuthorWebsiteLabel.Parent  := WizardForm.WelcomePage;
  AuthorWebsiteLabel.Left    := AuthorWebsiteBitmap.Left;
  AuthorWebsiteLabel.Top     := AuthorWebsiteBitmap.Top - ScaleY(18);
  AuthorWebsiteLabel.Cursor  := crHand;
  AuthorWebsiteLabel.OnClick := @AuthorWebsiteControlClick;
  AuthorWebsiteLabel.Anchors := [akLeft, akBottom];
  AuthorWebsiteLabel.Visible := (AuthorWebsiteUrl <> '');

  // Set InstallerAuthorLabel control properties...
  InstallerAuthorLabel         := TNewStaticText.Create(WizardForm);
  InstallerAuthorLabel.Parent  := WizardForm;
  InstallerAuthorLabel.Left    := ScaleX(2);
  InstallerAuthorLabel.Top     := WizardForm.NextButton.Top + WizardForm.NextButton.Height div 2 + ScaleY(10) - ScaleY(2);
  InstallerAuthorLabel.Anchors := [akLeft, akBottom];

  if ActiveLanguage = 'es' then begin
    InstallerAuthorLabel.Caption := 'Instalador creado por ElektroStudios';
    AuthorWebsiteLabel.Caption := 'Haga clic aquí para abrir el sitio web del autor del programa.';
  end else begin
    InstallerAuthorLabel.Caption := 'Installer made by ElektroStudios';
    AuthorWebsiteLabel.Caption := 'Click here to open the website of the program author.';
  end;

end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Creates a custom wizard page to select program install folder, and start menu group folder. //
//                                                                                             //
// This page is a full replacement for both 'wpSelectDir' and 'wpSelectProgramGroup' pages.    //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure CreateCustomSelectDirPage(CrateShortCutCheckBox: Boolean; DisableDirControls: Boolean; DisableGroupControls: Boolean);
var
  WizardStyle: String; // Determines the wizard style, 'modern' or 'classic'.

  // Pages
  ThisPage: TWizardPage;
  SelectDirPage: TWizardPage;
  SelectProgramGroupPage: TWizardPage;

begin

  WizardStyle := LowerCase('{#SetupSetting("WizardStyle")}');

  // Pages
  SelectDirPage          := PageFromID(wpSelectDir);
  SelectProgramGroupPage := PageFromID(wpSelectProgramGroup);
  ThisPage               := CreateCustomPage(wpInfoBefore, SelectDirPage.Caption + ' | ' + SelectProgramGroupPage.Caption,
                                                           SelectDirPage.Description + #13#10 + SelectProgramGroupPage.Description);
  CustomSelectDirPage    := ThisPage;

  // wpSelectDir Control Parenting
  WizardForm.SelectDirBitmapImage.Parent := ThisPage.Surface;
  WizardForm.SelectDirLabel.Parent       := ThisPage.Surface;
  WizardForm.SelectDirBrowseLabel.Parent := ThisPage.Surface;
  WizardForm.DirEdit.Parent              := ThisPage.Surface;
  WizardForm.DirBrowseButton.Parent      := ThisPage.Surface;
  WizardForm.DiskSpaceLabel.Parent       := ThisPage.Surface;

  // wpSelectProgramGroup Control Parenting
  WizardForm.SelectGroupBitmapImage.Parent           := ThisPage.Surface;
  WizardForm.SelectStartMenuFolderLabel.Parent       := ThisPage.Surface;
  WizardForm.SelectStartMenuFolderBrowseLabel.Parent := ThisPage.Surface;
  WizardForm.GroupEdit.Parent                        := ThisPage.Surface;
  WizardForm.GroupBrowseButton.Parent                := ThisPage.Surface;
  WizardForm.NoIconsCheck.Parent                     := ThisPage.Surface;

  // Control Positioning
  if WizardStyle = 'modern' then begin
      WizardForm.SelectGroupBitmapImage.Top           := WizardForm.DirEdit.Top + ScaleX(70);
      WizardForm.SelectStartMenuFolderLabel.Top       := WizardForm.SelectGroupBitmapImage.Top + ScaleX(6);
      WizardForm.SelectStartMenuFolderBrowseLabel.Top := WizardForm.SelectStartMenuFolderLabel.Top + ScaleX(35);
      WizardForm.GroupEdit.Top                        := WizardForm.SelectStartMenuFolderBrowseLabel.Top + ScaleX(20);
      WizardForm.GroupBrowseButton.Top                := WizardForm.GroupEdit.Top;
      WizardForm.NoIconsCheck.Top                     := WizardForm.GroupEdit.Top - ScaleX(42);
  end else begin
      WizardForm.SelectDirBrowseLabel.Visible             := False;
      WizardForm.SelectStartMenuFolderBrowseLabel.Visible := False;
      WizardForm.DirEdit.Top                    := WizardForm.SelectDirLabel.Top + ScaleX(30);
      WizardForm.DirBrowseButton.Top            := WizardForm.DirEdit.Top;
      WizardForm.SelectGroupBitmapImage.Top     := WizardForm.DirEdit.Top + ScaleX(70);
      WizardForm.SelectStartMenuFolderLabel.Top := WizardForm.SelectGroupBitmapImage.Top + ScaleX(6);
      WizardForm.GroupEdit.Top                  := WizardForm.SelectStartMenuFolderLabel.Top + ScaleX(30);
      WizardForm.GroupBrowseButton.Top          := WizardForm.GroupEdit.Top;
      WizardForm.NoIconsCheck.Top               := WizardForm.GroupEdit.Top + ScaleX(28);
  end;

  // Load custom bitmap images
  if FileExists(ExpandConstant('{tmp}\carbon.vsf')) then begin
      ExtractTemporaryFiles('{tmp}\WizardBitmaps\FolderCarbon.bmp');
      WizardForm.SelectDirBitmapImage.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\FolderCarbon.bmp'));
      ExtractTemporaryFiles('{tmp}\WizardBitmaps\StartMenuCarbon.bmp');
      WizardForm.SelectGroupBitmapImage.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\StartMenuCarbon.bmp'));
  end else begin
     begin
      if WizardStyle = 'modern' then begin
        ExtractTemporaryFiles('{tmp}\WizardBitmaps\FolderWhiteModern.bmp');
        WizardForm.SelectDirBitmapImage.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\FolderWhiteModern.bmp'));
        ExtractTemporaryFiles('{tmp}\WizardBitmaps\StartMenuWhiteModern.bmp');
        WizardForm.SelectGroupBitmapImage.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\StartMenuWhiteModern.bmp'));
      end else begin
        ExtractTemporaryFiles('{tmp}\WizardBitmaps\FolderWhiteClassic.bmp');
        WizardForm.SelectDirBitmapImage.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\FolderWhiteClassic.bmp'));
        ExtractTemporaryFiles('{tmp}\WizardBitmaps\StartMenuWhiteClassic.bmp');
        WizardForm.SelectGroupBitmapImage.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\StartMenuWhiteClassic.bmp'));
      end;
    end;
  end;

  // Disable 'DirEdit' and 'DirBrowseButton' controls if 'WizardGroupValue' contains a temp directory or Windows directory.
  WizardForm.DirEdit.Enabled := (Pos('{tmp}', LowerCase(WizardDirValue)) = 0) and (Pos('.tmp', LowerCase(WizardDirValue)) = 0) and (Pos('{win}', LowerCase(WizardDirValue)) = 0) and (Pos('\windows\', LowerCase(WizardDirValue)) = 0);
  WizardForm.DirBrowseButton.Enabled := (Pos('{tmp}', LowerCase(WizardDirValue)) = 0) and (Pos('.tmp', LowerCase(WizardDirValue)) = 0) and (Pos('{win}', LowerCase(WizardDirValue)) = 0) and (Pos('\windows\', LowerCase(WizardDirValue)) = 0);

  // Disable 'DirEdit' and 'DirBrowseButton' controls if specified in the function parameter.
  if DisableDirControls then begin
    WizardForm.DirEdit.Enabled := False;
    WizardForm.DirBrowseButton.Enabled := False;
  end;

  // Disable 'GroupEdit', 'GroupBrowseButton' and 'NoIconsCheck' controls if 'WizardGroupValue' is the default / not set.
  WizardForm.GroupEdit.Enabled := (Pos('(default)', LowerCase(WizardGroupValue)) = 0);
  WizardForm.GroupBrowseButton.Enabled := (Pos('(default)', LowerCase(WizardGroupValue)) = 0);
  WizardForm.NoIconsCheck.Enabled := (Pos('(default)', LowerCase(WizardGroupValue)) = 0);

  // Disable 'GroupEdit', 'GroupBrowseButton' and 'NoIconsCheck' controls if specified in the function parameter.
  if DisableGroupControls then begin
    WizardForm.GroupEdit.Enabled := False;
    WizardForm.GroupBrowseButton.Enabled := False;
    WizardForm.NoIconsCheck.Enabled := False;
  end;

  // 'Create a desktop shortcut' Control
  DesktopShortcutCheckBox         := TNewCheckBox.Create(ThisPage);
  if CrateShortCutCheckBox then begin
    DesktopShortcutCheckBox.Parent  := ThisPage.Surface;
    DesktopShortcutCheckBox.Top     := WizardForm.DirEdit.Top + ScaleX(30);
    DesktopShortcutCheckBox.Caption := CustomMessage('CreateDesktopIcon');
    DesktopShortcutCheckBox.Checked := True;
    DesktopShortcutCheckBox.Width   := WizardForm.NoIconsCheck.Width;
  end;

  // Set tab Order
  WizardForm.DirEdit.TabOrder           := 900;
  WizardForm.DirBrowseButton.TabOrder   := 901;
  DesktopShortcutCheckBox.TabOrder      := 902;
  WizardForm.GroupEdit.TabOrder         := 903;
  WizardForm.GroupBrowseButton.TabOrder := 904;
  WizardForm.NoIconsCheck.TabOrder      := 905;
  WizardForm.DiskSpaceLabel.TabOrder    := 906;

  // Set page event-handlers
  ThisPage.OnNextButtonClick := @CustomSelectDirPageNextButtonClick;

end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Determines whether the user has choosed to create a desktop shortcut of the program executable. //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function IsCreateDesktopShortcutChecked(): Boolean;
begin
  Result := not (CustomSelectDirPage = nil) and DesktopShortcutCheckBox.Checked;
end;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Installer Events ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Determine whether a wizard page should be skipped / not shown //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function ShouldSkipPage(PageID: Integer): Boolean;
begin
  // Force to skip the 'wpSelectDir' and 'wpSelectProgramGroup' pages if 'CustomSelectDirPage' is not nul.
  If ((PageID = wpSelectDir) or (PageID = wpSelectProgramGroup)) and (CustomSelectDirPage <> nil) then begin
    Result := True;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// It is called automatically when the Ready to Install wizard page becomes the active page.           //
//                                                                                                     //
// It should return the text to be displayed in the settings memo on the Ready to Install wizard page. //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function UpdateReadyMemo(
  Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo,
  MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
var
  additionalText: string;
begin
  if Length(MemoUserInfoInfo) > 0 then begin
    Result := Result + MemoUserInfoInfo + NewLine + NewLine;
  end;

  // Force to skip the the MemoDirInfo if 'DefaultDirName' contains a temp directory.
  if (Length(MemoDirInfo) > 0) and (Pos('.tmp', LowerCase(WizardDirValue)) = 0) and (Pos('{tmp}', LowerCase(WizardDirValue)) = 0) then begin
    Result := Result + MemoDirInfo + NewLine + NewLine;
  end;

  if Length(MemoTypeInfo) > 0 then begin
    Result := Result + MemoTypeInfo + NewLine + NewLine;
  end;

  if Length(MemoComponentsInfo) > 0 then begin
    Result := Result + MemoComponentsInfo + NewLine + NewLine;
  end;

  // This is necessary when 'CreateCustomSelectDirPage' method is called.
  if not (WizardNoIcons) and (CustomSelectDirPage <> nil) and (Pos('(default)', LowerCase(WizardGroupValue)) = 0) then begin
    additionalText := WizardGroupValue;
    StringChange(additionalText, '&', '');
    if (Length(MemoGroupInfo) = 0) then begin
      Result := Result + SetupMessage(msgReadyMemoGroup) + NewLine + Space + additionalText + NewLine + NewLine;
    end else begin
      Result := Result + MemoGroupInfo + NewLine + NewLine;
    end;
  end else if (Length(MemoGroupInfo) > 0) and (Pos('(default)', LowerCase(WizardGroupValue)) = 0) then begin
      Result := Result + MemoGroupInfo + NewLine + NewLine;
  end;

  // This is necessary when 'CreateCustomSelectDirPage' method is called.
  if not (DesktopShortcutCheckBox = nil) and (DesktopShortcutCheckBox.Checked) then begin
    additionalText := DesktopShortcutCheckBox.Caption;
    StringChange(additionalText, '&', '');
    if (Length(MemoTasksInfo) = 0) then begin
      Result := Result + SetupMessage(msgReadyMemoTasks) + NewLine + Space + additionalText + NewLine + NewLine;
    end else begin
      Result := Result + MemoTasksInfo + NewLine + Space + additionalText + NewLine + NewLine;
    end;
  end else if (Length(MemoTasksInfo) > 0) then begin
      Result := Result + MemoTasksInfo + NewLine + NewLine;
  end;

end;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Uninstaller Events ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// - - - - - - - - - - - - - - - - - - - - - - - - - //
// Occurs when the uninstaller current page changes  //
// - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  DeleteCustomFoldersEnabled: Boolean;
  FolderPath: String;
  FolderDescriptionEN: String;
  FolderDescriptionES: String;
  FoldersToDelete: array of string;
  Tokens: array of string;
  Index: Integer;
  DeleteFolderString: String;
  SelectedTasksHKLM: String;
  SelectedTasksHKCU: String;
begin
  case CurUninstallStep of
    usPostUninstall: begin
    end;

    usDone: begin
      UninstallSuccess:= True;
    end;

    usUninstall: begin
      // Remove the application directory from PATH environment variable.
      RegQueryStringValue(HKLM, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\' + '{#SetupSetting("AppId")}' + '_is1', 'Inno Setup: Selected Tasks', SelectedTasksHKLM)
      RegQueryStringValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\' + '{#SetupSetting("AppId")}' + '_is1', 'Inno Setup: Selected Tasks', SelectedTasksHKCU)

      if not DeleteCustomFoldersEnabled then Exit; // Exits from this block if DeleteCustomFoldersEnabled = False

      // Set custom folders to delete (Path|Description)
      SetArrayLength(FoldersToDelete, 10);
      FoldersToDelete[0] := ExpandConstant('{userappdata}\MyProgramFolder') + '|current user settings|la configuración de usuario actual';
      FoldersToDelete[1] := ExpandConstant('{app}\tmp')                     + '|program temp files|los archivos temporales del programa';

    end; // usUninstall
  end; // case CurUninstallStep
end;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// InitializeWizard Event ------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Occurs when the wizard initializes                                                //
// Use this event function to make changes to the wizard or wizard pages at startup. //
// At the time this event is triggered, the wizard form does not yet exist.          //
// https://jrsoftware.org/ishelp/index.php?topic=scriptevents                        //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
<event('InitializeWizard')>
procedure InitializeWizard1();
begin
  // No need to call CreateAuthorControls(), CreatePasswordHintControl() and CreateCustomSelectDirPage() if wizard has no GUI or it is the uninstaller.
  if WizardSilent or IsUninstaller then Exit;
  CreateAuthorControls(ExpandConstant('{#AuthorWebsite}'));
  CreateCustomSelectDirPage({CrateShortCutCheckBox} True, {DisableDirControls} False, {DisableGroupControls} False);
end;
