﻿Imports System.IO
Imports System.Threading
Imports System.Collections
Imports System.Management
Imports System.ComponentModel
Imports System.Text
Imports Ionic.Zip
Imports GostPlugin.Magma

Public Class MainForm

    Private MassDel As Boolean = False
    Private dvCOUNT As Integer
    Public nudH As Integer
    Public nudM As Integer
    Public nudS As Integer

    Private rCOUNT As Integer
    Private Const CP_NOCLOSE_BUTTON As Integer = &H200
    Public typeCRC As String
    Public LOG_EVT As Boolean = False

    Public ThrTIMER_ As System.Threading.Thread
    Public ThrTIMER_evt As System.Threading.Thread

    Public lvForm As String

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams

        Get
            Dim myCP As CreateParams = MyBase.CreateParams
            myCP.ClassStyle = myCP.ClassStyle Or CP_NOCLOSE_BUTTON

            Return myCP
        End Get

    End Property

    Private Sub typeCRCLoad()

        Try

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
                        LOG_EVT = .Fields("EVT").Value
                        NEWFILES = .Fields("NEWFILES").Value

                        nudH = .Fields("nudH").Value
                        nudM = .Fields("nudM").Value
                        nudS = .Fields("nudS").Value

                    End With
                    rs.Close()
                    rs = Nothing

                Case Else

                    sSQL = "DELETE * FROM TBL_CONF"
                    DB7.Execute(sSQL)

                    sSQL = "INSERT INTO [TBL_CONF]([type],[vers],[EVT],[NEWFILES]) VALUES('MD5','2','0','0')"
                    DB7.Execute(sSQL)

                    Call typeCRCLoad()
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Sub MainForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        ThrTIMER_.Abort()
        ThrTIMER_evt.Abort()
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
        lvFiles.Columns.Add(("Соответствие эталону"), 30, HorizontalAlignment.Left)
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
        ' Application.DoEvents()

        ThrTIMER_ = New System.Threading.Thread(AddressOf ThrTIMER)
        ThrTIMER_.Start()

        ThrTIMER_evt = New System.Threading.Thread(AddressOf ThrTIMER_2)
        ThrTIMER_evt.Start()

        'Application.DoEvents()
        'Me.Show()
        'Application.DoEvents()

        'Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))
        ' Me.BeginInvoke(New MethodInvoker(AddressOf Find_New_Files))

        Select Case NEWFILES
            Case 1
                Me.BeginInvoke(New MethodInvoker(AddressOf Find_New_Files))
            Case Else
                Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))
        End Select

    End Sub

    Public Sub LoadData()

        Me.Cursor = Cursors.WaitCursor

        Try

            Me.Text = "Проверка контрольной суммы файлов" & " - загрузка данных"

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
                    pb1.Visible = False

                Case Else

                    stlabel.Text = "Записей в базе: " & sCOUNT
                    UpdateList.Enabled = True
                    ClearDB.Enabled = True
                    SaveList.Enabled = True
                    findDouble.Enabled = True
                    pb1.Visible = True
                    pb1.Maximum = sCOUNT + 1
                    pb1.Minimum = 0
                    Call find_file_re()
            End Select


            Call MeText()

        Catch ex As Exception
            Call MeText()
        End Try

        ResList(lvFiles)

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub STAT_INF()

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
                pb1.Visible = False
                stat2.Text = "Изменений в файлах не зафиксировано"
                notfind.Text = "Все файлы в наличии"
                stat2.ForeColor = Color.Green
                notfind.ForeColor = Color.Green

            Case Else

                stlabel.Text = "Записей в базе: " & sCOUNT
                UpdateList.Enabled = True
                ClearDB.Enabled = True
                SaveList.Enabled = True
                findDouble.Enabled = True
                stat2.Text = "Изменений в файлах не зафиксировано"
                notfind.Text = "Все файлы в наличии"
                stat2.ForeColor = Color.Green
                notfind.ForeColor = Color.Green
        End Select


    End Sub

    Public Sub MeText()

        Select Case typeCRC
            Case "MD5"
                Me.Text = "Проверка контрольной суммы файлов (MD5)"
            Case "Crc32"
                Me.Text = "Проверка контрольной суммы файлов (CRC-32)"
            Case "SHA256"
                Me.Text = "Проверка контрольной суммы файлов (SHA-256)"
            Case "SHA512"
                Me.Text = "Проверка контрольной суммы файлов (SHA-512)"
            Case "SHA1"
                Me.Text = "Проверка контрольной суммы файлов (SHA-1)"
            Case "Sha384"
                Me.Text = "Проверка контрольной суммы файлов (SHA-384)"
            Case "Crc64"
                Me.Text = "Проверка контрольной суммы файлов (CRC-64)"
            Case "Adler32"
                Me.Text = "Проверка контрольной суммы файлов (ADLER-32)"
            Case "ripmd160"
                Me.Text = "Проверка контрольной суммы файлов (RIPEMD-160)"
            Case "GOST"
                Me.Text = "Проверка контрольной суммы файлов (GOST)"

            Case "HMACSHA256"

                Me.Text = "Проверка контрольной суммы файлов (HMAC-SHA256)"

            Case "HMACMD5"

                Me.Text = "Проверка контрольной суммы файлов (HMAC-MD5)"

            Case "HMACRIPEMD"

                Me.Text = "Проверка контрольной суммы файлов (HMAC-RIPEMD160)"

            Case "HMACSHA1"

                Me.Text = "Проверка контрольной суммы файлов (HMAC-SHA1)"

            Case "HMACSHA384"

                Me.Text = "Проверка контрольной суммы файлов (HMAC-SHA384)"

            Case "HMACSHA512"

                Me.Text = "Проверка контрольной суммы файлов (HMAC-SHA512)"

            Case Else
                Me.Text = "Проверка контрольной суммы файлов"
        End Select

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

            If Not RSExistsDir(BasePath) Then
                AddLogEntr("Добавлен новый каталог: " & BasePath, 2)
                DB7.Execute("INSERT INTO [TBL_DIR]([dir]) VALUES('" & BasePath & "')")
                Call find_file()
            End If

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Call ADD_DIR()

    End Sub

    Public Function File_ignore(ByVal sfile As String) As Boolean

        If Len(sfile) = 0 Then Exit Function

        Dim sSQL As String
        Dim sCOUNT As Integer

        Try

            Dim d() As String
            d = Split(sfile, ".")

            Dim FileExt As String = FileIO.FileSystem.GetFileInfo(sfile).Extension
            d = Split(FileExt, ".")

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

                    File_ignore = False

                Case Else

                    File_ignore = True

            End Select

        Catch ex As Exception

            File_ignore = False

        End Try

    End Function

    Private Sub ADD_DB_HASH(ByVal A1 As String, A2 As String, A3 As String)

        Dim sSQL As String

        If Not File_ignore(A1) Then

            sSQL = "INSERT INTO [TBL_HASH]([file],[hash],[dttm]) VALUES('" & A1 & "','" & A2 & "','" & A3 & "')"

            Try

                DB7.Execute(sSQL)

            Catch ex As Exception
                MsgBox(ex.Message)
            Finally

            End Try

        End If


    End Sub

    Public Sub find_file()

        lvFiles.Sorting = SortOrder.None
        lvFiles.ListViewItemSorter = Nothing
        Dim intcount As Integer
        Dim thisHash As String
        Dim dirs() As String = System.IO.Directory.GetFiles(BasePath, "*.*", SearchOption.AllDirectories)
        Dim dir As String

        If lvFiles.Items.Count <> 0 Then
            intcount = lvFiles.Items.Count
            pb1.Maximum = dirs.Length + lvFiles.Items.Count
        Else
            pb1.Maximum = dirs.Length
            intcount = 0
            lvFiles.Items.Clear()

        End If

        pb1.Visible = True


        For Each dir In dirs

            Dim FileName As String = Strings.Left(FileIO.FileSystem.GetName(dir), 1)

            If FileName = "~" Then

                Debug.Print(dir)

            Else

                If Not File_ignore(dir) Then

                    lvFiles.Items.Add(lvFiles.Items.Count + 1)
                    lvFiles.Items(intcount).SubItems.Add(dir)

                    thisHash = GettHash(dir)

                    lvFiles.Items(intcount).SubItems.Add(thisHash)
                    lvFiles.Items(intcount).SubItems.Add("Добавлено")
                    lvFiles.Items(intcount).SubItems.Add(DateAndTime.Now)
                    Dim sSQL As String
                    Dim sCOUNT As Integer

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
                            ADD_DB_HASH(dir, thisHash, DateAndTime.Now)
                        Case Else

                            If Not RSExistsHash(dir) Then
                                ADD_DB_HASH(dir, thisHash, DateAndTime.Now)
                            End If

                    End Select

                    intcount = intcount + 1
                    pb1.Value = intcount
                End If

            End If

        Next

        pb1.Visible = False
        ResList(lvFiles)
        'End Try

        Me.BeginInvoke(New MethodInvoker(AddressOf STAT_INF))
        ' Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

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
            lvFiles.Visible = False
            Dim sSQL As String

            sSQL = "SELECT * FROM TBL_HASH"

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


                    Dim thisHash As String


                    If IO.File.Exists(.Fields("file").Value) Then


                        thisHash = GettHash(.Fields("file").Value)

                        If UCase(.Fields("hash").Value) = thisHash Then

                            lvFiles.Items(intcount).SubItems.Add("Соответствует")

                        Else
                            sChange = True

                            lvFilesR.Items.Add(.Fields("id").Value)
                            lvFilesR.Items(intj).SubItems.Add(.Fields("file").Value)
                            lvFilesR.Items(intj).SubItems.Add(.Fields("hash").Value)
                            lvFilesR.Items(intj).SubItems.Add(thisHash)

                            intj = intj + 1
                            'Если хэш не соответствует то

                            lvFiles.Items(intcount).SubItems.Add("Не соответствует")

                            lvFiles.Items(CInt(intcount)).ForeColor = Color.Cyan
                            lvFiles.Items(CInt(intcount)).BackColor = Color.Red


                            If Not RsExistTXT(.Fields("file").Value, .Fields("hash").Value, "zfile") Then
                                Dim d = Now
                                IO.File.AppendAllText(Application.StartupPath & "\change.log", d & "|" & "Обнаружены изменения в файле: " & "|" & .Fields("file").Value & "|" & " Записанная контрольная сумма: " & "|" & .Fields("hash").Value & "|" & " не соответствует текущей: " & "|" & thisHash & vbNewLine, System.Text.Encoding.Default)

                                AddLogEntr("Обнаружены изменения в файле: " & .Fields("file").Value & vbCrLf & " Записанная контрольная сумма: " & .Fields("hash").Value & " не соответствует текущей: " & thisHash, 1)

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

                    'lvFiles.Items(CInt(intcount)).Selected = True
                    'lvFiles.Items(CInt(intcount)).EnsureVisible()

                    ' Dim f As New IO.FileInfo(.Fields("file").Value)
                    ' lvFiles.Items(CInt(intcount)).SubItems.Add(f.Length)

                    intcount = intcount + 1
                    pb1.Value = intcount
                    'Application.DoEvents()

                    .MoveNext()
                Loop
            End With
            rs.Close()
            rs = Nothing


            intcount = intcount + 1
            pb1.Value = intcount
            ' Application.DoEvents()

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

            lvFiles.Visible = True

        Catch ex As Exception
            MsgBox(ex.Message)
            lvFiles.Visible = True
            ResList(lvFiles)
        End Try

        Select Case sChange

            Case True

                ni.ShowBalloonTip(TIME_TOOL_TIPS * 1000, "Проверка контрольной суммы", sMessage, ToolTipIcon.Warning)

            Case Else

        End Select

        pb1.Visible = False

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

                Case "Не соответствует"
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

        Try
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

                    For i = lvFiles.Items.Count - 1 To 0 Step -1
                        If lvFiles.Items(i).Selected Then
                            lvFiles.Items(i).Remove()
                        End If
                    Next

                Else

                    'intj = 0
                    'Dim intj1 As Integer = 0

                    'lvFiles.Select()

                    'For intj = 0 To lvFiles.Items.Count - 1

                    '    lvFiles.Items(intj).Selected = True
                    '    lvFiles.Items(intj).EnsureVisible()

                    '    Select Case lvFiles.Items(intj).Checked
                    '        Case True
                    '            intj1 = intj1 + 1
                    '        Case Else
                    '    End Select

                    'Next

                    'If intj1 > 0 Then

                    '    lvFiles.Select()
                    '    intj = 0
                    '    For intj = 0 To lvFiles.Items.Count - 1

                    '        lvFiles.Items(intj).Selected = True
                    '        lvFiles.Items(intj).EnsureVisible()

                    '        Select Case lvFiles.Items(intj).Checked

                    '            Case True
                    '                Call DELETE_FILE(lvFiles.SelectedItems(intj).Text)
                    '                'lvFiles.Items.RemoveAt(intj)
                    '                AddLogEntr("Удален файл: " & lvFiles.SelectedItems(intj).SubItems(1).Text & vbCrLf & "Записанная контрольная сумма: " & lvFiles.SelectedItems(intj).SubItems(2).Text, 1)

                    '            Case Else
                    '        End Select
                    '    Next

                    '    intj = 0
                    '    For intj = 0 To lvFiles.Items.Count - 1

                    '        lvFiles.Items(intj).Selected = True
                    '        lvFiles.Items(intj).EnsureVisible()

                    '        Select Case lvFiles.Items(intj).Checked
                    '            Case True
                    '                lvFiles.Items.RemoveAt(intj)
                    '            Case Else
                    '        End Select

                    '    Next

                    'End If

                End If

                'Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

            End If

        Catch ex As Exception

        End Try

        lvFiles.CheckBoxes = False
        lvFiles.MultiSelect = False
        MassDel = False

        'Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

    End Sub

    Private Sub DELETE_FILE(Optional ByVal ssid As Integer = 0)
        Dim z As Integer

        If ssid = 0 Then

            For z = 0 To lvFiles.SelectedItems.Count - 1
                rCOUNT = (lvFiles.SelectedItems(z).Text)
            Next

            ssid = rCOUNT

        End If

        Try

            Dim sSQL As String
            sSQL = "Delete * FROM TBL_HASH where id =" & ssid

            DB7.Execute(sSQL)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub RepAddBrToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RepAddBrToolStripMenuItem.Click

        If MsgBox("Данные о файле будут перезаписаны" & vbCrLf & "Хотите продолжить?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            Dim intj As Integer

            For z = 0 To lvFiles.SelectedItems.Count - 1
                rCOUNT = (lvFiles.SelectedItems(z).Text)
                intj = z
            Next


            Dim thisHash As String
            thisHash = GettHash(lvFiles.SelectedItems(intj).SubItems(1).Text)

            Dim sSQL As String

            Try

                sSQL = "UPDATE TBL_HASH SET hash='" & thisHash & "', dttm='" & DateAndTime.Now.ToString & "' WHERE id =" & rCOUNT
                DB7.Execute(sSQL)

                AddLogEntr("Записана новая контрольная сумма для файла: " & lvFiles.SelectedItems(intj).SubItems(1).Text & vbCrLf & "Контрольная сумма: " & thisHash & vbCrLf & "Старая контрольная сумма: " & lvFiles.SelectedItems(intj).SubItems(2).Text, 1)

                lvFiles.SelectedItems(intj).SubItems(2).Text = thisHash
                lvFiles.SelectedItems(intj).SubItems(3).Text = "Соответствует"
                lvFiles.SelectedItems(intj).SubItems(4).Text = DateAndTime.Now.ToString
                lvFiles.SelectedItems(CInt(intj)).ForeColor = Color.Black
                lvFiles.SelectedItems(CInt(intj)).BackColor = Color.White


            Catch ex As Exception
                MsgBox(ex.Message)
            Finally

            End Try

            '
            ' Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

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

            Try
                Dim sSQL As String

                sSQL = "Delete * FROM TBL_HASH"
                DB7.Execute(sSQL)


                sSQL = "Delete * FROM TBL_DIR"
                DB7.Execute(sSQL)


                AddLogEntr("База данных очищена", 1)

                COMPARE_DB()

                If IO.File.Exists(Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString & "\change.log") Then

                    IO.File.Delete((Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString & "\change.log"))

                End If
            Catch ex As Exception

            End Try

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
        'Call ADD_DIR()
        Me.BeginInvoke(New MethodInvoker(AddressOf ADD_DIR))

    End Sub

    Private Sub ОбновитьДанныеToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateList.Click

        Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

    End Sub

    Private Sub ОчиститьБазуДанныхToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearDB.Click
        'Call All_Delete()
        Me.BeginInvoke(New MethodInvoker(AddressOf All_Delete))

    End Sub

    Private Sub ВыходToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Exitmnu.Click

        If MsgBox("Работа программы будет завершена" & vbCrLf & "Хотите продолжить?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            ThrTIMER_.Abort()
            ThrTIMER_evt.Abort()
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

        Do

            Select Case TimeOfDay.Hour

                Case 0

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Select Case NEWFILES
                                        Case 1
                                            Me.BeginInvoke(New MethodInvoker(AddressOf Find_New_Files))
                                        Case Else
                                            Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))
                                    End Select


                            End Select
                    End Select

                Case 3

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Select Case NEWFILES
                                        Case 1
                                            Me.BeginInvoke(New MethodInvoker(AddressOf Find_New_Files))
                                        Case Else
                                            Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))
                                    End Select

                            End Select
                    End Select

                Case 6

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Select Case NEWFILES
                                        Case 1
                                            Me.BeginInvoke(New MethodInvoker(AddressOf Find_New_Files))
                                        Case Else
                                            Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))
                                    End Select

                            End Select
                    End Select

                Case 9

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Select Case NEWFILES
                                        Case 1
                                            Me.BeginInvoke(New MethodInvoker(AddressOf Find_New_Files))
                                        Case Else
                                            Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))
                                    End Select

                            End Select
                    End Select

                Case 12

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Select Case NEWFILES
                                        Case 1
                                            Me.BeginInvoke(New MethodInvoker(AddressOf Find_New_Files))
                                        Case Else
                                            Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))
                                    End Select

                            End Select
                    End Select

                Case 15

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Select Case NEWFILES
                                        Case 1
                                            Me.BeginInvoke(New MethodInvoker(AddressOf Find_New_Files))
                                        Case Else
                                            Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))
                                    End Select

                            End Select
                    End Select

                Case 18

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Select Case NEWFILES
                                        Case 1
                                            Me.BeginInvoke(New MethodInvoker(AddressOf Find_New_Files))
                                        Case Else
                                            Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))
                                    End Select

                            End Select
                    End Select

                Case 21

                    Select Case TimeOfDay.Minute
                        Case 0
                            Select Case TimeOfDay.Second
                                Case 45

                                    Select Case NEWFILES
                                        Case 1
                                            Me.BeginInvoke(New MethodInvoker(AddressOf Find_New_Files))
                                        Case Else
                                            Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))
                                    End Select

                            End Select
                    End Select

            End Select

            Threading.Thread.Sleep(1000)

        Loop


    End Sub

    Sub ThrTIMER_2()

        'Сохранение журналов операционной системы
        Do

            Select Case TimeOfDay.Hour

                Case nudH

                    Select Case TimeOfDay.Minute
                        Case nudM
                            Select Case TimeOfDay.Second
                                Case nudS

                                    Select Case LOG_EVT

                                        Case True
                                            Me.BeginInvoke(New MethodInvoker(AddressOf Save_LOG_EVT))
                                        Case Else

                                    End Select

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

            If Not File_ignore(fdlg.FileName) Then

                Me.Cursor = Cursors.WaitCursor

                Dim thisHash As String

                thisHash = GettHash(fdlg.FileName)

                If Not RSExistsHash(fdlg.FileName) Then
                    ADD_DB_HASH(fdlg.FileName, thisHash, DateAndTime.Now)

                    AddLogEntr("Добавлен новый файл: " & fdlg.FileName & vbCrLf & "Расчитанная контрольная сумма: " & thisHash, 2)

                End If

                Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))


            Else
                MsgBox("Тип файла находится в списке игнориремых, для его добавления необхадимо его удалить из списка.", MsgBoxStyle.Exclamation, Application.ProductName)

            End If

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

    Public Sub Find_New_Files()

        Dim intcount As Integer
        Dim thisHash As String
        Dim sSQL As String
        Dim sCOUNT As Integer

        sSQL = "SELECT count(*) as t_n FROM TBL_DIR"

        Dim rs As Recordset
        rs = New Recordset
        rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

        With rs
            sCOUNT = .Fields("t_n").Value
        End With
        rs.Close()
        rs = Nothing

        If sCOUNT = 0 Then
            ' MsgBox("Не указаны каталоги для сканировоания", MsgBoxStyle.Exclamation, Application.ProductName)
            'frmAdd_dir.ShowDialog(Me)
            LoadData()
            Exit Sub
        End If

        sSQL = "SELECT * FROM TBL_DIR"

        rs = New Recordset
        rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

        With rs
            .MoveFirst()
            Do While Not .EOF

                Dim dirs() As String = Directory.GetFiles(.Fields("dir").Value, "*.*", SearchOption.AllDirectories)
                Dim dir As String

                Me.Text = "Проверка контрольной суммы файлов" & " - поиск новых файлов"
                For Each dir In dirs

                    Dim FileName As String = Strings.Left(FileIO.FileSystem.GetName(dir), 1)

                    If FileName = "~" Then

                        Debug.Print(dir)

                    Else

                        If Not File_ignore(dir) Then

                            sSQL = "SELECT count(*) as t_n FROM TBL_HASH"

                            Dim rs1 As Recordset
                            rs1 = New Recordset
                            rs1.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

                            With rs1
                                sCOUNT = .Fields("t_n").Value
                            End With
                            rs1.Close()
                            rs1 = Nothing

                            Select Case sCOUNT
                                Case 0
                                    'ADD_DB_HASH(dir, thisHash, DateAndTime.Now)
                                Case Else

                                    If Not RSExistsHash(dir) Then

                                        thisHash = GettHash(dir)

                                        ADD_DB_HASH(dir, thisHash, DateAndTime.Now)

                                        Dim d = Now
                                        IO.File.AppendAllText(Application.StartupPath & "\change.log", d & "|" & "Найден новый файл: " & "|" & dir & "|" & " Контрольная сумма: " & "|" & thisHash & vbNewLine, System.Text.Encoding.Default)
                                        '############################################

                                        AddLogEntr("Найден новый файл: " & dir & vbCrLf & " Контрольная сумма: " & thisHash, 1)

                                    End If
                            End Select
                            intcount = intcount + 1
                        End If

                    End If



                Next

                .MoveNext()
            Loop
        End With

        Me.Text = "Проверка контрольной суммы файлов" ' & " - поиск новых файлов"
        rs.Close()
        rs = Nothing

        Me.BeginInvoke(New MethodInvoker(AddressOf LoadData))

    End Sub

    Private Sub СканироватьКаталогиНаПредметНовыхФайловToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MnuScanDir.Click
        Me.BeginInvoke(New MethodInvoker(AddressOf Find_New_Files))
    End Sub

    Private Sub FindNewFiles_Click(sender As Object, e As EventArgs) Handles FindNewFiles.Click
        Me.BeginInvoke(New MethodInvoker(AddressOf Find_New_Files))
    End Sub

    Private Sub addToIgnore_Click(sender As Object, e As EventArgs) Handles addToIgnore.Click

        Dim sSQL As String
        Dim sCOUNT As Integer
        Dim intj As Integer = 0
        Dim path1 As String
        Dim d() As String
        Dim A As Integer

        Try

            For z = 0 To lvFiles.SelectedItems.Count - 1
                path1 = lvFiles.SelectedItems(intj).SubItems(1).Text
                intj = z
            Next

            d = Split(path1, ".")

            A = d.Length - 1

            sSQL = "SELECT count(*) as t_n FROM TBL_IGNORE WHERE type='" & d(A) & "'"

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

                    sSQL = "INSERT INTO [TBL_IGNORE]([type]) VALUES('" & d(A) & "')"
                    DB7.Execute(sSQL)

                Case Else

            End Select
        Catch ex As Exception

        End Try

    End Sub

    '###########################################################################################################
    'Блок для работы с системными журналами
    'Сохранение, архивация, удаление архивов старше 365 дней.
    '###########################################################################################################

    Private Sub Save_LOG_EVT()

        If IO.Directory.Exists(Directory.GetParent(Application.ExecutablePath).ToString & "\arhiv_evt") Then
        Else
            IO.Directory.CreateDirectory(Directory.GetParent(Application.ExecutablePath).ToString & "\arhiv_evt")
        End If

        Try
            Dim searcher As ManagementObjectSearcher
            Dim query As ObjectQuery
            Dim connection As ConnectionOptions
            Dim scope As ManagementScope = Nothing

            scope = New ManagementScope("\\.\root\CIMV2", connection)
            scope.Connect()

            scope.Options.EnablePrivileges = True
            scope.Options.Impersonation = ImpersonationLevel.Impersonate
            query = New ObjectQuery("Select * from Win32_NTEventLogFile") ' Where LogFileName='Application'
            searcher = New ManagementObjectSearcher(scope, query)

            For Each o As ManagementObject In searcher.Get()
                Dim inParams As ManagementBaseObject = o.GetMethodParameters("BackupEventlog")
                inParams("ArchiveFileName") = Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString & "\arhiv_evt\" & o("LogFileName") & ".evt"
                Dim outParams As ManagementBaseObject = o.InvokeMethod("BackupEventLog", inParams, Nothing)
                Debug.Write(outParams.Properties("ReturnValue").Value.ToString())

            Next

        Catch ex As Exception
            Debug.Write(ex.Message)

        End Try

        Me.BeginInvoke(New MethodInvoker(AddressOf EVT_ARHIVE))
        'Call EVT_ARHIVE()
    End Sub

    Private Sub EVT_ARHIVE()

        Dim sFIleName As String

        sFIleName = Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString & "\arhiv_evt\" & Date.Today.Day & "_" & Date.Today.Month & "_" & Date.Today.Year & ".zip"

        Dim folderName As String = Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString & "\arhiv_evt\"


        Dim dirs() As String = Directory.GetFiles(Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString & "\arhiv_evt\", "*.evt", SearchOption.TopDirectoryOnly)

        Dim dir As String

        Using zip As ZipFile = New ZipFile

            For Each dir In dirs

                zip.AddFile(dir, "")
            Next
            zip.Save(sFIleName)

        End Using

        For Each dir In dirs
            System.IO.File.Delete(dir)
        Next
        AddLogEntr("Архив системных журналов сохранен: " & sFIleName, 2)
        Me.BeginInvoke(New MethodInvoker(AddressOf ZIP_DELETE))
        'Call ZIP_DELETE()
    End Sub

    Private Sub ZIP_DELETE()

        Dim a As String
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString & "\arhiv_evt")
            a = (foundFile)

            Try

                Dim dt As DateTime = File.GetCreationTimeUtc(a)

                If (DateTime.Now.Subtract(dt).TotalDays > 365) Then
                    My.Computer.FileSystem.DeleteFile(a)
                    AddLogEntr("Из каталога с архивами системных журналов удален устаревший файл: " & a, 2)
                End If

            Catch e As Exception
                Console.WriteLine("The process failed: {0}", e.ToString())
            End Try

        Next

    End Sub


End Class
