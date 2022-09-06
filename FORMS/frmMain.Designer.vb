<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.lvFiles = New System.Windows.Forms.ListView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.stlabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.stat2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.notfind = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ilsCommands = New System.Windows.Forms.ImageList(Me.components)
        Me.cmBmenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.addFoldertoBranch = New System.Windows.Forms.ToolStripMenuItem()
        Me.RepAddBrToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdateData = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDeltoBranch = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllDeleteDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.M_exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.МенюToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.addFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.addFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdateList = New System.Windows.Forms.ToolStripMenuItem()
        Me.IgnoreFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveList = New System.Windows.Forms.ToolStripMenuItem()
        Me.ПоискДубликатовToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetupPrg = New System.Windows.Forms.ToolStripMenuItem()
        Me.Exitmnu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОПрограммеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lvFilesF = New System.Windows.Forms.ListView()
        Me.lvFilesR = New System.Windows.Forms.ListView()
        Me.ni = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cmenuNI = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.РазвернутьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.НастройкиToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ВыходToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1.SuspendLayout()
        Me.cmBmenu.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.cmenuNI.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtPath
        '
        Me.txtPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPath.Location = New System.Drawing.Point(3, 3)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(1116, 20)
        Me.txtPath.TabIndex = 0
        Me.txtPath.Visible = False
        '
        'lvFiles
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.lvFiles, 2)
        Me.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvFiles.FullRowSelect = True
        Me.lvFiles.GridLines = True
        Me.lvFiles.Location = New System.Drawing.Point(3, 32)
        Me.lvFiles.MultiSelect = False
        Me.lvFiles.Name = "lvFiles"
        Me.lvFiles.Size = New System.Drawing.Size(1166, 508)
        Me.lvFiles.TabIndex = 2
        Me.lvFiles.UseCompatibleStateImageBehavior = False
        Me.lvFiles.View = System.Windows.Forms.View.Details
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1125, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(44, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.stlabel, Me.ToolStripStatusLabel1, Me.stat2, Me.ToolStripStatusLabel2, Me.notfind})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 587)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1202, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'stlabel
        '
        Me.stlabel.Name = "stlabel"
        Me.stlabel.Size = New System.Drawing.Size(12, 17)
        Me.stlabel.Text = "-"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(10, 17)
        Me.ToolStripStatusLabel1.Text = "|"
        '
        'stat2
        '
        Me.stat2.Name = "stat2"
        Me.stat2.Size = New System.Drawing.Size(12, 17)
        Me.stat2.Text = "-"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(10, 17)
        Me.ToolStripStatusLabel2.Text = "|"
        '
        'notfind
        '
        Me.notfind.Name = "notfind"
        Me.notfind.Size = New System.Drawing.Size(12, 17)
        Me.notfind.Text = "-"
        '
        'ilsCommands
        '
        Me.ilsCommands.ImageStream = CType(resources.GetObject("ilsCommands.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilsCommands.TransparentColor = System.Drawing.Color.White
        Me.ilsCommands.Images.SetKeyName(0, "add.png")
        Me.ilsCommands.Images.SetKeyName(1, "remove.png")
        Me.ilsCommands.Images.SetKeyName(2, "ok.png")
        Me.ilsCommands.Images.SetKeyName(3, "delete.png")
        Me.ilsCommands.Images.SetKeyName(4, "editservice.png")
        Me.ilsCommands.Images.SetKeyName(5, "pcupdate.png")
        Me.ilsCommands.Images.SetKeyName(6, "exit.png")
        '
        'cmBmenu
        '
        Me.cmBmenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.addFoldertoBranch, Me.RepAddBrToolStripMenuItem, Me.UpdateData, Me.mnuDeltoBranch, Me.ToolStripMenuItem1, Me.AllDeleteDB, Me.M_exit})
        Me.cmBmenu.Name = "cmMENU"
        Me.cmBmenu.Size = New System.Drawing.Size(207, 158)
        '
        'addFoldertoBranch
        '
        Me.addFoldertoBranch.Image = Global.HASHER.My.Resources.Resources.add
        Me.addFoldertoBranch.Name = "addFoldertoBranch"
        Me.addFoldertoBranch.Size = New System.Drawing.Size(206, 22)
        Me.addFoldertoBranch.Text = "Добавить папку"
        '
        'RepAddBrToolStripMenuItem
        '
        Me.RepAddBrToolStripMenuItem.Image = Global.HASHER.My.Resources.Resources.ok
        Me.RepAddBrToolStripMenuItem.Name = "RepAddBrToolStripMenuItem"
        Me.RepAddBrToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.RepAddBrToolStripMenuItem.Text = "Принять изменения"
        '
        'UpdateData
        '
        Me.UpdateData.Image = Global.HASHER.My.Resources.Resources.pcupdate
        Me.UpdateData.Name = "UpdateData"
        Me.UpdateData.Size = New System.Drawing.Size(206, 22)
        Me.UpdateData.Text = "Обновить данные"
        '
        'mnuDeltoBranch
        '
        Me.mnuDeltoBranch.Image = Global.HASHER.My.Resources.Resources.remove
        Me.mnuDeltoBranch.Name = "mnuDeltoBranch"
        Me.mnuDeltoBranch.Size = New System.Drawing.Size(206, 22)
        Me.mnuDeltoBranch.Text = "Удалить"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.HASHER.My.Resources.Resources.delete
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(206, 22)
        Me.ToolStripMenuItem1.Text = "Массовое удаление"
        '
        'AllDeleteDB
        '
        Me.AllDeleteDB.Image = Global.HASHER.My.Resources.Resources.servnz
        Me.AllDeleteDB.Name = "AllDeleteDB"
        Me.AllDeleteDB.Size = New System.Drawing.Size(206, 22)
        Me.AllDeleteDB.Text = "Полная очистка данных"
        Me.AllDeleteDB.Visible = False
        '
        'M_exit
        '
        Me.M_exit.Image = Global.HASHER.My.Resources.Resources._exit
        Me.M_exit.Name = "M_exit"
        Me.M_exit.Size = New System.Drawing.Size(206, 22)
        Me.M_exit.Text = "Выход"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.МенюToolStripMenuItem, Me.ОПрограммеToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1202, 24)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'МенюToolStripMenuItem
        '
        Me.МенюToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.addFolder, Me.addFile, Me.UpdateList, Me.IgnoreFile, Me.SaveList, Me.ПоискДубликатовToolStripMenuItem, Me.ClearDB, Me.SetupPrg, Me.Exitmnu})
        Me.МенюToolStripMenuItem.Image = Global.HASHER.My.Resources.Resources.service
        Me.МенюToolStripMenuItem.Name = "МенюToolStripMenuItem"
        Me.МенюToolStripMenuItem.Size = New System.Drawing.Size(69, 20)
        Me.МенюToolStripMenuItem.Text = "Меню"
        '
        'addFolder
        '
        Me.addFolder.Image = Global.HASHER.My.Resources.Resources.fadd
        Me.addFolder.Name = "addFolder"
        Me.addFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.addFolder.Size = New System.Drawing.Size(272, 22)
        Me.addFolder.Text = "Добавить каталог"
        '
        'addFile
        '
        Me.addFile.Image = Global.HASHER.My.Resources.Resources.add
        Me.addFile.Name = "addFile"
        Me.addFile.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.addFile.Size = New System.Drawing.Size(272, 22)
        Me.addFile.Text = "Добавить файл"
        '
        'UpdateList
        '
        Me.UpdateList.Image = Global.HASHER.My.Resources.Resources.pcupdate
        Me.UpdateList.Name = "UpdateList"
        Me.UpdateList.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.UpdateList.Size = New System.Drawing.Size(272, 22)
        Me.UpdateList.Text = "Обновить данные"
        '
        'IgnoreFile
        '
        Me.IgnoreFile.Image = Global.HASHER.My.Resources.Resources.B8
        Me.IgnoreFile.Name = "IgnoreFile"
        Me.IgnoreFile.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.IgnoreFile.Size = New System.Drawing.Size(272, 22)
        Me.IgnoreFile.Text = "Игнорируемые типы файлов"
        '
        'SaveList
        '
        Me.SaveList.Image = Global.HASHER.My.Resources.Resources.save
        Me.SaveList.Name = "SaveList"
        Me.SaveList.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.SaveList.Size = New System.Drawing.Size(272, 22)
        Me.SaveList.Text = "Сохранить список в файл"
        '
        'ПоискДубликатовToolStripMenuItem
        '
        Me.ПоискДубликатовToolStripMenuItem.Image = Global.HASHER.My.Resources.Resources.update
        Me.ПоискДубликатовToolStripMenuItem.Name = "ПоискДубликатовToolStripMenuItem"
        Me.ПоискДубликатовToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.ПоискДубликатовToolStripMenuItem.Text = "Поиск дубликатов"
        '
        'ClearDB
        '
        Me.ClearDB.Image = Global.HASHER.My.Resources.Resources.servnz
        Me.ClearDB.Name = "ClearDB"
        Me.ClearDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.Delete), System.Windows.Forms.Keys)
        Me.ClearDB.Size = New System.Drawing.Size(272, 22)
        Me.ClearDB.Text = "Очистить базу данных"
        '
        'SetupPrg
        '
        Me.SetupPrg.Image = Global.HASHER.My.Resources.Resources.setup
        Me.SetupPrg.Name = "SetupPrg"
        Me.SetupPrg.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.SetupPrg.Size = New System.Drawing.Size(272, 22)
        Me.SetupPrg.Text = "Настройки"
        '
        'Exitmnu
        '
        Me.Exitmnu.Image = Global.HASHER.My.Resources.Resources._exit
        Me.Exitmnu.Name = "Exitmnu"
        Me.Exitmnu.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.Exitmnu.Size = New System.Drawing.Size(272, 22)
        Me.Exitmnu.Text = "Выход"
        '
        'ОПрограммеToolStripMenuItem
        '
        Me.ОПрограммеToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ОПрограммеToolStripMenuItem.Name = "ОПрограммеToolStripMenuItem"
        Me.ОПрограммеToolStripMenuItem.Size = New System.Drawing.Size(94, 20)
        Me.ОПрограммеToolStripMenuItem.Text = "&О программе"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.Controls.Add(Me.Button1, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtPath, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lvFiles, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.lvFilesF, 2, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.lvFilesR, 2, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 24)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1202, 563)
        Me.TableLayoutPanel2.TabIndex = 8
        '
        'lvFilesF
        '
        Me.lvFilesF.FullRowSelect = True
        Me.lvFilesF.GridLines = True
        Me.lvFilesF.Location = New System.Drawing.Point(1175, 546)
        Me.lvFilesF.MultiSelect = False
        Me.lvFilesF.Name = "lvFilesF"
        Me.lvFilesF.Size = New System.Drawing.Size(24, 14)
        Me.lvFilesF.TabIndex = 4
        Me.lvFilesF.UseCompatibleStateImageBehavior = False
        Me.lvFilesF.View = System.Windows.Forms.View.Details
        Me.lvFilesF.Visible = False
        '
        'lvFilesR
        '
        Me.lvFilesR.FullRowSelect = True
        Me.lvFilesR.GridLines = True
        Me.lvFilesR.Location = New System.Drawing.Point(1175, 3)
        Me.lvFilesR.MultiSelect = False
        Me.lvFilesR.Name = "lvFilesR"
        Me.lvFilesR.Size = New System.Drawing.Size(24, 21)
        Me.lvFilesR.TabIndex = 3
        Me.lvFilesR.UseCompatibleStateImageBehavior = False
        Me.lvFilesR.View = System.Windows.Forms.View.Details
        Me.lvFilesR.Visible = False
        '
        'ni
        '
        Me.ni.ContextMenuStrip = Me.cmenuNI
        Me.ni.Icon = CType(resources.GetObject("ni.Icon"), System.Drawing.Icon)
        Me.ni.Text = "Проверка контрольной суммы"
        Me.ni.Visible = True
        '
        'cmenuNI
        '
        Me.cmenuNI.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.РазвернутьToolStripMenuItem, Me.ToolStripMenuItem3, Me.НастройкиToolStripMenuItem1, Me.ToolStripMenuItem4, Me.ВыходToolStripMenuItem1})
        Me.cmenuNI.Name = "cmenuNI"
        Me.cmenuNI.Size = New System.Drawing.Size(136, 82)
        '
        'РазвернутьToolStripMenuItem
        '
        Me.РазвернутьToolStripMenuItem.Name = "РазвернутьToolStripMenuItem"
        Me.РазвернутьToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.РазвернутьToolStripMenuItem.Text = "Развернуть"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(132, 6)
        '
        'НастройкиToolStripMenuItem1
        '
        Me.НастройкиToolStripMenuItem1.Image = Global.HASHER.My.Resources.Resources.setup
        Me.НастройкиToolStripMenuItem1.Name = "НастройкиToolStripMenuItem1"
        Me.НастройкиToolStripMenuItem1.Size = New System.Drawing.Size(135, 22)
        Me.НастройкиToolStripMenuItem1.Text = "Настройки"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(132, 6)
        '
        'ВыходToolStripMenuItem1
        '
        Me.ВыходToolStripMenuItem1.Image = Global.HASHER.My.Resources.Resources._exit
        Me.ВыходToolStripMenuItem1.Name = "ВыходToolStripMenuItem1"
        Me.ВыходToolStripMenuItem1.Size = New System.Drawing.Size(135, 22)
        Me.ВыходToolStripMenuItem1.Text = "Выход"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1202, 609)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Проверка контрольной суммы файлов"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.cmBmenu.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.cmenuNI.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents lvFiles As System.Windows.Forms.ListView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents stlabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ilsCommands As System.Windows.Forms.ImageList
    Friend WithEvents cmBmenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents addFoldertoBranch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDeltoBranch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RepAddBrToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents M_exit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllDeleteDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents МенюToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ОПрограммеToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents addFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpdateList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Exitmnu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpdateData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents stat2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents IgnoreFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetupPrg As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ni As System.Windows.Forms.NotifyIcon
    Friend WithEvents cmenuNI As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents РазвернутьToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents НастройкиToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ВыходToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents notfind As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lvFilesF As System.Windows.Forms.ListView
    Friend WithEvents lvFilesR As System.Windows.Forms.ListView
    Friend WithEvents addFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ПоискДубликатовToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
