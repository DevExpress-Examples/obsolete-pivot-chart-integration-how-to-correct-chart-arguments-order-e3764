Imports System
Imports System.Data
Imports System.Linq
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Data.PivotGrid
Imports System.Collections.Generic
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraCharts.Web
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.XtraCharts
Imports System.Drawing
Imports DevExpress.Web.ASPxEditors

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Private data As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsPostBack Then
            AddHiddenSeriesToChart(WebChartControl1)
        End If
        ASPxPivotGrid1.DataSource = CreateTable()

    End Sub

    Private Sub AddHiddenSeriesToChart(ByVal chart As WebChartControl)
        Dim s As New Series(hiddenSeriesName, ViewType.Point)
        s.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False
        s.SeriesPointsSortingKey = SeriesPointKey.Value_1

        SetSortingBySeries(s, True)
        SetSeriesVisibility(s, False)

        chart.Series.Add(s)

    End Sub

    Private Sub SetSortingBySeries(ByVal s As Series, ByVal sort As Boolean)
        If sort Then
            s.SeriesPointsSorting = SortingMode.Ascending
        Else
            s.SeriesPointsSorting = SortingMode.None
        End If

    End Sub

    Private Sub SetSeriesVisibility(ByVal s As Series, ByVal visible As Boolean)
        If visible Then
            s.ShowInLegend = True
            CType(s.View, PointSeriesView).Color = Color.Red
            CType(s.View, PointSeriesView).PointMarkerOptions.BorderVisible = True
            CType(s.View, PointSeriesView).PointMarkerOptions.FillStyle.FillMode = FillMode.Solid
        Else
            s.ShowInLegend = False
            CType(s.View, PointSeriesView).Color = Color.Transparent
            CType(s.View, PointSeriesView).PointMarkerOptions.BorderVisible = False
            CType(s.View, PointSeriesView).PointMarkerOptions.FillStyle.FillMode = FillMode.Solid
        End If
    End Sub



    Private Function CreateTable() As DataTable
        data = New DataTable()
        data.Columns.Add("Name", GetType(String))
        data.Columns.Add("Date", GetType(Object))
        data.Columns.Add("Value", GetType(Integer))

        data.Rows.Add(New Object() { "Aaa", Date.Today, 7 })
        'data.Rows.Add(new object[] { "Bbb", DateTime.Today, 12 });
        data.Rows.Add(New Object() { "Ccc", Date.Today, 11 })
        data.Rows.Add(New Object() { "Ddd", Date.Today, 5 })


        data.Rows.Add(New Object() { "Aaa", Date.Today.AddYears(1), 4 })
        data.Rows.Add(New Object() { "Bbb", Date.Today.AddYears(1), 3 })
        'data.Rows.Add(new object[] { "Ccc", DateTime.Today.AddYears(1), 8 });
        data.Rows.Add(New Object() { "Ddd", Date.Today.AddYears(1), 9 })

        Return data
    End Function
    Protected Sub ASPxCheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        SetSeriesVisibility(WebChartControl1.Series(hiddenSeriesName), (Not DirectCast(sender, ASPxCheckBox).Checked))
    End Sub
    Protected Sub ASPxCheckBox2_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        SetSortingBySeries(WebChartControl1.Series(hiddenSeriesName), DirectCast(sender, ASPxCheckBox).Checked)
    End Sub
    Private hiddenSeriesName As String = "csHiddenSeries"
    Protected Sub ASPxPivotGrid1_CustomChartDataSourceRows(ByVal sender As Object, ByVal e As PivotCustomChartDataSourceRowsEventArgs)

        If e.Rows.Count > 1 Then
            Dim retrieveDataByColumns As Boolean = WebChartControl1.PivotGridDataSourceOptions.RetrieveDataByColumns
            Dim hiddenSeries As Series = WebChartControl1.Series(hiddenSeriesName)

            Dim arguments() As Object = e.Rows.OrderBy(Function(r) GetSortOder(retrieveDataByColumns, r)).Select(Function(r) r.Argument).Distinct().ToArray()
            Dim values = e.Rows.Select(Function(r) Convert.ToDouble(r.Value))
            Dim minValue As Double = values.Min()
            Dim [step] As Double = (values.Max() - minValue) / arguments.Length
            hiddenSeries.Points.Clear()
            For i As Integer = 0 To arguments.Length - 1
                hiddenSeries.Points.Add(New SeriesPoint(arguments(i), New Object() { minValue + [step] * CDbl(i) }))
            Next i
        End If
    End Sub

    Private Function GetSortOder(ByVal byColumn As Boolean, ByVal r As PivotChartDataSourceRow) As Integer
        If byColumn Then
            Return r.CellInfo.RowIndex
        Else
            Return r.CellInfo.ColumnIndex
        End If
    End Function


End Class
