<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_MensajeModal.ascx.cs" Inherits="GUI.User_Controls.UC_MensajeModal" %>

<!-- Modal Mensaje -->
<div class="modal-content">
    <div class="modal-header">
        <h5 class="modal-title" id="MensajeModalTitle" runat="server">-Mensaje</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
    </div>
    <div class="modal-body bg-info" style="text-align: center;">
        <br />
        <div>
            <asp:Label ID="lblMensaje" runat="server" Text="-Mensaje"></asp:Label>
        </div>
        <br />
    </div>
    <div class="modal-footer">
        <button id="btnConfirmar" runat="server" class="btn btn-primary fa fa-check-circle" data-dismiss="modal"></button>
    </div>
</div>
