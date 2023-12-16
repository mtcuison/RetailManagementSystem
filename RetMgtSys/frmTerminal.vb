Imports ggcAppDriver
Public Class frmTerminal

    Private pn_Loaded As Integer
    Private p_bCancelled As Boolean
    Private p_oDriver As ggcAppDriver.GRider
    Private p_oTerminal As String
    Private p_oIDNumber As String

    Public WriteOnly Property GRider() As ggcAppDriver.GRider
        Set(ByVal foValue As ggcAppDriver.GRider)
            p_oDriver = foValue
        End Set
    End Property

    Public ReadOnly Property Cancelled() As Boolean
        Get
            Return p_bCancelled
        End Get
    End Property

    Private Function isPOsNoOk() As Boolean
        Dim lsSQL As String
        lsSQL = "SELECT " & _
                        "sIDNumber" & _
                    " FROM Cash_Reg_Machine" & _
                    " WHERE nPOSNumbr = " & strParm(p_oTerminal)
        Dim loDta As DataTable
        loDta = p_oAppDriver.ExecuteQuery(lsSQL)

        If loDta.Rows.Count = 0 Then
            MsgBox("No POS Machine detected.", MsgBoxStyle.Information, "Notice")
            Return False
            Exit Function
        Else
            p_oIDNumber = loDta(0)("sIDNumber")
            Return True
        End If
    End Function

    Private Sub cmdButton01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdButton01.Click

        p_bCancelled = False

        If isPOsNoOk() Then
            Me.Hide()
            Dim loForm As frmEjournal

            loForm = New frmEjournal
            loForm.IDNumber = p_oIDNumber
            loForm.ShowDialog()
        End If
       
    End Sub

    Private Sub cmdButton00_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdButton00.Click
        p_bCancelled = True
        Me.Hide()
    End Sub

    Private Sub frmSalesCriteria_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtField00.Text = ""
    End Sub

    Private Sub clearFields()
        txtField00.Text = ""
    End Sub

    Private Sub txtField00_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtField00.Validated
        p_oTerminal = txtField00.Text
    End Sub
End Class