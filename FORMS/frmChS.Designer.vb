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
        Me.rbCRC64 = New System.Windows.Forms.RadioButton()
        Me.rbAdler = New System.Windows.Forms.RadioButton()
        Me.rbSHA1 = New System.Windows.Forms.RadioButton()
        Me.rbSHA256 = New System.Windows.Forms.RadioButton()
        Me.rbSHA384 = New System.Windows.Forms.RadioButton()
        Me.rbSHA512 = New System.Windows.Forms.RadioButton()
        Me.rbGOST = New System.Windows.Forms.RadioButton()
        Me.rbRipMD = New System.Windows.Forms.RadioButton()
        Me.btnDirectory = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.chkEVT = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkNF = New System.Windows.Forms.CheckBox()
        Me.nudH = New System.Windows.Forms.NumericUpDown()
        Me.nudM = New System.Windows.Forms.NumericUpDown()
        Me.nudS = New System.Windows.Forms.NumericUpDown()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.nudH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.12598!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.87402!))
        Me.TableLayoutPanel1.Controls.Add(Me.rbMD5, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rbCRC32, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.rbCRC64, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.rbAdler, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSHA1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSHA256, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSHA384, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSHA512, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.rbGOST, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.rbRipMD, 0, 5)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(254, 129)
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
        'rbCRC64
        '
        Me.rbCRC64.AutoSize = True
        Me.rbCRC64.Location = New System.Drawing.Point(3, 49)
        Me.rbCRC64.Name = "rbCRC64"
        Me.rbCRC64.Size = New System.Drawing.Size(59, 17)
        Me.rbCRC64.TabIndex = 9
        Me.rbCRC64.TabStop = True
        Me.rbCRC64.Text = "CRC64"
        Me.rbCRC64.UseVisualStyleBackColor = True
        '
        'rbAdler
        '
        Me.rbAdler.AutoSize = True
        Me.rbAdler.Location = New System.Drawing.Point(3, 72)
        Me.rbAdler.Name = "rbAdler"
        Me.rbAdler.Size = New System.Drawing.Size(61, 17)
        Me.rbAdler.TabIndex = 10
        Me.rbAdler.TabStop = True
        Me.rbAdler.Text = "Adler32"
        Me.rbAdler.UseVisualStyleBackColor = True
        '
        'rbSHA1
        '
        Me.rbSHA1.AutoSize = True
        Me.rbSHA1.Location = New System.Drawing.Point(109, 3)
        Me.rbSHA1.Name = "rbSHA1"
        Me.rbSHA1.Size = New System.Drawing.Size(53, 17)
        Me.rbSHA1.TabIndex = 4
        Me.rbSHA1.TabStop = True
        Me.rbSHA1.Text = "SHA1"
        Me.rbSHA1.UseVisualStyleBackColor = True
        '
        'rbSHA256
        '
        Me.rbSHA256.AutoSize = True
        Me.rbSHA256.Location = New System.Drawing.Point(109, 26)
        Me.rbSHA256.Name = "rbSHA256"
        Me.rbSHA256.Size = New System.Drawing.Size(68, 17)
        Me.rbSHA256.TabIndex = 2
        Me.rbSHA256.TabStop = True
        Me.rbSHA256.Text = "SHA-256"
        Me.rbSHA256.UseVisualStyleBackColor = True
        '
        'rbSHA384
        '
        Me.rbSHA384.AutoSize = True
        Me.rbSHA384.Location = New System.Drawing.Point(109, 49)
        Me.rbSHA384.Name = "rbSHA384"
        Me.rbSHA384.Size = New System.Drawing.Size(68, 17)
        Me.rbSHA384.TabIndex = 7
        Me.rbSHA384.TabStop = True
        Me.rbSHA384.Text = "SHA-384"
        Me.rbSHA384.UseVisualStyleBackColor = True
        '
        'rbSHA512
        '
        Me.rbSHA512.AutoSize = True
        Me.rbSHA512.Location = New System.Drawing.Point(109, 72)
        Me.rbSHA512.Name = "rbSHA512"
        Me.rbSHA512.Size = New System.Drawing.Size(68, 17)
        Me.rbSHA512.TabIndex = 5
        Me.rbSHA512.TabStop = True
        Me.rbSHA512.Text = "SHA-512"
        Me.rbSHA512.UseVisualStyleBackColor = True
        '
        'rbGOST
        '
        Me.rbGOST.AutoSize = True
        Me.rbGOST.Enabled = False
        Me.rbGOST.Location = New System.Drawing.Point(109, 95)
        Me.rbGOST.Name = "rbGOST"
        Me.rbGOST.Size = New System.Drawing.Size(65, 17)
        Me.rbGOST.TabIndex = 8
        Me.rbGOST.TabStop = True
        Me.rbGOST.Text = "Blake2S"
        Me.rbGOST.UseVisualStyleBackColor = True
        Me.rbGOST.Visible = False
        '
        'rbRipMD
        '
        Me.rbRipMD.AutoSize = True
        Me.rbRipMD.Location = New System.Drawing.Point(3, 95)
        Me.rbRipMD.Name = "rbRipMD"
        Me.rbRipMD.Size = New System.Drawing.Size(88, 17)
        Me.rbRipMD.TabIndex = 11
        Me.rbRipMD.TabStop = True
        Me.rbRipMD.Text = "RIPEMD-160"
        Me.rbRipMD.UseVisualStyleBackColor = True
        '
        'btnDirectory
        '
        Me.btnDirectory.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnDirectory.Location = New System.Drawing.Point(6, 153)
        Me.btnDirectory.Name = "btnDirectory"
        Me.btnDirectory.Size = New System.Drawing.Size(254, 23)
        Me.btnDirectory.TabIndex = 6
        Me.btnDirectory.Text = "Каталоги для контроля"
        Me.btnDirectory.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnSave.Location = New System.Drawing.Point(101, 182)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(159, 23)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Сохранить"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'chkEVT
        '
        Me.chkEVT.AutoSize = True
        Me.chkEVT.Location = New System.Drawing.Point(6, 19)
        Me.chkEVT.Name = "chkEVT"
        Me.chkEVT.Size = New System.Drawing.Size(234, 17)
        Me.chkEVT.TabIndex = 9
        Me.chkEVT.Text = "Сохранять системные журналы в архив?"
        Me.chkEVT.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(260, 148)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Алгоритм вычисления контрольной суммы:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.nudS)
        Me.GroupBox2.Controls.Add(Me.nudM)
        Me.GroupBox2.Controls.Add(Me.nudH)
        Me.GroupBox2.Controls.Add(Me.chkEVT)
        Me.GroupBox2.Location = New System.Drawing.Point(266, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(257, 70)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Архивация системных журналов"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkNF)
        Me.GroupBox3.Location = New System.Drawing.Point(266, 76)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(256, 60)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Поиск новых файлов"
        '
        'chkNF
        '
        Me.chkNF.AutoSize = True
        Me.chkNF.Location = New System.Drawing.Point(7, 20)
        Me.chkNF.Name = "chkNF"
        Me.chkNF.Size = New System.Drawing.Size(141, 17)
        Me.chkNF.TabIndex = 0
        Me.chkNF.Text = "Искать новые файлы?"
        Me.chkNF.UseVisualStyleBackColor = True
        '
        'nudH
        '
        Me.nudH.Location = New System.Drawing.Point(8, 42)
        Me.nudH.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.nudH.Name = "nudH"
        Me.nudH.Size = New System.Drawing.Size(41, 20)
        Me.nudH.TabIndex = 10
        Me.nudH.Value = New Decimal(New Integer() {23, 0, 0, 0})
        Me.nudH.Visible = False
        '
        'nudM
        '
        Me.nudM.Location = New System.Drawing.Point(55, 42)
        Me.nudM.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.nudM.Name = "nudM"
        Me.nudM.Size = New System.Drawing.Size(41, 20)
        Me.nudM.TabIndex = 10
        Me.nudM.Value = New Decimal(New Integer() {52, 0, 0, 0})
        Me.nudM.Visible = False
        '
        'nudS
        '
        Me.nudS.Location = New System.Drawing.Point(102, 42)
        Me.nudS.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.nudS.Name = "nudS"
        Me.nudS.Size = New System.Drawing.Size(41, 20)
        Me.nudS.TabIndex = 10
        Me.nudS.Value = New Decimal(New Integer() {27, 0, 0, 0})
        Me.nudS.Visible = False
        '
        'frmChS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 212)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnDirectory)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmChS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Настройки"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.nudH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents rbMD5 As System.Windows.Forms.RadioButton
    Friend WithEvents rbCRC32 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSHA256 As System.Windows.Forms.RadioButton
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents rbSHA1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSHA512 As System.Windows.Forms.RadioButton
    Friend WithEvents btnDirectory As System.Windows.Forms.Button
    Friend WithEvents rbSHA384 As System.Windows.Forms.RadioButton
    Friend WithEvents rbGOST As System.Windows.Forms.RadioButton
    Friend WithEvents chkEVT As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbCRC64 As System.Windows.Forms.RadioButton
    Friend WithEvents rbAdler As System.Windows.Forms.RadioButton
    Friend WithEvents rbRipMD As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkNF As System.Windows.Forms.CheckBox
    Friend WithEvents nudS As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudM As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudH As System.Windows.Forms.NumericUpDown
End Class
