<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="GUI.Servicios.Seguridad.Backup" MasterPageFile="~/Site.Master" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="text-align: center;">
        <br />
        <br />
        <asp:Label ID="lblBackup" runat="server" Text="-Backup page" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnBackup" runat="server" CssClass="btn btn-primary" Text="-Generar Backup" OnClick="btnBackup_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRestore" runat="server" CssClass="btn btn-primary" Text="-Restaurar Backup" />
        <br />
        <br />
    </div>

    <!-- Modal -->
    <div class="modal fade" id="CrearBkpModal" tabindex="-1" role="dialog" aria-labelledby="CrearBkpModalTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="CrearBkpModalTitle" runat="server">-Generar Backup</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblNombreBkp" runat="server" Text="-Nombre"></asp:Label>
                    :
                    <asp:TextBox ID="txtNombreBkp" runat="server" Font-Size="14px"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnGuardar" runat="server" Text="-Guardar" CssClass="btn btn-primary" OnClick="btnGuardarCambios_click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="-Cancelar" CssClass="btn btn-secondary" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function generarBackupShow() {
            $('#CrearBkpModal').modal();
        }
    </script>
</asp:Content>
