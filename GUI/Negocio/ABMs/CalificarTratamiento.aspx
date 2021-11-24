<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CalificarTratamiento.aspx.cs" Inherits="GUI.Negocio.ABMs.CalificarTratamiento" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div style="text-align: center;">
        <header>
            <asp:Label ID="lblTitle" runat="server" Text="-Calificar Tratamiento" Font-Size="Large"></asp:Label>
        </header>
        <br />
    </div>

    <div class="container">
        <div class="form-group col-md-12">
            <asp:GridView ID="grvTratamiento" runat="server" AllowSorting="True" Caption="-Tratamiento"
                AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="Cod_Tratamiento" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvTratamiento_PageIndexChanging" OnRowCancelingEdit="grvTratamiento_RowCancelingEdit" OnRowEditing="grvTratamiento_RowEditing" OnRowUpdating="grvTratamiento_RowUpdating" OnRowDataBound="grvTratamiento_RowDataBound">
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
                            <asp:Label ID="lbl_Cod_Tratamiento" runat="server" Text='<%#Eval("Cod_Tratamiento") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="-DescripcionTratamiento">
                        <ItemTemplate>
                            <asp:Label ID="lbl_DescripcionTratamiento" runat="server" Text='<%#Eval("DescripcionTratamiento") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="-Calificacion" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Calificacion" runat="server" Text='<%#Eval("Calificacion") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddl_Calificacion" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
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
