<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="GUI.Servicios.Bitacora.Bitacora" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>


<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <div class="form-group col-md-12">
        <asp:Panel runat="server" ScrollBars="Vertical" Height="400px">
            <asp:GridView ID="grvBitacora" runat="server" AllowSorting="True" Caption="-Bitacora sistema"
                AutoGenerateColumns="False" EnableTheming="True" OnPageIndexChanging="grvBitacora_PageIndexChanging">
                <AlternatingRowStyle BackColor="#CCFFFF" />
                <%--AllowPaging="True" PageSize="20"--%>
                <Columns>
                    <asp:BoundField DataField="Cod_Usuario" HeaderText="-Cod_Usuario!"></asp:BoundField>
                    <asp:BoundField DataField="Cod_Evento" HeaderText="-Cod_Evento!"></asp:BoundField>
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
                    <asp:BoundField DataField="Criticidad" HeaderText="-Criticidad!"></asp:BoundField>
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
