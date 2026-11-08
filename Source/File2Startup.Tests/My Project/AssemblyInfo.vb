' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="AssemblyInfo.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " Assembly Attributes "

' The tests share process-wide state (the static control-hints dictionary of ControlHintManager,
' the current user registry and process environment variables), so they must not run in parallel.
<Assembly: DoNotParallelize>

#End Region
