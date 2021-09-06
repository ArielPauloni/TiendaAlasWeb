<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearIdioma.aspx.cs" Async="true" Inherits="GUI.Servicios.CrearIdioma" MasterPageFile="~/Site.Master" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <div class="container">
        <div class="form-group col-md-12">
            <asp:Label ID="lblCodIdioma" runat="server" Text="-Cod Idioma"></asp:Label>
            <asp:TextBox ID="txtCodIdioma" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <br />
        <div class="form-group col-md-12">
            <asp:Label ID="lblDescripcionIdioma" runat="server" Text="-Descripcion Idioma"></asp:Label>
            <asp:TextBox ID="txtDescripcionIdioma" CssClass="form-control" runat="server"></asp:TextBox>
        </div>

        <div class="form-group form-check">
            <asp:CheckBox ID="chkTraducir" CssClass="form-check-inline" runat="server" Checked="true" Enabled="false" />
            <asp:Label ID="lblTraducir" runat="server" Text="-Traducir Idioma"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="-Guardar" OnClick="btnGuardar_Click" OnClientClick="processingShow()" />
            <asp:Button ID="btnCancelar" CssClass="btn btn-secondary" runat="server" Text="-Cancelar" OnClick="btnCancelar_Click" />
        </div>
    </div>

    <!-- Modal Procesando -->
    <div class="modal fade" id="processingModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-body" style="text-align: center;">
                    <br />
                    <br />
                    <div>
                        <asp:Label ID="lblWait" runat="server" Text="-Please wait... " />
                        <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" ImageUrl="~/Imagenes/processing.gif" />
                    </div>
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function processingShow() {
            $('#processingModal').modal({backdrop: 'static', keyboard: false});
        }
    </script>
</asp:Content>

