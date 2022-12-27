Imports System.IO

Public Class AdlerChecksum
    ' parameters
#Region ""
    ''' <summary>
    ''' AdlerBase is Adler-32 checksum algorithm parameter.
    ''' </summary>
    Public Const AdlerBase As UInteger = &HFFF1
    ''' <summary>
    ''' AdlerStart is Adler-32 checksum algorithm parameter.
    ''' </summary>
    Public Const AdlerStart As UInteger = &H1
    ''' <summary>
    ''' AdlerBuff is Adler-32 checksum algorithm parameter.
    ''' </summary>
    Public Const AdlerBuff As UInteger = &H400
    ''' Adler-32 checksum value
    Private m_unChecksumValue As UInteger = 0
#End Region
    ''' <value>
    ''' ChecksumValue is property which enables the user
    ''' to get Adler-32 checksum value for the last calculation 
    ''' </value>
    Public ReadOnly Property ChecksumValue As UInteger
        Get
            Return m_unChecksumValue
        End Get
    End Property
    ''' <summary>
    ''' Calculate Adler-32 checksum for buffer
    ''' </summary>
    ''' <paramname="bytesBuff">Bites array for checksum calculation</param>
    ''' <paramname="unAdlerCheckSum">Checksum start value (default=1)</param>
    ''' <returns>Returns true if the checksum values is successflly calculated</returns>
    Public Function MakeForBuff(ByVal bytesBuff As Byte(), ByVal unAdlerCheckSum As UInteger) As Boolean
        If Equals(bytesBuff, Nothing) Then
            m_unChecksumValue = 0
            Return False
        End If
        Dim nSize = bytesBuff.GetLength(0)
        If nSize = 0 Then
            m_unChecksumValue = 0
            Return False
        End If
        Dim unSum1 As UInteger = unAdlerCheckSum And &HFFFF
        Dim unSum2 As UInteger = unAdlerCheckSum >> 16 And &HFFFF
        For i = 0 To nSize - 1
            unSum1 = (unSum1 + bytesBuff(i)) Mod AdlerBase
            unSum2 = (unSum1 + unSum2) Mod AdlerBase
        Next
        m_unChecksumValue = (unSum2 << 16) + unSum1
        Return True
    End Function
    ''' <summary>
    ''' Calculate Adler-32 checksum for buffer
    ''' </summary>
    ''' <paramname="bytesBuff">Bites array for checksum calculation</param>
    ''' <returns>Returns true if the checksum values is successflly calculated</returns>
    Public Function MakeForBuff(ByVal bytesBuff As Byte()) As Boolean
        Return MakeForBuff(bytesBuff, AdlerStart)
    End Function
    ''' <summary>
    ''' Calculate Adler-32 checksum for file
    ''' </summary>
    ''' <paramname="sPath">Path to file for checksum calculation</param>
    ''' <returns>Returns true if the checksum values is successflly calculated</returns>
    Public Function MakeForFile(ByVal sPath As String) As Boolean
        Try
            If Not File.Exists(sPath) Then
                m_unChecksumValue = 0
                Return False
            End If
            Dim fs As FileStream = New FileStream(sPath, FileMode.Open, FileAccess.Read)
            If Equals(fs, Nothing) Then
                m_unChecksumValue = 0
                Return False
            End If
            If fs.Length = 0 Then
                m_unChecksumValue = 0
                Return False
            End If
            m_unChecksumValue = AdlerStart
            Dim bytesBuff = New Byte(AdlerBuff - 1) {}
            For i As UInteger = 0 To fs.Length - 1
                Dim index = i Mod AdlerBuff
                bytesBuff(index) = CByte(fs.ReadByte())
                If index = AdlerBuff - 1 OrElse i = fs.Length - 1 Then
                    If Not MakeForBuff(bytesBuff, m_unChecksumValue) Then
                        m_unChecksumValue = 0
                        Return False
                    End If
                End If
            Next

        Catch
            m_unChecksumValue = 0
            Return False
        End Try
        Return True
    End Function
    ''' <summary>
    ''' Equals determines whether two files (buffers) 
    ''' have the same checksum value (identical).
    ''' </summary>
    ''' <paramname="obj">A AdlerChecksum object for comparison</param>
    ''' <returns>Returns true if the value of checksum is the same
    ''' as this instance; otherwise, false
    ''' </returns>
    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj Is Nothing Then Return False
        If [GetType]() IsNot obj.GetType() Then Return False
        Dim other = CType(obj, AdlerChecksum)
        Return ChecksumValue = other.ChecksumValue
    End Function
    ''' <summary>
    ''' operator== determines whether AdlerChecksum objects are equal.
    ''' </summary>
    ''' <paramname="objA">A AdlerChecksum object for comparison</param>
    ''' <paramname="objB">A AdlerChecksum object for comparison</param>
    ''' <returns>Returns true if the values of its operands are equal</returns>
    Public Shared Operator =(ByVal objA As AdlerChecksum, ByVal objB As AdlerChecksum) As Boolean
        If Equals(objA, Nothing) AndAlso Equals(objB, Nothing) Then Return True
        If Equals(objA, Nothing) OrElse Equals(objB, Nothing) Then Return False
        Return objA.Equals(objB)
    End Operator
    ''' <summary>
    ''' operator!= determines whether AdlerChecksum objects are not equal.
    ''' </summary>
    ''' <paramname="objA">A AdlerChecksum object for comparison</param>
    ''' <paramname="objB">A AdlerChecksum object for comparison</param>
    ''' <returns>Returns true if the values of its operands are not equal</returns>
    Public Shared Operator <>(ByVal objA As AdlerChecksum, ByVal objB As AdlerChecksum) As Boolean
        Return Not objA Is objB
    End Operator
    ''' <summary>
    ''' GetHashCode returns hash code for this instance.
    ''' </summary>
    ''' <returns>hash code of AdlerChecksum</returns>
    Public Overrides Function GetHashCode() As Integer
        Return ChecksumValue.GetHashCode()
    End Function
    ''' <summary>
    ''' ToString is a method for current AdlerChecksum object
    ''' representation in textual form.
    ''' </summary>
    ''' <returns>Returns current checksum or
    ''' or "Unknown" if checksum value is unavailable 
    ''' </returns>
    Public Overrides Function ToString() As String
        If ChecksumValue <> 0 Then Return ChecksumValue.ToString()
        Return "Unknown"
    End Function
End Class