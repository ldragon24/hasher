
Imports System
Imports System.IO
'Imports System.Data.OleDb
'Imports ADOX

Module MOD_DATABASE

    'Public DB8 As OleDbConnection
    Public DB7 As Connection
    'Public ConNect As String

    Public ProgName As String
    Public BP As String
    Public Base_Name As String
    Public TIME_TOOL_TIPS As Integer = 20
    Private m_SortingColumn As ColumnHeader

    Public Sub CREATE_DATABASE()

        'Dim cat As New ADOX.Catalog
        'Dim sSQL As String

        'cat.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & BP & "\" & Base_Name & ";Jet OLEDB:Engine Type=5")
        'cat = Nothing

        'sSQL = "CREATE TABLE [TBL_CONF] ([id] counter, [type] varchar(255))"

        'DB7.Execute(sSQL)

        'sSQL = "CREATE TABLE [TBL_IGNORE] ([id] counter, [type] varchar(255))"

        'DB7.Execute(sSQL)

        'sSQL = "CREATE TABLE [TBL_HASH] ([id] counter, [file] varchar(255), [hash] varchar(255),[dttm] Date)"

        'DB7.Execute(sSQL)



    End Sub

    Public Sub LoadDatabase(Optional ByRef sFile As String = "")
        On Error GoTo ERR1
        Dim MyShadowPassword As String


        MyShadowPassword = ""
        BP = Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString & "\"

        Base_Name = "db.mdb"
        sFile = Base_Name


        If IO.File.Exists(BP & "\" & sFile) Then

        Else

            MsgBox("Не найден файл базы данных" & vbCrLf & "Обратитесь к производителю", MsgBoxStyle.Critical)
            End
            'Call CREATE_DATABASE()


        End If


        'ConNect = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & BP & "\" & sFile & ";User Id=admin;Password=;"
        'DB8 = New OleDbConnection(ConNect)
        'DB8.Open()

        ' DB7.Open("Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=" & BP & "\" & sFile & ";Exclusive=1;Uid=admin;Pwd=;")

        DB7 = New Connection
        DB7.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & BP & "\" & sFile & ";Jet OLEDB:Database Password=" & MyShadowPassword & ";")

        Exit Sub
ERR1:

        MsgBox(Err.Description)

    End Sub

    Public Sub UnLoadDatabase()

        DB7.Close()
        DB7 = Nothing

    End Sub

    Public Function RSExistsHash(ByVal sGroupName As String) As Boolean
        On Error GoTo Error_
        RSExistsHash = False
        Dim sSQL As String
        Dim sCOUNT As String

        'sGroupName = Replace(sGroupName, "'", " ")
        If Len(sGroupName) = 0 Then Exit Function

        sSQL = "SELECT COUNT(*) AS t_n FROM TBL_HASH WHERE file='" & sGroupName & "'"

        'Dim cmd As OleDbCommand
        'Dim rs As OleDbDataReader

        'cmd = New OleDbCommand(sSQL, DB8)
        'rs = cmd.ExecuteReader
        'While rs.Read

        '    sCOUNT = rs.Item("total_number")

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

            Case Else
                RSExistsHash = True
                Exit Function
        End Select

        rs.Close()
        rs = Nothing

        RSExistsHash = False

        Exit Function
Error_:
        RSExistsHash = False
    End Function

    Public Function RsExistTXT(ByVal sGroupName As String, ByVal sHash As String, ByVal stmp As String) As Boolean
        RsExistTXT = False
        Dim d() As String

        If IO.File.Exists(Application.StartupPath & "\change.log") Then

        Else
            Exit Function
        End If

        Try
            Dim stxt As String
            Dim Txt() As String = IO.File.ReadAllLines(Application.StartupPath & "\change.log", System.Text.Encoding.Default)

            For x = 0 To Txt.Length - 1

                d = Split(Txt(x), "|")

                If Len(d(2)) = 0 Then

                Else

                    Select Case stmp

                        Case "zfile"

                            stxt = "Обнаружены изменения в файле: "

                        Case "efile"

                            stxt = "Не найден файл: "
                    End Select


                    If d(2) = sGroupName And d(1) = stxt And d(4) = sHash Then

                        RsExistTXT = True
                        Exit Function


                    End If

                End If

            Next

        Catch ex As Exception

            MsgBox(ex.Message)
            RsExistTXT = False
        End Try

        RsExistTXT = False

    End Function

    Public Sub SORTING_LV(ByVal LV As ListView, e As System.Windows.Forms.ColumnClickEventArgs)

        Dim new_sorting_column As ColumnHeader =
              LV.Columns(e.Column)
        Dim sort_order As SortOrder
        If m_SortingColumn Is Nothing Then
            sort_order = SortOrder.Ascending
        Else
            If new_sorting_column.Equals(m_SortingColumn) Then
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                sort_order = SortOrder.Ascending
            End If

            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        LV.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        LV.Sort()

    End Sub

    Public Sub ResList(ByVal resizingListView As ListView)

        Dim columnIndex As Integer

        For columnIndex = 1 To resizingListView.Columns.Count - 1
            resizingListView.AutoResizeColumn(columnIndex, ColumnHeaderAutoResizeStyle.HeaderSize)
        Next

    End Sub

End Module
