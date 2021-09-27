<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ABMUsuario.aspx.cs" Inherits="GUI.Servicios.Usuarios.ABMUsuario" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_Procesando.ascx" TagPrefix="uc1" TagName="UC_Procesando" %>


<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />

    <div class="form-group col-md-12">
        <asp:GridView ID="grvUsuarios" runat="server" AllowSorting="True" Caption="-Usuarios"
            AutoGenerateColumns="False" AllowPaging="True" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvUsuarios_PageIndexChanging">
            <AlternatingRowStyle BackColor="#CCFFFF" />
            <Columns>
                <asp:BoundField DataField="Cod_Usuario" HeaderText="-Ocultar!">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
                <asp:BoundField DataField="Apellido" HeaderText="-Apellido">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
                <asp:BoundField DataField="Nombre" HeaderText="-Nombre">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
                <asp:BoundField DataField="Alias" HeaderText="-Alias">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
                <asp:BoundField DataField="TipoUsuario" HeaderText="-Tipo Usuario">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
                <asp:BoundField DataField="Telefono" HeaderText="-Telefono">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
                 <asp:BoundField DataField="Mail" HeaderText="-Mail">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
                 <asp:BoundField DataField="FechaNacimiento" HeaderText="-Fecha de Nacimiento" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <hr />
    </div>

    <div class="form-group col-md-12">
        <br />
        <asp:Button ID="btnCrearNuevoUsuario" CssClass="btn btn-primary" runat="server" Text="-Crear Nuevo Usuario" OnClick="btnCrearNuevoUsuario_Click" />
    </div>


    <!-- Modal Procesando -->
    <div class="modal fade" id="processingModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <uc1:UC_Procesando runat="server" ID="UC_Procesando" />
        </div>
    </div>

    <script type="text/javascript">
        function processingShow() {
            $('#processingModal').modal({ backdrop: 'static', keyboard: false });
        }
    </script>
</asp:Content>
