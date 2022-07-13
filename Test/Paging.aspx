<%@ Page Language="C#" MasterPageFile="~/CPanel.Master" AutoEventWireup="true" CodeBehind="Paging.aspx.cs" Inherits="KMS.Test.Paging" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function grid_EndCallback(s, e) {
            if (s.cpRefresh) {
                grid.Refresh();
                s.cpRefresh = false;
            }
            if (s.cpShowPopupDaily) {
                s.cpShowPopupDaily = false;
                popupUpdateDaily.Show();
            }
            if (s.cpShowPopupMonthly) {
                s.cpShowPopupMonthly = false;
                popupUpdateMonthly.Show();
            }
            EndCallback(s, e);
        }

        function onToolbarItemClick(s, e) {
            if (e.item.name === 'UpdateDaily' || e.item.name === 'UpdateMonthly') {
                e.processOnServer = true;
            }

            if (e.item.name === 'Rebuild') {
                e.processOnServer = true;
            }
        }

        function onTextChange(s, e) {
            console.log(e.htmlEvent)
            if (e.htmlEvent.key == "Enter") {
                grid.PerformCallback("AA");
            }
        }

    </script>
    <dx:ASPxGridView runat="server" ID="grid" 
        ClientInstanceName="grid"
        Width="100%" CssClass="w-100 mt-3"
        KeyFieldName="LOG_ID"
        OnInit="grid_Init"
        OnCustomCallback="grid_CustomCallback"
        OnPageIndexChanged="grid_PageIndexChanged"
        EnableCallBacks="true">
        <ClientSideEvents EndCallback="grid_EndCallback" ToolbarItemClick="onToolbarItemClick" />
        <%--<SettingsSearchPanel CustomEditorID="tbSearch" />--%>
        <SettingsDataSecurity AllowDelete="false" AllowEdit="false" AllowInsert="false" />
        <SettingsContextMenu Enabled="true">
            <ColumnMenuItemVisibility ClearFilter="true" />
        </SettingsContextMenu>
        <SettingsPager>
            <PageSizeItemSettings Visible="true" Items="5, 10, 20, 50, 100, 200, 500, 1000" />
        </SettingsPager>

        <Toolbars>
            <dx:GridViewToolbar Name="Toolbar">
                <Items>
                    <dx:GridViewToolbarItem Command="Refresh" Name="Refresh" BeginGroup="true" Image-IconID="actions_refresh_16x16office2013" />
                    <dx:GridViewToolbarItem Name="Updates" Text="Update" BeginGroup="true" Image-IconID="actions_sortbyorderdate_16x16devav">
                        <Items>
                            <dx:GridViewToolbarItem Name="UpdateDaily" Text="Daily" />
                            <dx:GridViewToolbarItem Name="UpdateMonthly" Text="Monthly" />
                        </Items>
                    </dx:GridViewToolbarItem>

                    <dx:GridViewToolbarItem AdaptivePriority="1" Name="Export" Text="Download to" Image-IconID="export_export_16x16office2013" BeginGroup="true">
                        <Items>
                            <dx:GridViewToolbarItem Command="ExportToPdf" Visible="false" />
                            <dx:GridViewToolbarItem Command="ExportToDocx" Visible="false" />
                            <dx:GridViewToolbarItem Command="ExportToRtf" Visible="false" />
                            <dx:GridViewToolbarItem Command="ExportToCsv" Image-IconID="mail_sendcsv_16x16office2013" />
                            <dx:GridViewToolbarItem Command="ExportToXls" Text="Export to XLS" Image-IconID="mail_sendxls_16x16office2013" />
                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to XLSX" Image-IconID="export_exporttoxlsx_16x16office2013" />
                            <dx:GridViewToolbarItem Name="CustomExportToXLS" Text="Export to XLS(WYSIWYG)" Visible="false" Image-IconID="export_exporttoxls_16x16office2013" />
                            <dx:GridViewToolbarItem Name="CustomExportToXLSX" Text="Export to XLSX(WYSIWYG)" Visible="false" Image-IconID="export_exporttoxlsx_16x16office2013" />
                        </Items>
                    </dx:GridViewToolbarItem>
                    <dx:GridViewToolbarItem Command="ShowCustomizationWindow" Text="Column chooser" BeginGroup="true" Image-IconID="spreadsheet_pivottablegroupselectioncontextmenuitem_16x16" />
                    <dx:GridViewToolbarItem AdaptivePriority="1" Name="Filter">
                        <Template>
                            <dx:ASPxCheckBox ID="checkShowFilter" runat="server" Text="Show filter column">
                                <ClientSideEvents CheckedChanged="function(s, e) { grid.PerformCallback('checkShowFilter'); }" />
                            </dx:ASPxCheckBox>
                        </Template>
                    </dx:GridViewToolbarItem>
                    <dx:GridViewToolbarItem BeginGroup="true">
                        <Template>
                            <dx:ASPxButtonEdit ClientSideEvents-KeyPress="onTextChange" ID="tbSearch" runat="server" NullText="Search" Height="100%" />
                        </Template>
                    </dx:GridViewToolbarItem>
                    <dx:GridViewToolbarItem Name="Rebuild" Text="Rebuild Index" BeginGroup="true" Image-IconID="reports_groupfieldcollection_16x16office2013" />
                </Items>
            </dx:GridViewToolbar>
        </Toolbars>

        <Columns>
            <dx:GridViewDataTextColumn FieldName="CARDUID" Caption="Card UID" Width="90" MaxWidth="90" Settings-AllowHeaderFilter="True" />
            <dx:GridViewDataDateColumn FieldName="CREATEDATE" Caption="Perso Date" Settings-AllowHeaderFilter="True" Width="70" MaxWidth="70">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn FieldName="UPDATEDATE" Caption="Commit Date" Settings-AllowHeaderFilter="True" Width="70" MaxWidth="70">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="CONTROLNUMBER" Caption="Control Number" Width="70" MaxWidth="90" Visible="false" Settings-AllowHeaderFilter="True" />

            <dx:GridViewDataTextColumn FieldName="MANUFACTURERCODE" Caption="Manufacturer Code" Width="70" MaxWidth="90" Visible="false" Settings-AllowHeaderFilter="True" />

            <dx:GridViewDataTextColumn FieldName="M_VENDOR.VENDOR_NAME" Caption="Vendor" Width="70" MaxWidth="90" Settings-AllowHeaderFilter="True" />

            <dx:GridViewDataTextColumn FieldName="IP_ADDRESS" Caption="IP Address" Visible="false" Width="70" MaxWidth="90" Settings-AllowHeaderFilter="True" />
            <dx:GridViewDataComboBoxColumn FieldName="IP_REQUEST" Caption="Server" Width="70" MaxWidth="90" Settings-AllowHeaderFilter="True">
                <PropertiesComboBox ValueField="ip_address" TextField="server_name" TextFormatString="{0}" DisplayFormatString="{0}">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn FieldName="M_CONTRACT.CONTRACT_NAME" Caption="Contract" Width="90" MaxWidth="90" Settings-AllowHeaderFilter="True" />
            <dx:GridViewDataTextColumn FieldName="PERSOSITE" Caption="Perso Site" Width="50" MaxWidth="60" Settings-AllowHeaderFilter="True" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="ERROR_CODE" Caption="Error Code" Width="90" MaxWidth="90" Settings-AllowHeaderFilter="True" />

        </Columns>

    </dx:ASPxGridView>

    <dx:EntityServerModeDataSource runat="server" ID="IISLogData" ContextTypeName="KMS.Context.KMSContext"
        TableName="LOG_IIS" OnSelecting="IISLogData_Selecting" DefaultSorting="LOG_ID DESC" />
</asp:Content>
