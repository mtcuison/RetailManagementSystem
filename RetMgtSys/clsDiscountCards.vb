'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
' Guanzon Software Engineering Group
' Guanzon Group of Companies
' Perez Blvd., Dagupan City
'
'     Retail Discount Cards
'
' Copyright 2012 and Beyond
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
'  iMac-Xurpas [ 10/12/2016 01:20 pm ]
'      Started creating this object.
'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€

Imports MySql.Data.MySqlClient
Imports ADODB
Imports ggcAppDriver

Public Class clsDiscountCards
    Private Const pxeModuleName As String = "clsDiscountCards"

    Private p_oApp As GRider
    Private p_oDTMstr As DataTable
    Private p_oDTDetl As DataTable
    Private p_nEditMode As xeEditMode

    Function InitRecord() As Boolean

        p_nEditMode = xeEditMode.MODE_UNKNOWN

        Return True
    End Function

    Function NewRecord() As Boolean
        p_nEditMode = xeEditMode.MODE_ADDNEW

        Return True
    End Function

    Function UpdateRecord() As Boolean
        p_nEditMode = xeEditMode.MODE_UPDATE

        Return True
    End Function

    Function SaveRecord() As Boolean

        Return InitRecord()
    End Function

    Function BrowseRecord() As Boolean
        Return True
    End Function

    Function OpenRecord(ByVal fsTransNox As String) As Boolean
        Return True
    End Function

    Function DeleteRecord() As Boolean
        Return True
    End Function

    Private Function getSQLMaster() As String
        Return "SELECT" & _
                     "  sCardIDxx" & _
                     ", sCardDesc" & _
                     ", sCompnyCd" & _
                     ", dPrtSince" & _
                     ", dStartxxx" & _
                     ", dExpiratn" & _
                     ", cRecdStat" & _
                     ", sModified" & _
                     ", dModified" & _
                " FROM Discount_Card"
    End Function
End Class
