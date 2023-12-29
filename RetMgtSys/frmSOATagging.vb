'Imports ggcAppDriver

'Public Class frmSOATagging
'    Private WithEvents oTrans As TLMSalesMC
'    Private pnLoadx As Integer
'    Private pnIndex As Integer
'    Private poControl As Control

'    Private Sub frmOGCallCustInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'        If pnLoadx = 0 Then
'            oTrans = New TLMSalesMC(p_oAppDriver)

'            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
'            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
'            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

'            initButton()

'            pnLoadx = 1
'        End If
'    End Sub

'    Private Sub loadMaster(ByVal loControl As Control)
'        Dim loTxt As Control

'        For Each loTxt In loControl.Controls
'            If loTxt.HasChildren Then
'                Call loadMaster(loTxt)
'            Else
'                If (TypeOf loTxt Is TextBox) Then
'                    Dim loIndex As Integer
'                    loIndex = Val(Mid(loTxt.Name, 9))
'                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtfield" Then
'                        Select Case loIndex
'                            Case 1
'                                If oTrans.Master(8) <> "" Then
'                                    loTxt.Text = Format(CDate(oTrans.Master(8)), "MMMM dd, yyyy")
'                                Else
'                                    loTxt.Text = oTrans.Master(8)
'                                End If
'                            Case 0
'                                loTxt.Text = oTrans.Master(loIndex)
'                            Case 2
'                                loTxt.Text = oTrans.Master(loIndex)
'                            Case 4
'                                If oTrans.Master(loIndex) <> "" Then
'                                    txtField03.Text = Format(CDate(oTrans.Master(loIndex)), "MMMM dd, yyyy")
'                                Else
'                                    txtField03.Text = ""
'                                End If
'                            Case 6
'                                If oTrans.Master(loIndex) <> "" Then
'                                    loTxt.Text = InquiryStatus(oTrans.Master(loIndex))
'                                Else
'                                    loTxt.Text = oTrans.Master(loIndex)
'                                End If
'                            Case 7
'                                loTxt.Text = oTrans.Master("sAgentNme")
'                            Case 8
'                                loTxt.Text = oTrans.Master("sRemarksx")
'                            Case Else
'                                loTxt.Text = oTrans.Master(loIndex)
'                        End Select
'                    End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
'                End If '(TypeOf loTxt Is TextBox)
'            End If 'If loTxt.HasChildren
'        Next 'loTxt In loControl.Controls
'        If oTrans.Master("cTranStat") <> "" Then
'            lblStatus.Text = TranStatus(oTrans.Master("cTranStat"))
'        Else
'            lblStatus.Text = "UNKNOWN"
'        End If
'        If oTrans.EditMode = xeEditMode.MODE_READY Then
'            txtField01.ReadOnly = True
'            txtField02.ReadOnly = True
'            txtField02.ReadOnly = True
'            txtField80.ReadOnly = True
'            txtField81.ReadOnly = True
'        End If

'    End Sub

'    Private Sub clearFields()
'        txtField00.Text = ""
'        txtField01.Text = ""
'        txtField02.Text = ""
'        txtField02.Text = ""
'        txtField03.Text = ""
'        txtField04.Text = ""
'        txtField06.Text = ""
'        txtField07.Text = ""
'        txtField08.Text = ""

'        txtField80.Text = ""
'        txtField81.Text = ""
'        txtfield82.Text = ""
'        txtField83.Text = ""
'        txtField84.Text = ""
'        txtField85.Text = ""
'        txtField86.Text = ""
'        txtField87.Text = ""
'        lblStatus.Text = "UNKNOWN"

'    End Sub

'    Private Sub initButton()
'        Dim lbShow As Boolean
'        lbShow = oTrans.EditMode = xeEditMode.MODE_ADDNEW

'        cmdButton01.Visible = lbShow
'        cmdButton02.Visible = lbShow
'        cmdButton04.Visible = lbShow
'        cmdButton00.Visible = Not lbShow
'        cmdButton03.Visible = Not lbShow
'        cmdButton05.Visible = Not lbShow
'        cmdButton06.Visible = Not lbShow
'        cmdButton06.Visible = Not lbShow
'        cmdButton07.Visible = Not lbShow
'        lblStatus.Visible = Not lbShow

'        GroupBox1.Enabled = lbShow
'        GroupBox3.Enabled = lbShow


'        txtField01.ReadOnly = Not lbShow
'        txtField02.ReadOnly = Not lbShow
'        txtField02.ReadOnly = Not lbShow
'        txtField80.ReadOnly = Not lbShow
'        txtField81.ReadOnly = Not lbShow


'        If oTrans.EditMode = xeEditMode.MODE_ADDNEW Then
'            txtField01.Focus()
'        End If

'    End Sub

'    Private Sub NewTransaction()
'        clearFields()

'        If oTrans.NewTransaction() Then
'            loadMaster(Me)
'            initButton()
'        End If

'    End Sub

'    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'        Dim loChk As Button
'        loChk = CType(sender, System.Windows.Forms.Button)

'        Dim lnIndex As Integer
'        lnIndex = Val(Mid(loChk.Name, 10))

'        Select Case lnIndex
'            Case 0 'new
'                NewTransaction()
'            Case 1 'save
'                Call validateControl()
'                If oTrans.SaveTransaction Then
'                    MsgBox("Transaction Saved Successfuly.", MsgBoxStyle.Information, "Notice")
'                    NewTransaction()
'                End If

'            Case 2 'search
'                Select Case pnIndex
'                    Case 2
'                    Case 82
'                    Case 83
'                End Select
'            Case 3 'close
'                Me.Close()
'            Case 4 'cancel
'                oTrans = Nothing
'                oTrans = New TLMSalesMC(p_oAppDriver)
'                clearFields()
'                initButton()
'            Case 5 'cancel
'                If oTrans.SearchTransaction("", False) Then
'                    loadMaster(Me)
'                Else
'                    clearFields()
'                End If
'            Case 6 'Approved
'                If txtField00.Text <> "" Then
'                    If MsgBox("Do you want to confirm this Trasnaction?", vbQuestion + vbYesNo, "Confirm") = vbYes Then
'                        If LCase(p_oAppDriver.ProductID) = "telemktg" Then
'                            If oTrans.CloseTransaction() Then
'                                MsgBox("Transaction Approved Successfuly.", MsgBoxStyle.Information, "Notice")
'                                oTrans = Nothing
'                                oTrans = New TLMSalesMC(p_oAppDriver)
'                                clearFields()
'                                initButton()
'                            End If
'                        Else
'                            If oTrans.PostTransaction() Then
'                                MsgBox("Transaction Posted Successfuly.", MsgBoxStyle.Information, "Notice")
'                                oTrans = Nothing
'                                oTrans = New TLMSalesMC(p_oAppDriver)
'                                clearFields()
'                                initButton()
'                            End If
'                        End If
'                    End If
'                End If
'            Case 7 'Dissapproved
'                If txtField00.Text <> "" Then
'                    If MsgBox("Do you want to approved this Trasnaction?", vbQuestion + vbYesNo, "Confirm") = vbYes Then
'                        If oTrans.CancelTransaction() Then
'                            MsgBox("Transaction Dissapproved Successfuly.", MsgBoxStyle.Information, "Notice")
'                            oTrans = Nothing
'                            oTrans = New TLMSalesMC(p_oAppDriver)
'                            clearFields()
'                            initButton()
'                        End If
'                    End If
'                End If
'        End Select
'    End Sub

'    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
'        Dim loTxt As TextBox
'        loTxt = CType(sender, System.Windows.Forms.TextBox)

'        Dim loIndex As Integer
'        loIndex = Val(Mid(loTxt.Name, 9))

'        If Mid(loTxt.Name, 1, 8) = "txtField" Then
'            Select Case loIndex
'                Case 1
'                    If loTxt.Text <> "" Then
'                        If IsDate(loTxt.Text) Then
'                            loTxt.Text = Format(CDate(oTrans.Master(8)), "yyyy/MM/dd")
'                        Else
'                            loTxt.Text = ""
'                        End If
'                    End If
'            End Select
'        End If

'        poControl = loTxt

'        loTxt.BackColor = Color.Azure
'        loTxt.SelectAll()
'    End Sub

'    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
'        Dim loTxt As TextBox
'        loTxt = CType(sender, System.Windows.Forms.TextBox)

'        Dim loIndex As Integer
'        loIndex = Val(Mid(loTxt.Name, 9))

'        Select Case loIndex
'            Case 1
'                If IsDate(loTxt.Text) Then
'                    loTxt.Text = Format(CDate(loTxt.Text), "yyyy-MM-dd")
'                    oTrans.Master(8) = loTxt.Text
'                    loTxt.Text = Format(CDate(oTrans.Master(8)), "MMMM dd, yyyy")

'                Else
'                    loTxt.Text = ""
'                End If
'            Case 8
'                If (loTxt.Text <> "") Then
'                    oTrans.Master(9) = loTxt.Text
'                End If
'        End Select

'        pnIndex = loIndex

'        loTxt.BackColor = SystemColors.Window
'        poControl = Nothing
'    End Sub

'    Private Sub oTrans_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles oTrans.MasterRetrieved
'        Select Case Index
'            Case 2
'                txtField02.Text = Value
'            Case 3
'                txtField02.Text = Value
'            Case 4
'                If Value <> "" Then
'                    txtField03.Text = Format(CDate(Value), "MMM dd, yyyy")
'                Else
'                    txtField03.Text = ""
'                End If
'            Case 5
'                txtField04.Text = Value
'            Case 6
'                If Value <> "" Then
'                    txtField06.Text = CallOutStatus(Value)
'                End If
'            Case 7
'                txtField07.Text = Value
'            Case 8
'                txtField01.Text = Value
'            Case 9
'                txtField08.Text = Value
'            Case 80
'                txtField80.Text = Value
'            Case 81
'                txtField81.Text = Value
'            Case 82
'                txtfield82.Text = Value
'            Case 83
'                txtField83.Text = Value
'            Case 84
'                txtField84.Text = Value
'            Case 85
'                txtField85.Text = Value
'            Case 86
'                txtField86.Text = Value
'            Case 87
'                txtField87.Text = Value
'        End Select
'    End Sub

'    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtField02.KeyDown, txtField02.KeyDown
'        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Return Then
'            Dim loTxt As TextBox
'            loTxt = CType(sender, System.Windows.Forms.TextBox)

'            Dim loIndex As Integer
'            loIndex = Val(Mid(loTxt.Name, 9))

'            If Mid(loTxt.Name, 1, 8) = "txtField" Then
'                Select Case loIndex
'                    Case 2
'                        If (txtField01.Text = "") Then
'                            MsgBox("Please input Date!!", MsgBoxStyle.Information, "Notice")
'                            Exit Sub
'                        End If
'                        If (txtField02.Text <> "") Then oTrans.SearchMaster(loIndex, loTxt.Text)
'                    Case 3
'                        If (txtField02.Text = "") Then
'                            MsgBox("Please input Date!!", MsgBoxStyle.Information, "Notice")
'                            Exit Sub
'                        End If
'                        If (txtField02.Text <> "") Then oTrans.SearchMaster(loIndex, loTxt.Text)
'                    Case 8
'                        If (txtField08.Text = "") Then
'                            MsgBox("Please input Note!!", MsgBoxStyle.Information, "Notice")
'                            Exit Sub
'                        End If

'                    Case 80
'                        oTrans.SearchMaster(loIndex, loTxt.Text)
'                    Case 81
'                        If (txtField80.Text = "") Then
'                            MsgBox("Please input Branch !!", MsgBoxStyle.Information, "Notice")
'                            Exit Sub
'                        End If
'                        If (txtField81.Text <> "") Then oTrans.SearchMaster(loIndex, loTxt.Text)
'                End Select
'            End If
'        End If
'    End Sub


'    'call this command to validate values since we are using lostfocus as substitute for validate
'    Private Sub validateControl()
'        txtField01.Focus()


'    End Sub

'End Class