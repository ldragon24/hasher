﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDouble
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lvFilesR = New System.Windows.Forms.ListView()
        Me.SuspendLayout()
        '
        'lvFilesR
        '
        Me.lvFilesR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvFilesR.FullRowSelect = True
        Me.lvFilesR.GridLines = True
        Me.lvFilesR.Location = New System.Drawing.Point(0, 0)
        Me.lvFilesR.MultiSelect = False
        Me.lvFilesR.Name = "lvFilesR"
        Me.lvFilesR.Size = New System.Drawing.Size(844, 401)
        Me.lvFilesR.TabIndex = 5
        Me.lvFilesR.UseCompatibleStateImageBehavior = False
        Me.lvFilesR.View = System.Windows.Forms.View.Details
        '
        'frmDouble
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 401)
        Me.Controls.Add(Me.lvFilesR)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmDouble"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Дубликаты файлов (Контрольная сумма)"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvFilesR As System.Windows.Forms.ListView
End Class
