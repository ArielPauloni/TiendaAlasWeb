﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="GUI.Servicios.Seguridad.Backup" MasterPageFile="~/Site.Master" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="text-align: center;">
        <br />
        <br />
        <asp:Label ID="lblBackup" runat="server" Text="-Backup page" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnBackup" runat="server" CssClass="btn btn-primary" Text="-Generar Backup" OnClick="btnBackup_Click" OnClientClick="generarBackupShow()" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRestore" runat="server" CssClass="btn btn-primary" Text="-Restaurar Backup" OnClick="btnRestore_Click" />
        <br />
        <br />
    </div>

    <!-- Modal Crear BKP -->
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

    <!-- Modal Mensaje -->
    <div class="modal fade" id="MensajeModal" tabindex="-1" role="dialog" aria-labelledby="MensajeModalTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="MensajeModalTitle" runat="server">-Mensaje</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="text-align: center;">
                    <br />
                    <br />
                    <div>
                        <asp:Label ID="lblMensaje" runat="server" Text="-Mensaje"></asp:Label>
                    </div>
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>

     <!-- Modal Restore -->
    <div class="modal fade" id="RestaurarModal" tabindex="-1" role="dialog" aria-labelledby="RestaurarModalTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="RestaurarModalTitle" runat="server">-Mensaje</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="text-align: center;">
                    <br />
                    <br />
                    <div>
                        <asp:Label ID="lblArchivoRestore" runat="server" Text="-Seleccione archivo"></asp:Label>:
                        <asp:DropDownList ID="ddlArchivosBkp" runat="server"></asp:DropDownList>
                    </div>
                    <br />
                    <br />
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnAceptarRestore" runat="server" Text="-Confirmar" CssClass="btn btn-primary" OnClick="btnAceptarRestore_Click" />
                    <asp:Button ID="btnCancelarRestore" runat="server" Text="-Cancelar" CssClass="btn btn-secondary" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function generarBackupShow() {
            $('#CrearBkpModal').modal({ backdrop: 'static', keyboard: false });
        }
    </script>

    <script type="text/javascript">
        function modalMensaje() {
            $('#MensajeModal').modal();
        }
    </script>

    <script type="text/javascript">
        function restaurarModal() {
            $('#RestaurarModal').modal();
        }
    </script>
</asp:Content>
