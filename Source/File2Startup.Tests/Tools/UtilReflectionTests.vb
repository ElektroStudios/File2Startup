' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="UtilReflectionTests.vb" company="Elektro Studios">
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
Imports System.Reflection

Imports DevCase.Core.Diagnostics.Assembly.Reflection

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " UtilReflectionTests "

Namespace Tools

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests for the <see cref="UtilReflection"/> class.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    Public NotInheritable Class UtilReflectionTests

#Region " Sample Type "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' A sample type with properties of different visibility and type.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private NotInheritable Class Sample

            ''' <summary>Gets or sets a public string property.</summary>
            Public Property Name As String = "Sample"

            ''' <summary>Gets or sets a public integer property.</summary>
            Public Property Count As Integer = 42

            ''' <summary>Gets or sets a public property whose value is Nothing.</summary>
            Public Property Missing As String = Nothing

            ''' <summary>Gets a private property.</summary>
            Private ReadOnly Property Secret As String = "hidden"

        End Class

#End Region

#Region " GetProperty "

        ''' <summary>A property is found by name, from an object or from a type.</summary>
        <TestMethod>
        Public Sub GetProperty_ExistingProperty_ReturnsPropertyInfo()

            Dim fromObject As PropertyInfo = UtilReflection.GetProperty(New Sample(), "Name")
            Dim fromType As PropertyInfo = UtilReflection.GetProperty(GetType(Sample), "Count")

            Assert.AreEqual("Name", fromObject.Name)
            Assert.AreEqual(GetType(Integer), fromType.PropertyType)

        End Sub

        ''' <summary>Non-public properties are found with the default binding flags.</summary>
        <TestMethod>
        Public Sub GetProperty_PrivateProperty_IsFoundWithDefaultFlags()

            Assert.AreEqual("Secret", UtilReflection.GetProperty(GetType(Sample), "Secret").Name)

        End Sub

        ''' <summary>A missing property (or one excluded by the binding flags) throws <see cref="ArgumentException"/>.</summary>
        <TestMethod>
        Public Sub GetProperty_MissingProperty_ThrowsArgumentException()

            Assert.ThrowsExactly(Of ArgumentException)(Sub() UtilReflection.GetProperty(GetType(Sample), "DoesNotExist"))
            Assert.ThrowsExactly(Of ArgumentException)(Sub() UtilReflection.GetProperty(GetType(Sample), "Secret", BindingFlags.Instance Or BindingFlags.Public))

        End Sub

        ''' <summary>Invalid arguments throw <see cref="ArgumentNullException"/>.</summary>
        <TestMethod>
        Public Sub GetProperty_InvalidArguments_ThrowArgumentNullException()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() UtilReflection.GetProperty(DirectCast(Nothing, Type), "Name"))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() UtilReflection.GetProperty(GetType(Sample), " "))

        End Sub

#End Region

#Region " GetPropertyValue "

        ''' <summary>The value of a property is returned and converted to the requested type.</summary>
        <TestMethod>
        Public Sub GetPropertyValue_ReturnsConvertedValue()

            Dim instance As New Sample()

            Assert.AreEqual("Sample", UtilReflection.GetPropertyValue(Of String)(instance, "Name"))
            Assert.AreEqual(42, UtilReflection.GetPropertyValue(Of Integer)(instance, "Count"))
            Assert.AreEqual(42L, UtilReflection.GetPropertyValue(Of Long)(instance, "Count"))
            Assert.AreEqual("hidden", UtilReflection.GetPropertyValue(Of String)(instance, "Secret"))

        End Sub

        ''' <summary>A Nothing value returns the specified default value.</summary>
        <TestMethod>
        Public Sub GetPropertyValue_NothingValue_ReturnsDefault()

            Dim instance As New Sample()

            Assert.IsNull(UtilReflection.GetPropertyValue(Of String)(instance, "Missing"))
            Assert.AreEqual("fallback", UtilReflection.GetPropertyValue(Of String)(instance, "Missing", "fallback"))

        End Sub

        ''' <summary>Invalid arguments throw <see cref="ArgumentNullException"/>.</summary>
        <TestMethod>
        Public Sub GetPropertyValue_InvalidArguments_ThrowArgumentNullException()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() UtilReflection.GetPropertyValue(Of String)(Nothing, "Name"))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() UtilReflection.GetPropertyValue(Of String)(New Sample(), ""))

        End Sub

#End Region

    End Class

End Namespace

#End Region
