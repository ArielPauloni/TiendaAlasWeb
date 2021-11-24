<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PacienteTratamiento.aspx.cs" Inherits="GUI.Negocio.ABMs.PacienteTratamiento" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div style="text-align: center;">
        <header>
            <asp:Label ID="lblTitle" runat="server" Text="-Asignar Tratamiento a Paciente" Font-Size="Large"></asp:Label>
        </header>
        <br />
    </div>

    <div class="container">
        <div class="form-row">
            <div class="form-group col-md-4">
                <asp:Label ID="lblPacientes" runat="server" Text="-Paciente"></asp:Label>
                <asp:DropDownList ID="ddlPacientes" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control form-control-select" OnSelectedIndexChanged="ddlPacientes_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="form-group col-md-4">
                <asp:Label ID="lblTratamiento" runat="server" Text="-Tratamiento"></asp:Label>
                <asp:DropDownList ID="ddlTratamiento" ClientIDMode="Static" runat="server" CssClass="form-control form-control-select"></asp:DropDownList>
            </div>
            <div class="form-group col-md-3">
                <asp:Label ID="lblConsultarTratamiento" runat="server" Text="-Tratamiento Recomendado"></asp:Label>
                <button runat="server" id="btnRecomendarTratamiento" class="btn btn-primary fa fa-question-circle form-control form-control-select" onserverclick="btnRecomendarTratamiento_ServerClick"></button>
            </div>

            <br />
            <div class="form-row">
                <div class="form-group col-md-6">
                    <button runat="server" id="btnGuardar" class="btn btn-primary fa fa-check-circle" onserverclick="btnGuardar_ServerClick"></button>
                </div>
            </div>
        </div>
        <br />
        <br />
        <div class="form-group col-md-12">
            <asp:GridView ID="grvTratamientos" runat="server" AllowSorting="True" Caption="-Tratamientos"
                AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="Cod_Tratamiento" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvTratamientos_PageIndexChanging" OnRowDataBound="grvTratamientos_RowDataBound">
                <AlternatingRowStyle BackColor="#CCFFFF" />
                <Columns>
                    <asp:TemplateField HeaderText="ID Tratamiento">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Cod_Tratamiento" runat="server" Text='<%#Eval("Cod_Tratamiento") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DescripcionTratamiento">
                        <ItemTemplate>
                            <asp:Label ID="lbl_DescripcionTratamiento" runat="server" Text='<%#Eval("DescripcionTratamiento") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ValorTotal" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ValorTotal" runat="server" Text='<%#"$" + Eval("ValorTotal") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Calificación" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Calificacion" runat="server" Text='<%#Eval("Calificacion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="-Activo" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Activo" runat="server" Enabled="false" Checked='<%#Eval("Activo") %>'></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- Modal Mensaje -->
    <div class="modal fade" id="MensajeModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <uc1:UC_MensajeModal runat="server" ID="UC_MensajeModal" />
        </div>
    </div>

    <script type="text/javascript">
        function mostrarMensaje() {
            $('#MensajeModal').modal({ backdrop: 'static', keyboard: false });
        }
    </script>
</asp:Content>
