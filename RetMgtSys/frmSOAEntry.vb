Imports System.Globalization
Imports ADODB
Imports ggcAppDriver
Imports ggcReceipt

Public Class frmSOAEntry
    Private WithEvents oTrans As clsSOA
    Private pnLoadx As Integer
    Private pnIndex As Integer
    Private pnTotalAmt As Double
    Private poControl As Control
    Private Const p_sMsgHeadr As String = "Billing of Statement"

    Private Sub frmSOAEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If pnLoadx = 0 Then
            oTrans = New clsSOA(p_oAppDriver, 12340)

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "textSrch", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            initButton()
            InitGrid()
            clearFields()
            pnLoadx = 1
            pnTotalAmt = 0
        End If
    End Sub

    Private Sub loadMaster(ByVal loControl As Control)
        Dim loTxt As Control

        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadMaster(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtfield" Then
                        Select Case loIndex
                            Case 1
                                If (IsDate(oTrans.Master(loIndex))) Then
                                    loTxt.Text = Format(oTrans.Master(loIndex), "MMMM dd, yyyy")
                                End If
                            Case 3
                                loTxt.Text = oTrans.Master(80)
                            Case 4
                                loTxt.Text = oTrans.Master(6)

                            Case 5
                                loTxt.Text = ""
                            Case Else
                                loTxt.Text = oTrans.Master(loIndex)
                        End Select
                    End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
                End If '(TypeOf loTxt Is TextBox)
            End If 'If loTxt.HasChildren
        Next 'loTxt In loControl.Controls
        If oTrans.Master("cTranStat") <> "" Then
            lblStatus.Text = oTrans.TranStatus(oTrans.Master("cTranStat"))
        Else
            lblStatus.Text = "UNKNOWN"
        End If
        If oTrans.EditMode = xeEditMode.MODE_READY Then
            txtField01.ReadOnly = True
            txtField02.ReadOnly = True
            LoadDetail()
        End If

        If oTrans.Master("sSourceCd") <> "" Then
            cmbfield01.SelectedIndex = IIf(oTrans.Master("sSourceCd") = "DS", 1, 0)
        End If
    End Sub

    Private Sub LoadDetail()
        pnTotalAmt = 0
        txtTotalAmt.Text = FormatNumber(pnTotalAmt, 2)
        With DataGridView1
            ' Assuming oTrans is an instance of your class with GetItemCount and Detail methods
            Dim itemCount As Integer = oTrans.GetItemDSCount()

            ' Set the row count in the DataGridView
            .RowCount = itemCount

            If itemCount > 9 Then
                .Columns(2).Width = 133
            Else
                .Columns(2).Width = 150
            End If

            ' Loop through the items and populate the DataGridView
            For lnCtr As Integer = 0 To itemCount - 1

                Dim cCollectdValue = oTrans.BillDetail(lnCtr, 5)
                .Rows(lnCtr).Cells(0).Value = lnCtr + 1
                .Rows(lnCtr).Cells(1).Value = oTrans.BillDetail(lnCtr, 1)
                .Rows(lnCtr).Cells(2).Value = oTrans.BillDetail(lnCtr, 2)
                .Rows(lnCtr).Cells(3).Value = Format(CDate(oTrans.BillDetail(lnCtr, 3)), "MMMM dd, yyyy")
                .Rows(lnCtr).Cells(4).Value = FormatNumber(CDbl(oTrans.BillDetail(lnCtr, 4)), 2)
                If Not cCollectdValue <> "" Then
                    .Rows(lnCtr).Cells(5).Value = IIf(Not (String.IsNullOrEmpty(cCollectdValue.ToString())), True, False)
                Else
                    .Rows(lnCtr).Cells(5).Value = IIf(cCollectdValue = 1, True, False)
                End If

                If cCollectdValue <> "" Then
                    If (cCollectdValue = 1) Then
                        pnTotalAmt += CDbl(oTrans.BillDetail(lnCtr, 4))
                        txtTotalAmt.Text = FormatNumber(pnTotalAmt, 2)
                    End If
                    If Not txtField04.Enabled = True Then
                        If isAllBilled() Then
                            chkBox01.Checked = True
                            chkBox01.Text = "UNBILL"
                        End If
                    End If
                End If
            Next


            ' Go to the last row
            If itemCount > 1 Then
                .ClearSelection()
                .CurrentCell = .Rows(itemCount - 1).Cells(0)
                .Rows(itemCount - 1).Selected = True
            End If

            If oTrans.EditMode = xeEditMode.MODE_READY Then
                .ReadOnly = True
                txtField05.Text = oTrans.BillDetail(0, 15)
            Else
                txtField05.Text = ""
            End If
        End With


    End Sub

    Private Sub InitGrid()
        With DataGridView1
            'Set No of Columns
            .ColumnCount = 6


            'Set Column Headers
            .Columns(0).HeaderText = "No."
            .Columns(1).HeaderText = "Service"
            .Columns(2).HeaderText = "Source"
            .Columns(3).HeaderText = "Date"
            .Columns(4).HeaderText = "Amount"
            .Columns(5).HeaderText = "Bill"

            'Set Column Sizes
            .Columns(0).Width = 31
            .Columns(1).Width = 170
            .Columns(2).Width = 150
            .Columns(3).Width = 95
            .Columns(4).Width = 100
            .Columns(5).Width = 40

            'Set Cell Alignment
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            For Each column As DataGridViewColumn In .Columns
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next
        End With
    End Sub

    Private Sub clearFields()
        txtField00.Text = ""
        txtField01.Text = ""
        txtField02.Text = ""
        txtField02.Text = ""
        txtField03.Text = ""
        txtField04.Text = ""
        txtField05.Text = ""

        txtTotalAmt.Text = "0.00"
        lblStatus.Text = "UNKNOWN"
        chkBox01.Checked = False
        chkBox01.Text = "BILL ALL"
        cmbfield01.SelectedIndex = -1
        DataGridView1.Rows.Clear()
        InitGrid()
        oTrans = Nothing
        oTrans = New clsSOA(p_oAppDriver, 12340)
    End Sub

    Private Sub initButton()
        Dim lbShow As Boolean
        lbShow = oTrans.EditMode = xeEditMode.MODE_ADDNEW

        cmdButton01.Visible = lbShow
        cmdButton02.Visible = lbShow
        cmdbutton05.Visible = lbShow
        cmdButton00.Visible = Not lbShow
        cmdButton03.Visible = Not lbShow
        cmdButton04.Visible = Not lbShow
        cmdButton06.Visible = Not lbShow
        cmdButton07.Visible = Not lbShow
        cmdButton08.Visible = Not lbShow
        lblStatus.Visible = Not lbShow

        GroupBox1.Enabled = lbShow
        GroupBox3.Enabled = Not lbShow

        txtField00.ReadOnly = Not lbShow
        txtField01.ReadOnly = Not lbShow
        txtField02.ReadOnly = Not lbShow
        txtField04.Enabled = lbShow
        chkBox01.Enabled = lbShow



        If oTrans.EditMode = xeEditMode.MODE_ADDNEW Then
            txtField01.Focus()
        End If

    End Sub

    Private Sub NewTransaction()
        clearFields()

        If oTrans.NewTransaction() Then
            loadMaster(Me)
            initButton()
        End If

    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 'new
                NewTransaction()
            Case 1 'save
                Call validateControl()
                If oTrans.SaveTransaction Then
                    MsgBox("Transaction Saved Successfuly.", MsgBoxStyle.Information, "Notice")
                    If MsgBox("Do you want to print this Transaction?", vbQuestion + vbYesNo, "Confirm") = vbYes Then
                        If (oTrans.PrintTransaction()) Then

                        End If
                    End If
                    clearFields()
                    initButton()
                End If

            Case 2 'search
                Select Case pnIndex
                    Case 2
                        If (txtField02.Text = "") Then Exit Sub

                        oTrans.SearchMaster(2, txtField02.Text)
                    Case 5
                        If (cmbfield01.SelectedIndex = 1) Then
                            If (oTrans.SearchDeliveryService(txtField05.Text, False)) Then
                                pnTotalAmt = 0
                                LoadDetail()

                                txtField05.Text = oTrans.BillDetail(0, 16)
                            Else
                                If (oTrans.loadBilling) Then

                                End If
                                pnTotalAmt = 0
                                LoadDetail()

                            End If
                        ElseIf (cmbfield01.SelectedIndex = 0) Then
                            MsgBox("This feature is not yet available", MsgBoxStyle.Information, "Notice")
                        Else
                            MsgBox("Please select source transaction first! ", MsgBoxStyle.Information, "Notice")
                        End If

                End Select
            Case 3 'close
                Me.Close()

            Case 4 'browse
                If pnIndex < 98 Then
                    pnIndex = 98
                End If
                Select Case pnIndex

                    Case 98
                        If (oTrans.SearchTransaction(textSrch98.Text, True)) Then
                            loadMaster(Me)
                            textSrch98.Focus()
                        End If
                    Case 99
                        If oTrans.SearchTransaction(textSrch99.Text, False) Then
                            loadMaster(Me)
                            textSrch99.Focus()
                        End If
                End Select
            Case 5 'cancel
                oTrans = Nothing
                oTrans = New clsSOA(p_oAppDriver, 12340)
                clearFields()
                initButton()
            Case 6 'print
                If Not txtField00.Text <> "" Then
                    MsgBox("No Transaction seems to be Loaded! Please load Transaction first...", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, p_sMsgHeadr)
                Else
                    If (oTrans.Master("cPrintedx") = "1") Then
                        If MsgBox("Do you want to re-print this Transaction?", vbQuestion + vbYesNo, "Confirm") = vbYes Then
                            If (p_oAppDriver.getUserApproval) Then
                                If (oTrans.PrintTransaction) Then

                                End If
                            End If

                        End If
                    Else
                        oTrans.PrintTransaction()
                    End If
                End If
            Case 7 'approve
                If Not txtField00.Text <> "" Then
                    MsgBox("No Transaction seems to be Loaded! Please load Transaction first...", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, p_sMsgHeadr)
                Else
                    If oTrans.Master("cPrintedx") = "0" Then
                        If MsgBox("This Transaction is not yet printed. Please print First!! Would you like to print to proceed ?", vbQuestion + vbYesNo, "Confirm") = vbYes Then
                            If (oTrans.PrintTransaction()) Then

                            End If
                        Else
                            Exit Sub
                        End If

                    End If
                    If Not p_oAppDriver.getUserApproval() Then Exit Sub
                    If (oTrans.CloseTransaction) Then
                        MsgBox("Transaction Approved Successfuly. ", MsgBoxStyle.Information, "Notice")
                    End If

                    If MsgBox("Do you want to print this Approved Transaction?", vbQuestion + vbYesNo, "Confirm") = vbYes Then
                        If (oTrans.PrintTransaction()) Then

                        End If
                    End If
                End If

            Case 8 'disapprove
                If Not txtField00.Text <> "" Then
                    MsgBox("No Transaction seems to be Loaded! Please load Transaction first...", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, p_sMsgHeadr)
                Else
                    If Not p_oAppDriver.getUserApproval() Then Exit Sub
                    If (oTrans.CancelTransaction) Then
                        MsgBox("Transaction Disapproved Successfuly. ", MsgBoxStyle.Information, "Notice")
                    End If
                End If

        End Select
    End Sub

    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" Then
            Select Case loIndex
                Case 1
                    If loTxt.Text <> "" Then
                        If IsDate(loTxt.Text) Then
                            loTxt.Text = Format(CDate(oTrans.Master(8)), "yyyy/MM/dd")
                        Else
                            loTxt.Text = ""
                        End If
                    End If
            End Select
        End If

        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        Select Case loIndex
            Case 1
                If IsDate(loTxt.Text) Then
                    loTxt.Text = Format(CDate(loTxt.Text), "yyyy-MM-dd")
                    'oTrans.Master(8) = loTxt.Text
                    'loTxt.Text = Format(CDate(oTrans.Master(8)), "MMMM dd, yyyy")

                Else
                    loTxt.Text = ""
                End If
            Case 4
                If (loTxt.Text <> "") Then
                    oTrans.Master(6) = loTxt.Text
                End If
            Case 99
                If IsDate(loTxt.Text) Then
                    loTxt.Text = Format(CDate(loTxt.Text), "yyyy-MM-dd")
                    'oTrans.Master(8) = loTxt.Text
                    'loTxt.Text = Format(CDate(oTrans.Master(8)), "MMMM dd, yyyy")

                Else
                    loTxt.Text = ""
                End If
        End Select

        pnIndex = loIndex

        loTxt.BackColor = SystemColors.Window
        poControl = Nothing
    End Sub

    Private Sub oTrans_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles oTrans.MasterRetrieved

        Select Case Index
            Case 2
                txtField02.Text = Value
            Case 80
                txtField03.Text = Value
            Case 6
                txtField04.Text = Value

            Case 17
                txtField05.Text = Value
            Case 9
                lblStatus.Text = oTrans.TranStatus(oTrans.Master("cTranStat"))
        End Select
    End Sub

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtField02.KeyDown, txtField05.KeyDown, textSrch98.KeyDown, textSrch99.KeyDown
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Return Then
            Dim loTxt As TextBox
            loTxt = CType(sender, System.Windows.Forms.TextBox)

            Dim loIndex As Integer
            Dim loIndexsearch As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            loIndexsearch = Val(Mid(loTxt.Name, 9, 10))
            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 2
                        If (loTxt.Text = "") Then Exit Sub

                        oTrans.SearchMaster(2, loTxt.Text)

                    Case 5
                        If (cmbfield01.SelectedIndex = 1) Then
                            If (oTrans.SearchDeliveryService(loTxt.Text, False)) Then
                                pnTotalAmt = 0
                                LoadDetail()

                                txtField05.Text = oTrans.BillDetail(0, 16)
                                Else
                                    If (oTrans.loadBilling) Then

                                End If
                                pnTotalAmt = 0
                                LoadDetail()

                            End If
                        ElseIf (cmbfield01.SelectedIndex = 0) Then
                            MsgBox("This feature is not yet available", MsgBoxStyle.Information, "Notice")
                        Else
                            MsgBox("Please select source transaction first! ", MsgBoxStyle.Information, "Notice")
                        End If

                End Select
            End If
            If Mid(loTxt.Name, 1, 8) = "textSrch" Then
                Select Case loIndexsearch
                    Case 98
                        If (oTrans.SearchTransaction(textSrch98.Text, True)) Then
                            loadMaster(Me)
                            textSrch98.Focus()
                        End If
                    Case 99
                        If oTrans.SearchTransaction(textSrch99.Text, False) Then
                            loadMaster(Me)
                            textSrch99.Focus()
                        End If
                End Select

            End If
        End If
    End Sub


    'call this command to validate values since we are using lostfocus as substitute for validate
    Private Sub validateControl()
        txtField01.Focus()


    End Sub



    Private Sub cmbfield01_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbfield01.SelectedIndexChanged
        If cmbfield01.Enabled = True Then
            If cmbfield01.SelectedIndex >= 0 Then

                oTrans.Master("sSourceCd") = IIf(cmbfield01.SelectedIndex = 0, "CI", "DS")

                If (oTrans.loadBilling()) Then

                End If
                pnTotalAmt = 0
                LoadDetail()
            End If
        End If

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        With DataGridView1
            Dim lnRow As Integer = .CurrentRow.Index

            If txtField02.Enabled = False Then Exit Sub
            If e.ColumnIndex = 5 AndAlso e.RowIndex >= 0 Then
                ' Toggle the checkbox value when the cell in column 5 is clicked
                Dim cell As DataGridViewCell = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)
                cell.Value = Not Convert.ToBoolean(cell.Value)

                oTrans.BillDetail(lnRow, 5) = IIf(cell.Value, 1, 0)

                If (cell.Value = True) Then
                    pnTotalAmt += CDbl(oTrans.BillDetail(lnRow, 4))
                    txtTotalAmt.Text = FormatNumber(pnTotalAmt, 2)
                Else
                    pnTotalAmt -= CDbl(oTrans.BillDetail(lnRow, 4))
                    txtTotalAmt.Text = FormatNumber(pnTotalAmt, 2)
                End If
            End If
            If isAllBilled() Then
                chkBox01.Checked = True
                chkBox01.Text = "UNBILL"
            Else
                chkBox01.Checked = False
                chkBox01.Text = "BILL ALL"

            End If
        End With
    End Sub

    Private Function isAllBilled()

        For lnRow As Integer = 0 To oTrans.GetItemDSCount() - 1
            If (oTrans.BillDetail(lnRow, 5) = "") Then
                oTrans.BillDetail(lnRow, 5) = 0
            End If

            If Not (oTrans.BillDetail(lnRow, 5) = 1) Then
                Return False
            End If
        Next

        Return True
    End Function


    Private Sub chkBox01_Click(sender As Object, e As EventArgs) Handles chkBox01.Click
        With DataGridView1

            If chkBox01.Checked = True Then
                pnTotalAmt = 0
            End If
            For lnRow As Integer = 0 To oTrans.GetItemDSCount() - 1
                If chkBox01.Checked = True Then

                    Dim CellBill As DataGridViewCell = .Rows(lnRow).Cells(5)
                    CellBill.Value = Not Convert.ToBoolean(CellBill.Value)

                    .Rows(lnRow).Cells(5).Value = True
                    oTrans.BillDetail(lnRow, 5) = IIf(CellBill.Value, 1, 0)
                    pnTotalAmt += CDbl(oTrans.BillDetail(lnRow, 4))
                    txtTotalAmt.Text = FormatNumber(pnTotalAmt, 2)
                    chkBox01.Text = "UNBILL"

                Else
                    Dim CellBill As DataGridViewCell = .Rows(lnRow).Cells(5)
                    CellBill.Value = Not Convert.ToBoolean(CellBill.Value)
                    pnTotalAmt -= CDbl(oTrans.BillDetail(lnRow, 4))
                    .Rows(lnRow).Cells(5).Value = False
                    oTrans.BillDetail(lnRow, 5) = IIf(CellBill.Value, 1, 0)
                    txtTotalAmt.Text = FormatNumber(pnTotalAmt, 2)
                    chkBox01.Text = "BILL ALL"

                End If
            Next




        End With
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub
End Class