<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEjournal
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtField01 = New System.Windows.Forms.RichTextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtField02 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtField00 = New System.Windows.Forms.TextBox()
        Me.cmblist = New System.Windows.Forms.ComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmdButton03 = New System.Windows.Forms.Button()
        Me.cmdButton02 = New System.Windows.Forms.Button()
        Me.cmdButton00 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.txtField01)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Location = New System.Drawing.Point(0, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(394, 725)
        Me.Panel1.TabIndex = 0
        '
        'txtField01
        '
        Me.txtField01.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtField01.Location = New System.Drawing.Point(9, 114)
        Me.txtField01.Name = "txtField01"
        Me.txtField01.ReadOnly = True
        Me.txtField01.Size = New System.Drawing.Size(378, 600)
        Me.txtField01.TabIndex = 2
        Me.txtField01.Text = ""
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.txtField02)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.txtField00)
        Me.Panel3.Controls.Add(Me.cmblist)
        Me.Panel3.Location = New System.Drawing.Point(9, 8)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(378, 100)
        Me.Panel3.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(187, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Date Thru:"
        '
        'txtField02
        '
        Me.txtField02.Location = New System.Drawing.Point(256, 47)
        Me.txtField02.Name = "txtField02"
        Me.txtField02.Size = New System.Drawing.Size(100, 20)
        Me.txtField02.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(187, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Date From:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Select Document Type"
        '
        'txtField00
        '
        Me.txtField00.Location = New System.Drawing.Point(256, 23)
        Me.txtField00.Name = "txtField00"
        Me.txtField00.Size = New System.Drawing.Size(100, 20)
        Me.txtField00.TabIndex = 1
        '
        'cmblist
        '
        Me.cmblist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmblist.FormattingEnabled = True
        Me.cmblist.Items.AddRange(New Object() {"X-Reading", "Z-Reading", "E-Journal"})
        Me.cmblist.Location = New System.Drawing.Point(17, 54)
        Me.cmblist.Name = "cmblist"
        Me.cmblist.Size = New System.Drawing.Size(121, 21)
        Me.cmblist.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.cmdButton03)
        Me.Panel2.Controls.Add(Me.cmdButton02)
        Me.Panel2.Controls.Add(Me.cmdButton00)
        Me.Panel2.Location = New System.Drawing.Point(398, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(83, 720)
        Me.Panel2.TabIndex = 1
        '
        'cmdButton03
        '
        Me.cmdButton03.Location = New System.Drawing.Point(2, 56)
        Me.cmdButton03.Name = "cmdButton03"
        Me.cmdButton03.Size = New System.Drawing.Size(75, 23)
        Me.cmdButton03.TabIndex = 3
        Me.cmdButton03.Text = "Cancel"
        Me.cmdButton03.UseVisualStyleBackColor = True
        '
        'cmdButton02
        '
        Me.cmdButton02.Location = New System.Drawing.Point(3, 31)
        Me.cmdButton02.Name = "cmdButton02"
        Me.cmdButton02.Size = New System.Drawing.Size(75, 23)
        Me.cmdButton02.TabIndex = 2
        Me.cmdButton02.Text = "to PDF"
        Me.cmdButton02.UseVisualStyleBackColor = True
        '
        'cmdButton00
        '
        Me.cmdButton00.Location = New System.Drawing.Point(3, 6)
        Me.cmdButton00.Name = "cmdButton00"
        Me.cmdButton00.Size = New System.Drawing.Size(75, 23)
        Me.cmdButton00.TabIndex = 0
        Me.cmdButton00.Text = "Okay"
        Me.cmdButton00.UseVisualStyleBackColor = True
        '
        'frmEjournal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(485, 729)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmEjournal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "E-Journal"
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmdButton02 As System.Windows.Forms.Button
    Friend WithEvents cmdButton00 As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtField00 As System.Windows.Forms.TextBox
    Friend WithEvents cmblist As System.Windows.Forms.ComboBox
    Friend WithEvents txtField01 As System.Windows.Forms.RichTextBox
    Friend WithEvents cmdButton03 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtField02 As System.Windows.Forms.TextBox
End Class
