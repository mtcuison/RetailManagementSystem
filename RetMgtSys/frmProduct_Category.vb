Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcRetailParams
Imports System.IO

Public Class frmProduct_Category
    Private Const pxeTableName As String = "Product_Category"
    Private Const pxeImagePath As String = "C:\GGC_Systems\Images\vb.net\Pedritos\Categories\"

    Private pnLoadx As Integer
    Private poControl As Control

    Private WithEvents p_oRecord As clsProductCategory
    Private p_nEditMode As Integer

    Private Sub frmProduct_Category_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmProduct_Category_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmProduct_Category_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If pnLoadx = 0 Then
            p_oRecord = New clsProductCategory(p_oAppDriver, True)
            p_oRecord.InitRecord()

            'Set event Handler for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            clearFields()
            initButton()

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
                    Case 1, 2
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
                Case 2
                    p_oRecord.Master(6) = loTxt.Text
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
                        clearFields()
                        initButton()
                    End If
                Case 3 'search
                Case 4 'browse
                    If .BrowseRecord Then
                        clearFields()
                        loadMaster()
                    End If
                Case 5 'cancel
                    If .CancelUpdate() Then
                        clearFields()
                        initButton()
                    End If
                Case 6 'update
                    .UpdateRecord()
                Case 7 'delete
                    If .DeleteRecord() Then
                        clearFields()
                        initButton()
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
        GroupBox1.Enabled = lbShow

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

        chkForward.Checked = False
        chkRecdStat.Checked = False
        chkPriority.Checked = False

        Try
            Dim ScanFolder() As String
            Dim lsFileName() As String
            ScanFolder = Directory.GetFiles(pxeImagePath, "*.jpg")

            fileList.Items.Clear()
            For Each dFile As String In ScanFolder
                lsFileName = dFile.Split("\")
                fileList.Items.Add(lsFileName(UBound(lsFileName)))
            Next

            If fileList.Items.Count > 0 Then
                fileList.SelectedIndex = 0
                filePic.BackgroundImage = Image.FromFile(pxeImagePath & "(none).jpg")
            Else
                filePic.BackgroundImage = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub loadMaster()
        With p_oRecord
            txtField00.MaxLength = .MasFldSze(0)
            txtField01.MaxLength = .MasFldSze(1)
            txtField02.MaxLength = .MasFldSze(6)

            txtField00.Text = .Master("sCategrCd")
            txtField01.Text = .Master("sDescript")
            txtField02.Text = IFNull(.Master("sPrntPath"), "")
            chkForward.Checked = IFNull(.Master("cForwardx"), 0)
            chkPriority.Checked = .Master("cPriority")
            chkRecdStat.Checked = .Master("cRecdStat")

            If IFNull(.Master("sImgePath"), "") <> "" Then
                For nCtr As Integer = 0 To fileList.Items.Count - 1
                    fileList.SelectedIndex = nCtr
                    If pxeImagePath & fileList.SelectedItem = Replace(.Master("sImgePath"), "/", "\") Then
                        filePic.BackgroundImage = Image.FromFile(pxeImagePath & fileList.SelectedItem)
                        Exit For
                    End If
                Next nCtr
            End If
        End With
    End Sub

    Private Sub chkForward_Click(sender As Object, e As System.EventArgs) Handles chkForward.Click
        p_oRecord.Master(2) = IIf(chkForward.Checked, 1, 0)
    End Sub

    Private Sub chkRecdStat_Click(sender As Object, e As System.EventArgs) Handles chkRecdStat.Click
        p_oRecord.Master(5) = IIf(chkRecdStat.Checked, 1, 0)
    End Sub

    Private Sub fileList_Click(sender As Object, e As System.EventArgs) Handles fileList.Click
        Try
            filePic.BackgroundImage = Image.FromFile(pxeImagePath & fileList.SelectedItem)
            p_oRecord.Master("sImgePath") = pxeImagePath & fileList.SelectedItem
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub chkPriority_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPriority.Click
        p_oRecord.Master(4) = IIf(chkPriority.Checked, 1, 0)
    End Sub
End Class