<%@ Page Title="-Acerca De" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcercaDe.aspx.cs" Inherits="GUI.AcercaDe" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2><%: Title %>.</h2>

    <div style="text-align: center;">
        <header>
            <asp:Label ID="lblTitle" runat="server" Text="-Acerca de" Font-Size="Large"></asp:Label>
        </header>
        <br />
        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Imagenes/logo_tiendaAlas.jpg" />
    </div>
    <br />
</asp:Content>
