<%@ Page Language="C#" MasterPageFile="~/CPanel.Master" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="KMS.Summary.Export" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="mb-4">
        <h5 class="font-weight-bold">Master Data</h5>
        <dx:ASPxButton runat="server" ID="btnExportVendor" Text="Export Vendor" OnClick="btnExportVendor_Click" />
        <dx:ASPxButton runat="server" ID="btnExportContract" Text="Export Contract" OnClick="btnExportContract_Click" />
        <dx:ASPxButton runat="server" ID="btnExportServer" Text="Export Server" OnClick="btnExportServer_Click" />
    </div>

    <div class="mb-4">
        <h5 class="font-weight-bold">Chart</h5>
        <dx:ASPxButton runat="server" ID="btnHourlyExport" Text="Export Hourly" OnClick="btnHourlyExport_Click" />
        <dx:ASPxButton runat="server" ID="btnDailyExport" Text="Export Daily" OnClick="btnDailyExport_Click" />
        <dx:ASPxButton runat="server" ID="btnMonthlyExport" Text="Export Monthly" OnClick="btnMonthlyExport_Click" />
    </div>
    
    
</asp:Content>
