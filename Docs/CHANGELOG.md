# File2Startup Change Log 📋

## v1.3.3 *(current)* 🆕

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
