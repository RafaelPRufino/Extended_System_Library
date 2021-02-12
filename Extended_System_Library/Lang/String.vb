Imports System.Text.RegularExpressions

Namespace Lang
    Public Class [String]

        Enum ReplaceType
            Onlynumbers = 1
        End Enum

        Public Shared Function match(ByVal strTarget As String, ByVal strSearch As String) As Boolean
            Try
                If String.IsNullOrEmpty(strTarget) Then
                    Return False
                End If

                Return strTarget.IndexOf(strSearch) <> -1
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function navegate(ByRef target As String, ByRef self As String, ByVal match As String) As Boolean
            Try
                If [String].match(target, match) = False AndAlso String.IsNullOrEmpty(target) = False Then
                    self = target
                    target = ""
                    Return True
                ElseIf [String].match(target, match) Then
                    Dim lPosReserva = target.IndexOf(match)
                    Dim lTamReserva = match.Length
                    lPosReserva = lPosReserva + lTamReserva
                    self = target.Substring(0, (lPosReserva)).Trim
                    target = target.Substring(lPosReserva).Trim
                    Return True
                End If

                Return False
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function padLeft(ByVal str As String, ByVal length As Integer, ByVal ch As String)
            If str Is Nothing Then
                Return str
            End If

            Return IIf(str.PadLeft(length, ch).Length > length, str.PadLeft(length, ch).Substring(0, length), str.PadLeft(length, ch))
        End Function

        Public Shared Function onlyNumbers(ByVal stringText As String) As String
            Try
                Dim stringString As String = ""

                If String.IsNullOrEmpty(stringText) Then
                    Return ""
                End If

                stringString = stringText.Trim
                stringString = System.Text.RegularExpressions.Regex.Replace(stringString, "\D", "")
                Return stringString.ToString
            Catch ex As Exception
                Return ""
            End Try
        End Function

        Public Shared Function replaceAll(ByVal stringText As String, ByVal str As String, Optional ByVal strReplace As String = "") As String
            Try
                Dim stringString As String = ""

                If String.IsNullOrEmpty(stringText) Then
                    Return ""
                End If

                stringString = stringText.Trim
                While Lang.String.match(stringString, str)
                    stringString = stringString.Replace(str, strReplace)
                End While
                Return stringString.ToString
            Catch ex As Exception
                Return ""
            End Try
        End Function

        Public Shared Function handleContentTAG(ByVal sPLHtmlLido As String, ByVal sPLTextoInicial As String, ByVal sPLTextoFinal As String) As String
            Try
                Dim sVarReserva As String
                Dim lPosReserva As Int32
                Dim lTamReserva As Int32
                Dim sRetorno As String = ""
                sVarReserva = sPLTextoInicial
                lPosReserva = 0
                lTamReserva = 0
                If sPLHtmlLido.IndexOf(sVarReserva) > -1 Then
                    lPosReserva = sPLHtmlLido.IndexOf(sVarReserva)
                    lTamReserva = sVarReserva.Length
                    lPosReserva = lPosReserva + lTamReserva
                    sRetorno = sPLHtmlLido.Substring(lPosReserva, sPLHtmlLido.IndexOf(sPLTextoFinal, lPosReserva) - (lPosReserva)).Replace(sPLTextoFinal, "").Replace("&nbsp;", "").Replace("  ", "").Replace("<br>", "").Replace("<br />", "").Replace("-", "").Replace("-", "").Replace("<td>", "").Replace("</td>", "").Trim
                End If
                Return sRetorno
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Function handleContentTAGNoEdit(ByVal sPLHtmlLido As String, ByVal sPLTextoInicial As String, ByVal sPLTextoFinal As String) As String
            Try
                Dim sVarReserva As String
                Dim lPosReserva As Int32
                Dim lTamReserva As Int32
                Dim sRetorno As String = ""
                sVarReserva = sPLTextoInicial
                lPosReserva = 0
                lTamReserva = 0
                If sPLHtmlLido.IndexOf(sVarReserva) > -1 Then
                    lPosReserva = sPLHtmlLido.IndexOf(sVarReserva)
                    lTamReserva = sVarReserva.Length
                    lPosReserva = lPosReserva + lTamReserva
                    sRetorno = sPLHtmlLido.Substring(lPosReserva, sPLHtmlLido.IndexOf(sPLTextoFinal, lPosReserva) - (lPosReserva)).Replace(sPLTextoFinal, "").Replace("&nbsp;", "")
                End If
                Return sRetorno
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Function handleContentTAGRemove(ByRef sPLHtmlLido As String, ByVal sPLTextoInicial As String, ByVal sPLTextoFinal As String) As String
            Try
                Dim sVarReserva As String
                Dim lPosReserva As Int32
                Dim lTamReserva As Int32
                Dim sRetorno As String = ""
                sVarReserva = sPLTextoInicial
                lPosReserva = 0
                lTamReserva = 0
                If sPLHtmlLido.IndexOf(sVarReserva) > -1 Then
                    Dim strAntes As String
                    Dim strDepois As String
                    lPosReserva = sPLHtmlLido.IndexOf(sVarReserva)
                    lTamReserva = sVarReserva.Length
                    lPosReserva = lPosReserva + lTamReserva
                    sRetorno = sPLHtmlLido.Substring(lPosReserva, sPLHtmlLido.IndexOf(sPLTextoFinal, lPosReserva) - (lPosReserva)).Replace(sPLTextoFinal, "")

                    strAntes = sPLHtmlLido.Substring(0, lPosReserva - lTamReserva)
                    strDepois = sPLHtmlLido.Substring(sPLHtmlLido.IndexOf(sPLTextoFinal, lPosReserva) + sPLTextoFinal.Length)
                    sPLHtmlLido = strAntes & strDepois
                    sPLHtmlLido = Trim(sPLHtmlLido).Trim
                End If
                Return sRetorno
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Function handleContentTAGRef(ByVal strTagStart As String, ByVal strTagEnd As String, ByVal strText As String) As String
            Try
                Dim indexStart As Integer = strText.IndexOf(strTagStart)
                Dim indexEnd As Integer
                Dim indexCalculate As Integer

                If String.IsNullOrEmpty(strTagStart) Then
                    Return strText
                End If

                If indexStart = -1 Then Return ""

                indexStart += strTagStart.Length
                indexEnd = strText.IndexOf(strTagEnd, indexStart)
                indexCalculate = indexEnd - indexStart

                If indexStart = -1 Then Return ""

                If Not strTagEnd = "" Then
                    If indexEnd = -1 Then Return ""
                Else
                    Return strText.Substring(indexStart)
                End If

                Return strText.Substring(indexStart, indexCalculate)
            Catch ex As Exception
                Return ""
            End Try
        End Function

        Public Shared Function Mask(ByVal stringText As Object, ByVal stringMaskText As String) As String
            Try
                Dim sMakared = ""
                Dim k = 0
                For i As Integer = 0 To stringMaskText.Length - 1
                    If (stringMaskText(i) = "_") Then
                        If ((stringText(k) <> "")) Then
                            sMakared &= stringText(k)
                            k = k + 1
                        End If
                    ElseIf (stringMaskText(i) = "%") Then
                        For j As Integer = k To stringText.Length - 1
                            sMakared &= stringText(k)
                            k = k + 1
                        Next
                    ElseIf (stringMaskText(i) <> "") Then
                        sMakared &= stringMaskText(i)
                    End If

                Next
                Return sMakared
            Catch ex As Exception
            End Try
            Return stringText
        End Function

        Public Shared Function Mask(ByVal stringText As Object, ByVal stringMaskText As String, ByVal booleanReplaceNothing As Boolean, Optional ByVal rTypeReplace As ReplaceType = [String].ReplaceType.Onlynumbers) As String
            Try
                Dim stringMakared As String = ""
                Dim indexMaskared As Integer = 0

                If stringText Is Nothing Then
                    If rTypeReplace = ReplaceType.Onlynumbers Then
                        stringText = onlyNumbers(stringText)
                    End If
                End If

                If booleanReplaceNothing Then
                    stringText = stringText
                End If

                For index As Integer = 0 To stringMaskText.Length - 1
                    If (stringMaskText(index) = "_") Then
                        If ((stringText(indexMaskared) <> "")) Then
                            stringMakared &= stringText(indexMaskared)
                            indexMaskared = indexMaskared + 1
                        End If
                    Else
                        If (stringMaskText(index) <> "") Then
                            stringMakared &= stringMaskText(index)
                        End If
                    End If
                Next

                Return stringMakared
            Catch ex As Exception

            End Try

            Return stringText
        End Function

        Public Shared Function punctuation(ByVal strText As String)
            Try
                Dim acentos As String() = New String() {"ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û"}
                Dim semAcento As String() = New String() {"c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U"}

                For i As Integer = 0 To acentos.Length - 1
                    strText = strText.Replace(acentos(i), semAcento(i))
                Next
                Return strText.Trim()
            Catch ex As Exception
                Return strText
            End Try
        End Function

        Public Shared Function splitRegex(ByVal sText As String, ByVal sRegx As String) As String()
            Try
                Dim rgx As New Regex(sRegx)
                Return rgx.Split(sText.Trim)
            Catch ex As Exception
                Return "".Split(",")
            End Try
        End Function

        Public Shared Function straighten(ByVal sText As String, ByVal sRegx As String) As String
            Try
                Dim rgx As String = " "
                Dim arrayValue As New List(Of String)

                For Each strText In sText.Trim.Split(" ")
                    If String.IsNullOrEmpty(strText) = False Then
                        arrayValue.Add(strText)
                    End If
                Next

                Return String.Join(" ", arrayValue.ToArray)
            Catch ex As Exception
                Return ""
            End Try
        End Function

        Public Shared Function regex(ByVal strRgx As String, ByVal strText As String, ByRef sValue As String) As Boolean
            Try
                Dim rgx As Regex

                rgx = New Regex(strRgx)

                regex = rgx.IsMatch(strText)
                If regex Then
                    sValue = rgx.Match(strText).Value
                Else
                    sValue = ""
                End If
            Catch ex As Exception
                sValue = ""
                Return False
            End Try
        End Function
#Region "Is"
        Public Shared Function isBase64(base64String As String) As Boolean
            Try
                Convert.FromBase64String(base64String)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function isString(ByVal obj As Object) As Boolean
            Try
                If IsNothing(obj) Then
                    Return False
                End If

                Return obj.GetType Is "".GetType
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function isNumber(ByVal sString As String) As Boolean
            Try
                If sString.ToString.Trim = "" Then Return False

                Dim sNumber As Double
                Dim sNumber2 As Integer
                Dim sTarifa_ret = CDbl(sString)
                If IsNumeric(sString.Replace(",", "")) = False Or sString.IndexOf("+") > -1 Then
                    Return False
                End If

                If Double.TryParse(sTarifa_ret, sNumber) Then
                    Return True
                ElseIf Integer.TryParse(sTarifa_ret, sNumber2) Then
                    Return True
                End If

                Return False
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function isMoney(ByVal sString As String) As Boolean
            Try
                If isNumber(sString) = False Then
                    Return False
                End If

                Return sString.IndexOf(",") > -1
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function isPercentual(ByVal sString As String) As Boolean
            Try
                Dim strText1 As String = sString
                Dim strText2 As String = sString.Replace("%", "")

                If isNumber(strText2) = False Then
                    Return False
                End If

                Return strText1.IndexOf(",") > -1 And strText1.Contains("%")
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region
#Region "Convert"
        Public Shared Function Encode64(ByVal stringText As String) As String
            Try
                Return Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(stringText))
            Catch ex As Exception
                Return ""
            End Try
        End Function

        Public Shared Function ToInt(ByVal str As String) As Integer
            Try
                If str Is Nothing Or Integer.TryParse(str, 0) = False Then
                    Return 0
                End If

                Return CInt(str)
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Shared Function ToLong(ByVal str As String) As Long
            Try
                If str Is Nothing Or Int64.TryParse(str, 0) = False Then
                    Return 0
                End If

                Return Int64.Parse(str)
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Shared Function ToBool(ByVal str As String) As Boolean
            Try
                If str Is Nothing Then
                    Return 0
                End If

                If ToInt(str) = 1 Then
                    str = True
                Else
                    str = False
                End If

                If Boolean.TryParse(str, True) = False Then
                    Return 0
                End If

                Return Boolean.Parse(str)
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Shared Function ToDouble(ByVal str As String) As Double
            Try
                If str Is Nothing Then
                    Return 0
                End If

                str = str.Replace("*", ",")
                If Double.TryParse(str, True) = False Then
                    Return 0
                End If

                Return Double.Parse(str)
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Shared Function ToDate(ByVal str As String) As DateTime
            Try
                If str Is Nothing Then
                    Return Nothing
                End If
                If Lang.Date.IsDate(str) Then
                    Return Lang.Date.GetDate(str, "dd/mm/yyyy")
                ElseIf Lang.Date.IsDate(str, "ddmmyyyy") Then
                    Return Convert.ToDateTime(str.Substring(0, 2) & "/" & str.Substring(2, 2) & "/" & str.Substring(4, 4))
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
#End Region
    End Class
End Namespace