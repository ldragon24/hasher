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

                End Using
            End Using

        Catch ex As Exception
            Trace.WriteLine(Err.Description)
        End Try

        Return stmp

    End Function

    Public Function GetSHA512(ByVal sfile As String) As String

        Dim stmp As String
        Try
            Using _sha512 As New System.Security.Cryptography.SHA512CryptoServiceProvider

                Using stream = File.OpenRead(sfile)
                    Dim hash2 = _sha512.ComputeHash(stream)
                    stmp = (BitConverter.ToString(hash2).Replace("-", String.Empty))

                End Using
            End Using

        Catch ex As Exception
            Trace.WriteLine(Err.Description)
        End Try

        Return stmp

    End Function

    Public Function GetSHA384(ByVal sfile As String) As String

        Dim stmp As String
        Try
            Using _sha384 As New System.Security.Cryptography.SHA384CryptoServiceProvider

                Using stream = File.OpenRead(sfile)
                    Dim hash2 = _sha384.ComputeHash(stream)
                    stmp = (BitConverter.ToString(hash2).Replace("-", String.Empty))

                End Using
            End Using

        Catch ex As Exception
            Trace.WriteLine(Err.Description)
        End Try

        Return stmp

    End Function

    Public Function GetSHA1(ByVal sfile As String) As String

        Using fs As FileStream = File.OpenRead(sfile)
            Dim sha As SHA1 = New SHA1Managed()
            Return BitConverter.ToString(sha.ComputeHash(fs))
        End Using

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

    Public Function GetGOST(ByVal sFileName As Byte()) As String
        'Dim G As GOST = New GOST(256)
        'Dim G512 As GOST = New GOST(512)
        'Dim message As Byte() = {&H32, &H31, &H30, &H39, &H38, &H37, &H36, &H35, &H34, &H33, &H32, &H31, &H30, &H39, &H38, &H37, &H36, &H35, &H34, &H33, &H32, &H31, &H30, &H39, &H38, &H37, &H36, &H35, &H34, &H33, &H32, &H31, &H30, &H39, &H38, &H37, &H36, &H35, &H34, &H33, &H32, &H31, &H30, &H39, &H38, &H37, &H36, &H35, &H34, &H33, &H32, &H31, &H30, &H39, &H38, &H37, &H36, &H35, &H34, &H33, &H32, &H31, &H30}


        'Dim res As Byte() = G.GetHashGost(sFileName)
        'Dim res2 As Byte() = G512.GetHashGost(sFileName)
        'Dim h256 = BitConverter.ToString(res)
        'Dim h512 = BitConverter.ToString(res2)

        'Return h512

    End Function

    Public Function ReadFileC(ByVal sFileName As String) As String

        Dim Sigma As Byte()

        Using fs As FileStream = New FileStream(sFileName, FileMode.Open, FileAccess.Read)

            Using br As BinaryReader = New BinaryReader(fs, New ASCIIEncoding())
                Dim chunk As Byte()

                chunk = br.ReadBytes(CHUNK_SIZE)

                While chunk.Length > 0

                    Select Case Len(sTEXT)

                        Case 0
                            sTEXT = GetGOST(Encoding.ASCII.GetBytes(chunk.ToString()))
                        Case Else
                            sTEXT = sTEXT + "-" + GetGOST(Encoding.ASCII.GetBytes(chunk.ToString()))
                    End Select

                    ' DumpBytes(chunk, chunk.Length)
                    chunk = br.ReadBytes(CHUNK_SIZE)

                End While

            End Using
        End Using

        'sTmp = sTEXT



        'нужно разбить существующую строку с хэщем на производные и делать это до предела, т.е. до того пока строка не будет 64 байта
        'Работает медленно, использовать нецелесообразно




        Return sTEXT

    End Function

    Public Function StrToByteArray(ByVal str As String) As Byte()

        Dim encoding As New System.Text.UTF8Encoding()
        Return encoding.GetBytes(str)

    End Function

    Public Function DumpBytes(ByVal bdata As Byte(), ByVal len As Integer)

        Dim i As Integer
        Dim j = 0
        Dim dchar As Char
        ' 3 * 16 chars for hex display, 16 chars for text and 8 chars
        ' for the 'gutter' int the middle.
        Dim dumptext As StringBuilder = New StringBuilder("        ", 16 * 4 + 8)
        For i = 0 To len - 1
            dumptext.Insert(j * 3, String.Format("{0:X2} ", CInt(bdata(i))))
            dchar = Microsoft.VisualBasic.ChrW(bdata(i))
            '' replace 'non-printable' chars with a '.'.
            If Char.IsWhiteSpace(dchar) OrElse Char.IsControl(dchar) Then
                dchar = "."c
            End If
            dumptext.Append(dchar)
            j += 1

            If j = 16 Then

                sTEXT = sTEXT + GetGOST(Encoding.ASCII.GetBytes(dumptext.ToString()))

                dumptext.Length = 0
                dumptext.Append("        ")
                j = 0
            End If
        Next
        ' display the remaining line

        If j > 0 Then
            For i = j To 15
                dumptext.Insert(j * 3, "   ")
            Next

            sTEXT = sTEXT + GetGOST(Encoding.ASCII.GetBytes(dumptext.ToString()))

        End If

        Return sTEXT

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
