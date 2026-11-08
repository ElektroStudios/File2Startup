# File2Startup Change Log 📋

## v1.6 *(current)* 🆕

 🛠️ Fixes:
 - The "Delete", "Send to item builder" and "Open in Regedit" buttons were enabled even when the startup list had no selected row, showing an error message when clicked.
 - Unquoted startup entries with arguments (e.g. `C:\App\app.exe -minimized`) were sent to the item builder with the arguments inside the file path field, which corrupted the entry when it was added again.
 - Startup entries with an unquoted file path and a quoted argument were not properly parsed.
 - The quotes of a quoted first argument were lost when sending a startup entry to the item builder.
 - A trailing white-space was stored in the registry value of startup entries added without arguments.
 - A file path typed or pasted between quotes produced a malformed registry value. Now the enclosing quotes are removed, and an invalid file path shows an error message instead of being stored.
 - A startup entry stored with a non-string registry value type prevented the whole startup list from loading.
 - A corrupted recent file list stored in the user settings crashed the program when opening the recent files menu.
 - The warning shown when a file path could not be parsed from a startup entry was not translated to the selected language.
 - The window docking to the screen borders never worked, because its window procedure was missing from the source code.
 - The text hints of the item builder fields were not restored after selecting an item from the recent files menu.
 - Entries without a resolvable icon showed the "missing image" glyph (a red cross) in the startup list.
 - The type column of the startup list ("Current User" / "All Users") and the registry error messages were not translated to the selected language.
 - Clearing the recent file list was not saved until the program was closed.

 🎨 User interface:
  - New dark theme, and light theme that uses the default Windows colors.
  - New theme switch (sun/moon) in the tool bar. The selected theme is saved in the user settings; on first run, the program follows the Windows apps theme.
  - The title bar follows the selected theme (dark title bar for the dark theme), also when the theme is switched while the program is running.
  - Dark scroll bars in the startup list, and dark menus, tool bar and status bar for the dark theme.
  - Redesigned layout: field captions above the text boxes, the "Add to Windows startup" button next to the startup type options, and the startup list actions grouped under the list. The window height fits the content, without empty space.
  - The layout adapts to the length of the texts of each language (no more cut or overlapped texts in Spanish or Portuguese), and to window resizing.
  - The "Item Builder" and "Startup List" tool bar buttons now look like tabs, highlighting the active page.
  - New status bar, that shows the elevation notice (instead of replacing the window title) and the number of entries of the startup list.
  - The selected language is checked in the language menu.
  - The "All users" option shows a tool tip explaining that the program must run as administrator when it is disabled.
  - The full value of a startup entry is shown as a tool tip in the startup list.
  - The text hint of a field is no longer shown as selected text when the field gets the focus.
  - The item builder page is shown when a file is dropped on the window from the startup list page.
  - The "About" window follows the selected theme and language.

 🌟 Improvements:
  - The icon of unquoted startup entries, and of entries whose file path contains environment variables (e.g. `%ProgramFiles%`), is now shown in the startup list and in the recent files menu.
  - The startup list opens the registry keys in read-only mode.
  - Added a unit tests project (MSTest).

## v1.5 🔄

 🛠️ Fixes:
 - The file path and arguments of some startup entries were not properly parsed.

 🌟 Improvements:
  - Rows in the startup list are sorted by 'Name' column by default.
  - The selected row in the startup list will remain selected after changes.
  - The program will warn if a file path could not be parsed from a startup entry, thus preventing data loss.

## v1.4 🔄

 🛠️ Fixes:
 - Recent file list was disabled after changing application language.

 🌟 Improvements:
 - Added a menu item to clear recent file list.
 - Removed useless "Compact Mode".
 - Added a dialog to optionally delete from startup list a item sent to item builder.

## v1.3.5 🔄

 🌟 Improvements:
 - TextBox fields are automatically cleared after successfully adding an item to Windows startup.
 - Replaced icons to be more representative.

## v1.3.4 🔄

 🛠️ Fixes:
 - Now the program disables read/write functionalities to HKLM registry key when the process is not running elevated.
 - Missing translation resource files from previous release caused an incomplete translation.
 - Deleted orphan translation resources for "en-AS" culture from source-code.

 🌟 Improvements:
 - Added an application manifest requiring user's highest available privileges to run the app.
 - ToolStrip CheckBox's ("Compact Mode") internal logic and performance has been improved.
 - Name value is auto-assigned when selecting a file through the file dialog, only in case the Name is empty or white spaced.

## v1.3.3 🔄

 🛠️ Fixes:
 - Missing language translations for the text of some modified controls from previous release.
 - Control hints were not updated properly when the control text was empty.

## v1.3.2 🔄

 🛠️ Fixes:
 - Added a 'Compact Mode' check box to hide/show tabs; whose main purpose is to serve as a final workaround for issue [#2](https://github.com/ElektroStudios/File2Startup/issues/2#issuecomment-2041072015)

## v1.3.1 🔄

 🛠️ Fixes:
 - Layout logic was interrupted for DataGridView control. Fixes issue [#2](https://github.com/ElektroStudios/File2Startup/issues/2#issuecomment-2041072015)
    
  🌟 Improvements:
 - Pruned unused code from source-code (faster loading).
 - Added additional error-handlings to prevent and inform about errors. Possible workaround for issue [#2](https://github.com/ElektroStudios/File2Startup/issues/2#issuecomment-2041072015)
 - Added additional languages for the installer.
 - Removed unused code and VCL styles from the installer to try prevent AV false positives.

## v1.3 🔄
 🚀 New Features:
 - Added an startup viewer to list and/or delete added items.
 - Added multilanguage support with English, Spanish and Portuguese.
    
 🛠️ Fixes:
 - Recent files button was enabled when it is empty.
 - Recent files list can contain duplicated items.
 - A directory can be dropped into the form.
 - Icon extraction methodology was not propertly retrieving icon for some files.
    
 🌟 Improvements:
 - Changed text font to 'Segoe UI' (10 pt)
 - Improved label descriptions and text-box hints.
 - Implemented logic to warn the user when overwriting an item in Windows startup.

## v1.2 🔄

 🚀 New Features:
 - Added a button to load a file.
 - Implemented a window magnetizer (Desktop Border Docking).
    
 🌟 Improvements:
 - Items in the MRU list now shows the associated file icons.

## v1.1 🔄
*(Change log not available)*

## v1.0 🔄
Initial Release.
