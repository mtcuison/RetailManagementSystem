Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcRetailParams
Imports ggcReceipt

Public Class frmZReading
    Private Const pxeTableName As String = "Terminal Z Reading"

    Private pnLoadx As Integer
    Private poControl As Control

    Private WithEvents p_oRecord As PRN_TZ_Reading

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
            p_oRecord = New PRN_TZ_Reading(p_oAppDriver)

            'Set event Handler for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            Call clearFields()

            pnLoadx = 1
        End If
    End Sub

    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
    End Sub

    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" Then
            Select Case loIndex
                Case 0, 1
                    loTxt.Text = Format(CDate(loTxt.Text), "yyyy/MM/dd")
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
                Case 0, 1
                    If Not IsDate(loTxt.Text) Then
                        loTxt.Text = Format(p_oAppDriver.SysDate, "MMM dd, yyyy")
                    Else
                        loTxt.Text = Format(CDate(loTxt.Text), "MMM dd, yyyy")
                    End If
            End Select
        End If

        loTxt.BackColor = SystemColors.Window
        poControl = Nothing
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        Dim lsSQL As String

        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        With p_oRecord
            Select Case lnIndex
                Case 1 'print
                    'If DateTime.Compare(CDate(txtField00.Text), CDate(txtField01.Text)) > 0 Then
                    '    MsgBox("Invalid date range detected.", vbCritical, "Warning")
                    '    GoTo endProc
                    'End If

                    lsSQL = "SELECT sTranDate, nZReadCtr FROM Daily_Summary" &
                        " WHERE sTranDate BETWEEN " & strParm(Format(CDate(txtField00.Text), "yyyyMMdd")) & " AND " & strParm(Format(CDate(txtField01.Text), "yyyyMMdd")) &
                            " AND sCRMNumbr = " & strParm(p_sPOSNo) &
                            " AND cTranStat IN ('2')" &
                       " ORDER BY sTranDate"

                    Dim loDta As DataTable
                    loDta = p_oAppDriver.ExecuteQuery(lsSQL)

                    If loDta.Rows.Count = 0 Then
                        MsgBox("There are no transaction for the given date....", , "New_Sales_Order")
                        Exit Sub
                    End If

                    For lnCtr = 0 To loDta.Rows.Count - 1
                        If p_oRecord.PrintTZReading(loDta.Rows(lnCtr)("sTranDate"),
                                                    loDta.Rows(lnCtr)("sTranDate"),
                                                    Environment.GetEnvironmentVariable("RMS-CRM-No"),
                                                    True, loDta.Rows(lnCtr)("nZReadCtr")) Then
                            Me.Close()
                        End If
                    Next
                Case 0 'close
                    Me.Close()
            End Select
        End With
endProc:
        Exit Sub
    End Sub

    Private Sub clearFields()
        txtField00.Text = Format(p_oAppDriver.SysDate, "MMM dd, yyyy")
        txtField01.Text = Format(p_oAppDriver.SysDate, "MMM dd, yyyy")
    End Sub
End Class