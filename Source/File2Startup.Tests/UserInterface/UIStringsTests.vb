' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="UIStringsTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Reflection

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UserInterface.Localization

#End Region

#Region " UIStringsTests "

Namespace UserInterface

    ''' <summary>
    ''' Tests for the <see cref="UIStrings"/> class.
    ''' </summary>
    <TestClass>
    Public NotInheritable Class UIStringsTests

#Region " Helpers "

        ''' <summary>
        ''' Gets the localized text properties of <see cref="UIStrings"/> (every public String property except the culture name).
        ''' </summary>
        ''' <returns>The properties.</returns>
        Private Shared Function GetTextProperties() As PropertyInfo()

            Return GetType(UIStrings).GetProperties(BindingFlags.Public Or BindingFlags.Instance).
                Where(Function(p As PropertyInfo) p.PropertyType Is GetType(String) AndAlso p.Name <> NameOf(UIStrings.CultureName)).
                ToArray()

        End Function

#End Region

#Region " Completeness "

        ''' <summary>The class exposes a meaningful number of texts.</summary>
        <TestMethod>
        Public Sub TextProperties_AreDiscovered()

            Assert.IsTrue(UIStringsTests.GetTextProperties().Length >= 40)

        End Sub

        ''' <summary>Every supported language provides every text (no empty values).</summary>
        <TestMethod>
        <DataRow("en")>
        <DataRow("es")>
        <DataRow("pt")>
        Public Sub FromCulture_EverySupportedLanguage_HasAllTexts(cultureName As String)

            Dim strings As UIStrings = UIStrings.FromCulture(cultureName)

            Assert.AreEqual(cultureName, strings.CultureName)
            For Each prop As PropertyInfo In UIStringsTests.GetTextProperties()
                Dim value As String = DirectCast(prop.GetValue(strings), String)
                Assert.IsFalse(String.IsNullOrWhiteSpace(value), $"Missing text '{prop.Name}' for '{cultureName}'.")
            Next prop

        End Sub

        ''' <summary>The Spanish and Portuguese texts are actually translated (most texts differ from English).</summary>
        <TestMethod>
        <DataRow("es")>
        <DataRow("pt")>
        Public Sub FromCulture_Translations_DifferFromEnglish(cultureName As String)

            Dim english As UIStrings = UIStrings.FromCulture("en")
            Dim translated As UIStrings = UIStrings.FromCulture(cultureName)
            Dim properties As PropertyInfo() = UIStringsTests.GetTextProperties()

            Dim translatedCount As Integer = properties.Count(
                Function(p As PropertyInfo) Not String.Equals(DirectCast(p.GetValue(english), String), DirectCast(p.GetValue(translated), String), StringComparison.Ordinal))

            Assert.IsTrue(translatedCount >= properties.Length * 0.8, $"Only {translatedCount} of {properties.Length} texts are translated.")

        End Sub

        ''' <summary>The composite format texts have their placeholder and can be formatted, in every language.</summary>
        <TestMethod>
        <DataRow("en")>
        <DataRow("es")>
        <DataRow("pt")>
        Public Sub FormatTexts_HavePlaceholder(cultureName As String)

            Dim strings As UIStrings = UIStrings.FromCulture(cultureName)

            For Each format As String In {strings.AboutTitleFormat, strings.EntryCountPluralFormat}
                StringAssert.Contains(format, "{0}")
                Assert.IsFalse(String.Format(CultureInfo.InvariantCulture, format, "X").Contains("{0}"))
            Next format

        End Sub

#End Region

#Region " Culture Resolution "

        ''' <summary>Specific cultures resolve to their neutral language, and unsupported or empty cultures to English.</summary>
        <TestMethod>
        <DataRow("en", "en")>
        <DataRow("en-US", "en")>
        <DataRow("es", "es")>
        <DataRow("es-ES", "es")>
        <DataRow("ES-mx", "es")>
        <DataRow("pt-BR", "pt")>
        <DataRow("fr", "en")>
        <DataRow("", "en")>
        <DataRow(Nothing, "en")>
        Public Sub GetNeutralLanguage_ResolvesSupportedLanguage(cultureName As String, expected As String)

            Assert.AreEqual(expected, UIStrings.GetNeutralLanguage(cultureName))
            Assert.AreEqual(expected, UIStrings.FromCulture(cultureName).CultureName)

        End Sub

        ''' <summary>The supported languages are English, Spanish and Portuguese.</summary>
        <TestMethod>
        Public Sub SupportedCultureNames_AreEnglishSpanishPortuguese()

            CollectionAssert.AreEqual({"en", "es", "pt"}, UIStrings.SupportedCultureNames.ToArray())

        End Sub

#End Region

#Region " FormatEntryCount "

        ''' <summary>The entry count uses the singular form only for one entry.</summary>
        <TestMethod>
        <DataRow("en", 0, "0 entries")>
        <DataRow("en", 1, "1 entry")>
        <DataRow("en", 5, "5 entries")>
        <DataRow("es", 1, "1 entrada")>
        <DataRow("es", 12, "12 entradas")>
        <DataRow("pt", 2, "2 entradas")>
        Public Sub FormatEntryCount_UsesSingularAndPlural(cultureName As String, count As Integer, expected As String)

            Assert.AreEqual(expected, UIStrings.FromCulture(cultureName).FormatEntryCount(count))

        End Sub

        ''' <summary>A negative entry count is rejected.</summary>
        <TestMethod>
        Public Sub FormatEntryCount_Negative_ThrowsArgumentOutOfRangeException()

            Assert.ThrowsExactly(Of ArgumentOutOfRangeException)(Sub() UIStrings.FromCulture("en").FormatEntryCount(-1))

        End Sub

#End Region

    End Class

End Namespace

#End Region
