<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxwpg" %>
<%@ Register Assembly="DevExpress.XtraCharts.v11.2.Web, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Untitled Page</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<dxwpg:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" EnableCallBacks="False" OnCustomChartDataSourceData="ASPxPivotGrid1_CustomChartDataSourceData">
			<Fields>
				<dxwpg:PivotGridField ID="fieldName" Area="ColumnArea" AreaIndex="0" FieldName="Name">
				</dxwpg:PivotGridField>
				<dxwpg:PivotGridField ID="fieldYear" Area="RowArea" AreaIndex="0" FieldName="Date"
					Caption="Year" GroupInterval="DateYear" UnboundFieldName="fieldYear">
				</dxwpg:PivotGridField>
				<dxwpg:PivotGridField ID="fieldValue" Area="DataArea" AreaIndex="0" FieldName="Value">
				</dxwpg:PivotGridField>
			</Fields>
			<OptionsChartDataSource ProvideColumnGrandTotals="false" ProvideRowGrandTotals="false"
				ProvideDataByColumns="false" ProvideEmptyCells="false" />
		</dxwpg:ASPxPivotGrid>
		<dxchartsui:WebChartControl ID="WebChartControl1" runat="server" DataSourceID="ASPxPivotGrid1"
			Height="300px" SeriesDataMember="Series" Width="600px" OnBoundDataChanged="WebChartControl1_BoundDataChanged">
			<legend maxhorizontalpercentage="30"></legend>
			<seriestemplate argumentdatamember="Arguments" valuedatamembersserializable="Values"><ViewSerializable>
<cc1:SideBySideBarSeriesView></cc1:SideBySideBarSeriesView>
</ViewSerializable>
<LabelSerializable>
<cc1:SideBySideBarSeriesLabel LineVisible="True">
<FillStyle><OptionsSerializable>
<cc1:SolidFillOptions></cc1:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
</cc1:SideBySideBarSeriesLabel>
</LabelSerializable>
<PointOptionsSerializable>
<cc1:PointOptions></cc1:PointOptions>
</PointOptionsSerializable>
<LegendPointOptionsSerializable>
<cc1:PointOptions></cc1:PointOptions>
</LegendPointOptionsSerializable>
</seriestemplate>
			<fillstyle><OptionsSerializable>
<cc1:SolidFillOptions></cc1:SolidFillOptions>
</OptionsSerializable>
</fillstyle>
			<diagramserializable>
				<cc1:XYDiagram>
					<axisx visibleinpanesserializable="-1">
						<label staggered="True" />
						<range sidemarginsenabled="True" />
					</axisx>
					<axisy visibleinpanesserializable="-1">
						<range sidemarginsenabled="True" />
					</axisy>
				</cc1:XYDiagram>
			</diagramserializable>
		</dxchartsui:WebChartControl>
		<table>
			<tr>
				<td>
					<dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Checked="true"  
						Text="Hide Sorting Series" AutoPostBack="true" 
						oncheckedchanged="ASPxCheckBox1_CheckedChanged" >
					</dx:ASPxCheckBox>
				</td>
				<td>
					<dx:ASPxCheckBox ID="ASPxCheckBox2" runat="server" Checked="true"  
						Text="Use Sorting Series" AutoPostBack="true" 
						oncheckedchanged="ASPxCheckBox2_CheckedChanged" >
					</dx:ASPxCheckBox>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>