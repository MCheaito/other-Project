Attribute VB_Name = "Module1"
Public Declare Function ConvertToPDF Lib "..\Debug\FreeDoc.dll" _
(ImageFileName As String, pdfFileName As String, Author As String, Creator As String, Title As String, Subject As String) As Integer



Public Declare Sub DisplayStringByRef Lib "..\Debug\FreeDoc.dll" _
   (sMyString As String)
Public Declare Sub DisplayStringByVal Lib "..\Debug\FreeDoc.dll" _
   (ByVal sMyString As String)
Public Declare Sub FillString Lib "..\Debug\FreeDoc.dll" _
   (ByVal sMyString As String, ByVal cBufferSize As Long)
Public Declare Function InStrRev Lib "..\Debug\FreeDoc.dll" _
   (ByVal sMyString As String, ByVal iChar As Integer) _
   As Long

Public Sub go()
Dim i As Integer
Dim pdfFile As String
    pdfFile = Space$(512)
    i = ConvertToPDF("c:\image.jpg", "D:\mouhamad\project\accounting\files\test.pdf", "Mouhamad cheaito", "FreeDoc", "Test", "à plus")
    
End Sub


Public Sub go1()
         sFillTest = Space$(260)
         FillString sFillTest, 260
         MsgBox Trim$(sFillTest), vbInformation, "Fill String"

End Sub
