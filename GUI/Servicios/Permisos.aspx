<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="GUI.Servicios.Permisos.Permisos" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="text-align: center;">
        <br />
        <asp:Label ID="lblPermisos" runat="server" Text="-Permisos page" Font-Bold="true"></asp:Label>
        <br />
        <hr />
    </div>
    <div class="container">
        <div class="form-group col-md-5">
            <asp:Label ID="lblPermisosArbol" runat="server" Text="-Permisos"></asp:Label>
            <asp:Panel runat="server" ScrollBars="Auto" Height="50%">
                <asp:TreeView ID="trvPermisos" runat="server" ShowCheckBoxes="All" OnSelectedNodeChanged="trvPermisos_SelectedNodeChanged" BackColor="#CCFFFF" BorderColor="Black">
                </asp:TreeView>
            </asp:Panel>
        </div>
        <div class="form-group col-md-2" style="text-align: center;">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Button ID="btnAsignarPermiso" runat="server" Text="-Agregar ->" OnClick="btnAsignarPermiso_Click" />
            <%--fa fa-arrow-right || fa fa-hand-point-right--%>
            <br />
            <br />
            <asp:Button ID="btnQuitarPermiso" runat="server" Text="-<- Quitar" OnClick="btnQuitarPermiso_Click" />
            <%--fa fa-arrow-left || fa fa-hand-point-left--%>
        </div>
        <div class="form-group col-md-5">
            <asp:Label ID="lblTipoUsuario" runat="server" Text="-Tipo de Usuario"></asp:Label>
            <asp:DropDownList ID="ddlTipoUsuarios" runat="server" OnSelectedIndexChanged="ddlTipoUsuarios_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <br />
            <asp:Panel runat="server" ScrollBars="Auto" Height="50%">
                <asp:TreeView ID="trvPermisosUsuario" runat="server" ShowCheckBoxes="All" BackColor="#CCFFFF" BorderColor="Black">
                </asp:TreeView>
            </asp:Panel>
        </div>
    </div>
    <hr />
    <div style="text-align: left;">
        <asp:CheckBox ID="chkEditarPermisos" runat="server" AutoPostBack="true" OnCheckedChanged="chkEditarPermisos_CheckedChanged" />
        <asp:Label ID="lblEditarPermisos" runat="server" Text="-Editar Permisos"></asp:Label>
        <br />
    </div>
    <br />
    <div class="container bg-info" id="divEditarPermisos" runat="server" visible="false">
        <div class="form-group col-md-4">
            <asp:Label ID="lblNuevoPermiso" runat="server" Text="-Permiso"></asp:Label>
            <asp:TextBox ID="txtNuevoPermiso" CssClass="form-control" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnCrearPermiso" runat="server" Text="-Crear Permiso" OnClick="btnCrearPermiso_Click" />
            <%--fa fa-plus--%>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnEliminarPermiso" runat="server" Text="-Eliminar Permiso" OnClick="btnEliminarPermiso_Click" />
            <%--fa fa-trash-alt || fa fa-trash-o || fa fa-trash--%>
        </div>
        <div class="form-group col-md-4">
            <asp:Label ID="lblPermisoPadre" runat="server" Text="-Permiso Padre"></asp:Label>
            <asp:TextBox ID="txtPermisoPadre" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label ID="lblPermisoHijo" runat="server" Text="-Permiso Hijo"></asp:Label>
            <asp:TextBox ID="txtPermisoHijo" CssClass="form-control" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnVincular" runat="server" Text="-Vincular" OnClick="btnVincular_Click" />
            <%--fa fa-link--%>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDesvincular" runat="server" Text="-Desvincular" OnClick="btnDesvincular_Click" />
            <%--fa fa-unlink--%>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnLimpiar" runat="server" Text="-Limpiar" OnClick="btnLimpiar_Click" />
            <%--fa fa-broom--%>
        </div>
    </div>

    <div class="modal fade" id="MensajeModal" tabindex="-1" role="dialog" aria-labelledby="MensajeModalTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <asp:UpdatePanel runat="server" ID="UpPanelDialog" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc1:UC_MensajeModal runat="server" ID="UC_MensajeModal" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script type="text/javascript">
        function mostrarMensaje() {
            $('#MensajeModal').modal({ backdrop: 'static', keyboard: false, toggle: true });
        }
    </script>
</asp:Content>
