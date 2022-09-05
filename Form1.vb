Option Strict Off
Option Explicit On
Friend Class Form1
	Inherits System.Windows.Forms.Form
	Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
		Text2.Text = GetPDFFormFields(Text1.Text, 2)
	End Sub
	
	Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click
		Text2.Text = GetPDFFormFields(Text1.Text, 1)
	End Sub
	
	Private Sub Command3_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command3.Click
		Text2.Text = GetPDFFField(Text1.Text, Text3.Text) 'Get...
	End Sub
	
	Private Sub option1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles option1.Click
		Text2.Text = GetPDFFormFields(Text1.Text, 3)
	End Sub
	
	Private Sub option2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles option2.Click
		Text2.Text = CStr(GetPDFffCount(Text1.Text))
	End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            Text1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Text2.Text = SetPDFFField(Text1.Text, Text3.Text, TextBox1.Text) 'Set...
    End Sub
End Class