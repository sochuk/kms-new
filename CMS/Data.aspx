<%@ Page Language="C#" MasterPageFile="~/CPanel.Master" AutoEventWireup="true" CodeBehind="Data.aspx.cs" Inherits="CMS.Data" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .chartTitle {
            font-size: 20px !important;
        }

        rect[fill=gray] {
            fill: #b4cae3 !important;
        }
    </style>
    <script type="text/javascript">
        function grid_EndCallback(s, e) {
            if (s.cpRefresh) {
                grid.Refresh();
                s.cpRefresh = false;
            }

            if (s.cpShowPopup) {
                s.cpShowPopup = false;
                popupUpdate.Show();
            }

            if (s.cpHideProgress) {
                s.cpHideProgress = false;
                $('#progress').hide();
            }

            $('#progress').hide();

            EndCallback(s, e);
        }

        function onToolbarItemClick(s, e) {
            if (e.item.name === 'Update') {
                e.processOnServer = true;
            }

            if (e.item.name === 'Rebuild') {
                e.processOnServer = true;
            }

            if (e.item.name === 'Update') {
                e.processOnServer = true;
            }

            if (e.item.name === 'Equalization') {
                e.processOnServer = true;
            }

            if (e.item.name === 'XLSExport' || e.item.name === 'XLSXExport') {
                e.processOnServer = true;
            }
        }

        function refreshChart() {
            if (ASPxClientEdit.ValidateGroup('ValidateFilter')) {
                cpChart.PerformCallback("Filter");
                LoadingPanel.Show();
            }
        }

        function refreshChartValue(value) {
            if (ASPxClientEdit.ValidateGroup('ValidateFilter')) {
                cpChart.PerformCallback(value);
                LoadingPanel.Show();
            }
        }

        function isSelected(checkbox, target) {
            if ($(checkbox).is(":checked")) {
                target.SetOptions({
                    commonSeriesSettings: {
                        label: { visible: true, connector: { visible: true } }
                    }
                });
            } else {
                target.SetOptions({
                    commonSeriesSettings: {
                        label: { visible: false, connector: { visible: true } }
                    }
                });
            }
        }

        function OnComboBoxInit(s, e) {
            ASPxClientUtils.AttachEventToElement(s.GetMainElement(), "click", function (event) {
                s.SetValue(null)
            });
        }

        function OnComboBoxValueChanged(s, e) {
            if (s.GetValue() === 13) { //Selected Date
                popupSelectDate.Show();
            }
            if (s.GetValue() === 17) { //Range Date
                popupRange.Show();
            }
        }

        function customizeTooltip(args) {
            console.log(args)
            return {
                html: "<div class='state-tooltip'>" +

                    "</div>"
            };
        }

    </script>

    <div id="sectionInfo">
        <h5 id="text-progress" style="display: none" class="my-2 font-weight-bold"></h5>
        <div role="progressbar" id="progress" class="mdc-linear-progress mdc-linear-progress--indeterminate mt-2" style="display: none">
            <div class="mdc-linear-progress__buffering-dots"></div>
            <div class="mdc-linear-progress__buffer"></div>
            <div class="mdc-linear-progress__bar mdc-linear-progress__primary-bar">
                <span class="mdc-linear-progress__bar-inner"></span>
            </div>
            <div class="mdc-linear-progress__bar mdc-linear-progress__secondary-bar">
                <span class="mdc-linear-progress__bar-inner"></span>
            </div>
        </div>
    </div>

    <dx:ASPxGridViewExporter ID="gridExporter" GridViewID="grid" runat="server"></dx:ASPxGridViewExporter>
    <dx:ASPxGridView runat="server" ID="grid" ClientInstanceName="grid"
        Width="100%" CssClass="w-100 mt-3" DataSourceID="VendorLogData"
        KeyFieldName="LOG_ID"
        EnableCallBacks="true"
        OnInit="grid_Init"
        OnToolbarItemClick="grid_ToolbarItemClick"
        OnHtmlRowPrepared="grid_HtmlRowPrepared">
        <ClientSideEvents EndCallback="grid_EndCallback" ToolbarItemClick="onToolbarItemClick" />
        <SettingsSearchPanel CustomEditorID="tbSearch" />
        <SettingsDataSecurity AllowDelete="false" AllowEdit="false" AllowInsert="false" />
        <SettingsContextMenu Enabled="true">
            <ColumnMenuItemVisibility ClearFilter="true" />
        </SettingsContextMenu>
        <SettingsPager PageSize="10">
            <PageSizeItemSettings Visible="true" Items="5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000" />
        </SettingsPager>
        <SettingsExport ExcelExportMode="WYSIWYG" PaperKind="A2" EnableClientSideExportAPI="true"></SettingsExport>
        <Toolbars>
            <dx:GridViewToolbar Name="Toolbar">
                <Items>
                    <dx:GridViewToolbarItem Command="Refresh" Name="Refresh" BeginGroup="true" Image-IconID="actions_refresh_16x16office2013" />
                    <dx:GridViewToolbarItem AdaptivePriority="1" Name="Export" Visible="false" Text="Download to" Image-IconID="export_export_16x16office2013" BeginGroup="true">
                        <Items>
                            <dx:GridViewToolbarItem Command="ExportToCsv" Image-IconID="mail_sendcsv_16x16office2013" />
                            <dx:GridViewToolbarItem Command="ExportToXls" Visible="false" Text="Export to XLS" Image-IconID="mail_sendxls_16x16office2013" />
                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to Excel" Visible="false" Image-IconID="export_exporttoxlsx_16x16office2013" />
                            <dx:GridViewToolbarItem Name="XLSExport" Visible="false" Text="Export to XLS" Image-IconID="export_exporttoxls_16x16office2013" />
                            <dx:GridViewToolbarItem Name="XLSXExport" Visible="true" Text="Export to Excel" Image-IconID="export_exporttoxlsx_16x16office2013" />
                        </Items>
                        <SubMenuTemplate>
                            <dx:ASPxButton ID="CSVExport" ClientInstanceName="CSVExport" Visible="false" Image-IconID="mail_sendcsv_16x16office2013" OnClick="CSVExport_Click" runat="server" Text="Download to CSV">
                                <ClientSideEvents Click="function(s,e){ s.Enabled = false }" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="ExcelExport" ClientInstanceName="ExcelExport" Visible="false" Image-IconID="mail_sendxls_16x16office2013" OnClick="ExcelExport_Click" runat="server" Text="Download to EXCEL">
                                <ClientSideEvents Click="function(s,e){ s.Enabled = false }" />
                            </dx:ASPxButton>
                        </SubMenuTemplate>
                    </dx:GridViewToolbarItem>

                    <dx:GridViewToolbarItem Text="Download to" Image-IconID="export_export_16x16office2013" BeginGroup="true">
                        <SubMenuTemplate>
                            <dx:ASPxButton ID="CSVExport" CssClass="m-1" Image-IconID="mail_sendcsv_16x16office2013" OnClick="CSVExport_Click" runat="server" Text="CSV"></dx:ASPxButton>
                            <dx:ASPxButton ID="ExcelExport" CssClass="m-1" Image-IconID="mail_sendxls_16x16office2013" OnClick="ExcelExport_Click" runat="server" Text="Excel"></dx:ASPxButton>
                        </SubMenuTemplate>
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
                            <dx:ASPxButtonEdit ID="tbSearch" runat="server" NullText="Search" Height="100%" />
                        </Template>
                    </dx:GridViewToolbarItem>
                </Items>
            </dx:GridViewToolbar>
        </Toolbars>

        <Columns>
            <dx:GridViewDataTextColumn FieldName="CARDUID" Caption="Card UID" Width="90" MaxWidth="90" Settings-AllowHeaderFilter="True" />
            <dx:GridViewDataTextColumn FieldName="NIK" Caption="NIK" Width="90" MaxWidth="90" Settings-AllowHeaderFilter="True" />
            <dx:GridViewDataTextColumn FieldName="NAMA" Caption="Nama" Width="90" MaxWidth="90" Settings-AllowHeaderFilter="True" />            
            <dx:GridViewDataTextColumn FieldName="PROV" Caption="Provinsi" Width="70" MaxWidth="90" Settings-AllowHeaderFilter="True" />
            <dx:GridViewDataTextColumn FieldName="KAB" Caption="Kabupaten" Width="70" MaxWidth="90" Settings-AllowHeaderFilter="True" />
            <dx:GridViewDataDateColumn FieldName="CREATEDATE" Caption="Create Date" Settings-AllowHeaderFilter="True" Width="70" MaxWidth="70">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
        </Columns>
    </dx:ASPxGridView>

    <dx:EntityServerModeDataSource runat="server" ID="VendorLogData" ContextTypeName="KMS.Context.KMSContext"
        TableName="CARD" OnSelecting="VendorLogData_Selecting" DefaultSorting="LOG_ID ASC" />

</asp:Content>

<asp:Content ID="Hub" ContentPlaceHolderID="Script" runat="server">
    <script>
        $(function () {
            let progress = $.connection.iISLogHub;
            var tryingToReconnect = false;
            var containerInfo = $("div#sectionInfo");
            var textstatus = $("h5#text-progress");
            var progressBar = $("div#progress")

            progress.client.getProgress = function (message) {
                containerInfo.show();
                textstatus.show();
                progressBar.show();
                textstatus.text(message);
            };

            progress.client.finished = function () {
                containerInfo.hide();
                textstatus.hide();
                grid.Refresh();
            };

            progress.client.started = function () {
                containerInfo.show();
                textstatus.show();
                progressBar.show();
            };

            progress.client.refresh = function () {
                grid.Refresh();
            };

            progress.client.success = function (data) {
                if ($('div.alert.notify-message.alert-success.border-danger.shadow-lg').length == 0) {
                    $('body').append(data);
                }
            };

            $.connection.hub.start(function () {
                $.get("https://www.cloudflare.com/cdn-cgi/trace", function (data) {
                    var d = data.split("\n");
                    var o = new Object();
                    d.forEach(function (itm, idx) {
                        var item = itm.split("=");
                        o[item[0]] = item[1];
                    });

                    progress.server.join(o.ip, o.uag)
                        .done(function (data) {
                            textstatus.text(data.message);
                        });
                })

            });

            $.connection.hub.disconnected(function () {
                setTimeout(function () {
                    $.connection.hub.start();
                    Pace.restart();
                }, 5000); // Re-start connection after 5 seconds
            });

            $.connection.hub.reconnecting(function () {
                tryingToReconnect = true;
            });

            $.connection.hub.reconnected(function () {
                tryingToReconnect = false;
                Pace.restart();
            });

            $.connection.hub.disconnected(function () {
                if (tryingToReconnect) {
                    console.log("Trying reconnecting...")
                }
            });

        })
    </script>
</asp:Content>
