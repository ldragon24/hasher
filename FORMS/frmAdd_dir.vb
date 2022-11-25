Imports System.IO

Public Class frmAdd_dir
    Private MassDel As Boolean = False
    Private rCOUNT As Integer = 0
    Private sSID As Integer
    Private sTXTtmp As String

    Private Sub frmAdd_dir_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        If BasePath = "" Then

        Else

            Call MainForm.find_file()
        End If



    End Sub

    Private Sub frmAdd_dir_Load(sender As Object, e As EventArgs) Handles Me.Load

        lvFiles.Columns.Add(("id"), 20, HorizontalAlignment.Left)
        lvFiles.Columns.Add(("Каталог"), 100, HorizontalAlignment.Left)

        Dim sSQL As String
        Dim sCOUNT As Integer

        Try

            sSQL = "SELECT count(*) as t_n FROM TBL_DIR"

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

                    Call LoadData()

            End Select

        Catch ex As Exception

        End Try


    End Sub

    Private Sub LoadData()

        Dim intcount As Integer
        intcount = 0

        Dim sSQL As String
        Dim sCOUNT As Integer

        Try

            sSQL = "SELECT count(*) as t_n FROM TBL_DIR"

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
                    lvFiles.Items.Clear()

                Case Else

                    lvFiles.Items.Clear()

                    sSQL = "SELECT * FROM TBL_DIR"
                    rs = New Recordset
                    rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

                    With rs
                        .MoveFirst()
                        Do While Not .EOF


                            lvFiles.Items.Add(.Fields("id").Value)
                            BasePath = .Fields("dir").Value
                            lvFiles.Items(intcount).SubItems.Add(.Fields("dir").Value)

                            intcount = intcount + 1
                            .MoveNext()
                        Loop
                    End With
                    rs.Close()
                    rs = Nothing


            End Select

        Catch ex As Exception

        End Try

        ResList(lvFiles)

    End Sub

    Private Sub DELETE_TYPE(Optional ByVal ssid As Integer = 0)
        Dim z As Integer

        If ssid = 0 Then

            For z = 0 To lvFiles.SelectedItems.Count - 1
                rCOUNT = (lvFiles.SelectedItems(z).Text)
            Next

            ssid = rCOUNT

        End If

        Dim sSQL As String

        sSQL = "Delete * FROM TBL_DIR where id =" & ssid

        DB7.Execute(sSQL)

        'LoadData()

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

        For z = 0 To lvFiles.SelectedItems.Count - 1
            rCOUNT = (lvFiles.SelectedItems(z).Text)
        Next

        If MsgBox("Выбранные данные будут удалены" & vbCrLf & "Данные будут потеряны" & vbCrLf & "Хотите продолжить?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            If MassDel = False Then

                DELETE_TYPE(rCOUNT)

            Else

                Dim intj As Integer = 0
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

                            Call DELETE_TYPE(lvFiles.SelectedItems(intj).Text)

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

    Private Sub lvFiles_DoubleClick(sender As Object, e As EventArgs) Handles lvFiles.DoubleClick
        Try
            If lvFiles.Items.Count = 0 Then Exit Sub

            Dim z As Integer

            For z = 0 To lvFiles.SelectedItems.Count - 1
                rCOUNT = (lvFiles.SelectedItems(z).Text)
            Next

            Dim rs As Recordset
            rs = New Recordset

            rs.Open("SELECT dir FROM TBL_DIR WHERE id=" & rCOUNT, DB7, CursorTypeEnum.adOpenDynamic,
                    LockTypeEnum.adLockOptimistic)

            With rs

                If Not IsDBNull(.Fields("dir").Value) Then txtType.Text = .Fields("dir").Value

                sTXTtmp = .Fields("dir").Value

            End With

            btnAdd.Text = "Сохранить"

            rs.Close()
            rs = Nothing

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub lvFiles_Mouseup(sender As Object, e As MouseEventArgs) Handles lvFiles.MouseUp
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


        Catch ex As Exception

        End Try
    End Sub

   
    Private Sub lvFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvFiles.SelectedIndexChanged
        If lvFiles.Items.Count = 0 Then Exit Sub

        Dim z As Integer

        For z = 0 To lvFiles.SelectedItems.Count - 1
            rCOUNT = (lvFiles.SelectedItems(z).Text)
        Next
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        If Len(txtType.Text) = 0 Then

            Try

                Dim DirectoryBrowser As New FolderBrowserDialog

                With DirectoryBrowser

                    .RootFolder = Environment.SpecialFolder.Desktop

                    .SelectedPath = Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString & "\"
                    .Description = "Выбор директории для загрузки данных"

                    If .ShowDialog = DialogResult.OK Then
                        ' BasePath = .SelectedPath
                        txtType.Text = .SelectedPath
                    Else
                        Exit Sub

                    End If

                End With

            Catch ex As Exception

                MsgBox(ex.Message)

            End Try

        End If

        Try

            Dim sSQL As String

            Select Case btnAdd.Text

                Case "Сохранить"

                    sSQL = "UPDATE TBL_DIR SET dir='" & txtType.Text & "' WHERE id =" & rCOUNT
                    DB7.Execute(sSQL)

                    MainForm.AddLogEntr("Изменен каталог c : " & sTXTtmp & " на " & txtType.Text, 2)
                    sTXTtmp = ""
                    txtType.Text = ""
                    btnAdd.Text = "Добавить"

                Case Else

                    If Not RSExistsType(txtType.Text) Then

                        sSQL = "INSERT INTO [TBL_DIR]([dir]) VALUES('" & txtType.Text & "')"
                        DB7.Execute(sSQL)

                        MainForm.AddLogEntr("Добавлен каталог: " & txtType.Text, 2)

                        txtType.Text = ""
                        btnAdd.Text = "Добавить"
                    End If

            End Select

        Catch ex As Exception

        End Try

        Call LoadData()
    End Sub

    Public Function RSExistsType(ByVal sGroupName As String) As Boolean
        On Error GoTo Error_
        RSExistsType = False
        Dim sSQL As String
        Dim sCOUNT As String

        sGroupName = Replace(sGroupName, "'", " ")
        If Len(sGroupName) = 0 Then Exit Function

        sSQL = "SELECT COUNT(*) AS t_n FROM TBL_DIR WHERE dir='" & sGroupName & "'"

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
                RSExistsType = True
                Exit Function
        End Select

        RSExistsType = False

        Exit Function
Error_:
        RSExistsType = False
    End Function
End Class