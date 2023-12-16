'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
' Guanzon Software Engineering Group
' Guanzon Group of Companies
' Perez Blvd., Dagupan City
'
'     Sales Promo Maintenance Object
'
' Copyright 2016 and Beyond
' All Rights Reserved
' ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº
' €  All  rights reserved. No part of this  software  €€  This Software is Owned by        €
' €  may be reproduced or transmitted in any form or  €€                                   €
' €  by   any   means,  electronic   or  mechanical,  €€    GUANZON MERCHANDISING CORP.    €
' €  including recording, or by information  storage  €€     Guanzon Bldg. Perez Blvd.     €
' €  and  retrieval  systems, without  prior written  €€           Dagupan City            €
' €  from the author.                                 €€  Tel No. 522-1085 ; 522-9275      €
' ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº
'
' ==========================================================================================
'  Kalyptus [ 10/22/2016 01:15 m ]
'      Started creating this object.
'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
Imports MySql.Data.MySqlClient
Imports ADODB
Imports ggcAppDriver

Public Class SalesPromo
    Private p_oApp As GRider
    Private p_oDTMstr As DataTable
    Private p_oOthersx As New Others

    Private p_nEditMode As xeEditMode
    Private p_sParent As String

    Private Const p_sMasTable As String = "Sales_Promo"

    Private Const p_sMsgHeadr As String = "Sales Promo Maintenance"

    Public Event MasterRetrieved(ByVal Index As Integer, _
                                  ByVal Value As Object)

    Public ReadOnly Property AppDriver() As ggcAppDriver.GRider
        Get
            Return p_oApp
        End Get
    End Property

    Public Property Master(ByVal Index As Integer) As Object
        Get
            If p_nEditMode <> xeEditMode.MODE_UNKNOWN Then
                Return p_oDTMstr(0).Item(Index)
            Else
                Return vbEmpty
            End If
        End Get

        Set(ByVal value As Object)
            If p_nEditMode <> xeEditMode.MODE_UNKNOWN Then
                Select Case Index
                    Case 80         'sBranchNm
                        Call getBranch(2, 80, value, False, False)
                    Case 81         'sBarCodex
                        Call getItem(4, 81, value, False, False)
                    Case 82         'xDescript
                        Call getItem(4, 82, value, False, False)
                    Case 83         'sCategrNm
                        Call getCategory(3, 83, value, False, False)
                    Case 0, 16, 17  'sTransNox, sModified, dModified
                    Case 5, 8       'nDiscRate, nExtDRate
                        If IsNumeric(value) Then
                            p_oDTMstr(0).Item(Index) = value
                        End If
                        RaiseEvent MasterRetrieved(Index, p_oDTMstr(0).Item(Index))
                    Case 6, 9       'nDiscAmtx, nExtDAmtx
                        If IsNumeric(value) Then
                            p_oDTMstr(0).Item(Index) = value
                        End If
                        RaiseEvent MasterRetrieved(Index, p_oDTMstr(0).Item(Index))
                    Case 7, 10      'nMinQtyxx, nExtQtyxx
                        If IsNumeric(value) Then
                            p_oDTMstr(0).Item(Index) = value
                        End If
                        RaiseEvent MasterRetrieved(Index, p_oDTMstr(0).Item(Index))
                    Case 11, 12     'dHappyHrF, dHappyHrT
                        If IsDate(value) Then
                            p_oDTMstr(0).Item(Index) = value
                        End If
                        RaiseEvent MasterRetrieved(Index, p_oDTMstr(0).Item(Index))
                    Case 13, 14     'dPromoFrm, dPromoTru
                        If IsDate(value) Then
                            p_oDTMstr(0).Item(Index) = CDate(value)
                        End If
                        RaiseEvent MasterRetrieved(Index, p_oDTMstr(0).Item(Index))
                    Case Else
                        p_oDTMstr(0).Item(Index) = value
                End Select
            End If
        End Set
    End Property

    'Property Master(String)
    Public Property Master(ByVal Index As String) As Object
        Get
            If p_nEditMode <> xeEditMode.MODE_UNKNOWN Then
                Return p_oDTMstr(0).Item(Index)
            Else
                Return vbEmpty
            End If
        End Get

        Set(ByVal value As Object)
            If p_nEditMode <> xeEditMode.MODE_UNKNOWN Then
                Select Case LCase(Index)
                    Case "sbranchnm"
                        Call getBranch(2, 80, value, False, False)
                    Case "sbarcodex"
                        Call getItem(4, 81, value, False, False)
                    Case "xdescript"
                        Call getItem(4, 82, value, False, False)
                    Case "scategrnm"
                        Call getCategory(3, 83, value, False, False)
                    Case Else
                        Master(p_oDTMstr.Columns(Index).Ordinal) = value
                End Select
            End If
        End Set
    End Property

    'Property EditMode()
    Public ReadOnly Property EditMode() As xeEditMode
        Get
            Return p_nEditMode
        End Get
    End Property

    Public Property Parent() As String
        Get
            Return p_sParent
        End Get
        Set(ByVal value As String)
            p_sParent = value
        End Set
    End Property

    'Public Function NewRecord()
    Public Function NewRecord() As Boolean
        Dim lsSQL As String

        lsSQL = AddCondition(getSQ_Master, "0=1")
        p_oDTMstr = p_oApp.ExecuteQuery(lsSQL)
        p_oDTMstr.Rows.Add(p_oDTMstr.NewRow())

        Call initMaster()
        Call initOthers()

        p_nEditMode = xeEditMode.MODE_ADDNEW

        Return True
    End Function

    'Public Function OpenRecord(String)
    Public Function OpenRecord(ByVal fsRecdIDxx As String) As Boolean
        Dim lsSQL As String

        lsSQL = AddCondition(getSQ_Master, "sTransNox = " & strParm(fsRecdIDxx))
        p_oDTMstr = p_oApp.ExecuteQuery(lsSQL)

        If p_oDTMstr.Rows.Count <= 0 Then
            p_nEditMode = xeEditMode.MODE_UNKNOWN
            Return False
        End If

        Call initOthers()

        p_nEditMode = xeEditMode.MODE_READY
        Return True
    End Function

    'Public Function SearchTransaction(String, Boolean, Boolean=False)
    Public Function SearchRecord( _
                        ByVal fsValue As String _
                      , Optional ByVal fbByCode As Boolean = False) As Boolean

        Dim lsSQL As String

        'Check if already loaded base on edit mode
        If p_nEditMode = xeEditMode.MODE_READY Or p_nEditMode = xeEditMode.MODE_UPDATE Then
            If fbByCode Then
                If fsValue = p_oDTMstr(0).Item("sTransNox") Then Return True
            Else
                If fsValue = p_oDTMstr(0).Item("sDescript") Then Return True
            End If
        End If

        'Initialize SQL filter
        lsSQL = getSQ_Browse()

        'create Kwiksearch filter
        Dim lsFilter As String
        If fbByCode Then
            lsFilter = "sTransNox = " & strParm(fsValue)
        Else
            lsFilter = "sDescript like " & strParm(fsValue & "%")
        End If

        Dim loDta As DataRow = KwikSearch(p_oApp _
                                        , lsSQL _
                                        , False _
                                        , lsFilter _
                                        , "sTransNox»sDescript»dPromoFrm»sBranchNm" _
                                        , "Trans No»Promo»Date From»Branch", _
                                        , "sTransNox»sDescript»dPromoFrm»sBranchNm" _
                                        , IIf(fbByCode, 0, 1))
        If IsNothing(loDta) Then
            p_nEditMode = xeEditMode.MODE_UNKNOWN
            Return False
        Else
            Return OpenRecord(loDta.Item("sTransNox"))
        End If
    End Function

    'Public Function SaveTransaction
    Public Function SaveRecord() As Boolean
        If Not (p_nEditMode = xeEditMode.MODE_ADDNEW Or _
                p_nEditMode = xeEditMode.MODE_READY Or _
                p_nEditMode = xeEditMode.MODE_UPDATE) Then

            MsgBox("Invalid Edit Mode detected!", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, p_sMsgHeadr)
            Return False
        End If

        If Not isEntryOk() Then
            Return False
        End If

        Dim lsSQL As String = ""

        If p_sParent = "" Then p_oApp.BeginTransaction()

        'Save master table 
        If p_nEditMode = xeEditMode.MODE_ADDNEW Then
            lsSQL = ADO2SQL(p_oDTMstr, p_sMasTable, , Encrypt(p_oApp.UserID), p_oApp.SysDate)
        Else
            lsSQL = ADO2SQL(p_oDTMstr, p_sMasTable, "sTransNox = " & strParm(p_oDTMstr(0).Item("sTransNox")), Encrypt(p_oApp.UserID), Format(p_oApp.SysDate, xsDATE_SHORT))
        End If

        If lsSQL <> "" Then
            p_oApp.Execute(lsSQL, p_sMasTable)
        End If

        If p_sParent = "" Then p_oApp.CommitTransaction()

        p_nEditMode = xeEditMode.MODE_READY

        Return True
    End Function

    'Public Function CancelTransaction
    Public Function CancelRecord() As Boolean
        If Not (p_nEditMode = xeEditMode.MODE_READY Or _
                p_nEditMode = xeEditMode.MODE_UPDATE) Then

            MsgBox("Invalid Edit Mode detected!", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, p_sMsgHeadr)
            Return False
        End If

        Dim lsSQL As String

        If p_sParent = "" Then p_oApp.BeginTransaction()

        p_oDTMstr(0).Item("cRecdStat") = "3"
        lsSQL = ADO2SQL(p_oDTMstr, p_sMasTable, "sTransNox = " & strParm(p_oDTMstr(0).Item("sTransNox")))
        p_oApp.Execute(lsSQL, p_sMasTable, p_oApp.BranchCode)

        If p_sParent = "" Then p_oApp.CommitTransaction()

        Return True
    End Function

    'This method implements a search master where id and desc are not joined.
    Private Sub getBranch(ByVal fnColIdx As Integer _
                        , ByVal fnColDsc As Integer _
                        , ByVal fsValue As String _
                        , ByVal fbIsCode As Boolean _
                        , ByVal fbIsSrch As Boolean)

        'Compare the value to be search against the value in our column
        If fbIsCode Then
            If fsValue = p_oDTMstr(0).Item(fnColIdx) And fsValue <> "" And p_oOthersx.sBranchNm <> "" Then Exit Sub
        Else
            If fsValue = p_oOthersx.sBranchNm And fsValue <> "" Then Exit Sub
        End If

        Dim lsSQL As String
        lsSQL = "SELECT" & _
                       "  a.sBranchCD" & _
                       ", a.sBranchNm" & _
               " FROM `Branch` a" & _
               IIf(fbIsCode = False, " WHERE a.cRecdStat = '1'", "")

        'Are we using like comparison or equality comparison
        If fbIsSrch Then
            Dim loRow As DataRow = KwikSearch(p_oApp _
                                             , lsSQL _
                                             , True _
                                             , fsValue _
                                             , "sBranchCD»sBranchNm" _
                                             , "Code»Branch", _
                                             , "a.sBranchCD»a.sBranchNm" _
                                             , IIf(fbIsCode, 0, 1))
            If IsNothing(loRow) Then
                p_oDTMstr(0).Item(fnColIdx) = ""
                p_oOthersx.sBranchNm = ""
            Else
                p_oDTMstr(0).Item(fnColIdx) = loRow.Item("sBranchCD")
                p_oOthersx.sBranchNm = loRow.Item("sBranchNm")
            End If

            RaiseEvent MasterRetrieved(fnColDsc, p_oOthersx.sBranchNm)
            Exit Sub

        End If

        If fsValue <> "" Then
            If fbIsCode Then
                lsSQL = AddCondition(lsSQL, "a.sBranchCD = " & strParm(fsValue))
            Else
                lsSQL = AddCondition(lsSQL, "a.sBranchNm = " & strParm(fsValue))
            End If
        End If

        Dim loDta As DataTable
        loDta = p_oApp.ExecuteQuery(lsSQL)

        If loDta.Rows.Count = 0 Then
            p_oDTMstr(0).Item(fnColIdx) = ""
            p_oOthersx.sBranchNm = ""
        ElseIf loDta.Rows.Count = 1 Then
            p_oDTMstr(0).Item(fnColIdx) = loDta(0).Item("sBranchCD")
            p_oOthersx.sBranchNm = loDta(0).Item("sBranchNm")
        End If

        RaiseEvent MasterRetrieved(fnColDsc, p_oOthersx.sBranchNm)
    End Sub

    'This method implements a search master where id and desc are not joined.
    Private Sub getItem(ByVal fnColIdx As Integer _
                      , ByVal fnColDsc As Integer _
                      , ByVal fsValue As String _
                      , ByVal fbIsCode As Boolean _
                      , ByVal fbIsSrch As Boolean)

        'Compare the value to be search against the value in our column
        If fbIsCode Then
            If fsValue = p_oDTMstr(0).Item(fnColIdx) And fsValue <> "" And p_oOthersx.xDescript <> "" Then Exit Sub
        Else
            If fnColDsc = 81 Then
                If fsValue = p_oOthersx.sBarCodex And fsValue <> "" Then Exit Sub
            Else
                If fsValue = p_oOthersx.xDescript And fsValue <> "" Then Exit Sub
            End If
        End If

        Dim lsSQL As String
        lsSQL = "SELECT" & _
                       "  a.sBarCodex" & _
                       ", a.sDescript" & _
                       ", a.sStockIDx" & _
               " FROM `Inventory` a" & _
        IIf(fbIsCode = False, " WHERE a.cRecdStat = '1'", "")

        'Are we using like comparison or equality comparison
        If fbIsSrch Then
            Dim loRow As DataRow = KwikSearch(p_oApp _
                                             , lsSQL _
                                             , True _
                                             , fsValue _
                                             , "sBarCodex»sDescript»sStockIDx" _
                                             , "Barcode»Description»Stock ID", _
                                             , "a.sBarCodex»a.sDescript»a.sStockIDx" _
                                             , IIf(fbIsCode, 2, IIf(fnColDsc = 81, 0, 1)))
            If IsNothing(loRow) Then
                p_oDTMstr(0).Item(fnColIdx) = ""
                p_oOthersx.sBarCodex = ""
                p_oOthersx.xDescript = ""
            Else
                p_oDTMstr(0).Item(fnColIdx) = loRow.Item("sStockIDx")
                p_oOthersx.sBarCodex = loRow.Item("sBarCodex")
                p_oOthersx.xDescript = loRow.Item("sDescript")
            End If

            RaiseEvent MasterRetrieved(81, p_oOthersx.sBarCodex)
            RaiseEvent MasterRetrieved(82, p_oOthersx.xDescript)
            Exit Sub

        End If

        If fsValue <> "" Then
            If fbIsCode Then
                lsSQL = AddCondition(lsSQL, "a.sStockIDx = " & strParm(fsValue))
            Else
                If fnColDsc = 81 Then
                    lsSQL = AddCondition(lsSQL, "a.sBarCodex = " & strParm(fsValue))
                Else
                    lsSQL = AddCondition(lsSQL, "a.sDescript = " & strParm(fsValue))
                End If
            End If
        End If

        Dim loDta As DataTable
        loDta = p_oApp.ExecuteQuery(lsSQL)

        If loDta.Rows.Count = 0 Then
            p_oDTMstr(0).Item(fnColIdx) = ""
            p_oOthersx.sBarCodex = ""
            p_oOthersx.xDescript = ""
        ElseIf loDta.Rows.Count = 1 Then
            p_oDTMstr(0).Item(fnColIdx) = loDta(0).Item("sStockIDx")
            p_oOthersx.sBarCodex = loDta(0).Item("sBarCodex")
            p_oOthersx.xDescript = loDta(0).Item("sDescript")
        End If

        RaiseEvent MasterRetrieved(81, p_oOthersx.sBarCodex)
        RaiseEvent MasterRetrieved(82, p_oOthersx.xDescript)
    End Sub

    'This method implements a search master where id and desc are not joined.
    Private Sub getCategory(ByVal fnColIdx As Integer _
                          , ByVal fnColDsc As Integer _
                          , ByVal fsValue As String _
                          , ByVal fbIsCode As Boolean _
                          , ByVal fbIsSrch As Boolean)

        'Compare the value to be search against the value in our column
        If fbIsCode Then
            If fsValue = p_oDTMstr(0).Item(fnColIdx) And fsValue <> "" And p_oOthersx.sCategrNm <> "" Then Exit Sub
        Else
            If fsValue = p_oOthersx.sCategrNm And fsValue <> "" Then Exit Sub
        End If

        Dim lsSQL As String
        lsSQL = "SELECT" & _
                       "  a.sCategrCd" & _
                       ", a.sDescript sCategrNm" & _
               " FROM `Product_Category` a" & _
               IIf(fbIsCode = False, " WHERE a.cRecdStat = '1'", "")

        'Are we using like comparison or equality comparison
        If fbIsSrch Then
            Dim loRow As DataRow = KwikSearch(p_oApp _
                                             , lsSQL _
                                             , True _
                                             , fsValue _
                                             , "sCategrCd»sCategrNm" _
                                             , "Code»Category", _
                                             , "a.sCategrCd»a.sDescript" _
                                             , IIf(fbIsCode, 0, 1))
            If IsNothing(loRow) Then
                p_oDTMstr(0).Item(fnColIdx) = ""
                p_oOthersx.sCategrNm = ""
            Else
                p_oDTMstr(0).Item(fnColIdx) = loRow.Item("sCategrCd")
                p_oOthersx.sCategrNm = loRow.Item("sCategrNm")
            End If

            RaiseEvent MasterRetrieved(fnColDsc, p_oOthersx.sCategrNm)
            Exit Sub

        End If

        If fsValue <> "" Then
            If fbIsCode Then
                lsSQL = AddCondition(lsSQL, "a.sCategrCd = " & strParm(fsValue))
            Else
                lsSQL = AddCondition(lsSQL, "a.sDescript = " & strParm(fsValue))
            End If
        End If

        Dim loDta As DataTable
        loDta = p_oApp.ExecuteQuery(lsSQL)

        If loDta.Rows.Count = 0 Then
            p_oDTMstr(0).Item(fnColIdx) = ""
            p_oOthersx.sCategrNm = ""
        ElseIf loDta.Rows.Count = 1 Then
            p_oDTMstr(0).Item(fnColIdx) = loDta(0).Item("sCategrCd")
            p_oOthersx.sCategrNm = loDta(0).Item("sCategrNm")
        End If

        RaiseEvent MasterRetrieved(fnColDsc, p_oOthersx.sCategrNm)
    End Sub

    Private Sub initMaster()
        Dim lnCtr As Integer
        For lnCtr = 0 To p_oDTMstr.Columns.Count - 1
            Select Case LCase(p_oDTMstr.Columns(lnCtr).ColumnName)
                Case "stransnox"
                    p_oDTMstr(0).Item(lnCtr) = GetNextCode(p_sMasTable, "sTransNox", True, p_oApp.Connection, True, p_oApp.BranchCode)
                Case "ndiscrate", "nextdrate"
                    p_oDTMstr(0).Item(lnCtr) = 0.0
                Case "ndiscamtx", "nextdamtx"
                    p_oDTMstr(0).Item(lnCtr) = 0.0
                Case "nminqtyxx", "nextqtyxx"
                    p_oDTMstr(0).Item(lnCtr) = 0
                Case "dpromofrm", "dpromotru"
                    p_oDTMstr(0).Item(lnCtr) = Format(p_oApp.getSysDate, xsDATE_SHORT)
                Case "dmodified", "smodified"
                Case "crecdstat"
                    p_oDTMstr(0).Item(lnCtr) = "0"
                Case Else
                    p_oDTMstr(0).Item(lnCtr) = ""
            End Select
        Next
    End Sub

    Private Sub initOthers()
        p_oOthersx.sBranchNm = ""
        p_oOthersx.sBarCodex = ""
        p_oOthersx.xDescript = ""
    End Sub

    Private Function isEntryOk() As Boolean
        'Check for the information about the card
        If Trim(p_oDTMstr(0).Item("sTransNox")) = "" Then
            MsgBox("Sales Promo No seems to have a problem! Please check your entry....", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, p_sMsgHeadr)
            Return False
        End If

        'Check how much does he intends to borrow
        If Trim(p_oDTMstr(0).Item("sDescript")) = "" Then
            MsgBox("Sales Promo sDescript seems to have a problem! Please check your entry....", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, p_sMsgHeadr)
            Return False
        End If

        Return True
    End Function

    Private Function getSQ_Master() As String
        Return "SELECT a.sTransNox" & _
                    ", a.sDescript" & _
                    ", a.sBranchCd" & _
                    ", a.sCategrCd" & _
                    ", a.sStockIDx" & _
                    ", a.nDiscRate" & _
                    ", a.nDiscAmtx" & _
                    ", a.nMinQtyxx" & _
                    ", a.nExtDRate" & _
                    ", a.nExtDAmtx" & _
                    ", a.nExtQtyxx" & _
                    ", a.dHappyHrF" & _
                    ", a.dHappyHrT" & _
                    ", a.dPromoFrm" & _
                    ", a.dPromoTru" & _
                    ", a.cRecdStat" & _
                    ", a.sModified" & _
                    ", a.dModified" & _
                " FROM " & p_sMasTable & " a"
    End Function

    Private Function getSQ_Browse() As String
        Return "SELECT a.sTransNox" & _
                    ", a.sDescript" & _
                    ", IFNULL(b.sBranchNm, '') sBranchNm" & _
              " FROM " & p_sMasTable & " a" & _
                " LEFT JOIN Branch b On a.sBranchCD = b.sBranchCD"
    End Function

    Public Sub New(ByVal foRider As GRider)
        p_oApp = foRider
        p_nEditMode = xeEditMode.MODE_UNKNOWN
    End Sub

    Private Class Others
        Public sBranchNm As String
        Public sBarCodex As String
        Public xDescript As String
        Public sCategrNm As String
    End Class

End Class
