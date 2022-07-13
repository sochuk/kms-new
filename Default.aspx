<%@ Page Language="C#" MasterPageFile="~/CPanel.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KMS.Default" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v19.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <%: Scripts.Render("~/Content/js/dev/devextreme") %>
    <%: Styles.Render("~/Content/css/dev/devextreme") %>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">

    <style>
        .chartTitle {
            font-size: 20px !important;
            color: rgb(0, 64, 133) !important
        }

        rect[fill=gray] {
            fill: #b4cae3 !important;
        }
    </style>
    <script type="text/javascript">

        var datef = null;
        var vendorValues = null;
        var contractValues = null;
        var statusValues = null;
        function OnDateFilterChange(s, e) {
            datef = new Date(s.GetValue());
        }

        function refreshChart() {
            var gridVendor = lookup_vendorf.GetGridView();
            var gridContract = lookup_contractf.GetGridView();
            var gridStatus = lookup_statusf.GetGridView();
            gridVendor.GetSelectedFieldValues('vendor_id', OnGetSelectedVendorFieldValues);
            gridContract.GetSelectedFieldValues('contract_id', OnGetSelectedContractFieldValues);
            gridStatus.GetSelectedFieldValues('status_id', OnGetSelectedStatusFieldValues);

            console.log(vendorValues);
            console.log(contractValues);
            console.log(statusValues);
            initAll(datef, vendorValues, contractValues, statusValues);
        }

        function OnGetSelectedContractFieldValues(selectedValues) {
            vendorValues =  selectedValues;
        }

        function OnGetSelectedVendorFieldValues(selectedValues) {
            contractValues = selectedValues;
        }

        function OnGetSelectedStatusFieldValues(selectedValues) {
            statusValues = selectedValues;
        }
    </script>
    <dx:ASPxFormLayout class="mdc-layout-grid mb-4" ID="flDateRangePicker" runat="server" RequiredMarkDisplayMode="None" CssClass="indent mdc-layout-grid mb-4" UseDefaultPaddings="false">
        <SettingsItemCaptions Location="Top"></SettingsItemCaptions>
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
        <Items>
            <dx:LayoutGroup ColCount="3" GroupBoxDecoration="none" UseDefaultPaddings="false">
                <Items>
                    <dx:LayoutItem Caption="Filter Tanggal" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxDateEdit ID="deFilter" Height="25" DisplayFormatString="dd MMMM yyyy" EditFormatString="dd MMMM yyyy" ClientInstanceName="deFilter" runat="server">
                                    <ClientSideEvents ValueChanged="OnDateFilterChange" />
                                    <CalendarProperties>
                                        <FastNavProperties DisplayMode="Popup" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Vendor" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup Height="25" ID="lookup_vendorf" ClientInstanceName="lookup_vendorf" runat="server"
                                    KeyFieldName="vendor_id" TextFormatString="{0}" AutoGenerateColumns="False"
                                    SelectionMode="Multiple" MultiTextSeparator=";" OnInit="lookup_vendorf_Init">
                                    <ClientSideEvents />
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Width="30" SelectAllCheckboxMode="Page" ShowCancelButton="true" ShowApplyFilterButton="true" />
                                        <dx:GridViewDataTextColumn FieldName="vendor_name" Caption="Vendor" />
                                        <dx:GridViewDataTextColumn FieldName="vendor_desc" Caption="Description" />
                                    </Columns>
                                    
                                    <GridViewProperties>
                                        <Settings ShowFilterRow="True" ShowStatusBar="Visible" />
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="false" />
                                        <SettingsPager EnableAdaptivity="true"></SettingsPager>
                                        <Templates>
                                            <StatusBar>
                                                <div class="row">
                                                    <div class="col">
                                                        <dx:ASPxButton runat="server" AutoPostBack="false" Text="Clear" Image-IconID="reports_none_16x16office2013"
                                                            ClientSideEvents-Click="function(s,e){ 
                                                                                                lookup_vendorf.SetValue(null);
                                                                                            }" />
                                                        <dx:ASPxButton runat="server" AutoPostBack="false" Text="Refresh" Image-IconID="actions_refresh_16x16office2013"
                                                            ClientSideEvents-Click="function(s,e){ 
                                                                                                lookup_vendorf.GetGridView().Refresh(); 
                                                                                            }" />
                                                    </div>
                                                    <div class="col-auto float-right">
                                                        <dx:ASPxButton ID="Close" runat="server" AutoPostBack="false" Text="Close"
                                                            ClientSideEvents-Click="function(s,e){ 
                                                                                                lookup_vendorf.ConfirmCurrentSelection();
                                                                                                lookup_vendorf.HideDropDown();
                                                                                                lookup_vendorf.Focus(); 
                                                                                            }" />
                                                    </div>
                                                </div>
                                            </StatusBar>
                                        </Templates>
                                    </GridViewProperties>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Contract" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup Height="25" ID="lookup_contractf" ClientInstanceName="lookup_contractf" runat="server"
                                    KeyFieldName="contract_id" TextFormatString="{0}" AutoGenerateColumns="False"
                                    SelectionMode="Multiple" MultiTextSeparator=";" OnInit="lookup_contractf_Init">

                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Width="30" SelectAllCheckboxMode="Page" ShowCancelButton="true" ShowApplyFilterButton="true" />
                                        <dx:GridViewDataTextColumn FieldName="contract_name" Caption="Contract" />
                                        <dx:GridViewDataTextColumn FieldName="contract_desc" Caption="Description" />
                                    </Columns>
                                    <GridViewProperties>
                                        <Settings ShowFilterRow="True" ShowStatusBar="Visible" />
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="false" />
                                        <SettingsPager EnableAdaptivity="true"></SettingsPager>
                                        <Templates>
                                            <StatusBar>
                                                <div class="row">
                                                    <div class="col">
                                                        <dx:ASPxButton runat="server" AutoPostBack="false" Text="Clear" Image-IconID="reports_none_16x16office2013"
                                                            ClientSideEvents-Click="function(s,e){ 
                                                                                                lookup_contractf.SetValue(null);
                                                                                            }" />
                                                        <dx:ASPxButton runat="server" AutoPostBack="false" Text="Refresh" Image-IconID="actions_refresh_16x16office2013"
                                                            ClientSideEvents-Click="function(s,e){ 
                                                                                                lookup_contractf.GetGridView().Refresh(); 
                                                                                            }" />
                                                    </div>
                                                    <div class="col-auto float-right">
                                                        <dx:ASPxButton ID="Close" runat="server" AutoPostBack="false" Text="Close"
                                                            ClientSideEvents-Click="function(s,e){ 
                                                                                                lookup_contractf.ConfirmCurrentSelection();
                                                                                                lookup_contractf.HideDropDown();
                                                                                                lookup_contractf.Focus(); 
                                                                                            }" />
                                                    </div>
                                                </div>
                                            </StatusBar>
                                        </Templates>
                                    </GridViewProperties>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Contract Status" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup Height="25" ID="lookup_statusf" ClientInstanceName="lookup_statusf" runat="server"
                                    KeyFieldName="status_id" TextFormatString="{0}" AutoGenerateColumns="False"
                                    OnInit="lookup_statusf_Init">

                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Width="30" SelectAllCheckboxMode="Page" ShowCancelButton="true" ShowApplyFilterButton="true" />
                                        <dx:GridViewDataTextColumn FieldName="status_name" Caption="Status" />
                                    </Columns>
                                    <GridViewProperties>
                                        <Settings ShowFilterRow="True" ShowStatusBar="Visible" />
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="false" />
                                        <SettingsPager EnableAdaptivity="true"></SettingsPager>
                                        <Templates>
                                            <StatusBar>
                                                <div class="row">
                                                    <div class="col">
                                                        <dx:ASPxButton runat="server" AutoPostBack="false" Text="Clear" Image-IconID="reports_none_16x16office2013"
                                                            ClientSideEvents-Click="function(s,e){ 
                                                                                                lookup_statusf.SetValue(null);
                                                                                            }" />
                                                        <dx:ASPxButton runat="server" AutoPostBack="false" Text="Refresh" Image-IconID="actions_refresh_16x16office2013"
                                                            ClientSideEvents-Click="function(s,e){ 
                                                                                                lookup_statusf.GetGridView().Refresh(); 
                                                                                            }" />
                                                    </div>
                                                    <div class="col-auto float-right">
                                                        <dx:ASPxButton ID="Close" runat="server" AutoPostBack="false" Text="Close"
                                                            ClientSideEvents-Click="function(s,e){ 
                                                                                                lookup_statusf.ConfirmCurrentSelection();
                                                                                                lookup_statusf.HideDropDown();
                                                                                                lookup_statusf.Focus(); 
                                                                                            }" />
                                                    </div>
                                                </div>
                                            </StatusBar>
                                        </Templates>
                                    </GridViewProperties>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%" Paddings-PaddingTop="25px">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnFilter" Text="Filter" AutoPostBack="false" ValidationGroup="ValidateFilter">
                                    <ClientSideEvents Click="refreshChart" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    <div class="mdc-layout-grid mb-4">
        <div class="mdc-layout-grid__inner">
            <%  
                string list = "";
                foreach (DataRow row in vendor.Rows)
                {
                    string vendorName = row["vendor_name"].ToString().ToUpper();
                    string vendorCountId = "count" + vendorName.Replace(" ", string.Empty);
                    string vendorProgressId = "progress" + vendorName.Replace(" ", string.Empty);
                    string vendorTextProgressId = "textProgress" + vendorName.Replace(" ", string.Empty);
                    string vendorColor = row["color"].ToString();
                    var percentage = (((row["current_hit"]).ToInteger() * 0.1f) / ((row["quota"]).ToInteger() * 0.1f));

                    list += "<div class=\"mdc-layout-grid__cell stretch-card mdc-layout-grid__cell--span-3-desktop mdc-layout-grid__cell--span-4-tablet\">";
                    list += "<div class=\"mdc-card info-card info-card\">";
                    list += "<div class=\"card-inner\">";
                    list += "<h5 class=\"card-title\">" + vendorName + "</h5>";
                    list += "<h4 class=\"font-weight-bold text-primary pb-2 mb-1 border-bottom\" id=\"" + vendorCountId + "\">" + string.Format("{0:N0}", (row["current_hit"]).ToInteger()) + "</h4>";
                    list += "<div class=\"card-icon-wrapper\" style=\"background-color:" + vendorColor + ";box-shadow:0 0 10px 5px #ffffff\"><i class=\"material-icons\">trending_up</i></div>";
                    list += "</div>";
                    list += "<p class=\"tx-12 text-muted mt-2\" id=\"" + vendorTextProgressId + "\">" + (Math.Round(percentage, 2) * 100) + "% quota reached of " + string.Format("{0:N0}", (row["quota"]).ToInteger()) + "</p>";

                    list += "<div role=\"progressbar\" class=\"mdc-linear-progress mdc-linear-progress\" id=\"" + vendorProgressId + "\" aria-valuemin=\"0\" aria-valuemax=\"1\" aria-valuenow=\"" + percentage.ToString().Replace(",", ".") + "\">";
                    list += "<div class=\"mdc-linear-progress__buffering-dots\"></div>";
                    list += "<div class=\"mdc-linear-progress__buffer\"></div>";
                    list += "<div class=\"mdc-linear-progress__bar mdc-linear-progress__primary-bar\"><span class=\"mdc-linear-progress__bar-inner\" style=\"background-color:" + vendorColor + ";\"></span></div>";
                    list += "<div class=\"mdc-linear-progress__bar mdc-linear-progress__secondary-bar\"><span class=\"mdc-linear-progress__bar-inner\"></span></div>";
                    list += "</div>";

                    list += "</div>";
                    list += "</div>";
                }

                Response.Write(list);
            %>

            <asp:Panel runat="server" ID="pnAverage">
                <div id="pnAverage" class="mdc-layout-grid__cell stretch-card mdc-layout-grid__cell--span-3-desktop mdc-layout-grid__cell--span-4-tablet d-none">
                    <div class="mdc-card info-card info-card--dark">
                        <div class="card-inner">
                            <h5 class="card-title">AVERAGE</h5>
                            <h4 class="font-weight-bold text-primary pb-2 mb-1 border-bottom" id="countAVERAGE">0</h4>
                            <div class="card-icon-wrapper">
                                <i class="material-icons">trending_up</i>
                            </div>
                        </div>
                        <p class="tx-12 text-muted mt-2" id="textProgressAVERAGE">-</p>

                        <div role="progressbar" class="mdc-linear-progress" id="progressAVERAGE" aria-valuemin="0" aria-valuemax="1" aria-valuenow="0.0">
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
                </div>
            </asp:Panel>
        </div>
    </div>

    <div class="mdc-layout-grid">
        <div class="mdc-layout-grid__inner">
            <div class="mdc-layout-grid__cell stretch-card mdc-layout-grid__cell--span-12 mdc-layout-grid__cell--span-12-tablet w-100">
                <div class="mdc-card m-0">
                    <div id="chkHourlyShowLabel"></div>
                    <div id="chartHourly" class="w-100"></div>
                    <div id="chartHourlyLoadPanel"></div>

                    <div class="mdc-layout-grid__inner grid-gap-0">
                        <div class="mdc-layout-grid__cell mdc-layout-grid__cell--span-12">
                            <div class="d-block d-md-flex mt-4">
                                <%  
                                    string hourlyDetail = "";
                                    foreach (DataRow row in vendor.Rows)
                                    {
                                        string vendorName = row["vendor_name"].ToString().ToUpper();
                                        string vendorTodayId = "today" + vendorName.Replace(" ", string.Empty);
                                        string vendorYesterdayId = "yesterday" + vendorName.Replace(" ", string.Empty);
                                        string vendorColor = row["color"].ToString();

                                        hourlyDetail += "<div class=\"mdc-card border box-shadow-none rounded-0 statitics-card\">";
                                        hourlyDetail += "<div class=\"d-flex justify-content-between statitics-card-header\">";
                                        hourlyDetail += "<h6 class=\"font-weight-bold\">" + vendorName + "</h6>";
                                        hourlyDetail += "</div>";
                                        hourlyDetail += "<div class=\"d-flex justify-content-between statitics-card-content\">";
                                        hourlyDetail += "<div>";
                                        hourlyDetail += "<h3 class=\"font-weight-bold text-primary\" id=\"" + vendorTodayId + "\">0</h3>";
                                        hourlyDetail += "<h6 class=\"font-weight-light\">TODAY COUNTER<span id=\"" + vendorYesterdayId + "\"></span></h6>";
                                        hourlyDetail += "</div>";
                                        hourlyDetail += "<i class=\"material-icons\" style=\"color:" + vendorColor + ";\">account_balance_wallet</i>";
                                        hourlyDetail += "</div>";


                                        hourlyDetail += "";
                                        hourlyDetail += "";
                                        hourlyDetail += "</div>";
                                    }

                                    Response.Write(hourlyDetail);
                                %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="mdc-layout-grid__cell stretch-card mdc-layout-grid__cell--span-6 mdc-layout-grid__cell--span-12-tablet w-100">
                <div class="mdc-card">
                    <div id="chkDailyShowLabel"></div>
                    <div id="chartDaily" class="w-100"></div>
                    <div id="chartDailyLoadPanel"></div>
                </div>
            </div>

            <div class="mdc-layout-grid__cell stretch-card mdc-layout-grid__cell--span-6 mdc-layout-grid__cell--span-12-tablet w-100">
                <div class="mdc-card">
                    <div id="chkWeeklyShowLabel"></div>
                    <div id="chartWeekly" class="w-100"></div>
                    <div id="chartWeeklyLoadPanel"></div>
                </div>
            </div>
            <div class="mdc-layout-grid__cell stretch-card mdc-layout-grid__cell--span-6 mdc-layout-grid__cell--span-12-tablet w-100">
                <div class="mdc-card">
                    <div id="chkMonthlyShowLabel"></div>
                    <div id="chartMonthly" class="w-100"></div>
                    <div id="chartMonthlyLoadPanel"></div>
                </div>
            </div>
            <div class="mdc-layout-grid__cell stretch-card mdc-layout-grid__cell--span-6 mdc-layout-grid__cell--span-12-tablet w-100">
                <div class="mdc-card">
                    <div id="chkSemesterShowLabel"></div>
                    <div id="chartSemester" class="w-100"></div>
                    <div id="chartSemesterLoadPanel"></div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Scripts" ContentPlaceHolderID="Script" runat="server">
    <script>

        $("#pnAverage").unwrap();
        let hourly, daily, monthly, weekly, semester;
        let chartHourly, chartDaily, chartMonthly, chartWeekly, chartSemester;

        let monthLabel = "";
        let dayLabel = "";
        let yearLabel = "";


        $(function () {
            getMonth(null);
            initAll(null);
        });

        function initAll(date, vendorIds, contractIds, statusIds) {
            var log = $.connection.vendorLogHub;
            //hourly = new DevExpress.data.DataSource({
            //    load: function () {
            //        return log.server.getHourlyData(date);
            //    },
            //    key: "fulldate",
            //});

            //daily = new DevExpress.data.DataSource({
            //    load: function () {
            //        return log.server.getDailyData(date, vendorIds, contractIds, statusIds);
            //    },
            //    key: "fulldate",
            //});

            weekly = new DevExpress.data.DataSource({
                load: function () {
                    return log.server.getWeeklyData(date);
                },
                key: "week",
            });

            monthly = new DevExpress.data.DataSource({
                load: function () {
                    return log.server.getMonthlyData(date);
                },
                key: "fulldate",
            });

            semester = new DevExpress.data.DataSource({
                load: function () {
                    return log.server.getSemesterData(date);
                },
                key: "half_year",
            });

            log.client.refreshDashboard = function (data) {
                window.location.reload();
            };

            log.client.getContractInfo = function (data) {
                var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
                $("#textTarget").text(formatNumber(data[0].quota));
                $("#textValid").html("<i class='fa fa-calendar'></i> <b>START :</b> " + Globalize.dateFormatter({ skeleton: "yMMMEd" })(new Date(data[0].period_start)));
                $("#textValidUntil").html("<i class='fa fa-calendar-check'></i> <b>VALID UNTIL :</b> " + Globalize.dateFormatter({ skeleton: "yMMMEd" })(new Date(data[0].period_end)));
            };

            //log.client.updateHourly = function (data) {
            //    hourly.reload();
            //};

            //log.client.updateDailySummary = function (data) {
            //    data.forEach(item => {
            //        var vendorName = item.vendor_name.toUpperCase().replace(" ", "");
            //        $("#today" + vendorName).text(formatNumber(item.total_today));
            //    });
            //};

            //log.client.updateDaily = function (data) {
            //    daily.reload();
            //};

            log.client.updateWeekly = function (data) {
                weekly.reload();
            };

            log.client.updateMonthly = function (data) {
                monthly.reload();
            };

            log.client.updateSemester = function (data) {
                semester.reload();
            };

            log.client.updateSummary = function (data) {
                var avr = 0;
                var avrQuota = 0;
                data.forEach(item => {
                    var vendorName = item.vendor_name.toUpperCase().replace(" ", "");
                    $("#count" + vendorName).text(formatNumber(item.count));
                    $("#textProgress" + vendorName).text(parseFloat(item.reached * 100).toFixed(2) + "% quota reached of " + formatNumber(item.quota));
                    $("#progress" + vendorName).attr("aria-valuenow", parseFloat((item.reached)).toFixed(2));
                    avr += item.count;
                    avrQuota += item.quota
                    updateProgressbar();
                    //console.log(parseFloat(item.reached * 100).toFixed(2))
                });
                $("#countAVERAGE").text(parseFloat(avr / data.length).toFixed(1));
                //console.log(data);
                //$("#textProgressAVERAGE").text(parseFloat(avrQuota / data.length).toFixed(2) + "% quota reached of " + parseFloat(avrQuota/data.length).toFixed(2));
                //$("#progressAVERAGE").attr("aria-valuenow", item.reached);
                $('a[href*="www.devexpress.com"], body > div:first-child[style*="padding"]').attr("style", "none !important");
                //console.log(data);
            };

            $.connection.hub.start(function () {
                $.get("https://www.cloudflare.com/cdn-cgi/trace", function (data) {
                    var d = data.split("\n");
                    var o = new Object();
                    d.forEach(function (itm, idx) {
                        var item = itm.split("=");
                        o[item[0]] = item[1];
                    });

                    log.server.join(o.ip, o.uag)
                        .done(function () {

                        });

                })

            }).done(function () {
                setTimeout(function () {
                    generateChartHourly();
                    generateChartDaily();
                    generateChartWeekly();
                    generateChartMonthly();
                    generateChartSemester();

                    $("#chkHourlyShowLabel").dxCheckBox({
                        text: "Show chart label",
                        value: false,
                        onValueChanged: function (e) {
                            chartHourly.option("commonSeriesSettings.label.visible", e.value)
                        },
                    });

                    $("#chkDailyShowLabel").dxCheckBox({
                        text: "Show chart label",
                        value: false,
                        onValueChanged: function (e) {
                            chartDaily.option("commonSeriesSettings.label.visible", e.value)
                        },
                        elementAttr: {
                            class: "mb-3"
                        }
                    });

                    $("#chkWeeklyShowLabel").dxCheckBox({
                        text: "Show chart label",
                        value: false,
                        onValueChanged: function (e) {
                            chartWeekly.option("commonSeriesSettings.label.visible", e.value)
                        },
                    });

                    $("#chkMonthlyShowLabel").dxCheckBox({
                        text: "Show chart label",
                        value: false,
                        onValueChanged: function (e) {
                            chartMonthly.option("commonSeriesSettings.label.visible", e.value)
                        },
                        elementAttr: {
                            class: "mb-3"
                        }
                    });

                    $("#chkSemesterShowLabel").dxCheckBox({
                        text: "Show chart label",
                        value: false,
                        onValueChanged: function (e) {
                            chartSemester.option("commonSeriesSettings.label.visible", e.value)
                        },
                        elementAttr: {
                            class: "mb-3"
                        }
                    });

                    log.server.getSummaryData();

                    //chartHourly.showLoadingIndicator();
                    //chartDaily.showLoadingIndicator();
                    //chartMonthly.showLoadingIndicator();

                    repeatEvery(log.server.reloadCounter, (20 * 1000));
                    repeatEvery(log.server.reloadAll(date), (3600 * 1000));
                }, 1000);

                updateProgressbar();
            });

        }

        document.querySelector('.sidebar-toggler').addEventListener('click', function () {
            setTimeout(function () {
                chartHourly.render();
                chartDaily.render();
                chartWeekly.render();
                chartMonthly.render();
                chartSemester.render();
            }, 700);

        });

        function generateChartHourly() {
            chartHourly = $("#chartHourly").dxChart({
                dataSource: hourly,
                loadingIndicator: {
                    enabled: false,
                },
                onInitialized: function () {
                    $("#chartHourlyLoadPanel").dxLoadPanel({
                        visible: true,
                        position: { of: "#chartHourly" }
                    });
                },
                onDone: function () {
                    $("#chartHourlyLoadPanel").dxLoadPanel('instance').option("visible", false);
                },
                commonSeriesSettings: {
                    label: {
                        visible: false,
                        font: {
                            size: 8
                        },
                        connector: {
                            visible: true
                        },
                        customizeText: function (pointInfo) {
                            //var date = new Date(pointInfo.argument);                            
                            //return Globalize.dateFormatter({ time: "short" })(date) + ' : ' + pointInfo.value;
                            return pointInfo.value;
                        }
                    },
                    ignoreEmptyPoints: true,
                    barPadding: 0.05,
                    type: "bar"
                },
                //customizeLabel: function () {
                //return { backgroundColor: "transparent"}
                //},
                title: {
                    text: "" == dayLabel ? "TODAY" : dayLabel.toUpperCase() + " HOURLY LOG ACTIVITY",
                    font: {
                        color: "#063a69",
                        size: 20,
                        weight: 900,
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                crosshair: {
                    enabled: true,
                    horizontalLine: { visible: true }
                },
                zoomAndPan: {
                    argumentAxis: "both"
                },
                scrollBar: {
                    visible: true,
                    position: 'bottom',
                    offset: 10,
                    width: 5
                },
                legend: {
                    visible: true,
                    position: 'outside',
                    horizontalAlignment: 'center',
                    verticalAlignment: 'top',
                    itemTextPosition: 'top',
                    font: {
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                argumentAxis: {
                    argumentType: "datetime",
                    minVisualRangeLength: { minutes: 20 },
                    visualRange: {
                        length: { minutes: 30 },
                    },
                    font: {
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                valueAxis: {
                    placeholderSize: 50
                },
                tooltip: {
                    enabled: true,
                    location: "edge",
                    customizeTooltip: function (arg) {
                        var date = Globalize.dateFormatter({ time: "short" })(new Date(arg.argument));
                        return {
                            text: '<div><p><b>' + arg.seriesName + "</b></p> (" + date + ') : ' + formatNumber(parseInt(arg.valueText)) + '</div>'
                        };
                    }
                }, export: {
                    enabled: true,
                    printingEnabled: false
                },
                series: [
                <%= seriesHourly %>
                ]
            }).dxChart("instance");
        }

        function generateChartDaily() {
            chartDaily = $("#chartDaily").dxChart({
                dataSource: daily,
                loadingIndicator: {
                    enabled: false
                },
                onInitialized: function () {
                    $("#chartDailyLoadPanel").dxLoadPanel({
                        visible: true,
                        position: { of: "#chartDaily" }
                    });
                },
                onDone: function () {
                    $("#chartDailyLoadPanel").dxLoadPanel('instance').option("visible", false);
                },
                commonSeriesSettings: {
                    label: {
                        visible: false,
                        font: {
                            size: 8
                        },
                        connector: {
                            visible: true
                        },
                        customizeText: function (pointInfo) {
                            //var date = new Date(pointInfo.argument);                            
                            //return Globalize.dateFormatter({ time: "short" })(date) + ' : ' + pointInfo.value;
                            return pointInfo.value;
                        }
                    },
                    ignoreEmptyPoints: true,
                    barPadding: 0.05,
                    type: "bar"
                },
                customizeLabel: function () {
                    //return { backgroundColor: "transparent"}
                },
                title: {
                    text: monthLabel.toUpperCase() + " DAILY LOG ACTIVITY",
                    font: {
                        color: "#063a69",
                        size: 20,
                        weight: 900,
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                crosshair: {
                    enabled: true,
                    horizontalLine: { visible: false }
                },
                zoomAndPan: {
                    argumentAxis: "both"
                },
                scrollBar: {
                    visible: true,
                    position: 'bottom',
                    offset: 10,
                    width: 5
                },
                legend: {
                    visible: true,
                    position: 'outside',
                    horizontalAlignment: 'center',
                    verticalAlignment: 'bottom',
                    itemTextPosition: 'top',
                    font: {
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                tooltip: {
                    enabled: true,
                    location: "edge",
                    customizeTooltip: function (arg) {
                        return {
                            text: arg.seriesName + " : " + formatNumber(parseInt(arg.valueText))
                        };
                    }
                },
                argumentAxis: {
                    argumentType: "datetime",
                    minVisualRangeLength: { days: 15 },
                    visualRange: {
                        length: 'month',
                    },
                    font: {
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                }, export: {
                    enabled: true,
                    printingEnabled: false
                },
                series: [
                <%= seriesDaily %>
                ]
            }).dxChart("instance");
        }

        function generateChartWeekly() {
            chartHourly = $("#chartWeekly").dxChart({
                dataSource: weekly,
                loadingIndicator: {
                    enabled: false,
                },
                onInitialized: function () {
                    $("#chartWeeklyLoadPanel").dxLoadPanel({
                        visible: true,
                        position: { of: "#chartWeekly" }
                    });
                },
                onDone: function () {
                    $("#chartWeeklyLoadPanel").dxLoadPanel('instance').option("visible", false);
                },
                commonSeriesSettings: {
                    label: {
                        visible: false,
                        font: {
                            size: 8
                        },
                        connector: {
                            visible: true
                        },
                        customizeText: function (pointInfo) {
                            //var date = new Date(pointInfo.argument);                            
                            //return Globalize.dateFormatter({ time: "short" })(date) + ' : ' + pointInfo.value;
                            return pointInfo.value;
                        }
                    },
                    ignoreEmptyPoints: true,
                    barPadding: 0.05,
                    type: "bar"
                },
                //customizeLabel: function () {
                //return { backgroundColor: "transparent"}
                //},
                title: {
                    text: monthLabel.toUpperCase() + " WEEK LOG ACTIVITY",
                    font: {
                        color: "#063a69",
                        size: 20,
                        weight: 900,
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                crosshair: {
                    enabled: true,
                    horizontalLine: { visible: true }
                },
                zoomAndPan: {
                    argumentAxis: "both"
                },
                scrollBar: {
                    visible: true,
                    position: 'bottom',
                    offset: 10,
                    width: 5
                },
                legend: {
                    visible: true,
                    position: 'outside',
                    horizontalAlignment: 'center',
                    verticalAlignment: 'top',
                    itemTextPosition: 'top',
                    font: {
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                argumentAxis: {
                    argumentType: "week",
                    //minVisualRangeLength: { minutes: 20 },
                    //visualRange: {
                    //    length: { minutes: 30 },
                    //},
                    font: {
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                valueAxis: {
                    placeholderSize: 50
                },
                tooltip: {
                    enabled: true,
                    location: "edge",
                    customizeTooltip: function (arg) {
                        var date = Globalize.dateFormatter({ time: "short" })(new Date(arg.argument));
                        return {
                            text: '<div><p><b>' + arg.seriesName + "</b></p> (" + date + ') : ' + formatNumber(parseInt(arg.valueText)) + '</div>'
                        };
                    }
                }, export: {
                    enabled: true,
                    printingEnabled: false
                },
                series: [
                <%= seriesWeekly %>
                ]
            }).dxChart("instance");
        }

        function generateChartMonthly() {
            chartMonthly = $("#chartMonthly").dxChart({
                dataSource: monthly,
                loadingIndicator: {
                    enabled: false
                },
                onInitialized: function () {
                    $("#chartMonthlyLoadPanel").dxLoadPanel({
                        visible: true,
                        position: { of: "#chartMonthly" }
                    });
                },
                onDone: function () {
                    $("#chartMonthlyLoadPanel").dxLoadPanel('instance').option("visible", false);
                },
                commonSeriesSettings: {
                    label: {
                        visible: false,
                        font: {
                            size: 8
                        },
                        connector: {
                            visible: true
                        },
                        customizeText: function (pointInfo) {
                            //var date = new Date(pointInfo.argument);                            
                            //return Globalize.dateFormatter({ time: "short" })(date) + ' : ' + pointInfo.value;
                            return pointInfo.value;
                        }
                    },
                    ignoreEmptyPoints: true,
                    barPadding: 0.05,
                    type: "bar"
                },
                customizeLabel: function () {
                    //return { backgroundColor: "transparent"}
                },
                title: {
                    text: yearLabel + " MONTHLY LOG ACTIVITY",
                    font: {
                        color: "#063a69",
                        size: 20,
                        weight: 900,
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                crosshair: {
                    enabled: true,
                    horizontalLine: { visible: false }
                },
                zoomAndPan: {
                    argumentAxis: "both"
                },
                scrollBar: {
                    visible: true,
                    position: 'bottom',
                    offset: 10,
                    width: 5
                },
                legend: {
                    visible: true,
                    position: 'outside',
                    horizontalAlignment: 'center',
                    verticalAlignment: 'bottom',
                    itemTextPosition: 'top',
                    font: {
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                tooltip: {
                    enabled: true,
                    location: "edge",
                    customizeTooltip: function (arg) {
                        return {
                            text: arg.seriesName + " : " + formatNumber(parseInt(arg.valueText))
                        };
                    }
                },
                export: {
                    enabled: true,
                    printingEnabled: false
                },
                series: [
                <%= seriesMonthly %>
                ],
            }).dxChart("instance");
        }

        function generateChartSemester() {
            chartHourly = $("#chartSemester").dxChart({
                dataSource: semester,
                loadingIndicator: {
                    enabled: false,
                },
                onInitialized: function () {
                    $("#chartSemesterLoadPanel").dxLoadPanel({
                        visible: true,
                        position: { of: "#chartSemester" }
                    });
                },
                onDone: function () {
                    $("#chartSemesterLoadPanel").dxLoadPanel('instance').option("visible", false);
                },
                commonSeriesSettings: {
                    label: {
                        visible: false,
                        font: {
                            size: 8
                        },
                        connector: {
                            visible: true
                        },
                        customizeText: function (pointInfo) {
                            //var date = new Date(pointInfo.argument);                            
                            //return Globalize.dateFormatter({ time: "short" })(date) + ' : ' + pointInfo.value;
                            return pointInfo.value;
                        }
                    },
                    ignoreEmptyPoints: true,
                    barPadding: 0.05,
                    type: "bar"
                },
                //customizeLabel: function () {
                //return { backgroundColor: "transparent"}
                //},
                title: {
                    text: yearLabel + " SEMESTER LOG ACTIVITY",
                    font: {
                        color: "#063a69",
                        size: 20,
                        weight: 900,
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                crosshair: {
                    enabled: true,
                    horizontalLine: { visible: true }
                },
                zoomAndPan: {
                    argumentAxis: "both"
                },
                scrollBar: {
                    visible: true,
                    position: 'bottom',
                    offset: 10,
                    width: 5
                },
                legend: {
                    visible: true,
                    position: 'outside',
                    horizontalAlignment: 'center',
                    verticalAlignment: 'top',
                    itemTextPosition: 'top',
                    font: {
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                argumentAxis: {
                    argumentType: "semester",
                    //minVisualRangeLength: { minutes: 20 },
                    //visualRange: {
                    //    length: { minutes: 30 },
                    //},
                    font: {
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                valueAxis: {
                    placeholderSize: 50
                },
                tooltip: {
                    enabled: true,
                    location: "edge",
                    customizeTooltip: function (arg) {
                        var date = Globalize.dateFormatter({ time: "short" })(new Date(arg.argument));
                        return {
                            text: '<div><p><b>' + arg.seriesName + "</b></p> (" + date + ') : ' + formatNumber(parseInt(arg.valueText)) + '</div>'
                        };
                    }
                }, export: {
                    enabled: true,
                    printingEnabled: false
                },
                series: [
                <%= seriesSemester %>
                ]
            }).dxChart("instance");
        }

        function updateProgressbar() {
            /* Progress bar */
            var determinates = document.querySelectorAll('.mdc-linear-progress');
            for (var i = 0, determinate; determinate = determinates[i]; i++) {
                var linearProgress = mdc.linearProgress.MDCLinearProgress.attachTo(determinate);
                linearProgress.progress = $(determinate).attr("aria-valuenow");
            }
        }

        function formatNumber(value) {
            return Globalize.formatNumber(value, { maximumFractionDigits: 0 });
        }

        function getMonth(date) {
            var a = null == date ? new Date() : date;
            yearLabel = a.getFullYear();

            var month = new Array();
            month[0] = "January";
            month[1] = "February";
            month[2] = "March";
            month[3] = "April";
            month[4] = "May";
            month[5] = "June";
            month[6] = "July";
            month[7] = "August";
            month[8] = "September";
            month[9] = "October";
            month[10] = "November";
            month[11] = "December";

            monthLabel = month[a.getMonth()];

            dayLabel = null == date ? "TODAY" : a.getDate() + " " + monthLabel;

            return month[a.getMonth()];
        }

    </script>
</asp:Content>


