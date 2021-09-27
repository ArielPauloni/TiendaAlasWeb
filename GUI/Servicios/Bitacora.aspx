<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="GUI.Servicios.Bitacora.Bitacora" MasterPageFile="~/Site.Master" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <div class="form-group col-md-12">
        <asp:GridView ID="grvBitacora" runat="server" AllowSorting="True" Caption="-Bitacora sistema"
            AutoGenerateColumns="False" AllowPaging="True" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvBitacora_PageIndexChanging">
            <AlternatingRowStyle BackColor="#CCFFFF" />
            <Columns>
                <asp:BoundField DataField="Cod_Usuario" HeaderText="-Ocultar!">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
                <asp:BoundField DataField="Cod_Evento" HeaderText="-Ocultar!">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
                <asp:BoundField DataField="CriticidadTexto" HeaderText="-Criticidad" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
                <asp:BoundField DataField="NombreUsuario" HeaderText="-Usuario">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
                <asp:BoundField DataField="DescripcionEvento" HeaderText="-Descripcion">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
                <asp:BoundField DataField="FechaEvento" HeaderText="-Fecha Evento">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <hr />
    </div>
</asp:Content>
