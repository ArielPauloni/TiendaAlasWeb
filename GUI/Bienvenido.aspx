<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bienvenido.aspx.cs" Inherits="GUI.Bienvenido" MasterPageFile="~/Site.Master" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <%--<div style="text-align: center;">
        <br />
        <asp:DropDownList ID="ddlIdiomas" ClientIDMode="Static" runat="server"></asp:DropDownList>
    </div>--%>
    <div style="text-align: center;">
        <br />
        <asp:Label ID="lblHolaMundo" runat="server" Text="-¡Hola Mundo!" Font-Bold="true"></asp:Label>
        <%-- <br />
        <br />
         <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-success" Text="-Aceptar" OnClick="btnAceptar_Click" />--%>
        <br />
        <br />
        <%--<asp:Button ID="btnAlerta" runat="server" CssClass="btn btn-info" Text="-Alerta" data-toggle="modal" data-target="#exampleModalCenter" />--%>
        <asp:Button ID="btnAlerta" runat="server" CssClass="btn btn-info" Text="-Alerta" OnClick="btnAlerta_Click" />
    </div>

    <!-- Modal -->
    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalCenterTitle">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblAlerta" runat="server" Text="-Alerta"></asp:Label>
                    <br />
                    <br />
                    <asp:TextBox ID="txtPrueba" runat="server"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnGuardarCambios" runat="server" Text="-Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardarCambios_click" />
                    <asp:Button ID="btnCerrarAlerta" runat="server" Text="-Cancelar" CssClass="btn btn-secondary" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function alertaShow() {
            $('#exampleModalCenter').modal();
        }
    </script>
</asp:Content>
