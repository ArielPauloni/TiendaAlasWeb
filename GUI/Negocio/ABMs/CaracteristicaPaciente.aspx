<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CaracteristicaPaciente.aspx.cs" Inherits="GUI.Negocio.ABMs.Caracteristica" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <br />
    <div style="text-align: center;">
        <header>
            <asp:Label ID="lblTitle" runat="server" Text="-Características del paciente" Font-Size="Large"></asp:Label>
        </header>
        <br />
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
