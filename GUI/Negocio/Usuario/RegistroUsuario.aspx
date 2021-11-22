<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="GUI.Negocio.Usuario.RegistroUsuario" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

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
    <br />
    <div class="wrap">
        <div class="login-form">
            <div class="form-header">
                <asp:Label ID="lblRegistrarse" runat="server" Text="-Register page" Font-Bold="true"></asp:Label>
            </div>
            <!--Aliad Input-->
            <div class="form-group">
                <label id="lblAlias" runat="server">-Alias</label>
                <asp:TextBox ID="txtAlias" runat="server" CssClass="form-input" AutoPostBack="true"></asp:TextBox>
            </div>
            <!--Email Input-->
            <div class="form-group">
                <label id="lblMail" runat="server">-Mail</label>
                <asp:TextBox ID="txtMail" runat="server" CssClass="form-input" AutoPostBack="true"></asp:TextBox>
            </div>
            <!--Password Input-->
            <div class="form-group">
                <label id="lblPassword1" runat="server">-pass</label>
                <br />
                <asp:TextBox ID="txtPassword1" runat="server" CssClass="form-inputPass" TextMode="Password"></asp:TextBox>
                <button id="btnShowHidePass1" runat="server" class="btnShowHidePass fa fa-eye" onserverclick="ShowHidePass1_click"></button>
            </div>
            <div class="form-group">
                <label id="lblPassword2" runat="server">-pass2</label>
                <br />
                <asp:TextBox ID="txtPassword2" runat="server" CssClass="form-inputPass" TextMode="Password"></asp:TextBox>
                <button id="btnShowHidePass2" runat="server" class="btnShowHidePass fa fa-eye" onserverclick="ShowHidePass2_click"></button>
            </div>
            <!--Grabar/cancelar-->
            <div class="form-group">
                <button id="btnGrabar" runat="server" class="btn btn-primary fa fa-check-circle" onserverclick="btnGrabar_Click"></button>
                <button id="btnCancelar" runat="server" class="btn btn-secondary fa fa-window-close-o" onserverclick="btnCancelar_Click"></button>
            </div>
        </div>
    </div>

    <div class="modal fade" id="MensajeModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <uc1:UC_MensajeModal runat="server" ID="UC_MensajeModal" />
        </div>
    </div>

    <script type="text/javascript">
        function mostrarMensaje() {
            $('#MensajeModal').modal({ backdrop: 'static', keyboard: false });
        }
    </script>

</asp:Content>
