<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_Procesando.ascx.cs" Inherits="GUI.User_Controls.UC_Procesando" %>

<div class="modal-content">
    <div class="modal-body" style="text-align: center;">
        <br />
        <br />
        <div>
            <asp:Label ID="lblWait" runat="server" Text="-Por favor espere... " />
            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" ImageUrl="~/Imagenes/processing.gif" />
        </div>
        <br />
        <br />
    </div>
</div>


