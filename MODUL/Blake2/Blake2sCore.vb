Imports System

Namespace Blake2sCSharp
    Partial Public NotInheritable Class Blake2sCore
        Private _isInitialized As Boolean = False

        Private _bufferFilled As Integer
        Private _buf As Byte() = New Byte(63) {} '

        Private _m As UInteger() = New UInteger(15) {}
        Private _h As UInteger() = New UInteger(7) {} ' stays the same
        Private _counter0 As UInteger
        Private _counter1 As UInteger
        Private _finalizationFlag0 As UInteger
        Private _finalizationFlag1 As UInteger

        'private const int NumberOfRounds = 10; //
        Public Const BlockSizeInBytes As Integer = 64 '

        Const IV0 As UInteger = &H6A09E667UI '
        Const IV1 As UInteger = &HBB67AE85UI '
        Const IV2 As UInteger = &H3C6EF372UI '
        Const IV3 As UInteger = &HA54FF53AUI '
        Const IV4 As UInteger = &H510E527FUI '
        Const IV5 As UInteger = &H9B05688CUI '
        Const IV6 As UInteger = &H1F83D9ABUI '
        Const IV7 As UInteger = &H5BE0CD19UI '

        ' private static readonly int[] Sigma = new int[NumberOfRounds * 16] {
        ' 			0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
        ' 			14, 10, 4, 8, 9, 15, 13, 6, 1, 12, 0, 2, 11, 7, 5, 3,
        ' 			11, 8, 12, 0, 5, 2, 15, 13, 10, 14, 3, 6, 7, 1, 9, 4,
        ' 			7, 9, 3, 1, 13, 12, 11, 14, 2, 6, 5, 10, 4, 0, 15, 8,
        ' 			9, 0, 5, 7, 2, 4, 10, 15, 14, 1, 11, 12, 6, 8, 3, 13,
        ' 			2, 12, 6, 10, 0, 11, 8, 3, 4, 13, 7, 5, 15, 14, 1, 9,
        ' 			12, 5, 1, 15, 14, 13, 4, 10, 0, 7, 6, 3, 9, 2, 8, 11,
        ' 			13, 11, 7, 14, 12, 1, 3, 9, 5, 0, 15, 4, 8, 6, 2, 10,
        ' 			6, 15, 14, 9, 11, 3, 0, 8, 12, 2, 13, 7, 1, 4, 10, 5,
        ' 			10, 2, 8, 4, 7, 6, 1, 5, 15, 11, 9, 14, 3, 12, 13, 0,
        ' 		}; //

        Friend Shared Function BytesToUInt32(ByVal buf As Byte(), ByVal offset As Integer) As UInteger '
            Return (CUInt(buf(offset + 3)) << 24) + (CUInt(buf(offset + 2)) << 16) + (CUInt(buf(offset + 1)) << 8) + buf(offset)  '
            '
            '
            '
        End Function

        Private Shared Sub UInt32ToBytes(ByVal value As UInteger, ByVal buf As Byte(), ByVal offset As Integer) '
            buf(offset + 3) = CByte(value >> 24) '
            buf(offset + 2) = CByte(value >> 16) '
            buf(offset + 1) = CByte(value >> 8) '
            buf(offset) = CByte(value)
        End Sub

        Partial Private Sub Compress(ByVal block As Byte(), ByVal start As Integer)
        End Sub

        Public Sub Initialize(ByVal salt As UInteger()) '
            If salt Is Nothing Then Throw New ArgumentNullException("salt")
            If salt.Length <> 8 Then Throw New ArgumentException("salt length must be 8 words")
            _isInitialized = True

            _h(0) = IV0
            _h(1) = IV1
            _h(2) = IV2
            _h(3) = IV3
            _h(4) = IV4
            _h(5) = IV5
            _h(6) = IV6
            _h(7) = IV7

            _counter0 = 0
            _counter1 = 0
            _finalizationFlag0 = 0
            _finalizationFlag1 = 0

            _bufferFilled = 0

            Array.Clear(_buf, 0, _buf.Length)

            For i = 0 To _h.Length - 1
                _h(i) = _h(i) Xor salt(i)
            Next
        End Sub

        Public Sub HashCore(ByVal array As Byte(), ByVal start As Integer, ByVal count As Integer)
            If Not _isInitialized Then Throw New InvalidOperationException("Not initialized")
            If array Is Nothing Then Throw New ArgumentNullException("array")
            If start < 0 Then Throw New ArgumentOutOfRangeException("start")
            If count < 0 Then Throw New ArgumentOutOfRangeException("count")
            If start + CLng(count) > array.Length Then Throw New ArgumentOutOfRangeException("start+count")
            Dim offset = start
            Dim bufferRemaining = BlockSizeInBytes - _bufferFilled

            If _bufferFilled > 0 AndAlso count > bufferRemaining Then
                System.Array.Copy(array, offset, _buf, _bufferFilled, bufferRemaining)
                _counter0 += BlockSizeInBytes
                If _counter0 = 0 Then _counter1 += 1
                Compress(_buf, 0)
                offset += bufferRemaining
                count -= bufferRemaining
                _bufferFilled = 0
            End If

            While count > BlockSizeInBytes
                _counter0 += BlockSizeInBytes
                If _counter0 = 0 Then _counter1 += 1
                Compress(array, offset)
                offset += BlockSizeInBytes
                count -= BlockSizeInBytes
            End While

            If count > 0 Then
                System.Array.Copy(array, offset, _buf, _bufferFilled, count)
                _bufferFilled += count
            End If
        End Sub

        Public Function HashFinal() As Byte()
            Return HashFinal(False)
        End Function

        Public Function HashFinal(ByVal isEndOfLayer As Boolean) As Byte()
            If Not _isInitialized Then Throw New InvalidOperationException("Not initialized")
            _isInitialized = False

            'Last compression
            _counter0 += CUInt(_bufferFilled)
            _finalizationFlag0 = UInteger.MaxValue '
            'if (isEndOfLayer) // tree mode
            '    _finalizationFlag1 = UInt32.MaxValue; //
            For i = _bufferFilled To _buf.Length - 1
                _buf(i) = 0
            Next
            Compress(_buf, 0)

            'Output
            Dim hash = New Byte(31) {} '
            For i = 0 To 7 '
                UInt32ToBytes(_h(i), hash, i * 4)
            Next '
            Return hash
        End Function
    End Class
End Namespace