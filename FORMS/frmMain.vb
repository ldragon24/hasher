Imports System.IO
Imports System.Threading

Public Class MainForm

    Private MassDel As Boolean = False
    Private dvCOUNT As Integer
    Private rCOUNT As Integer
    Private Const CP_NOCLOSE_BUTTON As Integer = &H200
    Public typeCRC As String

    Public ThrTIMER_ As System.Threading.Thread

    Public lvForm As String

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams

        Get
            Dim myCP As CreateParams = MyBase.CreateParams
            myCP.ClassStyle = myCP.ClassStyle Or CP_NOCLOSE_BUTTON

            Return myCP
        End Get

    End Property

    Private Sub typeCRCLoad()
        Dim sSQL As String

        Dim sCOUNT As Integer

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
                    typeCRC = .Fields("type").Value
                End With
                rs.Close()
                rs = Nothing

            Case Else

                sSQL = "DELETE * FROM TBL_CONF"
                DB7.Execute(sSQL)

                sSQL = "INSERT INTO FROM TBL_CONF (type) VALUES ('MD5')"
                DB7.Execute(sSQL)

                Call typeCRCLoad()
        End Select

    End Sub

    Private Sub MainForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        ThrTIMER_.Abort()
        UnLoadDatabase()
        ni.Visible = False


    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load

        AddLogEntr("Программа запущена", 2)


        LoadDatabase()

        RepAddBrToolStripMenuItem.Enabled = False

        lvFiles.Columns.Add(("id"), 20, HorizontalAlignment.Left)
        lvFiles.Columns.Add(("Имя файла"), 100, HorizontalAlignment.Left)
        lvFiles.Columns.Add(("Контрольная сумма"), 100, HorizontalAlignment.Left)
        lvFiles.Columns.Add(("Проверка"), 30, HorizontalAlignment.Left)
        lvFiles.Columns.Add(("Дата"), 30, HorizontalAlignment.Left)
        'lvFiles.Columns.Add(("Размер файла"), 30, HorizontalAlignment.Left)

        lvFilesR.Columns.Add(("id"), 20, HorizontalAlignment.Left)
        lvFilesR.Columns.Add(("Имя файла"), 100, HorizontalAlignment.Left)
        lvFilesR.Columns.Add(("Контрольная сумма"), 100, HorizontalAlignment.Left)
        lvFilesR.Columns.Add(("Расчитанная КС"), 30, HorizontalAlignment.Left)


        lvFilesF.Columns.Add(("id"), 20, HorizontalAlignment.Left)
        lvFilesF.Columns.Add(("Имя файла"), 100, HorizontalAlignment.Left)
        lvFilesF.Columns.Add(("Контрольная сумма"), 100, HorizontalAlignment.Left)


        Call typeCRCLoad()

        Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))


        ThrTIMER_ = New System.Threading.Thread(AddressOf ThrTIMER)
        ThrTIMER_.Start()




    End Sub

    Public Sub LoadData()


        Me.Cursor = Cursors.WaitCursor

        Dim sCOUNT As Integer
        Dim sSQL As String

        sSQL = "SELECT count(*) as t_n FROM TBL_HASH"

        Dim rs As Recordset
        rs = New Recordset
        rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

        With rs
            sCOUNT = .Fields("t_n").Value
        End With
        rs.Close()
        rs = Nothing

        Select Case sCOUNT

            Case 0
                stlabel.Text = "Записей в базе не найдено"
                UpdateList.Enabled = False
                SaveList.Enabled = False
                ClearDB.Enabled = False
                findDouble.Enabled = False
            Case Else

                stlabel.Text = "Записей в базе: " & sCOUNT
                UpdateList.Enabled = True
                ClearDB.Enabled = True
                SaveList.Enabled = True
                findDouble.Enabled = True
                Call find_file_re()

        End Select

        ResList(lvFiles)


        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ADD_DIR()

        Try

            Dim DirectoryBrowser As New FolderBrowserDialog

            With DirectoryBrowser

                .RootFolder = Environment.SpecialFolder.Desktop

                .SelectedPath = Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString & "\"
                .Description = "Выбор директории для загрузки данных"

                If .ShowDialog = DialogResult.OK Then
                    BasePath = .SelectedPath
                    txtPath.Text = .SelectedPath
                Else
                    Exit Sub

                End If

            End With

            BasePath = BasePath & "\"

            AddLogEntr("Добавлен новый каталог: " & BasePath, 2)

            Call find_file()

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Call ADD_DIR()

    End Sub

    Private Sub ADD_DB_HASH(ByVal A1 As String, A2 As String, A3 As String)

        Dim sSQL As String
        Dim sCOUNT As Integer

        Try

            Dim d() As String
            d = Split(A1, ".")

            sSQL = "SELECT count(*) as t_n FROM TBL_IGNORE WHERE type='" & d(1) & "'"

            Dim rs As Recordset
            rs = New Recordset
            rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

            With rs
                sCOUNT = .Fields("t_n").Value
            End With
            rs.Close()
            rs = Nothing

            Select Case sCOUNT

                Case 0

                Case Else

                    Exit Sub

            End Select

        Catch ex As Exception

        End Try


        ' sSQL = "INSERT INTO [TBL_HASH]([file],[hash],[dttm]) VALUES(@item1,@item2,@item3)"

        sSQL = "INSERT INTO [TBL_HASH]([file],[hash],[dttm]) VALUES('" & A1 & "','" & A2 & "','" & A3 & "')"


        Try

            DB7.Execute(sSQL)

            'Dim cmdInsert As New OleDbCommand
            'Dim iSqlStatus As Integer

            'cmdInsert.Parameters.Clear()

            'With cmdInsert

            '    .CommandText = sSQL
            '    .CommandType = CommandType.Text
            '    .Parameters.AddWithValue("@value1", A1)
            '    .Parameters.AddWithValue("@value2", A2)
            '    .Parameters.AddWithValue("@value3", A3)

            '    .Connection = DB8

            'End With

            'iSqlStatus = cmdInsert.ExecuteNonQuery

            'If Not iSqlStatus = 0 Then

            'Else

            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

        End Try


    End Sub

    Private Sub find_file()

        lvFiles.Sorting = SortOrder.None
        lvFiles.ListViewItemSorter = Nothing
        lvFiles.Items.Clear()

        Dim intcount As Integer
        Dim thisHash As String
        intcount = 0

        On Error Resume Next


        Dim dirs() As String = Directory.GetFiles(BasePath, "*.*", SearchOption.AllDirectories)

        Dim dir As String

        For Each dir In dirs

            lvFiles.Items.Add(lvFiles.Items.Count + 1)
            lvFiles.Items(intcount).SubItems.Add(dir)

            Select Case typeCRC

                Case "MD5"
                    thisHash = GetHash(dir)

                Case "Crc32"

                    thisHash = GetCRC32(dir)

                Case "SHA256"

                    thisHash = GetSHA256(dir)

                Case "SHA512"

                    thisHash = GetSHA512(dir)

                Case "SHA1"

                    thisHash = GetSHA1(dir)

                Case "GOST"

                    ' thisHash = GetGOST(FileToByteArray(dir))

                Case Else

                    MsgBox("Не выбран алгоритм вычисления контрольной суммы", MsgBoxStyle.Critical)

                    frmChS.ShowDialog(Me)

            End Select

            lvFiles.Items(intcount).SubItems.Add(thisHash)

            Dim sSQL As String
            Dim sCOUNT As Integer

            sSQL = "SELECT count(*) as t_n FROM TBL_HASH"

            'Dim cmd As OleDbCommand
            'Dim rs As OleDbDataReader

            'cmd = New OleDbCommand(sSQL, DB8)
            'rs = cmd.ExecuteReader

            'While rs.Read

            '    sCOUNT = rs.Item("t_n")

            'End While
            'rs.Close()
            'rs = Nothing

            Dim rs As Recordset
            rs = New Recordset
            rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

            With rs
                sCOUNT = .Fields("t_n").Value
            End With
            rs.Close()
            rs = Nothing

            Select Case sCOUNT

                Case 0
                    ADD_DB_HASH(dir, thisHash, DateAndTime.Now)
                Case Else

                    If Not RSExistsHash(dir) Then
                        ADD_DB_HASH(dir, thisHash, DateAndTime.Now)
                    End If

            End Select

            intcount = intcount + 1

        Next

        ' Catch ex As Exception
        'MsgBox(ex.Message)
        ResList(lvFiles)
        'End Try

        Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

    End Sub

    Private Sub find_file_re()

        Me.Cursor = Cursors.WaitCursor


        lvFiles.Sorting = SortOrder.None
        lvFiles.ListViewItemSorter = Nothing
        lvFiles.Items.Clear()

        lvFilesR.Sorting = SortOrder.None
        lvFilesR.ListViewItemSorter = Nothing
        lvFilesR.Items.Clear()

        lvFilesF.Sorting = SortOrder.None
        lvFilesF.ListViewItemSorter = Nothing
        lvFilesF.Items.Clear()

        Dim intcount As Integer
        Dim intj As Integer = 0
        Dim intjFF As Integer = 0
        Dim sChange As Boolean = False
        Dim sMessage As String

        intcount = 0
        Try

            Dim sSQL As String

            sSQL = "SELECT * FROM TBL_HASH"

            'Dim cmd As OleDbCommand
            'Dim rs As OleDbDataReader
            'cmd = New OleDbCommand(sSQL, DB8)
            'rs = cmd.ExecuteReader

            'While rs.Read

            '    lvFiles.Items.Add(rs.Item("id"))
            '    lvFiles.Items(intcount).SubItems.Add(rs.Item("file"))
            '    lvFiles.Items(intcount).SubItems.Add(rs.Item("hash"))

            '    Dim uname, uname1 As String
            '    uname = rs.Item("file")
            '    uname1 = rs.Item("hash")
            '    Dim newHash As String = GetHash(rs.Item("file"))

            '    If rs.Item("hash") = newHash Then

            '        lvFiles.Items(intcount).SubItems.Add("Ok")

            '    Else
            '        lvFiles.Items(intcount).SubItems.Add("No")
            '        lvFiles.Items(CInt(intcount)).ForeColor = Color.Cyan
            '        lvFiles.Items(CInt(intcount)).BackColor = Color.Red
            '        intj = intj + 1
            '    End If

            '    intcount = intcount + 1
            'End While
            'rs.Close()
            'rs = Nothing

            Dim rs As Recordset

            rs = New Recordset
            rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)


            With rs
                .MoveFirst()
                Do While Not .EOF

                    lvFiles.Items.Add(.Fields("id").Value)

                    If Not IsDBNull(.Fields("file").Value) Then
                        lvFiles.Items(CInt(intcount)).SubItems.Add(.Fields("file").Value)
                    Else
                        lvFiles.Items(CInt(intcount)).SubItems.Add("")
                    End If

                    If Not IsDBNull(.Fields("hash").Value) Then
                        lvFiles.Items(CInt(intcount)).SubItems.Add(.Fields("hash").Value)
                    Else
                        lvFiles.Items(CInt(intcount)).SubItems.Add("")
                    End If

                    Dim uname, uname1 As String
                    uname = .Fields("file").Value
                    uname1 = .Fields("hash").Value


                    Dim newHash As String


                    If IO.File.Exists(.Fields("file").Value) Then


                        'typeCRC = "GOST"

                        Select Case typeCRC

                            Case "MD5"
                                newHash = GetHash(.Fields("file").Value)

                            Case "Crc32"

                                newHash = GetCRC32(.Fields("file").Value)

                            Case "SHA256"

                                newHash = GetSHA256(.Fields("file").Value)

                            Case "SHA1"

                                newHash = GetSHA1(.Fields("file").Value)


                            Case "SHA512"

                                newHash = GetSHA512(.Fields("file").Value)

                            Case "GOST"


                                '  newHash = GetGOST(FileToByteArray(.Fields("file").Value))
                                'newHash = ReadFileC(.Fields("file").Value)


                            Case Else

                                MsgBox("Не выбран алгоритм вычисления контрольной суммы", MsgBoxStyle.Critical)

                                frmChS.ShowDialog(Me)

                        End Select

                        If .Fields("hash").Value = newHash Then

                            lvFiles.Items(intcount).SubItems.Add("Ok")

                        Else
                            sChange = True

                            lvFilesR.Items.Add(.Fields("id").Value)
                            lvFilesR.Items(intj).SubItems.Add(.Fields("file").Value)
                            lvFilesR.Items(intj).SubItems.Add(.Fields("hash").Value)
                            lvFilesR.Items(intj).SubItems.Add(newHash)

                            intj = intj + 1
                            'Если хэш не соответствует то


                            lvFiles.Items(intcount).SubItems.Add("No")

                            lvFiles.Items(CInt(intcount)).ForeColor = Color.Cyan
                            lvFiles.Items(CInt(intcount)).BackColor = Color.Red


                            If Not RsExistTXT(.Fields("file").Value, .Fields("hash").Value, "zfile") Then
                                Dim d = Now
                                IO.File.AppendAllText(Application.StartupPath & "\change.log", d & "|" & "Обнаружены изменения в файле: " & "|" & .Fields("file").Value & "|" & " Записанная контрольная сумма: " & "|" & .Fields("hash").Value & "|" & " не соответствует текущей: " & "|" & newHash & vbNewLine, System.Text.Encoding.Default)

                                ' My.Application.Log.WriteEntry("Обнаружены изменения в файле: " & .Fields("file").Value & " Записанная контрольная сумма: " & .Fields("hash").Value & " не соответствует текущей: " & newHash, TraceEventType.Warning)

                                AddLogEntr("Обнаружены изменения в файле: " & .Fields("file").Value & vbCrLf & " Записанная контрольная сумма: " & .Fields("hash").Value & " не соответствует текущей: " & newHash, 1)

                            End If

                            If Len(sMessage) <> 0 Then

                                sMessage = sMessage & vbCrLf & "Обнаружены изменения в файле: " & .Fields("file").Value & vbCrLf

                            Else

                                sMessage = "Обнаружены изменения в файле: " & .Fields("file").Value & vbCrLf

                            End If

                        End If

                    Else

                        sChange = True

                        lvFilesF.Items.Add(.Fields("id").Value)
                        lvFilesF.Items(intjFF).SubItems.Add(.Fields("file").Value)
                        lvFilesF.Items(intjFF).SubItems.Add(.Fields("hash").Value)

                        intjFF = intjFF + 1

                        lvFiles.Items(intcount).SubItems.Add("Файл не найден")
                        lvFiles.Items(CInt(intcount)).ForeColor = Color.Yellow
                        lvFiles.Items(CInt(intcount)).BackColor = Color.SlateGray

                        If Not RsExistTXT(.Fields("file").Value, .Fields("hash").Value, "efile") Then
                            Dim d = Now
                            IO.File.AppendAllText(Application.StartupPath & "\change.log", d & "|" & "Не найден файл: " & "|" & .Fields("file").Value & "|" & " Записанная контрольная сумма: " & "|" & .Fields("hash").Value & vbNewLine, System.Text.Encoding.Default)
                            '############################################

                            AddLogEntr("Не найден файл: " & .Fields("file").Value & vbCrLf & " Записанная контрольная сумма: " & .Fields("hash").Value, 1)

                        End If

                        If Len(sMessage) <> 0 Then

                            sMessage = sMessage & vbCrLf & "Не найден файл: " & .Fields("file").Value & vbCrLf

                        Else

                            sMessage = "Не найден файл: " & .Fields("file").Value & vbCrLf

                        End If

                    End If

                    If Not IsDBNull(.Fields("dttm").Value) Then
                        lvFiles.Items(CInt(intcount)).SubItems.Add(.Fields("dttm").Value)
                    Else
                        lvFiles.Items(CInt(intcount)).SubItems.Add("")
                    End If

                    lvFiles.Items(CInt(intcount)).Selected = True
                    lvFiles.Items(CInt(intcount)).EnsureVisible()

                    ' Dim f As New IO.FileInfo(.Fields("file").Value)
                    ' lvFiles.Items(CInt(intcount)).SubItems.Add(f.Length)

                    intcount = intcount + 1

                    Application.DoEvents()

                    .MoveNext()
                Loop
            End With
            rs.Close()
            rs = Nothing

            intcount = intcount + 1

            'Если имеются измененные файлы
            Select Case intj

                Case 0
                    stat2.Text = "Изменений в файлах не зафиксировано"
                    stat2.ForeColor = Color.Green
                Case Else

                    stat2.Text = "Имеются изменения в " & intj & " файлах"
                    stat2.ForeColor = Color.Red
            End Select

            'Если имеются отсутствующие файлы (удаленные)
            Select Case intjFF

                Case 0

                    notfind.Text = "Все файлы в наличии"
                    notfind.ForeColor = Color.Green

                Case Else

                    notfind.Text = "Не найдены файлы " & intjFF & " шт."
                    notfind.ForeColor = Color.Red

            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
            ResList(lvFiles)
        End Try

        Select Case sChange

            Case True

                ni.ShowBalloonTip(TIME_TOOL_TIPS * 1000, "Проверка контрольной суммы", sMessage, ToolTipIcon.Warning)

            Case Else

        End Select

        Me.Cursor = Cursors.Default
        ResList(lvFiles)
    End Sub

    Private Sub lvFiles_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvFiles.ColumnClick

        SORTING_LV(lvFiles, e)

    End Sub

    Private Sub lvFiles_MouseUp(sender As Object, e As MouseEventArgs) Handles lvFiles.MouseUp

        Try
            If lvFiles.Items.Count = 0 Then Exit Sub

            If e.Button = MouseButtons.Right Then
                cmBmenu.Show(CType(sender, Control), e.Location)

            Else

            End If

            Dim intj As Integer

            For z = 0 To lvFiles.SelectedItems.Count - 1
                rCOUNT = (lvFiles.SelectedItems(z).Text)
                intj = z
            Next

            Select Case lvFiles.SelectedItems(intj).SubItems(3).Text

                Case "No"
                    RepAddBrToolStripMenuItem.Enabled = True
                Case Else

                    RepAddBrToolStripMenuItem.Enabled = False

            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub lvFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvFiles.SelectedIndexChanged

        If lvFiles.Items.Count = 0 Then Exit Sub

        Dim z As Integer

        For z = 0 To lvFiles.SelectedItems.Count - 1
            dvCOUNT = (lvFiles.SelectedItems(z).Text)
        Next

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

        If MassDel = False Then
            lvFiles.CheckBoxes = True
            lvFiles.MultiSelect = True
            MassDel = True

            MsgBox("Для удаления пометьте галочкой нужное, и выберите в меню - Удалить!", MsgBoxStyle.Exclamation)

        Else
            lvFiles.CheckBoxes = False
            lvFiles.MultiSelect = False
            MassDel = False
        End If

    End Sub

    Private Sub mnuDeltoBranch_Click(sender As Object, e As EventArgs) Handles mnuDeltoBranch.Click

        If lvFiles.Items.Count = 0 Then Exit Sub

        Dim z As Integer

        Dim intj As Integer

        For z = 0 To lvFiles.SelectedItems.Count - 1
            rCOUNT = (lvFiles.SelectedItems(z).Text)
            intj = z
        Next

        If MsgBox("Выбранные данные будут удалены" & vbCrLf & "Данные будут потеряны" & vbCrLf & "Хотите продолжить?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            If MassDel = False Then

                AddLogEntr("Удален файл: " & lvFiles.SelectedItems(intj).SubItems(1).Text & vbCrLf & "Записанная контрольная сумма: " & lvFiles.SelectedItems(intj).SubItems(2).Text, 1)
                DELETE_FILE(rCOUNT)

            Else

                intj = 0
                Dim intj1 As Integer = 0

                lvFiles.Select()

                For intj = 0 To lvFiles.Items.Count - 1

                    lvFiles.Items(intj).Selected = True
                    lvFiles.Items(intj).EnsureVisible()

                    If lvFiles.Items(intj).Checked = True Then

                        intj1 = intj1 + 1

                    End If

                Next

                If intj1 > 0 Then

                    lvFiles.Select()

                    For intj = 0 To lvFiles.Items.Count - 1

                        lvFiles.Items(intj).Selected = True
                        lvFiles.Items(intj).EnsureVisible()

                        If lvFiles.Items(intj).Checked = True Then

                            Call DELETE_FILE(lvFiles.SelectedItems(intj).Text)
                            AddLogEntr("Удален файл: " & lvFiles.SelectedItems(intj).SubItems(1).Text & vbCrLf & "Записанная контрольная сумма: " & lvFiles.SelectedItems(intj).SubItems(2).Text, 1)
                        End If

                    Next

                End If

            End If

            Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))
        End If

        lvFiles.CheckBoxes = False
        lvFiles.MultiSelect = False
        MassDel = False

    End Sub

    Private Sub DELETE_FILE(Optional ByVal ssid As Integer = 0)
        Dim z As Integer

        If ssid = 0 Then

            For z = 0 To lvFiles.SelectedItems.Count - 1
                rCOUNT = (lvFiles.SelectedItems(z).Text)
            Next

            ssid = rCOUNT

        End If

        Dim sSQL As String
        'Dim cmd As OleDbCommand
        'Dim dr As OleDbDataReader

        sSQL = "Delete * FROM TBL_HASH where id =" & ssid

        DB7.Execute(sSQL)

        'cmd = New OleDbCommand(sSQL, DB8)
        'dr = cmd.ExecuteReader
        'dr = Nothing

    End Sub

    Private Sub RepAddBrToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RepAddBrToolStripMenuItem.Click

        If MsgBox("Выбранные изменения будут приняты" & vbCrLf & "Хотите продолжить?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            Dim intj As Integer

            For z = 0 To lvFiles.SelectedItems.Count - 1
                rCOUNT = (lvFiles.SelectedItems(z).Text)
                intj = z
            Next


            Dim newHash As String ' = GetHash(lvFiles.SelectedItems(intj).SubItems(1).Text)


            Select Case typeCRC

                Case "MD5"
                    newHash = GetHash(lvFiles.SelectedItems(intj).SubItems(1).Text)

                Case "Crc32"

                    newHash = GetCRC32(lvFiles.SelectedItems(intj).SubItems(1).Text)

                Case "SHA256"

                    newHash = GetSHA256(lvFiles.SelectedItems(intj).SubItems(1).Text)

                Case "SHA512"

                    newHash = GetSHA512(lvFiles.SelectedItems(intj).SubItems(1).Text)

                Case "SHA1"

                    newHash = GetSHA1(lvFiles.SelectedItems(intj).SubItems(1).Text)

                Case Else

                    MsgBox("Не выбран алгоритм вычисления контрольной суммы", MsgBoxStyle.Critical)

                    frmChS.ShowDialog(Me)

            End Select

            Dim sSQL As String
            ' sSQL = "UPDATE [TBL_HASH] SET [hash]=@item1,[dttm]=@item2 WHERE id=@id"

            'Dim cmdInsert As New OleDbCommand
            'Dim query As String = sSQL
            'Dim iSqlStatus As Integer

            'cmdInsert.Parameters.Clear()

            Try

                'sSQL = "UPDATE [TBL_HASH] SET [hash]=@item1,[dttm]=@item2 WHERE id=@id"

                sSQL = "UPDATE TBL_HASH SET hash='" & newHash & "', dttm='" & DateAndTime.Now.ToString & "' WHERE id =" & rCOUNT
                DB7.Execute(sSQL)

                AddLogEntr("Записана новая контрольная сумма для файла: " & lvFiles.SelectedItems(intj).SubItems(1).Text & vbCrLf & "Контрольная сумма: " & newHash & vbCrLf & "Старая контрольная сумма: " & lvFiles.SelectedItems(intj).SubItems(2).Text, 1)

                'With cmdInsert

                '    .CommandText = sSQL
                '    .CommandType = CommandType.Text
                '    .Parameters.AddWithValue("@value1", newHash)
                '    'de6f77bd304e63d98592218630a12633
                '    .Parameters.AddWithValue("@value2", DateAndTime.Now.ToString)

                '    .Parameters.AddWithValue("@id", rCOUNT)

                '    .Connection = DB8
                'End With

                ''HandleConnection(DB8)

                'iSqlStatus = cmdInsert.ExecuteNonQuery

                'If Not iSqlStatus = 0 Then
                '    'Return False
                'Else
                '    'Return True
                'End If
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally

                'HandleConnection(DB8)
            End Try

            Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

        End If

    End Sub

    Private Sub M_exit_Click(sender As Object, e As EventArgs) Handles M_exit.Click

        If MsgBox("Работа программы будет завершена" & vbCrLf & "Хотите продолжить?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            ThrTIMER_.Abort()
            UnLoadDatabase()
            ni.Visible = False
            End

        End If

    End Sub

    Private Sub addFoldertoBranch_Click(sender As Object, e As EventArgs) Handles addFoldertoBranch.Click

        Call ADD_DIR()

    End Sub

    Private Sub AllDeleteDB_Click(sender As Object, e As EventArgs) Handles AllDeleteDB.Click

        Call All_Delete()

    End Sub

    Private Sub All_Delete()

        If MsgBox("Все данные содержащиеся в базе данных" & vbCrLf & "будут удалены" & vbCrLf & "Хотите продолжить?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            Dim sSQL As String
            'Dim cmd As OleDbCommand
            'Dim dr As OleDbDataReader

            sSQL = "Delete * FROM TBL_HASH"
            'cmd = New OleDbCommand(sSQL, DB8)
            'dr = cmd.ExecuteReader
            'dr = Nothing

            AddLogEntr("База данных очищена", 1)

            DB7.Execute(sSQL)

            COMPARE_DB()

            If IO.File.Exists(Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString & "\change.log") Then

                IO.File.Delete((Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString & "\change.log"))

            End If

        End If
        lvFiles.Items.Clear()

        Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

    End Sub

    Public Sub COMPARE_DB()

        Dim JRO As JRO.JetEngine

        Try

            UnLoadDatabase()

            Dim sBname As String
            sBname = "temp_" & Base_Name

            JRO = New JRO.JetEngine

            JRO.CompactDatabase("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & Base_Name, "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & sBname)
            Kill(Base_Name)
            Rename(sBname, Base_Name)
            LoadDatabase()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            LoadDatabase()
        Finally
            JRO = Nothing
        End Try

    End Sub

    Private Sub ДобавитьКаталогToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles addFolder.Click
        Call ADD_DIR()
    End Sub

    Private Sub ОбновитьДанныеToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateList.Click

        Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

    End Sub

    Private Sub ОчиститьБазуДанныхToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearDB.Click
        Call All_Delete()
    End Sub

    Private Sub ВыходToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Exitmnu.Click

        If MsgBox("Работа программы будет завершена" & vbCrLf & "Хотите продолжить?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            ThrTIMER_.Abort()
            Call COMPARE_DB()
            UnLoadDatabase()
            ni.Visible = False
            AddLogEntr("Работа программы завершена", 2)

            End

        End If

    End Sub

    Private Sub UpdateData_Click(sender As Object, e As EventArgs) Handles UpdateData.Click
        Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))
    End Sub

    Private Sub ОПрограммеToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ОПрограммеToolStripMenuItem.Click
        frmAbout.ShowDialog(Me)
    End Sub

    Private Sub ИгнорируемыеТипыФайловToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IgnoreFile.Click
        frmAdd_type.ShowDialog(Me)
    End Sub

    'Public Sub saveExcelFile(ByVal FileName As String)
    '    Dim xls As New Excel.Application
    '    Dim sheet As Excel.Worksheet
    '    Dim i As Integer
    '    xls.Workbooks.Add()
    '    sheet = xls.ActiveWorkbook.ActiveSheet
    '    Dim row As Integer = 1
    '    Dim col As Integer = 1
    '    For Each item As ListViewItem In lvFiles.Items
    '        For i = 0 To item.SubItems.Count - 1
    '            sheet.Cells(row, col) = item.SubItems(i).Text
    '            col = col + 1
    '        Next
    '        row += 1
    '        col = 1
    '    Next
    '    xls.Cells.EntireColumn.AutoFit()
    '    xls.ActiveWorkbook.SaveAs(FileName)
    '    xls.Workbooks.Close()
    '    xls.Quit()
    'End Sub

    Private Sub СохранитьСписокВФайлToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveList.Click

        If lvFiles.Items.Count = 0 Then

            MsgBox("Нечего сохранять, данных нет", MsgBoxStyle.Exclamation, Application.ProductName)
            Exit Sub

        End If

        Me.Cursor = Cursors.WaitCursor

        Dim saveFileDialog1 As New SaveFileDialog
        ' saveFileDialog1.Filter = "Excel File|*.xlsx"
        ' saveFileDialog1.Title = "Сохранить в формате Excel"

        ' saveFileDialog1.Filter = "CSV File|*.csv"

        saveFileDialog1.Filter = "CSV File|*.csv|HTML files (.html)|*.html|Text Files (.txt)|*.txt"
        saveFileDialog1.DefaultExt = "csv"
        saveFileDialog1.ValidateNames = True

        saveFileDialog1.Title = "Сохранить в файл"

        saveFileDialog1.ShowDialog()
        If saveFileDialog1.FileName <> "" Then
            'saveExcelFile(saveFileDialog1.FileName)

            Select Case saveFileDialog1.FilterIndex

                Case 1
                    WriteListViewToCSVFile(saveFileDialog1.FileName)
                Case 2

                    wbrTable.DocumentText = ListViewToHtmlTable(lvFiles, 1, 1, 2)

                    Do Until wbrTable.ReadyState = WebBrowserReadyState.Complete
                        Application.DoEvents()
                        System.Threading.Thread.Sleep(100)
                    Loop

                    My.Computer.FileSystem.WriteAllText(saveFileDialog1.FileName, wbrTable.DocumentText, False)

                    wbrTable.Navigate("about:blank")
                    Do Until wbrTable.ReadyState = WebBrowserReadyState.Complete
                        Application.DoEvents()
                        System.Threading.Thread.Sleep(100)
                    Loop

                Case 3
                    Dim Write As New IO.StreamWriter(saveFileDialog1.FileName)
                    Dim k As ListView.ColumnHeaderCollection = lvFiles.Columns
                    For Each x As ListViewItem In lvFiles.Items
                        Dim StrLn As String = ""
                        For i = 0 To x.SubItems.Count - 1
                            StrLn += k(i).Text + " :" + x.SubItems(i).Text + Space(3)
                        Next
                        Write.WriteLine(StrLn)
                    Next
                    Write.Close()

            End Select

        Else
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Me.Cursor = Cursors.Default
        AddLogEntr("Данные сохранены в файл: " & saveFileDialog1.FileName, 2)

        MessageBox.Show("Файл сохранен!")

    End Sub

    Sub WriteListViewToCSVFile(ByVal FileName As String)
        Dim CSVWriter As New StreamWriter(FileName)
        For i As Integer = 0 To Me.lvFiles.Items.Count - 1
            For j As Integer = 0 To Me.lvFiles.Columns.Count - 1
                'CSVWriter.Write(Me.lvFiles.Items(i).SubItems(j).Text)
                CSVWriter.Write(Me.lvFiles.Items(i).SubItems(j).Text & System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator)
            Next
            CSVWriter.WriteLine()
        Next
        CSVWriter.Close()
    End Sub

    Private Sub НастройкиToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetupPrg.Click
        frmChS.ShowDialog(Me)
    End Sub

    Private Sub MainForm_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged

        If Me.WindowState = FormWindowState.Minimized Then
            Me.WindowState = FormWindowState.Minimized
            Me.Visible = False
            Me.ni.Visible = True
        End If

    End Sub

    Private Sub ВыходToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ВыходToolStripMenuItem1.Click

        If MsgBox("Работа программы будет завершена" & vbCrLf & "Хотите продолжить?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            Call COMPARE_DB()

            UnLoadDatabase()

            ni.Visible = False

            AddLogEntr("Работа программы завершена", 2)

            End

        End If

    End Sub

    Private Sub РазвернутьToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles РазвернутьToolStripMenuItem.Click
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal

    End Sub

    Private Sub НастройкиToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles НастройкиToolStripMenuItem1.Click
        frmChS.ShowDialog(Me)
    End Sub

    Private Sub ni_DoubleClick(sender As Object, e As EventArgs) Handles ni.DoubleClick
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
    End Sub

    Sub ThrTIMER()
        Dim SDA As DateTime

        Do

            SDA = Date.Today.AddDays(-7)

            Select Case TimeOfDay.Hour

                Case 0

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

                            End Select
                    End Select

                Case 3

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

                            End Select
                    End Select

                Case 6

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

                            End Select
                    End Select

                Case 9

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

                            End Select
                    End Select

                Case 12

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

                            End Select
                    End Select

                Case 15

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

                            End Select
                    End Select

                Case 18

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

                            End Select
                    End Select

                Case 21

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

                            End Select
                    End Select

            End Select

            Threading.Thread.Sleep(1000)

        Loop

    End Sub

    Private Sub stat2_Click(sender As Object, e As EventArgs) Handles stat2.Click

        Select Case stat2.Text

            Case "Изменений в файлах не зафиксировано"

            Case Else

                lvForm = "exCS"

                Dim intcount As Integer = 0

                frmExFile.lvFilesR.Sorting = SortOrder.None
                frmExFile.lvFilesR.ListViewItemSorter = Nothing
                frmExFile.lvFilesR.Items.Clear()

                For Each itm As ListViewItem In lvFilesR.Items

                    frmExFile.lvFilesR.Items.Add(itm.Clone())

                    frmExFile.lvFilesR.Items(CInt(intcount)).ForeColor = Color.Cyan
                    frmExFile.lvFilesR.Items(CInt(intcount)).BackColor = Color.Red

                    intcount = intcount + 1
                Next

                frmExFile.Text = "Измененные файлы"

                frmExFile.ShowDialog(Me)

        End Select


    End Sub

    Private Sub notfind_Click(sender As Object, e As EventArgs) Handles notfind.Click

        Select Case notfind.Text

            Case "Все файлы в наличии"


            Case Else

                frmExFile.lvFilesR.Sorting = SortOrder.None
                frmExFile.lvFilesR.ListViewItemSorter = Nothing
                frmExFile.lvFilesR.Items.Clear()

                lvForm = "exFF"

                Dim intcount As Integer = 0

                For Each itm As ListViewItem In lvFilesF.Items

                    frmExFile.lvFilesR.Items.Add(itm.Clone())

                    frmExFile.lvFilesR.Items(CInt(intcount)).ForeColor = Color.Yellow
                    frmExFile.lvFilesR.Items(CInt(intcount)).BackColor = Color.SlateGray

                    intcount = intcount + 1
                Next

                frmExFile.Text = "Не найденные файлы"

                frmExFile.ShowDialog(Me)

        End Select

    End Sub

    Private Sub addFile_Click(sender As Object, e As EventArgs) Handles addFile.Click

        Call FilesAdd()

    End Sub

    Private Sub FilesAdd()
        Dim fdlg As OpenFileDialog = New OpenFileDialog()

        fdlg.Title = "Выбор файла"
        fdlg.InitialDirectory = Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString
        fdlg.Filter = "Все файлы (*.*)|*.*"
        fdlg.FilterIndex = 2

        fdlg.RestoreDirectory = True

        If fdlg.ShowDialog() = DialogResult.OK Then
            Me.Cursor = Cursors.WaitCursor

            Dim thisHash As String

            Select Case typeCRC

                Case "MD5"
                    thisHash = GetHash(fdlg.FileName)

                Case "Crc32"

                    thisHash = GetCRC32(fdlg.FileName)

                Case "SHA256"

                    thisHash = GetSHA256(fdlg.FileName)

                Case "SHA512"

                    thisHash = GetSHA512(fdlg.FileName)

                Case "SHA1"

                    thisHash = GetSHA1(fdlg.FileName)

                Case Else

                    MsgBox("Не выбран алгоритм вычисления контрольной суммы", MsgBoxStyle.Critical)

                    frmChS.ShowDialog(Me)

            End Select

            If Not RSExistsHash(fdlg.FileName) Then
                ADD_DB_HASH(fdlg.FileName, thisHash, DateAndTime.Now)

                AddLogEntr("Добавлен новый файл: " & fdlg.FileName & vbCrLf & "Расчитанная контрольная сумма: " & thisHash, 2)

            End If

            Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

        Else

        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub findDouble_Click(sender As Object, e As EventArgs) Handles findDouble.Click
        frmDouble.ShowDialog(Me)
    End Sub

    Private Sub ДобавитьФайлToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ДобавитьФайлToolStripMenuItem.Click

        Call FilesAdd()

    End Sub

    Private Sub ПоискДубликатаToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ПоискДубликатаToolStripMenuItem.Click

        Dim rCOUNT As Integer
        Dim intj As Integer

        For z = 0 To lvFilesR.SelectedItems.Count - 1
            rCOUNT = (lvFilesR.SelectedItems(z).Text)
            intj = z
        Next

        frmDouble.stmp_hash = lvFiles.SelectedItems(intj).SubItems(2).Text

        frmDouble_f.ShowDialog()

        frmDouble_f.Focus()

    End Sub

    Public Sub AddLogEntr(ByVal stxt As String, ByVal stmp As Integer)

        Dim myLog As New EventLog

        If Not EventLog.SourceExists("Application") Then
            EventLog.CreateEventSource(Application.ProductName, "Application")
        End If

        ' AddHandler myLog.EntryWritten, AddressOf OnEntryWritten

        With myLog
            .Source = Application.ProductName
            .Log = "Application"
            .EnableRaisingEvents = True


            Select Case stmp

                Case 1
                    .WriteEntry(stxt, EventLogEntryType.Warning)
                Case 0
                    .WriteEntry(stxt, EventLogEntryType.Error)
                Case 2
                    .WriteEntry(stxt, EventLogEntryType.Information)

            End Select

        End With

        myLog.Close()

    End Sub

    'Public Sub OnEntryWritten(ByVal source As Object, ByVal e As EntryWrittenEventArgs)
    '    Console.WriteLine(("Written: " + e.Entry.Message))
    'End Sub

End Class
