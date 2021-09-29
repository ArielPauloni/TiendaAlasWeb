<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GUI.Login" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>
<%@ Register Src="~/User_Controls/UC_Procesando.ascx" TagPrefix="uc1" TagName="UC_Procesando" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <style>
        .btnShowHidePass {
            background: #fafafa;
            margin: -5px;
            outline: none;
            border: 1px solid #eeeeee;
            padding: 15px;
            border-left: hidden;
        }
    </style>
    <br />
    <div class="wrap">
        <div class="login-form">
            <div class="form-header">
                <asp:Label ID="lblLogin" runat="server" Text="-Login page" Font-Bold="true"></asp:Label>
            </div>
            <!--Alias Input-->
            <div class="form-group">
                <label id="lblAlias" runat="server">-Alias</label>
                <asp:TextBox ID="txtAlias" runat="server" CssClass="form-input" AutoPostBack="true"></asp:TextBox>
            </div>
            <!--Password Input-->
            <div class="form-group">
                <label id="lblPassword" runat="server">-pass</label>
                <br />
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-inputPass" TextMode="Password"></asp:TextBox>
                <button id="btnShowHidePass" runat="server" class="btnShowHidePass fa fa-eye" onserverclick="ShowHidePass_click"></button>
            </div>
            <!--Login Button + Recupero pass + Recordar datos-->
            <div class="form-group">
                <asp:Button CssClass="form-button" ID="btnLogin" runat="server" Text="-LOGIN" OnClick="btnLogin_Click" />
            </div>
            <div class="form-group">
                <asp:CheckBox ID="chkRecordarDatos" runat="server" />
                &nbsp;<asp:Label ID="lblRecordarDatos" runat="server" Text="-Recordarme"></asp:Label>
            </div>
            <div class="form-footer">
                <asp:Label ID="lblRecuperoPass" runat="server" Text="-Olvidó su contraseña?"></asp:Label>
                &nbsp;<asp:LinkButton ID="lnkRecuperoPass" runat="server" Text="-Recuperar contraseña" OnClick="lnkRecuperoPass_Click" OnClientClick="processingShow();"></asp:LinkButton>
            </div>
        </div>
    </div>

    <div class="modal fade" id="MensajeModal" tabindex="-1" role="dialog" aria-labelledby="MensajeModalTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <asp:UpdatePanel runat="server" ID="UpPanelDialog" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc1:UC_MensajeModal runat="server" ID="UC_MensajeModal" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script type="text/javascript">
        function mostrarMensaje() {
            $('#MensajeModal').modal({ backdrop: 'static', keyboard: false, toggle: true });
        }
    </script>

    <!-- Modal Procesando -->
    <div class="modal fade" id="processingModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <uc1:UC_Procesando runat="server" ID="UC_Procesando" />
        </div>
    </div>

    <script type="text/javascript">
        function processingShow() {
            $('#processingModal').modal({ backdrop: 'static', keyboard: false });
        }
    </script>
</asp:Content>
