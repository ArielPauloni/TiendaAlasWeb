<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearIdioma.aspx.cs" Async="true" Inherits="GUI.Servicios.CrearIdioma" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_Procesando.ascx" TagPrefix="uc1" TagName="UC_Procesando" %>
<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <div class="container">
        <div class="form-group col-md-12">
            <asp:Label ID="lblCodIdioma" runat="server" Text="-Cod Idioma"></asp:Label>
            <asp:TextBox ID="txtCodIdioma" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <br />
        <div class="form-group col-md-12">
            <asp:Label ID="lblDescripcionIdioma" runat="server" Text="-Descripcion Idioma"></asp:Label>
            <asp:TextBox ID="txtDescripcionIdioma" CssClass="form-control" runat="server"></asp:TextBox>
        </div>

        <div class="form-group form-check">
            <asp:CheckBox ID="chkTraducir" CssClass="form-check-inline" runat="server" Checked="true" Enabled="false" />
            <asp:Label ID="lblTraducir" runat="server" Text="-Traducir Idioma"></asp:Label>
        </div>

        <div class="form-group">
            <button runat="server" id="btnGuardar" class="btn btn-primary fa fa-check-circle" onserverclick="btnGuardar_Click" onclick="processingShow();"></button>
            <button runat="server" id="btnCancelar" class="btn btn-secondary fa fa-window-close-o" onserverclick="btnCancelar_Click"></button>
        </div>
    </div>

    <!-- Modal Procesando -->
    <div class="modal fade" id="processingModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <uc1:UC_Procesando runat="server" id="UC_Procesando" />
        </div>
    </div>

    <script type="text/javascript">
        function processingShow() {
            $('#processingModal').modal({ backdrop: 'static', keyboard: false });
        }
    </script>

    <!-- Modal Mensajes -->
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

