﻿<%@ Page Language="C#" MasterPageFile="~/CPanel.Master" AutoEventWireup="true" CodeBehind="Server.aspx.cs" Inherits="KMS.Master.Server" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function grid_EndCallback(s, e) {
            if (s.cpRefresh) {
                grid.Refresh();
                s.cpRefresh = false;
            }
            if (s.cpShowDeleteConfirm) {
                grid_delete.Refresh();
                s.cpShowDeleteConfirm = false;
                Confirm.Show();                
            }

            EndCallback(s, e);
        }
    </script>

    <dx:ASPxGridView runat="server" ID="grid" ClientInstanceName="grid" 
        Width="100%" CssClass="w-100"
        KeyFieldName="server_id"
        EnableCallBacks="true"        
        OnInit="grid_Init"
        OnCustomButtonInitialize="grid_CustomButtonInitialize"
        OnCustomButtonCallback="grid_CustomButtonCallback"
        OnCustomCallback="grid_CustomCallback"
        OnCommandButtonInitialize="grid_CommandButtonInitialize"
        OnRowValidating="grid_RowValidating"
        OnRowInserting="grid_RowInserting"
        OnRowUpdating="grid_RowUpdating"
        OnRowDeleting="grid_RowDeleting"
        OnHtmlRowPrepared="grid_HtmlRowPrepared"
        ClientSideEvents-EndCallback="grid_EndCallback">
        <SettingsSearchPanel CustomEditorID="tbSearch" />

        <Toolbars>
            <dx:GridViewToolbar Name="Toolbar">
                <Items>
                    <%-- Image-IconID Refer to https://demos.devexpress.com/ASPxMultiUseControlsDemos/Features/IconLibraryExplorer.aspx --%>
                    <dx:GridViewToolbarItem Command="New" Name="New" Image-IconID="actions_new_16x16office2013" Text="New" />
                    <dx:GridViewToolbarItem Command="Refresh" Name="Refresh" BeginGroup="true" Image-IconID="actions_refresh_16x16office2013" />
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
                    <dx:GridViewToolbarItem AdaptivePriority="1" Name="Import" Visible="false" Text="Upload from" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                        <Items>
                            <dx:GridViewToolbarItem Text="CSV" Image-IconID="mail_sendcsv_16x16office2013" />
                            <dx:GridViewToolbarItem Text="Excel File" Image-IconID="mail_sendxls_16x16office2013" />
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
                            <dx:ASPxButtonEdit ID="tbSearch" runat="server" NullText="Search" Height="100%" />
                        </Template>
                    </dx:GridViewToolbarItem>                  
                </Items>
            </dx:GridViewToolbar>
        </Toolbars>

        <Columns>
            <dx:GridViewBandColumn Caption="Data" VisibleIndex="0">
                <HeaderStyle HorizontalAlign="Center" />
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" Width="30" ShowClearFilterButton="true" SelectAllCheckboxMode="Page" />

                    <dx:GridViewDataTextColumn FieldName="server_name" Caption="Server Name" Settings-AllowHeaderFilter="True">
                        <PropertiesTextEdit MaxLength="200">
                            <ValidationSettings ErrorTextPosition="Bottom" SetFocusOnError="true" Display="Dynamic" ErrorDisplayMode="Text" ValidationGroup="Validate">
                                <RequiredField IsRequired="true" ErrorText="* Data required" />
                                <ErrorFrameStyle CssClass="small pl-0"></ErrorFrameStyle>
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataMemoColumn FieldName="server_desc" Caption="Server Description" />

                    <dx:GridViewDataTextColumn FieldName="ip_address" Caption="IP Address" >
                        <PropertiesTextEdit>
                            <ValidationSettings ErrorTextPosition="Bottom" SetFocusOnError="true" Display="Dynamic" ErrorDisplayMode="Text" ValidationGroup="Validate">
                                <RequiredField IsRequired="true" ErrorText="* Data required" />
                                <ErrorFrameStyle CssClass="small pl-0"></ErrorFrameStyle>
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="log_path" Caption="Log Path" >
                        <PropertiesTextEdit>
                            <ValidationSettings ErrorTextPosition="Bottom" SetFocusOnError="true" Display="Dynamic" ErrorDisplayMode="Text" ValidationGroup="Validate">
                                <RequiredField IsRequired="true" ErrorText="* Data required" />
                                <ErrorFrameStyle CssClass="small pl-0"></ErrorFrameStyle>
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataCheckColumn MinWidth="50" Width="60" FieldName="isactive" Caption="Active" EditFormSettings-Visible="False" />
                    <dx:GridViewDataComboBoxColumn FieldName="createby" Caption="Create by" Visible="false" Settings-AllowHeaderFilter="True" MaxWidth="100">
                        <PropertiesComboBox DisplayFormatString="{1}" ValueField="user_id" TextField="fullname" />
                    </dx:GridViewDataComboBoxColumn> 
                    <dx:GridViewDataDateColumn FieldName="createdate" Caption="Create Date" Visible="false" Settings-AllowHeaderFilter="True"  />
                    <dx:GridViewDataComboBoxColumn FieldName="updateby" Caption="Update by" Visible="false" Settings-AllowHeaderFilter="True" MaxWidth="100">
                        <PropertiesComboBox DisplayFormatString="{1}" ValueField="user_id" TextField="fullname" />
                    </dx:GridViewDataComboBoxColumn> 
                    <dx:GridViewDataTextColumn FieldName="updatedate" Caption="Update Date" Visible="false" Settings-AllowHeaderFilter="True" />
                </Columns>

            </dx:GridViewBandColumn>
            <dx:GridViewCommandColumn HeaderStyle-HorizontalAlign="Center"
                AdaptivePriority="2"
                VisibleIndex="0"
                ButtonRenderMode="Button"
                ShowEditButton="true"
                ShowDeleteButton="true"
                ShowApplyFilterButton="true"
                ShowClearFilterButton="true"
                MaxWidth="230"
                Caption="Action">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton Text="Enable" ID="ButtonEnable"></dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton Text="Disable" ID="ButtonDisable"></dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>

        <Templates>
            <EditForm>
                <dx:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors" runat="server" />
                <div class="d-flex justify-content-end mt-3 mb-3">
                    <dx:ASPxButton CssClass="mr-2" runat="server" Text="Update" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e) { if(ASPxClientEdit.ValidateGroup('Validate'))grid.UpdateEdit();}" />
                    </dx:ASPxButton>
                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server" />
                </div>
            </EditForm>
        </Templates>
    </dx:ASPxGridView>

    <dx:ASPxPopupControl ID="Confirm" runat="server" ClientInstanceName="Confirm"
        Width="450" Height="150" ClientSideEvents-PopUp="function(s,e){s.ShowAtPos(s.Left, 100)}"
        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="BottomSides"
        CloseAction="CloseButton" CloseOnEscape="true" AllowDragging="true" PopupAnimationType="None"
        EnableViewState="false" AutoUpdatePosition="true" ShowFooter="true" FooterImage-AlternateText="FS"
        HeaderText="Confirmation" >
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <p>Are you sure want delete selected data?</p>
                <dx:ASPxGridView runat="server" ID="grid_delete" ClientInstanceName="grid_delete"  AutoGenerateColumns="true" CssClass="w-100">                    
                    <SettingsPager EnableAdaptivity="true" />
                    <SettingsBehavior AllowEllipsisInText="true" />
                </dx:ASPxGridView>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <FooterTemplate>
            <table class="w-100">
                <tr>
                    <td class="d-flex justify-content-end p-2">
                        <dx:ASPxButton CssClass="mr-2" ID="YesButton" runat="server" Text="Yes" AutoPostBack="False" 
                        ClientSideEvents-Click="function(s, e) { 
                            var k = grid.GetSelectedKeysOnPage();
                            grid.PerformCallback('Delete');
                            Confirm.Hide();       
                            }"/>
                        <dx:ASPxButton ID="CancelButton" runat="server" Text="Cancel" AutoPostBack="False" 
                        ClientSideEvents-Click="function(s, e) { Confirm.Hide(); }"/>
                    </td>
                </tr>
            </table>
        </FooterTemplate>
    </dx:ASPxPopupControl>
</asp:Content>