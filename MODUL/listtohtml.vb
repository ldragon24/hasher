Module listtohtml

    Public Function ListViewToHtmlTable(ByVal lvw As ListView, ByVal border As Integer, ByVal cell_spacing As Integer, ByVal cell_padding As Integer) As String
        ' Open the <table> element.
        Dim txt As String = "<table " & _
            "border=""" & border.ToString() & """ " & _
            "cellspacing=""" & cell_spacing.ToString() & """ " & _
            "cellpadding=""" & cell_padding.ToString() & """>" & vbCrLf

        ' See how many columns there are.
        Dim num_cols As Integer = lvw.Columns.Count

        ' See if there are any non-grouped items.
        Dim have_non_grouped_items As Boolean = False
        For Each item As ListViewItem In lvw.Items
            If (item.Group Is Nothing) Then
                have_non_grouped_items = True
                Exit For
            End If
        Next item

        ' Display non-grouped items.
        If (have_non_grouped_items) Then
            ' Display the column headers.
            txt &= ListViewColumnHeaderHtml(lvw)

            ' Display the non-grouped items.
            For Each item As ListViewItem In lvw.Items
                If (item.Group Is Nothing) Then
                    ' Display this item.
                    txt &= ListViewItemHtml(item)
                End If
            Next item
        End If

        ' Process the groups.
        For Each grp As ListViewGroup In lvw.Groups
            ' Display the header.
            txt &= "  <tr><th " & _
                "colspan=""" & num_cols & """ " & _
                "align=""" & grp.HeaderAlignment.ToString() & """ " & _
                "bgcolor=""LightBlue"">" & _
                grp.Header & "</th></tr>" & vbCrLf

            ' Display the column headers.
            txt &= ListViewColumnHeaderHtml(lvw)

            ' Display the items in the group.
            For Each item As ListViewItem In grp.Items
                txt &= ListViewItemHtml(item)
            Next item
        Next grp

        txt &= "</table>" & vbCrLf
        Return txt
    End Function

    ' Return a string representing ListView column headers.
    Private Function ListViewColumnHeaderHtml(ByVal lvw As ListView) As String
        ' Display the column headers.
        Dim txt As String = "  <tr>"
        For Each col As ColumnHeader In lvw.Columns
            ' Display this column header.
            txt &= "<th bgcolor=""#CCFFFF""" & _
                "width=""" & col.Width.ToString() & """ " & _
                "align=""" & col.TextAlign.ToString() & """>" & _
                col.Text & "</th>"
        Next col

        txt &= "</tr>" & vbCrLf
        Return txt
    End Function

    ' Return the HTML text representing this item's row.
    Private Function ListViewItemHtml(ByVal item As ListViewItem) As String
        Dim txt As String = "  <tr>"
        Dim lvw As ListView = item.ListView
        For i As Integer = 0 To item.SubItems.Count - 1
            txt &= "<td " & _
                "align=""" & lvw.Columns(i).TextAlign.ToString() + """>" & _
                item.SubItems(i).Text & "</td>"
        Next i

        txt &= "</tr>" & vbCrLf
        Return txt
    End Function
End Module
