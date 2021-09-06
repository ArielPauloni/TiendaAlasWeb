<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Idioma.aspx.cs" Inherits="GUI.Servicios.Idioma" MasterPageFile="~/Site.Master" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="form-group col-md-12">
        <br />
        <asp:Label ID="lblIdioma" runat="server" Text="-Idioma" Font-Bold="true"></asp:Label>
        :<asp:DropDownList ID="ddlIdiomas" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control form-control-select" OnSelectedIndexChanged="ddlIdiomas_SelectedIndexChanged" Width="159px"></asp:DropDownList>
    </div>

    <div class="form-group col-md-12">
        <asp:GridView ID="grvTexto" runat="server" AllowSorting="True" Caption="-Textos"
            AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="idFrase" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvTexto_PageIndexChanging">
            <AlternatingRowStyle BackColor="#CCFFFF" />
            <Columns>
                <asp:BoundField DataField="idFrase" />
                <asp:BoundField DataField="Texto" HeaderText="-Texto">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>

    <div class="form-group col-md-12">
        <br />
        <asp:Button ID="btnCrearNuevoIdioma" CssClass="btn btn-primary" runat="server" Text="-Crear Nuevo Idioma" OnClick="btnCrearNuevoIdioma_Click" />
    </div>
</asp:Content>

