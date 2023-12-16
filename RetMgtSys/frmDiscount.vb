Imports ggcAppDriver
Imports ggcRetailParams

Public Class frmDiscount

    Private Const pxeModuleName As String = "frmDiscount"
    Private pnLoadx As Integer
    Private pnIndex As Integer
    Private pnAcRow As Integer
    Private poControl As Control

    Private WithEvents p_oRecord As clsDiscountCards

    Private Sub frmDiscount_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmDiscount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Up, Keys.Down
                Select Case e.KeyCode
                    Case Keys.Return, Keys.Down
                        SetNextFocus()
                    Case Keys.Up
                        SetPreviousFocus()
                End Select
            Case Keys.F3
                If pnIndex = 1 Then
                    Call p_oRecord.SearchItem(pnAcRow, txtOther01.Text)
                End If
        End Select
    End Sub

    Private Sub frmDiscount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If pnLoadx = 0 Then
            p_oRecord = New clsDiscountCards(p_oAppDriver)
            p_oRecord.InitRecord()

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtOther", "GotFocus", AddressOf txtOther_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtOther", "LostFocus", AddressOf txtOther_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtOther", "Validating", AddressOf txtOther_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtOther", "KeyDown", AddressOf txtOther_KeyDown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            initButton()
            clearFields()
            initGrid()

            pnLoadx = 1
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
                        clearFields()
                        loadMaster()
                        loadDetail()
                    End If
                Case 2 'save
                    If isEntryOk() Then
                        If .SaveTransaction() Then clearFields()
                    End If
                Case 3 'search
                    If pnIndex = 1 Then
                        Call .SearchItem(pnAcRow, txtOther01.Text)
                    End If
                Case 4 'browse
                    If .BrowseRecord(clsDiscountCards.xeSpclDisc.xeSpecial) Then
                        loadMaster()
                        loadDetail()
                    End If
                Case 5 'cancel
                    If .InitRecord() Then clearFields()
                Case 6 'update
                    .UpdateRecord()
                Case 7 'delete
                    If .DeleteRecord() Then clearFields()
                Case 8 'close
                    Me.Close()
                    GoTo endProc
                    'Case 9 'delete detail
                    '    If .DeleteDetail(pnAcRow) Then loadDetail()
            End Select
        End With

        initButton()
endProc:
        Exit Sub
    End Sub

    Public Function isEntryOk() As Boolean
        If Trim(txtField01.Text = "") Then
            MsgBox("No Description detected!" & vbCrLf & _
                   "Please check entry and try again!", MsgBoxStyle.Critical, "Warning")
            txtField01.Focus()
            Return False
        ElseIf Trim(txtField02.Text = "") Then
            MsgBox("No Company Code detected!" & vbCrLf & _
                   "Please check entry and try again!.", MsgBoxStyle.Critical, "Warning")
            txtField02.Focus()
            Return False
        End If

        Return True
    End Function

    Public Function SumOfTable() As Double
        Dim addDisc As Double

        For index As Integer = 0 To DataGridView1.RowCount - 1
            addDisc += DataGridView1.Rows(index).Cells(4).Value
        Next

        Return addDisc
    End Function

    Public Function SumOfTable1() As Double
        Dim discRate As Double

        For index As Integer = 0 To DataGridView1.RowCount - 1
            discRate += DataGridView1.Rows(index).Cells(3).Value
        Next

        Return discRate
    End Function

    Private Sub txtOther_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtOther" Then
            Select Case loIndex
                Case 1
            End Select
        End If

        poControl = loTxt
        pnIndex = loIndex

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtOther_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If p_oRecord.EditMode = xeEditMode.xeModeUnknown Then Exit Sub

        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtOther" Then
            Select Case loIndex
                Case 1
                Case 2, 3, 4
                    If Not IsNumeric(loTxt.Text) Then loTxt.Text = 0

                    p_oRecord.Detail(pnAcRow, loIndex) = loTxt.Text
            End Select

            With DataGridView1
                If loIndex = 4 Then
                    If pnAcRow < .Rows.Count - 1 Then
                        .ClearSelection()
                        .Rows(pnAcRow + 1).Selected = True
                        'Call loadDetailInfo(pnAcRow + 1)
                    Else
                        'If p_oRecord.AddDetail() Then
                        '    loadDetail()

                        '    .ClearSelection()
                        '    .Rows(.Rows.Count - 1).Selected = True
                        '    'Call loadDetailInfo(.Rows.Count - 1)
                        '    txtOther01.Focus()
                        'End If
                    End If
                End If
            End With
        End If

        loTxt.BackColor = SystemColors.Window
        poControl = Nothing
    End Sub

    Private Sub txtOther_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" Then
            Select Case loIndex
                Case 3, 4, 5
                    loTxt.Text = Format(p_oRecord.Master(loIndex), "yyyy/MM/dd")
            End Select
        End If

        poControl = loTxt
        pnIndex = -1

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If p_oRecord.EditMode = xeEditMode.xeModeUnknown Then Exit Sub

        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" Then
            Select Case loIndex
                Case 1, 2
                    p_oRecord.Master(loIndex) = loTxt.Text
                Case 3, 4, 5
                    If Not IsDate(loTxt.Text) Then loTxt.Text = p_oAppDriver.SysDate

                    p_oRecord.Master(loIndex) = loTxt.Text
                    loTxt.Text = Format(p_oRecord.Master(loIndex), "MMM dd, yyyy")
            End Select
        End If

        loTxt.BackColor = SystemColors.Window
        poControl = Nothing
    End Sub

    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        'With DataGridView1
        '    Dim lnRow As Integer
        '    lnRow = .CurrentRow.Index

        '    If Mid(loTxt.Name, 1, 8) = "txtField" Then
        '        Select Case loIndex
        '            Case 1
        '        End Select
        '    End If
        'End With
    End Sub

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    End Sub

    Private Sub txtOther_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If e.KeyCode = Keys.Return Then
            With DataGridView1
                Dim lnRow As Integer
                lnRow = 0

                If Mid(loTxt.Name, 1, 8) = "txtOther" Then
                    Select Case loIndex
                        Case 1
                            Call p_oRecord.SearchItem(pnAcRow, txtOther01.Text)
                    End Select
                End If
            End With
        End If
    End Sub

    Private Sub loadMaster()
        With p_oRecord
            txtField00.Text = .Master("sCardIDxx")
            txtField01.Text = .Master("sCardDesc")
            txtField02.Text = .Master("sCompnyCd")
            txtField03.Text = Format(.Master("dPrtSince"), "MMM dd, yyyy")
            txtField04.Text = Format(.Master("dStartxxx"), "MMM dd, yyyy")
            txtField05.Text = Format(.Master("dExpiratn"), "MMM dd, yyyy")
            chkField.Checked = .Master("cRecdStat")
            If (.Master("cNoneVatx") = "1") Then
                chkField00.Checked = False
            Else
                chkField00.Checked = True
            End If
        End With
    End Sub

    Private Sub loadDetail()

        Dim loDta As DataTable
        Dim lsSQL As String

        lsSQL = "SELECT " & _
                " nMinAmtxx, " & _
                " nDiscRate, " & _
                " nDiscAmtx " & _
                " FROM Discount_Card_Detail " & _
                " WHERE sCardIDxx = " & strParm(p_oRecord.Master("sCardIDxx"))
        loDta = p_oAppDriver.ExecuteQuery(lsSQL)

        If loDta.Rows.Count = 0 Then
            txtOther02.Text = 0.0
            txtOther03.Text = 0.0
            txtOther04.Text = 0.0
            Exit Sub
        End If

        txtOther02.Text = loDta(0).Item("nMinAmtxx")
        txtOther03.Text = loDta(0).Item("nDiscRate")
        txtOther04.Text = loDta(0).Item("nDiscAmtx")

    End Sub

    Private Sub initGrid()
        With DataGridView1
            .RowCount = 0

            'Set No of Columns
            .ColumnCount = 5

            'Set Column Headers
            .Columns(0).HeaderText = "No"
            .Columns(1).HeaderText = "Desription"
            .Columns(2).HeaderText = "Min Disc"
            .Columns(3).HeaderText = "Disc Rate"
            .Columns(4).HeaderText = "Disc Amnt"

            'Set Column Sizes
            .Columns(0).Width = 32
            .Columns(1).Width = 160
            .Columns(2).Width = 78
            .Columns(3).Width = 78
            .Columns(4).Width = 78

            'Set No of Rows
            .RowCount = 1
        End With
    End Sub

    Private Sub clearFields()
        txtField00.Text = ""
        txtField01.Text = ""
        txtField02.Text = ""
        txtField03.Text = ""
        txtField04.Text = ""
        txtField05.Text = ""

        txtOther01.Text = ""
        txtOther02.Text = ""
        txtOther03.Text = ""
        txtOther04.Text = ""

        chkField.Checked = False

        Call initGrid()
    End Sub

    Private Sub initButton()
        Dim lbShow As Integer
        Dim lnEditMode As xeEditMode = p_oRecord.EditMode

        lbShow = (lnEditMode = 1 Or lnEditMode = 2)

        cmdButton02.Visible = lbShow
        cmdButton03.Visible = lbShow
        cmdButton05.Visible = lbShow
        GroupBox1.Enabled = lbShow
        GroupBox3.Enabled = lbShow

        cmdButton01.Visible = Not lbShow
        cmdButton06.Visible = Not lbShow
        cmdButton07.Visible = Not lbShow
        cmdButton08.Visible = Not lbShow

        If lbShow Then txtField01.Focus()
    End Sub

    Private Sub p_oRecord_DetailRetreive(ByVal lnRow As Integer, ByVal lnIndex As Integer) Handles p_oRecord.DetailRetreive
        With DataGridView1
            If lnIndex = 1 Then
                .Item(lnIndex, lnRow).Value = p_oRecord.Detail(lnRow, lnIndex)
            Else
                .Item(lnIndex, lnRow).Value = Format(p_oRecord.Detail(lnRow, lnIndex), "#,##0.00")
            End If

            Select Case lnIndex
                Case 1
                    txtOther01.Text = .Item(lnIndex, lnRow).Value
                Case 2
                    txtOther02.Text = Format(p_oRecord.Detail(lnRow, lnIndex), "#,##0.00")
                Case 3
                    txtOther03.Text = Format(p_oRecord.Detail(lnRow, lnIndex), "#,##0.00")
                Case 4
                    txtOther04.Text = Format(p_oRecord.Detail(lnRow, lnIndex), "#,##0.00")
            End Select
        End With
    End Sub

    Private Sub p_oRecord_MasterRetreive(ByVal lnIndex As Integer) Handles p_oRecord.MasterRetreive
    End Sub

    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        'Call loadDetailInfo()
    End Sub

    Private Sub chkField_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkField.Leave
        p_oRecord.Master("cRecdStat") = IIf(chkField.Checked, 1, 0)
    End Sub

    Private Sub chkField00_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkField00.Leave
        p_oRecord.Master("cNoneVatx") = If(chkField00.Checked, 0, 1)
    End Sub
End Class