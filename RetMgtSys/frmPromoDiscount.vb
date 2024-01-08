Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcRetailParams

Public Class frmPromoDiscount
    Private pnLoadx As Integer
    Private poControl As Control

    Private WithEvents p_oRecord As clsPromoDiscount
    Private p_nEditMode As Integer
    Private pnSeek As Integer
    Private pnIndx As Integer
    Private p_nActiveRow As Integer

    Private Sub frmPromoDiscount_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmPromoDiscount_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmPromoDiscount_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If pnLoadx = 0 Then
            p_oRecord = New clsPromoDiscount(p_oAppDriver)

            'Set event Handler for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtDetail", "GotFocus", AddressOf txtDetail_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtDetail", "LostFocus", AddressOf txtDetail_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtDetail", "Validating", AddressOf txtDetail_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtDetail", "KeyDown", AddressOf txtDetail_KeyDown)

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
                Case 1 To 8
                    p_oRecord.Master(loIndex) = loTxt.Text
                Case 9, 10
                    If Not IsDate(loTxt.Text) Then loTxt.Text = Format(Now, "hh:mm")

                    p_oRecord.Master(loIndex) = loTxt.Text
                Case 11, 12
                    If Not IsDate(loTxt.Text) Then loTxt.Text = p_oAppDriver.SysDate

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
                    Case 1 To 4
                        p_oRecord.SearchMaster(loIndex, loTxt.Text)
                End Select
            End If
        End If
    End Sub

    Private Function isEntryOk()
        If txtField08.Text = CInt(0) Then
            MsgBox("Base Quantity cannot be zero! ." & vbCrLf &
                   "Please check entry and try again!", MsgBoxStyle.Information, "Error")
            txtField08.Focus()
            Return False
            Exit Function
        End If


        Return True
    End Function

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

    Private Sub txtDetail_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 10))

        If Mid(loTxt.Name, 1, 9) = "txtDetail" Then
            Select Case loIndex
                Case 1, 2, 3
                    p_oRecord.Detail(p_nActiveRow, loIndex) = loTxt.Text
                Case 4, 5, 6
                    If Not IsNumeric(loTxt.Text) Then loTxt.Text = 0

                    p_oRecord.Detail(p_nActiveRow, loIndex) = loTxt.Text
            End Select
        End If
    End Sub

    Private Sub txtDetail_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 10))

        If Mid(loTxt.Name, 1, 9) = "txtDetail" Then
            Select Case loIndex
            End Select
        End If

        pnIndx = loIndex

        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 10))

        If Mid(loTxt.Name, 1, 9) = "txtDetail" Then
            Select Case loIndex
                Case 1, 2, 3
                    If e.KeyCode = Keys.F3 Then
                        p_oRecord.searchDetail(p_nActiveRow, loIndex, loTxt.Text & "%")
                    ElseIf e.KeyCode = Keys.Enter Then
                        p_oRecord.searchDetail(p_nActiveRow, loIndex, loTxt.Text)
                    End If
            End Select
        End If
    End Sub

    Private Sub txtDetail_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 10))

        If Mid(loTxt.Name, 1, 9) = "txtDetail" Then
            Select Case loIndex
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
                        loadDetail()
                    End If
                Case 2 'save
                    If Not isEntryOk() Then Exit Sub
                    If Trim(.Detail(.ItemCount - 1, "sStockIDx")) = "" Then .DeleteDetail(.ItemCount - 1)

                    If .SaveRecord Then
                        MsgBox("Record Saved Successfuly.", MsgBoxStyle.Information, "Success")

                        clearFields()
                        If .OpenRecord(.Master("sTransNox")) Then loadMaster()
                    End If
                Case 3 'search
                    'Dim loTxt As TextBox

                    'loTxt = CType(FindTextBox(Me, "txtField" & Format(pnIndx, "00")), TextBox)

                    'Select Case pnIndx
                    '    Case 80 To 83
                    '        p_oRecord.SearchMaster(pnIndx, "%")

                    '        loTxt.Focus()
                    'End Select
                    'GoTo endProc
                Case 4 'browse
                    If pnSeek = 1 Then
                        If p_oRecord.SearchRecord(txtSeeks01.Text, True) Then
                            loadMaster()
                            loadDetail()
                        End If
                    Else
                        If p_oRecord.SearchRecord(txtSeeks02.Text, False) Then
                            loadMaster()
                            loadDetail()
                        End If
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
                Case 9
                    If .EditMode = xeEditMode.MODE_ADDNEW Or .EditMode = xeEditMode.MODE_UPDATE Then
                        If .Detail(.ItemCount - 1, "sStockIDx") <> "" Then
                            .newDetail()
                            loadDetail()
                        End If
                    End If

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
                            loadDetail()
                        Else
                            SetNextFocus()
                        End If
                    Case 2
                        If p_oRecord.SearchRecord(txtSeeks02.Text, False) Then
                            loadMaster()
                            loadDetail()
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
            txtField02.Text = .Master(2)
            txtField03.Text = .Master(3)
            txtField04.Text = .Master(4)
            txtField05.Text = Format(.Master(5), "#,##0.00")
            txtField06.Text = Format(.Master(6), "#,##0.00")
            txtField07.Text = .Master(7)
            txtField08.Text = .Master(8)
            txtField09.Text = Format(.Master(9), "hh:mm")
            txtField10.Text = Format(.Master(10), "hh:mm")
            txtField11.Text = Format(.Master(11), "MMM dd, yyyy")
            txtField12.Text = Format(.Master(12), "MMM dd, yyyy")

            CheckBox1.Checked = IIf(.Master("cRecdStat") = 1, True, False)
        End With
    End Sub

    Private Sub initGrid()
        With DataGridView1
            .RowCount = 0

            'Set No of Columns
            .ColumnCount = 3

            'Set Column Headers
            .Columns(0).HeaderText = "No"
            .Columns(1).HeaderText = "Category"
            .Columns(2).HeaderText = "Item Description"

            'Set Column Sizes
            .Columns(0).Width = 35
            .Columns(1).Width = 160
            .Columns(2).Width = 160

            'Set No of Rows
            .RowCount = 1
        End With
    End Sub

    Private Sub loadDetail()
        Dim lnCtr As Integer

        Call initGrid()

        With DataGridView1
            .RowCount = p_oRecord.ItemCount

            For lnCtr = 0 To p_oRecord.ItemCount - 1
                .Item(0, lnCtr).Value = lnCtr + 1
                .Item(1, lnCtr).Value = p_oRecord.Detail(lnCtr, 1)
                .Item(2, lnCtr).Value = p_oRecord.Detail(lnCtr, 3)
            Next

            If .RowCount > 0 Then
                p_nActiveRow = .RowCount - 1
                .ClearSelection()
                .Rows(p_nActiveRow).Selected = True

                Call setFieldInfo()
            End If
        End With
    End Sub

    Private Sub setFieldInfo()
        If txtField00.Text = "" Then Exit Sub
        With p_oRecord
            txtDetail01.Text = .Detail(p_nActiveRow, "sCategrDs")
            txtDetail02.Text = .Detail(p_nActiveRow, "sBarcodex")
            txtDetail03.Text = .Detail(p_nActiveRow, "sBriefDsc")
            txtDetail04.Text = Format(.Detail(p_nActiveRow, "nDiscRate"), "#,##0.00")

            txtDetail05.Text = Format(.Detail(p_nActiveRow, "nDiscAmtx"), "#,##0.00")
            txtDetail06.Text = .Detail(p_nActiveRow, "nMinQtyxx")

            txtDetail01.Focus()
        End With
    End Sub

    Private Sub DataGridView1_Click(sender As Object, e As System.EventArgs) Handles DataGridView1.Click
        p_nActiveRow = DataGridView1.CurrentRow.Index

        Call setFieldInfo()
    End Sub

    Private Sub clearFields()
        txtSeeks01.Text = ""
        txtSeeks02.Text = ""

        txtField00.Text = ""
        txtField01.Text = ""
        txtField02.Text = ""
        txtField03.Text = ""
        txtField04.Text = ""
        txtField05.Text = "0.00"
        txtField06.Text = "0.00"
        txtField07.Text = "0"
        txtField08.Text = "0"
        txtField09.Text = ""
        txtField10.Text = ""
        txtField11.Text = ""
        txtField12.Text = ""

        txtDetail01.Text = ""
        txtDetail02.Text = ""
        txtDetail03.Text = ""
        txtDetail04.Text = ""
        txtDetail05.Text = ""
        txtDetail06.Text = ""

        CheckBox1.Checked = False

        initGrid()
    End Sub

    Private Sub p_oRecord_DetailRetrieved(Row As Integer, Index As Integer, Value As Object) Handles p_oRecord.DetailRetrieved
        Dim loTxt As TextBox

        loTxt = CType(FindTextBox(Me, "txtDetail" & Format(Index, "00")), TextBox)

        With DataGridView1
            Select Case Index
                Case 1
                    loTxt.Text = Value

                    .Item(1, Row).Value = Value
                Case 2
                    loTxt.Text = Value
                Case 3
                    loTxt.Text = Value

                    .Item(2, Row).Value = Value
                Case 4, 5
                    loTxt.Text = Format(Value, "#,##0.00")
                Case Else
                    loTxt.Text = Value
            End Select
        End With
    End Sub

    Private Sub p_oRecord_MasterRetrieved(Index As Integer, Value As Object) Handles p_oRecord.MasterRetrieved
        Dim loTxt As TextBox

        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)

        Select Case Index
            Case 11, 12
                loTxt.Text = Format(Value, "MMM dd, yyyy")
            Case 9, 10
                loTxt.Text = Format(Value, "hh:mm")
            Case 5, 6
                loTxt.Text = Format(Value, "#,##0.00")
            Case Else
                loTxt.Text = Value
        End Select
    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As System.EventArgs) Handles CheckBox1.Click
        p_oRecord.Master("cRecdStat") = IIf(CheckBox1.Checked, "1", "0")
    End Sub
End Class