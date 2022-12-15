Imports System

Namespace Blake2sCSharp
    Public NotInheritable Class Blake2sConfig
        Implements ICloneable
        Public Property Personalization As Byte()
        Public Property Salt As Byte()
        Public Property Key As Byte()
        Public Property OutputSizeInBytes As Integer
        Public Property OutputSizeInBits As Integer
            Get
                Return OutputSizeInBytes * 8
            End Get
            Set(ByVal value As Integer)
                If value Mod 8 = 0 Then Throw New ArgumentException("Output size must be a multiple of 8 bits")
                OutputSizeInBytes = value / 8
            End Set
        End Property

        Public Sub New()
            OutputSizeInBytes = 32
        End Sub

        Public Function Clone() As Blake2sConfig
            Dim result = New Blake2sConfig()
            result.OutputSizeInBytes = OutputSizeInBytes
            If Key IsNot Nothing Then result.Key = CType(Key.Clone(), Byte())
            If Personalization IsNot Nothing Then result.Personalization = CType(Personalization.Clone(), Byte())
            If Salt IsNot Nothing Then result.Salt = CType(Salt.Clone(), Byte())
            Return result
        End Function

        Private Function Clone1() As Object Implements ICloneable.Clone
            Return Clone()
        End Function
    End Class
End Namespace