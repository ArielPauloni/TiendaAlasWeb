<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="GUI.Servicios.Permisos.Permisos" MasterPageFile="~/Site.Master" %>

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
            <asp:TreeView ID="trvPermisos" runat="server" ShowCheckBoxes="All" Height="50%" OnSelectedNodeChanged="trvPermisos_SelectedNodeChanged" BackColor="#CCFFFF" BorderColor="Black">
            </asp:TreeView>
        </div>
        <div class="form-group col-md-2" style="text-align: center;">
            <br />
            <br /> 
            <br />
            <br /> 
            <br />
            <br />
            <asp:Button ID="btnAsignarPermiso" runat="server" Text="-Agregar ->" OnClick="btnAsignarPermiso_Click" />
            <br />
            <br />
            <asp:Button ID="btnQuitarPermiso" runat="server" Text="-<- Quitar" OnClick="btnQuitarPermiso_Click" />
        </div>
        <div class="form-group col-md-5">
            <asp:Label ID="lblTipoUsuario" runat="server" Text="-Tipo de Usuario"></asp:Label>
            <asp:DropDownList ID="ddlTipoUsuarios" runat="server" OnSelectedIndexChanged="ddlTipoUsuarios_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <br />
            <asp:TreeView ID="trvPermisosUsuario" runat="server" ShowCheckBoxes="All" Height="50%" BackColor="#CCFFFF" BorderColor="Black">
            </asp:TreeView>
        </div>
    </div>
    <hr />
    <div style="text-align: left;">
        <asp:CheckBox ID="chkEditarPermisos" runat="server" AutoPostBack="true" OnCheckedChanged="chkEditarPermisos_CheckedChanged" />
        <asp:Label ID="lblEditarPermisos" runat="server" Text="-Editar Permisos"></asp:Label>
        <br />
    </div>
    <br />
    <div class="container" id="divEditarPermisos" runat="server" visible="false">
        <div class="form-group col-md-4">
            <asp:Label ID="lblNuevoPermiso" runat="server" Text="-Permiso"></asp:Label>
            <asp:TextBox ID="txtNuevoPermiso" CssClass="form-control" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnCrearPermiso" runat="server" Text="-Crear Permiso" OnClick="btnCrearPermiso_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnEliminarPermiso" runat="server" Text="-Eliminar Permiso" OnClick="btnEliminarPermiso_Click" />
        </div>
        <div class="form-group col-md-4">
            <asp:Label ID="lblPermisoPadre" runat="server" Text="-Permiso Padre"></asp:Label>
            <asp:TextBox ID="txtPermisoPadre" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label ID="lblPermisoHijo" runat="server" Text="-Permiso Hijo"></asp:Label>
            <asp:TextBox ID="txtPermisoHijo" CssClass="form-control" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnVincular" runat="server" Text="-Vincular" OnClick="btnVincular_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDesvincular" runat="server" Text="-Desvincular" OnClick="btnDesvincular_Click" /> 
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnLimpiar" runat="server" Text="-Limpiar" OnClick="btnLimpiar_Click" />
        </div>
    </div>
</asp:Content>
