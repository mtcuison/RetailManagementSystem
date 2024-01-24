Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcRetailParams

Public Class frmDeliveryService
    Private Const pxeTableName As String = "Delivery_Service"

    Private pnLoadx As Integer
    Private poControl As Control

    Private pnIndx As Integer
    Private WithEvents p_oRecord As ggcRetailParams.clsDeliveryServiceParam
    Private p_nEditMode As Integer
    Private prevServiceCharge As Decimal
    Private prevDteEffective As Date
    Private p_bNew As Boolean
    Private Sub frmDeliveryService_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            pnLoadx = 2
        End If
    End Sub
    Private Sub frmDeliveryService_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Up, Keys.Down
                Select Case e.KeyCode
                    Case Keys.Return, Keys.Down
                        SetNextFocus()
                    Case Keys.Up
                        SetPreviousFocus()
                End Select
        End Select
    End Sub

    Private Sub frmDeliveryService_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If pnLoadx = 0 Then
            p_oRecord = New ggcRetailParams.clsDeliveryServiceParam(p_oAppDriver, True)
            'p_oHisRecord = New clsDeliveryServiceHisParam(p_oAppDriver, True)
            p_oRecord.InitRecord()
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            initButton()
            p_bNew = False
            pnLoadx = 1
        End If
    End Sub
    Private Sub initButton()
        Dim lbShow As Integer
        Dim lnEditMode As xeEditMode = p_oRecord.EditMode

        lbShow = (lnEditMode = 1 Or lnEditMode = 2)

        cmdButton02.Visible = lbShow
        'cmdButton03.Visible = lbShow
        cmdButton05.Visible = lbShow
        GroupBox1.Enabled = lbShow

        cmdButton01.Visible = Not lbShow
        cmdButton06.Visible = Not lbShow
        cmdButton07.Visible = Not lbShow
        cmdButton08.Visible = Not lbShow

        If lbShow Then txtField01.Focus()
    End Sub
    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" Then
            'Select Case loIndex
            '    Case 1, 2, 3, 17, 8 To 14, 80 To 85
            p_oRecord.Master(loIndex) = loTxt.Text
            'End Select
        End If
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        With p_oRecord
            Select Case lnIndex
                Case 1 'new
                    If .NewRecord() Then
                        p_bNew = True
                        clearFields()
                        loadMaster()
                    End If
                Case 2 'save
                    If isEntryOkay() Then
                        With p_oRecord
                            If .SaveRecord() Then
                                'p_oHisRecord.SaveRecord()
                                clearFields()
                            End If
                        End With
                    End If

                Case 3 'search
                Case 4 'browse
                    If .BrowseRecord Then
                        loadMaster()
                    End If
                Case 5 'cancel
                    If .CancelUpdate() Then clearFields()
                Case 6 'update

                    If Not txtField00.Text = "" Then
                        If Not p_oAppDriver.getUserApproval Then Exit Sub
                        updateMaster()
                        .UpdateRecord()
                    End If

                Case 7 'delete
                    If .DeleteRecord() Then clearFields()
                Case 8 'close
                    Me.Close()
                    GoTo endProc
            End Select
        End With

        initButton()
endProc:
        Exit Sub
    End Sub
    Private Sub loadMaster()
        With p_oRecord
            txtField00.Text = .Master("sRiderIDx")
            txtField01.Text = IFNull(.Master("sBriefDsc"))
            txtField02.Text = IFNull(.Master("sDescript"))
            txtField03.Text = Format(.Master("dPartnerx"), "MMM dd, yyyy")
            txtField04.Text = IFNull(.Master("nSrvcChrg"))
            txtField05.Text = IFNull(Format(.Master("dSrvcChrg"), "MMM dd, yyyy"))
            chkField.Checked = .Master("cRecdStat")

            prevServiceCharge = IFNull(.Master("nSrvcChrg"))
            prevDteEffective = Format(.Master("dSrvcChrg"), "MMM dd, yyyy")
        End With
    End Sub
    Private Function isEntryOkay() As Boolean
        Dim lsProcName As String = pxeTableName & "." & "SaveRecord"
        If Not p_bNew = True Then Return True
        With p_oRecord
            Dim currentServiceCharge As String = txtField04.Text
            Dim currentDteEffective As String = txtField05.Text

            If currentServiceCharge <> prevServiceCharge Then
                txtField04.Text = IFNull(.Master("nSrvcChrg"))
                Return True
            ElseIf currentDteEffective <> prevDteEffective Then
                txtField05.Text = Format(.Master("dSrvcChrg"), "MMM dd, yyyy")
                Return True
            Else
                MsgBox(lsProcName & vbCrLf & "No modifications have been made for this update.", MsgBoxStyle.Critical, "Warning")
                Return False
            End If
        End With
    End Function


    Private Sub updateMaster()
        txtField00.Enabled = False
        txtField01.ReadOnly = True
        txtField02.ReadOnly = True
        txtField03.ReadOnly = True
        p_bNew = False
    End Sub
    Private Sub clearFields()
        txtField00.Text = ""
        txtField01.Text = ""
        txtField02.Text = ""
        txtField03.Text = ""
        txtField04.Text = ""
        txtField05.Text = ""
        p_bNew = False
        chkField.Checked = False

    End Sub
    Private Sub chkField_Click(sender As Object, e As System.EventArgs) Handles chkField.Click
        p_oRecord.Master(2) = IIf(chkField.Checked, 1, 0)
    End Sub

    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" Then
            Select Case loIndex
                Case 1, 2, 4
                Case 3, 5
                    loTxt.Text = Format(p_oRecord.Master(loIndex), "yyyy/MM/dd")
            End Select
        End If

        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As TextBox
            loTxt = CType(sender, System.Windows.Forms.TextBox)
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 1
                End Select
            End If
        End If
    End Sub

    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" Then
            Select Case loIndex
                Case 1, 2, 4
                    p_oRecord.Master(loIndex) = loTxt.Text
                Case 3, 5
                    If Not IsDate(loTxt.Text) Then loTxt.Text = p_oAppDriver.SysDate

                    p_oRecord.Master(loIndex) = loTxt.Text
                    loTxt.Text = Format(p_oRecord.Master(loIndex), "MMM dd, yyyy")
            End Select
        End If

        loTxt.BackColor = SystemColors.Window
        poControl = Nothing
    End Sub


End Class