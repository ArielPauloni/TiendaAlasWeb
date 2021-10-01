<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="GUI.Contact" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div style="text-align: center;">
        <header>
            <asp:Label ID="lblTitle" runat="server" Text="-Contactanos" Font-Size="Large"></asp:Label>
        </header>
    </div>
    <br />

    <div class="wrap">
        <div class="contact-form">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lblNombre" runat="server" Text="-Nombre y apellido"></asp:Label>
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lblEmpresa" runat="server" Text="-Empresa"></asp:Label>
                    <asp:TextBox ID="txtEmpresa" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lblTelefono" runat="server" Text="-Telefono"></asp:Label>
                    <asp:TextBox ID="txtTelefono" CssClass="form-control" placeholder="11 4444-44444" TextMode="Phone" runat="server"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lblMail" runat="server" Text="-Mail"></asp:Label>
                    <asp:TextBox ID="txtMail" CssClass="form-control" TextMode="Email" placeholder="contacto@mail.com" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-12">
                    <asp:Label ID="lblMensaje" runat="server" Text="-Mensaje"></asp:Label>
                    <asp:TextBox ID="txtMensaje" CssClass="form-control form-textboxMultiline" Height="110px" TextMode="MultiLine" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-12">
                    <asp:Label ID="lblNumeroA" runat="server"></asp:Label>&nbsp;+&nbsp;<asp:Label ID="lblNumeroB" runat="server"></asp:Label>&nbsp;=&nbsp;
            <asp:TextBox ID="txtResultado" Width="50px" runat="server" TextMode="Number"></asp:TextBox>
                    <button runat="server" id="btnEnviar" class="btn btn-primary fa fa-paper-plane" onserverclick="btnEnviar_Click"></button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Mensaje -->
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
