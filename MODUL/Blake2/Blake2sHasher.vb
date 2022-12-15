
Imports System
Imports System.Security.Cryptography

Namespace Blake2sCSharp
    Friend Class Blake2sHasher
        Inherits hasher
        Private core As Blake2sCore = New Blake2sCore()
        Private rawConfig As UInteger() = Nothing ' no longer read only
        Private key As Byte() = Nothing
        Private outputSizeInBytes As Integer
        Private Shared ReadOnly DefaultConfig As Blake2sConfig = New Blake2sConfig()

        Public Overrides Sub Init()
            core.Initialize(rawConfig)
            If key IsNot Nothing Then
                core.HashCore(key, 0, key.Length)
            End If
        End Sub

        Public Overrides Function Finish() As Byte()
            Dim fullResult As Byte() = core.HashFinal()
            If outputSizeInBytes <> fullResult.Length Then
                Dim result = New Byte(outputSizeInBytes - 1) {}
                Array.Copy(fullResult, result, result.Length)
                Return result
            Else
                Return fullResult
            End If
        End Function

        Public Sub New(ByVal config As Blake2sConfig)
            If config Is Nothing Then config = DefaultConfig
            rawConfig = Blake2sIvBuilder.ConfigS(config) ', null); no tree config;
            If config.Key IsNot Nothing AndAlso config.Key.Length <> 0 Then
                key = New Byte(Blake2sCore.BlockSizeInBytes - 1) {} '  DOES THIS NEED TO BE THE BLOCK LENGTH?!
                Array.Copy(config.Key, key, config.Key.Length)
            End If
            outputSizeInBytes = config.OutputSizeInBytes
            Init()
        End Sub

        Public Overrides Sub Update(ByVal data As Byte(), ByVal start As Integer, ByVal count As Integer)
            core.HashCore(data, start, count)
        End Sub
    End Class

    Public MustInherit Class Hasher
        Public MustOverride Sub Init()
        Public MustOverride Function Finish() As Byte()
        Public MustOverride Sub Update(ByVal data As Byte(), ByVal start As Integer, ByVal count As Integer)

        Public Sub Update(ByVal data As Byte())
            Update(data, 0, data.Length)
        End Sub

        Public Function AsHashAlgorithm() As HashAlgorithm
            Return New HashAlgorithmAdapter(Me)
        End Function

        Friend Class HashAlgorithmAdapter
            Inherits HashAlgorithm
            Private ReadOnly _hasher As hasher

            Protected Overrides Sub HashCore(ByVal array As Byte(), ByVal ibStart As Integer, ByVal cbSize As Integer)
                _hasher.Update(array, ibStart, cbSize)
            End Sub

            Protected Overrides Function HashFinal() As Byte()
                Return _hasher.Finish()
            End Function

            Public Overrides Sub Initialize()
                _hasher.Init()
            End Sub

            Public Sub New(ByVal hasher As hasher)
                _hasher = hasher
            End Sub
        End Class
    End Class
End Namespace