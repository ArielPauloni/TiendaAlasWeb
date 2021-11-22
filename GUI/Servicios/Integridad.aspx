<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Integridad.aspx.cs" Inherits="GUI.Servicios.Seguridad.Integridad" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />

    <div style="text-align: center;">
        <br />
        <br />
        <asp:Label ID="lblIntegridad" runat="server" Text="-Página de Integridad" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnChequearIntegridad" runat="server" CssClass="btn btn-primary" Text="-Chequear Integridad" OnClick="btnCheckIntegridad_Click" />
        <br />
        <br />
    </div>

     <!-- Modal Mensaje -->
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

