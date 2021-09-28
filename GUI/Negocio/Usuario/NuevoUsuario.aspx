<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevoUsuario.aspx.cs" Inherits="GUI.Negocio.Usuario.NuevoUsuario"  MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>
<%@ Register Src="~/User_Controls/UC_Procesando.ascx" TagPrefix="uc1" TagName="UC_Procesando" %>



<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
     <div class="container">
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblNombre" runat="server" Text="-Nombre"></asp:Label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblApellido" runat="server" Text="-Apellido"></asp:Label>
                <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblAlias" runat="server" Text="-Alias"></asp:Label>
                <asp:TextBox ID="txtAlias" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblMail" runat="server" Text="-Mail"></asp:Label>
                <asp:TextBox ID="txtMail" CssClass="form-control" placeholder="usuario@mail.com" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblPass1" runat="server" Text="-Contraseña"></asp:Label>
                <asp:TextBox ID="txtPass1" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblPass2" runat="server" Text="-Repita Contraseña"></asp:Label>
                <asp:TextBox ID="txtPass2" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblTelefono" runat="server" Text="-Telefono"></asp:Label>
                <asp:TextBox ID="txtTelefono" CssClass="form-control" placeholder="11 4444-44444" TextMode="Phone" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblTipoUsuario" runat="server" Text="-Tipo Usuario"></asp:Label>
                <asp:DropDownList ID="ddlTipoUsuario" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblFechaNacimiento" runat="server" Text="-Fecha Nacimiento"></asp:Label>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-row">
        <div class="form-group">
            <button runat="server" id="btnGuardar" class="btn btn-primary fa fa-check-circle" onserverclick="btnGuardar_Click" onclick="processingShow();"></button>
            <button runat="server" id="btnCancelar" class="btn btn-secondary fa fa-window-close-o" onserverclick="btnCancelar_Click"></button>
        </div>
    </div>

    <div class="modal fade" id="MensajeModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <uc1:UC_MensajeModal runat="server" ID="UC_MensajeModal" />
        </div>
    </div>

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

    <script type="text/javascript">
        function mostrarMensaje() {
            $('#MensajeModal').modal({ backdrop: 'static', keyboard: false });
        }
    </script>
</asp:Content>
