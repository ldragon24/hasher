Public Class frmExFile





    Private Sub frmExFile_Load(sender As Object, e As EventArgs) Handles Me.Load

        lvFilesR.Columns.Clear()

        Select Case MainForm.lvForm
            Case "exCS"

                lvFilesR.Columns.Add(("id"), 20, HorizontalAlignment.Left)
                lvFilesR.Columns.Add(("Имя файла"), 100, HorizontalAlignment.Left)
                lvFilesR.Columns.Add(("Контрольная сумма"), 100, HorizontalAlignment.Left)
                lvFilesR.Columns.Add(("Расчитанная Контрольная сумма"), 30, HorizontalAlignment.Left)

            Case "exFF"

                lvFilesR.Columns.Add(("id"), 20, HorizontalAlignment.Left)
                lvFilesR.Columns.Add(("Имя файла"), 100, HorizontalAlignment.Left)
                lvFilesR.Columns.Add(("Контрольная сумма"), 100, HorizontalAlignment.Left)

        End Select

        ResList(lvFilesR)

    End Sub
End Class