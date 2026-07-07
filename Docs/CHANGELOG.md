# File2Startup Change Log 📋

## v1.5 *(current)* 🆕

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
