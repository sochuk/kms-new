<%@ Page Language="C#" MasterPageFile="~/CPanel.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="KMS.Dashboard" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="Header" runat="server">
    <link href="Content/dx.common.css" rel="stylesheet" />
    <link href="Content/dx.light.css" rel="stylesheet" />
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

    <div class="mdc-layout-grid mb-4">
        tes
        <div class="mdc-layout-grid__inner">
            <%  
                int vendorId = HttpContext.Current.User.Identity.Get_VendorID().ToInteger();
                M_Contract contract = M_Contract.Select("vendor_id", vendorId.ToString());

                if(vendorId > 0)
                {
                    var contract_data = KMS.Logs.Model.Vendor_Log.GetContractInfo();
                    string list = "";
                    foreach (DataRow row in vendor.Rows)
                    {
                        string vendorName = row["vendor_name"].ToString().ToUpper();
                        string vendorCountId = "count" + vendorName.Replace(" ", string.Empty);
                        string vendorProgressId = "progress" + vendorName.Replace(" ", string.Empty);
                        string vendorTextProgressId = "textProgress" + vendorName.Replace(" ", string.Empty);
                        string vendorColor = row["color"].ToString();
                        var percentage = (((contract_data.Rows[0]["current_hit"]).ToInteger() * 0.1f) / ((contract_data.Rows[0]["quota"]).ToInteger() * 0.1f));

                        list += "<div class=\"mdc-layout-grid__cell stretch-card mdc-layout-grid__cell--span-3-desktop mdc-layout-grid__cell--span-4-tablet\">";
                        list += "<div class=\"mdc-card info-card info-card\">";
                        list += "<div class=\"card-inner\">";
                        list += "<h5 class=\"card-title\">" + vendorName + "</h5>";
                        list += "<h4 class=\"font-weight-bold text-primary pb-2 mb-1 border-bottom\" id=\"" + vendorCountId + "\">"+ string.Format("{0:N0}", (contract_data.Rows[0]["current_hit"]).ToInteger()) +"</h4>";
                        list += "<div class=\"card-icon-wrapper\" style=\"background-color:"+ vendorColor+";box-shadow:0 0 10px 5px #ffffff\"><i class=\"material-icons\">trending_up</i></div>";
                        list += "</div>";
                        list += "<p class=\"tx-12 text-muted mt-2\" id=\"" + vendorTextProgressId + "\">"+ (Math.Round(percentage, 2) * 100) +"% quota reached of "+ string.Format("{0:N0}", (contract_data.Rows[0]["quota"]).ToInteger()) +"</p>";

                        list += "<div role=\"progressbar\" class=\"mdc-linear-progress mdc-linear-progress\" id=\"" + vendorProgressId + "\" aria-valuemin=\"0\" aria-valuemax=\"1\" aria-valuenow=\""+ percentage.ToString().Replace(",", ".") +"\">";
                        list += "<div class=\"mdc-linear-progress__buffering-dots\"></div>";
                        list += "<div class=\"mdc-linear-progress__buffer\"></div>";
                        list += "<div class=\"mdc-linear-progress__bar mdc-linear-progress__primary-bar\"><span class=\"mdc-linear-progress__bar-inner\" style=\"background-color:"+ vendorColor+";\"></span></div>";
                        list += "<div class=\"mdc-linear-progress__bar mdc-linear-progress__secondary-bar\"><span class=\"mdc-linear-progress__bar-inner\"></span></div>";
                        list += "</div>";

                        list += "</div>";
                        list += "</div>";
                    }

                    Response.Write(list);


                %>
                <div class="mdc-layout-grid__cell stretch-card mdc-layout-grid__cell--span-3-desktop mdc-layout-grid__cell--span-4-tablet">
                    <div class="mdc-card info-card info-card--dark">
                        <div class="card-inner">
                            <h5 class="card-title">TARGET</h5>
                            <h4 class="font-weight-bold text-primary pb-2 mb-1 border-bottom" id="textTarget">
                                <% Response.Write(string.Format("{0:N0}", (contract_data.Rows[0]["quota"]).ToInteger())); %>
                            </h4>
                            <div class="card-icon-wrapper">
                                <i class="material-icons">dvr</i>
                            </div>
                        </div>
                        <p class="tx-12 mt-2 mb-0" id="textValid"><i class='fa fa-calendar'></i> <b>START : </b><% Response.Write(contract_data.Rows[0].Field<DateTime>("period_start").ToString("ddd, dd MMM yyyy HH:mm:ss")); %></p>
                        <p class="tx-12 m-0" id="textValidUntil"><i class='fa fa-calendar-check'></i> <b>VALID UNTIL : </b><% Response.Write(contract_data.Rows[0].Field<DateTime>("period_end").ToString("ddd, dd MMM yyyy HH:mm:ss")); %></p>
                    </div>
                </div>

                <%
                }
                else
                {
                
                Response.Redirect("~/");

                }
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
                <div class="mdc-card">
                    
                    <div id="chkHourlyShowLabel"></div>
                    <div id="chartHourly" class="w-100"></div>
                    <div id="chartHourlyLoadPanel"></div>
                </div>
            </div>
            <div class="mdc-layout-grid__cell stretch-card mdc-layout-grid__cell--span-7 mdc-layout-grid__cell--span-12-tablet w-100">
                <div class="mdc-card">
                    <div id="chkDailyShowLabel"></div>
                    <div id="chartDaily" class="w-100"></div>
                    <div id="chartDailyLoadPanel"></div>
                </div>
            </div>
            <div class="mdc-layout-grid__cell stretch-card mdc-layout-grid__cell--span-5 mdc-layout-grid__cell--span-12-tablet w-100">
                <div class="mdc-card">
                    <div id="chkMonthlyShowLabel"></div>
                    <div id="chartMonthly" class="w-100"></div>
                    <div id="chartMonthlyLoadPanel"></div>
                </div>
            </div>
        </div>
    </div>



    <div class="d-none">
        <dx:BootstrapChart runat="server" ID="chartBootstrap"></dx:BootstrapChart>
    </div>

</asp:Content>

<asp:Content ID="Scripts" ContentPlaceHolderID="Script" runat="server">
    <script>
        $("#pnAverage").unwrap();
        let hourly, daily, monthly;
        let chartHourly, chartDaily, chartMonthly;

        $(function () {
            var log = $.connection.vendorLogHub;
            hourly = new DevExpress.data.DataSource({
                load: function () {
                    return log.server.getHourlyData();
                },
                key: "fulldate",
            });

            daily = new DevExpress.data.DataSource({
                load: function () {
                    return log.server.getDailyData();
                },
                key: "fulldate",
            });

            monthly = new DevExpress.data.DataSource({
                load: function () {
                    return log.server.getMonthlyData();
                },
                key: "fulldate",
            });

            log.client.refreshDashboard = function (data) {
                window.location.reload();
            };

            log.client.getContractInfo = function (data) {                
                var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
                $("#textTarget").text(formatNumber(data[0].quota));
                $("#textValid").html("<i class='fa fa-calendar'></i> <b>START :</b> " + Globalize.dateFormatter({ skeleton: "yMMMEd Hms" })(new Date(data[0].period_start)));
                $("#textValidUntil").html("<i class='fa fa-calendar-check'></i> <b>VALID UNTIL :</b> " + Globalize.dateFormatter({ skeleton: "yMMMEd Hms" })(new Date(data[0].period_end)));
            };

            log.client.updateHourly = function (data) {
                hourly.reload();
            };

            log.client.updateDaily = function (data) {
                daily.reload();
            };

            log.client.updateMonthly = function (data) {
                monthly.reload();
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
                });
                $("#countAVERAGE").text(parseFloat(avr / data.length).toFixed(1));  
                $('a[href*="www.devexpress.com"], body > div:first-child[style*="padding"]').attr("style", "none !important");
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
                    generateChartMonthly();
                    log.server.getSummaryData();

                    log.server.getContractInfoData();

                    repeatEvery(log.server.reloadCounter, (20 * 1000));
                    repeatEvery(log.server.reloadAll, (3600 * 1000));
                }, 1000);

                updateProgressbar();
            });


        })

        document.querySelector('.sidebar-toggler').addEventListener('click', function () {
            setTimeout(function () {
                chartHourly.render();
                chartDaily.render();
                chartMonthly.render();
            }, 700);
            
        });

        function generateChartHourly() {
            chartHourly = $("#chartHourly").dxChart({
                dataSource: hourly,
                loadingIndicator: {
                    enabled: false
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
                        connector: {
                            visible: true
                        }
                    },
                    ignoreEmptyPoints: true,
                    barPadding: 0.05,
                    type: "bar"
                },
                title: {
                    text: "TODAY HOURLY LOG ACTIVITY",
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
                    verticalAlignment: 'bottom',
                    itemTextPosition: 'top',
                    font: {
                        family: 'Roboto, Helvetica, Arial, sans-serif'
                    },
                },
                argumentAxis: {
                    argumentType: "datetime",
                    minVisualRangeLength: { minutes: 60 },
                    visualRange: {
                        length: "hour"
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
                        return {
                            text: arg.seriesName + " : " + formatNumber(parseInt(arg.valueText))
                        };
                    }
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
                    type: "spline",
                },
                title: {
                    text: getMonth().toUpperCase() + " DAILY LOG ACTIVITY",
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
                series: [
                <%= seriesDaily %>
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
                    type: "splinearea",
                },
                title: {
                    text: new Date().getFullYear() +  " MONTHLY LOG ACTIVITY",
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
                series: [
                <%= seriesMonthly %>
                ],
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

        function getMonth() {
            var a = new Date();
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

            return month[a.getMonth()];
        }
    </script>
</asp:Content>