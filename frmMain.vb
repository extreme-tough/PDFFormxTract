Imports System.Text
Imports System.IO
Imports Projekt1.Module1
Imports System.Data.Odbc
Imports System.Xml

Public Class frmMain
    Dim oCon As New OdbcConnection()

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim i As Integer
        Dim FilePath As String
        Dim FullPath As String
        Dim retFileData As FileData
        Dim sTempFile As String
        Dim startIndex As Integer
        Dim arFieldNames() As String
        Dim arFieldValues() As String
        Dim j As Integer
        Dim XMLData As String
        Dim TempXMLData As String
        Dim ParentXMLDoc As New Xml.XmlDocument
        Dim ChildXMLDoc As New XmlDocument
        Dim nLastItemAdded As Integer
        Dim nExceptionThrown As Integer
        Dim nFilesProcssed As Integer

        lstStatus.Items.Clear()


        If txtOutputFile.Text.Trim() = "" Then
            MessageBox.Show("Please enter the output file name", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If lvwFiles.Items.Count = 0 Then
            MessageBox.Show("No files selected", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        

        startIndex = 0
        nFilesProcssed = 0

        btnProcess.Enabled = False

        'Delete 0 bytes size files
        If File.Exists(txtOutputFile.Text) Then
            If lvwFiles.Items(0).Text.ToUpper() = "INDEX.PDF" Then
                MessageBox.Show("Output file already exists. You cannot define Index fields using index.pdf", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                btnProcess.Enabled = True
                Exit Sub
            End If

            Dim a As New FileInfo(txtOutputFile.Text)
            If a.Length = 0 Then
                a.Delete()
            End If
        End If

        If Not File.Exists(txtOutputFile.Text) Then
            Dim fs As New StreamWriter(New FileStream(txtOutputFile.Text, FileMode.Create))
            Do While True
                nExceptionThrown = 0
                If startIndex + 1 > lvwFiles.Items.Count Then
                    Exit Do
                End If
                FilePath = lvwFiles.Items(startIndex).SubItems(3).Text
                FullPath = FilePath + "\" + lvwFiles.Items(startIndex).Text
                nLastItemAdded = lstStatus.Items.Add("Processing " + FullPath + "...")
                Try
                    retFileData = ProcessFile(FullPath)
                Catch ex As InvalidFileFormatException
                    nExceptionThrown = 1
                    lstStatus.Items(nLastItemAdded) = "Processing " + FullPath + "...Failed!"
                    lstStatus.Items.Add("Not a valid PDF file!")
                    lvwFiles.Items(startIndex).ImageKey = "delete.ico"
                Catch ex As EmptyFieldsException
                    nExceptionThrown = 1
                    lstStatus.Items(nLastItemAdded) = "Processing " + FullPath + "...Failed!"
                    lstStatus.Items.Add("No fields found!")
                    lvwFiles.Items(startIndex).ImageKey = "delete.ico"
                End Try



                If nExceptionThrown = 0 Then
                    lstStatus.Items(nLastItemAdded) = "Processing " + FullPath + "...Successful"
                    lvwFiles.Items(startIndex).ImageKey = "tick.ico"
                    If lvwFiles.Items(startIndex).Text.ToUpper() = "INDEX.PDF" Then
                        fs.WriteLine("filename,processDate," + retFileData.FieldValues)
                    Else
                        fs.WriteLine("filename,processDate," + retFileData.FieldList)
                        fs.WriteLine(lvwFiles.Items(startIndex).Text + "," + DateTime.Now + "," + retFileData.FieldValues)
                    End If
                    fs.Close()
                    nFilesProcssed = nFilesProcssed + 1
                    startIndex = startIndex + 1
                    Exit Do
                End If

                startIndex = startIndex + 1
            Loop
        End If

        'Load content as XML
        Try
            XMLData = clsXMLCSV.CSV2XML(txtOutputFile.Text)
        Catch ex As Exception
            lstStatus.Items.Add("Output file not valid or no valid PDF to process. Procssing stopped.")
            btnProcess.Enabled = True
            Exit Sub
        End Try

        ParentXMLDoc.LoadXml(XMLData)

        'Loop through each additional item
        For i = startIndex To lvwFiles.Items.Count - 1
            FilePath = lvwFiles.Items(i).SubItems(3).Text
            FullPath = FilePath + "\" + lvwFiles.Items(i).Text
            
            nLastItemAdded = lstStatus.Items.Add("Processing " + FullPath + "...")
            retFileData = ProcessFile(FullPath)
            lstStatus.Items(nLastItemAdded) = "Processing " + FullPath + "...Successful"
            lvwFiles.Items(i).ImageKey = "tick.ico"
            lvwFiles.Refresh()

            nFilesProcssed = nFilesProcssed + 1


            sTempFile = Path.GetTempFileName()
            String2File("filename,processDate," + retFileData.FieldList + vbCrLf + _
                    lvwFiles.Items(i).Text + "," + DateTime.Now + "," + retFileData.FieldValues, sTempFile)

            TempXMLData = clsXMLCSV.CSV2XML(sTempFile)

            ChildXMLDoc.LoadXml(TempXMLData)

            Dim NodeName As String


            Dim selectedNode As XmlNode
            Dim NodeList As XmlNodeList
            Dim k As Integer
            Dim TableNode As XmlNode
            Dim RecordNode As XmlNode
            Dim TextNode As XmlText
            Dim NewRecordNode As XmlNode
            Dim DataRecordNode As XmlNode
            Dim FieldNode As XmlNode

            selectedNode = ChildXMLDoc.SelectSingleNode("/table/record")

            'Search for new fields
            For j = 0 To selectedNode.ChildNodes.Count - 1
                NodeName = selectedNode.ChildNodes(j).Name()
                If ParentXMLDoc.SelectNodes("/table/record/" + NodeName).Count = 0 Then
                    'A new node in the current file. Add it to parent
                    NodeList = ParentXMLDoc.SelectNodes("/table/record")
                    For k = 0 To NodeList.Count - 1
                        RecordNode = NodeList(k)
                        FieldNode = ParentXMLDoc.CreateElement(NodeName)
                        RecordNode.AppendChild(FieldNode)
                    Next
                End If
            Next

            selectedNode = ParentXMLDoc.SelectSingleNode("/table/record[filename='" + lvwFiles.Items(i).Text + "']")

            If selectedNode Is Nothing Then
                'This is a new node. It can be added to parent
                TableNode = ParentXMLDoc.SelectSingleNode("/table")
                NewRecordNode = ParentXMLDoc.CreateElement("record")
                DataRecordNode = ChildXMLDoc.SelectSingleNode("/table/record")

                For k = 0 To DataRecordNode.ChildNodes.Count - 1
                    FieldNode = ParentXMLDoc.CreateElement(DataRecordNode.ChildNodes(k).Name)
                    TextNode = ParentXMLDoc.CreateTextNode(DataRecordNode.ChildNodes(k).InnerText)
                    FieldNode.AppendChild(TextNode)
                    NewRecordNode.AppendChild(FieldNode)
                Next
                TableNode.AppendChild(NewRecordNode)
            Else
                'Selected node is existing. It need to be updated
                DataRecordNode = ChildXMLDoc.SelectSingleNode("/table/record")
                For k = 0 To DataRecordNode.ChildNodes.Count - 1
                    NewRecordNode = DataRecordNode.ChildNodes(k)
                    FieldNode = selectedNode.SelectSingleNode(NewRecordNode.Name)
                    FieldNode.InnerText = NewRecordNode.InnerText
                Next
            End If
            If nFilesProcssed >= 3 And DEMO_VERSION And lvwFiles.Items.Count > 3 Then
                MessageBox.Show("You can process only three files in the demo version. Finishing")
                Exit For
            End If
        Next

        XML2CSV(ParentXMLDoc, txtOutputFile.Text)

        nLastItemAdded = lstStatus.Items.Add("Process Completed. Output file generated")
        MessageBox.Show("Process completed", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnProcess.Enabled = True

    End Sub


    Private Sub OpenConnection(ByVal sFilePath As String)
        oCon = New OdbcConnection("Dsn=PDFOut;dbq=" + sFilePath + ";defaultdir=" + sFilePath + ";driverid=27;fil=text;maxbuffersize=2048;pagetimeout=5")
        'oCon = New OdbcConnection("DSN=PDFMDB")
    End Sub

    Public Function ProcessFile(ByVal sFileName As String) As FileData
        Dim nFieldCount As Integer
        Dim sText As String
        Dim arItems() As String
        Dim sbFields As New StringBuilder("")
        Dim sbValues As New StringBuilder("")

        nFieldCount = CStr(GetPDFffCount(sFileName))
        If nFieldCount = 9002 Then
            Throw New InvalidFileFormatException()
            Exit Function
        End If

        If nFieldCount = 0 Then
            Throw New EmptyFieldsException()
            Exit Function
        End If
        sText = GetPDFFormFields(sFileName, 3)

        Dim Encoding As New System.Text.ASCIIEncoding()
        Dim MStream As New MemoryStream(Encoding.GetBytes(sText))
        Dim sr1 As New StreamReader(MStream)
        Dim sLine As String
        Dim nRowNo As Integer
        Dim sFieldValue As String
        Dim sFieldTitle As String
        Dim flData As New FileData
        Dim sLastFieldTitle As String = ""
        Dim sLastFieldValue As String = ""

        nRowNo = 1

        sLine = sr1.ReadLine()
        While sLine <> Nothing
            arItems = Split(sLine, ";")

            sFieldTitle = arItems(11)
            sFieldValue = arItems(12)

            If nRowNo <> 1 Then
                'Exclude check box data
                If sLastFieldTitle <> sFieldTitle Then
                    sbFields.Append(sFieldTitle)
                    sbFields.Append(",")
                    'End If

                    'If sLastFieldValue <> sFieldValue Then
                    If sFieldValue.Contains(",") Then
                        sFieldValue = """" + sFieldValue + """"
                    End If
                    sbValues.Append(sFieldValue)
                    sbValues.Append(",")
                End If
            End If
            sLastFieldTitle = sFieldTitle
            sLastFieldValue = sFieldValue
            sLine = sr1.ReadLine()
            nRowNo = nRowNo + 1
        End While
        flData.FieldList = sbFields.ToString().Substring(0, sbFields.Length - 1)
        flData.FieldValues = sbValues.ToString().Substring(0, sbValues.Length - 1)
        Return flData
    End Function

    Private Sub btnAddFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFolder.Click
        Dim SelectedFolder As String
        Dim Files As String()
        Dim i As Integer
        Dim FileExt As String


        BrowseFolder.SelectedPath = ""
        BrowseFolder.ShowDialog()
        SelectedFolder = BrowseFolder.SelectedPath

        If SelectedFolder <> "" Then
            Files = Directory.GetFiles(SelectedFolder, "*.pdf", SearchOption.AllDirectories)
            For i = 0 To Files.Length - 1
                '                FileExt = Path.GetExtension(Files(i)).ToUpper
                '               If FileExt = ".PDF" Then
                AddFileToList(Files(i))
                'End If
            Next
        End If
    End Sub

    Private Sub btnAddFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFiles.Click
        dlgOpenFile.FileName = ""
        dlgOpenFile.ShowDialog()
        If (dlgOpenFile.FileName <> "") Then
            AddFileToList(dlgOpenFile.FileName)
        End If
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lvwFiles.Columns.Add("File Name")
        lvwFiles.Columns.Add("Date")
        lvwFiles.Columns.Add("Size")
        lvwFiles.Columns.Add("Path")
        lvwFiles.View = View.Details
        lvwFiles.Columns(0).Width = lvwFiles.Width * 0.3
        lvwFiles.Columns(1).Width = lvwFiles.Width * 0.15
        lvwFiles.Columns(2).Width = lvwFiles.Width * 0.15
        lvwFiles.Columns(3).Width = lvwFiles.Width * 0.4

        If DEMO_VERSION Then
            Me.Text = "MyApplicationManager 1.0 FREE"
        Else
            Me.Text = "MyApplicationManager 1.0 PRO"
        End If
        txtOutputFile.Text = GetSetting("Path")
        LoadLV()
    End Sub

    Private Sub AddFileToList(ByVal FileName As String)
        Dim FileDetails As New FileInfo(FileName)
        Dim JustFileName As String

        JustFileName = Path.GetFileName(FileName)

        If JustFileName.ToUpper = "INDEX.PDF" And lvwFiles.Items.Count > 0 Then
            MessageBox.Show("INDX.PDF can be added only as a first file to extract fields", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        lvwFiles.Items.Add("K" + (lvwFiles.Items.Count + 1).ToString().Trim(), _
            JustFileName, 0)
        lvwFiles.Items(lvwFiles.Items.Count - 1).SubItems.Add(FileDetails.LastWriteTime)
        lvwFiles.Items(lvwFiles.Items.Count - 1).SubItems.Add(FileDetails.Length)
        lvwFiles.Items(lvwFiles.Items.Count - 1).SubItems.Add(Path.GetDirectoryName(FileName))

    End Sub

    Private Sub btnSelectCSVFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectCSVFile.Click
        dlgSaveFile.FileName = ""
        dlgSaveFile.ShowDialog()
        If dlgSaveFile.FileName <> "" Then
            txtOutputFile.Text = dlgSaveFile.FileName
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        lvwFiles.Items.Clear()
        lstStatus.Items.Clear()
    End Sub

    Private Sub lvwFiles_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lvwFiles.KeyPress
        'MessageBox.Show(e.KeyChar.ToString())
    End Sub

    Private Sub lvwFiles_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvwFiles.KeyUp
        Try
            If e.KeyCode.ToString() = "Delete" Then
                lstStatus.Items.Remove(lstStatus.SelectedItem)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        
        Application.Exit()

    End Sub

    Private Sub btnQuickFolderAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuickFolderAdd.Click
        Dim Files() As String
        Dim i As Integer

        If txtFolderName.Text <> "" Then
            Files = Directory.GetFiles(txtFolderName.Text, "*.pdf", SearchOption.AllDirectories)
            For i = 0 To Files.Length - 1
                '                FileExt = Path.GetExtension(Files(i)).ToUpper
                '               If FileExt = ".PDF" Then
                AddFileToList(Files(i))
                'End If
            Next
        End If
    End Sub

    Public Sub SaveLV()
        Dim lvItem As ListViewItem
        Dim baseDir As String
        Dim i As Integer
        Dim Items As String = ""
        For Each lvItem In lvwFiles.Items
            Items += lvItem.Text + "|"
            i = 0
            For Each subitem As ListViewItem.ListViewSubItem In lvItem.SubItems
                If i <> 0 Then
                    Items += subitem.Text + "|"
                End If
                i = i + 1
            Next
            Items = Items.Substring(0, Len(Items) - 1) 'Remove Last |
            Items += "`" 'We need a different delimiter to separate between list view items
        Next
        baseDir = System.AppDomain.CurrentDomain.BaseDirectory()
        Dim tw As New StreamWriter(baseDir + "lvcontent.txt")
        tw.Write(Items)
        tw.Close()
    End Sub

    Public Sub LoadLV()
        Dim baseDir As String
        Dim sData As String
        baseDir = System.AppDomain.CurrentDomain.BaseDirectory()
        If Not File.Exists(baseDir + "lvcontent.txt") Then
            Return
        End If
        Dim tr As New StreamReader(baseDir + "lvcontent.txt")
        sData = tr.ReadToEnd()
        tr.Close()

        Dim arrItems As String() = sData.Split("`")
        For Each strItem As String In arrItems
            Dim arrSubItems As String() = strItem.Split("|")
            If arrSubItems(0) <> "" Then
                Dim lvItem As ListViewItem = lvwFiles.Items.Add("K" + (lvwFiles.Items.Count + 1).ToString().Trim(), arrSubItems(0), 0)

                For i As Integer = 1 To arrSubItems.Length - 1
                    lvItem.SubItems.Add(arrSubItems(i))
                Next
            End If
        Next
    End Sub

    Private Sub frmMain_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        SaveSetting("Path", txtOutputFile.Text)
        SaveLV()
    End Sub
End Class

Public Class InvalidFileFormatException
    Inherits Exception


End Class
Public Class EmptyFieldsException
    Inherits Exception


End Class
