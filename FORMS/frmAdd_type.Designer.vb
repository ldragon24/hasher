<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdd_type
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
        Me.components = New System.ComponentModel.Container()
        Me.txtType = New System.Windows.Forms.TextBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lvFiles = New System.Windows.Forms.ListView()
        Me.cmBmenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeltoBranch = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmBmenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtType
        '
        Me.txtType.Location = New System.Drawing.Point(4, 12)
        Me.txtType.Name = "txtType"
        Me.txtType.Size = New System.Drawing.Size(333, 20)
        Me.txtType.TabIndex = 1
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(343, 13)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 2
        Me.btnAdd.Text = "&Добавить"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lvFiles
        '
        Me.lvFiles.FullRowSelect = True
        Me.lvFiles.GridLines = True
        Me.lvFiles.Location = New System.Drawing.Point(4, 42)
        Me.lvFiles.MultiSelect = False
        Me.lvFiles.Name = "lvFiles"
        Me.lvFiles.Size = New System.Drawing.Size(410, 433)
        Me.lvFiles.TabIndex = 3
        Me.lvFiles.UseCompatibleStateImageBehavior = False
        Me.lvFiles.View = System.Windows.Forms.View.Details
        '
        'cmBmenu
        '
        Me.cmBmenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeltoBranch, Me.ToolStripMenuItem1})
        Me.cmBmenu.Name = "cmMENU"
        Me.cmBmenu.Size = New System.Drawing.Size(184, 48)
        '
        'mnuDeltoBranch
        '
        Me.mnuDeltoBranch.Image = Global.HASHER.My.Resources.Resources.remove
        Me.mnuDeltoBranch.Name = "mnuDeltoBranch"
        Me.mnuDeltoBranch.Size = New System.Drawing.Size(183, 22)
        Me.mnuDeltoBranch.Text = "Удалить"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.HASHER.My.Resources.Resources.delete
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(183, 22)
        Me.ToolStripMenuItem1.Text = "Массовое удаление"
        '
        'frmAdd_type
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(418, 485)
        Me.Controls.Add(Me.lvFiles)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtType)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmAdd_type"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Добавить расширение"
        Me.cmBmenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtType As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lvFiles As System.Windows.Forms.ListView
    Friend WithEvents cmBmenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeltoBranch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
End Class
