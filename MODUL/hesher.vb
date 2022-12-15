Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Text.RegularExpressions

Module hasher
    Public PrPath As String
    Public BasePath As String
    Private ReadOnly CHUNK_SIZE As Integer = 63

    Private sTEXT As String

    Public Function GetHash(sfile As String) As String

        Dim hash = MD5.Create()
        Dim fInfo As FileInfo = New FileInfo(sfile)

        Using fileStream1 = fInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read)
            Try

                fileStream1.Position = 0

                Dim hashValue1 = hash.ComputeHash(fileStream1)
                PrintByteArray(hashValue1)

                Return UCase(PrintByteArray(hashValue1))

            Catch e As IOException

            Catch e As UnauthorizedAccessException

            End Try

        End Using

        ' Return hash_hex
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

                Dim fInfo As FileInfo = New FileInfo(sfile)
                Using fileStream1 = fInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read)
                    Try

                        Dim hash2 = _sha512.ComputeHash(fileStream1)
                        stmp = (BitConverter.ToString(hash2).Replace("-", String.Empty))
                        Return UCase(stmp)

                    Catch e As IOException

                    Catch e As UnauthorizedAccessException

                    End Try

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

                Dim fInfo As FileInfo = New FileInfo(sfile)
                Using fileStream1 = fInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read)
                    Try

                        Dim hash2 = _sha512.ComputeHash(fileStream1)
                        stmp = (BitConverter.ToString(hash2).Replace("-", String.Empty))
                        Return UCase(stmp)

                    Catch e As IOException

                    Catch e As UnauthorizedAccessException

                    End Try

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

                Dim fInfo As FileInfo = New FileInfo(sfile)
                Using fileStream1 = fInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read)
                    Try

                        Dim hash2 = _sha512.ComputeHash(fileStream1)
                        _result$ = BitConverter.ToString(hash2).Replace("-", String.Empty)
                        Return UCase(_result$)

                    Catch e As IOException

                    Catch e As UnauthorizedAccessException

                    End Try

                End Using


            End Using
        Catch ex As Exception
            Trace.WriteLine(Err.Description)
            Return _result$
        End Try

    End Function

    Public Function GetAdler32(ByVal sfile As String) As String

        Try
            Dim arrB() As Byte = My.Computer.FileSystem.ReadAllBytes(sfile)
            Return UCase(Adler32(arrB))

        Catch ex As Exception

        End Try

    End Function

    Function Adler32(arrB() As Byte) As String

        Dim s1 As UInt32 = 1
        Dim s2 As UInt32 = 0

        For Each byteB In arrB
            s1 = (s1 + byteB) Mod 65521
            s2 = (s2 + s1) Mod 65521
        Next

        s2 = (s2 << 16) + s1

        Return s2.ToString("X2").PadLeft(8, "0"c)

    End Function

    Public Function GetRipEmd(ByVal sfile As String) As String

        Try

            Dim fInfo As FileInfo = New FileInfo(sfile)

            Using fileStream1 = fInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read)
                Dim myRIPEMD160 As RIPEMD160 = RIPEMD160Managed.Create()
                Dim hashValue As Byte()

                hashValue = myRIPEMD160.ComputeHash(fileStream1)
                PrintByteArray(hashValue)
                Return UCase(PrintByteArray(hashValue))
            End Using

        Catch __unusedDirectoryNotFoundException1__ As DirectoryNotFoundException


        Catch __unusedIOException2__ As IOException


        End Try

    End Function

    Public Function GetSHA1(ByVal sfile As String) As String

        ' If IsValidPath(sfile) = True Then

        Dim fInfo As FileInfo = New FileInfo(sfile)
        Using fileStream1 = fInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read)
            Try

                Dim sha As SHA1 = New SHA1Managed()
                Return UCase(BitConverter.ToString(sha.ComputeHash(fileStream1)).Replace("-", String.Empty))

            Catch e As IOException

            Catch e As UnauthorizedAccessException

            End Try

        End Using

        ' End If
    End Function

    Public Function GetCRC64(ByVal sfile As String) As String

        Dim fInfo As FileInfo = New FileInfo(sfile)
        Using fileStream1 = fInfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Try

                Return UCase(Hex(CRC64.ComputeChecksum(System.IO.File.ReadAllBytes(sfile))))

            Catch e As IOException

            Catch e As UnauthorizedAccessException

            End Try

        End Using

    End Function

    'Public Function GetBlake2(ByVal sfile As String) As String

    '    Dim fInfo As FileInfo = New FileInfo(sfile)
    '    Using fileStream1 = fInfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '        Try

    '            Return UCase(Blake2sCSharp.ComputeHash(System.IO.File.ReadAllBytes(sfile)))

    '        Catch e As IOException

    '        Catch e As UnauthorizedAccessException

    '        End Try

    '    End Using

    'End Function

    Public Function IsValidPath(ByVal path As String) As Boolean
        Dim r As Regex = New Regex("^(([a-zA-Z]\:)|(\\))(\\{1}|((\\{1})[^\\]([/:*?<>""|]*))+)$")
        Return r.IsMatch(path)
    End Function

    Public Function FilenameIsOK(ByVal fileNameAndPath As String) As Boolean
        Dim fileName = Path.GetFileName(fileNameAndPath)
        Dim directory = Path.GetDirectoryName(fileNameAndPath)

        For Each c In Path.GetInvalidFileNameChars()
            If fileName.Contains(c) Then
                Return False
            Else
                Return True
            End If
        Next

        'For Each c In Path.GetInvalidPathChars()
        '    If directory.Contains(c) Then
        '        Return False
        '    End If
        'Next
        '
    End Function

    Public Function GetStribog(ByVal sfile As String) As String

        'Dim gst As GOST2 = New GOST2(512)
        'Dim gst512 As String

        'Using fs = File.OpenRead(sfile)
        '    Dim fileData = New Byte(fs.Length - 1) {}
        '    fs.Read(fileData, 0, fs.Length)
        '    Dim checkSum As Byte() = gst.GetHashGOST(fileData)
        '    gst512 = BitConverter.ToString(checkSum)
        '    Return gst512
        'End Using

    End Function

    Public Function GetSHA224(ByVal sfile As String) As String

        ' Return SHA224.SHA224(sfile)

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

End Module
