<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
<!-- default file list end -->
# (Obsolete) Pivot Chart Integration - How to correct chart arguments order 
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e3764)**
<!-- run online end -->


<p>Update: Starting with version 15.2, it is possible to use the <strong>AxisBase.QualitativeScaleComparer </strong>property to sort values manually. Refer to the <a href="https://www.devexpress.com/Support/Center/p/S31660">S31660: Qualitative Scale - Provide custom sorting</a> thread for additional information. </p>
<p><br>If the pivot grid control has empty cells the order of arguments in the attached chart can differ from the order of pivot grid columns. The problem appears because the XtraCharts control sorts data on the Series level. E.g in this example there are 2 different series: "2012" and "2013". Each series contains points corresponding to three arguments: 2012: Aaa, Ccc, Ddd and 2013: Aaa, Bbb, Ddd. The resulting axis scale will contain 4 arguments: Aaa, Bbb, Ccc, Ddd. Note that a relative order of arguments B and C is not set anywhere, so argument C is placed at the first position, because itis provided by the first series. To solve the issue it is necessary to add a custom series to the chart control. This series should provide the correct sort order by using the SeriesBase.SeriesPointsSorting feature.</p>

<br/>


