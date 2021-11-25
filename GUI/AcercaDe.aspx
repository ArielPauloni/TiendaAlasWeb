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

    <div class="container">
        <div class="form-row col-md-12">
            <asp:Label ID="lblHistoria" runat="server" Font-Bold="true" Text="-Historia:"></asp:Label>
            <asp:Label ID="lblHistoriaTexto" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblMision" runat="server" Font-Bold="true" Text="-Mision:"></asp:Label>
            <asp:Label ID="lblMisionTexto" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblVision" runat="server" Font-Bold="true" Text="-Vision:"></asp:Label>
            <asp:Label ID="lblVisionTexto" runat="server" Text=""></asp:Label>
            <br />
        </div>
    </div>
</asp:Content>
