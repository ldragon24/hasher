
Imports System.IO
Public Class frmDouble_f


    Private Sub frmDouble_f_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Me.lvFilesR.Items.Clear()
        Me.lvFilesR.Columns.Clear()

    End Sub

    Private Sub frmDouble_f_Load(sender As Object, e As EventArgs) Handles Me.Load

        lvFilesR.Items.Clear()
        lvFilesR.Columns.Clear()

        lvFilesR.Columns.Add(("id"), 20, HorizontalAlignment.Left)
        lvFilesR.Columns.Add(("Имя файла"), 100, HorizontalAlignment.Left)
        lvFilesR.Columns.Add(("Контрольная сумма"), 100, HorizontalAlignment.Left)
        lvFilesR.Columns.Add(("Размер файла"), 100, HorizontalAlignment.Left)

        Call Load_data(frmDouble.stmp_hash)

    End Sub

    Private Sub Load_data(ByVal stmp As String)

        Dim sSQL As String
        Dim intcount As Integer
        intcount = 0

        Dim fileDetail As IO.FileInfo
        Dim Filename As String
        Dim strfilesize As Integer
        Dim ValueinMB As Double

        Try

            Dim sCOUNT As String
            Dim rs As Recordset
            rs = New Recordset
            sSQL = "SELECT count(*) as t_n FROM TBL_HASH where hash='" & stmp & "'"
            rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

            With rs
                sCOUNT = .Fields("t_n").Value
            End With
            rs.Close()
            rs = Nothing

            Select Case sCOUNT

                Case 1
                    MsgBox("Дубликатов не обнаружено", MsgBoxStyle.Information)
                    Me.Close()
                Case Else


            End Select

        Catch ex As Exception

        End Try

        sSQL = "SELECT * FROM TBL_HASH where hash='" & stmp & "'"

        Try

            Dim rs As Recordset
            rs = New Recordset
            rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

            With rs
                .MoveFirst()
                Do While Not .EOF

                    lvFilesR.Items.Add(.Fields("id").Value)
                    lvFilesR.Items(intcount).SubItems.Add(.Fields("file").Value)
                    lvFilesR.Items(intcount).SubItems.Add(.Fields("hash").Value)

                    Filename = Path.GetFileName(.Fields("file").Value)
                    fileDetail = My.Computer.FileSystem.GetFileInfo((.Fields("file").Value))

                    strfilesize = fileDetail.Length
                    ValueinMB = Math.Round(((strfilesize) / 1024), 2)   'Value in MB

                    lvFilesR.Items(intcount).SubItems.Add(ValueinMB & " kb")

                    intcount = intcount + 1

                    .MoveNext()
                Loop
            End With
            rs.Close()
            rs = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ResList(lvFilesR)

    End Sub

    Private Sub lvFilesR_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvFilesR.ColumnClick
        SORTING_LV(lvFilesR, e)
    End Sub

    Private Sub lvFilesR_DoubleClick(sender As Object, e As EventArgs) Handles lvFilesR.DoubleClick

        Dim rCOUNT As Integer
        Dim intj As Integer

        Try

            For z = 0 To lvFilesR.SelectedItems.Count - 1
                rCOUNT = (lvFilesR.SelectedItems(z).Text)
                intj = z
            Next

            Dim path1 As String
            path1 = Path.GetDirectoryName(lvFilesR.SelectedItems(intj).SubItems(1).Text)

            Process.Start(path1)

        Catch ex As Exception

        End Try


    End Sub


    Private Sub lvFilesR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvFilesR.SelectedIndexChanged

    End Sub
End Class