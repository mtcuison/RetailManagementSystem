Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcRetailParams

Public Class frmSysUser
    Private pnLoadx As Integer
    Private poControl As Control

    Private WithEvents p_oRecord As clsSysUser
    Private p_nEditMode As Integer

    Private Sub frmBanks_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmBank_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmBank_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If pnLoadx = 0 Then
            p_oRecord = New clsSysUser(p_oAppDriver)

            'Set event Handler for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            initButton()

            pnLoadx = 1
        End If
    End Sub

    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" Then
            Select Case loIndex
                Case 1, 2, 3, 4, 83, 84
                    p_oRecord.Master(loIndex) = loTxt.Text
            End Select
        End If
    End Sub

    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" Then
            Select Case loIndex
                Case 1
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
                    Case 3, 83, 84
                        p_oRecord.SearchMaster(loIndex, loTxt.Text)
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
                Case 1
                    p_oRecord.Master(loIndex) = loTxt.Text
            End Select
        End If

        loTxt.BackColor = SystemColors.Window
        poControl = Nothing
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
                        clearFields()
                        loadMaster()
                    End If
                Case 2 'save
                    If .SaveRecord() Then
                        MsgBox("Successfully Save!", vbCritical + vbInformation, "Information")
                        clearFields()
                    Else
                        MsgBox("Unable to Save!" + vbCrLf + "Please check entry!", vbCritical, "Error")
                    End If
                Case 3 'search
                Case 4 'browse
                    If .SearchRecord("", False) Then loadMaster()
                Case 5 'cancel
                    If .CancelUpdate() Then clearFields()
                Case 6 'update
                    .UpdateRecord()
                Case 7 'cancel record
                    If .CancelRecord() Then
                        MsgBox("Successfully Save!", vbCritical + vbInformation, "Information")
                        clearFields()
                    Else
                        MsgBox("Unable to Save!" + vbCrLf + "Please check entry!", vbCritical, "Error")
                    End If
                Case 8 'close
                    Me.Close()
                    GoTo endProc
            End Select
        End With

        initButton()
endProc:
        Exit Sub
    End Sub

    Private Sub initButton()
        Dim lbShow As Integer
        Dim lnEditMode As xeEditMode = p_oRecord.EditMode

        lbShow = (lnEditMode = 1 Or lnEditMode = 2)

        cmdButton02.Visible = lbShow
        cmdButton03.Visible = lbShow
        cmdButton05.Visible = lbShow
        Panel2.Enabled = lbShow

        cmdButton01.Visible = Not lbShow
        cmdButton06.Visible = Not lbShow
        cmdButton07.Visible = Not lbShow
        cmdButton08.Visible = Not lbShow

        If lbShow Then txtField01.Focus()
    End Sub

    Private Sub clearFields()
        txtField00.Text = ""
        txtField01.Text = ""
        txtField02.Text = ""
        txtField03.Text = ""
        txtField04.Text = ""
        txtField79.Text = ""
        txtField81.Text = ""
        txtField82.Text = ""
        txtField83.Text = ""

        cmbField06.SelectedItem = 1
        cmbField07.SelectedItem = 1

        chkField11.Checked = False
        chkField12.Checked = False
        chkField15.Checked = False
    End Sub

    Private Sub loadMaster()
        Dim nUserLevel As Integer
        With cmbField06
            Dim comboSource As New Dictionary(Of String, String)()
            comboSource.Add("1", "Encoder")
            comboSource.Add("2", "Supervisor")
            comboSource.Add("3", "Manager")
            comboSource.Add("4", "Auditor")

            If p_oAppDriver.UserLevel = xeUserRights.ENGINEER Then
                comboSource.Add("5", "Sys Admin")
                comboSource.Add("6", "Owner")
                comboSource.Add("7", "Engineer")
            End If

            .DataSource = New BindingSource(comboSource, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With

        With p_oRecord
            txtField00.Text = .Master("sUserIDxx")
            txtField01.Text = .Master("sLogNamex")
            txtField02.Text = .Master("sPassword")
            txtField79.Text = .Master("sPassword")
            txtField03.Text = .Master("sUserName")

            If .Master("sEmployNo") <> "" Then
                txtField03.Text = .Master("sClientNm")
                txtField04.Text = .Master("sEmployNo")
                txtField81.Text = .Master("sDeptName")
                txtField82.Text = .Master("sPositnNm")
            Else
                txtField03.Text = ""
                txtField04.Text = ""
                txtField81.Text = ""
                txtField82.Text = ""
            End If

            txtField83.Text = getBranch(IFNull(.Master("sBranchCD"), ""))
            txtField84.Text = .Master(8)

            nUserLevel = .Master("nUserLevl")

            Select Case nUserLevel
                Case 1
                    nUserLevel = 0
                Case 2
                    nUserLevel = 1
                Case 4
                    nUserLevel = 2
                Case 8
                    nUserLevel = 3
                Case 16
                    nUserLevel = 4
                Case 32
                    nUserLevel = 5
                Case 64
                    nUserLevel = 6
            End Select

            cmbField06.SelectedIndex = nUserLevel
            cmbField07.SelectedIndex = .Master("cUserType")

            chkField11.Checked = .Master("cAllwLock")
            chkField12.Checked = .Master("cAllwView")
            chkField15.Checked = .Master("cUserStat")
        End With

        'txtField83.Enabled = p_oAppDriver.UserLevel = xeUserRights.ENGINEER
        'txtField84.Enabled = p_oAppDriver.UserLevel = xeUserRights.ENGINEER
    End Sub

    Private Function getBranch(ByVal fsBranchCd As String) As String
        Dim loDT As DataTable
        Dim lsSQL = "SELECT sBranchNm FROM sBranchCD" + fsBranchCd

        loDT = p_oAppDriver.ExecuteQuery(lsSQL)
        If loDT.Rows.Count = 1 Then
            Return loDT(0).Item("sBranchNm")
        Else
            Return ""
        End If

    End Function


    Private Sub chkField_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkField15.Click
        p_oRecord.Master(2) = IIf(chkField15.Checked, 1, 0)
    End Sub

    Private Sub p_oRecord_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles p_oRecord.MasterRetrieved
        Dim loTxt As TextBox

        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)

        Select Case Index
            Case 3, 4, 81 To 84
                loTxt.Text = Value
        End Select
    End Sub

    Private Sub cmbField07_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbField07.LostFocus
        p_oRecord.Master(7) = cmbField07.SelectedIndex
    End Sub

    Private Sub cmbField06_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbField06.LostFocus
        p_oRecord.Master(6) = cmbField06.SelectedIndex
    End Sub

    Private Sub cmbField06_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbField06.SelectedIndexChanged

    End Sub
End Class