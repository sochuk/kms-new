<%@ Page Language="C#" MasterPageFile="~/CPanel.Master" AutoEventWireup="true" CodeBehind="CompareNIK.aspx.cs" Inherits="CMS.CompareNIK" %>

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

            if (s.cpShowDetail) {
                gridDetail.Refresh();
                Popup.Show();
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


    <dx:ASPxPageControl ID="Tabs" ClientInstanceName="Tabs" Width="100%" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True">
        <TabPages>
            <dx:TabPage Text="NIK VS Card UID">
                <ContentCollection>
                    <dx:ContentControl>
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
                            Width="100%" CssClass="w-100 mt-3" DataSourceID="Datasource"
                            KeyFieldName="ID"
                            EnableCallBacks="true"
                            OnInit="grid_Init"
                            OnCustomButtonCallback="grid_CustomButtonCallback"
                            OnToolbarItemClick="grid_ToolbarItemClick">
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

                                        <dx:GridViewToolbarItem Text="Download to" Image-IconID="export_export_16x16office2013" BeginGroup="true">
                                            <SubMenuTemplate>
                                                <dx:ASPxButton ID="CSVExport" CssClass="m-1" Image-IconID="mail_sendcsv_16x16office2013" OnClick="CSVNIKExport_Click" runat="server" Text="CSV"></dx:ASPxButton>
                                                <dx:ASPxButton ID="ExcelExport" CssClass="m-1" Image-IconID="mail_sendxls_16x16office2013" OnClick="ExcelNIKExport_Click" runat="server" Text="Excel"></dx:ASPxButton>
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
                                        <dx:GridViewToolbarItem Name="Rebuild" Text="Rebuild Data" BeginGroup="true" Image-IconID="reports_groupfieldcollection_16x16office2013" />
                                    </Items>
                                </dx:GridViewToolbar>
                            </Toolbars>

                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="NIK" Caption="NIK" Settings-AllowHeaderFilter="True" />
                                <dx:GridViewDataTextColumn FieldName="TOTAL" Caption="Card UID Count" Width="200" MaxWidth="200" Settings-AllowHeaderFilter="True" />
                                <dx:GridViewCommandColumn HeaderStyle-HorizontalAlign="Center"
                                    AdaptivePriority="2"
                                    VisibleIndex="1000"
                                    ButtonRenderMode="Button"
                                    ShowEditButton="false"
                                    ShowDeleteButton="false"
                                    ShowApplyFilterButton="false"
                                    ShowClearFilterButton="false"
                                    Width="150"
                                    MaxWidth="200"
                                    Caption="Action">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton Text="Detail"></dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                            </Columns>

                        </dx:ASPxGridView>

                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>

    <dx:ASPxPopupControl ID="Popup" runat="server" ClientInstanceName="Popup"
        Width="750" Height="400" ClientSideEvents-PopUp="function(s,e){s.ShowAtPos(s.Left, 100)}"
        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="BottomSides"
        CloseAction="CloseButton" CloseOnEscape="true" AllowDragging="true" PopupAnimationType="None"
        EnableViewState="false" AutoUpdatePosition="true" ShowFooter="true" FooterImage-AlternateText="FS"
        HeaderText="Reference Detail">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxGridView runat="server" ID="gridDetail" 
                    ClientInstanceName="gridDetail" 
                    AutoGenerateColumns="false" CssClass="w-100" 
                    KeyFieldName="LOG_ID">
                    <SettingsPager EnableAdaptivity="true" />
                    <SettingsBehavior AllowEllipsisInText="true" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="nik" Caption="NIK" Settings-AllowHeaderFilter="False" />
                        <dx:GridViewDataTextColumn FieldName="carduid" Caption="Card UID" Settings-AllowHeaderFilter="False" />
                        <dx:GridViewDataTextColumn FieldName="nama" Caption="Nama" Settings-AllowHeaderFilter="False" />
                        <dx:GridViewDataTextColumn FieldName="prov" Caption="Provinsi" Settings-AllowHeaderFilter="False" />
                        <dx:GridViewDataTextColumn FieldName="kab" Caption="Kabupaten" Settings-AllowHeaderFilter="False" />
                        <dx:GridViewDataTextColumn FieldName="createdate" Caption="Create Date" Settings-AllowHeaderFilter="False" />
                    </Columns>
                </dx:ASPxGridView>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <FooterTemplate>
            <table class="w-100">
                <tr>
                    <td class="d-flex justify-content-end p-2">
                        <dx:ASPxButton ID="CancelButton" runat="server" Text="Close" AutoPostBack="False"
                            ClientSideEvents-Click="function(s, e) { Popup.Hide(); }" />
                    </td>
                </tr>
            </table>
        </FooterTemplate>
    </dx:ASPxPopupControl>

    <dx:EntityServerModeDataSource runat="server" ID="Datasource" ContextTypeName="KMS.Context.KMSContext" TableName="CARD_NIK" OnSelecting="NIK_Selecting" />
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
