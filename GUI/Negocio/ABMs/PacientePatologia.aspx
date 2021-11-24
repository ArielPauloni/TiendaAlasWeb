<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PacientePatologia.aspx.cs" Inherits="GUI.Negocio.ABMs.PacientePatologia" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div style="text-align: center;">
        <header>
            <asp:Label ID="lblTitle" runat="server" Text="-Paciente Patologia" Font-Size="Large"></asp:Label>
        </header>
        <br />
    </div>

    <div class="container">
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblPacientes" runat="server" Text="-Paciente"></asp:Label>
                <asp:DropDownList ID="ddlPacientes" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control form-control-select" OnSelectedIndexChanged="ddlPacientes_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblPatologia" runat="server" Text="-Patología"></asp:Label>
                <asp:DropDownList ID="ddlPatologia" ClientIDMode="Static" runat="server" CssClass="form-control form-control-select"></asp:DropDownList>
            </div>
        </div>

        <br />
        <div class="form-row">
            <div class="form-group col-md-6">
                <button runat="server" id="btnGuardar" class="btn btn-primary fa fa-check-circle" onserverclick="btnGuardar_ServerClick"></button>
            </div>
        </div>
        <br />
        <br />
        <div class="form-group col-md-12">
            <asp:Label ID="lblPatologiaPaciente" runat="server" Text="-Patología Actual del paciente"></asp:Label>
            <asp:TextBox ID="txtPatologia" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
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
