' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="UIStrings.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization

#End Region

#Region " UIStrings "

Namespace UserInterface.Localization

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains the localized texts of the user interface, for English (default), Spanish and Portuguese.
    ''' <para></para>
    ''' The layout of the forms is language-independent (it adapts to the length of the texts), 
    ''' so a language only needs its texts.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class UIStrings

#Region " Constants "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The two-letter name of the default language (English).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Const DefaultCultureName As String = "en"

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the two-letter name of the language of these texts (<c>en</c>, <c>es</c> or <c>pt</c>).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property CultureName As String

        ''' <summary>Gets the text of the navigation button of the item builder page.</summary>
        Public ReadOnly Property ItemBuilder As String

        ''' <summary>Gets the text of the navigation button of the startup list page.</summary>
        Public ReadOnly Property StartupList As String

        ''' <summary>Gets the text of the language menu.</summary>
        Public ReadOnly Property Language As String

        ''' <summary>Gets the name of the English language.</summary>
        Public ReadOnly Property English As String

        ''' <summary>Gets the name of the Spanish language.</summary>
        Public ReadOnly Property Spanish As String

        ''' <summary>Gets the name of the Portuguese language.</summary>
        Public ReadOnly Property Portuguese As String

        ''' <summary>Gets the text of the about button.</summary>
        Public ReadOnly Property About As String

        ''' <summary>Gets the tool tip of the theme switch.</summary>
        Public ReadOnly Property DarkTheme As String

        ''' <summary>Gets the caption of the name field.</summary>
        Public ReadOnly Property NameCaption As String

        ''' <summary>Gets the caption of the file field.</summary>
        Public ReadOnly Property FileCaption As String

        ''' <summary>Gets the caption of the arguments field.</summary>
        Public ReadOnly Property ArgumentsCaption As String

        ''' <summary>Gets the caption of the startup type options.</summary>
        Public ReadOnly Property StartupTypeCaption As String

        ''' <summary>Gets the hint of the name field.</summary>
        Public ReadOnly Property NameHint As String

        ''' <summary>Gets the hint of the file field.</summary>
        Public ReadOnly Property FileHint As String

        ''' <summary>Gets the hint of the arguments field.</summary>
        Public ReadOnly Property ArgumentsHint As String

        ''' <summary>Gets the text of the browse button of the file field.</summary>
        Public ReadOnly Property Browse As String

        ''' <summary>Gets the text of the current user startup type option.</summary>
        Public ReadOnly Property CurrentUser As String

        ''' <summary>Gets the text of the all users startup type option.</summary>
        Public ReadOnly Property AllUsers As String

        ''' <summary>Gets the tool tip of the all users option when the program is not running elevated.</summary>
        Public ReadOnly Property AllUsersRequiresElevation As String

        ''' <summary>Gets the text of the recent files drop-down button.</summary>
        Public ReadOnly Property RecentFiles As String

        ''' <summary>Gets the text of the menu item that clears the recent file list.</summary>
        Public ReadOnly Property ClearRecentFiles As String

        ''' <summary>Gets the text of the button that adds the file to Windows startup.</summary>
        Public ReadOnly Property AddToStartup As String

        ''' <summary>Gets the text of the button that deletes the selected startup entry.</summary>
        Public ReadOnly Property DeleteEntry As String

        ''' <summary>Gets the text of the button that sends the selected startup entry to the item builder.</summary>
        Public ReadOnly Property SendToItemBuilder As String

        ''' <summary>Gets the text of the button that opens the selected startup entry in Regedit.</summary>
        Public ReadOnly Property OpenInRegedit As String

        ''' <summary>Gets the text of the button that refreshes the startup list.</summary>
        Public ReadOnly Property RefreshList As String

        ''' <summary>Gets the header of the type column of the startup list.</summary>
        Public ReadOnly Property TypeColumn As String

        ''' <summary>Gets the header of the icon column of the startup list.</summary>
        Public ReadOnly Property IconColumn As String

        ''' <summary>Gets the header of the name column of the startup list.</summary>
        Public ReadOnly Property NameColumn As String

        ''' <summary>Gets the header of the value column of the startup list.</summary>
        Public ReadOnly Property ValueColumn As String

        ''' <summary>Gets the status bar text when the program is running elevated.</summary>
        Public ReadOnly Property RunningElevated As String

        ''' <summary>Gets the status bar text when the program is not running elevated.</summary>
        Public ReadOnly Property NotRunningElevated As String

        ''' <summary>Gets the status bar text of the startup list when it has one entry.</summary>
        Public ReadOnly Property EntryCountSingular As String

        ''' <summary>Gets the composite format of the status bar text of the startup list when it has zero or several entries ({0} is the count).</summary>
        Public ReadOnly Property EntryCountPluralFormat As String

        ''' <summary>Gets the question shown before deleting a startup entry.</summary>
        Public ReadOnly Property DeleteEntryQuestion As String

        ''' <summary>Gets the warning shown before deleting a startup entry.</summary>
        Public ReadOnly Property CannotBeUndone As String

        ''' <summary>Gets the message shown when an entry with the same name already exists.</summary>
        Public ReadOnly Property EntryAlreadyExists As String

        ''' <summary>Gets the question shown before overwriting an existing entry.</summary>
        Public ReadOnly Property OverwriteEntryQuestion As String

        ''' <summary>Gets the message shown when the entry was added to Windows startup.</summary>
        Public ReadOnly Property EntryAdded As String

        ''' <summary>Gets the message shown when the entry could not be added to Windows startup.</summary>
        Public ReadOnly Property EntryAddFailed As String

        ''' <summary>Gets the question shown after sending an entry to the item builder.</summary>
        Public ReadOnly Property DeleteSentEntryQuestion As String

        ''' <summary>Gets the warning shown when the file path of a startup entry could not be parsed.</summary>
        Public ReadOnly Property ParseFilePathFailed As String

        ''' <summary>Gets the error shown when the startup registry values could not be read.</summary>
        Public ReadOnly Property RegistryValuesError As String

        ''' <summary>Gets the error shown when a startup registry key could not be accessed.</summary>
        Public ReadOnly Property RegistryKeyError As String

        ''' <summary>Gets the composite format of the title of the about window ({0} is the program name).</summary>
        Public ReadOnly Property AboutTitleFormat As String

        ''' <summary>Gets the caption of the loaded assemblies list of the about window.</summary>
        Public ReadOnly Property LoadedAssemblies As String

        ''' <summary>Gets the error shown when an URL cannot be opened.</summary>
        Public ReadOnly Property OpenUrlError As String

#End Region

#Region " Shared Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the two-letter names of the supported languages.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property SupportedCultureNames As ReadOnlyCollection(Of String) =
            New ReadOnlyCollection(Of String)({"en", "es", "pt"})

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="UIStrings"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="cultureName">
        ''' The two-letter name of the language.
        ''' </param>
        ''' 
        ''' <param name="table">
        ''' The string table of the language.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="KeyNotFoundException">
        ''' <paramref name="table"/> does not contain a text.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub New(cultureName As String, table As Dictionary(Of String, String))

            Me.CultureName = cultureName
            Me.ItemBuilder = UIStrings.GetEntry(table, NameOf(UIStrings.ItemBuilder))
            Me.StartupList = UIStrings.GetEntry(table, NameOf(UIStrings.StartupList))
            Me.Language = UIStrings.GetEntry(table, NameOf(UIStrings.Language))
            Me.English = UIStrings.GetEntry(table, NameOf(UIStrings.English))
            Me.Spanish = UIStrings.GetEntry(table, NameOf(UIStrings.Spanish))
            Me.Portuguese = UIStrings.GetEntry(table, NameOf(UIStrings.Portuguese))
            Me.About = UIStrings.GetEntry(table, NameOf(UIStrings.About))
            Me.DarkTheme = UIStrings.GetEntry(table, NameOf(UIStrings.DarkTheme))
            Me.NameCaption = UIStrings.GetEntry(table, NameOf(UIStrings.NameCaption))
            Me.FileCaption = UIStrings.GetEntry(table, NameOf(UIStrings.FileCaption))
            Me.ArgumentsCaption = UIStrings.GetEntry(table, NameOf(UIStrings.ArgumentsCaption))
            Me.StartupTypeCaption = UIStrings.GetEntry(table, NameOf(UIStrings.StartupTypeCaption))
            Me.NameHint = UIStrings.GetEntry(table, NameOf(UIStrings.NameHint))
            Me.FileHint = UIStrings.GetEntry(table, NameOf(UIStrings.FileHint))
            Me.ArgumentsHint = UIStrings.GetEntry(table, NameOf(UIStrings.ArgumentsHint))
            Me.Browse = UIStrings.GetEntry(table, NameOf(UIStrings.Browse))
            Me.CurrentUser = UIStrings.GetEntry(table, NameOf(UIStrings.CurrentUser))
            Me.AllUsers = UIStrings.GetEntry(table, NameOf(UIStrings.AllUsers))
            Me.AllUsersRequiresElevation = UIStrings.GetEntry(table, NameOf(UIStrings.AllUsersRequiresElevation))
            Me.RecentFiles = UIStrings.GetEntry(table, NameOf(UIStrings.RecentFiles))
            Me.ClearRecentFiles = UIStrings.GetEntry(table, NameOf(UIStrings.ClearRecentFiles))
            Me.AddToStartup = UIStrings.GetEntry(table, NameOf(UIStrings.AddToStartup))
            Me.DeleteEntry = UIStrings.GetEntry(table, NameOf(UIStrings.DeleteEntry))
            Me.SendToItemBuilder = UIStrings.GetEntry(table, NameOf(UIStrings.SendToItemBuilder))
            Me.OpenInRegedit = UIStrings.GetEntry(table, NameOf(UIStrings.OpenInRegedit))
            Me.RefreshList = UIStrings.GetEntry(table, NameOf(UIStrings.RefreshList))
            Me.TypeColumn = UIStrings.GetEntry(table, NameOf(UIStrings.TypeColumn))
            Me.IconColumn = UIStrings.GetEntry(table, NameOf(UIStrings.IconColumn))
            Me.NameColumn = UIStrings.GetEntry(table, NameOf(UIStrings.NameColumn))
            Me.ValueColumn = UIStrings.GetEntry(table, NameOf(UIStrings.ValueColumn))
            Me.RunningElevated = UIStrings.GetEntry(table, NameOf(UIStrings.RunningElevated))
            Me.NotRunningElevated = UIStrings.GetEntry(table, NameOf(UIStrings.NotRunningElevated))
            Me.EntryCountSingular = UIStrings.GetEntry(table, NameOf(UIStrings.EntryCountSingular))
            Me.EntryCountPluralFormat = UIStrings.GetEntry(table, NameOf(UIStrings.EntryCountPluralFormat))
            Me.DeleteEntryQuestion = UIStrings.GetEntry(table, NameOf(UIStrings.DeleteEntryQuestion))
            Me.CannotBeUndone = UIStrings.GetEntry(table, NameOf(UIStrings.CannotBeUndone))
            Me.EntryAlreadyExists = UIStrings.GetEntry(table, NameOf(UIStrings.EntryAlreadyExists))
            Me.OverwriteEntryQuestion = UIStrings.GetEntry(table, NameOf(UIStrings.OverwriteEntryQuestion))
            Me.EntryAdded = UIStrings.GetEntry(table, NameOf(UIStrings.EntryAdded))
            Me.EntryAddFailed = UIStrings.GetEntry(table, NameOf(UIStrings.EntryAddFailed))
            Me.DeleteSentEntryQuestion = UIStrings.GetEntry(table, NameOf(UIStrings.DeleteSentEntryQuestion))
            Me.ParseFilePathFailed = UIStrings.GetEntry(table, NameOf(UIStrings.ParseFilePathFailed))
            Me.RegistryValuesError = UIStrings.GetEntry(table, NameOf(UIStrings.RegistryValuesError))
            Me.RegistryKeyError = UIStrings.GetEntry(table, NameOf(UIStrings.RegistryKeyError))
            Me.AboutTitleFormat = UIStrings.GetEntry(table, NameOf(UIStrings.AboutTitleFormat))
            Me.LoadedAssemblies = UIStrings.GetEntry(table, NameOf(UIStrings.LoadedAssemblies))
            Me.OpenUrlError = UIStrings.GetEntry(table, NameOf(UIStrings.OpenUrlError))

        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Formats the number of entries of the startup list (e.g. <c>1 entry</c>, <c>5 entries</c>).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="count">
        ''' The number of entries.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The formatted text.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' <paramref name="count"/> is negative.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Function FormatEntryCount(count As Integer) As String

            If count < 0 Then
                Throw New ArgumentOutOfRangeException(NameOf(count), count, "The count cannot be negative.")
            End If

            Return If(count = 1,
                      Me.EntryCountSingular,
                      String.Format(CultureInfo.GetCultureInfo(Me.CultureName), Me.EntryCountPluralFormat, count))

        End Function

#End Region

#Region " Public Shared Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the texts of the specified language.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="cultureName">
        ''' The name of the culture (e.g. <c>es</c> or <c>es-ES</c>). Only its neutral (two-letter) language is used.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The texts of the language, or the English texts if the language is not supported.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function FromCulture(cultureName As String) As UIStrings

            Dim language As String = UIStrings.GetNeutralLanguage(cultureName)

            Select Case language
                Case "es"
                    Return New UIStrings("es", UIStrings.CreateSpanishTable())
                Case "pt"
                    Return New UIStrings("pt", UIStrings.CreatePortugueseTable())
                Case Else
                    Return New UIStrings(UIStrings.DefaultCultureName, UIStrings.CreateEnglishTable())
            End Select

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the texts of the current user interface language of the current thread.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The texts of the language, or the English texts if the language is not supported.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function FromCurrentUICulture() As UIStrings

            Return UIStrings.FromCulture(CultureInfo.CurrentUICulture.Name)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the supported two-letter language of the specified culture name.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="cultureName">
        ''' The name of the culture (e.g. <c>es</c> or <c>es-ES</c>), or <see langword="Nothing"/>.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <c>en</c>, <c>es</c> or <c>pt</c>; <c>en</c> if the language is not supported.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function GetNeutralLanguage(cultureName As String) As String

            If String.IsNullOrWhiteSpace(cultureName) Then
                Return UIStrings.DefaultCultureName
            End If

            Dim language As String = cultureName.Trim().Split("-"c)(0).ToLowerInvariant()
            Return If(UIStrings.SupportedCultureNames.Contains(language), language, UIStrings.DefaultCultureName)

        End Function

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a text from a string table, failing if it is missing or empty.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="table">
        ''' The string table.
        ''' </param>
        ''' 
        ''' <param name="key">
        ''' The key of the text.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The text.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="KeyNotFoundException">
        ''' The text is missing or empty.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function GetEntry(table As Dictionary(Of String, String), key As String) As String

            Dim value As String = Nothing
            If Not table.TryGetValue(key, value) OrElse String.IsNullOrWhiteSpace(value) Then
                Throw New KeyNotFoundException($"The localized text '{key}' is missing.")
            End If

            Return value

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Creates the English string table.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The string table.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function CreateEnglishTable() As Dictionary(Of String, String)

            Return New Dictionary(Of String, String)(StringComparer.Ordinal) From {
                {NameOf(UIStrings.ItemBuilder), "Item Builder"},
                {NameOf(UIStrings.StartupList), "Startup List"},
                {NameOf(UIStrings.Language), "Language"},
                {NameOf(UIStrings.English), "English"},
                {NameOf(UIStrings.Spanish), "Spanish"},
                {NameOf(UIStrings.Portuguese), "Portuguese"},
                {NameOf(UIStrings.About), "About..."},
                {NameOf(UIStrings.DarkTheme), "Dark theme"},
                {NameOf(UIStrings.NameCaption), "Name"},
                {NameOf(UIStrings.FileCaption), "File"},
                {NameOf(UIStrings.ArgumentsCaption), "Arguments"},
                {NameOf(UIStrings.StartupTypeCaption), "Startup type"},
                {NameOf(UIStrings.NameHint), "Unique startup entry name for the file (drop a file here to auto-assign)."},
                {NameOf(UIStrings.FileHint), "Full path to the file (drop a file here to auto-assign)."},
                {NameOf(UIStrings.ArgumentsHint), "Optional arguments (for an executable file only)."},
                {NameOf(UIStrings.Browse), "Browse..."},
                {NameOf(UIStrings.CurrentUser), "Current user"},
                {NameOf(UIStrings.AllUsers), "All users"},
                {NameOf(UIStrings.AllUsersRequiresElevation), "Run the program as administrator to add entries for all users."},
                {NameOf(UIStrings.RecentFiles), "Recent Files..."},
                {NameOf(UIStrings.ClearRecentFiles), "Clear recent file list"},
                {NameOf(UIStrings.AddToStartup), "Add to Windows startup"},
                {NameOf(UIStrings.DeleteEntry), "Delete entry"},
                {NameOf(UIStrings.SendToItemBuilder), "Send to item builder"},
                {NameOf(UIStrings.OpenInRegedit), "Open in Regedit"},
                {NameOf(UIStrings.RefreshList), "Refresh"},
                {NameOf(UIStrings.TypeColumn), "Type"},
                {NameOf(UIStrings.IconColumn), "Icon"},
                {NameOf(UIStrings.NameColumn), "Name"},
                {NameOf(UIStrings.ValueColumn), "Value"},
                {NameOf(UIStrings.RunningElevated), "Running as administrator."},
                {NameOf(UIStrings.NotRunningElevated), "Program is not running elevated, some features are disabled."},
                {NameOf(UIStrings.EntryCountSingular), "1 entry"},
                {NameOf(UIStrings.EntryCountPluralFormat), "{0} entries"},
                {NameOf(UIStrings.DeleteEntryQuestion), "Do you really want to delete the following entry?:"},
                {NameOf(UIStrings.CannotBeUndone), "This action cannot be undone."},
                {NameOf(UIStrings.EntryAlreadyExists), "An entry with the same name already exists in Windows startup."},
                {NameOf(UIStrings.OverwriteEntryQuestion), "Do you really want to overwrite the entry?"},
                {NameOf(UIStrings.EntryAdded), "Item successfully added to Windows startup."},
                {NameOf(UIStrings.EntryAddFailed), "Failed to add the item to Windows startup."},
                {NameOf(UIStrings.DeleteSentEntryQuestion), "Do you also want to delete the sent item from startup list?:"},
                {NameOf(UIStrings.ParseFilePathFailed), "Failed to parse entry file path, the full value has been retrieved instead:"},
                {NameOf(UIStrings.RegistryValuesError), "Error trying to parse registry values. Error message:"},
                {NameOf(UIStrings.RegistryKeyError), "Error trying to parse registry key. Error message:"},
                {NameOf(UIStrings.AboutTitleFormat), "About {0}"},
                {NameOf(UIStrings.LoadedAssemblies), "Loaded assemblies:"},
                {NameOf(UIStrings.OpenUrlError), "Error opening the URL:"}
            }

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Creates the Spanish string table.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The string table.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function CreateSpanishTable() As Dictionary(Of String, String)

            Return New Dictionary(Of String, String)(StringComparer.Ordinal) From {
                {NameOf(UIStrings.ItemBuilder), "Constructor"},
                {NameOf(UIStrings.StartupList), "Lista de inicio"},
                {NameOf(UIStrings.Language), "Idioma"},
                {NameOf(UIStrings.English), "Inglés"},
                {NameOf(UIStrings.Spanish), "Español"},
                {NameOf(UIStrings.Portuguese), "Portugués"},
                {NameOf(UIStrings.About), "Acerca de..."},
                {NameOf(UIStrings.DarkTheme), "Tema oscuro"},
                {NameOf(UIStrings.NameCaption), "Nombre"},
                {NameOf(UIStrings.FileCaption), "Archivo"},
                {NameOf(UIStrings.ArgumentsCaption), "Argumentos"},
                {NameOf(UIStrings.StartupTypeCaption), "Tipo de inicio"},
                {NameOf(UIStrings.NameHint), "Nombre único de la entrada de inicio para el archivo (arrastre un archivo aquí para asignarlo automáticamente)."},
                {NameOf(UIStrings.FileHint), "Ruta completa al archivo (arrastre un archivo aquí para asignarlo automáticamente)."},
                {NameOf(UIStrings.ArgumentsHint), "Argumentos opcionales (sólo para un archivo ejecutable)."},
                {NameOf(UIStrings.Browse), "Examinar..."},
                {NameOf(UIStrings.CurrentUser), "Usuario actual"},
                {NameOf(UIStrings.AllUsers), "Todos los usuarios"},
                {NameOf(UIStrings.AllUsersRequiresElevation), "Ejecute el programa como administrador para añadir entradas para todos los usuarios."},
                {NameOf(UIStrings.RecentFiles), "Archivos Recientes..."},
                {NameOf(UIStrings.ClearRecentFiles), "Borrar lista de archivos recientes"},
                {NameOf(UIStrings.AddToStartup), "Añadir al inicio de Windows"},
                {NameOf(UIStrings.DeleteEntry), "Eliminar entrada"},
                {NameOf(UIStrings.SendToItemBuilder), "Enviar al constructor"},
                {NameOf(UIStrings.OpenInRegedit), "Abrir en Regedit"},
                {NameOf(UIStrings.RefreshList), "Actualizar"},
                {NameOf(UIStrings.TypeColumn), "Tipo"},
                {NameOf(UIStrings.IconColumn), "Icono"},
                {NameOf(UIStrings.NameColumn), "Nombre"},
                {NameOf(UIStrings.ValueColumn), "Valor"},
                {NameOf(UIStrings.RunningElevated), "Ejecutando como administrador."},
                {NameOf(UIStrings.NotRunningElevated), "El programa no se está ejecutando en modo elevado, algunas funciones están desactivadas."},
                {NameOf(UIStrings.EntryCountSingular), "1 entrada"},
                {NameOf(UIStrings.EntryCountPluralFormat), "{0} entradas"},
                {NameOf(UIStrings.DeleteEntryQuestion), "¿Realmente desea eliminar la siguiente entrada?:"},
                {NameOf(UIStrings.CannotBeUndone), "Esta acción no se podrá deshacer."},
                {NameOf(UIStrings.EntryAlreadyExists), "Ya existe una entrada con el mismo nombre en el inicio de Windows."},
                {NameOf(UIStrings.OverwriteEntryQuestion), "¿Realmente desea sobrescribir la entrada?"},
                {NameOf(UIStrings.EntryAdded), "Elemento añadido correctamente al inicio de Windows."},
                {NameOf(UIStrings.EntryAddFailed), "Error al añadir el elemento al inicio de Windows."},
                {NameOf(UIStrings.DeleteSentEntryQuestion), "¿Desea además eliminar de la lista de inicio la entrada enviada?:"},
                {NameOf(UIStrings.ParseFilePathFailed), "No se pudo analizar la ruta del archivo de la entrada; en su lugar, se ha recuperado el valor completo:"},
                {NameOf(UIStrings.RegistryValuesError), "Error al analizar los valores del registro. Mensaje de error:"},
                {NameOf(UIStrings.RegistryKeyError), "Error al analizar la clave del registro. Mensaje de error:"},
                {NameOf(UIStrings.AboutTitleFormat), "Acerca de {0}"},
                {NameOf(UIStrings.LoadedAssemblies), "Ensamblados cargados:"},
                {NameOf(UIStrings.OpenUrlError), "Error al abrir la URL:"}
            }

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Creates the Portuguese string table.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The string table.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function CreatePortugueseTable() As Dictionary(Of String, String)

            Return New Dictionary(Of String, String)(StringComparer.Ordinal) From {
                {NameOf(UIStrings.ItemBuilder), "Construtor"},
                {NameOf(UIStrings.StartupList), "Lista de inicialização"},
                {NameOf(UIStrings.Language), "Idioma"},
                {NameOf(UIStrings.English), "Inglês"},
                {NameOf(UIStrings.Spanish), "Espanhol"},
                {NameOf(UIStrings.Portuguese), "Português"},
                {NameOf(UIStrings.About), "Sobre..."},
                {NameOf(UIStrings.DarkTheme), "Tema escuro"},
                {NameOf(UIStrings.NameCaption), "Nome"},
                {NameOf(UIStrings.FileCaption), "Arquivo"},
                {NameOf(UIStrings.ArgumentsCaption), "Argumentos"},
                {NameOf(UIStrings.StartupTypeCaption), "Tipo de inicialização"},
                {NameOf(UIStrings.NameHint), "Nome exclusivo da entrada de inicialização para o arquivo (arraste um arquivo aqui para atribuí-lo automaticamente)."},
                {NameOf(UIStrings.FileHint), "Caminho completo para o arquivo (arraste um arquivo aqui para atribuí-lo automaticamente)."},
                {NameOf(UIStrings.ArgumentsHint), "Argumentos opcionais (apenas para um arquivo executável)."},
                {NameOf(UIStrings.Browse), "Procurar..."},
                {NameOf(UIStrings.CurrentUser), "Usuário atual"},
                {NameOf(UIStrings.AllUsers), "Todos os usuários"},
                {NameOf(UIStrings.AllUsersRequiresElevation), "Execute o programa como administrador para adicionar entradas para todos os usuários."},
                {NameOf(UIStrings.RecentFiles), "Arquivos recentes..."},
                {NameOf(UIStrings.ClearRecentFiles), "Limpar lista de arquivos recentes"},
                {NameOf(UIStrings.AddToStartup), "Adicionar à inicialização do Windows"},
                {NameOf(UIStrings.DeleteEntry), "Excluir entrada"},
                {NameOf(UIStrings.SendToItemBuilder), "Enviar ao construtor"},
                {NameOf(UIStrings.OpenInRegedit), "Abrir no Regedit"},
                {NameOf(UIStrings.RefreshList), "Atualizar"},
                {NameOf(UIStrings.TypeColumn), "Tipo"},
                {NameOf(UIStrings.IconColumn), "Ícone"},
                {NameOf(UIStrings.NameColumn), "Nome"},
                {NameOf(UIStrings.ValueColumn), "Valor"},
                {NameOf(UIStrings.RunningElevated), "Executando como administrador."},
                {NameOf(UIStrings.NotRunningElevated), "O programa não está sendo executado em modo elevado, algumas funções estão desabilitadas."},
                {NameOf(UIStrings.EntryCountSingular), "1 entrada"},
                {NameOf(UIStrings.EntryCountPluralFormat), "{0} entradas"},
                {NameOf(UIStrings.DeleteEntryQuestion), "Deseja mesmo excluir a seguinte entrada?:"},
                {NameOf(UIStrings.CannotBeUndone), "Esta ação não pode ser desfeita."},
                {NameOf(UIStrings.EntryAlreadyExists), "Já existe uma entrada com o mesmo nome na inicialização do Windows."},
                {NameOf(UIStrings.OverwriteEntryQuestion), "Deseja realmente substituir a entrada?"},
                {NameOf(UIStrings.EntryAdded), "Item adicionado com sucesso à inicialização do Windows."},
                {NameOf(UIStrings.EntryAddFailed), "Erro ao adicionar o item à inicialização do Windows."},
                {NameOf(UIStrings.DeleteSentEntryQuestion), "Deseja também remover a entrada enviada da lista de inicialização?:"},
                {NameOf(UIStrings.ParseFilePathFailed), "Falha ao analisar o caminho do arquivo da entrada; em vez disso, o valor completo foi recuperado:"},
                {NameOf(UIStrings.RegistryValuesError), "Erro ao analisar os valores do registro. Mensagem de erro:"},
                {NameOf(UIStrings.RegistryKeyError), "Erro ao analisar a chave do registro. Mensagem de erro:"},
                {NameOf(UIStrings.AboutTitleFormat), "Sobre {0}"},
                {NameOf(UIStrings.LoadedAssemblies), "Assemblies carregados:"},
                {NameOf(UIStrings.OpenUrlError), "Erro ao abrir a URL:"}
            }

        End Function

#End Region

    End Class

End Namespace

#End Region
