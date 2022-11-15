Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Module hasher
    Public PrPath As String
    Public BasePath As String
    Private ReadOnly CHUNK_SIZE As Integer = 63

    Private sTEXT As String

    Public Function GetHash(theInput As String) As String

        Dim hash = MD5.Create()
        Dim hashValue() As Byte

        Dim fileStream As FileStream = File.OpenRead(theInput)
        fileStream.Position = 0
        hashValue = hash.ComputeHash(fileStream)
        Dim hash_hex = PrintByteArray(hashValue)
        fileStream.Close()
        Return hash_hex

    End Function

    Public Function PrintByteArray(ByVal array() As Byte)
        Dim hex_value As String = ""
        Dim i As Integer
        For i = 0 To array.Length - 1
            hex_value += array(i).ToString("X2")
        Next i
        Return hex_value.ToLower
    End Function

    Public Function GetSHA256(ByVal sfile As String) As String

        Dim stmp As String
        Try
            Using _sha512 As New System.Security.Cryptography.SHA256CryptoServiceProvider

                Using stream = File.OpenRead(sfile)
                    Dim hash2 = _sha512.ComputeHash(stream)
                    stmp = (BitConverter.ToString(hash2).Replace("-", String.Empty))
                    'Trace.WriteLine(String.Format("{0}", stmp))
                    Return stmp

                End Using
            End Using

        Catch ex As Exception
            Trace.WriteLine(Err.Description)
            Return stmp
        End Try

    End Function

    Public Function GetSHA512(ByVal sfile As String) As String

        Dim stmp As String
        Try
            Using _sha512 As New System.Security.Cryptography.SHA512CryptoServiceProvider

                Using stream = File.OpenRead(sfile)
                    Dim hash2 = _sha512.ComputeHash(stream)
                    stmp = (BitConverter.ToString(hash2).Replace("-", String.Empty))
                    ' Trace.WriteLine(String.Format("{0}", stmp))
                    Return stmp
                End Using
            End Using

        Catch ex As Exception
            Trace.WriteLine(Err.Description)
            Return stmp
        End Try

    End Function

    Public Function GetSha384Hash(ByVal sfile As String) As String
        Dim _result$ = ""

        Try
            Using _sha512 As New System.Security.Cryptography.SHA384CryptoServiceProvider
                Using stream = File.OpenRead(sfile)
                    Dim _hash = _sha512.ComputeHash(stream)
                    _result$ = BitConverter.ToString(_hash).Replace("-", String.Empty)
                    ' Trace.WriteLine(String.Format("{0}", _result$))
                    Return _result$
                End Using
            End Using
        Catch ex As Exception
            Trace.WriteLine(Err.Description)
            Return _result$
        End Try

    End Function

    Public Function GetSHA1(ByVal sfile As String) As String

        Using fs As FileStream = File.OpenRead(sfile)
            Dim sha As SHA1 = New SHA1Managed()
            Return BitConverter.ToString(sha.ComputeHash(fs)).Replace("-", String.Empty)
        End Using

    End Function

    Public Function GetStribog(ByVal sfile As String) As String

        Dim gst As GOST2 = New GOST2(512)
        Dim gst512 As String

        Using fs = File.OpenRead(sfile)
            Dim fileData = New Byte(fs.Length - 1) {}
            fs.Read(fileData, 0, fs.Length)
            Dim checkSum As Byte() = gst.GetHashGOST(fileData)
            gst512 = BitConverter.ToString(checkSum)
            Return gst512
        End Using

    End Function

    Public Function GetSHA224(ByVal sfile As String) As String

        Return SHA224.SHA224(sfile)

    End Function

    Public Function GetCRC32(ByVal sFileName As String) As String
        Try
            Dim FS As FileStream = New FileStream(sFileName, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
            Dim CRC32Result As Integer = &HFFFFFFFF
            Dim Buffer(4096) As Byte
            Dim ReadSize As Integer = 4096
            Dim Count As Integer = FS.Read(Buffer, 0, ReadSize)
            Dim CRC32Table(256) As Integer
            Dim DWPolynomial As Integer = &HEDB88320
            Dim DWCRC As Integer
            Dim i As Integer, j As Integer, n As Integer

            For i = 0 To 255
                DWCRC = i
                For j = 8 To 1 Step -1
                    If (DWCRC And 1) Then
                        DWCRC = ((DWCRC And &HFFFFFFFE) \ 2&) And &H7FFFFFFF
                        DWCRC = DWCRC Xor DWPolynomial
                    Else
                        DWCRC = ((DWCRC And &HFFFFFFFE) \ 2&) And &H7FFFFFFF
                    End If
                Next j
                CRC32Table(i) = DWCRC
            Next i

            Do While (Count > 0)
                For i = 0 To Count - 1
                    n = (CRC32Result And &HFF) Xor Buffer(i)
                    CRC32Result = ((CRC32Result And &HFFFFFF00) \ &H100) And &HFFFFFF
                    CRC32Result = CRC32Result Xor CRC32Table(n)
                Next i
                Count = FS.Read(Buffer, 0, ReadSize)
            Loop
            Return Hex(Not (CRC32Result))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    'Public Function GetGOST(inputStream As String) As String

    '    Dim gh = New GOST()
    '    Dim bin = File.ReadAllBytes(inputStream)

    '    Return (gh.ComputeHash(bin))

    'End Function

    'Public Function GetMagMa(inputStream As String)

    '    Dim gh = New MagmaProvider()

    '    Return (gh.OFBEncrypt(File.ReadAllBytes(inputStream), 66467))

    'End Function


    Public Function StrToByteArray(ByVal str As String) As Byte()

        Dim encoding As New System.Text.UTF8Encoding()
        Return encoding.GetBytes(str)

    End Function


    Public Function FileToByteArray(ByVal _FileName As String) As Byte()
        Dim _Buffer() As Byte = Nothing

        Try
            ' Открыть файл для чтения
            Dim _FileStream As New System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)

            ' Присоединить поток файлов к устройству чтения двоичных файлов
            Dim _BinaryReader As New System.IO.BinaryReader(_FileStream)

            ' получить общую длину файла в байтах
            Dim _TotalBytes As Long = New System.IO.FileInfo(_FileName).Length

            ' прочитать весь файл в буфер
            _Buffer = _BinaryReader.ReadBytes(CInt(Fix(_TotalBytes)))

            ' закрыть устройство чтения файлов
            _FileStream.Close()
            _FileStream.Dispose()
            _BinaryReader.Close()
        Catch _Exception As Exception
            ' Error
            MsgBox("Exception caught in process: {0}", _Exception.ToString())

        End Try

        Return _Buffer
    End Function

    Private Sub StribogChaeckSumm()
        Throw New NotImplementedException
    End Sub


End Module
