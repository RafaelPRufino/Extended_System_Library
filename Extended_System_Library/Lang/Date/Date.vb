Namespace Lang
    Namespace [Date]
        Public Class [Date]
#Region "Is"
            Public Shared Function isDate(ByVal sData As String, Optional ByVal sFormate As String = "") As Boolean
                Try
                    If TypeName(getDate(sData, sFormate)) = "Date" Then
                        Return True
                    ElseIf TypeName(getDate(sData, [DateFormats].get(DateFormats.Formats.ddmmyyyy))) = "Date" Then
                        Return True
                    ElseIf TypeName(getDate(sData, [DateFormats].get(DateFormats.Formats.ddmmyy))) = "Date" Then
                        Return True
                    ElseIf TypeName(getDate(sData, [DateFormats].get(DateFormats.Formats.dmyyyyhmms))) = "Date" Then
                        Return True
                    ElseIf TypeName(getDate(sData, [DateFormats].get(DateFormats.Formats.dmy))) = "Date" Then
                        Return True
                    ElseIf TypeName(getDate(sData, [DateFormats].get(DateFormats.Formats.mmyyyy))) = "Date" Then
                        Return True
                    End If
                    Return False
                Catch ex As Exception
                    Return False
                End Try

            End Function

            Public Shared Function isHour(ByVal sData As String, Optional ByVal sFormate As String = "") As Boolean
                Try
                    If TypeName(getDate(sData, sFormate)) = "Date" Then
                        Return True
                    End If
                    Return False
                Catch ex As Exception
                    Return False
                End Try
            End Function

            Public Shared Function isDateTime(ByVal sData As String) As Boolean
                Try
                    If TypeName(Date.ParseExact(sData, [DateFormats].get(DateFormats.Formats.dmyhmms), Globalization.CultureInfo.InvariantCulture)) = "Date" Then
                        Return True
                    ElseIf TypeName(Date.ParseExact(sData, [DateFormats].get(DateFormats.Formats.dmyyyyhmms), Globalization.CultureInfo.InvariantCulture)) = "Date" Then
                        Return True
                    End If

                    Return False
                Catch ex As Exception
                    Return False
                End Try
            End Function

            Public Shared ReadOnly Property isNullOrEmpty(ByVal [date] As Object) As Boolean
                Get
                    Try
                        If String.IsNullOrEmpty([date]) Then
                            Return True
                        End If

                        If [date].GetType Is GetType(System.DateTime) Then
                            Return [date] <= Date.MinValue
                        End If


                        Return DateTime.Parse([date]) <= Date.MinValue
                    Catch ex As Exception
                        Return True
                    End Try
                End Get
            End Property
#End Region
#Region "Convert"
            Public Shared Function MinutesToSeconds(newIntegerWaitMinutes As Double) As Double
                Return TimeSpan.FromMinutes(newIntegerWaitMinutes).TotalSeconds
            End Function

            Public Shared Function MinutesToMilliSeconds(newIntegerWaitSeconds) As Double
                Return TimeSpan.FromMinutes(newIntegerWaitSeconds).TotalMilliseconds
            End Function

            Public Shared Function SecondsToMilliSeconds(newIntegerWaitSeconds) As Double
                Return TimeSpan.FromSeconds(newIntegerWaitSeconds).TotalMilliseconds
            End Function

            Public Shared Function DaysToMinutes(newIntegerDays) As Double
                Return TimeSpan.FromDays(newIntegerDays).TotalMinutes
            End Function

            Public Shared Function DaysToSeconds(newIntegerDays) As Double
                Return TimeSpan.FromDays(newIntegerDays).TotalSeconds
            End Function

            Public Shared Function DaysToMilliSeconds(newIntegerDays) As Double
                Return TimeSpan.FromDays(newIntegerDays).TotalMilliseconds
            End Function

            Public Shared Function ToTimeStamp(ByVal utcNow As DateTime) As Double
                Return utcNow.Subtract(New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            End Function

            Public Shared Function ToDateTime(ByVal _UnixTimeStamp As Double) As DateTime
                Return (New DateTime(1970, 1, 1, 0, 0, 0)).AddSeconds(_UnixTimeStamp)
            End Function
#End Region

            Public Shared ReadOnly Property StartOfMonth(ByVal dtReference As Date) As Date
                Get
                    Return StartOfDate(New DateTime(dtReference.Year, dtReference.Month, 1))
                End Get
            End Property

            Public Shared ReadOnly Property EndOfMonth(ByVal dtReference As Date) As Date
                Get
                    Return EndOfDate(StartOfMonth(dtReference).AddMonths(1).AddDays(-1))
                End Get
            End Property

            Public Shared ReadOnly Property StartOfDate(ByVal dtReference As Date) As Date
                Get
                    Return New DateTime(dtReference.Year, dtReference.Month, dtReference.Day)
                End Get
            End Property

            Public Shared ReadOnly Property EndOfDate(ByVal dtReference As Date) As Date
                Get
                    Return StartOfDate(dtReference).AddDays(1).AddSeconds(-1)
                End Get
            End Property

            Public Shared ReadOnly Property DiffMinutes(ByVal dtReference As DateTime) As Double
                Get
                    Return DateDiff(DateInterval.Minute, dtReference, DateTime.Now)
                End Get
            End Property

            Public Shared Function getDate(ByVal sData As String, ByVal sFormato As String)
                Try
                    Return Date.ParseExact(sData, sFormato, Globalization.CultureInfo.InvariantCulture)
                Catch ex As Exception
                    Return Nothing
                End Try
            End Function
        End Class
    End Namespace
End Namespace