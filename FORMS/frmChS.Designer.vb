<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChS
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.rbMD5 = New System.Windows.Forms.RadioButton()
        Me.rbCRC32 = New System.Windows.Forms.RadioButton()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.rbSHA256 = New System.Windows.Forms.RadioButton()
        Me.rbSHA1 = New System.Windows.Forms.RadioButton()
        Me.rbSHA512 = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.rbMD5, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rbCRC32, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSave, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSHA256, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSHA1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSHA512, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(329, 76)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'rbMD5
        '
        Me.rbMD5.AutoSize = True
        Me.rbMD5.Location = New System.Drawing.Point(3, 3)
        Me.rbMD5.Name = "rbMD5"
        Me.rbMD5.Size = New System.Drawing.Size(48, 17)
        Me.rbMD5.TabIndex = 0
        Me.rbMD5.TabStop = True
        Me.rbMD5.Text = "MD5"
        Me.rbMD5.UseVisualStyleBackColor = True
        '
        'rbCRC32
        '
        Me.rbCRC32.AutoSize = True
        Me.rbCRC32.Location = New System.Drawing.Point(3, 26)
        Me.rbCRC32.Name = "rbCRC32"
        Me.rbCRC32.Size = New System.Drawing.Size(59, 17)
        Me.rbCRC32.TabIndex = 1
        Me.rbCRC32.TabStop = True
        Me.rbCRC32.Text = "CRC32"
        Me.rbCRC32.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(251, 49)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Сохранить"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'rbSHA256
        '
        Me.rbSHA256.AutoSize = True
        Me.rbSHA256.Location = New System.Drawing.Point(167, 3)
        Me.rbSHA256.Name = "rbSHA256"
        Me.rbSHA256.Size = New System.Drawing.Size(68, 17)
        Me.rbSHA256.TabIndex = 2
        Me.rbSHA256.TabStop = True
        Me.rbSHA256.Text = "SHA-256"
        Me.rbSHA256.UseVisualStyleBackColor = True
        '
        'rbSHA1
        '
        Me.rbSHA1.AutoSize = True
        Me.rbSHA1.Location = New System.Drawing.Point(167, 26)
        Me.rbSHA1.Name = "rbSHA1"
        Me.rbSHA1.Size = New System.Drawing.Size(53, 17)
        Me.rbSHA1.TabIndex = 4
        Me.rbSHA1.TabStop = True
        Me.rbSHA1.Text = "SHA1"
        Me.rbSHA1.UseVisualStyleBackColor = True
        '
        'rbSHA512
        '
        Me.rbSHA512.AutoSize = True
        Me.rbSHA512.Location = New System.Drawing.Point(3, 49)
        Me.rbSHA512.Name = "rbSHA512"
        Me.rbSHA512.Size = New System.Drawing.Size(68, 17)
        Me.rbSHA512.TabIndex = 5
        Me.rbSHA512.TabStop = True
        Me.rbSHA512.Text = "SHA-512"
        Me.rbSHA512.UseVisualStyleBackColor = True
        '
        'frmChS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(329, 76)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmChS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Алгоритм вычисления контрольной суммы"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents rbMD5 As System.Windows.Forms.RadioButton
    Friend WithEvents rbCRC32 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSHA256 As System.Windows.Forms.RadioButton
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents rbSHA1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSHA512 As System.Windows.Forms.RadioButton
End Class
