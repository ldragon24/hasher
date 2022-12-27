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
        Me.rbRipMD = New System.Windows.Forms.RadioButton()
        Me.rbSHA1 = New System.Windows.Forms.RadioButton()
        Me.rbSHA256 = New System.Windows.Forms.RadioButton()
        Me.rbSHA384 = New System.Windows.Forms.RadioButton()
        Me.rbSHA512 = New System.Windows.Forms.RadioButton()
        Me.rbGOST = New System.Windows.Forms.RadioButton()
        Me.rbhMD5 = New System.Windows.Forms.RadioButton()
        Me.rbhRipMD = New System.Windows.Forms.RadioButton()
        Me.rbhSHA1 = New System.Windows.Forms.RadioButton()
        Me.rbhSHA256 = New System.Windows.Forms.RadioButton()
        Me.rbhSHA384 = New System.Windows.Forms.RadioButton()
        Me.rbhSHA512 = New System.Windows.Forms.RadioButton()
        Me.btnDirectory = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.chkEVT = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.nudS = New System.Windows.Forms.NumericUpDown()
        Me.nudM = New System.Windows.Forms.NumericUpDown()
        Me.nudH = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkNF = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.nudS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
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
        Me.TableLayoutPanel1.Controls.Add(Me.rbRipMD, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSHA1, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSHA256, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSHA384, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSHA512, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.rbGOST, 0, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.rbhMD5, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rbhRipMD, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.rbhSHA1, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.rbhSHA256, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.rbhSHA384, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.rbhSHA512, 1, 9)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 11
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(254, 231)
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
        'rbSHA1
        '
        Me.rbSHA1.AutoSize = True
        Me.rbSHA1.Location = New System.Drawing.Point(3, 118)
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
        Me.rbSHA256.Location = New System.Drawing.Point(3, 141)
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
        Me.rbSHA384.Location = New System.Drawing.Point(3, 164)
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
        Me.rbSHA512.Location = New System.Drawing.Point(3, 187)
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
        Me.rbGOST.Location = New System.Drawing.Point(3, 210)
        Me.rbGOST.Name = "rbGOST"
        Me.rbGOST.Size = New System.Drawing.Size(60, 17)
        Me.rbGOST.TabIndex = 8
        Me.rbGOST.TabStop = True
        Me.rbGOST.Text = "Magma"
        Me.rbGOST.UseVisualStyleBackColor = True
        Me.rbGOST.Visible = False
        '
        'rbhMD5
        '
        Me.rbhMD5.AutoSize = True
        Me.rbhMD5.Location = New System.Drawing.Point(109, 3)
        Me.rbhMD5.Name = "rbhMD5"
        Me.rbhMD5.Size = New System.Drawing.Size(82, 17)
        Me.rbhMD5.TabIndex = 12
        Me.rbhMD5.TabStop = True
        Me.rbhMD5.Text = "HMAC-MD5"
        Me.rbhMD5.UseVisualStyleBackColor = True
        '
        'rbhRipMD
        '
        Me.rbhRipMD.AutoSize = True
        Me.rbhRipMD.Location = New System.Drawing.Point(109, 95)
        Me.rbhRipMD.Name = "rbhRipMD"
        Me.rbhRipMD.Size = New System.Drawing.Size(119, 17)
        Me.rbhRipMD.TabIndex = 12
        Me.rbhRipMD.TabStop = True
        Me.rbhRipMD.Text = "HMAC-RIPEMD160"
        Me.rbhRipMD.UseVisualStyleBackColor = True
        '
        'rbhSHA1
        '
        Me.rbhSHA1.AutoSize = True
        Me.rbhSHA1.Location = New System.Drawing.Point(109, 118)
        Me.rbhSHA1.Name = "rbhSHA1"
        Me.rbhSHA1.Size = New System.Drawing.Size(87, 17)
        Me.rbhSHA1.TabIndex = 12
        Me.rbhSHA1.TabStop = True
        Me.rbhSHA1.Text = "HMAC-SHA1"
        Me.rbhSHA1.UseVisualStyleBackColor = True
        '
        'rbhSHA256
        '
        Me.rbhSHA256.AutoSize = True
        Me.rbhSHA256.Location = New System.Drawing.Point(109, 141)
        Me.rbhSHA256.Name = "rbhSHA256"
        Me.rbhSHA256.Size = New System.Drawing.Size(99, 17)
        Me.rbhSHA256.TabIndex = 12
        Me.rbhSHA256.TabStop = True
        Me.rbhSHA256.Text = "HMAC-SHA256"
        Me.rbhSHA256.UseVisualStyleBackColor = True
        '
        'rbhSHA384
        '
        Me.rbhSHA384.AutoSize = True
        Me.rbhSHA384.Location = New System.Drawing.Point(109, 164)
        Me.rbhSHA384.Name = "rbhSHA384"
        Me.rbhSHA384.Size = New System.Drawing.Size(99, 17)
        Me.rbhSHA384.TabIndex = 12
        Me.rbhSHA384.TabStop = True
        Me.rbhSHA384.Text = "HMAC-SHA384"
        Me.rbhSHA384.UseVisualStyleBackColor = True
        '
        'rbhSHA512
        '
        Me.rbhSHA512.AutoSize = True
        Me.rbhSHA512.Location = New System.Drawing.Point(109, 187)
        Me.rbhSHA512.Name = "rbhSHA512"
        Me.rbhSHA512.Size = New System.Drawing.Size(99, 17)
        Me.rbhSHA512.TabIndex = 12
        Me.rbhSHA512.TabStop = True
        Me.rbhSHA512.Text = "HMAC-SHA512"
        Me.rbhSHA512.UseVisualStyleBackColor = True
        '
        'btnDirectory
        '
        Me.btnDirectory.Image = Global.HASHER.My.Resources.Resources.fadd
        Me.btnDirectory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDirectory.Location = New System.Drawing.Point(5, 256)
        Me.btnDirectory.Name = "btnDirectory"
        Me.btnDirectory.Size = New System.Drawing.Size(254, 39)
        Me.btnDirectory.TabIndex = 6
        Me.btnDirectory.Text = "Каталоги для контроля"
        Me.btnDirectory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnDirectory.UseCompatibleTextRendering = True
        Me.btnDirectory.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = Global.HASHER.My.Resources.Resources.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(411, 256)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(105, 39)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Сохранить"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(260, 250)
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
        Me.GroupBox2.Size = New System.Drawing.Size(250, 70)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Архивация системных журналов"
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
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkNF)
        Me.GroupBox3.Location = New System.Drawing.Point(266, 76)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(250, 60)
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
        'frmChS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 298)
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
        CType(Me.nudS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
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
    Friend WithEvents rbhMD5 As System.Windows.Forms.RadioButton
    Friend WithEvents rbhRipMD As System.Windows.Forms.RadioButton
    Friend WithEvents rbhSHA1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbhSHA256 As System.Windows.Forms.RadioButton
    Friend WithEvents rbhSHA384 As System.Windows.Forms.RadioButton
    Friend WithEvents rbhSHA512 As System.Windows.Forms.RadioButton
End Class
