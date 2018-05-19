using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Data.PivotGrid;
using System.Collections.Generic;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraCharts.Web;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraCharts;
using System.Drawing;
using DevExpress.Web.ASPxEditors;

public partial class _Default : System.Web.UI.Page
{
    DataTable data;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            AddHiddenSeriesToChart(WebChartControl1);
        ASPxPivotGrid1.DataSource = CreateTable();

    }

    private void AddHiddenSeriesToChart(WebChartControl chart)
    {
        Series s = new Series("HiddenSeries", ViewType.Point);
        s.Label.Visible = false;
        s.SeriesPointsSortingKey = SeriesPointKey.Value_1;

        SetSortingBySeries(s, true);        
        SetSeriesVisibility(s, false);

        chart.Series.Add(s);

    }

    private void SetSortingBySeries(Series s, bool sort)
    {
        if (sort)
            s.SeriesPointsSorting = SortingMode.Ascending;
        else
            s.SeriesPointsSorting = SortingMode.None;
        
    }

    private void SetSeriesVisibility(Series s, bool visible)
    {        
        if (visible)
        {
            s.ShowInLegend = true;
            ((PointSeriesView)s.View).Color = Color.Red ;
            ((PointSeriesView)s.View).PointMarkerOptions.BorderVisible = true;
            ((PointSeriesView)s.View).PointMarkerOptions.FillStyle.FillMode = FillMode.Solid;
        }
        else
        {
            s.ShowInLegend = false;
            ((PointSeriesView)s.View).Color = Color.Transparent;
            ((PointSeriesView)s.View).PointMarkerOptions.BorderVisible = false;
            ((PointSeriesView)s.View).PointMarkerOptions.FillStyle.FillMode = FillMode.Solid;
        }
    }



    private DataTable CreateTable()
    {
        data = new DataTable();
        data.Columns.Add("Name", typeof(string));
        data.Columns.Add("Date", typeof(object));
        data.Columns.Add("Value", typeof(int));

        data.Rows.Add(new object[] { "Aaa", DateTime.Today, 7 });
        //data.Rows.Add(new object[] { "Bbb", DateTime.Today, 12 });
        data.Rows.Add(new object[] { "Ccc", DateTime.Today, 11 });
        data.Rows.Add(new object[] { "Ddd", DateTime.Today, 5 });


        data.Rows.Add(new object[] { "Aaa", DateTime.Today.AddYears(1), 4 });
        data.Rows.Add(new object[] { "Bbb", DateTime.Today.AddYears(1), 3 });
        //data.Rows.Add(new object[] { "Ccc", DateTime.Today.AddYears(1), 8 });
        data.Rows.Add(new object[] { "Ddd", DateTime.Today.AddYears(1), 9 });

        return data;
    }
    Dictionary<int, object> argumentsCollection = new Dictionary<int, object>();
    protected void ASPxPivotGrid1_CustomChartDataSourceData(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomChartDataSourceDataEventArgs e)
    {
        if (e.ItemDataMember == PivotChartItemDataMember.Argument && e.FieldValueInfo != null)
        {
            argumentsCollection[e.FieldValueInfo.MinIndex] = e.Value;
        }
    }
    protected void WebChartControl1_BoundDataChanged(object sender, EventArgs e)
    {
        WebChartControl chart = sender as WebChartControl;
        if (chart.Series.Count > 1)
        {
            Series s = chart.Series["HiddenSeries"];

            double minValue = double.MaxValue;
            double maxValue = double.MinValue;
            foreach (Series series in chart.Series)
            {
                if (series.Name == "HiddenSeries") continue;
                foreach (SeriesPoint point in series.Points)
                {
                    if (maxValue < point.Values[0]) maxValue = point.Values[0];
                    if (minValue > point.Values[0]) minValue = point.Values[0];
                }
            }

            s.Points.Clear();
            int columnCount = argumentsCollection.Count;
            for (int i = 0; i < columnCount; i++)
            {
                double sortOrder = minValue + (maxValue - minValue) / columnCount * i;
                s.Points.Add(new SeriesPoint(argumentsCollection[i], new object[] { sortOrder }));
            }
        }
    }
    protected void ASPxCheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        SetSeriesVisibility(WebChartControl1.Series["HiddenSeries"], !((ASPxCheckBox)sender).Checked);
    }
    protected void ASPxCheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        SetSortingBySeries(WebChartControl1.Series["HiddenSeries"], ((ASPxCheckBox)sender).Checked);
    }
}
