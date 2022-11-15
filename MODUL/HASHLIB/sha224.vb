Imports System
Imports System.IO
Imports System.Text
Imports System.Runtime.InteropServices

Public Class SHA224
    Public Shared Function SHA224(ByVal message As String) As String
        Dim lH0 As UInteger = &H6A09E667
        Dim lH1 = &HBB67AE85UI
        Dim lH2 As UInteger = &H3C6EF372
        Dim lH3 = &HA54FF53AUI
        Dim lH4 As UInteger = &H510E527F
        Dim lH5 = &H9B05688CUI
        Dim h6 As UInteger = &H1F83D9AB
        Dim h7 As UInteger = &H5BE0CD19

        Dim k = New UInteger() {&H428A2F98, &H71374491, &HB5C0FBCFUI, &HE9B5DBA5UI, &H3956C25B, &H59F111F1, &H923F82A4UI, &HAB1C5ED5UI, &HD807AA98UI, &H12835B01, &H243185BE, &H550C7DC3, &H72BE5D74, &H80DEB1FEUI, &H9BDC06A7UI, &HC19BF174UI, &HE49B69C1UI, &HEFBE4786UI, &HFC19DC6, &H240CA1CC, &H2DE92C6F, &H4A7484AA, &H5CB0A9DC, &H76F988DA, &H983E5152UI, &HA831C66DUI, &HB00327C8UI, &HBF597FC7UI, &HC6E00BF3UI, &HD5A79147UI, &H6CA6351, &H14292967, &H27B70A85, &H2E1B2138, &H4D2C6DFC, &H53380D13, &H650A7354, &H766A0ABB, &H81C2C92EUI, &H92722C85UI, &HA2BFE8A1UI, &HA81A664BUI, &HC24B8B70UI, &HC76C51A3UI, &HD192E819UI, &HD6990624UI, &HF40E3585UI, &H106AA070, &H19A4C116, &H1E376C08, &H2748774C, &H34B0BCB5, &H391C0CB3, &H4ED8AA4A, &H5B9CCA4F, &H682E6FF3, &H748F82EE, &H78A5636F, &H84C87814UI, &H8CC70208UI, &H90BEFFFAUI, &HA4506CEBUI, &HBEF9A3F7UI, &HC67178F2UI}

        Dim s0, s1, ch, maj, temp1, temp2 As UInteger


        Using fs As FileStream = File.OpenRead(message)

            Dim padded_message = paddmessage(message)
            Dim chunks As String() = Nothing, length As Integer = Nothing
            chunked(padded_message, chunks, length)
            Dim splitted As UInteger() = Nothing
            For x = 0 To length - 1
                Dim w = New UInteger(63) {}
                splitmessage(chunks(x), splitted)

                For i = 0 To 15
                    w(i) = splitted(i)
                Next

                For i = 16 To 63
                    s0 = w(i - 15) >> 7 Or w(i - 15) << 25 Xor w(i - 15) >> 18 Or w(i - 15) << 14 Xor w(i - 15) >> 3
                    s1 = w(i - 2) >> 17 Or w(i - 2) << 15 Xor w(i - 2) >> 19 Or w(i - 2) << 13 Xor w(i - 2) >> 10
                    w(i) = w(i - 16) + s0 + w(i - 7) + s1
                Next

                Dim a = lH0
                Dim b = lH1
                Dim c = lH2
                Dim d = lH3
                Dim e = lH4
                Dim f = lH5
                Dim g = h6
                Dim h = h7

                For i = 0 To 63
                    s1 = e >> 6 Or e << 26 Xor e >> 11 Or e << 21 Xor e >> 25 Or e << 7
                    ch = e And f Xor Not e And g

                    temp1 = h + s1 + ch + k(i) + w(i)
                    s0 = a >> 2 Or a << 30 Xor a >> 13 Or a << 19 Xor a >> 22 Or a << 10
                    maj = a And b Xor a And c Xor b And c
                    temp2 = s0 + maj
                    h = g
                    g = f
                    f = e
                    e = d + temp1
                    d = c
                    c = b
                    b = a
                    a = temp1 + temp2
                Next
                lH0 += a
                lH1 += b
                lH2 += c
                lH3 += d
                lH4 += e
                lH5 += f
                h6 += g
                h7 += h
            Next
            Dim H0 = String.Format(lH0.ToString("X").PadLeft(8, "0"c))
            Dim H1 = String.Format(lH1.ToString("X").PadLeft(8, "0"c))
            Dim H2 = String.Format(lH2.ToString("X").PadLeft(8, "0"c))
            Dim H3 = String.Format(lH3.ToString("X").PadLeft(8, "0"c))
            Dim H4 = String.Format(lH4.ToString("X").PadLeft(8, "0"c))
            Dim H5 = String.Format(lH5.ToString("X").PadLeft(8, "0"c))
            Dim lH6 = String.Format(h6.ToString("X").PadLeft(8, "0"c))

            Return H0 & H1 & H2 & H3 & H4 & H5 & lH6


        End Using


    End Function
    Public Shared Function SHA256(ByVal message As String) As String
        Dim lH0 As UInteger = &H6A09E667
        Dim lH1 = &HBB67AE85UI
        Dim lH2 As UInteger = &H3C6EF372
        Dim lH3 = &HA54FF53AUI
        Dim lH4 As UInteger = &H510E527F
        Dim lH5 = &H9B05688CUI
        Dim h6 As UInteger = &H1F83D9AB
        Dim lH7 As UInteger = &H5BE0CD19

        Dim k = New UInteger() {&H428A2F98, &H71374491, &HB5C0FBCFUI, &HE9B5DBA5UI, &H3956C25B, &H59F111F1, &H923F82A4UI, &HAB1C5ED5UI, &HD807AA98UI, &H12835B01, &H243185BE, &H550C7DC3, &H72BE5D74, &H80DEB1FEUI, &H9BDC06A7UI, &HC19BF174UI, &HE49B69C1UI, &HEFBE4786UI, &HFC19DC6, &H240CA1CC, &H2DE92C6F, &H4A7484AA, &H5CB0A9DC, &H76F988DA, &H983E5152UI, &HA831C66DUI, &HB00327C8UI, &HBF597FC7UI, &HC6E00BF3UI, &HD5A79147UI, &H6CA6351, &H14292967, &H27B70A85, &H2E1B2138, &H4D2C6DFC, &H53380D13, &H650A7354, &H766A0ABB, &H81C2C92EUI, &H92722C85UI, &HA2BFE8A1UI, &HA81A664BUI, &HC24B8B70UI, &HC76C51A3UI, &HD192E819UI, &HD6990624UI, &HF40E3585UI, &H106AA070, &H19A4C116, &H1E376C08, &H2748774C, &H34B0BCB5, &H391C0CB3, &H4ED8AA4A, &H5B9CCA4F, &H682E6FF3, &H748F82EE, &H78A5636F, &H84C87814UI, &H8CC70208UI, &H90BEFFFAUI, &HA4506CEBUI, &HBEF9A3F7UI, &HC67178F2UI}

        Dim s0, s1, ch, maj, temp1, temp2 As UInteger

        Dim padded_message = paddmessage(message)
        Dim chunks As String() = Nothing, length As Integer = Nothing
        chunked(padded_message, chunks, length)
        Dim splitted As UInteger() = Nothing
        For x = 0 To length - 1
            Dim w = New UInteger(63) {}
            splitmessage(chunks(x), splitted)

            For i = 0 To 15
                w(i) = splitted(i)
            Next

            For i = 16 To 63
                s0 = w(i - 15) >> 7 Or w(i - 15) << 25 Xor w(i - 15) >> 18 Or w(i - 15) << 14 Xor w(i - 15) >> 3
                s1 = w(i - 2) >> 17 Or w(i - 2) << 15 Xor w(i - 2) >> 19 Or w(i - 2) << 13 Xor w(i - 2) >> 10
                w(i) = w(i - 16) + s0 + w(i - 7) + s1
            Next

            Dim a = lH0
            Dim b = lH1
            Dim c = lH2
            Dim d = lH3
            Dim e = lH4
            Dim f = lH5
            Dim g = h6
            Dim h = lH7

            For i = 0 To 63
                s1 = e >> 6 Or e << 26 Xor e >> 11 Or e << 21 Xor e >> 25 Or e << 7
                ch = e And f Xor Not e And g

                temp1 = h + s1 + ch + k(i) + w(i)
                s0 = a >> 2 Or a << 30 Xor a >> 13 Or a << 19 Xor a >> 22 Or a << 10
                maj = a And b Xor a And c Xor b And c
                temp2 = s0 + maj
                h = g
                g = f
                f = e
                e = d + temp1
                d = c
                c = b
                b = a
                a = temp1 + temp2
            Next
            lH0 += a
            lH1 += b
            lH2 += c
            lH3 += d
            lH4 += e
            lH5 += f
            h6 += g
            lH7 += h
        Next
        Dim H0 = String.Format(lH0.ToString("X").PadLeft(8, "0"c))
        Dim H1 = String.Format(lH1.ToString("X").PadLeft(8, "0"c))
        Dim H2 = String.Format(lH2.ToString("X").PadLeft(8, "0"c))
        Dim H3 = String.Format(lH3.ToString("X").PadLeft(8, "0"c))
        Dim H4 = String.Format(lH4.ToString("X").PadLeft(8, "0"c))
        Dim H5 = String.Format(lH5.ToString("X").PadLeft(8, "0"c))
        Dim lH6 = String.Format(h6.ToString("X").PadLeft(8, "0"c))
        Dim H7 = String.Format(lH7.ToString("X").PadLeft(8, "0"c))
        Return H0 & H1 & H2 & H3 & H4 & H5 & lH6 & H7
    End Function
    Public Shared Function SHA384(ByVal message As String) As String
        Dim lH0 = &HCBBB9D5DC1059ED8UL
        Dim lH1 As ULong = &H629A292A367CD507
        Dim lH2 = &H9159015A3070DD17UL
        Dim lH3 As ULong = &H152FECD8F70E5939
        Dim lH4 As ULong = &H67332667FFC00B31
        Dim lH5 = &H8EB44A8768581511UL
        Dim h6 = &HDB0C2E0D64F98FA7UL
        Dim lH7 As ULong = &H47B5481DBEFA4FA4

        Dim k = New ULong() {&H428A2F98D728AE22, &H7137449123EF65CD, &HB5C0FBCFEC4D3B2FUL, &HE9B5DBA58189DBBCUL, &H3956C25BF348B538, &H59F111F1B605D019, &H923F82A4AF194F9BUL, &HAB1C5ED5DA6D8118UL, &HD807AA98A3030242UL, &H12835B0145706FBE, &H243185BE4EE4B28C, &H550C7DC3D5FFB4E2, &H72BE5D74F27B896F, &H80DEB1FE3B1696B1UL, &H9BDC06A725C71235UL, &HC19BF174CF692694UL, &HE49B69C19EF14AD2UL, &HEFBE4786384F25E3UL, &HFC19DC68B8CD5B5, &H240CA1CC77AC9C65, &H2DE92C6F592B0275, &H4A7484AA6EA6E483, &H5CB0A9DCBD41FBD4, &H76F988DA831153B5, &H983E5152EE66DFABUL, &HA831C66D2DB43210UL, &HB00327C898FB213FUL, &HBF597FC7BEEF0EE4UL, &HC6E00BF33DA88FC2UL, &HD5A79147930AA725UL, &H6CA6351E003826F, &H142929670A0E6E70, &H27B70A8546D22FFC, &H2E1B21385C26C926, &H4D2C6DFC5AC42AED, &H53380D139D95B3DF, &H650A73548BAF63DE, &H766A0ABB3C77B2A8, &H81C2C92E47EDAEE6UL, &H92722C851482353BUL, &HA2BFE8A14CF10364UL, &HA81A664BBC423001UL, &HC24B8B70D0F89791UL, &HC76C51A30654BE30UL, &HD192E819D6EF5218UL, &HD69906245565A910UL, &HF40E35855771202AUL, &H106AA07032BBD1B8, &H19A4C116B8D2D0C8, &H1E376C085141AB53, &H2748774CDF8EEB99, &H34B0BCB5E19B48A8, &H391C0CB3C5C95A63, &H4ED8AA4AE3418ACB, &H5B9CCA4F7763E373, &H682E6FF3D6B2B8A3, &H748F82EE5DEFB2FC, &H78A5636F43172F60, &H84C87814A1F0AB72UL, &H8CC702081A6439ECUL, &H90BEFFFA23631E28UL, &HA4506CEBDE82BDE9UL, &HBEF9A3F7B2C67915UL, &HC67178F2E372532BUL, &HCA273ECEEA26619CUL, &HD186B8C721C0C207UL, &HEADA7DD6CDE0EB1EUL, &HF57D4F7FEE6ED178UL, &H6F067AA72176FBA, &HA637DC5A2C898A6, &H113F9804BEF90DAE, &H1B710B35131C471B, &H28DB77F523047D84, &H32CAAB7B40C72493, &H3C9EBE0A15C9BEBC, &H431D67C49C100D4C, &H4CC5D4BECB3E42B6, &H597F299CFC657E2A, &H5FCB6FAB3AD6FAEC, &H6C44198C4A475817}

        Dim s0, s1, ch, maj, temp1, temp2 As ULong

        Dim padded_message = paddmessage512(message)
        Dim chunks As String() = Nothing, length As Integer = Nothing
        chunked512(padded_message, chunks, length)
        Dim splitted As ULong() = Nothing

        For x = 0 To length - 1
            Dim w = New ULong(79) {}
            splitmessage512(chunks(x), splitted)

            For i = 0 To 15
                w(i) = splitted(i)
            Next

            For i = 16 To 79
                s0 = w(i - 15) >> 1 Or w(i - 15) << 63 Xor w(i - 15) >> 8 Or w(i - 15) << 56 Xor w(i - 15) >> 7
                s1 = w(i - 2) >> 19 Or w(i - 2) << 45 Xor w(i - 2) >> 61 Or w(i - 2) << 3 Xor w(i - 2) >> 6
                w(i) = w(i - 16) + s0 + w(i - 7) + s1
            Next

            Dim a = lH0
            Dim b = lH1
            Dim c = lH2
            Dim d = lH3
            Dim e = lH4
            Dim f = lH5
            Dim g = h6
            Dim h = lH7

            For i = 0 To 79
                s1 = e >> 14 Or e << 50 Xor e >> 18 Or e << 46 Xor e >> 41 Or e << 23
                ch = e And f Xor Not e And g

                temp1 = h + s1 + ch + k(i) + w(i)
                s0 = a >> 28 Or a << 36 Xor a >> 34 Or a << 30 Xor a >> 39 Or a << 25
                maj = a And b Xor a And c Xor b And c
                temp2 = s0 + maj
                h = g
                g = f
                f = e
                e = d + temp1
                d = c
                c = b
                b = a
                a = temp1 + temp2
            Next
            lH0 += a
            lH1 += b
            lH2 += c
            lH3 += d
            lH4 += e
            lH5 += f
            h6 += g
            lH7 += h
        Next
        Dim H0 = String.Format(lH0.ToString("X").PadLeft(16, "0"c))
        Dim H1 = String.Format(lH1.ToString("X").PadLeft(16, "0"c))
        Dim H2 = String.Format(lH2.ToString("X").PadLeft(16, "0"c))
        Dim H3 = String.Format(lH3.ToString("X").PadLeft(16, "0"c))
        Dim H4 = String.Format(lH4.ToString("X").PadLeft(16, "0"c))
        Dim H5 = String.Format(lH5.ToString("X").PadLeft(16, "0"c))
        Return H0 & H1 & H2 & H3 & H4 & H5
    End Function
    Public Shared Function SHA512(ByVal message As String) As String
        Dim lH0 As ULong = &H6A09E667F3BCC908
        Dim lH1 = &HBB67AE8584CAA73BUL
        Dim lH2 As ULong = &H3C6EF372FE94F82B
        Dim lH3 = &HA54FF53A5F1D36F1UL
        Dim lH4 As ULong = &H510E527FADE682D1
        Dim lH5 = &H9B05688C2B3E6C1FUL
        Dim h6 As ULong = &H1F83D9ABFB41BD6B
        Dim lH7 As ULong = &H5BE0CD19137E2179

        Dim k = New ULong() {&H428A2F98D728AE22, &H7137449123EF65CD, &HB5C0FBCFEC4D3B2FUL, &HE9B5DBA58189DBBCUL, &H3956C25BF348B538, &H59F111F1B605D019, &H923F82A4AF194F9BUL, &HAB1C5ED5DA6D8118UL, &HD807AA98A3030242UL, &H12835B0145706FBE, &H243185BE4EE4B28C, &H550C7DC3D5FFB4E2, &H72BE5D74F27B896F, &H80DEB1FE3B1696B1UL, &H9BDC06A725C71235UL, &HC19BF174CF692694UL, &HE49B69C19EF14AD2UL, &HEFBE4786384F25E3UL, &HFC19DC68B8CD5B5, &H240CA1CC77AC9C65, &H2DE92C6F592B0275, &H4A7484AA6EA6E483, &H5CB0A9DCBD41FBD4, &H76F988DA831153B5, &H983E5152EE66DFABUL, &HA831C66D2DB43210UL, &HB00327C898FB213FUL, &HBF597FC7BEEF0EE4UL, &HC6E00BF33DA88FC2UL, &HD5A79147930AA725UL, &H6CA6351E003826F, &H142929670A0E6E70, &H27B70A8546D22FFC, &H2E1B21385C26C926, &H4D2C6DFC5AC42AED, &H53380D139D95B3DF, &H650A73548BAF63DE, &H766A0ABB3C77B2A8, &H81C2C92E47EDAEE6UL, &H92722C851482353BUL, &HA2BFE8A14CF10364UL, &HA81A664BBC423001UL, &HC24B8B70D0F89791UL, &HC76C51A30654BE30UL, &HD192E819D6EF5218UL, &HD69906245565A910UL, &HF40E35855771202AUL, &H106AA07032BBD1B8, &H19A4C116B8D2D0C8, &H1E376C085141AB53, &H2748774CDF8EEB99, &H34B0BCB5E19B48A8, &H391C0CB3C5C95A63, &H4ED8AA4AE3418ACB, &H5B9CCA4F7763E373, &H682E6FF3D6B2B8A3, &H748F82EE5DEFB2FC, &H78A5636F43172F60, &H84C87814A1F0AB72UL, &H8CC702081A6439ECUL, &H90BEFFFA23631E28UL, &HA4506CEBDE82BDE9UL, &HBEF9A3F7B2C67915UL, &HC67178F2E372532BUL, &HCA273ECEEA26619CUL, &HD186B8C721C0C207UL, &HEADA7DD6CDE0EB1EUL, &HF57D4F7FEE6ED178UL, &H6F067AA72176FBA, &HA637DC5A2C898A6, &H113F9804BEF90DAE, &H1B710B35131C471B, &H28DB77F523047D84, &H32CAAB7B40C72493, &H3C9EBE0A15C9BEBC, &H431D67C49C100D4C, &H4CC5D4BECB3E42B6, &H597F299CFC657E2A, &H5FCB6FAB3AD6FAEC, &H6C44198C4A475817}

        Dim s0, s1, ch, maj, temp1, temp2 As ULong

        Dim padded_message = paddmessage512(message)
        Dim chunks As String() = Nothing, length As Integer = Nothing
        chunked512(padded_message, chunks, length)
        Dim splitted As ULong() = Nothing

        For x = 0 To length - 1
            Dim w = New ULong(79) {}
            splitmessage512(chunks(x), splitted)

            For i = 0 To 15
                w(i) = splitted(i)
            Next

            For i = 16 To 79
                s0 = w(i - 15) >> 1 Or w(i - 15) << 63 Xor w(i - 15) >> 8 Or w(i - 15) << 56 Xor w(i - 15) >> 7
                s1 = w(i - 2) >> 19 Or w(i - 2) << 45 Xor w(i - 2) >> 61 Or w(i - 2) << 3 Xor w(i - 2) >> 6
                w(i) = w(i - 16) + s0 + w(i - 7) + s1
            Next

            Dim a = lH0
            Dim b = lH1
            Dim c = lH2
            Dim d = lH3
            Dim e = lH4
            Dim f = lH5
            Dim g = h6
            Dim h = lH7

            For i = 0 To 79
                s1 = e >> 14 Or e << 50 Xor e >> 18 Or e << 46 Xor e >> 41 Or e << 23
                ch = e And f Xor Not e And g

                temp1 = h + s1 + ch + k(i) + w(i)
                s0 = a >> 28 Or a << 36 Xor a >> 34 Or a << 30 Xor a >> 39 Or a << 25
                maj = a And b Xor a And c Xor b And c
                temp2 = s0 + maj
                h = g
                g = f
                f = e
                e = d + temp1
                d = c
                c = b
                b = a
                a = temp1 + temp2
            Next
            lH0 += a
            lH1 += b
            lH2 += c
            lH3 += d
            lH4 += e
            lH5 += f
            h6 += g
            lH7 += h
        Next
        Dim H0 = String.Format(lH0.ToString("X").PadLeft(16, "0"c))
        Dim H1 = String.Format(lH1.ToString("X").PadLeft(16, "0"c))
        Dim H2 = String.Format(lH2.ToString("X").PadLeft(16, "0"c))
        Dim H3 = String.Format(lH3.ToString("X").PadLeft(16, "0"c))
        Dim H4 = String.Format(lH4.ToString("X").PadLeft(16, "0"c))
        Dim H5 = String.Format(lH5.ToString("X").PadLeft(16, "0"c))
        Dim lH6 = String.Format(h6.ToString("X").PadLeft(16, "0"c))
        Dim H7 = String.Format(lH7.ToString("X").PadLeft(16, "0"c))
        Return H0 & H1 & H2 & H3 & H4 & H5 & lH6 & H7
    End Function

    Private Shared Function paddmessage(ByVal message As String) As String
        Dim pad As StringBuilder = New StringBuilder()
        Dim length As StringBuilder = New StringBuilder()

        For Each c In message.ToCharArray()
            pad.Append(Convert.ToString(Microsoft.VisualBasic.AscW(c), 2).PadLeft(8, "0"c))
        Next
        Dim message_length = pad.Length
        pad.Append(1)
        While pad.Length Mod 512 <> 448
            pad.Append(0)
        End While

        length.Append(Convert.ToString(message_length, 2).PadLeft(64, "0"c))
        pad.Append(length.ToString())
        Return pad.ToString()
    End Function
    Private Shared Sub chunked(ByVal padded_messgage As String, <Out> ByRef chunks As String(), <Out> ByRef length As Integer)
        length = padded_messgage.Length / 512
        Dim temp = New String(length - 1) {}
        Dim i = 0, k = 0

        While i < padded_messgage.Length
            temp(k) = padded_messgage.Substring(i, 512)
            i += 512
            k += 1
        End While
        chunks = temp
    End Sub
    Private Shared Sub splitmessage(ByVal message As String, <Out> ByRef splitted As UInteger())
        Dim split = New UInteger(message.Length / 32 - 1) {}
        Dim i = 0, k = 0

        While i < message.Length
            split(k) = Convert.ToUInt32(message.Substring(i, 32), 2)
            i += 32
            k += 1
        End While
        splitted = split
    End Sub

    Private Shared Function paddmessage512(ByVal message As String) As String
        Dim pad As StringBuilder = New StringBuilder()
        Dim length As StringBuilder = New StringBuilder()

        For Each c In message.ToCharArray()
            pad.Append(Convert.ToString(Microsoft.VisualBasic.AscW(c), 2).PadLeft(8, "0"c))
        Next
        Dim message_length = pad.Length
        pad.Append(1)
        While pad.Length Mod 1024 <> 896
            pad.Append(0)
        End While

        length.Append(Convert.ToString(message_length, 2).PadLeft(128, "0"c))
        pad.Append(length.ToString())
        Return pad.ToString()
    End Function
    Private Shared Sub chunked512(ByVal padded_messgage As String, <Out> ByRef chunks As String(), <Out> ByRef length As Integer)
        length = padded_messgage.Length / 1024
        Dim temp = New String(length - 1) {}
        Dim i = 0, k = 0

        While i < padded_messgage.Length
            temp(k) = padded_messgage.Substring(i, 1024)
            i += 1024
            k += 1
        End While
        chunks = temp
    End Sub
    Private Shared Sub splitmessage512(ByVal message As String, <Out> ByRef splitted As ULong())
        Dim split = New ULong(message.Length / 64 - 1) {}
        Dim i = 0, k = 0

        While i < message.Length
            split(k) = Convert.ToUInt64(message.Substring(i, 64), 2)
            i += 64
            k += 1
        End While
        splitted = split
    End Sub
End Class
