Namespace Blake2sCSharp
    Public Module Blake2S
        Public Function Create() As Hasher
            Return Blake2S.Create(New Blake2sConfig())
        End Function

        Public Function Create(ByVal config As Blake2sConfig) As Hasher
            Return New Blake2sHasher(config)
        End Function

        Public Function ComputeHash(ByVal data As Byte(), ByVal start As Integer, ByVal count As Integer) As Byte()
            Return Blake2S.ComputeHash(data, start, count, Nothing)
        End Function

        Public Function ComputeHash(ByVal data As Byte()) As Byte()
            Return Blake2S.ComputeHash(data, 0, data.Length, Nothing)
        End Function

        Public Function ComputeHash(ByVal data As Byte(), ByVal config As Blake2sConfig) As Byte()
            Return Blake2S.ComputeHash(data, 0, data.Length, config)
        End Function

        Public Function ComputeHash(ByVal data As Byte(), ByVal start As Integer, ByVal count As Integer, ByVal config As Blake2sConfig) As Byte()
            Dim hasher = Blake2S.Create(config)
            hasher.Update(data, start, count)
            Return hasher.Finish()
        End Function
    End Module

End Namespace