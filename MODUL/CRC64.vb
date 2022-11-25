Public Class CRC64
    Shared table As ULong()
    Shared Sub New()
        Dim poly As ULong = &HC96C5795D7870F42UL
        table = New ULong(255) {}
        Dim temp As ULong = 0
        For i As ULong = 0 To table.Length - 1
            temp = i
            For j As Integer = 8 To 1 Step -1
                If (temp And 1UL) = 1 Then
                    temp = CULng((temp >> 1) Xor poly)
                Else
                    temp >>= 1
                End If
            Next
            table(i) = temp
        Next
    End Sub

    Public Shared Function ComputeChecksum(bytes As Byte()) As ULong
        Dim crc As ULong = &HFFFFFFFFFFFFFFFFUL
        Dim i As Integer
        For i = 0 To bytes.Length - 1
            Dim index As Byte = CByte(((crc) And &HFFUL) Xor bytes(i))
            crc = CULng((crc >> 8) Xor table(index))
        Next i
        Return Not crc
    End Function
End Class
