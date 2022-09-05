
Imports System.IO
Imports System.Xml
Imports System.Text

Public Class clsXMLCSV

    Public Shared Function CSV2XML(ByVal CSVFilePath As String) As String

        Dim sFile As New StreamReader(CSVFilePath, System.Text.Encoding.ASCII)

        Dim sLine As String

        Dim arFields() As String

        Dim arValues() As String

        Dim nLineNo As Integer

        Dim xmlData As New StringBuilder("")

        Dim i As Integer


        nLineNo = 1
        Do While True
            sLine = sFile.ReadLine()
            If sLine = "" Then Exit Do
            If nLineNo = 1 Then
                arFields = QuoteSplit(sLine)
                xmlData.Append("<table>" + vbCrLf + "<record>")
            Else
                arValues = QuoteSplit(sLine)
                If arValues.Length <> arFields.Length Then
                    Throw New Exception("Field and values count do not match")
                    Exit Function
                End If
                For i = 0 To arValues.Length - 1
                    arFields(i) = arFields(i).Replace("(", "_")
                    arFields(i) = arFields(i).Replace(")", "_")
                    arFields(i) = arFields(i).Replace("[", "_")
                    arFields(i) = arFields(i).Replace("]", "_")
                    xmlData.Append("<" + arFields(i) + ">")
                    xmlData.Append(arValues(i))
                    xmlData.Append("</" + arFields(i) + ">")
                Next
            End If
            nLineNo = nLineNo + 1
        Loop
        xmlData.Append("</record>" + vbCrLf + "</table>")
        sFile.Close()
        Return xmlData.ToString()
    End Function







    Private Shared Function QuoteSplit(ByVal str As String, Optional ByVal splitChar As Char = ","c, Optional ByVal QuoteChar As Char = """"c) As String()

        'Use double-quotes to escape the quote character. Example: Hello ""John"" will produce Hello "John"

        Dim quoteOpened As Boolean = False

        Dim al As New ArrayList

        Dim curStr As New System.Text.StringBuilder

        For i As Integer = 0 To str.Length - 1

            Dim c As Char = CChar(str.Substring(i, 1))

            Dim nextChar As String = "" ' Cannot use Char because it is a value type and cannot contain Nothing or empty string

            If str.Length > (i + 1) Then nextChar = str.Substring(i + 1, 1)

            If quoteOpened Then

                'Look for ending quote character

                If (Not c = QuoteChar) Then

                    curStr.Append(c)

                ElseIf c = QuoteChar AndAlso Not nextChar = "" AndAlso nextChar = QuoteChar Then

                    curStr.Append(QuoteChar)

                    i += 1

                ElseIf c = QuoteChar Then

                    quoteOpened = False 'Clear

                End If

            Else 'If Not quoteOpened

                If c = splitChar Then

                    al.Add(curStr.ToString) 'Add to arraylist

                    curStr.Length = 0 'Clear current string

                ElseIf c = QuoteChar Then

                    quoteOpened = True

                    curStr.Length = 0 'Clear the current string, so if we have something like: , "Hello World" the result is "Hello World" instead of " Hello World"

                Else

                    curStr.Append(c)

                End If

            End If

        Next

        al.Add(curStr.ToString) 'Add to arraylist

        curStr.Length = 0 'Clear current string

        Return CType(al.ToArray(GetType(String)), String())

    End Function

End Class





