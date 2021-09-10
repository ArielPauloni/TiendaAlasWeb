<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="GUI.Servicios.Permisos.Permisos" MasterPageFile="~/Site.Master" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="text-align: center;">
        <br />
        <br />
        <asp:Label ID="lblPermisos" runat="server" Text="Permisos page" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:TreeView ID="trvPermisos" runat="server" ShowCheckBoxes="All">
        </asp:TreeView>
    </div>
</asp:Content>
