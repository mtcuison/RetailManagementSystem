Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcRetailParams

Public Class frmSalesPromo
    Private pnLoadx As Integer
    Private poControl As Control

    Private WithEvents p_oRecord As clsSalesPromo
    Private p_nEditMode As Integer
    Private pnSeek As Integer
    Private pnIndx As Integer

    Private Sub frmSalesPromo_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmSalesPromo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmSalesPromo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If pnLoadx = 0 Then
            p_oRecord = New clsSalesPromo(p_oAppDriver)

            'Set event Handler for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSeeks", "KeyDown", AddressOf txtSeeks_KeyDown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            clearFields()
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
                Case 1, 5 To 14, 80 To 83
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
                Case 13, 14
                    loTxt.Text = Format(p_oRecord.Master(loIndex), "yyyy/MM/dd")
            End Select
        End If

        pnIndx = loIndex

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
                    Case 80 To 83
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
                Case 13, 14
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
                    If p_oRecord.NewRecord Then
                        clearFields()
                        loadMaster()
                    End If
                Case 2 'save
                    If .SaveRecord Then
                        MsgBox("Record Saved Successfuly.", MsgBoxStyle.Information, "Success")

                        clearFields()
                        If .OpenRecord(.Master("sTransNox")) Then loadMaster()
                    End If
                Case 3 'search
                    Dim loTxt As TextBox

                    loTxt = CType(FindTextBox(Me, "txtField" & Format(pnIndx, "00")), TextBox)

                    Select Case pnIndx
                        Case 80 To 83
                            p_oRecord.SearchMaster(pnIndx, "%")

                            loTxt.Focus()
                    End Select
                    GoTo endProc
                Case 4 'browse
                    If pnSeek = 1 Then
                        If p_oRecord.SearchRecord(txtSeeks01.Text, True) Then loadMaster()
                    Else
                        If p_oRecord.SearchRecord(txtSeeks02.Text, False) Then loadMaster()
                    End If
                Case 5 'cancel
                    If p_oRecord.CancelUpdate() Then clearFields()
                Case 6 'update
                    p_oRecord.UpdateRecord()
                Case 7 'delete
                    If MsgBox("Do you want to disable this item?", MsgBoxStyle.Question & MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                        If p_oRecord.CancelRecord Then
                            MsgBox("Record has disabled successfuly.", MsgBoxStyle.Information, "Notice")
                            clearFields()
                        End If
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

    Private Sub txtSeeks_GotFocus(sender As Object, e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
            Select Case loIndex
                Case 1, 2
                    pnSeek = loIndex
            End Select
        End If

        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtSeeks_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
            Select Case loIndex
            End Select
        End If

        loTxt.BackColor = SystemColors.Window
        poControl = Nothing
    End Sub

    Private Sub txtSeeks_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As TextBox
            loTxt = CType(sender, System.Windows.Forms.TextBox)
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
                Select Case loIndex
                    Case 1
                        If p_oRecord.SearchRecord(txtSeeks01.Text, True) Then
                            loadMaster()
                        Else
                            SetNextFocus()
                        End If
                    Case 2
                        If p_oRecord.SearchRecord(txtSeeks02.Text, False) Then
                            loadMaster()
                        Else
                            SetNextFocus()
                        End If
                End Select
            End If
        End If
    End Sub

    Private Sub initButton()
        Dim lbShow As Integer
        Dim lnEditMode As xeEditMode = p_oRecord.EditMode

        lbShow = (lnEditMode = 1 Or lnEditMode = 2)

        cmdButton02.Visible = lbShow
        cmdButton03.Visible = lbShow
        cmdButton05.Visible = lbShow
        Panel1.Enabled = lbShow
        Panel2.Enabled = lbShow

        cmdButton01.Visible = Not lbShow
        cmdButton06.Visible = Not lbShow
        cmdButton07.Visible = Not lbShow
        cmdButton08.Visible = Not lbShow
        Panel3.Enabled = Not lbShow

        If lbShow Then
            txtField01.Focus()
        Else
            txtSeeks01.Focus()
        End If
    End Sub

    Private Sub loadMaster()
        With p_oRecord
            txtSeeks01.Text = ""
            txtSeeks02.Text = ""

            txtField00.Text = .Master(0)
            txtField01.Text = .Master(1)
            txtField80.Text = .Master(80)
            txtField81.Text = .Master(81)
            txtField82.Text = .Master(82)
            txtField83.Text = .Master(83)
            txtField13.Text = Format(.Master(13), "MMM dd, yyyy")
            txtField14.Text = Format(.Master(14), "MMM dd, yyyy")
            txtField11.Text = .Master(11).ToString
            txtField12.Text = .Master(12).ToString
            txtField05.Text = Format(.Master(5), "#,##0.00")
            txtField06.Text = Format(.Master(6), "#,##0.00")
            txtField07.Text = .Master(7)
            txtField08.Text = Format(.Master(8), "#,##0.00")
            txtField09.Text = Format(.Master(9), "#,##0.00")
            txtField10.Text = .Master(10)

            CheckBox1.Checked = IIf(.Master(15) = 1, True, False)
        End With
    End Sub

    Private Sub clearFields()
        txtSeeks01.Text = ""
        txtSeeks02.Text = ""

        txtField00.Text = ""
        txtField01.Text = ""
        txtField80.Text = ""
        txtField81.Text = ""
        txtField82.Text = ""
        txtField83.Text = ""
        txtField13.Text = ""
        txtField14.Text = ""
        txtField11.Text = ""
        txtField12.Text = ""
        txtField05.Text = "0.00"
        txtField06.Text = "0.00"
        txtField07.Text = "0"
        txtField08.Text = "0.00"
        txtField09.Text = "0.00"
        txtField10.Text = "0"

        CheckBox1.Checked = False
    End Sub

    Private Sub p_oRecord_MasterRetrieved(Index As Integer, Value As Object) Handles p_oRecord.MasterRetrieved
        Dim loTxt As TextBox

        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)

        Select Case Index
            Case 13, 14
                loTxt.Text = Format(Value, "MMM dd, yyyy")
            Case 5, 6, 8, 9
                loTxt.Text = Format(Value, "#,##0.00")
            Case 80 To 85
                loTxt.Text = Value
            Case 11, 12
                loTxt.Text = Value.ToString
            Case Else
                loTxt.Text = Value
        End Select
    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As System.EventArgs) Handles CheckBox1.Click
        p_oRecord.Master("cRecdStat") = IIf(CheckBox1.Checked, "1", "0")
    End Sub
End Class