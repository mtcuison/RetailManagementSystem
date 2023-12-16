Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcRetailParams

Public Class frmMachine
    Private Const pxeTableName As String = "Cash_Reg_Machine"

    Private pnLoadx As Integer
    Private poControl As Control

    Private WithEvents p_oRecord As clsMachine
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
            p_oRecord = New clsMachine(p_oAppDriver, True)
            p_oRecord.InitRecord()

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
                Case 9
                    If Not IsNumeric(loTxt.Text) Then
                        p_oRecord.Master(9) = 0
                        loTxt.Text = Format(0, xsDECIMAL)
                    Else
                        p_oRecord.Master(9) = loTxt.Text
                    End If
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
                Case 8
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
                Case 0, 1, 2, 3, 4, 5, 6
                    p_oRecord.Master(loIndex) = loTxt.Text
                Case 8
                    If Not IsDate(loTxt.Text) Then loTxt.Text = p_oAppDriver.SysDate

                    p_oRecord.Master(loIndex) = loTxt.Text
                    loTxt.Text = Format(p_oRecord.Master(loIndex), "MMM dd, yyyy")
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
                    If .SaveRecord() Then clearFields()
                Case 3 'search
                Case 4 'browse
                    If .BrowseRecord Then loadMaster()
                Case 5 'cancel
                    If .CancelUpdate() Then clearFields()
                Case 6 'update
                    .UpdateRecord()
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

    Private Sub initButton()
        Dim lbShow As Integer
        Dim lnEditMode As xeEditMode = p_oRecord.EditMode

        lbShow = (lnEditMode = 1 Or lnEditMode = 2)

        cmdButton02.Visible = lbShow
        cmdButton03.Visible = lbShow
        cmdButton05.Visible = lbShow
        GroupBox1.Enabled = lbShow

        cmdButton01.Visible = Not lbShow
        cmdButton06.Visible = Not lbShow
        cmdButton07.Visible = Not lbShow
        cmdButton08.Visible = Not lbShow

        If txtField00.Text = "" Then
            txtField00.ReadOnly = False
            txtField00.Focus()
        Else
            txtField00.ReadOnly = True
            txtField01.Focus()
        End If

        If lbShow Then txtField00.Focus()
    End Sub

    Private Sub clearFields()
        txtField00.Text = ""
        txtField01.Text = ""
        txtField02.Text = ""
        txtField03.Text = ""
        txtField04.Text = ""
        txtField05.Text = ""
        txtField06.Text = ""
        txtField08.Text = ""
        txtField09.Text = 0.0

        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub loadMaster()
        With p_oRecord
            txtField00.MaxLength = .MasFldSze(0)
            txtField01.MaxLength = .MasFldSze(1)
            txtField02.MaxLength = .MasFldSze(2)
            txtField03.MaxLength = .MasFldSze(3)
            txtField04.MaxLength = .MasFldSze(4)
            txtField05.MaxLength = .MasFldSze(5)

            txtField00.Text = .Master("sIDNumber")
            txtField01.Text = .Master("sAccredtn")
            txtField02.Text = .Master("sApproval")
            txtField03.Text = .Master("sPermitNo")
            txtField04.Text = .Master("nPOSNumbr")
            txtField05.Text = .Master("sORNoxxxx")
            txtField06.Text = Format(.Master("nSalesTot"), "#,#0.00")
            txtField08.Text = Format(.Master("dExpiratn"), "MMM dd, yyyy")
            txtField09.Text = Format(.Master("nSChargex"), xsDECIMAL)

            ComboBox1.SelectedIndex = IIf(.Master("cTranMode") = "D", 0, 1)
        End With
    End Sub

    Private Sub ComboBox1_LostFocus(sender As Object, e As System.EventArgs) Handles ComboBox1.LostFocus
        p_oRecord.Master("cTranMode") = IIf(ComboBox1.SelectedIndex = 0, "D", "A")
    End Sub
End Class