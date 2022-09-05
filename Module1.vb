
Option Strict Off
Option Explicit On

Imports Microsoft.VisualBasic
Imports system.IO
Imports System.Text
Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.XPath
Imports Microsoft.Win32


Public Module Module1
    Public Declare Function GetPDFffCount Lib "PDFform.dll" (ByVal Filename As String) As Integer
    Public Declare Function GetPDFFormFields Lib "PDFform.dll" (ByVal Filename As String, ByVal Opt As Integer) As String
    Public Declare Function GetPDFFField Lib "PDFform.dll" (ByVal Filename As String, ByVal FieldName As String) As String
    Public Declare Function SetPDFFField Lib "PDFform.dll" (ByVal Filename As String, ByVal FieldName As String, ByVal FieldValue As String) As String

    Public Const DEMO_VERSION As Boolean = True

    Public Class FileData
        Public FieldList As String
        Public FieldValues As String
    End Class
    Public Sub XML2CSV(ByVal XML As XmlDocument, ByVal CSVFilePath As String)
        Dim sFieldList As String
        Dim cmd As String
        Dim baseDir As String
        baseDir = System.AppDomain.CurrentDomain.BaseDirectory()
        sFieldList = GetFieldList(XML)

        String2File(XML.InnerXml, baseDir + "\temp.xml")
        String2File(sFieldList, baseDir + "\temp.lst")

        cmd = baseDir + "\xml2csv " + baseDir + "\temp.xml " + CSVFilePath + " " + baseDir + "\temp.lst -Q"
        Shell(cmd, AppWinStyle.Hide, True)

    End Sub

    Private Function GetFieldList(ByVal xmlDoc As XmlDocument) As String
        Dim DataRecordNode As XmlNode
        Dim k As Integer
        Dim FieldName As String
        Dim sFieldList As String

        DataRecordNode = xmlDoc.SelectSingleNode("/table/record")
        sFieldList = ""

        For k = 0 To DataRecordNode.ChildNodes.Count - 1
            FieldName = DataRecordNode.ChildNodes(k).Name
            If k = 0 Then
                sFieldList = FieldName
            Else
                sFieldList = sFieldList + "," + FieldName
            End If
        Next
        Return sFieldList
    End Function


    Public Function File2String(ByVal file_name As String) As String
        Dim oFile As New StreamReader(file_name)
        Return oFile.ReadToEnd()
        oFile.Close()
    End Function

    Public Sub String2File(ByVal strInput As String, ByVal file_name As String)
        Dim oFile As New StreamWriter(file_name)
        oFile.Write(strInput)
        oFile.Close()
    End Sub

    Public Sub SaveSetting(ByVal Setting As String, ByVal sValue As String)
        Dim regKey As RegistryKey
        Dim ver As Decimal

        regKey = Registry.LocalMachine.OpenSubKey("Software\MyAppMgr", True)
        If regKey Is Nothing Then
            Registry.LocalMachine.CreateSubKey("Software\MyAppMgr")
            regKey = Registry.LocalMachine.OpenSubKey("Software\MyAppMgr", True)
        End If
        regKey.SetValue(Setting, sValue)
        regKey.Close()
    End Sub
    Public Function GetSetting(ByVal Setting As String) As String
        Dim regKey As RegistryKey
        Dim ver As String
        regKey = Registry.LocalMachine.OpenSubKey("Software\MyAppMgr", True)
        If regKey Is Nothing Then
            Return ""
        End If
        ver = regKey.GetValue(Setting)
        regKey.Close()
        Return ver

    End Function
End Module
