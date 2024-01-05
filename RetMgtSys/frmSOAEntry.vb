Imports System.Globalization
Imports ADODB
Imports ggcAppDriver
Imports ggcReceipt

Public Class frmSOAEntry
    Private WithEvents oTrans As clsSOA
    Private pnLoadx As Integer
    Private pnIndex As Integer
    Private poControl As Control
    Private Const p_sMsgHeadr As String = "Billing of Statement"

    Private Sub frmSOAEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If pnLoadx = 0 Then
            oTrans = New clsSOA(p_oAppDriver)

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "textSrch", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            initButton()

            pnLoadx = 1
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
                                If loTxt.Text <> "" Then
                                    loTxt.Text = oTrans.Master(7)
                                End If
                            Case Else
                                loTxt.Text = oTrans.Master(loIndex)
                        End Select
                    End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
                End If '(TypeOf loTxt Is TextBox)
            End If 'If loTxt.HasChildren
        Next 'loTxt In loControl.Controls
        If oTrans.Master("cTranStat") <> "" Then
            lblStatus.Text = TranStatus(oTrans.Master("cTranStat"))
        Else
            lblStatus.Text = "UNKNOWN"
        End If
        If oTrans.EditMode = xeEditMode.MODE_READY Then
            txtField01.ReadOnly = True
            txtField02.ReadOnly = True
        End If

        'cmbfield01.SelectedIndex = IIf(oTrans.Master("sSourceCd") = "DS", 1, 0)

    End Sub

    Private Sub LoadDetail()
        With DataGridView1
            ' Assuming oTrans is an instance of your class with GetItemCount and Detail methods
            Dim itemCount As Integer = oTrans.GetItemDSCount()

            ' Set the row count in the DataGridView
            .RowCount = itemCount

            Debug.Print(itemCount)
            If itemCount > 6 Then
                .Columns(0).Width = 23
            Else
                .Columns(0).Width = 40
            End If
            ' Loop through the items and populate the DataGridView
            For lnCtr As Integer = 0 To itemCount - 1

                .Rows(lnCtr).Cells(0).Value = lnCtr + 1
                .Rows(lnCtr).Cells(1).Value = oTrans.BillDetail(lnCtr, 1)
                .Rows(lnCtr).Cells(2).Value = oTrans.BillDetail(lnCtr, 2)
                .Rows(lnCtr).Cells(3).Value = oTrans.BillDetail(lnCtr, 3)
            Next

            ' Go to the last row
            If itemCount > 1 Then
                .ClearSelection()
                .CurrentCell = .Rows(itemCount - 1).Cells(0)
                .Rows(itemCount - 1).Selected = True
            End If
        End With
    End Sub

    Private Sub clearFields()
        txtField00.Text = ""
        txtField01.Text = ""
        txtField02.Text = ""
        txtField02.Text = ""
        txtField03.Text = ""
        txtField04.Text = ""
        lblStatus.Text = "UNKNOWN"
        cmbfield01.SelectedIndex = -1

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
        lblStatus.Visible = Not lbShow

        GroupBox1.Enabled = lbShow
        GroupBox3.Enabled = Not lbShow

        txtField00.ReadOnly = Not lbShow
        txtField01.ReadOnly = Not lbShow
        txtField02.ReadOnly = Not lbShow


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
                    clearFields()
                    initButton()
                End If

            Case 2 'search
                Select Case pnIndex
                    Case 2
                        If (txtField02.Text = "") Then Exit Sub

                        oTrans.SearchMaster(2, txtField02.Text)

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
                        If oTrans.SearchTransaction(textSrch98.Tag, False) Then
                            loadMaster(Me)
                            textSrch99.Focus()
                        End If
                End Select
            Case 5 'cancel
                oTrans = Nothing
                oTrans = New clsSOA(p_oAppDriver)
                clearFields()
                initButton()
            Case 6 '
                If Not txtField00.Text <> "" Then
                    MsgBox("No Transaction seems to be Loaded! Please load Transaction first...", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, p_sMsgHeadr)

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
            Case 8
                If (loTxt.Text <> "") Then
                    oTrans.Master(9) = loTxt.Text
                End If
            Case 99
                If IsDate(loTxt.Text) Then
                    loTxt.Text = Format(CDate(loTxt.Text), "MMMM dd, yyyy")
                    loTxt.Tag = Format(CDate(loTxt.Text), "yyyy-MM-dd")
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
            Case 7
                txtField04.Text = Value
        End Select
    End Sub

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtField02.KeyDown
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Return Then
            Dim loTxt As TextBox
            loTxt = CType(sender, System.Windows.Forms.TextBox)

            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 2
                        If (loTxt.Text = "") Then Exit Sub

                        oTrans.SearchMaster(2, loTxt.Text)
                        'Case 3
                        '    If (txtField02.Text = "") Then
                        '        MsgBox("Please input Date!!", MsgBoxStyle.Information, "Notice")
                        '        Exit Sub
                        '    End If
                        '    If (txtField02.Text <> "") Then oTrans.SearchMaster(loIndex, loTxt.Text)
                        'Case 8
                        '    If (txtField08.Text = "") Then
                        '        MsgBox("Please input Note!!", MsgBoxStyle.Information, "Notice")
                        '        Exit Sub
                        '    End If

                        'Case 80
                        '    oTrans.SearchMaster(loIndex, loTxt.Text)
                        'Case 81
                        '    If (txtField80.Text = "") Then
                        '        MsgBox("Please input Branch !!", MsgBoxStyle.Information, "Notice")
                        '        Exit Sub
                        '    End If
                        '    If (txtField81.Text <> "") Then oTrans.SearchMaster(loIndex, loTxt.Text)
                End Select
            End If
        End If
    End Sub


    'call this command to validate values since we are using lostfocus as substitute for validate
    Private Sub validateControl()
        txtField01.Focus()


    End Sub



    Private Sub cmbfield01_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbfield01.SelectedIndexChanged
        If cmbfield01.SelectedIndex > 0 Then
            oTrans.Master("sSourceCd") = IIf(cmbfield01.SelectedIndex = 0, "CI", "DS")

            If (oTrans.loadBilling()) Then
                LoadDetail()

            End If
        End If

    End Sub


End Class