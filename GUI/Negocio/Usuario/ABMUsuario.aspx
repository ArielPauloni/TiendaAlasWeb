<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ABMUsuario.aspx.cs" Inherits="GUI.Servicios.Usuarios.ABMUsuario" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_Procesando.ascx" TagPrefix="uc1" TagName="UC_Procesando" %>
<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />

    <div class="form-group col-md-12">
        <asp:GridView ID="grvUsuarios" runat="server" AllowSorting="True" Caption="-Usuarios" CssClass="table table-responsive"
            AutoGenerateColumns="False" AllowPaging="True" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvUsuarios_PageIndexChanging"
            OnRowCancelingEdit="grvUsuarios_RowCancelingEdit" OnRowEditing="grvUsuarios_RowEditing" OnRowUpdating="grvUsuarios_RowUpdating"
            OnRowDataBound="grvUsuarios_RowDataBound" OnRowDeleting="grvUsuarios_RowDeleting">
            <AlternatingRowStyle BackColor="#CCFFFF" />
            <EditRowStyle BackColor="Turquoise" />
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btn_Edit" CssClass="btn btn-mini" CommandName="Edit"><i class="fa fa-edit" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton runat="server" ID="btn_Update" CssClass="btn btn-mini" CommandName="Update"><i class="fa fa-check" aria-hidden="true"></i>
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btn_Undo" CssClass="btn btn-mini" CommandName="Cancel"><i class="fa fa-undo" aria-hidden="true"></i>
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btn_Delete" CssClass="btn btn-mini" CommandName="Delete"><i class="fa fa-trash" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Cod_Usuario" runat="server" Text='<%#Eval("Cod_Usuario") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Apellido" HeaderStyle-CssClass="th">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Apellido" runat="server" Text='<%#Eval("Apellido") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Apellido" runat="server" Text='<%#Eval("Apellido") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Nombre" HeaderStyle-CssClass="th">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Nombre" runat="server" Text='<%#Eval("Nombre") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Nombre" runat="server" Text='<%#Eval("Nombre") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Alias" HeaderStyle-CssClass="th">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Alias" runat="server" Text='<%#Eval("Alias") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Alias" runat="server" Text='<%#Eval("Alias") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Tipo Usuario" HeaderStyle-CssClass="th">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TipoUsuario" runat="server" Text='<%#Eval("TipoUsuario") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddl_TipoUsuario" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Telefono" HeaderStyle-CssClass="th">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Telefono" runat="server" Text='<%#Eval("Telefono") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Telefono" runat="server" Text='<%#Eval("Telefono") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Mail" HeaderStyle-CssClass="th">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Mail" runat="server" Text='<%#Eval("Mail") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Mail" runat="server" Text='<%#Eval("Mail") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Nacimiento" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_FechaNacimiento" runat="server" Text='<%#Eval("FechaNacimiento", "{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_FechaNacimiento" runat="server" TextMode="Date" Width="100%" Text='<%#Eval("FechaNacimiento") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Password">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Password" runat="server" Text='<%#Eval("Contraseña") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Inactivo">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Inactivo" runat="server" Text='<%#Eval("Inactivo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Bloqueado" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_Bloqueado" runat="server" Enabled="false" Checked='<%# int.Parse(Eval("IntentosEquivocados").ToString()) >= 3%>'></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IntentosEquivocados">
                    <ItemTemplate>
                        <asp:Label ID="lbl_IntentosEquivocados" runat="server" Text='<%#Eval("IntentosEquivocados") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UltimoLogin">
                    <ItemTemplate>
                        <asp:Label ID="lbl_UltimoLogin" runat="server" Text='<%#Eval("UltimoLogin") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <hr />
    </div>

    <div class="form-row">
        <div class="form-group" style="text-align: right;">
            <button runat="server" id="btnExportarPDF" class="btn btn-primary fa fa-file-pdf-o" onserverclick="btnExportarPDF_ServerClick"></button>
        </div>
    </div>

    <div class="form-group col-md-12">
        <br />
        <asp:Button ID="btnCrearNuevoUsuario" CssClass="btn btn-primary" runat="server" Text="-Crear Nuevo Usuario" OnClick="btnCrearNuevoUsuario_Click" />
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
