<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CaracteristicaPaciente.aspx.cs" Inherits="GUI.Negocio.ABMs.Caracteristica" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div style="text-align: center;">
        <header>
            <asp:Label ID="lblTitle" runat="server" Text="-Características del paciente" Font-Size="Large"></asp:Label>
        </header>
        <br />
        <br />
    </div>

    <div class="form-group col-md-12">
        <asp:GridView ID="grvCaracteristicasPaciente" runat="server" AllowSorting="True" Caption="-Caracteristicas del Paciente"
            AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="cod_Paciente" PageSize="30" EnableTheming="True" OnPageIndexChanging="grvCaracteristicasPaciente_PageIndexChanging" OnRowCancelingEdit="grvCaracteristicasPaciente_RowCancelingEdit" OnRowEditing="grvCaracteristicasPaciente_RowEditing" OnRowUpdating="grvCaracteristicasPaciente_RowUpdating" OnRowDataBound="grvCaracteristicasPaciente_RowDataBound">
            <AlternatingRowStyle BackColor="#CCFFFF" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btn_Edit" class="btn btn-mini" CommandName="Edit"><i class="fa fa-edit" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton runat="server" ID="btn_Update" class="btn btn-mini" CommandName="Update"><i class="fa fa-check" aria-hidden="true"></i>
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btn_Undo" class="btn btn-mini" CommandName="Cancel"><i class="fa fa-undo" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lbl_cod_Paciente" runat="server" Text='<%#Eval("cod_Paciente") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Paciente">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Paciente" runat="server" Text='<%#Eval("Paciente") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Genero" HeaderStyle-CssClass="th">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Genero" runat="server" Text='<%#Eval("Genero") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddl_Genero" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Fuma" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_Fuma" runat="server" Enabled="false" Checked='<%#Eval("Fuma") %>'></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-DiasActividadDeportiva" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_DiasActividadDeportiva" runat="server" Text='<%#Eval("DiasActividadDeportiva") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddl_DiasActividadDeportiva" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-HorasRelax" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_HorasRelax" runat="server" Text='<%#Eval("HorasRelax") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddl_HorasRelax" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Edad" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Edad" runat="server" Text='<%#Eval("Edad") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
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
