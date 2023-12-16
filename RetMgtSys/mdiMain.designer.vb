<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class mdiMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InventoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalesPromoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PromoAddOnsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.UserManagerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ParametersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AffiliatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BanksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BinToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DiscountCardsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InventoryTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MachineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MeasureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductCategoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TermToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SpecialDiscountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdminitratorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AccumulatedGrandTotalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EventLogsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BIRSalesSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelledReceiptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItemListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalesReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VoidItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ComplementaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChargeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EJournalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DailySalesSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalesInventorySummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StandardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.YReadingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TerminalZReadingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UtilitiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackupDatabaseToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestoreDatabseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tslDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslUser = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sfd = New System.Windows.Forms.SaveFileDialog()
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.AdminitratorToolStripMenuItem, Me.UtilitiesToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1276, 28)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InventoryToolStripMenuItem, Me.SalesPromoToolStripMenuItem, Me.PromoAddOnsToolStripMenuItem, Me.ToolStripSeparator3, Me.UserManagerToolStripMenuItem, Me.ToolStripSeparator1, Me.ParametersToolStripMenuItem, Me.ToolStripSeparator2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(46, 24)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'InventoryToolStripMenuItem
        '
        Me.InventoryToolStripMenuItem.Name = "InventoryToolStripMenuItem"
        Me.InventoryToolStripMenuItem.Size = New System.Drawing.Size(232, 26)
        Me.InventoryToolStripMenuItem.Text = "&Product Maintenance"
        '
        'SalesPromoToolStripMenuItem
        '
        Me.SalesPromoToolStripMenuItem.Name = "SalesPromoToolStripMenuItem"
        Me.SalesPromoToolStripMenuItem.Size = New System.Drawing.Size(232, 26)
        Me.SalesPromoToolStripMenuItem.Text = "Promo &Discount"
        '
        'PromoAddOnsToolStripMenuItem
        '
        Me.PromoAddOnsToolStripMenuItem.Name = "PromoAddOnsToolStripMenuItem"
        Me.PromoAddOnsToolStripMenuItem.Size = New System.Drawing.Size(232, 26)
        Me.PromoAddOnsToolStripMenuItem.Text = "Promo &Add Ons"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(229, 6)
        '
        'UserManagerToolStripMenuItem
        '
        Me.UserManagerToolStripMenuItem.Name = "UserManagerToolStripMenuItem"
        Me.UserManagerToolStripMenuItem.Size = New System.Drawing.Size(232, 26)
        Me.UserManagerToolStripMenuItem.Text = "User Manager"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(229, 6)
        '
        'ParametersToolStripMenuItem
        '
        Me.ParametersToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AffiliatesToolStripMenuItem, Me.BanksToolStripMenuItem, Me.BinToolStripMenuItem, Me.DiscountCardsToolStripMenuItem, Me.InventoryTypeToolStripMenuItem, Me.MachineToolStripMenuItem, Me.MeasureToolStripMenuItem, Me.ProductCategoryToolStripMenuItem, Me.SectionToolStripMenuItem, Me.SizeToolStripMenuItem, Me.TermToolStripMenuItem, Me.SpecialDiscountToolStripMenuItem})
        Me.ParametersToolStripMenuItem.Name = "ParametersToolStripMenuItem"
        Me.ParametersToolStripMenuItem.Size = New System.Drawing.Size(232, 26)
        Me.ParametersToolStripMenuItem.Text = "&Parameters"
        '
        'AffiliatesToolStripMenuItem
        '
        Me.AffiliatesToolStripMenuItem.Name = "AffiliatesToolStripMenuItem"
        Me.AffiliatesToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.AffiliatesToolStripMenuItem.Text = "Affiliates"
        '
        'BanksToolStripMenuItem
        '
        Me.BanksToolStripMenuItem.Name = "BanksToolStripMenuItem"
        Me.BanksToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.BanksToolStripMenuItem.Text = "Banks"
        '
        'BinToolStripMenuItem
        '
        Me.BinToolStripMenuItem.Name = "BinToolStripMenuItem"
        Me.BinToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.BinToolStripMenuItem.Text = "Bin"
        '
        'DiscountCardsToolStripMenuItem
        '
        Me.DiscountCardsToolStripMenuItem.Name = "DiscountCardsToolStripMenuItem"
        Me.DiscountCardsToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.DiscountCardsToolStripMenuItem.Text = "Discount Cards"
        '
        'InventoryTypeToolStripMenuItem
        '
        Me.InventoryTypeToolStripMenuItem.Name = "InventoryTypeToolStripMenuItem"
        Me.InventoryTypeToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.InventoryTypeToolStripMenuItem.Text = "Inventory Type"
        '
        'MachineToolStripMenuItem
        '
        Me.MachineToolStripMenuItem.Name = "MachineToolStripMenuItem"
        Me.MachineToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.MachineToolStripMenuItem.Text = "Machine"
        '
        'MeasureToolStripMenuItem
        '
        Me.MeasureToolStripMenuItem.Name = "MeasureToolStripMenuItem"
        Me.MeasureToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.MeasureToolStripMenuItem.Text = "Measure"
        '
        'ProductCategoryToolStripMenuItem
        '
        Me.ProductCategoryToolStripMenuItem.Name = "ProductCategoryToolStripMenuItem"
        Me.ProductCategoryToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.ProductCategoryToolStripMenuItem.Text = "Product Category"
        '
        'SectionToolStripMenuItem
        '
        Me.SectionToolStripMenuItem.Name = "SectionToolStripMenuItem"
        Me.SectionToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.SectionToolStripMenuItem.Text = "Section"
        '
        'SizeToolStripMenuItem
        '
        Me.SizeToolStripMenuItem.Name = "SizeToolStripMenuItem"
        Me.SizeToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.SizeToolStripMenuItem.Text = "Size"
        '
        'TermToolStripMenuItem
        '
        Me.TermToolStripMenuItem.Name = "TermToolStripMenuItem"
        Me.TermToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.TermToolStripMenuItem.Text = "Term"
        '
        'SpecialDiscountToolStripMenuItem
        '
        Me.SpecialDiscountToolStripMenuItem.Name = "SpecialDiscountToolStripMenuItem"
        Me.SpecialDiscountToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.SpecialDiscountToolStripMenuItem.Text = "Special Discount"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(229, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(232, 26)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'AdminitratorToolStripMenuItem
        '
        Me.AdminitratorToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BackendToolStripMenuItem, Me.StandardToolStripMenuItem, Me.YReadingToolStripMenuItem, Me.TerminalZReadingToolStripMenuItem})
        Me.AdminitratorToolStripMenuItem.Name = "AdminitratorToolStripMenuItem"
        Me.AdminitratorToolStripMenuItem.Size = New System.Drawing.Size(74, 24)
        Me.AdminitratorToolStripMenuItem.Text = "Reports"
        '
        'BackendToolStripMenuItem
        '
        Me.BackendToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AccumulatedGrandTotalToolStripMenuItem, Me.EventLogsToolStripMenuItem, Me.BIRSalesSummaryToolStripMenuItem, Me.CancelledReceiptToolStripMenuItem, Me.ItemListToolStripMenuItem, Me.SalesReportToolStripMenuItem, Me.VoidItemsToolStripMenuItem, Me.ComplementaryToolStripMenuItem, Me.ChargeToolStripMenuItem, Me.EJournalToolStripMenuItem, Me.DailySalesSummaryToolStripMenuItem, Me.SalesInventorySummaryToolStripMenuItem})
        Me.BackendToolStripMenuItem.Name = "BackendToolStripMenuItem"
        Me.BackendToolStripMenuItem.Size = New System.Drawing.Size(224, 26)
        Me.BackendToolStripMenuItem.Text = "Standard"
        Me.BackendToolStripMenuItem.Visible = False
        '
        'AccumulatedGrandTotalToolStripMenuItem
        '
        Me.AccumulatedGrandTotalToolStripMenuItem.Name = "AccumulatedGrandTotalToolStripMenuItem"
        Me.AccumulatedGrandTotalToolStripMenuItem.Size = New System.Drawing.Size(260, 26)
        Me.AccumulatedGrandTotalToolStripMenuItem.Text = "Accumulated Grand Total"
        '
        'EventLogsToolStripMenuItem
        '
        Me.EventLogsToolStripMenuItem.Name = "EventLogsToolStripMenuItem"
        Me.EventLogsToolStripMenuItem.Size = New System.Drawing.Size(260, 26)
        Me.EventLogsToolStripMenuItem.Text = "Activity Log"
        '
        'BIRSalesSummaryToolStripMenuItem
        '
        Me.BIRSalesSummaryToolStripMenuItem.Name = "BIRSalesSummaryToolStripMenuItem"
        Me.BIRSalesSummaryToolStripMenuItem.Size = New System.Drawing.Size(260, 26)
        Me.BIRSalesSummaryToolStripMenuItem.Text = "BIR Sales Summary"
        '
        'CancelledReceiptToolStripMenuItem
        '
        Me.CancelledReceiptToolStripMenuItem.Name = "CancelledReceiptToolStripMenuItem"
        Me.CancelledReceiptToolStripMenuItem.Size = New System.Drawing.Size(260, 26)
        Me.CancelledReceiptToolStripMenuItem.Text = "Cancelled Receipt"
        '
        'ItemListToolStripMenuItem
        '
        Me.ItemListToolStripMenuItem.Name = "ItemListToolStripMenuItem"
        Me.ItemListToolStripMenuItem.Size = New System.Drawing.Size(260, 26)
        Me.ItemListToolStripMenuItem.Text = "Item List"
        '
        'SalesReportToolStripMenuItem
        '
        Me.SalesReportToolStripMenuItem.Name = "SalesReportToolStripMenuItem"
        Me.SalesReportToolStripMenuItem.Size = New System.Drawing.Size(260, 26)
        Me.SalesReportToolStripMenuItem.Text = "Sales Report"
        '
        'VoidItemsToolStripMenuItem
        '
        Me.VoidItemsToolStripMenuItem.Name = "VoidItemsToolStripMenuItem"
        Me.VoidItemsToolStripMenuItem.Size = New System.Drawing.Size(260, 26)
        Me.VoidItemsToolStripMenuItem.Text = "Void Items"
        '
        'ComplementaryToolStripMenuItem
        '
        Me.ComplementaryToolStripMenuItem.Name = "ComplementaryToolStripMenuItem"
        Me.ComplementaryToolStripMenuItem.Size = New System.Drawing.Size(260, 26)
        Me.ComplementaryToolStripMenuItem.Text = "Complementary"
        '
        'ChargeToolStripMenuItem
        '
        Me.ChargeToolStripMenuItem.Name = "ChargeToolStripMenuItem"
        Me.ChargeToolStripMenuItem.Size = New System.Drawing.Size(260, 26)
        Me.ChargeToolStripMenuItem.Text = "Charge"
        '
        'EJournalToolStripMenuItem
        '
        Me.EJournalToolStripMenuItem.Name = "EJournalToolStripMenuItem"
        Me.EJournalToolStripMenuItem.Size = New System.Drawing.Size(260, 26)
        Me.EJournalToolStripMenuItem.Text = "E-Journal"
        '
        'DailySalesSummaryToolStripMenuItem
        '
        Me.DailySalesSummaryToolStripMenuItem.Name = "DailySalesSummaryToolStripMenuItem"
        Me.DailySalesSummaryToolStripMenuItem.Size = New System.Drawing.Size(260, 26)
        Me.DailySalesSummaryToolStripMenuItem.Text = "Daily Sales Summary"
        '
        'SalesInventorySummaryToolStripMenuItem
        '
        Me.SalesInventorySummaryToolStripMenuItem.Name = "SalesInventorySummaryToolStripMenuItem"
        Me.SalesInventorySummaryToolStripMenuItem.Size = New System.Drawing.Size(260, 26)
        Me.SalesInventorySummaryToolStripMenuItem.Text = "Sales Ranking Report"
        '
        'StandardToolStripMenuItem
        '
        Me.StandardToolStripMenuItem.Name = "StandardToolStripMenuItem"
        Me.StandardToolStripMenuItem.Size = New System.Drawing.Size(224, 26)
        Me.StandardToolStripMenuItem.Text = "Standard"
        '
        'YReadingToolStripMenuItem
        '
        Me.YReadingToolStripMenuItem.Name = "YReadingToolStripMenuItem"
        Me.YReadingToolStripMenuItem.Size = New System.Drawing.Size(224, 26)
        Me.YReadingToolStripMenuItem.Text = "Y-Reading"
        Me.YReadingToolStripMenuItem.Visible = False
        '
        'TerminalZReadingToolStripMenuItem
        '
        Me.TerminalZReadingToolStripMenuItem.Name = "TerminalZReadingToolStripMenuItem"
        Me.TerminalZReadingToolStripMenuItem.Size = New System.Drawing.Size(224, 26)
        Me.TerminalZReadingToolStripMenuItem.Text = "Z-Reading"
        '
        'UtilitiesToolStripMenuItem
        '
        Me.UtilitiesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BackupDatabaseToolStripMenuItem1, Me.RestoreDatabseToolStripMenuItem})
        Me.UtilitiesToolStripMenuItem.Name = "UtilitiesToolStripMenuItem"
        Me.UtilitiesToolStripMenuItem.Size = New System.Drawing.Size(73, 24)
        Me.UtilitiesToolStripMenuItem.Text = "Utilities"
        '
        'BackupDatabaseToolStripMenuItem1
        '
        Me.BackupDatabaseToolStripMenuItem1.Name = "BackupDatabaseToolStripMenuItem1"
        Me.BackupDatabaseToolStripMenuItem1.Size = New System.Drawing.Size(207, 26)
        Me.BackupDatabaseToolStripMenuItem1.Text = "Backup Database"
        '
        'RestoreDatabseToolStripMenuItem
        '
        Me.RestoreDatabseToolStripMenuItem.Name = "RestoreDatabseToolStripMenuItem"
        Me.RestoreDatabseToolStripMenuItem.Size = New System.Drawing.Size(207, 26)
        Me.RestoreDatabseToolStripMenuItem.Text = "Restore Databse"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslDate, Me.tslUser})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 348)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(19, 0, 1, 0)
        Me.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusStrip1.Size = New System.Drawing.Size(1276, 30)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tslDate
        '
        Me.tslDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.tslDate.Name = "tslDate"
        Me.tslDate.Size = New System.Drawing.Size(157, 24)
        Me.tslDate.Text = "ToolStripStatusLabel1"
        '
        'tslUser
        '
        Me.tslUser.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.tslUser.Name = "tslUser"
        Me.tslUser.Size = New System.Drawing.Size(157, 24)
        Me.tslUser.Text = "ToolStripStatusLabel2"
        '
        'ofd
        '
        Me.ofd.FileName = "OpenFileDialog1"
        '
        'mdiMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1276, 378)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "mdiMain"
        Me.ShowIcon = False
        Me.Text = "FoodHaus POS System v1.0 Back End"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tslDate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslUser As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ParametersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DiscountCardsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AffiliatesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MachineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BanksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BinToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InventoryTypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MeasureToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProductCategoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InventoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalesPromoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TermToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents UserManagerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PromoAddOnsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AdminitratorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TerminalZReadingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sfd As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents UtilitiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackupDatabaseToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RestoreDatabseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackendToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EventLogsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalesReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents YReadingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CancelledReceiptToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VoidItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BIRSalesSummaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AccumulatedGrandTotalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ComplementaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChargeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EJournalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SpecialDiscountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StandardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DailySalesSummaryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SalesInventorySummaryToolStripMenuItem As ToolStripMenuItem
End Class
