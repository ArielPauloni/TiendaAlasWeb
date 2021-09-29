<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipoUsuario.aspx.cs" Inherits="GUI.Negocio.Usuario.TipoUsuario" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <div class="container">
        <div class="form-group col-md-12">
            <asp:Label ID="lblTipoUsuario" runat="server" Text="-Tipo Usuario"></asp:Label>
            <asp:TextBox ID="txtTipoUsuario" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <br />

        <div class="form-group">
            <button runat="server" id="btnGuardar" class="btn btn-primary fa fa-check-circle" onserverclick="btnGrabar_Click"></button>
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
</asp:Content>
