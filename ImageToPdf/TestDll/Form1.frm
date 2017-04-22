VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3090
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3090
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton Command3 
      Caption         =   "Command3"
      Height          =   975
      Left            =   2520
      TabIndex        =   2
      Top             =   360
      Width           =   1815
   End
   Begin VB.CommandButton Command2 
      Caption         =   "Command2"
      Height          =   855
      Left            =   240
      TabIndex        =   1
      Top             =   1320
      Width           =   1935
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Command1"
      Height          =   855
      Left            =   240
      TabIndex        =   0
      Top             =   240
      Width           =   1935
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
     Private Sub Command1_Click()
         Dim sTestString1 As String
         Dim sTestString2 As String

         sTestString1 = "This is my string passed to the dll by value."
         DisplayStringByVal sTestString1

         sTestString2 = "This is my string passed to the dll by reference."
         DisplayStringByRef sTestString2

      End Sub

      Private Sub Command2_Click()
         Dim sFillTest As String

         sFillTest = Space$(260)
         FillString sFillTest, 260
         MsgBox Trim$(sFillTest), vbInformation, "Fill String"
      End Sub

      Private Sub Command3_Click()
         Dim sPathString As String
         Dim sMsg As String
         Dim lCharPosition As Long

         sPathString = "C:\My Documents\Temp\Item.txt"
         lCharPosition = InStrRev(sPathString, Asc("\"))

         If CBool(lCharPosition) Then
            sMsg = "The file '" & Mid$(sPathString, lCharPosition + 1)
            sMsg = sMsg & "' is at this location:" & vbCrLf & vbCrLf
            sMsg = sMsg & Left$(sPathString, lCharPosition - 1)
            MsgBox sMsg, vbInformation, "InStrRev"
         Else
            MsgBox "Cannot find '/' in " & sPathString, vbCritical
        End If

      End Sub


