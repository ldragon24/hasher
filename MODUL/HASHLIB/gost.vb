Imports System
Imports System.IO
Imports System.Linq
Imports System.Collections

Friend Class GOST2
    ' Matrix A for MixColumns (L) function
    Private A As ULong() = {&H8E20FAA72BA0B470UL, &H47107DDD9B505A38, &HAD08B0E0C3282D1CUL, &HD8045870EF14980EUL, &H6C022C38F90A4C07, &H3601161CF205268D, &H1B8E0B0E798C13C8, &H83478B07B2468764UL, &HA011D380818E8F40UL, &H5086E740CE47C920, &H2843FD2067ADEA10, &H14AFF010BDD87508, &HAD97808D06CB404, &H5E23C0468365A02, &H8C711E02341B2D01UL, &H46B60F011A83988E, &H90DAB52A387AE76FUL, &H486DD4151C3DFDB9, &H24B86A840E90F0D2, &H125C354207487869, &H92E94218D243CBA, &H8A174A9EC8121E5DUL, &H4585254F64090FA0, &HACCC9CA9328A8950UL, &H9D4DF05D5F661451UL, &HC0A878A0A1330AA6UL, &H60543C50DE970553, &H302A1E286FC58CA7, &H18150F14B9EC46DD, &HC84890AD27623E0, &H642CA05693B9F70, &H321658CBA93C138, &H86275DF09CE8AAA8UL, &H439DA0784E745554, &HAFC0503C273AA42AUL, &HD960281E9D1D5215UL, &HE230140FC0802984UL, &H71180A8960409A42, &HB60C05CA30204D21UL, &H5B068C651810A89E, &H456C34887A3805B9, &HAC361A443D1C8CD2UL, &H561B0D22900E4669, &H2B838811480723BA, &H9BCF4486248D9F5DUL, &HC3E9224312C8C1A0UL, &HEFFA11AF0964EE50UL, &HF97D86D98A327728UL, &HE4FA2054A80B329CUL, &H727D102A548B194E, &H39B008152ACB8227, &H9258048415EB419DUL, &H492C024284FBAEC0, &HAA16012142F35760UL, &H550B8E9E21F7A530, &HA48B474F9EF5DC18UL, &H70A6A56E2440598E, &H3853DC371220A247, &H1CA76E95091051AD, &HEDD37C48A08A6D8, &H7E095624504536C, &H8D70C431AC02A736UL, &HC83862965601DD1BUL, &H641C314B2B8EE083}

    ' Substitution for SubBytes function
    Private Sbox As Byte() = {&HFC, &HEE, &HDD, &H11, &HCF, &H6E, &H31, &H16, &HFB, &HC4, &HFA, &HDA, &H23, &HC5, &H4, &H4D, &HE9, &H77, &HF0, &HDB, &H93, &H2E, &H99, &HBA, &H17, &H36, &HF1, &HBB, &H14, &HCD, &H5F, &HC1, &HF9, &H18, &H65, &H5A, &HE2, &H5C, &HEF, &H21, &H81, &H1C, &H3C, &H42, &H8B, &H1, &H8E, &H4F, &H5, &H84, &H2, &HAE, &HE3, &H6A, &H8F, &HA0, &H6, &HB, &HED, &H98, &H7F, &HD4, &HD3, &H1F, &HEB, &H34, &H2C, &H51, &HEA, &HC8, &H48, &HAB, &HF2, &H2A, &H68, &HA2, &HFD, &H3A, &HCE, &HCC, &HB5, &H70, &HE, &H56, &H8, &HC, &H76, &H12, &HBF, &H72, &H13, &H47, &H9C, &HB7, &H5D, &H87, &H15, &HA1, &H96, &H29, &H10, &H7B, &H9A, &HC7, &HF3, &H91, &H78, &H6F, &H9D, &H9E, &HB2, &HB1, &H32, &H75, &H19, &H3D, &HFF, &H35, &H8A, &H7E, &H6D, &H54, &HC6, &H80, &HC3, &HBD, &HD, &H57, &HDF, &HF5, &H24, &HA9, &H3E, &HA8, &H43, &HC9, &HD7, &H79, &HD6, &HF6, &H7C, &H22, &HB9, &H3, &HE0, &HF, &HEC, &HDE, &H7A, &H94, &HB0, &HBC, &HDC, &HE8, &H28, &H50, &H4E, &H33, &HA, &H4A, &HA7, &H97, &H60, &H73, &H1E, &H0, &H62, &H44, &H1A, &HB8, &H38, &H82, &H64, &H9F, &H26, &H41, &HAD, &H45, &H46, &H92, &H27, &H5E, &H55, &H2F, &H8C, &HA3, &HA5, &H7D, &H69, &HD5, &H95, &H3B, &H7, &H58, &HB3, &H40, &H86, &HAC, &H1D, &HF7, &H30, &H37, &H6B, &HE4, &H88, &HD9, &HE7, &H89, &HE1, &H1B, &H83, &H49, &H4C, &H3F, &HF8, &HFE, &H8D, &H53, &HAA, &H90, &HCA, &HD8, &H85, &H61, &H20, &H71, &H67, &HA4, &H2D, &H2B, &H9, &H5B, &HCB, &H9B, &H25, &HD0, &HBE, &HE5, &H6C, &H52, &H59, &HA6, &H74, &HD2, &HE6, &HF4, &HB4, &HC0, &HD1, &H66, &HAF, &HC2, &H39, &H4B, &H63, &HB6}

    ' Substitution for Transposition (P) function
    Private Tau As Byte() = {0, 8, 16, 24, 32, 40, 48, 56, 1, 9, 17, 25, 33, 41, 49, 57, 2, 10, 18, 26, 34, 42, 50, 58, 3, 11, 19, 27, 35, 43, 51, 59, 4, 12, 20, 28, 36, 44, 52, 60, 5, 13, 21, 29, 37, 45, 53, 61, 6, 14, 22, 30, 38, 46, 54, 62, 7, 15, 23, 31, 39, 47, 55, 63}

    ' Constant values for KeySchedule function
    Private C As Byte()() = {New Byte(63) {&HB1, &H8, &H5B, &HDA, &H1E, &HCA, &HDA, &HE9, &HEB, &HCB, &H2F, &H81, &HC0, &H65, &H7C, &H1F, &H2F, &H6A, &H76, &H43, &H2E, &H45, &HD0, &H16, &H71, &H4E, &HB8, &H8D, &H75, &H85, &HC4, &HFC, &H4B, &H7C, &HE0, &H91, &H92, &H67, &H69, &H1, &HA2, &H42, &H2A, &H8, &HA4, &H60, &HD3, &H15, &H5, &H76, &H74, &H36, &HCC, &H74, &H4D, &H23, &HDD, &H80, &H65, &H59, &HF2, &HA6, &H45, &H7}, New Byte(63) {&H6F, &HA3, &HB5, &H8A, &HA9, &H9D, &H2F, &H1A, &H4F, &HE3, &H9D, &H46, &HF, &H70, &HB5, &HD7, &HF3, &HFE, &HEA, &H72, &HA, &H23, &H2B, &H98, &H61, &HD5, &H5E, &HF, &H16, &HB5, &H1, &H31, &H9A, &HB5, &H17, &H6B, &H12, &HD6, &H99, &H58, &H5C, &HB5, &H61, &HC2, &HDB, &HA, &HA7, &HCA, &H55, &HDD, &HA2, &H1B, &HD7, &HCB, &HCD, &H56, &HE6, &H79, &H4, &H70, &H21, &HB1, &H9B, &HB7}, New Byte(63) {&HF5, &H74, &HDC, &HAC, &H2B, &HCE, &H2F, &HC7, &HA, &H39, &HFC, &H28, &H6A, &H3D, &H84, &H35, &H6, &HF1, &H5E, &H5F, &H52, &H9C, &H1F, &H8B, &HF2, &HEA, &H75, &H14, &HB1, &H29, &H7B, &H7B, &HD3, &HE2, &HF, &HE4, &H90, &H35, &H9E, &HB1, &HC1, &HC9, &H3A, &H37, &H60, &H62, &HDB, &H9, &HC2, &HB6, &HF4, &H43, &H86, &H7A, &HDB, &H31, &H99, &H1E, &H96, &HF5, &HA, &HBA, &HA, &HB2}, New Byte(63) {&HEF, &H1F, &HDF, &HB3, &HE8, &H15, &H66, &HD2, &HF9, &H48, &HE1, &HA0, &H5D, &H71, &HE4, &HDD, &H48, &H8E, &H85, &H7E, &H33, &H5C, &H3C, &H7D, &H9D, &H72, &H1C, &HAD, &H68, &H5E, &H35, &H3F, &HA9, &HD7, &H2C, &H82, &HED, &H3, &HD6, &H75, &HD8, &HB7, &H13, &H33, &H93, &H52, &H3, &HBE, &H34, &H53, &HEA, &HA1, &H93, &HE8, &H37, &HF1, &H22, &HC, &HBE, &HBC, &H84, &HE3, &HD1, &H2E}, New Byte(63) {&H4B, &HEA, &H6B, &HAC, &HAD, &H47, &H47, &H99, &H9A, &H3F, &H41, &HC, &H6C, &HA9, &H23, &H63, &H7F, &H15, &H1C, &H1F, &H16, &H86, &H10, &H4A, &H35, &H9E, &H35, &HD7, &H80, &HF, &HFF, &HBD, &HBF, &HCD, &H17, &H47, &H25, &H3A, &HF5, &HA3, &HDF, &HFF, &H0, &HB7, &H23, &H27, &H1A, &H16, &H7A, &H56, &HA2, &H7E, &HA9, &HEA, &H63, &HF5, &H60, &H17, &H58, &HFD, &H7C, &H6C, &HFE, &H57}, New Byte(63) {&HAE, &H4F, &HAE, &HAE, &H1D, &H3A, &HD3, &HD9, &H6F, &HA4, &HC3, &H3B, &H7A, &H30, &H39, &HC0, &H2D, &H66, &HC4, &HF9, &H51, &H42, &HA4, &H6C, &H18, &H7F, &H9A, &HB4, &H9A, &HF0, &H8E, &HC6, &HCF, &HFA, &HA6, &HB7, &H1C, &H9A, &HB7, &HB4, &HA, &HF2, &H1F, &H66, &HC2, &HBE, &HC6, &HB6, &HBF, &H71, &HC5, &H72, &H36, &H90, &H4F, &H35, &HFA, &H68, &H40, &H7A, &H46, &H64, &H7D, &H6E}, New Byte(63) {&HF4, &HC7, &HE, &H16, &HEE, &HAA, &HC5, &HEC, &H51, &HAC, &H86, &HFE, &HBF, &H24, &H9, &H54, &H39, &H9E, &HC6, &HC7, &HE6, &HBF, &H87, &HC9, &HD3, &H47, &H3E, &H33, &H19, &H7A, &H93, &HC9, &H9, &H92, &HAB, &HC5, &H2D, &H82, &H2C, &H37, &H6, &H47, &H69, &H83, &H28, &H4A, &H5, &H4, &H35, &H17, &H45, &H4C, &HA2, &H3C, &H4A, &HF3, &H88, &H86, &H56, &H4D, &H3A, &H14, &HD4, &H93}, New Byte(63) {&H9B, &H1F, &H5B, &H42, &H4D, &H93, &HC9, &HA7, &H3, &HE7, &HAA, &H2, &HC, &H6E, &H41, &H41, &H4E, &HB7, &HF8, &H71, &H9C, &H36, &HDE, &H1E, &H89, &HB4, &H44, &H3B, &H4D, &HDB, &HC4, &H9A, &HF4, &H89, &H2B, &HCB, &H92, &H9B, &H6, &H90, &H69, &HD1, &H8D, &H2B, &HD1, &HA5, &HC4, &H2F, &H36, &HAC, &HC2, &H35, &H59, &H51, &HA8, &HD9, &HA4, &H7F, &HD, &HD4, &HBF, &H2, &HE7, &H1E}, New Byte(63) {&H37, &H8F, &H5A, &H54, &H16, &H31, &H22, &H9B, &H94, &H4C, &H9A, &HD8, &HEC, &H16, &H5F, &HDE, &H3A, &H7D, &H3A, &H1B, &H25, &H89, &H42, &H24, &H3C, &HD9, &H55, &HB7, &HE0, &HD, &H9, &H84, &H80, &HA, &H44, &HB, &HDB, &HB2, &HCE, &HB1, &H7B, &H2B, &H8A, &H9A, &HA6, &H7, &H9C, &H54, &HE, &H38, &HDC, &H92, &HCB, &H1F, &H2A, &H60, &H72, &H61, &H44, &H51, &H83, &H23, &H5A, &HDB}, New Byte(63) {&HAB, &HBE, &HDE, &HA6, &H80, &H5, &H6F, &H52, &H38, &H2A, &HE5, &H48, &HB2, &HE4, &HF3, &HF3, &H89, &H41, &HE7, &H1C, &HFF, &H8A, &H78, &HDB, &H1F, &HFF, &HE1, &H8A, &H1B, &H33, &H61, &H3, &H9F, &HE7, &H67, &H2, &HAF, &H69, &H33, &H4B, &H7A, &H1E, &H6C, &H30, &H3B, &H76, &H52, &HF4, &H36, &H98, &HFA, &HD1, &H15, &H3B, &HB6, &HC3, &H74, &HB4, &HC7, &HFB, &H98, &H45, &H9C, &HED}, New Byte(63) {&H7B, &HCD, &H9E, &HD0, &HEF, &HC8, &H89, &HFB, &H30, &H2, &HC6, &HCD, &H63, &H5A, &HFE, &H94, &HD8, &HFA, &H6B, &HBB, &HEB, &HAB, &H7, &H61, &H20, &H1, &H80, &H21, &H14, &H84, &H66, &H79, &H8A, &H1D, &H71, &HEF, &HEA, &H48, &HB9, &HCA, &HEF, &HBA, &HCD, &H1D, &H7D, &H47, &H6E, &H98, &HDE, &HA2, &H59, &H4A, &HC0, &H6F, &HD8, &H5D, &H6B, &HCA, &HA4, &HCD, &H81, &HF3, &H2D, &H1B}, New Byte(63) {&H37, &H8E, &HE7, &H67, &HF1, &H16, &H31, &HBA, &HD2, &H13, &H80, &HB0, &H4, &H49, &HB1, &H7A, &HCD, &HA4, &H3C, &H32, &HBC, &HDF, &H1D, &H77, &HF8, &H20, &H12, &HD4, &H30, &H21, &H9F, &H9B, &H5D, &H80, &HEF, &H9D, &H18, &H91, &HCC, &H86, &HE7, &H1D, &HA4, &HAA, &H88, &HE1, &H28, &H52, &HFA, &HF4, &H17, &HD5, &HD9, &HB2, &H1B, &H99, &H48, &HBC, &H92, &H4A, &HF1, &H1B, &HD7, &H20}}

    Private iv As Byte() = New Byte(63) {}

    Private N As Byte() = New Byte(63) {}

    Private Sigma As Byte() = New Byte(63) {}

    Public outLen As Integer = 0

    Public Sub New(ByVal outputLenght As Integer)
        If outputLenght = 512 Then
            For i = 0 To 63
                N(i) = &H0
                Sigma(i) = &H0
                iv(i) = &H0
            Next
            outLen = 512
        ElseIf outputLenght = 256 Then
            For i = 0 To 63
                N(i) = &H0
                Sigma(i) = &H0
                iv(i) = &H1
            Next
            outLen = 256
        End If
    End Sub

    Private Function AddModulo512(ByVal a As Byte(), ByVal b As Byte()) As Byte()
        Dim temp = New Byte(63) {}
        Dim i = 0, t = 0
        Dim tempA = New Byte(63) {}
        Dim tempB = New Byte(63) {}
        Array.Copy(a, 0, tempA, 64 - a.Length, a.Length)
        Array.Copy(b, 0, tempB, 64 - b.Length, b.Length)
        For i = 63 To 0 Step -1
            t = tempA(i) + tempB(i) + (t >> 8)
            temp(i) = CByte(t And &HFF)
        Next
        Return temp
    End Function

    Private Function AddXor512(ByVal a As Byte(), ByVal b As Byte()) As Byte()
        Dim c = New Byte(63) {}
        For i = 0 To 63
            c(i) = a(i) Xor b(i)
        Next
        Return c
    End Function

    Private Function S(ByVal state As Byte()) As Byte()
        Dim result = New Byte(63) {}
        For i = 0 To 63
            result(i) = Sbox(state(i))
        Next
        Return result
    End Function

    Private Function P(ByVal state As Byte()) As Byte()
        Dim result = New Byte(63) {}
        For i = 0 To 63
            result(i) = state(Tau(i))
        Next
        Return result
    End Function

    Private Function L(ByVal state As Byte()) As Byte()
        Dim result = New Byte(63) {}
        For i = 0 To 7
            Dim t As ULong = 0
            Dim tempArray = New Byte(7) {}
            Array.Copy(state, i * 8, tempArray, 0, 8)
            tempArray = tempArray.Reverse().ToArray()
            Dim tempBits1 As BitArray = New BitArray(tempArray)
            Dim tempBits = New Boolean(63) {}
            tempBits1.CopyTo(tempBits, 0)
            tempBits = tempBits.Reverse().ToArray()
            For j = 0 To 63
                If tempBits(j) <> False Then t = t Xor A(j)
            Next
            Dim ResPart As Byte() = BitConverter.GetBytes(t).Reverse().ToArray()
            Array.Copy(ResPart, 0, result, i * 8, 8)
        Next
        Return result
    End Function

    Private Function KeySchedule(ByVal K As Byte(), ByVal i As Integer) As Byte()
        K = AddXor512(K, C(i))
        K = S(K)
        K = P(K)
        K = L(K)
        Return K
    End Function

    Private Function E(ByVal K As Byte(), ByVal m As Byte()) As Byte()
        Dim state = AddXor512(K, m)
        For i = 0 To 11
            state = S(state)
            state = P(state)
            state = L(state)
            K = KeySchedule(K, i)
            state = AddXor512(state, K)
        Next
        Return state
    End Function

    Private Function G_n(ByVal N As Byte(), ByVal h As Byte(), ByVal m As Byte()) As Byte()
        Dim K = AddXor512(h, N)
        K = S(K)
        K = P(K)
        K = L(K)
        Dim t = E(K, m)
        t = AddXor512(t, h)
        Dim newh = AddXor512(t, m)
        Return newh
    End Function

    Public Function GetHashGOST(ByVal message As Byte()) As Byte()
        Dim paddedMes = New Byte(63) {}
        Dim len = message.Length * 8
        Dim h = New Byte(63) {}
        Array.Copy(iv, h, 64)
        Dim N_0 As Byte() = {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
        If outLen = 512 Then
            For i = 0 To 63
                N(i) = &H0
                Sigma(i) = &H0
                iv(i) = &H0
            Next
        ElseIf outLen = 256 Then
            For i = 0 To 63
                N(i) = &H0
                Sigma(i) = &H0
                iv(i) = &H1
            Next
        End If
        Dim N_512 = BitConverter.GetBytes(512)
        Dim inc = 0
        While len >= 512
            inc += 1
            Dim tempMes = New Byte(63) {}
            Array.Copy(message, message.Length - inc * 64, tempMes, 0, 64)
            h = G_n(N, h, tempMes)
            N = AddModulo512(N, N_512.Reverse().ToArray())
            Sigma = AddModulo512(Sigma, tempMes)
            len -= 512
        End While
        Dim message1 = New Byte(message.Length - inc * 64 - 1) {}
        Array.Copy(message, 0, message1, 0, message.Length - inc * 64)
        If message1.Length < 64 Then
            For i = 0 To 64 - message1.Length - 1 - 1
                paddedMes(i) = 0
            Next
            paddedMes(64 - message1.Length - 1) = &H1
            Array.Copy(message1, 0, paddedMes, 64 - message1.Length, message1.Length)
        End If
        h = G_n(N, h, paddedMes)
        Dim MesLen = BitConverter.GetBytes(message1.Length * 8)
        N = AddModulo512(N, MesLen.Reverse().ToArray())
        Sigma = AddModulo512(Sigma, paddedMes)
        h = G_n(N_0, h, N)
        h = G_n(N_0, h, Sigma)
        If outLen = 512 Then
            Return h
        Else
            Dim h256 = New Byte(31) {}
            Array.Copy(h, 0, h256, 0, 32)
            Return h256
        End If
    End Function
End Class

