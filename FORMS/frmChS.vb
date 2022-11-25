Public Class frmChS
    Dim oldType As String

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim sSQL As String

        Dim sCOUNT As Integer
        Dim rs As Recordset

        sSQL = "SELECT count(*) as t_n FROM TBL_HASH"

        rs = New Recordset
        rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

        With rs
            sCOUNT = .Fields("t_n").Value
        End With
        rs.Close()
        rs = Nothing

        Select Case sCOUNT

            Case 0

                sSQL = "SELECT count(*) as t_n FROM TBL_CONF"

                rs = New Recordset
                rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

                With rs
                    sCOUNT = .Fields("t_n").Value
                End With
                rs.Close()
                rs = Nothing

                Select Case sCOUNT

                    Case 1

                        sSQL = "SELECT * FROM TBL_CONF"
                        rs = New Recordset
                        rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)
                        With rs
                            sCOUNT = .Fields("id").Value
                        End With
                        rs.Close()
                        rs = Nothing

                    Case Else

                End Select

                sSQL = "UPDATE TBL_CONF SET type='" & MainForm.typeCRC & "' WHERE id =" & sCOUNT
                DB7.Execute(sSQL)

            Case Else

                If MsgBox("В базе данных уже имеются данные" & vbCrLf & "Изменение алгоритма вычисления контрольной суммы " & vbCrLf & "может привести к повреждению данных" & vbCrLf & "Хотите продолжить?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    sSQL = "SELECT count(*) as t_n FROM TBL_CONF"

                    rs = New Recordset
                    rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

                    With rs
                        sCOUNT = .Fields("t_n").Value
                    End With
                    rs.Close()
                    rs = Nothing

                    Select Case sCOUNT

                        Case 1

                            sSQL = "SELECT * FROM TBL_CONF"
                            rs = New Recordset
                            rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)
                            With rs
                                sCOUNT = .Fields("id").Value
                            End With
                            rs.Close()
                            rs = Nothing

                            sSQL = "UPDATE TBL_CONF SET type='" & MainForm.typeCRC & "' WHERE id =" & sCOUNT
                            DB7.Execute(sSQL)

                        Case Else

                    End Select

                End If

        End Select

        If oldType <> MainForm.typeCRC Then

            If MsgBox("Изменен алгоритм вычисления контрольной суммы" & vbCrLf & "Очистить существующие данные" & vbCrLf & "и пересчитать контрольные суммы?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                sSQL = "SELECT count(*) as t_n FROM TBL_DIR"

                rs = New Recordset
                rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

                With rs
                    sCOUNT = .Fields("t_n").Value
                End With
                rs.Close()
                rs = Nothing

                Select Case sCOUNT

                    Case 0

                        Call MainForm.LoadData()

                    Case Else

                        sSQL = "Delete * from TBL_HASH"
                        DB7.Execute(sSQL)


                        sSQL = "SELECT * FROM TBL_DIR"
                        rs = New Recordset
                        rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

                        MainForm.lvFiles.Visible = False

                        With rs
                            .MoveFirst()
                            Do While Not .EOF

                                BasePath = .Fields("dir").Value
                                Call MainForm.find_file()

                                .MoveNext()
                            Loop
                        End With
                        rs.Close()
                        rs = Nothing
                        MainForm.lvFiles.Visible = True

                End Select
            End If

            MainForm.AddLogEntr("Изменен алгоритм вычисления контрольной суммы с " & oldType & " на " & MainForm.typeCRC & vbCrLf & "Произведена очистка базы данных и пересчитаны контрольные суммы", 1)

        End If

        '        MainForm.LoadData()
        Me.Close()

    End Sub

    Private Sub rbMD5_CheckedChanged(sender As Object, e As EventArgs) Handles rbMD5.CheckedChanged
        MainForm.typeCRC = "MD5"
    End Sub

    Private Sub rbCRC32_CheckedChanged(sender As Object, e As EventArgs) Handles rbCRC32.CheckedChanged
        MainForm.typeCRC = "Crc32"
    End Sub

    Private Sub rbSHA256_CheckedChanged(sender As Object, e As EventArgs) Handles rbSHA256.CheckedChanged
        MainForm.typeCRC = "SHA256"
    End Sub

    Private Sub frmChS_Load(sender As Object, e As EventArgs) Handles Me.Load

        oldType = MainForm.typeCRC

        Dim sSQL As String

        Dim sCOUNT As Integer
        Dim sTMP As String

        sSQL = "SELECT count(*) as t_n FROM TBL_CONF"
        Dim rs As Recordset
        rs = New Recordset
        rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

        With rs
            sCOUNT = .Fields("t_n").Value
        End With
        rs.Close()
        rs = Nothing

        Select Case sCOUNT

            Case 1

                sSQL = "SELECT * FROM TBL_CONF"
                rs = New Recordset
                rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)
                With rs
                    sTMP = .Fields("type").Value
                End With
                rs.Close()
                rs = Nothing

            Case Else

        End Select

        Select Case sTMP

            Case "MD5"
                rbMD5.Checked = True

            Case "Crc32"

                rbCRC32.Checked = True

            Case "SHA256"

                rbSHA256.Checked = True

            Case "SHA1"

                rbSHA1.Checked = True

            Case "SHA512"

                rbSHA512.Checked = True

            Case "GOST"

                rbGOST.Checked = True

            Case "Sha384"

                rbSHA384.Checked = True

            Case "Crc64"

                rbCRC64.Checked = True

            Case "Adler32"

                rbAdler.Checked = True


            Case "ripmd160"

                rbRipMD.Checked = True

            Case Else

                rbMD5.Checked = True

        End Select



        Dim LOG_EVT As String

        sSQL = "SELECT * FROM TBL_CONF"
        rs = New Recordset
        rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

        With rs
            LOG_EVT = .Fields("EVT").Value
        End With
        rs.Close()
        rs = Nothing

        Select Case LOG_EVT

            Case "1"
                chkEVT.Checked = True

            Case Else

                chkEVT.Checked = False

        End Select


    End Sub

    Private Sub rbSHA1_CheckedChanged(sender As Object, e As EventArgs) Handles rbSHA1.CheckedChanged
        MainForm.typeCRC = "SHA1"
    End Sub

    Private Sub rbSHA512_CheckedChanged(sender As Object, e As EventArgs) Handles rbSHA512.CheckedChanged
        MainForm.typeCRC = "SHA512"
    End Sub

    Private Sub btnDirectory_Click(sender As Object, e As EventArgs) Handles btnDirectory.Click
        frmAdd_dir.ShowDialog(Me)
    End Sub

    Private Sub rbSHA384_CheckedChanged(sender As Object, e As EventArgs) Handles rbSHA384.CheckedChanged
        ' MainForm.typeCRC = "GOST"

        'GetSha384Hash()
        MainForm.typeCRC = "Sha384"

    End Sub


    Private Sub rbGOST_CheckedChanged_1(sender As Object, e As EventArgs) Handles rbGOST.CheckedChanged
        MainForm.typeCRC = "GOST"
    End Sub

    Private Sub chkEVT_CheckedChanged(sender As Object, e As EventArgs) Handles chkEVT.CheckedChanged

        Select Case chkEVT.Checked

            Case True

                DB7.Execute("UPDATE TBL_CONF SET EVT='1'")

                MainForm.LOG_EVT = True
            Case Else
                DB7.Execute("UPDATE TBL_CONF SET EVT='0'")
                MainForm.LOG_EVT = False
        End Select


    End Sub

    Private Sub rbCRC64_CheckedChanged(sender As Object, e As EventArgs) Handles rbCRC64.CheckedChanged
        MainForm.typeCRC = "Crc64"
    End Sub

    Private Sub rbAdler_CheckedChanged(sender As Object, e As EventArgs) Handles rbAdler.CheckedChanged
        MainForm.typeCRC = "Adler32"
    End Sub

    Private Sub rbRipMD_CheckedChanged(sender As Object, e As EventArgs) Handles rbRipMD.CheckedChanged
        MainForm.typeCRC = "ripmd160"
    End Sub
End Class