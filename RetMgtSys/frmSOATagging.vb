Imports System.Globalization
Imports System.Linq.Expressions
Imports ADODB
Imports ggcAppDriver
Imports ggcReceipt

Public Class frmSOATagging
    Private WithEvents oTrans As clsSOA
    Private pnLoadx As Integer
    Private pnIndex As Integer
    Private pnAmtPaid As Double
    Private poControl As Control
    Private Const p_sMsgHeadr As String = "Statement of Account"

    Private Sub frmSOATagging_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If pnLoadx = 0 Then
            oTrans = New clsSOA(p_oAppDriver, 1)

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "textSrch", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            initButton()
            InitGrid()
            clearFields()
            pnLoadx = 1
            pnAmtPaid = 0
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

        txtTotalAmt.Text = FormatNumber(CDbl(oTrans.Master("nTranTotl")), 2)
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

            'reset  pnAmtPaid to 0
            pnAmtPaid = 0
            txtAmtPaid.Text = FormatNumber(pnAmtPaid, 2)
            ' Loop through the items and populate the DataGridView
            For lnCtr As Integer = 0 To itemCount - 1
                Dim cCollectdValue = oTrans.BillDetail(lnCtr, 10)
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

                If (oTrans.BillDetail(lnCtr, 10) = 1) Then
                    pnAmtPaid += CDbl(oTrans.BillDetail(lnCtr, 4))
                    txtAmtPaid.Text = FormatNumber(pnAmtPaid, 2)
                End If
            Next

            If isAllPaid() Then
                chkBox01.Checked = True
                chkBox01.Text = "UNPAID"
            End If
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
    Private Function isAllPaid()

        For lnRow As Integer = 0 To oTrans.GetItemDSCount() - 1
            If Not (oTrans.BillDetail(lnRow, 10) = 1) Then
                Return False
            End If
        Next

        Return True
    End Function

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
            .Columns(5).HeaderText = "Pay"

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
        lblStatus.Text = "UNKNOWN"
        txtAmtPaid.Text = "0.00"
        txtTotalAmt.Text = "0.00"

        chkBox01.Text = "PAY ALL"
        cmbfield01.SelectedIndex = -1
        DataGridView1.Rows.Clear()
        InitGrid()
        oTrans = Nothing
        oTrans = New clsSOA(p_oAppDriver, 1)
    End Sub

    Private Sub initButton()
        Dim lbShow As Boolean
        lbShow = oTrans.EditMode = xeEditMode.MODE_READY

        cmdButton00.Visible = True
        'cmdButton01.Visible = True
        cmdButton02.Visible = True
        cmdButton03.Visible = True
        cmdButton04.Visible = True
        lblStatus.Visible = lbShow


        GroupBox1.Enabled = False
        GroupBox4.Enabled = lbShow

        If isAllPaid() Then
            chkBox01.Enabled = True
            chkBox01.Text = "UNPAID"
        Else
            chkBox01.Enabled = True
            chkBox01.Text = "PAY ALL"

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
            Case 0 'browse
                If pnIndex < 98 Then
                    pnIndex = 98
                End If
                Select Case pnIndex

                    Case 98
                        If (oTrans.SearchTransaction(textSrch98.Text, True)) Then
                            loadMaster(Me)
                            initButton()
                            textSrch98.Focus()
                        End If
                    Case 99
                        If oTrans.SearchTransaction(textSrch99.Text, False) Then
                            loadMaster(Me)
                            initButton()
                            textSrch99.Focus()
                        End If
                End Select
            'Case 1 'print
            '    If Not txtField00.Text <> "" Then
            '        MsgBox("No Transaction seems to be Loaded! Please load Transaction first...", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, p_sMsgHeadr)
            '    Else
            '        If (oTrans.PrintTransaction) Then

            '        End If
            '    End If
            Case 2 'pay
                If Not oTrans.isModified Then
                    MsgBox("Details do not appear to have been modified. Please modify the transaction first.!! ", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, p_sMsgHeadr)
                    Exit Sub
                End If
                If Not txtField00.Text <> "" Then
                    MsgBox("No Transaction seems to be Loaded! Please load Transaction first...", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, p_sMsgHeadr)
                Else
                    If oTrans.PrePostTransaction Then
                        MsgBox("Transaction Tag Successfuly.", MsgBoxStyle.Information, "Notice")
                        initButton()
                        LoadDetail()
                        If isAllPaid() Then chkBox01.Enabled = True
                    End If
                End If
            Case 3 'waive
                MsgBox("This Feature is Ongoing.", MsgBoxStyle.Information, "Notice")
            Case 4 'close
                Me.Close()
                'Case 5 'cancel
                '    oTrans = Nothing
                '    oTrans = New clsSOA(p_oAppDriver, 1)
                '    clearFields()
                '    initButton()

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

            Case 9
                lblStatus.Text = oTrans.TranStatus(oTrans.Master("cTranStat"))
        End Select
    End Sub

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles textSrch98.KeyDown, textSrch99.KeyDown
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Return Then
            Dim loTxt As TextBox
            loTxt = CType(sender, System.Windows.Forms.TextBox)

            Dim loIndexsearch As Integer
            loIndexsearch = Val(Mid(loTxt.Name, 9, 10))

            If Mid(loTxt.Name, 1, 8) = "textSrch" Then
                Select Case loIndexsearch
                    Case 98
                        If (oTrans.SearchTransaction(textSrch98.Text, True)) Then
                            loadMaster(Me)
                            initButton()
                            textSrch98.Focus()
                        End If
                    Case 99
                        If oTrans.SearchTransaction(textSrch99.Text, False) Then
                            loadMaster(Me)
                            initButton()
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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        With DataGridView1
            Dim lnRow As Integer = .CurrentRow.Index
            Dim cCollectdValue = oTrans.BillDetail(lnRow, "cCollectd")
            If Not oTrans.EditMode = xeEditMode.MODE_READY Then Exit Sub
            If Not (String.IsNullOrEmpty(cCollectdValue.ToString()) OrElse cCollectdValue = 0) Then
                MsgBox("Unable to untag Paid Detail", MsgBoxStyle.Information, "Notice")
                Exit Sub
            End If
            If e.ColumnIndex = 5 AndAlso e.RowIndex >= 0 Then
                ' Toggle the checkbox value when the cell in column 5 is clicked
                Dim cell As DataGridViewCell = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)
                cell.Value = Not Convert.ToBoolean(cell.Value)


                oTrans.BillDetail(lnRow, 10) = IIf(cell.Value, 1, 0)


                If (cell.Value = True) Then
                    pnAmtPaid += CDbl(oTrans.BillDetail(lnRow, 4))
                    txtAmtPaid.Text = FormatNumber(pnAmtPaid, 2)
                Else
                    pnAmtPaid -= CDbl(oTrans.BillDetail(lnRow, 4))
                    txtAmtPaid.Text = FormatNumber(pnAmtPaid, 2)
                End If
            End If

            If isAllPaid() Then
                chkBox01.Checked = True
                chkBox01.Text = "UNPAID"
            Else
                chkBox01.Checked = False
                chkBox01.Text = "PAY ALL"

            End If
        End With
    End Sub

    Private Async Sub chkBox01_Click(sender As Object, e As EventArgs) Handles chkBox01.Click
        With DataGridView1

            If chkBox01.Checked = True Then
                pnAmtPaid = 0
            End If
            For lnRow As Integer = 0 To oTrans.GetItemDSCount() - 1

                Dim cCollectdValue = oTrans.BillDetail(lnRow, "cCollectd")

                If chkBox01.Checked = True Then

                    Dim CellBill As DataGridViewCell = .Rows(lnRow).Cells(5)
                    CellBill.Value = Not Convert.ToBoolean(CellBill.Value)

                    .Rows(lnRow).Cells(5).Value = True
                    oTrans.BillDetail(lnRow, 10) = IIf(CellBill.Value, 1, 0)
                    pnAmtPaid += CDbl(oTrans.BillDetail(lnRow, 4))
                    txtAmtPaid.Text = FormatNumber(pnAmtPaid, 2)
                    chkBox01.Text = "UNPAID"

                Else

                    If Not (String.IsNullOrEmpty(cCollectdValue.ToString()) OrElse cCollectdValue = 0) Then

                        Continue For
                    End If
                    Dim CellBill As DataGridViewCell = .Rows(lnRow).Cells(5)
                    CellBill.Value = Not Convert.ToBoolean(CellBill.Value)

                    pnAmtPaid -= CDbl(oTrans.BillDetail(lnRow, 4))
                    .Rows(lnRow).Cells(5).Value = False
                    oTrans.BillDetail(lnRow, 10) = IIf(CellBill.Value, 1, 0)
                    txtAmtPaid.Text = FormatNumber(pnAmtPaid, 2)
                    chkBox01.Text = "PAY ALL"

                End If
            Next




        End With
    End Sub
End Class