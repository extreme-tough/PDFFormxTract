<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnQuickFolderAdd = New System.Windows.Forms.Button
        Me.txtFolderName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnSelectCSVFile = New System.Windows.Forms.Button
        Me.txtOutputFile = New System.Windows.Forms.TextBox
        Me.lblOutput = New System.Windows.Forms.Label
        Me.btnAddFiles = New System.Windows.Forms.Button
        Me.btnAddFolder = New System.Windows.Forms.Button
        Me.btnProcess = New System.Windows.Forms.Button
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.lvwFiles = New System.Windows.Forms.ListView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lstStatus = New System.Windows.Forms.ListBox
        Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog
        Me.BrowseFolder = New System.Windows.Forms.FolderBrowserDialog
        Me.dlgSaveFile = New System.Windows.Forms.SaveFileDialog
        Me.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnQuickFolderAdd)
        Me.Panel1.Controls.Add(Me.txtFolderName)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.btnSelectCSVFile)
        Me.Panel1.Controls.Add(Me.txtOutputFile)
        Me.Panel1.Controls.Add(Me.lblOutput)
        Me.Panel1.Controls.Add(Me.btnAddFiles)
        Me.Panel1.Controls.Add(Me.btnAddFolder)
        Me.Panel1.Controls.Add(Me.btnProcess)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(974, 71)
        Me.Panel1.TabIndex = 0
        '
        'btnQuickFolderAdd
        '
        Me.btnQuickFolderAdd.Location = New System.Drawing.Point(262, 8)
        Me.btnQuickFolderAdd.Name = "btnQuickFolderAdd"
        Me.btnQuickFolderAdd.Size = New System.Drawing.Size(56, 27)
        Me.btnQuickFolderAdd.TabIndex = 2
        Me.btnQuickFolderAdd.Text = "&Add"
        Me.btnQuickFolderAdd.UseVisualStyleBackColor = True
        '
        'txtFolderName
        '
        Me.txtFolderName.Location = New System.Drawing.Point(54, 11)
        Me.txtFolderName.Name = "txtFolderName"
        Me.txtFolderName.Size = New System.Drawing.Size(202, 20)
        Me.txtFolderName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Folder"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(874, 41)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(91, 27)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(483, 8)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(91, 27)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "&Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSelectCSVFile
        '
        Me.btnSelectCSVFile.Location = New System.Drawing.Point(940, 10)
        Me.btnSelectCSVFile.Name = "btnSelectCSVFile"
        Me.btnSelectCSVFile.Size = New System.Drawing.Size(25, 23)
        Me.btnSelectCSVFile.TabIndex = 7
        Me.btnSelectCSVFile.Text = "..."
        Me.btnSelectCSVFile.UseVisualStyleBackColor = True
        '
        'txtOutputFile
        '
        Me.txtOutputFile.Location = New System.Drawing.Point(660, 11)
        Me.txtOutputFile.Name = "txtOutputFile"
        Me.txtOutputFile.Size = New System.Drawing.Size(281, 20)
        Me.txtOutputFile.TabIndex = 6
        '
        'lblOutput
        '
        Me.lblOutput.AutoSize = True
        Me.lblOutput.Location = New System.Drawing.Point(596, 15)
        Me.lblOutput.Name = "lblOutput"
        Me.lblOutput.Size = New System.Drawing.Size(58, 13)
        Me.lblOutput.TabIndex = 3
        Me.lblOutput.Text = "Output File"
        '
        'btnAddFiles
        '
        Me.btnAddFiles.Location = New System.Drawing.Point(386, 8)
        Me.btnAddFiles.Name = "btnAddFiles"
        Me.btnAddFiles.Size = New System.Drawing.Size(91, 27)
        Me.btnAddFiles.TabIndex = 4
        Me.btnAddFiles.Text = "Add &Files"
        Me.btnAddFiles.UseVisualStyleBackColor = True
        '
        'btnAddFolder
        '
        Me.btnAddFolder.Location = New System.Drawing.Point(324, 8)
        Me.btnAddFolder.Name = "btnAddFolder"
        Me.btnAddFolder.Size = New System.Drawing.Size(56, 27)
        Me.btnAddFolder.TabIndex = 3
        Me.btnAddFolder.Text = "&Pick..."
        Me.btnAddFolder.UseVisualStyleBackColor = True
        '
        'btnProcess
        '
        Me.btnProcess.Location = New System.Drawing.Point(777, 41)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(91, 27)
        Me.btnProcess.TabIndex = 8
        Me.btnProcess.Text = "&Process"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 71)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lvwFiles)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lstStatus)
        Me.SplitContainer1.Size = New System.Drawing.Size(974, 518)
        Me.SplitContainer1.SplitterDistance = 316
        Me.SplitContainer1.TabIndex = 1
        '
        'lvwFiles
        '
        Me.lvwFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwFiles.FullRowSelect = True
        Me.lvwFiles.Location = New System.Drawing.Point(0, 0)
        Me.lvwFiles.Name = "lvwFiles"
        Me.lvwFiles.Size = New System.Drawing.Size(974, 316)
        Me.lvwFiles.SmallImageList = Me.ImageList1
        Me.lvwFiles.TabIndex = 0
        Me.lvwFiles.UseCompatibleStateImageBehavior = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "PDF.ico")
        Me.ImageList1.Images.SetKeyName(1, "tick.ico")
        Me.ImageList1.Images.SetKeyName(2, "delete.ico")
        '
        'lstStatus
        '
        Me.lstStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstStatus.FormattingEnabled = True
        Me.lstStatus.Location = New System.Drawing.Point(0, 0)
        Me.lstStatus.Name = "lstStatus"
        Me.lstStatus.Size = New System.Drawing.Size(974, 186)
        Me.lstStatus.TabIndex = 0
        '
        'dlgOpenFile
        '
        Me.dlgOpenFile.FileName = "OpenFileDialog1"
        Me.dlgOpenFile.Filter = "Adobe Reader (*.pdf)|*.pdf"
        '
        'dlgSaveFile
        '
        Me.dlgSaveFile.Filter = "CSV Files(*.csv)|*.csv"
        Me.dlgSaveFile.OverwritePrompt = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 589)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MyApplicationManager 1.0"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lvwFiles As System.Windows.Forms.ListView
    Friend WithEvents lstStatus As System.Windows.Forms.ListBox
    Friend WithEvents btnAddFiles As System.Windows.Forms.Button
    Friend WithEvents btnAddFolder As System.Windows.Forms.Button
    Friend WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BrowseFolder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnSelectCSVFile As System.Windows.Forms.Button
    Friend WithEvents txtOutputFile As System.Windows.Forms.TextBox
    Friend WithEvents lblOutput As System.Windows.Forms.Label
    Friend WithEvents dlgSaveFile As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtFolderName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnQuickFolderAdd As System.Windows.Forms.Button
End Class
