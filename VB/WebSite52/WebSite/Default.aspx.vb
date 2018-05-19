Imports Microsoft.VisualBasic
Imports System
Imports System.Data
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
		If (Not IsPostBack) Then
			AddHiddenSeriesToChart(WebChartControl1)
		End If
		ASPxPivotGrid1.DataSource = CreateTable()

	End Sub

	Private Sub AddHiddenSeriesToChart(ByVal chart As WebChartControl)
		Dim s As New Series("HiddenSeries", ViewType.Point)
		s.Label.Visible = False
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

		data.Rows.Add(New Object() { "Aaa", DateTime.Today, 7 })
		'data.Rows.Add(new object[] { "Bbb", DateTime.Today, 12 });
		data.Rows.Add(New Object() { "Ccc", DateTime.Today, 11 })
		data.Rows.Add(New Object() { "Ddd", DateTime.Today, 5 })


		data.Rows.Add(New Object() { "Aaa", DateTime.Today.AddYears(1), 4 })
		data.Rows.Add(New Object() { "Bbb", DateTime.Today.AddYears(1), 3 })
		'data.Rows.Add(new object[] { "Ccc", DateTime.Today.AddYears(1), 8 });
		data.Rows.Add(New Object() { "Ddd", DateTime.Today.AddYears(1), 9 })

		Return data
	End Function
	Private argumentsCollection As New Dictionary(Of Integer, Object)()
	Protected Sub ASPxPivotGrid1_CustomChartDataSourceData(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxPivotGrid.PivotCustomChartDataSourceDataEventArgs)
		If e.ItemDataMember = PivotChartItemDataMember.Argument AndAlso e.FieldValueInfo IsNot Nothing Then
			argumentsCollection(e.FieldValueInfo.MinIndex) = e.Value
		End If
	End Sub
	Protected Sub WebChartControl1_BoundDataChanged(ByVal sender As Object, ByVal e As EventArgs)
		Dim chart As WebChartControl = TryCast(sender, WebChartControl)
		If chart.Series.Count > 1 Then
			Dim s As Series = chart.Series("HiddenSeries")

			Dim minValue As Double = Double.MaxValue
			Dim maxValue As Double = Double.MinValue
			For Each series As Series In chart.Series
				If series.Name = "HiddenSeries" Then
					Continue For
				End If
				For Each point As SeriesPoint In series.Points
					If maxValue < point.Values(0) Then
						maxValue = point.Values(0)
					End If
					If minValue > point.Values(0) Then
						minValue = point.Values(0)
					End If
				Next point
			Next series

			s.Points.Clear()
			Dim columnCount As Integer = argumentsCollection.Count
			For i As Integer = 0 To columnCount - 1
				Dim sortOrder As Double = minValue + (maxValue - minValue) / columnCount * i
				s.Points.Add(New SeriesPoint(argumentsCollection(i), New Object() { sortOrder }))
			Next i
		End If
	End Sub
	Protected Sub ASPxCheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		SetSeriesVisibility(WebChartControl1.Series("HiddenSeries"), Not(CType(sender, ASPxCheckBox)).Checked)
	End Sub
	Protected Sub ASPxCheckBox2_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		SetSortingBySeries(WebChartControl1.Series("HiddenSeries"), (CType(sender, ASPxCheckBox)).Checked)
	End Sub
End Class
