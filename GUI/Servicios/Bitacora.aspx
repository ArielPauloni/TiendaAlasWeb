<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="GUI.Servicios.Bitacora.Bitacora" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>
<%@ Register Src="~/User_Controls/UC_Procesando.ascx" TagPrefix="uc1" TagName="UC_Procesando" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <div class="form-group col-md-12">
        <button runat="server" id="btnMostrarFiltros" class="btn btn-primary fa fa-filter" onserverclick="btnMostrarFiltros_Click">
        </button>
    </div>

    <div id="divFiltros" runat="server" class="form-group col-md-12 bg-info" visible="false">
        <asp:Label ID="lblUsuario" runat="server" Text="-Usuario">: </asp:Label>
        <asp:DropDownList ID="ddlUsuarios" runat="server"></asp:DropDownList>
        <asp:Label ID="lblFechaDesde" runat="server" Text="-Fecha desde">: </asp:Label>
        <asp:DropDownList ID="ddlFechaDesde" runat="server"></asp:DropDownList>
       <asp:Label ID="lblFechaHasta" runat="server" Text="-Fecha hasta">: </asp:Label>
        <asp:DropDownList ID="ddlFechaHasta" runat="server"></asp:DropDownList>
        <asp:Label ID="lblEvento" runat="server" Text="-Evento">: </asp:Label>
        <asp:DropDownList ID="ddlEventos" runat="server"></asp:DropDownList>
        <asp:Label ID="lblCriticidad" runat="server" Text="-Criticidad">: </asp:Label>
        <asp:DropDownList ID="ddlCriticidad" runat="server"></asp:DropDownList>
        <button id="btnFiltrar" runat="server" class="btn btn-info btn-sm fa fa-filter" onserverclick="btnFiltrar_Click"></button>
        <button id="btnLimpiarFiltros" runat="server" class="btn btn-info btn-sm fa fa-undo" onserverclick="btnLimpiarFiltros_Click"></button>
    </div>

    <div class="form-group col-md-12">
        <asp:Panel runat="server" ScrollBars="Vertical" Height="400px">
            <asp:GridView ID="grvBitacora" runat="server" AllowSorting="true" Caption="-Bitacora sistema"
                AutoGenerateColumns="False" EnableTheming="true" OnRowDataBound="grvBitacora_RowDataBound"
                OnSorting="OnSorting" >
                <AlternatingRowStyle BackColor="#CCFFFF" />
                <Columns>
                    <asp:BoundField DataField="Cod_Usuario" HeaderText="-Cod_Usuario!" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"></asp:BoundField>
                    <asp:BoundField DataField="Cod_Evento" HeaderText="-Cod_Evento!" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"></asp:BoundField>
                    <asp:BoundField DataField="CriticidadTexto" HeaderText="-Criticidad" ItemStyle-HorizontalAlign="Center" SortExpression="CriticidadTexto">
                        <HeaderStyle CssClass="th" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NombreUsuario" HeaderText="-Usuario" SortExpression="NombreUsuario">
                        <HeaderStyle CssClass="th" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DescripcionEvento" HeaderText="-Descripcion" SortExpression="DescripcionEvento">
                        <HeaderStyle CssClass="th" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FechaEvento" HeaderText="-Fecha Evento" SortExpression="FechaEvento">
                        <HeaderStyle CssClass="th" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Criticidad" HeaderText="-Criticidad!" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <hr />
    </div>

    <div class="form-row">
        <div class="form-group" style="text-align: right;">
            <button runat="server" id="btnExportarJson" class="btn btn-primary fa fa-file-code-o" onserverclick="btnExportarJson_Click"></button>
            <button runat="server" id="btnExportarXML" class="btn btn-primary fa fa-file" onserverclick="btnExportarXML_Click"></button>
            <button runat="server" id="btnExportarPDF" class="btn btn-primary fa fa-file-pdf-o" onserverclick="btnExportarPDF_Click"></button>
        </div>
    </div>

    <!-- Modal Mensaje -->
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
