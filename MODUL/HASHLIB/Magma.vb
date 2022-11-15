Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

Friend Class MagmaProvider

    Private _key As String
    Private _roundkeys As UInteger()

    ''' <summary>
    ''' id-tc26-gost-28147-param-Z
    ''' </summary>
    Private ReadOnly _sbox As UInteger(,) = New UInteger(7, 15) {
{&HC, &H4, &H6, &H2, &HA, &H5, &HB, &H9, &HE, &H8, &HD, &H7, &H0, &H3, &HF, &H1},
{&H6, &H8, &H2, &H3, &H9, &HA, &H5, &HC, &H1, &HE, &H4, &H7, &HB, &HD, &H0, &HF},
{&HB, &H3, &H5, &H8, &H2, &HF, &HA, &HD, &HE, &H1, &H7, &H4, &HC, &H9, &H6, &H0},
{&HC, &H8, &H2, &H1, &HD, &H4, &HF, &H6, &H7, &H0, &HA, &H5, &H3, &HE, &H9, &HB},
{&H7, &HF, &H5, &HA, &H8, &H1, &H6, &HD, &H0, &H9, &H3, &HE, &HB, &H4, &H2, &HC},
{&H5, &HD, &HF, &H6, &H9, &H2, &HC, &HA, &HB, &H7, &H8, &H1, &H4, &H3, &HE, &H0},
{&H8, &HE, &H2, &H5, &H6, &H9, &H1, &HC, &HF, &H4, &HB, &H0, &HD, &HA, &H3, &H7},
        {&H1, &H7, &HE, &HD, &H0, &H5, &H8, &H3, &H4, &HF, &HA, &H6, &H9, &HC, &HB, &H2}}

    Public Property Key As String
        Get
            Return _key
        End Get
        Set(ByVal value As String)
            If Encoding.UTF8.GetBytes(value).Length = 32 Then
                _key = value
                _roundkeys = GetRoundKeys(Encoding.UTF8.GetBytes(value))
            Else
                Throw New CryptographicException("Key length is not equal to 256 bit, but to {Encoding.UTF8.GetBytes(value).Length * 8}")
            End If
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal sboxseed As Integer)
        SeedSbox(sboxseed)
    End Sub

    Private Sub SeedSbox(ByVal seed As Integer)
        Dim r As Random = New Random(seed)
        Dim i = 0

        While i < 8
            Dim perm = Enumerable.Range(0, 16).OrderBy(Function(x) r.Next()).ToArray()
            Dim j = 0

            While j < 16
                _sbox(i, j) = CUInt(perm(j))
                Threading.Interlocked.Increment(j)
            End While

            Threading.Interlocked.Increment(i)
        End While
    End Sub

    Private Function GetRoundKeys(ByVal key As Byte()) As UInteger()
        Dim keyr = New Byte(key.Length - 1) {}
        Dim subkeys = New UInteger(7) {}
        Array.Copy(key, keyr, key.Length)
        '            Array.Reverse(keyr);
        For i = 0 To 7
            subkeys(i) = BitConverter.ToUInt32(keyr, i * 4)
        Next
        '            Array.Reverse(subkeys);
        Return subkeys
    End Function

    Private Function F(ByVal input As UInteger, ByVal key As UInteger) As UInteger
        Dim temp = S(input + key)
        Return temp << 11 Or temp >> 21
    End Function

    Private Function S(ByVal input As UInteger) As UInteger
        Dim res As UInteger = 0
        res = res Xor _sbox(0, input And &HF)
        res = res Xor _sbox(1, (input And &HF0) >> 4) << 4
        res = res Xor _sbox(2, (input And &HF00) >> 8) << 8
        res = res Xor _sbox(3, (input And &HF000) >> 12) << 12
        res = res Xor _sbox(4, (input And &HF0000) >> 16) << 16
        res = res Xor _sbox(5, (input And &HF00000) >> 20) << 20
        res = res Xor _sbox(6, (input And &HF000000) >> 24) << 24
        res = res Xor _sbox(7, (input And &HF0000000UI) >> 28) << 28
        Return res
    End Function

    Private Function Encrypt(ByVal data As Byte()) As Byte()
        Dim datar = New Byte(data.Length - 1) {}
        Array.Copy(data, datar, data.Length)
        '            Array.Reverse(datar);
        Dim a0 = BitConverter.ToUInt32(datar, 0)
        Dim a1 = BitConverter.ToUInt32(datar, 4)
        Dim result = New Byte(7) {}
        For i = 0 To 30
            Dim keyIndex = If(i < 24, i Mod 8, 7 - i Mod 8)
            Dim round = a1 Xor F(a0, _roundkeys(keyIndex))
            a1 = a0
            a0 = round
        Next
        a1 = a1 Xor F(a0, _roundkeys(0))
        Array.Copy(BitConverter.GetBytes(a0), 0, result, 0, 4)
        Array.Copy(BitConverter.GetBytes(a1), 0, result, 4, 4)
        '            Array.Reverse(result);
        Return result
    End Function

    Private Function Decrypt(ByVal data As Byte()) As Byte()
        Dim datar = New Byte(data.Length - 1) {}
        Array.Copy(data, datar, data.Length)
        '            Array.Reverse(datar);
        Dim a0 = BitConverter.ToUInt32(datar, 0)
        Dim a1 = BitConverter.ToUInt32(datar, 4)
        Dim result = New Byte(7) {}
        For i = 0 To 30
            Dim keyIndex = If(i < 8, i Mod 8, 7 - i Mod 8)
            Dim round = a1 Xor F(a0, _roundkeys(keyIndex))
            a1 = a0
            a0 = round
        Next
        a1 = a1 Xor F(a0, _roundkeys(0))
        Array.Copy(BitConverter.GetBytes(a0), 0, result, 0, 4)
        Array.Copy(BitConverter.GetBytes(a1), 0, result, 4, 4)
        '            Array.Reverse(result);
        Return result
    End Function

    Public Function CBCEncrypt(ByVal input As Byte(), ByVal ivseed As Integer) As Result
        Dim rand As Random = New Random(ivseed)
        Dim iv = New Byte(7) {}
        rand.NextBytes(iv)
        Return CBCEncrypt(input, iv)
    End Function

    Public Function CBCEncrypt(ByVal input As Byte(), ByVal iv As Byte()) As Result
        If iv.Length <> 8 Then
            Throw New CryptographicException("Initialization vector length is {iv.Length * 8} bit.")
        End If
        Dim inputBytes As List(Of Byte) = New List(Of Byte)(input)
        If inputBytes.Count Mod 8 <> 0 Then
            inputBytes.AddRange(New Byte(8 - inputBytes.Count Mod 8 - 1) {})
        End If
        Dim temp As Byte() = inputBytes.Take(8).ToArray()
        Dim i As Integer = 0

        While i < 8
            temp(i) = temp(i) Xor iv(i)
            Threading.Interlocked.Increment(i)
        End While
        temp = Encrypt(temp)
        Dim res As List(Of Byte) = New List(Of Byte)(temp)
        i = 1

        While i < inputBytes.Count / 8
            Dim cur As Byte() = inputBytes.Skip(i * 8).Take(8).ToArray()
            Dim j = 0

            While j < 8
                temp(j) = cur(j) Xor temp(j)
                Threading.Interlocked.Increment(j)
            End While
            temp = Encrypt(temp)
            res.AddRange(temp)
            Threading.Interlocked.Increment(i)
        End While
        Return New Result(res.ToArray(), input.Length)
    End Function

    Public Function CBCDecrypt(ByVal input As Result, ByVal ivseed As Integer) As Byte()
        Dim rand As Random = New Random(ivseed)
        Dim iv = New Byte(7) {}
        rand.NextBytes(iv)
        Return CBCDecrypt(input, iv)
    End Function

    Public Function CBCDecrypt(ByVal input As Result, ByVal iv As Byte()) As Byte()
        If iv.Length <> 8 Then
            Throw New CryptographicException("Initialization vector length is {iv.Length * 8} bit.")
        End If
        Dim inputBytes As List(Of Byte) = New List(Of Byte)(input.Encrypted)
        Dim temp As Byte() = inputBytes.Take(8).ToArray()
        Dim decr = Decrypt(temp)
        Dim i As Integer = 0

        While i < 8
            decr(i) = decr(i) Xor iv(i)
            Threading.Interlocked.Increment(i)
        End While
        Dim res As List(Of Byte) = New List(Of Byte)(decr)
        i = 1

        While i < inputBytes.Count / 8
            Dim cur As Byte() = inputBytes.Skip(i * 8).Take(8).ToArray()
            decr = Decrypt(cur)
            Dim j = 0

            While j < 8
                decr(j) = decr(j) Xor temp(j)
                Threading.Interlocked.Increment(j)
            End While
            temp = cur
            res.AddRange(decr)
            Threading.Interlocked.Increment(i)
        End While
        res.RemoveRange(input.Length, res.Count - input.Length)
        Return res.ToArray()
    End Function

    Public Function CFBEncrypt(ByVal input As Byte(), ByVal ivseed As Integer) As Result
        Dim rand As Random = New Random(ivseed)
        Dim iv = New Byte(7) {}
        rand.NextBytes(iv)
        Return CFBEncrypt(input, iv)
    End Function

    Public Function CFBEncrypt(ByVal input As Byte(), ByVal iv As Byte()) As Result
        If iv.Length <> 8 Then
            Throw New CryptographicException("Initialization vector length is {iv.Length * 8} bit.")
        End If
        Dim inputBytes As List(Of Byte) = New List(Of Byte)(input)
        If inputBytes.Count Mod 8 <> 0 Then
            inputBytes.AddRange(New Byte(8 - inputBytes.Count Mod 8 - 1) {})
        End If
        Dim res As List(Of Byte) = New List(Of Byte)()
        Dim temp = iv
        Dim i = 0

        While i < inputBytes.Count / 8
            Dim cur = Encrypt(temp)
            Dim ot As Byte() = inputBytes.Skip(i * 8).Take(8).ToArray()
            Dim j = 0

            While j < 8
                cur(j) = cur(j) Xor ot(j)
                Threading.Interlocked.Increment(j)
            End While
            res.AddRange(cur)
            temp = cur
            Threading.Interlocked.Increment(i)
        End While
        Return New Result(res.ToArray(), input.Length)
    End Function

    Public Function CFBDecrypt(ByVal input As Result, ByVal ivseed As Integer) As Byte()
        Dim rand As Random = New Random(ivseed)
        Dim iv = New Byte(7) {}
        rand.NextBytes(iv)
        Return CFBDecrypt(input, iv)
    End Function

    Public Function CFBDecrypt(ByVal input As Result, ByVal iv As Byte()) As Byte()
        If iv.Length <> 8 Then
            Throw New CryptographicException("Initialization vector length is {iv.Length * 8} bit.")
        End If
        Dim inputBytes As List(Of Byte) = New List(Of Byte)(input.Encrypted)
        Dim res As List(Of Byte) = New List(Of Byte)()
        Dim temp = iv
        Dim i = 0

        While i < inputBytes.Count / 8
            Dim cur = Encrypt(temp)
            Dim ct As Byte() = inputBytes.Skip(i * 8).Take(8).ToArray()
            Dim j = 0

            While j < 8
                cur(j) = cur(j) Xor ct(j)
                Threading.Interlocked.Increment(j)
            End While
            res.AddRange(cur)
            temp = ct
            Threading.Interlocked.Increment(i)
        End While
        res.RemoveRange(input.Length, res.Count - input.Length)
        Return res.ToArray()
    End Function

    Public Function OFBEncrypt(ByVal input As Byte(), ByVal ivseed As Integer) As Result
        Dim rand As Random = New Random(ivseed)
        Dim iv = New Byte(7) {}
        rand.NextBytes(iv)
        Return OFBEncrypt(input, iv)
    End Function

    Public Function OFBEncrypt(ByVal input As Byte(), ByVal iv As Byte()) As Result
        If iv.Length <> 8 Then
            Throw New CryptographicException("Initialization vector length is {iv.Length * 8} bit.")
        End If
        Dim inputBytes As List(Of Byte) = New List(Of Byte)(input)
        If inputBytes.Count Mod 8 <> 0 Then
            inputBytes.AddRange(New Byte(8 - inputBytes.Count Mod 8 - 1) {})
        End If
        Dim res As List(Of Byte) = New List(Of Byte)()
        Dim temp = iv
        Dim i = 0

        While i < inputBytes.Count / 8
            Dim cur = Encrypt(temp)
            temp = CType(cur.Clone(), Byte())
            Dim ot As Byte() = inputBytes.Skip(i * 8).Take(8).ToArray()
            Dim j = 0

            While j < 8
                cur(j) = cur(j) Xor ot(j)
                Threading.Interlocked.Increment(j)
            End While
            res.AddRange(cur)
            Threading.Interlocked.Increment(i)
        End While
        Return New Result(res.ToArray(), input.Length)
    End Function

    Public Function OFBDecrypt(ByVal input As Result, ByVal ivseed As Integer) As Byte()
        Dim rand As Random = New Random(ivseed)
        Dim iv = New Byte(7) {}
        rand.NextBytes(iv)
        Return OFBDecrypt(input, iv)
    End Function

    Public Function OFBDecrypt(ByVal input As Result, ByVal iv As Byte()) As Byte()
        If iv.Length <> 8 Then
            Throw New CryptographicException("Initialization vector length is {iv.Length * 8} bit.")
        End If
        Dim inputBytes As List(Of Byte) = New List(Of Byte)(input.Encrypted)
        Dim res As List(Of Byte) = New List(Of Byte)()
        Dim temp = iv
        Dim i = 0

        While i < inputBytes.Count / 8
            Dim cur = Encrypt(temp)
            temp = CType(cur.Clone(), Byte())
            Dim ct As Byte() = inputBytes.Skip(i * 8).Take(8).ToArray()
            Dim j = 0

            While j < 8
                cur(j) = cur(j) Xor ct(j)
                Threading.Interlocked.Increment(j)
            End While
            res.AddRange(cur)
            Threading.Interlocked.Increment(i)
        End While
        res.RemoveRange(input.Length, res.Count - input.Length)
        Return res.ToArray()
    End Function
End Class

Friend Class Result
    Public Sub New(ByVal encrypted As Byte(), ByVal length As Integer)
        encrypted = encrypted
        length = length
    End Sub

    Public Property Encrypted As Byte()
    Public Property Length As Integer

End Class

