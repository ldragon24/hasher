Public Class frmDouble
    Public stmp_hash As String

    Private Sub frmDouble_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        lvFilesR.Items.Clear()
        lvFilesR.Columns.Clear()
    End Sub

    Private Sub frmDouble_Load(sender As Object, e As EventArgs) Handles Me.Load

        lvFilesR.Items.Clear()
        lvFilesR.Columns.Clear()

        Dim sSQL As String
        Dim intcount As Integer
        intcount = 0

        sSQL = "SELECT hash, COUNT(hash) as tot_num FROM TBL_HASH group by hash"

        lvFilesR.Columns.Add(("id"), 20, HorizontalAlignment.Left)
        lvFilesR.Columns.Add(("Контрольная сумма"), 100, HorizontalAlignment.Left)
        lvFilesR.Columns.Add(("Кол-во"), 100, HorizontalAlignment.Left)

        Try

            Dim rs As Recordset
            rs = New Recordset
            rs.Open(sSQL, DB7, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic)

            With rs
                .MoveFirst()
                Do While Not .EOF

                    Select Case .Fields("tot_num").Value

                        Case 1

                        Case Else

                            lvFilesR.Items.Add(intcount + 1)
                            lvFilesR.Items(intcount).SubItems.Add(.Fields("Hash").Value)
                            lvFilesR.Items(intcount).SubItems.Add(.Fields("tot_num").Value)

                            intcount = intcount + 1
                    End Select

                    .MoveNext()
                Loop
            End With
            rs.Close()
            rs = Nothing

        Catch ex As Exception

        End Try

        ResList(lvFilesR)

        If lvFilesR.Items.Count = 0 Then
            MsgBox("Дубликатов не обнаружено", MsgBoxStyle.Information)
            Me.Close()
        End If

    End Sub

    Private Sub lvFilesR_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvFilesR.ColumnClick
        SORTING_LV(lvFilesR, e)
    End Sub

    Private Sub lvFilesR_DoubleClick(sender As Object, e As EventArgs) Handles lvFilesR.DoubleClick

        Dim rCOUNT As Integer
        Dim intj As Integer

        For z = 0 To lvFilesR.SelectedItems.Count - 1
            rCOUNT = (lvFilesR.SelectedItems(z).Text)
            intj = z
        Next

        stmp_hash = lvFilesR.SelectedItems(intj).SubItems(1).Text

        frmDouble_f.ShowDialog()

        frmDouble_f.Focus()

    End Sub

    
    Private Sub lvFilesR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvFilesR.SelectedIndexChanged

    End Sub
End Class