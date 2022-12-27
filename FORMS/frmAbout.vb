Imports System.IO

Public NotInheritable Class frmAbout
    Private Sub frmAbout_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        ' Установить заголовок формы.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.ProductName
        Else
            ApplicationTitle = Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("О программе {0}", ApplicationTitle)
        ' Инициализировать текст, отображаемый в окне "О программе".
        ' TODO: настроить сведения о сборке приложения в области "Приложение" диалогового окна 
        '    свойств проекта (в меню "Проект").

        Me.LabelProductName.Text = My.Application.Info.ProductName

        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = "Арзамасское ЛПУМГ"

        Dim sStr As String
        Dim sStr2 As String

        Me.LabelVersion.Text = String.Format("Версия {0}", My.Application.Info.Version.ToString)
        sStr = "Эта программа распространяется в расчете на то, что она окажется полезной, но БЕЗ КАКИХ-ЛИБО ГАРАНТИЙ, включая подразумеваемую гарантию КАЧЕСТВА либо ПРИГОДНОСТИ ДЛЯ ОПРЕДЕЛЕННЫХ ЦЕЛЕЙ. " & vbNewLine & vbNewLine &
         "Программа распространяется на условиях лицензии GNU GPL v2."

        sStr2 =
            "Предназначена для контрольного суммирования, проведения инспекционного контроля, контроля целостности и отслеживания изменений версий программных продуктов." & vbCrLf & vbCrLf & "Что может прогамма:" & vbCrLf & vbCrLf & "- подсчет контрольных сумм папок и файлов по 15 алгоритмам;" & vbCrLf & "- создание отчёта контрольного суммирования в файлах форматов .html, .csv и .txt;" & vbCrLf & "- определение количества файлов с одинаковыми контрольными суммами;" & vbCrLf & "- поиск новых файлов в каталогах."

        Me.TextBoxDescription.Text = sStr & vbCrLf & vbCrLf & "Краткое описание:" & vbCrLf & vbCrLf & sStr2

    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

    Private Sub TextBoxDescription_TextChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles TextBoxDescription.TextChanged
    End Sub
End Class
