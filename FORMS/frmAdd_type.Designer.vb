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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmBmenu.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtType
        '
        Me.txtType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtType.Location = New System.Drawing.Point(3, 3)
        Me.txtType.Name = "txtType"
        Me.txtType.Size = New System.Drawing.Size(341, 20)
        Me.txtType.TabIndex = 1
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.AutoSize = True
        Me.btnAdd.Location = New System.Drawing.Point(350, 3)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(84, 23)
        Me.btnAdd.TabIndex = 2
        Me.btnAdd.Text = "&Добавить"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lvFiles
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.lvFiles, 2)
        Me.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvFiles.FullRowSelect = True
        Me.lvFiles.GridLines = True
        Me.lvFiles.Location = New System.Drawing.Point(3, 32)
        Me.lvFiles.MultiSelect = False
        Me.lvFiles.Name = "lvFiles"
        Me.lvFiles.Size = New System.Drawing.Size(431, 442)
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
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.txtType, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnAdd, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lvFiles, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(437, 477)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'frmAdd_type
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(437, 477)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmAdd_type"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Игнорируемые файлы"
        Me.cmBmenu.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtType As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lvFiles As System.Windows.Forms.ListView
    Friend WithEvents cmBmenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeltoBranch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
End Class
