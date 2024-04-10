' ***********************************************************************
' Author   : ElektroStudios
' Modified : 06-March-2024
' ***********************************************************************

' UNUSED CODE HAS BEEN PRUNED. 06/APRIL/2024




' THIS OPEN-SOURCE APPLICATION IS POWERED BY DEVCASE CLASS LIBRARY, CREATED BY ELEKTRO STUDIOS.

' WHAT YOU SEE HERE IS FREE CUTTED CONTENT OF THIS FRAMEWORK.

' IF YOU LIKED THIS FREE APPLICATION, THEN PLEASE CONSIDER TO BUY DEVCASE CLASS LIBRARY FOR .NET AT:
' https://codecanyon.net/item/DevCase-class-library-for-net/19260282

' YOU CAN FIND THESE HELPER METHODS AND A MASSIVE AMOUNT MORE!, 
' +850 EXTENSION METHODS FOR ALL KIND OF TYPES, CUSTOM USER-CONTROLS, 
' EVERYTHING FOR THE NEWBIE And THE ADVANCED USER, FOR VB.NET AND C#. 

' DevCase is a utility framework containing new APIs and extensions to the core .NET Framework 
' to help complete your developer toolbox.
' It Is a set of general purpose classes provided as easy to consume packages.
' These utility classes and components provide productivity in day to day software development 
' mainly focused To WindowsForms. 

' UPDATES OF DevCase ARE MAINTAINED AND RELEASED EVERY MONTH.




#Region " Public Members Summary "

#Region " Functions "

' IsNetAssembly(String) As Boolean
' GetAssemblyPEKind(String) As PortableExecutableKinds
' GetAssemblyTargetPlatform(String) As ImageFileMachine

' GetCLRVersion(String) As Version

' GetTargetFrameworkName(String) As String
' GetTargetFrameworkVersion(String) As Version

' GetAllEnums(Assembly) As IEnumerable(Of Type)

' TypeHasPublicConstructor(Type) As Boolean
' TypeHasPublicDefaultConstructor(Type) As Boolean

' GetAllConstructors(Object,Opt: BindingFlags) As IEnumerable(Of ConstructorInfo)
' GetAllConstructors(Of T)(Opt: BindingFlags) As IEnumerable(Of ConstructorInfo)
' GetAllConstructors(Type,Opt: BindingFlags) As IEnumerable(Of ConstructorInfo)

' GetAllFields(Object, Boolean,Opt: BindingFlags) As IEnumerable(Of FieldInfo)
' GetAllFields(Of T)(Boolean,Opt: BindingFlags) As IEnumerable(Of FieldInfo)
' GetAllFields(Type,Boolean,Opt: BindingFlags) As IEnumerable(Of FieldInfo)

' GetAllInterfaces(Object) As Type()
' GetAllInterfaces(Of T)() As Type()
' GetAllInterfaces(Type) As Type()

' GetAllMethods(Object, Boolean, Opt: BindingFlags) As IEnumerable(Of MethodInfo)
' GetAllMethods(Of T)(Boolean, Opt: BindingFlags) As IEnumerable(Of MethodInfo)
' GetAllMethods(Type, Boolean, Opt: BindingFlags) As IEnumerable(Of MethodInfo)

' GetAllProperties(Object, Boolean, Opt: BindingFlags) As IEnumerable(Of PropertyInfo)
' GetAllProperties(Of T)(Boolean, Opt: BindingFlags) As IEnumerable(Of PropertyInfo)
' GetAllProperties(Type, Boolean, Opt: BindingFlags) As IEnumerable(Of PropertyInfo)

' GetField(Object, String, Opt: BindingFlags) As FieldInfo
' GetField(Of T)(String, Opt: BindingFlags) As FieldInfo
' GetField(Type, String, Opt: BindingFlags) As FieldInfo

' GetFieldValue(Of T)(Object, String, Opt: BindingFlags) As T
' GetFieldValue(Of T)(Object, String, T, Opt: BindingFlags) As T

' GetInterface(Object, String) As Type
' GetInterface(Of T)(String) As Type
' GetInterface(Type, String) As Type

' GetMethod(Object, String, Opt: BindingFlags) As MethodInfo
' GetMethod(Object, String, Type()) As MethodInfo
' GetMethod(Type, String, Opt: BindingFlags) As MethodInfo
' GetMethod(Type, String, Type()) As MethodInfo

' GetProperty(Object, String, Opt: BindingFlags) As PropertyInfo
' GetProperty(Of T)(String, Opt: BindingFlags) As PropertyInfo
' GetProperty(Type, String, Opt: BindingFlags) As PropertyInfo

' GetPropertyValue(Of T)(Object, String, Opt: BindingFlags) As T
' GetPropertyValue(Of T)(Object, String, T, Opt: BindingFlags) As T

' GetAllBaseTypes(Of T)() As IEnumerable(Of Type)
' GetAllDerivedTypes(Of T)() As IEnumerable(Of Type)
' GetAllDerivedTypes(Of T)(Assembly) As IEnumerable(Of Type)

#End Region

#End Region

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Reflection

#End Region

#Region " Reflection Util "

' ReSharper disable once CheckNamespace

Namespace DevCase.Core.Diagnostics.Assembly.Reflection

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains Reflection related utilites.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa383751%28v=vs.85%29.aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class UtilReflection

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="UtilReflection"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Searches for the specified property in the specified object.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code language="VB.NET">
        ''' Dim prop As PropertyInfo = GetProperty(Me, "Text")
        ''' Dim text As String = DirectCast(prop.GetValue(Me), String)
        ''' 
        ''' Console.WriteLine(text)
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="obj">
        ''' The source object from which to retrieve the property.
        ''' </param>
        ''' 
        ''' <param name="propName">
        ''' The name of the property to search for.
        ''' </param>
        ''' 
        ''' <param name="bindingFlags">
        ''' Flags that controls binding and the way in which the search for members and types is conducted by Reflection.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' A <see cref="PropertyInfo"/> instance that represents the obtained property.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentException">
        ''' Property not found using the current flags.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function GetProperty(obj As Object,
                                           propName As String,
                                           Optional bindingFlags As BindingFlags =
                                                                          BindingFlags.Instance Or
                                                                          BindingFlags.FlattenHierarchy Or
                                                                          BindingFlags.Static Or
                                                                          BindingFlags.Public Or
                                                                          BindingFlags.NonPublic Or
                                                                          BindingFlags.GetProperty) As PropertyInfo

            Return UtilReflection.GetProperty(obj.GetType(), propName, bindingFlags)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Searches for the specified property in the specified <see cref="Type"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code language="VB.NET">
        ''' Dim prop As PropertyInfo = GetProperty(GetType(Form), "Text")
        ''' Dim text As String = DirectCast(prop.GetValue(Me), String)
        ''' 
        ''' Console.WriteLine(text)
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="type">
        ''' The source type to search for the property.
        ''' </param>
        ''' 
        ''' <param name="propName">
        ''' The name of the property to search for.
        ''' </param>
        ''' 
        ''' <param name="bindingFlags">
        ''' Flags that controls binding and the way in which the search for members and types is conducted by Reflection.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' A <see cref="PropertyInfo"/> instance that represents the obtained property.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' </exception>
        ''' 
        ''' <exception cref="ArgumentException">
        ''' Property not found using the current flags.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function GetProperty(type As Type,
                                           propName As String,
                                           Optional bindingFlags As BindingFlags =
                                                                          BindingFlags.Instance Or
                                                                          BindingFlags.FlattenHierarchy Or
                                                                          BindingFlags.Static Or
                                                                          BindingFlags.Public Or
                                                                          BindingFlags.NonPublic Or
                                                                          BindingFlags.GetProperty) As PropertyInfo

#If Not NETCOREAPP Then
            If type Is Nothing Then
                Throw New ArgumentNullException(NameOf(type))
            End If
#Else
            ArgumentNullException.ThrowIfNull(type, NameOf(type))
#End If

            If String.IsNullOrWhiteSpace(propName) Then
                Throw New ArgumentNullException(paramName:=NameOf(propName))
            End If

            Dim prop As PropertyInfo = type.GetProperty(propName, bindingFlags)
            If prop Is Nothing Then
                Throw New ArgumentException(paramName:=NameOf(bindingFlags), message:="Property not found using the current flags.")
            End If
            Return prop

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Searches for the specified property in the specified object, and returns the property value.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code language="VB.NET">
        ''' Dim text As String = GetPropertyValue(Of String)(Me, "Text")
        ''' 
        ''' Console.WriteLine(text)
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <typeparam name="T">
        ''' The type of the property to be returned.
        ''' </typeparam>
        '''
        ''' <param name="obj">
        ''' The source object from which to retrieve the property.
        ''' </param>
        ''' 
        ''' <param name="propName">
        ''' The name of the property to search for.
        ''' </param>
        ''' 
        ''' <param name="bindingFlags">
        ''' Flags that controls binding and the way in which the search for members and types is conducted by Reflection.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The property value.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' </exception>
        ''' 
        ''' <exception cref="ArgumentException">
        ''' Property not found using the current flags.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function GetPropertyValue(Of T)(obj As Object,
                                                      propName As String,
                                                      Optional bindingFlags As BindingFlags =
                                                                                     BindingFlags.Instance Or
                                                                                     BindingFlags.FlattenHierarchy Or
                                                                                     BindingFlags.Static Or
                                                                                     BindingFlags.Public Or
                                                                                     BindingFlags.NonPublic Or
                                                                                     BindingFlags.GetProperty) As T

            Return UtilReflection.GetPropertyValue(Of T)(obj, propName, Nothing, bindingFlags)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Searches for the specified property in the specified object, and returns the property value.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code language="VB.NET">
        ''' Dim text As String = GetPropertyValue(Of String)(Me, "Text", String.Empty)
        ''' 
        ''' Console.WriteLine(text)
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <typeparam name="T">
        ''' The type of the property to be returned.
        ''' </typeparam>
        '''
        ''' <param name="obj">
        ''' The source object from which to retrieve the property.
        ''' </param>
        ''' 
        ''' <param name="propName">
        ''' The name of the property to search for.
        ''' </param>
        ''' 
        ''' <param name="defaultIfEmpty">
        ''' The default value to return in case of the property value is <see langword="Nothing"/>.
        ''' </param>
        ''' 
        ''' <param name="bindingFlags">
        ''' Flags that controls binding and the way in which the search for members and types is conducted by Reflection.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The property value.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' </exception>
        ''' 
        ''' <exception cref="ArgumentException">
        ''' Property not found using the current flags.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function GetPropertyValue(Of T)(obj As Object,
                                                      propName As String,
                                                      defaultIfEmpty As T,
                                                      Optional bindingFlags As BindingFlags =
                                                                                     BindingFlags.Instance Or
                                                                                     BindingFlags.FlattenHierarchy Or
                                                                                     BindingFlags.Static Or
                                                                                     BindingFlags.Public Or
                                                                                     BindingFlags.NonPublic Or
                                                                                     BindingFlags.GetProperty) As T

#If Not NETCOREAPP Then
            If obj Is Nothing Then
                Throw New ArgumentNullException(NameOf(obj))
            End If
#Else
            ArgumentNullException.ThrowIfNull(obj, NameOf(obj))
#End If

            If String.IsNullOrWhiteSpace(propName) Then
                Throw New ArgumentNullException(paramName:=NameOf(propName))
            End If

            Dim prop As PropertyInfo = UtilReflection.GetProperty(obj, propName, bindingFlags)
            If prop Is Nothing Then
                Throw New ArgumentException(paramName:=NameOf(bindingFlags), message:="Property not found using the current flags.")
            End If

            Dim value As Object = prop.GetValue(obj, bindingFlags, Type.DefaultBinder, Nothing, Nothing)
            Return If(value Is Nothing, defaultIfEmpty, DirectCast(Convert.ChangeType(value, GetType(T)), T))

        End Function

#End Region

    End Class

End Namespace

#End Region
